using System.Net;
using System.Text.Json;
using SeeStarS50Lib;
using SeeStarS50Lib.Models;

namespace SeeStarS50GUI
{
    public partial class Form1 : Form
    {
        SeeStarS50 telescope;
        Task listenTask;
        Task captureTask;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mtxtIpAddress.ValidatingType = typeof(IPAddress);
            btnSyncRaDec.Enabled = false;
            btnStart.Enabled = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists("IPAddresses.config"))
            {
                var AllIps = File.ReadAllLines("IPAddresses.config");
                if (AllIps.Length > 0)
                {
                    mtxtIpAddress.Text = AllIps[0];
                    btnConnect_Click(sender, e);
                }
            }
            else
            {
                mtxtIpAddress.Focus();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                telescope = new SeeStarS50(mtxtIpAddress.Text.Replace(" ", ""), 999, false);
                mtxtIpAddress.Enabled = false;
                btnConnect.Enabled = false;
                btnSyncRaDec.Enabled = true;
                btnStart.Enabled = true;

                if (listenTask == null)
                    listenTask = new Task(() => telescope.ReceiveMessageThreadFn());

                if (listenTask.Status != TaskStatus.Running)
                {
                    listenTask.Start();
                }

                //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                File.WriteAllLines("IPAddresses.config", new string[] { mtxtIpAddress.Text });
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        private void btnSyncRaDec_Click(object sender, EventArgs e)
        {
            telescope.jsonMessage("scope_get_equ_coord");
            JsonData parsedData;
            while (telescope.ResponseQueue.Count == 0)
                Thread.Sleep(10);
            parsedData = telescope.ResponseQueue.Dequeue();
            mtxtRA.Text = convertRaFromDecimal(parsedData.result.ra);
            mtxtDec.Text = convertDecFromDecimal(parsedData.result.dec);

            /*
            var data = telescope.GetResponse();
            while (!data.StartsWith("{\"jsonrpc"))
                data = data.Substring(data.IndexOf("\r\n") + 2);
            var parsedData = JsonSerializer.Deserialize<JsonData>(data!, SourceGenerationContext.Default.JsonData);
            var data_result = parsedData.result;
            mtxtRA.Text = convertRaFromDecimal(data_result.ra);
            mtxtDec.Text = convertDecFromDecimal(data_result.dec);
            */

            //decimal ra = convertRaToDecimal(mtxtRA.Text);
            //decimal dec = convertDecToDecimal(mtxtDec.Text);
        }

        public string convertRaFromDecimal(decimal ra)
        {
            int raHours = (int)ra;
            int raMinutes = (int)((ra - raHours) * 60);
            decimal raSeconds = ((((ra - raHours) * 60) - raMinutes) * 60);
            return $"{raHours:00}h {raMinutes:00}m {raSeconds:00.0000}s";
        }
        public string convertDecFromDecimal(decimal dec)
        {
            int decSign = Math.Sign(dec);
            dec = Math.Abs(dec);
            int decDegrees = (int)dec;
            int decMinutes = (int)((dec - decDegrees) * 60);
            decimal decSeconds = ((((dec - decDegrees) * 60) - decMinutes) * 60);
            return $"{(decSign > 0 ? "+" : "-")}{decDegrees:00}° {decMinutes:00}' {decSeconds:00.0000}\"";
        }
        public decimal convertRaToDecimal(string ra)
        {
            // ##h ##m ##s
            string[] raParts = ra.Split(' ');
            int raHours = int.Parse(raParts[0].Substring(0, raParts[0].Length - 1));
            int raMinutes = int.Parse(raParts[1].Substring(0, raParts[1].Length - 1));
            decimal raSeconds = decimal.Parse(raParts[2].Substring(0, raParts[2].Length - 1));
            return raHours + raMinutes / 60m + raSeconds / 3600m;
        }

        public decimal convertDecToDecimal(string dec)
        {
            // ###° ##' ##"
            string[] decParts = dec.Split(' ');
            int DecDegrees = int.Parse(decParts[0].Substring(0, decParts[0].Length - 1));
            int DecMinutes = int.Parse(decParts[1].Substring(0, decParts[1].Length - 1));
            decimal DecSeconds = decimal.Parse(decParts[2].Substring(0, decParts[2].Length - 1));
            int decSign = Math.Sign(DecDegrees);
            DecDegrees = Math.Abs(DecDegrees);
            return (DecDegrees + DecMinutes / 60m + DecSeconds / 3600m) * decSign;
        }

        public decimal convertTimeToDecimal(string time)
        {
            // ## Hours ## Minutes
            string newtime = time.Replace("Hours", "").Replace("Minutes", "").Trim();
            while (newtime.Contains("  "))
                newtime = newtime.Replace("  ", " ");

            string[] timeParts = newtime.Split(' ');
            int timeHours = int.Parse(timeParts[0]);
            int timeMinutes = int.Parse(timeParts[1]);
            return timeHours + timeMinutes / 60m;
        }

        public string convertTimeFromDecimal(decimal time)
        {
            int timeHours = (int)time;
            int timeMinutes = (int)((time - timeHours) * 60);
            return $"{timeHours:00}h {timeMinutes:00}m";
        }

        public void CheckState()
        {

            if (telescope == null || !telescope.isConnected)
            {
                mtxtIpAddress.Enabled = true;
                btnConnect.Enabled = true;
                btnSyncRaDec.Enabled = false;
                btnStart.Enabled = false;
            }
            else
            {
                btnStart.Enabled = captureTask?.Status != TaskStatus.Running;
                mtxtIpAddress.Enabled = false;
                btnConnect.Enabled = false;
                btnSyncRaDec.Enabled = true;
                string message;
                if (telescope.EventQueue.TryDequeue(out message))
                {
                    lstEvents.Items.Insert(0, message);
                }
            }

            if (lstTargetList.Items.Count == 0)
                btnStart.Enabled = false;

            if (lstEvents.SelectedItems.Count == 0)
                toolEventsCopy.Visible = false;
            else
                toolEventsCopy.Visible = true;

            if (lstTargetList.SelectedItems.Count == 0)
            {
                toolTargetsRemove.Visible = false;
            }
            else
            {
                toolTargetsRemove.Visible = true;
            }

            if (lstTargetList.Items.Count == 0)
            {
                toolTargetsClearAll.Visible = false;
                toolTargetsSaveAll.Visible = false;
                toolTargetsLoadLastList.Visible = File.Exists("TargetList.config");
            }
            else
            {
                toolTargetsSaveAll.Visible = true;
                toolTargetsClearAll.Visible = true;
                toolTargetsLoadLastList.Visible = false;
            }

            bool enableAdd = true;
            if (txtmDec.Text == "")
                enableAdd = false;
            decimal dummy;
            try { convertRaToDecimal(mtxtRA.Text); } catch { enableAdd = false; }
            try { convertDecToDecimal(mtxtDec.Text); } catch { enableAdd = false; }
            try { convertTimeToDecimal(mtxtSessionTime.Text); } catch { enableAdd = false; }
            if (!decimal.TryParse(txtnRA.Text, out dummy)) enableAdd = false;
            if (!decimal.TryParse(txtnDec.Text, out dummy)) enableAdd = false;
            if (!decimal.TryParse(txtmRA.Text, out dummy)) enableAdd = false;
            if (!decimal.TryParse(txtmDec.Text, out dummy)) enableAdd = false;
            btnAddToList.Enabled = enableAdd;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckState();
        }

        private void cmsEvents_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolEventsCopy)
            {
                string copyText = "";
                foreach (var item in lstEvents.SelectedItems)
                {
                    copyText += item.ToString();// + "\r\n";
                }
                Clipboard.SetText(copyText);
            }
            else if (e.ClickedItem == toolEventsClear)
            {
                lstEvents.Items.Clear();
            }
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            Target target = new Target();
            target.Name = txtTargetName.Text;
            target.RA = convertRaToDecimal(mtxtRA.Text);
            target.Dec = convertDecToDecimal(mtxtDec.Text);
            target.LPFilter = (byte)(chkLPFilter.Checked ? 1:0);
            target.SessionTime = convertTimeToDecimal(mtxtSessionTime.Text);
            target.nRA = int.Parse(txtnRA.Text);
            target.nDec = int.Parse(txtnRA.Text);
            target.mRA = decimal.Parse(txtmRA.Text);
            target.mDec = decimal.Parse(txtmRA.Text);
            lstTargetList.Items.Add(
                new ListViewItem(
                    new string[] {
                        target.Name,
                        mtxtRA.Text + " / " + mtxtDec.Text,
                        target.LPFilter == 1 ? "Y":"N",
                        mtxtSessionTime.Text,
                        txtnRA.Text + " " + txtnDec.Text + " " + txtmRA.Text + " " + txtmDec.Text,
                        JsonSerializer.Serialize(target!, SourceGenerationContext.Default.Target)
             }));
        }

        private void cmsTargets_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolTargetsRemove)
            {
                foreach (ListViewItem item in lstTargetList.SelectedItems)
                {
                    lstTargetList.Items.Remove(item);
                }
            }
            else if (e.ClickedItem == toolTargetsClearAll)
            {
                lstTargetList.Items.Clear();
            }
            else if (e.ClickedItem == toolTargetsSaveAll)
            {
                if (File.Exists("TargetList.config") && MessageBox.Show("Overwrite existing Target List?", "Save Target List", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                string[] allTargets = new string[lstTargetList.Items.Count];
                foreach (ListViewItem item in lstTargetList.Items)
                {
                    allTargets[lstTargetList.Items.IndexOf(item)] = item.SubItems[5].Text.Replace("\r\n", "");
                }
                File.WriteAllLines("TargetList.config", allTargets);
                MessageBox.Show("Target List Saved!", "Save Target List");
            }
            else if (e.ClickedItem == toolTargetsLoadLastList)
            {
                if (!File.Exists("TargetList.config"))
                {
                    return;
                }
                string[] allTargets = File.ReadAllLines("TargetList.config");
                foreach (var targetItem in allTargets)
                {
                    Target target = JsonSerializer.Deserialize<Target>(targetItem, SourceGenerationContext.Default.Target);
                    lstTargetList.Items.Add(
                       new ListViewItem(
                          new string[]
                              {
                                target.Name,
                                convertRaFromDecimal(target.RA) + " / " + convertDecFromDecimal(target.Dec),
                                target.LPFilter == 1 ? "Y":"N",
                                convertTimeFromDecimal(target.SessionTime),
                                target.nRA + " " + target.nDec + " " + target.mRA + " " + target.mDec,
                                JsonSerializer.Serialize(target!, SourceGenerationContext.Default.Target)
                     }));
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            List<string> allTargets = new List<string>();
            foreach (ListViewItem item in lstTargetList.Items)
            {
                allTargets.Add(item.SubItems[5].Text);
            }

            btnStart.Enabled = false;
            captureTask = new Task(() =>
            {
                foreach (string item in allTargets)
                {
                    Target target = JsonSerializer.Deserialize<Target>(item, SourceGenerationContext.Default.Target);
                    if (target != null)
                    {
                        SeeStarS50.SeeStarRun(target, telescope);
                    }
                }
            });
            captureTask.Start();
            btnStart.Enabled = false;
        }
    }
}
