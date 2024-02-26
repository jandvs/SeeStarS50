using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SeeStarS50Lib.Models;

namespace SeeStarS50Lib
{

    public class SeeStarS50: IDisposable
    {
        // Public Properties
        public bool isWatchEvents { get; set; } = true;
        public string OpState { get; set; } = "";
        public int SessionTime {get; set;}

        // Private Proterties
        private const int _port = 4700;
        private readonly IPAddress _ip;
        private Socket _socket { get; set; }
        private bool _isDebug {get;set;}
        private int _cmdId { get; set; }


        public SeeStarS50(string ip, int cmdId = 999, bool debug = false)
        {
            _ip = new IPAddress(ip.Split('.').Select(s => byte.Parse(s)).ToArray());
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(_ip, _port);
            _isDebug = debug;
            _cmdId = 999;
        }

        public void Dispose()
        {
            isWatchEvents = false;
            _socket.Close();
            _socket.Dispose();
        }

        public void SendCommand(string command) // send_message
        {
            try
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                _socket.Send(data);
            } catch (Exception ex)
            {
                //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(_ip, _port);
                SendCommand(command);
            }
        }

        public string GetResponse() //get_socket_msg
        {
            byte[] data = new byte[1024 * 60];
            try
            {
                int receivedDataLength = _socket.Receive(data);
                string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                Console.WriteLine(stringData);
                return stringData;
            } catch (Exception ex)
            {
                //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(_ip, _port);
                return GetResponse();
            }
        }

