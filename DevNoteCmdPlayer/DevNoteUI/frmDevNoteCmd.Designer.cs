namespace DevNoteCmdPlayer
{
    partial class frmDevNoteCmd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevNoteCmd));
            this.timerUpdateUI = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxRec = new System.Windows.Forms.GroupBox();
            this.dgActions = new System.Windows.Forms.DataGridView();
            this.OrderNo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFailed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.actionSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripMenuBottom = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabelConsoleState = new System.Windows.Forms.ToolStripLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRec = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTEST = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItemNEW = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItemOPEN = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxConsole = new System.Windows.Forms.GroupBox();
            this.groupLib = new System.Windows.Forms.Panel();
            this.addNewLibControl1 = new DevNoteWindowsFormsControlLibrary.AddNewLibControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxLimbo = new System.Windows.Forms.GroupBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxRec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionSource)).BeginInit();
            this.toolStripMenuBottom.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.flowMain.SuspendLayout();
            this.groupBoxConsole.SuspendLayout();
            this.groupLib.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdateUI
            // 
            this.timerUpdateUI.Enabled = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "codecept|*.js|XML|*.xml|All Files|*.*";
            this.saveFileDialog1.Title = "Save DevNote Script (json)";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "\"json\"";
            this.openFileDialog1.Filter = "codecept|*.js|XML|*.xml|All Files|*.*";
            this.openFileDialog1.Title = "Browse Script Files (json)";
            // 
            // groupBoxRec
            // 
            this.groupBoxRec.Controls.Add(this.dgActions);
            this.groupBoxRec.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRec.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBoxRec.Location = new System.Drawing.Point(3, 787);
            this.groupBoxRec.Name = "groupBoxRec";
            this.groupBoxRec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxRec.Size = new System.Drawing.Size(706, 190);
            this.groupBoxRec.TabIndex = 2;
            this.groupBoxRec.TabStop = false;
            this.groupBoxRec.Text = "Loaded Script";
            // 
            // dgActions
            // 
            this.dgActions.AutoGenerateColumns = false;
            this.dgActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderNo,
            this.Script,
            this.command,
            this.IsFailed});
            this.dgActions.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgActions.DataSource = this.actionSource;
            this.dgActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgActions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgActions.Location = new System.Drawing.Point(3, 13);
            this.dgActions.Name = "dgActions";
            this.dgActions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActions.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgActions.RowHeadersVisible = false;
            this.dgActions.RowHeadersWidth = 51;
            this.dgActions.RowTemplate.DividerHeight = 1;
            this.dgActions.RowTemplate.Height = 35;
            this.dgActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActions.Size = new System.Drawing.Size(700, 174);
            this.dgActions.TabIndex = 1;
            // 
            // OrderNo
            // 
            this.OrderNo.DataPropertyName = "OrderNo";
            this.OrderNo.Frozen = true;
            this.OrderNo.HeaderText = "OrderNo";
            this.OrderNo.MinimumWidth = 6;
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OrderNo.Width = 75;
            // 
            // Script
            // 
            this.Script.DataPropertyName = "Script";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Script.DefaultCellStyle = dataGridViewCellStyle1;
            this.Script.Frozen = true;
            this.Script.HeaderText = "Script";
            this.Script.MinimumWidth = 6;
            this.Script.Name = "Script";
            this.Script.Width = 400;
            // 
            // command
            // 
            this.command.DataPropertyName = "command";
            this.command.Frozen = true;
            this.command.HeaderText = "command";
            this.command.MinimumWidth = 6;
            this.command.Name = "command";
            this.command.Visible = false;
            this.command.Width = 125;
            // 
            // IsFailed
            // 
            this.IsFailed.DataPropertyName = "IsFailed";
            this.IsFailed.Frozen = true;
            this.IsFailed.HeaderText = "IsFailed";
            this.IsFailed.MinimumWidth = 6;
            this.IsFailed.Name = "IsFailed";
            this.IsFailed.Visible = false;
            this.IsFailed.Width = 125;
            // 
            // toolStripMenuBottom
            // 
            this.toolStripMenuBottom.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMenuBottom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripMenuBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMenuBottom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStripMenuBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConsoleState});
            this.toolStripMenuBottom.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripMenuBottom.Location = new System.Drawing.Point(64, 16);
            this.toolStripMenuBottom.Name = "toolStripMenuBottom";
            this.toolStripMenuBottom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripMenuBottom.Size = new System.Drawing.Size(50, 25);
            this.toolStripMenuBottom.TabIndex = 1;
            this.toolStripMenuBottom.Text = "toolStrip1";
            // 
            // toolStripStatusLabelConsoleState
            // 
            this.toolStripStatusLabelConsoleState.Name = "toolStripStatusLabelConsoleState";
            this.toolStripStatusLabelConsoleState.Size = new System.Drawing.Size(38, 22);
            this.toolStripStatusLabelConsoleState.Text = "status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnRec);
            this.flowLayoutPanel1.Controls.Add(this.btnPlay);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.panel6);
            this.flowLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(830, 68);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.MouseLeave += new System.EventHandler(this.FlowLayoutPanel1_MouseLeave);
            // 
            // btnRec
            // 
            this.btnRec.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRec.ForeColor = System.Drawing.Color.Black;
            this.btnRec.Location = new System.Drawing.Point(3, 3);
            this.btnRec.Name = "btnRec";
            this.btnRec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRec.Size = new System.Drawing.Size(84, 59);
            this.btnRec.TabIndex = 0;
            this.btnRec.Text = "REC";
            this.btnRec.UseVisualStyleBackColor = true;
            this.btnRec.Click += new System.EventHandler(this.BtnRec_Click);
            this.btnRec.MouseLeave += new System.EventHandler(this.BtnRec_MouseLeave);
            this.btnRec.MouseHover += new System.EventHandler(this.BtnRec_MouseHover);
            // 
            // btnPlay
            // 
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.Location = new System.Drawing.Point(93, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPlay.Size = new System.Drawing.Size(84, 59);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "REPLAY";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            this.btnPlay.MouseHover += new System.EventHandler(this.BtnPlay_MouseHover);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(183, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSave.Size = new System.Drawing.Size(84, 59);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.btnSave.MouseHover += new System.EventHandler(this.BtnSave_MouseHover);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel6.Location = new System.Drawing.Point(273, 3);
            this.panel6.Name = "panel6";
            this.panel6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel6.Size = new System.Drawing.Size(88, 59);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.menuStrip2);
            this.panel5.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel5.Location = new System.Drawing.Point(3, 27);
            this.panel5.Name = "panel5";
            this.panel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel5.Size = new System.Drawing.Size(66, 32);
            this.panel5.TabIndex = 2;
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip2.Location = new System.Drawing.Point(-4, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip2.Size = new System.Drawing.Size(62, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripSeparator2,
            this.toolStripMenuItem6,
            this.toolStripMenuItemTEST,
            this.toolStripSeparator6,
            this.toolStripMenuItem8});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.toolStripMenuItem1.Text = "&Library";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItem2.Text = "&New";
            this.toolStripMenuItem2.Visible = false;
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItem3.Text = "&Open";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItem4.Text = "&Save";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.ToolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItem5.Text = "Save &As";
            this.toolStripMenuItem5.Visible = false;
            this.toolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItem6.Text = "&Print";
            this.toolStripMenuItem6.Visible = false;
            // 
            // toolStripMenuItemTEST
            // 
            this.toolStripMenuItemTEST.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemTEST.Image")));
            this.toolStripMenuItemTEST.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemTEST.Name = "toolStripMenuItemTEST";
            this.toolStripMenuItemTEST.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItemTEST.Text = "Test Pre&view";
            this.toolStripMenuItemTEST.Click += new System.EventHandler(this.ToolStripMenuItemTEST_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(140, 6);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(143, 26);
            this.toolStripMenuItem8.Text = "E&xit";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.menuStrip1);
            this.panel4.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel4.Size = new System.Drawing.Size(88, 29);
            this.panel4.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(88, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItemNEW,
            this.openToolStripMenuItemOPEN,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1,
            this.toolStripSeparator4,
            this.settingToolStripMenu,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newToolStripMenuItemNEW
            // 
            this.newToolStripMenuItemNEW.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItemNEW.Image")));
            this.newToolStripMenuItemNEW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItemNEW.Name = "newToolStripMenuItemNEW";
            this.newToolStripMenuItemNEW.Size = new System.Drawing.Size(116, 22);
            this.newToolStripMenuItemNEW.Text = "&New";
            this.newToolStripMenuItemNEW.Visible = false;
            this.newToolStripMenuItemNEW.Click += new System.EventHandler(this.NewToolStripMenuItemNEW_Click);
            // 
            // openToolStripMenuItemOPEN
            // 
            this.openToolStripMenuItemOPEN.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItemOPEN.Image")));
            this.openToolStripMenuItemOPEN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItemOPEN.Name = "openToolStripMenuItemOPEN";
            this.openToolStripMenuItemOPEN.Size = new System.Drawing.Size(116, 22);
            this.openToolStripMenuItemOPEN.Text = "&Open";
            this.openToolStripMenuItemOPEN.Click += new System.EventHandler(this.OpenToolStripMenuItemOPEN_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(113, 6);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem1.Image")));
            this.saveToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem1.Text = "&Save";
            this.saveToolStripMenuItem1.Visible = false;
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.SaveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.saveAsToolStripMenuItem1.Text = "Save &As";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.SaveAsToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(113, 6);
            // 
            // settingToolStripMenu
            // 
            this.settingToolStripMenu.Image = global::DevNoteCmdPlayer2.Properties.Resources.Control_TextBox;
            this.settingToolStripMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingToolStripMenu.Name = "settingToolStripMenu";
            this.settingToolStripMenu.Size = new System.Drawing.Size(116, 22);
            this.settingToolStripMenu.Text = "Settings";
            this.settingToolStripMenu.Click += new System.EventHandler(this.SettingToolStripMenu_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem1.Text = "E&xit";
            // 
            // flowMain
            // 
            this.flowMain.AutoSize = true;
            this.flowMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowMain.Controls.Add(this.groupBoxConsole);
            this.flowMain.Controls.Add(this.groupLib);
            this.flowMain.Controls.Add(this.panel3);
            this.flowMain.Controls.Add(this.panel2);
            this.flowMain.Controls.Add(this.panel1);
            this.flowMain.Controls.Add(this.groupBoxRec);
            this.flowMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.flowMain.Location = new System.Drawing.Point(0, 68);
            this.flowMain.MaximumSize = new System.Drawing.Size(725, 600);
            this.flowMain.MinimumSize = new System.Drawing.Size(300, 0);
            this.flowMain.Name = "flowMain";
            this.flowMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowMain.Size = new System.Drawing.Size(725, 389);
            this.flowMain.TabIndex = 4;
            // 
            // groupBoxConsole
            // 
            this.groupBoxConsole.Controls.Add(this.toolStripMenuBottom);
            this.groupBoxConsole.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxConsole.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBoxConsole.Location = new System.Drawing.Point(3, 3);
            this.groupBoxConsole.Name = "groupBoxConsole";
            this.groupBoxConsole.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxConsole.Size = new System.Drawing.Size(287, 77);
            this.groupBoxConsole.TabIndex = 6;
            this.groupBoxConsole.TabStop = false;
            this.groupBoxConsole.Text = "Console";
            this.groupBoxConsole.Visible = false;
            // 
            // groupLib
            // 
            this.groupLib.Controls.Add(this.addNewLibControl1);
            this.groupLib.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupLib.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLib.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupLib.Location = new System.Drawing.Point(3, 86);
            this.groupLib.Name = "groupLib";
            this.groupLib.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupLib.Size = new System.Drawing.Size(578, 500);
            this.groupLib.TabIndex = 7;
            this.groupLib.Visible = false;
            // 
            // addNewLibControl1
            // 
            this.addNewLibControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.addNewLibControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.addNewLibControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addNewLibControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewLibControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.addNewLibControl1.Location = new System.Drawing.Point(0, 0);
            this.addNewLibControl1.Margin = new System.Windows.Forms.Padding(4);
            this.addNewLibControl1.Name = "addNewLibControl1";
            this.addNewLibControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.addNewLibControl1.Size = new System.Drawing.Size(578, 500);
            this.addNewLibControl1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel3.Location = new System.Drawing.Point(3, 592);
            this.panel3.Name = "panel3";
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel3.Size = new System.Drawing.Size(403, 59);
            this.panel3.TabIndex = 3;
            this.panel3.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 25);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Open library of reordings.";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new System.Drawing.Point(3, 657);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel2.Size = new System.Drawing.Size(403, 59);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 25);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Play the latest recording.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(3, 722);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(403, 59);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(293, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create a fresh Chrome browser window for recording actions.";
            // 
            // groupBoxLimbo
            // 
            this.groupBoxLimbo.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxLimbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxLimbo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBoxLimbo.Location = new System.Drawing.Point(18, 8);
            this.groupBoxLimbo.Name = "groupBoxLimbo";
            this.groupBoxLimbo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxLimbo.Size = new System.Drawing.Size(287, 72);
            this.groupBoxLimbo.TabIndex = 6;
            this.groupBoxLimbo.TabStop = false;
            this.groupBoxLimbo.Text = "Console";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // frmDevNoteCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(830, 457);
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBoxLimbo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(849, 1198);
            this.MinimumSize = new System.Drawing.Size(399, 98);
            this.Name = "frmDevNoteCmd";
            this.Text = "DevNote Recorder";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDevNoteCmd_FormClosing);
            this.Load += new System.EventHandler(this.frmDevNoteCmd_Load);
            this.MouseLeave += new System.EventHandler(this.FrmDevNoteCmd_MouseLeave);
            this.Move += new System.EventHandler(this.FrmDevNoteCmd_Move);
            this.Resize += new System.EventHandler(this.FrmDevNoteCmd_Resize);
            this.groupBoxRec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionSource)).EndInit();
            this.toolStripMenuBottom.ResumeLayout(false);
            this.toolStripMenuBottom.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowMain.ResumeLayout(false);
            this.groupBoxConsole.ResumeLayout(false);
            this.groupBoxConsole.PerformLayout();
            this.groupLib.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdateUI;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.BindingSource actionSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxRec;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRec;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
        private System.Windows.Forms.GroupBox groupBoxConsole;
        private System.Windows.Forms.ToolStrip toolStripMenuBottom;
        private System.Windows.Forms.ToolStripLabel toolStripStatusLabelConsoleState;
        private System.Windows.Forms.GroupBox groupBoxLimbo;
        private System.Windows.Forms.DataGridView dgActions;
        private System.Windows.Forms.DataGridViewButtonColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Script;
        private System.Windows.Forms.DataGridViewTextBoxColumn command;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFailed;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel groupLib;
        private DevNoteWindowsFormsControlLibrary.DevNoteLibFormControl devNoteLibFormControl1;
        private DevNoteWindowsFormsControlLibrary.AddNewLibControl addNewLibControl1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItemNEW;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItemOPEN;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTEST;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}