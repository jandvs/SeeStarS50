using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using SeeStarS50Lib.Models;

namespace SeeStarS50Lib.Libraries
{
    // All the code in this file is included in all platforms.
    public class SeeStarS50 : IDisposable
    {
        ////////////////////////////////////////////////////////////////////////////////
        // Private properties 
        ////////////////////////////////////////////////////////////////////////////////
        #region Private properties

        // Network retailed
        private readonly int _port = 4700;
        private readonly IPAddress _ip;
        private Socket _socket { get; set; }
        private Task _socketReader { get; set; }
        private CancellationToken _socketReaderCancellationToken { get; set; }

        // Commands related
        private int _cmdId { get; set; } = 999;
        private bool isWatchEvents { get; set; }
        private string OpState { get; set; }

        // Other
        private bool _isDebug { get; set; }

        private string lockStatus { get; set; }
        private object lockObj { get; set; }


        #endregion


        ////////////////////////////////////////////////////////////////////////////////
        // Public properties
        ////////////////////////////////////////////////////////////////////////////////
        #region Public propoerties

        //Not sure we need these
        public bool isConnected { get { return _socket.Connected; } }
        public Queue<string> EventQueue { get; set; }
        public Queue<string> CommandResponseQueue { get; set; }
        public double progress { get; set; }
        public string tileNumber { get; set; }
        public Target? currentTarget { get; set; }
        public bool SkipTarget { get; set; }
        public bool CancelAll { get; set; }

        #endregion


        ////////////////////////////////////////////////////////////////////////////////
        // Constructors and public methods 
        ////////////////////////////////////////////////////////////////////////////////
        #region Constructors and public methods

        /// <summary>
        /// Setup the connection to a SeeStarS50
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="debug"></param>
        public SeeStarS50(string ip, bool debug = false)
        {
            lockStatus = "";
            lockObj = new object();
            EventQueue = new Queue<string>();
            CommandResponseQueue = new Queue<string>();
            _isDebug = debug;
            _ip = new IPAddress(ip.Split('.').Select(s => byte.Parse(s)).ToArray());
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            progress = 0;
        }

        public void Dispose()
        {
            _socketReader.Dispose();
            _socket.Dispose();
        }

        public async Task Connect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await _socket.ConnectAsync(_ip, _port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                isWatchEvents = true;
                _socketReaderCancellationToken = new CancellationToken();
                _socketReader = new Task(ReceiveSocketMsgTask, _socketReaderCancellationToken);
                _socketReader.Start();
            }

        }

        public void Disconnect()
        {
            isWatchEvents = false;
            _socketReader.Wait(_socketReaderCancellationToken);
            Thread.Sleep(_socket.ReceiveTimeout + 1000);
            _socket.Close();
        }

        public async Task InvokeInstruction(string instruction)
        {
            CmdData data = new CmdData() { id = _cmdId++.ToString(), method = instruction };
            string jsonDataString = JsonSerializer.Serialize(data!, SourceGenerationContext.Default.CmdData);
            //if (_isDebug)
            //    Console.WriteLine($"Sending {jsonDataString}");
            jsonDataString = jsonDataString.Replace("\r\n", "");
            await SendCommand(jsonDataString + "\r\n");
        }

        public async Task InvokeInstruction(CmdData data)
        {
            if (data != null)
            {
                string jsonDataString = JsonSerializer.Serialize(data!, SourceGenerationContext.Default.CmdData);
                //if (_isDebug)
                //    Console.WriteLine($"Sending {jsonDataString}");
                jsonDataString = jsonDataString.Replace("\r\n", "");
                await SendCommand(jsonDataString + "\r\n");
            }
        }

        public async Task<string> GotoTarget(double ra, double dec, string targetName, byte is_lp_filter)
        {
            //Console.WriteLine($"Going to target. ra: {ra}, dec: {dec}");
            double[] radec = new double[] { ra, dec };
            JsonParams parameters = new JsonParams() { mode = "star", targetRaDec = radec, targetName = targetName, lpFilter = is_lp_filter };
            CmdData data = new CmdData() { id = _cmdId++.ToString(), method = "iscope_start_view", parameters = parameters };

            await InvokeInstruction(data);
            await WaitEndOp();
            Thread.Sleep(2000);
            return OpState;
        }