        public void ReceiveMessageThreadFn()
        {
            string msgRemainder = "";
            while (isWatchEvents)
            {
                var data = GetResponse();
                if (!string.IsNullOrWhiteSpace(data))
                {
                    msgRemainder += data;
                    var firstIndex = msgRemainder.IndexOf("\r\n");
                    while (firstIndex > 0)
                    {
                        var firstMsg = msgRemainder.Substring(0, firstIndex);
                        msgRemainder = msgRemainder.Substring(firstIndex + 2);
                        var parsedData = JsonSerializer.Deserialize<JsonReturn>(firstMsg!, SourceGenerationContext.Default.JsonReturn);
                        if (parsedData != null && parsedData.Event == "AutoGoto")
                        {
                            if (parsedData.state == "complete" || parsedData.state == "fail")
                            {
                                OpState = parsedData.state;
                            }
                        }

                        if (_isDebug)
                        {
                            Console.WriteLine(parsedData);
                        }

                        firstIndex = msgRemainder.IndexOf("\r\n");
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public void jsonMessage(string instruction)
        {
            JsonData data = new JsonData() { id = _cmdId++.ToString(), method = instruction };
            string jsonDataString = JsonSerializer.Serialize(data!, SourceGenerationContext.Default.JsonData);
            if (_isDebug)
                Console.WriteLine($"Sending {jsonDataString}");
            jsonDataString = jsonDataString.Replace("\r\n", "");
            SendCommand(jsonDataString + "\r\n");
        } 

        public void jsonMessage2(JsonData data)
        {
            if (data != null)
            {
                string jsonDataString = JsonSerializer.Serialize<JsonData>(data!, SourceGenerationContext.Default.JsonData);
                if (_isDebug)
                    Console.WriteLine($"Sending {jsonDataString}");
                jsonDataString = jsonDataString.Replace("\r\n", "");
                SendCommand(jsonDataString + "\r\n");
            }
        }

        public void GotoTarget(decimal ra, decimal dec, string targetName, bool is_lp_filter)
        {
            Console.WriteLine($"Going to target. ra: {ra}, dec: {dec}");

            decimal[] radec = new decimal[] { ra, dec };
            JsonParams parameters = new JsonParams() { mode = "star", targetRaDec = radec, targetName = targetName, lpFilter = is_lp_filter };
            JsonData data = new JsonData() { id = _cmdId++.ToString(), method = "iscope_start_view", parameters = parameters };

            jsonMessage2(data);
        }

        public void StartStack()
        {
            Console.WriteLine("starting to stack...");
            JsonParams parameters = new JsonParams() { restart = true };
            JsonData data = new JsonData() { id = _cmdId++.ToString(), method = "iscope_start_stack", parameters = parameters };

            jsonMessage2(data);
        }

        public void StopStack()
        {
            Console.WriteLine("stop to stacking...");
            JsonParams parameters = new JsonParams() { stage = "Stack" };
            JsonData data = new JsonData() { id = _cmdId++.ToString(), method = "iscope_stop_view", parameters = parameters };

            jsonMessage2(data);
        }

        public void WaitEndOp()
        {
            OpState = "working";
            int heartbeatTimer = 0;
            while (OpState == "working")
            {
                heartbeatTimer++;
                if (heartbeatTimer > 5)
                {
                    heartbeatTimer = 0;
                    jsonMessage("test_connection");
                }
                Thread.Sleep(1000);
            }
        }

        public void sleep_with_heartbeat()
        {
            int stacking_timer = 0;
            while (stacking_timer < SessionTime) // stacking time per segment
            {
                if (++stacking_timer % 5 == 0)
                    jsonMessage("test_connection");
                Thread.Sleep(1000);
            }
        }


        public static void SeeStarRun(string HOST, string TargetName, decimal centerRA, decimal centerDec, bool is_use_LP_filter, int sessionTime, int nRA, int nDec, int mRA, int mDec, bool debug)
        {
            if (nRA < 1 || nDec < 0)
            {
                Console.Write("Mosaic size is invalid");
                return;
            }

            Console.WriteLine($"nRA: {nRA}, nDec: {nDec}");
            decimal deltaRA = 0.06M;
            decimal deltaDec = 0.9M;

            using (SeeStarS50 telescope = new SeeStarS50(HOST, 999, true))
            {
                if (centerRA < 0)
                {
                    telescope.jsonMessage("scope_get_equ_coord");
                    var data = telescope.GetResponse();
                    var parsedData = JsonSerializer.Deserialize<JsonData>(data);
                    var data_result = parsedData.result;
                    centerRA = data_result.ra;
                    centerDec = data_result.dec;
                    Console.WriteLine($"Center RA: {centerRA}, Center Dec: {centerDec}");
                }

                // print input requests  
                Console.WriteLine("received parameters:");
                Console.WriteLine($"  ip address    : {HOST}");
                Console.WriteLine($"  target        : {TargetName}");
                Console.WriteLine($"  RA            : {centerRA}");
                Console.WriteLine($"  Dec           : {centerDec}");
                Console.WriteLine($"  use LP filter : {is_use_LP_filter}");
                Console.WriteLine($"  session time  : {sessionTime}");
                Console.WriteLine($"  RA num panels : {nRA}");
                Console.WriteLine($"  Dec num panels: {nDec}");
                Console.WriteLine($"  RA offset x   : {mRA}");
                Console.WriteLine($"  Dec offset x  : {mDec}");

                deltaRA *= mRA;
                deltaDec *= mDec;

                // Adjust mosaic center if num panels is even
                if (nRA % 2 == 0)
                    centerRA += deltaRA / 2;
                if (nDec % 2 == 0)
                    centerDec += deltaDec / 2;

                Task get_msg_thread = new Task(telescope.ReceiveMessageThreadFn);
                get_msg_thread.Start();

                int mosaicIndex = 0;

                decimal cur_ra = centerRA - (nRA/2) * deltaRA;

                string save_target_name = TargetName;

                foreach(var index_ra in Enumerable.Range(0,nRA))
                {
                    decimal cur_dec = centerDec - (nDec/2) * deltaDec;
                    foreach (var index_dec in Enumerable.Range(0,nDec))
                    {
                        if (nRA != 1 || nDec != 1)
                            save_target_name = $"{TargetName}_{index_ra+1}{index_dec+1}";
                        Console.WriteLine("goto {cur_ra}, {cur_dec}");
                        telescope.GotoTarget(cur_ra, cur_dec, save_target_name, is_use_LP_filter);
                        telescope.WaitEndOp();
                        Console.WriteLine("Goto operation finished");
                        Thread.Sleep(3000);
                        if (telescope.OpState == "complete")
                        {
                            telescope.StartStack();
                            telescope.sleep_with_heartbeat();
                            telescope.StopStack();
                            Console.WriteLine($"Stacking operation finished {save_target_name}");
                        }
                        else
                        {
                            Console.WriteLine($"Goto operation failed.");
                        }

                        cur_dec += deltaDec;
                        mosaicIndex++;
                    }
                    cur_ra += deltaRA;
                }


            }

            Console.WriteLine("finished SeeStarRun.");

            
        }
    }


}
