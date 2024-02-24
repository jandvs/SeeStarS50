namespace SeeStarS50GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            mtxtIpAddress = new MaskedTextBox();
            lblTargetName = new Label();
            txtTargetName = new TextBox();
            lblRA = new Label();
            mtxtRA = new MaskedTextBox();
            cmsSync = new ContextMenuStrip(components);
            toolSyncFromSeeStar = new ToolStripMenuItem();
            toolSyncFromStellariumObject = new ToolStripMenuItem();
            mtxtDec = new MaskedTextBox();
            lblDEC = new Label();
            chkLPFilter = new CheckBox();
            mtxtSessionTime = new MaskedTextBox();
            lblSessionTime = new Label();
            btnAddToList = new Button();
            lstTargetList = new ListView();
            colTarget = new ColumnHeader();
            colRADec = new ColumnHeader();
            colLP = new ColumnHeader();
            colSession = new ColumnHeader();
            colSubExposure = new ColumnHeader();
            colMosaic = new ColumnHeader();
            colJson = new ColumnHeader();
            cmsTargets = new ContextMenuStrip(components);
            toolTargetEdit = new ToolStripMenuItem();
            toolTargetCopy = new ToolStripMenuItem();
            toolTargetsRemove = new ToolStripMenuItem();
            toolTargetsClearAll = new ToolStripMenuItem();
            toolTargetsSaveAll = new ToolStripMenuItem();
            toolTargetsLoadLastList = new ToolStripMenuItem();
            btnStart = new Button();
            btnConnect = new Button();
            pictureBox1 = new PictureBox();
            grpIPs = new GroupBox();
            txtnRA = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtnDec = new TextBox();
            txtmDec = new TextBox();
            label4 = new Label();
            txtmRA = new TextBox();
            label5 = new Label();
            lstEvents = new ListBox();
            cmsEvents = new ContextMenuStrip(components);
            toolEventsCopy = new ToolStripMenuItem();
            toolEventsClear = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            prgProgress = new ProgressBar();
            lblTargetProgress = new Label();
            lblTileNumber = new Label();
            btnSkipTarget = new Button();
            btnCancelAll = new Button();
            dlgOpenTargets = new OpenFileDialog();
            dlgSaveTargets = new SaveFileDialog();
            btnCancelEdit = new Button();
            grpStellerium = new GroupBox();
            mtxtStelleriumIP = new MaskedTextBox();
            txtSubExposure = new TextBox();
            label1 = new Label();
            Target = new GroupBox();
            cmsSync.SuspendLayout();
            cmsTargets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            grpIPs.SuspendLayout();
            cmsEvents.SuspendLayout();
            grpStellerium.SuspendLayout();
            Target.SuspendLayout();
            SuspendLayout();
            // 
            // mtxtIpAddress
            // 
            mtxtIpAddress.Font = new Font("Segoe UI", 11F);
            mtxtIpAddress.Location = new Point(6, 27);
            mtxtIpAddress.Name = "mtxtIpAddress";
            mtxtIpAddress.Size = new Size(246, 27);
            mtxtIpAddress.TabIndex = 1;
            // 
            // lblTargetName
            // 
            lblTargetName.AutoSize = true;
            lblTargetName.Font = new Font("Segoe UI", 11F);
            lblTargetName.Location = new Point(6, 28);
            lblTargetName.Name = "lblTargetName";
            lblTargetName.Size = new Size(49, 20);
            lblTargetName.TabIndex = 2;
            lblTargetName.Text = "Name";
            // 
            // txtTargetName
            // 
            txtTargetName.Font = new Font("Segoe UI", 11F);
            txtTargetName.Location = new Point(8, 51);
            txtTargetName.Name = "txtTargetName";
            txtTargetName.Size = new Size(171, 27);
            txtTargetName.TabIndex = 3;
            // 
            // lblRA
            // 
            lblRA.AutoSize = true;
            lblRA.Font = new Font("Segoe UI", 11F);
            lblRA.Location = new Point(15, 91);
            lblRA.Name = "lblRA";
            lblRA.Size = new Size(31, 20);
            lblRA.TabIndex = 4;
            lblRA.Text = "RA:";
            // 
            // mtxtRA
            // 
            mtxtRA.ContextMenuStrip = cmsSync;
            mtxtRA.Font = new Font("Segoe UI", 11F);
            mtxtRA.Location = new Point(48, 88);
            mtxtRA.Mask = "##h ##m ##.####s";
            mtxtRA.Name = "mtxtRA";
            mtxtRA.Size = new Size(137, 27);
            mtxtRA.TabIndex = 4;
            // 
            // cmsSync
            // 
            cmsSync.BackColor = SystemColors.ScrollBar;
            cmsSync.Items.AddRange(new ToolStripItem[] { toolSyncFromSeeStar, toolSyncFromStellariumObject });
            cmsSync.Name = "cmsSync";
            cmsSync.ShowImageMargin = false;
            cmsSync.Size = new Size(196, 48);
            cmsSync.Text = "Get RA/Dec from...";
            cmsSync.ItemClicked += cmsSync_ItemClicked;
            // 
            // toolSyncFromSeeStar
            // 
            toolSyncFromSeeStar.BackColor = SystemColors.Control;
            toolSyncFromSeeStar.Name = "toolSyncFromSeeStar";
            toolSyncFromSeeStar.Size = new Size(195, 22);
            toolSyncFromSeeStar.Text = "    SeeStar RA/Dec";
            // 
            // toolSyncFromStellariumObject
            // 
            toolSyncFromStellariumObject.BackColor = SystemColors.Control;
            toolSyncFromStellariumObject.Name = "toolSyncFromStellariumObject";
            toolSyncFromStellariumObject.Size = new Size(195, 22);
            toolSyncFromStellariumObject.Text = "    Stellarium Object RA/Dec";
            // 
            // mtxtDec
            // 
            mtxtDec.ContextMenuStrip = cmsSync;
            mtxtDec.Font = new Font("Segoe UI", 11F);
            mtxtDec.Location = new Point(48, 121);
            mtxtDec.Mask = "###° ##' ##.####\"";
            mtxtDec.Name = "mtxtDec";
            mtxtDec.Size = new Size(137, 27);
            mtxtDec.TabIndex = 5;
            // 
            // lblDEC
            // 
            lblDEC.AutoSize = true;
            lblDEC.Font = new Font("Segoe UI", 11F);
            lblDEC.Location = new Point(6, 124);
            lblDEC.Name = "lblDEC";
            lblDEC.Size = new Size(40, 20);
            lblDEC.TabIndex = 6;
            lblDEC.Text = "DEC:";
            // 
            // chkLPFilter
            // 
            chkLPFilter.AutoSize = true;
            chkLPFilter.Font = new Font("Segoe UI", 11F);
            chkLPFilter.Location = new Point(74, 192);
            chkLPFilter.Name = "chkLPFilter";
            chkLPFilter.RightToLeft = RightToLeft.Yes;
            chkLPFilter.Size = new Size(111, 24);
            chkLPFilter.TabIndex = 12;
            chkLPFilter.Text = ":Use LP Filter";
            chkLPFilter.UseVisualStyleBackColor = true;
            // 
            // mtxtSessionTime
            // 
            mtxtSessionTime.Font = new Font("Segoe UI", 11F);
            mtxtSessionTime.Location = new Point(185, 51);
            mtxtSessionTime.Mask = "## Hours   ## Minutes";
            mtxtSessionTime.Name = "mtxtSessionTime";
            mtxtSessionTime.Size = new Size(156, 27);
            mtxtSessionTime.TabIndex = 7;
            // 
            // lblSessionTime
            // 
            lblSessionTime.AutoSize = true;
            lblSessionTime.Font = new Font("Segoe UI", 11F);
            lblSessionTime.Location = new Point(188, 28);
            lblSessionTime.Name = "lblSessionTime";
            lblSessionTime.Size = new Size(98, 20);
            lblSessionTime.TabIndex = 10;
            lblSessionTime.Text = "Session Time:";
            // 
            // btnAddToList
            // 
            btnAddToList.Font = new Font("Segoe UI", 11F);
            btnAddToList.Location = new Point(12, 222);
            btnAddToList.Name = "btnAddToList";
            btnAddToList.Size = new Size(127, 36);
            btnAddToList.TabIndex = 13;
            btnAddToList.Text = "Add To List";
            btnAddToList.UseVisualStyleBackColor = true;
            btnAddToList.Click += btnAddToList_Click;
            // 
            // lstTargetList
            // 
            lstTargetList.Columns.AddRange(new ColumnHeader[] { colTarget, colRADec, colLP, colSession, colSubExposure, colMosaic, colJson });
            lstTargetList.ContextMenuStrip = cmsTargets;
            lstTargetList.Font = new Font("Segoe UI", 8F);
            lstTargetList.FullRowSelect = true;
            lstTargetList.GridLines = true;
            lstTargetList.Location = new Point(10, 394);
            lstTargetList.MultiSelect = false;
            lstTargetList.Name = "lstTargetList";
            lstTargetList.Size = new Size(679, 249);
            lstTargetList.TabIndex = 14;
            lstTargetList.Tag = "";
            lstTargetList.UseCompatibleStateImageBehavior = false;
            lstTargetList.View = View.Details;
            // 
            // colTarget
            // 
            colTarget.Text = "Target";
            colTarget.Width = 210;
            // 
            // colRADec
            // 
            colRADec.Text = "RA/Dec";
            colRADec.Width = 174;
            // 
            // colLP
            // 
            colLP.Text = "LP";
            colLP.Width = 30;
            // 
            // colSession
            // 
            colSession.Text = "Session";
            colSession.Width = 150;
            // 
            // colSubExposure
            // 
            colSubExposure.Text = "Exp";
            colSubExposure.Width = 40;
            // 
            // colMosaic
            // 
            colMosaic.Text = "Mosaic";
            colMosaic.Width = 85;
            // 
            // colJson
            // 
            colJson.Text = "Json";
            // 
            // cmsTargets
            // 
            cmsTargets.Items.AddRange(new ToolStripItem[] { toolTargetEdit, toolTargetCopy, toolTargetsRemove, toolTargetsClearAll, toolTargetsSaveAll, toolTargetsLoadLastList });
            cmsTargets.Name = "cmsTargets";
            cmsTargets.Size = new Size(166, 136);
            cmsTargets.Text = "Targets";
            cmsTargets.ItemClicked += cmsTargets_ItemClicked;
            // 
            // toolTargetEdit
            // 
            toolTargetEdit.Name = "toolTargetEdit";
            toolTargetEdit.Size = new Size(165, 22);
            toolTargetEdit.Text = "Edit";
            // 
            // toolTargetCopy
            // 
            toolTargetCopy.Name = "toolTargetCopy";
            toolTargetCopy.Size = new Size(165, 22);
            toolTargetCopy.Text = "Copy";
            // 
            // toolTargetsRemove
            // 
            toolTargetsRemove.Name = "toolTargetsRemove";
            toolTargetsRemove.Size = new Size(165, 22);
            toolTargetsRemove.Text = "Remove";
            // 
            // toolTargetsClearAll
            // 
            toolTargetsClearAll.Name = "toolTargetsClearAll";
            toolTargetsClearAll.Size = new Size(165, 22);
            toolTargetsClearAll.Text = "Clear All";
            // 
            // toolTargetsSaveAll
            // 
            toolTargetsSaveAll.Name = "toolTargetsSaveAll";
            toolTargetsSaveAll.Size = new Size(165, 22);
            toolTargetsSaveAll.Text = "Save Target List...";
            toolTargetsSaveAll.TextAlign = ContentAlignment.TopLeft;
            // 
            // toolTargetsLoadLastList
            // 
            toolTargetsLoadLastList.Name = "toolTargetsLoadLastList";
            toolTargetsLoadLastList.Size = new Size(165, 22);
            toolTargetsLoadLastList.Text = "Load Target List...";
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 11F);
            btnStart.Location = new Point(10, 649);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(119, 36);
            btnStart.TabIndex = 15;
            btnStart.Text = "Start Captures";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnConnect
            // 
            btnConnect.Font = new Font("Segoe UI", 11F);
            btnConnect.Location = new Point(258, 27);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(84, 27);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(455, 111);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(196, 242);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // grpIPs
            // 
            grpIPs.Controls.Add(mtxtIpAddress);
            grpIPs.Controls.Add(btnConnect);
            grpIPs.Location = new Point(12, 12);
            grpIPs.Name = "grpIPs";
            grpIPs.Size = new Size(355, 70);
            grpIPs.TabIndex = 20;
            grpIPs.TabStop = false;
            grpIPs.Text = "SeeStar IPs";
            // 
            // txtnRA
            // 
            txtnRA.Font = new Font("Segoe UI", 11F);
            txtnRA.Location = new Point(314, 88);
            txtnRA.Name = "txtnRA";
            txtnRA.Size = new Size(27, 27);
            txtnRA.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(243, 91);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 21;
            label2.Text = "RA Tiles:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(236, 124);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 23;
            label3.Text = "Dec Tiles:";
            // 
            // txtnDec
            // 
            txtnDec.Font = new Font("Segoe UI", 11F);
            txtnDec.Location = new Point(314, 121);
            txtnDec.Name = "txtnDec";
            txtnDec.Size = new Size(27, 27);
            txtnDec.TabIndex = 9;
            // 
            // txtmDec
            // 
            txtmDec.Font = new Font("Segoe UI", 11F);
            txtmDec.Location = new Point(314, 189);
            txtmDec.Name = "txtmDec";
            txtmDec.Size = new Size(27, 27);
            txtmDec.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(226, 192);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 27;
            label4.Text = "Dec Offset:";
            // 
            // txtmRA
            // 
            txtmRA.Font = new Font("Segoe UI", 11F);
            txtmRA.Location = new Point(314, 153);
            txtmRA.Name = "txtmRA";
            txtmRA.Size = new Size(27, 27);
            txtmRA.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(233, 156);
            label5.Name = "label5";
            label5.Size = new Size(75, 20);
            label5.TabIndex = 25;
            label5.Text = "RA Offset:";
            // 
            // lstEvents
            // 
            lstEvents.ContextMenuStrip = cmsEvents;
            lstEvents.FormattingEnabled = true;
            lstEvents.HorizontalScrollbar = true;
            lstEvents.ItemHeight = 15;
            lstEvents.Location = new Point(6, 707);
            lstEvents.Name = "lstEvents";
            lstEvents.ScrollAlwaysVisible = true;
            lstEvents.Size = new Size(683, 199);
            lstEvents.TabIndex = 29;
            // 
            // cmsEvents
            // 
            cmsEvents.Items.AddRange(new ToolStripItem[] { toolEventsCopy, toolEventsClear });
            cmsEvents.Name = "cmsEvents";
            cmsEvents.Size = new Size(103, 48);
            cmsEvents.Text = "Events";
            cmsEvents.ItemClicked += cmsEvents_ItemClicked;
            // 
            // toolEventsCopy
            // 
            toolEventsCopy.Name = "toolEventsCopy";
            toolEventsCopy.Size = new Size(102, 22);
            toolEventsCopy.Text = "Copy";
            // 
            // toolEventsClear
            // 
            toolEventsClear.Name = "toolEventsClear";
            toolEventsClear.Size = new Size(102, 22);
            toolEventsClear.Text = "Clear";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            // 
            // prgProgress
            // 
            prgProgress.Location = new Point(135, 657);
            prgProgress.Name = "prgProgress";
            prgProgress.Size = new Size(362, 23);
            prgProgress.TabIndex = 30;
            // 
            // lblTargetProgress
            // 
            lblTargetProgress.AutoSize = true;
            lblTargetProgress.Location = new Point(155, 661);
            lblTargetProgress.Name = "lblTargetProgress";
            lblTargetProgress.Size = new Size(42, 15);
            lblTargetProgress.TabIndex = 31;
            lblTargetProgress.Text = "Target:";
            // 
            // lblTileNumber
            // 
            lblTileNumber.AutoSize = true;
            lblTileNumber.Location = new Point(444, 661);
            lblTileNumber.Name = "lblTileNumber";
            lblTileNumber.Size = new Size(28, 15);
            lblTileNumber.TabIndex = 32;
            lblTileNumber.Text = "Tile:";
            // 
            // btnSkipTarget
            // 
            btnSkipTarget.Font = new Font("Segoe UI", 11F);
            btnSkipTarget.Location = new Point(503, 649);
            btnSkipTarget.Name = "btnSkipTarget";
            btnSkipTarget.Size = new Size(90, 36);
            btnSkipTarget.TabIndex = 33;
            btnSkipTarget.Text = "Skip Target";
            btnSkipTarget.UseVisualStyleBackColor = true;
            btnSkipTarget.Click += btnSkipTarget_Click;
            // 
            // btnCancelAll
            // 
            btnCancelAll.Font = new Font("Segoe UI", 11F);
            btnCancelAll.Location = new Point(599, 649);
            btnCancelAll.Name = "btnCancelAll";
            btnCancelAll.Size = new Size(90, 36);
            btnCancelAll.TabIndex = 34;
            btnCancelAll.Text = "Cancel All";
            btnCancelAll.UseVisualStyleBackColor = true;
            btnCancelAll.Click += btnCancelAll_Click;
            // 
            // dlgOpenTargets
            // 
            dlgOpenTargets.Title = "Open Target List";
            // 
            // dlgSaveTargets
            // 
            dlgSaveTargets.Title = "Save Target List";
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.Font = new Font("Segoe UI", 11F);
            btnCancelEdit.Location = new Point(145, 222);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Size = new Size(127, 36);
            btnCancelEdit.TabIndex = 35;
            btnCancelEdit.Text = "Cancel Edit";
            btnCancelEdit.UseVisualStyleBackColor = true;
            btnCancelEdit.Visible = false;
            btnCancelEdit.Click += btnCancelEdit_Click;
            // 
            // grpStellerium
            // 
            grpStellerium.Controls.Add(mtxtStelleriumIP);
            grpStellerium.Location = new Point(427, 12);
            grpStellerium.Name = "grpStellerium";
            grpStellerium.Size = new Size(262, 70);
            grpStellerium.TabIndex = 21;
            grpStellerium.TabStop = false;
            grpStellerium.Text = "Stellerium IP:Port";
            // 
            // mtxtStelleriumIP
            // 
            mtxtStelleriumIP.Font = new Font("Segoe UI", 11F);
            mtxtStelleriumIP.Location = new Point(6, 27);
            mtxtStelleriumIP.Name = "mtxtStelleriumIP";
            mtxtStelleriumIP.Size = new Size(250, 27);
            mtxtStelleriumIP.TabIndex = 1;
            mtxtStelleriumIP.Leave += mtxtStelleriumIP_Leave;
            // 
            // txtSubExposure
            // 
            txtSubExposure.Font = new Font("Segoe UI", 11F);
            txtSubExposure.Location = new Point(145, 153);
            txtSubExposure.Name = "txtSubExposure";
            txtSubExposure.Size = new Size(40, 27);
            txtSubExposure.TabIndex = 36;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(6, 156);
            label1.Name = "label1";
            label1.Size = new Size(139, 20);
            label1.TabIndex = 37;
            label1.Text = "Exposure (seconds):";
            // 
            // Target
            // 
            Target.Controls.Add(lblTargetName);
            Target.Controls.Add(txtTargetName);
            Target.Controls.Add(lblRA);
            Target.Controls.Add(mtxtRA);
            Target.Controls.Add(txtSubExposure);
            Target.Controls.Add(lblDEC);
            Target.Controls.Add(label1);
            Target.Controls.Add(mtxtDec);
            Target.Controls.Add(chkLPFilter);
            Target.Controls.Add(btnCancelEdit);
            Target.Controls.Add(lblSessionTime);
            Target.Controls.Add(mtxtSessionTime);
            Target.Controls.Add(btnAddToList);
            Target.Controls.Add(label2);
            Target.Controls.Add(txtnRA);
            Target.Controls.Add(label3);
            Target.Controls.Add(txtmDec);
            Target.Controls.Add(txtnDec);
            Target.Controls.Add(label4);
            Target.Controls.Add(label5);
            Target.Controls.Add(txtmRA);
            Target.Location = new Point(10, 95);
            Target.Name = "Target";
            Target.Size = new Size(375, 278);
            Target.TabIndex = 38;
            Target.TabStop = false;
            Target.Text = "Target Editor";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 921);
            Controls.Add(Target);
            Controls.Add(grpStellerium);
            Controls.Add(btnCancelAll);
            Controls.Add(lblTileNumber);
            Controls.Add(lblTargetProgress);
            Controls.Add(prgProgress);
            Controls.Add(lstEvents);
            Controls.Add(grpIPs);
            Controls.Add(pictureBox1);
            Controls.Add(btnStart);
            Controls.Add(lstTargetList);
            Controls.Add(btnSkipTarget);
            Name = "Form1";
            Text = "SeeStar S50 Companion V0.02 (Alpha)";
            Load += Form1_Load;
            cmsSync.ResumeLayout(false);
            cmsTargets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            grpIPs.ResumeLayout(false);
            grpIPs.PerformLayout();
            cmsEvents.ResumeLayout(false);
            grpStellerium.ResumeLayout(false);
            grpStellerium.PerformLayout();
            Target.ResumeLayout(false);
            Target.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MaskedTextBox mtxtIpAddress;
        private Label lblTargetName;
        private TextBox txtTargetName;
        private Label lblRA;
        private MaskedTextBox mtxtRA;
        private MaskedTextBox mtxtDec;
        private Label lblDEC;
        private CheckBox chkLPFilter;
        private MaskedTextBox mtxtSessionTime;
        private Label lblSessionTime;
        private Button btnAddToList;
        private ListBox listBox1;
        private ListView lstTargetList;
        private ColumnHeader colTarget;
        private ColumnHeader colLP;
        private ColumnHeader colSession;
        private ColumnHeader colMosaic;
        private Button btnStart;
        private Button btnConnect;
        private PictureBox pictureBox1;
        private GroupBox grpIPs;
        private TextBox txtnRA;
        private Label label2;
        private Label label3;
        private TextBox txtnDec;
        private TextBox txtmDec;
        private Label label4;
        private TextBox txtmRA;
        private Label label5;
        private ColumnHeader colRADec;
        private ListBox lstEvents;
        private System.Windows.Forms.Timer timer1;
        private ContextMenuStrip cmsEvents;
        private ToolStripMenuItem toolEventsCopy;
        private ToolStripMenuItem toolEventsClear;
        private ColumnHeader colJson;
        private ContextMenuStrip cmsTargets;
        private ToolStripMenuItem toolTargetsRemove;
        private ToolStripMenuItem toolTargetsClearAll;
        private ToolStripMenuItem toolTargetsSaveAll;
        private ToolStripMenuItem toolTargetsLoadLastList;
        private ProgressBar prgProgress;
        private Label lblTargetProgress;
        private Label lblTileNumber;
        private Button button2;
        private Button bntSkipTarget;
        private Button btnCancelAll;
        private Button btnSkipTarget;
        private OpenFileDialog dlgOpenTargets;
        private SaveFileDialog dlgSaveTargets;
        private ToolStripMenuItem toolTargetEdit;
        private Button btnCancelEdit;
        private ToolStripMenuItem toolTargetCopy;
        private GroupBox groupBox1;
        private MaskedTextBox mtxtStelleriumIP;
        private Button button3;
        private GroupBox grpStellerium;
        private ContextMenuStrip cmsSync;
        private ToolStripMenuItem toolSyncFromSeeStar;
        private ToolStripMenuItem toolSyncFromStellariumObject;
        private TextBox txtSubExposure;
        private Label label1;
        private GroupBox Target;
        private ColumnHeader colSubExposure;
    }
}