        public async Task CancelGotoTarget()
        {
            JsonParams parameters = new JsonParams() { stage = "View" };
            CmdData data = new CmdData() { id = _cmdId++.ToString(), method = "iscope_stop_view", parameters = parameters };

            await InvokeInstruction(data);
        }

        public async Task StartStack(double sessionTime, byte tilenumber, byte numbertiles, int subExposure)
        {
            int tempExposure = subExposure;
            if (tempExposure > 30) tempExposure = 30;
            else if (tempExposure > 20) tempExposure = 20;
            else if (tempExposure > 10) tempExposure = 10;
            else if (tempExposure > 5) tempExposure = 5;
            else if (tempExposure > 2) tempExposure = 2;
            else if (tempExposure > 1) tempExposure = 1;

            await SendCommand( "{ \"id\":" + _cmdId++.ToString() + ",\"method\":\"set_setting\",\"params\":{ \"exp_ms\":{ \"stack_l\":"+ tempExposure + "000, \"continuous\":1000} } }\r\n" );
            Thread.Sleep(1000);

            //Console.WriteLine("starting to stack...");
            JsonParams parameters = new JsonParams() { restart = true };
            CmdData data = new CmdData() { id = _cmdId++.ToString(), method = "iscope_start_stack", parameters = parameters };

            await InvokeInstruction(data);
            Thread.Sleep(1000);
            await SendCommand( "{ \"id\":" + _cmdId++.ToString() + ",\"method\":\"set_setting\",\"params\":{ \"exp_ms\":{ \"stack_l\":" + subExposure + "000, \"continuous\":1000} } }\r\n" );
            await sleep_with_heartbeat(sessionTime, tilenumber, numbertiles);
        }

        public async Task StopStack()
        {
            Console.WriteLine("stop to stacking...");
            JsonParams parameters = new JsonParams() { stage = "Stack" };
            CmdData data = new CmdData() { id = _cmdId++.ToString(), method = "iscope_stop_view", parameters = parameters };

            await InvokeInstruction(data);
        }

        #endregion



        ////////////////////////////////////////////////////////////////////////////////
        // Private methods
        ////////////////////////////////////////////////////////////////////////////////
        #region Private methods

