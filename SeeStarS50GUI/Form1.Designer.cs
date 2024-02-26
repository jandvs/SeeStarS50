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
            mtxtDec = new MaskedTextBox();
            lblDEC = new Label();
            btnSyncRaDec = new Button();
            chkLPFilter = new CheckBox();
            mtxtSessionTime = new MaskedTextBox();
            lblSessionTime = new Label();
            btnAddToList = new Button();
            lstTargetList = new ListView();
            colTarget = new ColumnHeader();
            colRADec = new ColumnHeader();
            colLP = new ColumnHeader();
            colSession = new ColumnHeader();
            colMosaic = new ColumnHeader();
            colJson = new ColumnHeader();
            cmsTargets = new ContextMenuStrip(components);
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
            cmsTargets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            grpIPs.SuspendLayout();
            cmsEvents.SuspendLayout();
            SuspendLayout();
            // 
            // mtxtIpAddress
            // 
            mtxtIpAddress.Font = new Font("Segoe UI", 11F);
            mtxtIpAddress.Location = new Point(6, 27);
            mtxtIpAddress.Name = "mtxtIpAddress";
            mtxtIpAddress.Size = new Size(245, 27);
            mtxtIpAddress.TabIndex = 1;
            // 
            // lblTargetName
            // 
            lblTargetName.AutoSize = true;
            lblTargetName.Font = new Font("Segoe UI", 11F);
            lblTargetName.Location = new Point(12, 111);
            lblTargetName.Name = "lblTargetName";
            lblTargetName.Size = new Size(97, 20);
            lblTargetName.TabIndex = 2;
            lblTargetName.Text = "Target Name:";
            // 
            // txtTargetName
            // 
            txtTargetName.Font = new Font("Segoe UI", 11F);
            txtTargetName.Location = new Point(115, 108);
            txtTargetName.Name = "txtTargetName";
            txtTargetName.Size = new Size(258, 27);
            txtTargetName.TabIndex = 3;
            // 
            // lblRA
            // 
            lblRA.AutoSize = true;
            lblRA.Font = new Font("Segoe UI", 11F);
            lblRA.Location = new Point(78, 143);
            lblRA.Name = "lblRA";
            lblRA.Size = new Size(31, 20);
            lblRA.TabIndex = 4;
            lblRA.Text = "RA:";
            // 
            // mtxtRA
            // 
            mtxtRA.Font = new Font("Segoe UI", 11F);
            mtxtRA.Location = new Point(115, 143);
            mtxtRA.Mask = "##h ##m ##.####s";
            mtxtRA.Name = "mtxtRA";
            mtxtRA.Size = new Size(137, 27);
            mtxtRA.TabIndex = 4;
            // 
            // mtxtDec
            // 
            mtxtDec.Font = new Font("Segoe UI", 11F);
            mtxtDec.Location = new Point(115, 182);
            mtxtDec.Mask = "###° ##' ##.####\"";
            mtxtDec.Name = "mtxtDec";
            mtxtDec.Size = new Size(137, 27);
            mtxtDec.TabIndex = 5;
            // 
            // lblDEC
            // 
            lblDEC.AutoSize = true;
            lblDEC.Font = new Font("Segoe UI", 11F);
            lblDEC.Location = new Point(69, 185);
            lblDEC.Name = "lblDEC";
            lblDEC.Size = new Size(40, 20);
            lblDEC.TabIndex = 6;
            lblDEC.Text = "DEC:";
            // 
            // btnSyncRaDec
            // 
            btnSyncRaDec.Font = new Font("Segoe UI", 11F);
            btnSyncRaDec.Location = new Point(258, 143);
            btnSyncRaDec.Name = "btnSyncRaDec";
            btnSyncRaDec.Size = new Size(109, 66);
            btnSyncRaDec.TabIndex = 6;
            btnSyncRaDec.Text = "Sync from SeeStar";
            btnSyncRaDec.UseVisualStyleBackColor = true;
            btnSyncRaDec.Click += btnSyncRaDec_Click;
            // 
            // chkLPFilter
            // 
            chkLPFilter.AutoSize = true;
            chkLPFilter.Font = new Font("Segoe UI", 11F);
            chkLPFilter.Location = new Point(18, 319);
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
            mtxtSessionTime.Location = new Point(115, 219);
            mtxtSessionTime.Mask = "## Hours   ## Minutes";
            mtxtSessionTime.Name = "mtxtSessionTime";
            mtxtSessionTime.Size = new Size(156, 27);
            mtxtSessionTime.TabIndex = 7;
            // 
            // lblSessionTime
            // 
            lblSessionTime.AutoSize = true;
            lblSessionTime.Font = new Font("Segoe UI", 11F);
            lblSessionTime.Location = new Point(12, 222);
            lblSessionTime.Name = "lblSessionTime";
            lblSessionTime.Size = new Size(98, 20);
            lblSessionTime.TabIndex = 10;
            lblSessionTime.Text = "Session Time:";
            // 
            // btnAddToList
            // 
            btnAddToList.Font = new Font("Segoe UI", 11F);
            btnAddToList.Location = new Point(158, 319);
            btnAddToList.Name = "btnAddToList";
            btnAddToList.Size = new Size(127, 36);
            btnAddToList.TabIndex = 13;
            btnAddToList.Text = "Add To List";
            btnAddToList.UseVisualStyleBackColor = true;
            btnAddToList.Click += btnAddToList_Click;
            // 
            // lstTargetList
            // 
            lstTargetList.Columns.AddRange(new ColumnHeader[] { colTarget, colRADec, colLP, colSession, colMosaic, colJson });
            lstTargetList.ContextMenuStrip = cmsTargets;
            lstTargetList.Font = new Font("Segoe UI", 8F);
            lstTargetList.FullRowSelect = true;
            lstTargetList.GridLines = true;
            lstTargetList.Location = new Point(10, 368);
            lstTargetList.MultiSelect = false;
            lstTargetList.Name = "lstTargetList";
            lstTargetList.Size = new Size(627, 275);
            lstTargetList.TabIndex = 14;
            lstTargetList.Tag = "";
            lstTargetList.UseCompatibleStateImageBehavior = false;
            lstTargetList.View = View.Details;
            // 
            // colTarget
            // 
            colTarget.Text = "Target";
            colTarget.Width = 220;
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
            colSession.Width = 120;
            // 
            // colMosaic
            // 
            colMosaic.Text = "Mosaic";
            colMosaic.Width = 80;
            // 
            // colJson
            // 
            colJson.Text = "Json";
            // 
            // cmsTargets
            // 
            cmsTargets.Items.AddRange(new ToolStripItem[] { toolTargetsRemove, toolTargetsClearAll, toolTargetsSaveAll, toolTargetsLoadLastList });
            cmsTargets.Name = "cmsTargets";
            cmsTargets.Size = new Size(146, 92);
            cmsTargets.Text = "Targets";
            cmsTargets.ItemClicked += cmsTargets_ItemClicked;
            // 
            // toolTargetsRemove
            // 
            toolTargetsRemove.Name = "toolTargetsRemove";
            toolTargetsRemove.Size = new Size(145, 22);
            toolTargetsRemove.Text = "Remove";
            // 
            // toolTargetsClearAll
            // 
            toolTargetsClearAll.Name = "toolTargetsClearAll";
            toolTargetsClearAll.Size = new Size(145, 22);
            toolTargetsClearAll.Text = "Clear All";
            // 
            // toolTargetsSaveAll
            // 
            toolTargetsSaveAll.Name = "toolTargetsSaveAll";
            toolTargetsSaveAll.Size = new Size(145, 22);
            toolTargetsSaveAll.Text = "Save All";
            toolTargetsSaveAll.TextAlign = ContentAlignment.TopLeft;
            // 
            // toolTargetsLoadLastList
            // 
            toolTargetsLoadLastList.Name = "toolTargetsLoadLastList";
            toolTargetsLoadLastList.Size = new Size(145, 22);
            toolTargetsLoadLastList.Text = "Load Last List";
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 11F);
            btnStart.Location = new Point(10, 649);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(127, 36);
            btnStart.TabIndex = 15;
            btnStart.Text = "Start Captures";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnConnect
            // 
            btnConnect.Font = new Font("Segoe UI", 11F);
            btnConnect.Location = new Point(257, 22);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(127, 36);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(427, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(210, 293);
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
            grpIPs.Size = new Size(398, 81);
            grpIPs.TabIndex = 20;
            grpIPs.TabStop = false;
            grpIPs.Text = "SeeStar IPs";
            // 
            // txtnRA
            // 
            txtnRA.Font = new Font("Segoe UI", 11F);
            txtnRA.Location = new Point(115, 254);
            txtnRA.Name = "txtnRA";
            txtnRA.Size = new Size(27, 27);
            txtnRA.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(44, 257);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 21;
            label2.Text = "RA Tiles:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(37, 289);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 23;
            label3.Text = "Dec Tiles:";
            // 
            // txtnDec
            // 
            txtnDec.Font = new Font("Segoe UI", 11F);
            txtnDec.Location = new Point(115, 286);
            txtnDec.Name = "txtnDec";
            txtnDec.Size = new Size(27, 27);
            txtnDec.TabIndex = 9;
            // 
            // txtmDec
            // 
            txtmDec.Font = new Font("Segoe UI", 11F);
            txtmDec.Location = new Point(258, 286);
            txtmDec.Name = "txtmDec";
            txtmDec.Size = new Size(27, 27);
            txtmDec.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(170, 289);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 27;
            label4.Text = "Dec Offset:";
            // 
            // txtmRA
            // 
            txtmRA.Font = new Font("Segoe UI", 11F);
            txtmRA.Location = new Point(258, 254);
            txtmRA.Name = "txtmRA";
            txtmRA.Size = new Size(27, 27);
            txtmRA.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(177, 257);
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
            lstEvents.Location = new Point(6, 692);
            lstEvents.Name = "lstEvents";
            lstEvents.ScrollAlwaysVisible = true;
            lstEvents.Size = new Size(631, 214);
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
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(653, 921);
            Controls.Add(lstEvents);
            Controls.Add(txtmDec);
            Controls.Add(label4);
            Controls.Add(txtmRA);
            Controls.Add(label5);
            Controls.Add(txtnDec);
            Controls.Add(label3);
            Controls.Add(txtnRA);
            Controls.Add(label2);
            Controls.Add(grpIPs);
            Controls.Add(pictureBox1);
            Controls.Add(btnStart);
            Controls.Add(lstTargetList);
            Controls.Add(btnAddToList);
            Controls.Add(mtxtSessionTime);
            Controls.Add(lblSessionTime);
            Controls.Add(chkLPFilter);
            Controls.Add(btnSyncRaDec);
            Controls.Add(mtxtDec);
            Controls.Add(lblDEC);
            Controls.Add(mtxtRA);
            Controls.Add(lblRA);
            Controls.Add(txtTargetName);
            Controls.Add(lblTargetName);
            Name = "Form1";
            Text = "SeeStar S50 Companion";
            Load += Form1_Load;
            cmsTargets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            grpIPs.ResumeLayout(false);
            grpIPs.PerformLayout();
            cmsEvents.ResumeLayout(false);
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
        private Button btnSyncRaDec;
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
    }
}
