using System.Net;
using System.Text.Json;
using SeeStarS50Lib.Libraries;
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
            /*
            double ra = (double) FrontendSupport.convertRaToDouble("10h 09m 39.5500s");
            double Dec =(double) FrontendSupport.convertDecToDouble("+11° 50' 59.4000\"");
            double alt = (double) FrontendSupport.convertDecToDouble("+42° 16' 02.9000\"");
            double az = (double) FrontendSupport.convertDecToDouble("+104° 51' 41.3000\"");

            var testlat = FrontendSupport.convertDecFromDouble((double)32.861457042778945);
            var testlong = FrontendSupport.convertDecFromDouble((double)-96.97981101312875);
            var altaz = FrontendSupport.ConvertEquatorialToAltAz(ra, Dec, DateTime.Parse("2/19/2024 21:31:21"), -96.97981101312875, 32.861457042778945);
            var RaDec = FrontendSupport.ConvertAltAzToEquatorial(alt, az, DateTime.Parse("2/19/2024 21:31:21"), -96.97981101312875, 32.861457042778945);
            var newra = FrontendSupport.convertRaFromDouble((double)RaDec.Item1);
            var newdec = FrontendSupport.convertDecFromDouble((double)RaDec.Item2);
            */

            mtxtIpAddress.ValidatingType = typeof(IPAddress);
            btnStart.Enabled = false;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists("StellariumIP.config"))
            {
                var AllIps = File.ReadAllLines("StellariumIP.config");
                if (AllIps.Length > 0)
                {
                    mtxtStelleriumIP.Text = AllIps[0];
                }
            }
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
            lblTargetProgress.Text = "";
            lblTileNumber.Text = "";

            cmsSync.Items.Insert(0, new ToolStripLabel(cmsSync.Text) { Font = new Font(DefaultFont, FontStyle.Bold) });


            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(mtxtRA, "Coords should be enters as JNow. Right click to pull Coords from Seestar or Stellarium.");
            toolTip.SetToolTip(mtxtDec, "Coords should be enters as JNow. Right click to pull Coords from Seestar or Stellarium.");
            toolTip.SetToolTip(lstTargetList, "Right click to manage targets.");
            toolTip.SetToolTip(lstEvents, "Right click to manage events.");

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                telescope = new SeeStarS50(mtxtIpAddress.Text.Replace(" ", ""), false);
                telescope.Connect();
                mtxtIpAddress.Enabled = false;
                btnConnect.Enabled = false;

                

                //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                File.WriteAllLines("IPAddresses.config", new string[] { mtxtIpAddress.Text });
            }
            catch (Exception ex)
            {
                // Log error
            }

        }

        public void CheckState()
        {
            if (telescope?.currentTarget != null)
            {
                if (!lblTargetProgress.Text.EndsWith(telescope.currentTarget.Name))
                {
                    prgProgress.Value = 0;
                    lblTargetProgress.Text = "Current Target: " + telescope.currentTarget.Name;
                }
                lblTileNumber.Text = telescope.tileNumber;
                if (telescope.progress < 0)
                    prgProgress.Value = 0;
                else if (telescope.progress >= 100)
                {
                    prgProgress.Value = 100;
                }
                else
                    prgProgress.Value = (int)telescope.progress;
            }

            if (telescope == null || !telescope.isConnected)
            {
                mtxtIpAddress.Enabled = true;
                btnConnect.Enabled = true;
                btnStart.Enabled = false;
                btnSkipTarget.Enabled = false;
                btnCancelAll.Enabled = false;
            }
            else
            {
                if (captureTask?.Status != TaskStatus.Running)
                {
                    prgProgress.Value = 0;
                    lblTargetProgress.Text = "";
                    lblTileNumber.Text = "";
                    btnSkipTarget.Enabled = false;
                    btnCancelAll.Enabled = false;
                }
                else
                {
                    btnSkipTarget.Enabled = true;
                    btnCancelAll.Enabled = true;
                }
                btnStart.Enabled = captureTask?.Status != TaskStatus.Running;
                mtxtIpAddress.Enabled = false;
                btnConnect.Enabled = false;
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
                toolTargetEdit.Visible = false;
                toolTargetCopy.Visible = false;
            }
            else
            {
                toolTargetsRemove.Visible = true;
                toolTargetEdit.Visible = true;
                toolTargetCopy.Visible = true;
            }

            if (lstTargetList.Items.Count == 0)
            {
                toolTargetsClearAll.Visible = false;
                toolTargetsSaveAll.Visible = false;
                toolTargetsLoadLastList.Visible = true;
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
            double dummy;
            int dummyint;
            //TODO: make target names unique. //if (lstTargetList.Items[0].SubItems.ContainsKey(txtTargetName.Text.Trim())) { enableAdd = false; }
            if (txtTargetName.Text.Trim() == "") { enableAdd = false; }
            if (FrontendSupport.convertRaToDouble(mtxtRA.Text) == null) { enableAdd = false; }
            if (FrontendSupport.convertDecToDouble(mtxtDec.Text) == null) { enableAdd = false; }
            if (FrontendSupport.convertTimeToDouble(mtxtSessionTime.Text) == null) { enableAdd = false; }
            if (!double.TryParse(txtnRA.Text, out dummy)) enableAdd = false;
            if (!double.TryParse(txtnDec.Text, out dummy)) enableAdd = false;
            if (!double.TryParse(txtmRA.Text, out dummy)) enableAdd = false;
            if (!double.TryParse(txtmDec.Text, out dummy)) enableAdd = false;
            if (!int.TryParse(txtSubExposure.Text, out dummyint)) enableAdd = false;
            btnAddToList.Enabled = enableAdd;


            if (mtxtStelleriumIP.Text == "") // || !(telescope?.isConnected ?? false))
                toolSyncFromStellariumObject.Visible = false;
            else
                toolSyncFromStellariumObject.Visible = true;
            if (telescope?.isConnected ?? false)
                toolSyncFromSeeStar.Visible = true;
            else
                toolSyncFromSeeStar.Visible = false;
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
            target.RA = (double)FrontendSupport.convertRaToDouble(mtxtRA.Text);
            target.Dec = (double)FrontendSupport.convertDecToDouble(mtxtDec.Text);
            target.LPFilter = (byte)(chkLPFilter.Checked ? 1 : 0);
            target.SessionTime = (double)FrontendSupport.convertTimeToDouble(mtxtSessionTime.Text);
            target.nRA = int.Parse(txtnRA.Text);
            target.nDec = int.Parse(txtnRA.Text);
            target.mRA = double.Parse(txtmRA.Text);
            target.mDec = double.Parse(txtmRA.Text);
            target.SubExposure = int.Parse(txtSubExposure.Text);
            if (((Button)sender).Text == "Update Target" && btnAddToList.Tag != null)
            {
                ((ListViewItem)btnAddToList.Tag).SubItems[0].Text = target.Name;
                ((ListViewItem)btnAddToList.Tag).SubItems[1].Text = mtxtRA.Text + " / " + mtxtDec.Text;
                ((ListViewItem)btnAddToList.Tag).SubItems[2].Text = target.LPFilter == 1 ? "Y" : "N";
                ((ListViewItem)btnAddToList.Tag).SubItems[3].Text = mtxtSessionTime.Text;
                ((ListViewItem)btnAddToList.Tag).SubItems[4].Text = txtSubExposure.Text;
                ((ListViewItem)btnAddToList.Tag).SubItems[5].Text = txtnRA.Text + " " + txtnDec.Text + " " + txtmRA.Text + " " + txtmDec.Text;
                ((ListViewItem)btnAddToList.Tag).SubItems[6].Text = JsonSerializer.Serialize(target!, SourceGenerationContext.Default.Target);
            }
            else
            {
                lstTargetList.Items.Add(
                    new ListViewItem(
                        new string[] {
                        target.Name,
                        mtxtRA.Text + " / " + mtxtDec.Text,
                        target.LPFilter == 1 ? "Y":"N",
                        mtxtSessionTime.Text,
                        txtSubExposure.Text,
                        txtnRA.Text + " " + txtnDec.Text + " " + txtmRA.Text + " " + txtmDec.Text,
                        JsonSerializer.Serialize(target!, SourceGenerationContext.Default.Target)
                 }));
            }
            txtTargetName.Text = "";
            mtxtRA.Text = "";
            mtxtDec.Text = "";
            //mtxtSessionTime.Text = "";
            txtnRA.Text = "1";
            txtnDec.Text = "1";
            txtmRA.Text = "1";
            txtmDec.Text = "1";
            txtSubExposure.Text = "10";
            chkLPFilter.Checked = false;
            btnAddToList.Text = "Add To List";
            btnAddToList.Tag = null;
            btnCancelEdit.Visible = false;
        }

        private void cmsTargets_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolTargetEdit)
            {
                var target = JsonSerializer.Deserialize(lstTargetList.SelectedItems[0].SubItems[6].Text, SourceGenerationContext.Default.Target);
                txtTargetName.Text = target.Name;
                mtxtRA.Text = FrontendSupport.convertRaFromDouble(target.RA);
                mtxtDec.Text = FrontendSupport.convertDecFromDouble(target.Dec);
                mtxtSessionTime.Text = FrontendSupport.convertTimeFromDouble(target.SessionTime);
                txtnRA.Text = target.nRA.ToString();
                txtnDec.Text = target.nDec.ToString();
                txtmRA.Text = target.mRA.ToString();
                txtmDec.Text = target.mDec.ToString();
                txtSubExposure.Text = target.SubExposure.ToString();
                chkLPFilter.Checked = target.LPFilter == 1;
                btnAddToList.Text = "Update Target";
                btnAddToList.Tag = lstTargetList.SelectedItems[0];
                btnCancelEdit.Visible = true;
            }
            else if (e.ClickedItem == toolTargetCopy)
            {
                var target = JsonSerializer.Deserialize(lstTargetList.SelectedItems[0].SubItems[6].Text, SourceGenerationContext.Default.Target);
                txtTargetName.Text = target.Name;
                mtxtRA.Text = FrontendSupport.convertRaFromDouble(target.RA);
                mtxtDec.Text = FrontendSupport.convertDecFromDouble(target.Dec);
                mtxtSessionTime.Text = FrontendSupport.convertTimeFromDouble(target.SessionTime);
                txtnRA.Text = target.nRA.ToString();
                txtnDec.Text = target.nDec.ToString();
                txtmRA.Text = target.mRA.ToString();
                txtmDec.Text = target.mDec.ToString();
                txtSubExposure.Text = target.SubExposure.ToString();
                chkLPFilter.Checked = target.LPFilter == 1;
            }
            else if (e.ClickedItem == toolTargetsRemove)
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
                dlgOpenTargets.SupportMultiDottedExtensions = true;
                dlgOpenTargets.Filter = "Seestar Target List (*.stl)|*.stl|All Files (*.*)|*.*";
                dlgOpenTargets.DefaultExt = "stl";
                dlgSaveTargets.FileName = "";
                var result = dlgSaveTargets.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] allTargets = new string[lstTargetList.Items.Count];
                    foreach (ListViewItem item in lstTargetList.Items)
                    {
                        allTargets[lstTargetList.Items.IndexOf(item)] = item.SubItems[6].Text.Replace("\r\n", "");
                    }
                    File.WriteAllLines(dlgSaveTargets.FileName, allTargets);
                }

                /*
                if (File.Exists("TargetList.config") && MessageBox.Show("Overwrite existing Target List?", "Save Target List", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                string[] allTargets = new string[lstTargetList.Items.Count];
                foreach (ListViewItem item in lstTargetList.Items)
                {
                    allTargets[lstTargetList.Items.IndexOf(item)] = item.SubItems[6].Text.Replace("\r\n", "");
                }
                File.WriteAllLines("TargetList.config", allTargets);
                MessageBox.Show("Target List Saved!", "Save Target List");
                */
            }
            else if (e.ClickedItem == toolTargetsLoadLastList)
            {
                dlgOpenTargets.Filter = "Seestar Target List (*.stl)|*.stl|All Files (*.*)|*.*";
                dlgOpenTargets.DefaultExt = "stl";
                dlgOpenTargets.FileName = "";
                var result = dlgOpenTargets.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] allTargets = File.ReadAllLines(dlgOpenTargets.FileName);
                    foreach (var targetItem in allTargets)
                    {
                        Target target = JsonSerializer.Deserialize<Target>(targetItem, SourceGenerationContext.Default.Target);
                        lstTargetList.Items.Add(
                           new ListViewItem(
                              new string[]
                                  {
                                target.Name,
                                FrontendSupport.convertRaFromDouble(target.RA) + " / " + FrontendSupport.convertDecFromDouble(target.Dec),
                                target.LPFilter == 1 ? "Y":"N",
                                FrontendSupport.convertTimeFromDouble(target.SessionTime),
                                target.SubExposure.ToString(),
                                target.nRA + " " + target.nDec + " " + target.mRA + " " + target.mDec,
                                JsonSerializer.Serialize(target!, SourceGenerationContext.Default.Target)
                         }));
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            telescope.SkipTarget = false;
            telescope.CancelAll = false;
            List<string> allTargets = new List<string>();
            foreach (ListViewItem item in lstTargetList.Items)
            {
                allTargets.Add(item.SubItems[6].Text);
            }

            btnStart.Enabled = false;
            captureTask = new Task(() =>
            {
                foreach (string item in allTargets)
                {
                    if (telescope.CancelAll)
                    {
                        break;
                    }
                    telescope.SkipTarget = false;
                    Target target = JsonSerializer.Deserialize<Target>(item, SourceGenerationContext.Default.Target);
                    if (target != null)
                    {
                        telescope.currentTarget = target;
                        Task run = SeeStarS50.SeeStarRun(target, telescope);
                        run.Wait();
                    }
                }
                telescope.currentTarget = null;
            });
            captureTask.Start();
            btnStart.Enabled = false;
        }

        private void btnSkipTarget_Click(object sender, EventArgs e)
        {
            telescope.SkipTarget = true;
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            telescope.SkipTarget = true;
            telescope.CancelAll = true;
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            txtTargetName.Text = "";
            mtxtRA.Text = "";
            mtxtDec.Text = "";
            mtxtSessionTime.Text = "";
            txtnRA.Text = "1";
            txtnDec.Text = "1";
            txtmRA.Text = "1";
            txtmDec.Text = "1";
            txtSubExposure.Text = "10";
            chkLPFilter.Checked = false;
            btnAddToList.Text = "Add To List";
            btnAddToList.Tag = null;
            btnCancelEdit.Visible = false;
        }

        private void cmsSync_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Test to move scope 
            //Task d = telescope.InvokeInstruction(new CmdData() { id = "999", method = "scope_speed_move", parameters = new JsonParams() { speed = 200, angle = 0, dur_sec = 5 } });
            //d.Wait();

            mtxtRA.Text = "  h   m   .    s";
            mtxtDec.Text = "+  °   '   .    \"";

            if (e.ClickedItem == toolSyncFromSeeStar)
            {
                CmdData? parsedData = null;
                Task invoke = telescope.InvokeInstruction("scope_get_equ_coord");
                if (invoke.Wait(2000))
                {
                    while (telescope.CommandResponseQueue.Count == 0)
                        Thread.Sleep(10);
                    string response = telescope.CommandResponseQueue.Dequeue();
                    if (response != null)
                    {
                        parsedData = JsonSerializer.Deserialize<CmdData>(response, SourceGenerationContext.Default.CmdData);
                        mtxtRA.Text = FrontendSupport.convertRaFromDouble(parsedData.result.ra);
                        mtxtDec.Text = FrontendSupport.convertDecFromDouble(parsedData.result.dec);
                    }
                }
            }
            else if (e.ClickedItem == toolSyncFromStellariumObject)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri($"http://{mtxtStelleriumIP.Text.Trim()}/api/");

                var httpResponseTask = httpClient.GetAsync("objects/info");
                httpResponseTask.Wait();
                var httpResponse = httpResponseTask.Result;

                var contentTask = httpResponse.Content.ReadAsStringAsync();
                contentTask.Wait();
                var content = contentTask.Result;




                var start = content.IndexOf("RA/Dec (on date):");
                if (start != -1)
                {
                    start += 17;
                    var end = content.IndexOf("<br/>", start);
                    var RaDec = content.Substring(start, end - start).Trim();
                    var RaDecSplit = RaDec.Split('/');
                    mtxtRA.Text = FrontendSupport.convertRaFromDouble((double)FrontendSupport.convertRaToDouble(RaDecSplit[0].Trim()));
                    mtxtDec.Text = FrontendSupport.convertDecFromDouble((double)FrontendSupport.convertDecToDouble(RaDecSplit[1].Trim()));
                }

                start = content.IndexOf("<h2>");
                if (start != -1)
                {
                    start += 4;
                    var end = content.IndexOf("</h2>", start);
                    var name = content.Substring(start, end - start).Trim();
                    if (name.Length > 40)
                        name = name.Substring(0, 40);
                    txtTargetName.Text = name;
                }
            }

        }


        private void mtxtStelleriumIP_Leave(object sender, EventArgs e)
        {
            if (mtxtStelleriumIP.Text != "")
                File.WriteAllLines("StellariumIP.config", new string[] { mtxtStelleriumIP.Text });
        }
    }

}