        /// <summary>
        /// Reconnect in the case that the connection was lost.
        /// </summary>
        /// <returns></returns>
        private async Task Reconnect()
        {
            lock (lockObj)
            {
                if (lockStatus == "locked")
                    return;
                lockStatus = "locked";
            }

            if (!_socket.Connected)
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await _socket.ConnectAsync(_ip, _port);
                Thread.Sleep(100);
            }
            lockStatus = "";
        }

        private async Task SendCommand(string command) // send_message
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(command);
                _socket.Send(data);
            }
            catch (Exception ex)
            {
                await Reconnect();
                await SendCommand(command);
            }
        }

        private async Task<string> GetResponse() //get_socket_msg
        {
            byte[] data = new byte[1024 * 60];
            try
            {
                int receivedDataLength = _socket.Receive(data);
                string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                Console.WriteLine(stringData);
                return stringData;
            }
            catch (Exception ex)
            {
                await Reconnect();
                return await GetResponse();
            }
        }

        private async void ReceiveSocketMsgTask()
        {
            string msgRemainder = "";
            while (isWatchEvents)
            {
                try
                {
                    if (!isConnected)
                        await Reconnect();
                    if (isConnected)
                    {
                        var data = await GetResponse();
                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            msgRemainder += data;
                            var firstIndex = msgRemainder.IndexOf("\r\n");
                            while (firstIndex > 0)
                            {
                                var firstMsg = msgRemainder.Substring(0, firstIndex);
                                msgRemainder = msgRemainder.Substring(firstIndex + 2);

                                if (firstMsg.StartsWith("{\"Event\""))
                                {
                                    EventQueue.Enqueue(firstMsg);
                                    // Handle an Event response.
                                    var parsedData = JsonSerializer.Deserialize(firstMsg!, SourceGenerationContext.Default.EventResponse);
                                    if (parsedData?.Event == "AutoGoto" && (parsedData.state == "complete" || parsedData.state == "fail"))
                                        OpState = parsedData.state;
#if DEBUG
                                    // These lines are for testing.. It makes the app think that the goto operation completed successfully.
                                    // So you can run a full test indoors.
                                    //if (parsedData?.Event == "AutoGoto")
                                    //    OpState = "complete";
#endif
                                }
                                else
                                {
                                    // Handle a Command response.
                                    try
                                    {
                                        CommandResponseQueue.Enqueue(firstMsg);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                                firstIndex = msgRemainder.IndexOf("\r\n");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Thread.Sleep(100);
            }
        }

        private async Task WaitEndOp()
        {
            OpState = "working";
            int heartbeatTimer = 0;
            while (OpState == "working")
            {
                if (SkipTarget)
                    break;
                heartbeatTimer++;
                if (heartbeatTimer > 5)
                {
                    heartbeatTimer = 0;
                    await InvokeInstruction("test_connection");
                }
                Thread.Sleep(1000);
            }
        }

        private async Task sleep_with_heartbeat(double TimePerTile, byte TileNumber, byte NumberTiles)
        {
            double OneTileTimeInSeconds = TimePerTile * 3600; // convert hours to seconds.
            double AllTilesTimeInSeconds = OneTileTimeInSeconds * NumberTiles;
            TileNumber--;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            double ElapsedSeconds = 0;
            while (ElapsedSeconds < OneTileTimeInSeconds) // compare elapsed seconds to session time in seconds 
            {
                if (SkipTarget)
                    break;

                ElapsedSeconds= sw.ElapsedMilliseconds / 1000;
                progress = ((OneTileTimeInSeconds * TileNumber + ElapsedSeconds) / AllTilesTimeInSeconds)*100;
                if (ElapsedSeconds % 5 == 0)
                    await InvokeInstruction("test_connection");
                Thread.Sleep(100);
            }
        }

#endregion







        public static async Task SeeStarRun(Target target, SeeStarS50 telescope)
        {
            double deltaRA = 0.06;
            double deltaDec = 0.9;

            deltaRA *= target.mRA;
            deltaDec *= target.mDec;

            // Adjust mosaic center if num panels is even
            if (target.nRA % 2 == 0)
                target.RA += deltaRA / 2;
            if (target.nDec % 2 == 0)
                target.Dec += deltaDec / 2;

            int mosaicIndex = 0;

            double cur_ra = target.RA - ((int)target.nRA / 2) * deltaRA;

            string save_target_name = target.Name;
            telescope.progress = 0;

            foreach (var index_ra in Enumerable.Range(0, target.nRA))
            {
                double cur_dec = target.Dec - ((int) target.nDec / 2) * deltaDec;
                foreach (var index_dec in Enumerable.Range(0, target.nDec))
                {
                    if (target.nRA != 1 || target.nDec != 1)
                    {
                        save_target_name = $"{target.Name}_{index_ra + 1}{index_dec + 1}";
                        telescope.tileNumber = $"Tile: {index_ra + 1}:{index_dec + 1}";
                    }
                    else
                    {
                        telescope.tileNumber = "";
                    }
                    Console.WriteLine("goto {cur_ra}, {cur_dec}");
                    await telescope.GotoTarget(cur_ra, cur_dec, save_target_name, target.LPFilter);
                    if (telescope.SkipTarget)
                    {
                        await telescope.CancelGotoTarget();
                        Thread.Sleep(3000);
                        return;
                    }
                    Thread.Sleep(3000);
                    //await telescope.WaitEndOp();
                    //Console.WriteLine("Goto operation finished");
                    if (telescope.OpState == "complete")
                    {
                        await telescope.StartStack(target.SessionTime, (byte)((index_ra * target.nRA + index_dec) + 1), (byte)(target.nRA * target.nDec), target.SubExposure);
                        //await telescope.sleep_with_heartbeat(target.SessionTime * 3600);
                        await telescope.StopStack();
                        //Console.WriteLine($"Stacking operation finished {save_target_name}");
                    }
                    else
                    {
                        //Console.WriteLine($"Goto operation failed.");
                    }

                    cur_dec += deltaDec;
                    mosaicIndex++;
                }
                cur_ra += deltaRA;
            }
            telescope.tileNumber = "";
            telescope.progress = 100;
        }
    }
}
