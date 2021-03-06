using System.Windows.Forms;
namespace WeightingSystem
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolbarMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnDataSearch = new System.Windows.Forms.ToolStripSplitButton();
            this.过磅记录查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.过磅材料统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.干混出厂过磅记录查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出厂ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.转退料过磅记录查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.材料出厂记录查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.ConcreteWeight = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSiloStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlblOperator = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslConnectInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslConnectInfo2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            this.menuSpeech = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.称重完毕祝您一路平安ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进入厂区请安全驾驶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCustomerSpeech = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PicBox2 = new System.Windows.Forms.PictureBox();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdbPic4 = new System.Windows.Forms.RadioButton();
            this.rdbPic3 = new System.Windows.Forms.RadioButton();
            this.rdbPic2 = new System.Windows.Forms.RadioButton();
            this.rdbPic1 = new System.Windows.Forms.RadioButton();
            this.PicGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Video2 = new System.Windows.Forms.Panel();
            this.Video1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rdbVideo4 = new System.Windows.Forms.RadioButton();
            this.rdbVideo3 = new System.Windows.Forms.RadioButton();
            this.rdbVideo2 = new System.Windows.Forms.RadioButton();
            this.rdbVideo1 = new System.Windows.Forms.RadioButton();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.btnLastCarWeight = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.ckAutoSelectWeight = new System.Windows.Forms.CheckBox();
            this.txtHandCarWeight = new System.Windows.Forms.TextBox();
            this.btnHandCarWeight = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWeight = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtKZPercent = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLastKzValue = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.rdbValue = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.rdbPercent = new System.Windows.Forms.RadioButton();
            this.label22 = new System.Windows.Forms.Label();
            this.txtKzValue = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStopWeight = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCarWeight = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.btnCarNoSoftKeyboard = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.cbSilo = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbCar = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStartWeight = new System.Windows.Forms.Button();
            this.btnSpeech = new System.Windows.Forms.Button();
            this.btnGetCarWeight = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.FastMetage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuffName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.供应商 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuffInID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.规格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入仓 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pic1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentStuffID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pic2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinalStuffType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Driver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel15 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.toolbarMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuSpeech.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            this.panel3.SuspendLayout();
            this.PicGroup.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.Panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKZPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKzValue)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbarMain
            // 
            this.toolbarMain.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolbarMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbarMain.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.btnDataSearch,
            this.toolStripSeparator1,
            this.toolStripButton6,
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton10,
            this.toolStripButton11,
            this.ConcreteWeight,
            this.toolStripButton4,
            this.toolStripSeparator2,
            this.toolStripButton9,
            this.toolStripButtonSiloStatus,
            this.toolStripButton5});
            this.toolbarMain.Location = new System.Drawing.Point(0, 0);
            this.toolbarMain.Name = "toolbarMain";
            this.toolbarMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolbarMain.Size = new System.Drawing.Size(1316, 59);
            this.toolbarMain.TabIndex = 0;
            this.toolbarMain.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton1.Text = "车辆管理";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnDataSearch
            // 
            this.btnDataSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.过磅记录查询ToolStripMenuItem,
            this.过磅材料统计ToolStripMenuItem,
            this.干混出厂过磅记录查询ToolStripMenuItem,
            this.出厂ToolStripMenuItem,
            this.转退料过磅记录查询ToolStripMenuItem,
            this.材料出厂记录查询ToolStripMenuItem});
            this.btnDataSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnDataSearch.Image")));
            this.btnDataSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDataSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataSearch.Name = "btnDataSearch";
            this.btnDataSearch.Size = new System.Drawing.Size(81, 56);
            this.btnDataSearch.Text = "数据查询";
            this.btnDataSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // 过磅记录查询ToolStripMenuItem
            // 
            this.过磅记录查询ToolStripMenuItem.Name = "过磅记录查询ToolStripMenuItem";
            this.过磅记录查询ToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.过磅记录查询ToolStripMenuItem.Text = "材料入库记录查询";
            this.过磅记录查询ToolStripMenuItem.Click += new System.EventHandler(this.过磅记录查询ToolStripMenuItem_Click);
            // 
            // 过磅材料统计ToolStripMenuItem
            // 
            this.过磅材料统计ToolStripMenuItem.Name = "过磅材料统计ToolStripMenuItem";
            this.过磅材料统计ToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.过磅材料统计ToolStripMenuItem.Text = "过磅材料统计";
            this.过磅材料统计ToolStripMenuItem.Visible = false;
            this.过磅材料统计ToolStripMenuItem.Click += new System.EventHandler(this.过磅材料统计ToolStripMenuItem_Click);
            // 
            // 干混出厂过磅记录查询ToolStripMenuItem
            // 
            this.干混出厂过磅记录查询ToolStripMenuItem.Name = "干混出厂过磅记录查询ToolStripMenuItem";
            this.干混出厂过磅记录查询ToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.干混出厂过磅记录查询ToolStripMenuItem.Text = "干混出厂过磅记录查询";
            this.干混出厂过磅记录查询ToolStripMenuItem.Click += new System.EventHandler(this.干混出厂过磅记录查询ToolStripMenuItem_Click);
            // 
            // 出厂ToolStripMenuItem
            // 
            this.出厂ToolStripMenuItem.Name = "出厂ToolStripMenuItem";
            this.出厂ToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.出厂ToolStripMenuItem.Text = "出厂过磅记录查询";
            this.出厂ToolStripMenuItem.Click += new System.EventHandler(this.出厂ToolStripMenuItem_Click);
            // 
            // 转退料过磅记录查询ToolStripMenuItem
            // 
            this.转退料过磅记录查询ToolStripMenuItem.Name = "转退料过磅记录查询ToolStripMenuItem";
            this.转退料过磅记录查询ToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.转退料过磅记录查询ToolStripMenuItem.Text = "转退料记录查询";
            this.转退料过磅记录查询ToolStripMenuItem.Click += new System.EventHandler(this.转退料过磅记录查询ToolStripMenuItem_Click);
            // 
            // 材料出厂记录查询ToolStripMenuItem
            // 
            this.材料出厂记录查询ToolStripMenuItem.Name = "材料出厂记录查询ToolStripMenuItem";
            this.材料出厂记录查询ToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.材料出厂记录查询ToolStripMenuItem.Text = "材料出厂记录查询";
            this.材料出厂记录查询ToolStripMenuItem.Click += new System.EventHandler(this.材料出厂记录查询ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 59);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton6.Text = "材料管理";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton3.Text = "磅单设计";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(57, 48);
            this.toolStripButton2.Text = "毛重";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(83, 56);
            this.toolStripButton7.Text = "剩退料过磅";
            this.toolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(83, 56);
            this.toolStripButton8.Text = "分合车过磅";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton10.Text = "材料出厂";
            this.toolStripButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(97, 56);
            this.toolStripButton11.Text = "干混出厂过磅";
            this.toolStripButton11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // ConcreteWeight
            // 
            this.ConcreteWeight.Image = ((System.Drawing.Image)(resources.GetObject("ConcreteWeight.Image")));
            this.ConcreteWeight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ConcreteWeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConcreteWeight.Name = "ConcreteWeight";
            this.ConcreteWeight.Size = new System.Drawing.Size(69, 56);
            this.ConcreteWeight.Text = "出厂过磅";
            this.ConcreteWeight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ConcreteWeight.Click += new System.EventHandler(this.ConcreteWeight_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton4.Text = "系统设置";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Visible = false;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 59);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Image = global::WeightingSystem.Properties.Resources._530736;
            this.toolStripButton9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton9.Text = "重新登录";
            this.toolStripButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton9.Visible = false;
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripButtonSiloStatus
            // 
            this.toolStripButtonSiloStatus.Image = global::WeightingSystem.Properties.Resources._529365;
            this.toolStripButtonSiloStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSiloStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSiloStatus.Name = "toolStripButtonSiloStatus";
            this.toolStripButtonSiloStatus.Size = new System.Drawing.Size(83, 56);
            this.toolStripButtonSiloStatus.Text = "库存状态图";
            this.toolStripButtonSiloStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonSiloStatus.Click += new System.EventHandler(this.toolStripButtonSiloStatus_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(69, 56);
            this.toolStripButton5.Text = "退出系统";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlblOperator,
            this.tsslConnectInfo,
            this.tsslConnectInfo2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1316, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlblOperator
            // 
            this.tlblOperator.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tlblOperator.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tlblOperator.Name = "tlblOperator";
            this.tlblOperator.Size = new System.Drawing.Size(87, 21);
            this.tlblOperator.Text = "操作员:admin";
            // 
            // tsslConnectInfo
            // 
            this.tsslConnectInfo.AutoSize = false;
            this.tsslConnectInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslConnectInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslConnectInfo.Name = "tsslConnectInfo";
            this.tsslConnectInfo.Size = new System.Drawing.Size(100, 21);
            this.tsslConnectInfo.Text = "已连接地磅A";
            this.tsslConnectInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslConnectInfo2
            // 
            this.tsslConnectInfo2.AutoSize = false;
            this.tsslConnectInfo2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslConnectInfo2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsslConnectInfo2.Name = "tsslConnectInfo2";
            this.tsslConnectInfo2.Size = new System.Drawing.Size(100, 21);
            this.tsslConnectInfo2.Text = "已连接地磅B";
            this.tsslConnectInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerFlash
            // 
            this.timerFlash.Enabled = true;
            this.timerFlash.Interval = 3000;
            this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
            // 
            // menuSpeech
            // 
            this.menuSpeech.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.称重完毕祝您一路平安ToolStripMenuItem,
            this.进入厂区请安全驾驶ToolStripMenuItem,
            this.menuItemCustomerSpeech});
            this.menuSpeech.Name = "menuSpeech";
            this.menuSpeech.ShowImageMargin = false;
            this.menuSpeech.Size = new System.Drawing.Size(196, 70);
            // 
            // 称重完毕祝您一路平安ToolStripMenuItem
            // 
            this.称重完毕祝您一路平安ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.称重完毕祝您一路平安ToolStripMenuItem.Name = "称重完毕祝您一路平安ToolStripMenuItem";
            this.称重完毕祝您一路平安ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.称重完毕祝您一路平安ToolStripMenuItem.Text = "称重完毕，祝您一路平安！";
            this.称重完毕祝您一路平安ToolStripMenuItem.Click += new System.EventHandler(this.SpeechMenuItem_Click);
            // 
            // 进入厂区请安全驾驶ToolStripMenuItem
            // 
            this.进入厂区请安全驾驶ToolStripMenuItem.Name = "进入厂区请安全驾驶ToolStripMenuItem";
            this.进入厂区请安全驾驶ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.进入厂区请安全驾驶ToolStripMenuItem.Text = "进入厂区，请安全驾驶";
            this.进入厂区请安全驾驶ToolStripMenuItem.Click += new System.EventHandler(this.SpeechMenuItem_Click);
            // 
            // menuItemCustomerSpeech
            // 
            this.menuItemCustomerSpeech.Name = "menuItemCustomerSpeech";
            this.menuItemCustomerSpeech.Size = new System.Drawing.Size(195, 22);
            this.menuItemCustomerSpeech.Tag = "customer";
            this.menuItemCustomerSpeech.Text = "自定义...";
            this.menuItemCustomerSpeech.Click += new System.EventHandler(this.menuItemCustomerSpeech_Click);
            // 
            // serialPort
            // 
            this.serialPort.NewLine = "\r\n";
            this.serialPort.ReadTimeout = 3000;
            this.serialPort.RtsEnable = true;
            this.serialPort.WriteTimeout = 3000;
            this.serialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort_ErrorReceived);
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel6);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(329, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(987, 649);
            this.panel9.TabIndex = 36;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.splitContainer1);
            this.panel6.Controls.Add(this.Panel8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(987, 649);
            this.panel6.TabIndex = 38;
            // 
            // splitContainer1
            // 
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PicGroup);
            this.splitContainer1.Size = new System.Drawing.Size(983, 418);
            this.splitContainer1.SplitterDistance = 493;
            this.splitContainer1.TabIndex = 35;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 418);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "皮重照片";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.PicBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PicBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(487, 396);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // PicBox2
            // 
            this.PicBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBox2.ErrorImage = null;
            this.PicBox2.Location = new System.Drawing.Point(3, 201);
            this.PicBox2.Name = "PicBox2";
            this.PicBox2.Size = new System.Drawing.Size(481, 192);
            this.PicBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBox2.TabIndex = 2;
            this.PicBox2.TabStop = false;
            // 
            // PicBox
            // 
            this.PicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBox.ErrorImage = null;
            this.PicBox.Location = new System.Drawing.Point(3, 3);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(481, 192);
            this.PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBox.TabIndex = 2;
            this.PicBox.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdbPic4);
            this.panel3.Controls.Add(this.rdbPic3);
            this.panel3.Controls.Add(this.rdbPic2);
            this.panel3.Controls.Add(this.rdbPic1);
            this.panel3.Location = new System.Drawing.Point(78, -4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(343, 21);
            this.panel3.TabIndex = 0;
            this.panel3.Visible = false;
            // 
            // rdbPic4
            // 
            this.rdbPic4.AutoSize = true;
            this.rdbPic4.ForeColor = System.Drawing.Color.Black;
            this.rdbPic4.Location = new System.Drawing.Point(264, 4);
            this.rdbPic4.Name = "rdbPic4";
            this.rdbPic4.Size = new System.Drawing.Size(81, 18);
            this.rdbPic4.TabIndex = 2;
            this.rdbPic4.Text = "4#摄像头";
            this.rdbPic4.UseVisualStyleBackColor = true;
            // 
            // rdbPic3
            // 
            this.rdbPic3.AutoSize = true;
            this.rdbPic3.ForeColor = System.Drawing.Color.Black;
            this.rdbPic3.Location = new System.Drawing.Point(177, 4);
            this.rdbPic3.Name = "rdbPic3";
            this.rdbPic3.Size = new System.Drawing.Size(81, 18);
            this.rdbPic3.TabIndex = 2;
            this.rdbPic3.Text = "3#摄像头";
            this.rdbPic3.UseVisualStyleBackColor = true;
            // 
            // rdbPic2
            // 
            this.rdbPic2.AutoSize = true;
            this.rdbPic2.ForeColor = System.Drawing.Color.Black;
            this.rdbPic2.Location = new System.Drawing.Point(90, 4);
            this.rdbPic2.Name = "rdbPic2";
            this.rdbPic2.Size = new System.Drawing.Size(81, 18);
            this.rdbPic2.TabIndex = 1;
            this.rdbPic2.Text = "2#摄像头";
            this.rdbPic2.UseVisualStyleBackColor = true;
            // 
            // rdbPic1
            // 
            this.rdbPic1.AutoSize = true;
            this.rdbPic1.Checked = true;
            this.rdbPic1.ForeColor = System.Drawing.Color.Black;
            this.rdbPic1.Location = new System.Drawing.Point(3, 3);
            this.rdbPic1.Name = "rdbPic1";
            this.rdbPic1.Size = new System.Drawing.Size(81, 18);
            this.rdbPic1.TabIndex = 0;
            this.rdbPic1.TabStop = true;
            this.rdbPic1.Text = "1#摄像头";
            this.rdbPic1.UseVisualStyleBackColor = true;
            this.rdbPic1.CheckedChanged += new System.EventHandler(this.rdbPic1_CheckedChanged);
            // 
            // PicGroup
            // 
            this.PicGroup.Controls.Add(this.tableLayoutPanel2);
            this.PicGroup.Controls.Add(this.panel7);
            this.PicGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicGroup.ForeColor = System.Drawing.Color.Black;
            this.PicGroup.Location = new System.Drawing.Point(0, 0);
            this.PicGroup.Name = "PicGroup";
            this.PicGroup.Size = new System.Drawing.Size(486, 418);
            this.PicGroup.TabIndex = 1;
            this.PicGroup.TabStop = false;
            this.PicGroup.Text = "实时视频";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.Video2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Video1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(480, 396);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Video2
            // 
            this.Video2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Video2.Location = new System.Drawing.Point(3, 201);
            this.Video2.Name = "Video2";
            this.Video2.Size = new System.Drawing.Size(474, 192);
            this.Video2.TabIndex = 2;
            // 
            // Video1
            // 
            this.Video1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Video1.Location = new System.Drawing.Point(3, 3);
            this.Video1.Name = "Video1";
            this.Video1.Size = new System.Drawing.Size(474, 192);
            this.Video1.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.rdbVideo4);
            this.panel7.Controls.Add(this.rdbVideo3);
            this.panel7.Controls.Add(this.rdbVideo2);
            this.panel7.Controls.Add(this.rdbVideo1);
            this.panel7.Location = new System.Drawing.Point(78, -3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(354, 18);
            this.panel7.TabIndex = 0;
            this.panel7.Visible = false;
            // 
            // rdbVideo4
            // 
            this.rdbVideo4.AutoSize = true;
            this.rdbVideo4.ForeColor = System.Drawing.Color.Black;
            this.rdbVideo4.Location = new System.Drawing.Point(270, 2);
            this.rdbVideo4.Name = "rdbVideo4";
            this.rdbVideo4.Size = new System.Drawing.Size(81, 18);
            this.rdbVideo4.TabIndex = 1;
            this.rdbVideo4.Text = "4#摄像头";
            this.rdbVideo4.UseVisualStyleBackColor = true;
            // 
            // rdbVideo3
            // 
            this.rdbVideo3.AutoSize = true;
            this.rdbVideo3.ForeColor = System.Drawing.Color.Black;
            this.rdbVideo3.Location = new System.Drawing.Point(190, 2);
            this.rdbVideo3.Name = "rdbVideo3";
            this.rdbVideo3.Size = new System.Drawing.Size(81, 18);
            this.rdbVideo3.TabIndex = 2;
            this.rdbVideo3.Text = "3#摄像头";
            this.rdbVideo3.UseVisualStyleBackColor = true;
            // 
            // rdbVideo2
            // 
            this.rdbVideo2.AutoSize = true;
            this.rdbVideo2.ForeColor = System.Drawing.Color.Black;
            this.rdbVideo2.Location = new System.Drawing.Point(98, 1);
            this.rdbVideo2.Name = "rdbVideo2";
            this.rdbVideo2.Size = new System.Drawing.Size(81, 18);
            this.rdbVideo2.TabIndex = 1;
            this.rdbVideo2.Text = "2#摄像头";
            this.rdbVideo2.UseVisualStyleBackColor = true;
            // 
            // rdbVideo1
            // 
            this.rdbVideo1.AutoSize = true;
            this.rdbVideo1.Checked = true;
            this.rdbVideo1.ForeColor = System.Drawing.Color.Black;
            this.rdbVideo1.Location = new System.Drawing.Point(3, 1);
            this.rdbVideo1.Name = "rdbVideo1";
            this.rdbVideo1.Size = new System.Drawing.Size(81, 18);
            this.rdbVideo1.TabIndex = 0;
            this.rdbVideo1.TabStop = true;
            this.rdbVideo1.Text = "1#摄像头";
            this.rdbVideo1.UseVisualStyleBackColor = true;
            this.rdbVideo1.CheckedChanged += new System.EventHandler(this.rdbVideo1_CheckedChanged);
            // 
            // Panel8
            // 
            this.Panel8.AutoSize = true;
            this.Panel8.Controls.Add(this.btnLastCarWeight);
            this.Panel8.Controls.Add(this.pictureBox1);
            this.Panel8.Controls.Add(this.textBox1);
            this.Panel8.Controls.Add(this.button4);
            this.Panel8.Controls.Add(this.ckAutoSelectWeight);
            this.Panel8.Controls.Add(this.txtHandCarWeight);
            this.Panel8.Controls.Add(this.btnHandCarWeight);
            this.Panel8.Controls.Add(this.tabControl1);
            this.Panel8.Controls.Add(this.groupBox1);
            this.Panel8.Controls.Add(this.button3);
            this.Panel8.Controls.Add(this.button1);
            this.Panel8.Controls.Add(this.btnStopWeight);
            this.Panel8.Controls.Add(this.txtRemark);
            this.Panel8.Controls.Add(this.label21);
            this.Panel8.Controls.Add(this.checkBox1);
            this.Panel8.Controls.Add(this.txtWeight);
            this.Panel8.Controls.Add(this.label20);
            this.Panel8.Controls.Add(this.txtCarWeight);
            this.Panel8.Controls.Add(this.label19);
            this.Panel8.Controls.Add(this.txtTotalWeight);
            this.Panel8.Controls.Add(this.btnCarNoSoftKeyboard);
            this.Panel8.Controls.Add(this.label18);
            this.Panel8.Controls.Add(this.cbSilo);
            this.Panel8.Controls.Add(this.label17);
            this.Panel8.Controls.Add(this.cbCar);
            this.Panel8.Controls.Add(this.label4);
            this.Panel8.Controls.Add(this.btnStartWeight);
            this.Panel8.Controls.Add(this.btnSpeech);
            this.Panel8.Controls.Add(this.btnGetCarWeight);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel8.Location = new System.Drawing.Point(0, 418);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(983, 227);
            this.Panel8.TabIndex = 34;
            // 
            // btnLastCarWeight
            // 
            this.btnLastCarWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastCarWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLastCarWeight.Location = new System.Drawing.Point(545, 95);
            this.btnLastCarWeight.Name = "btnLastCarWeight";
            this.btnLastCarWeight.Size = new System.Drawing.Size(74, 29);
            this.btnLastCarWeight.TabIndex = 60;
            this.btnLastCarWeight.Text = "上次皮重";
            this.btnLastCarWeight.UseVisualStyleBackColor = true;
            this.btnLastCarWeight.Click += new System.EventHandler(this.btnLastCarWeight_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(226, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 24);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(438, 153);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 23);
            this.textBox1.TabIndex = 59;
            this.textBox1.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(438, 190);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 58;
            this.button4.Text = "手动皮重";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // ckAutoSelectWeight
            // 
            this.ckAutoSelectWeight.AutoSize = true;
            this.ckAutoSelectWeight.Location = new System.Drawing.Point(386, 12);
            this.ckAutoSelectWeight.Name = "ckAutoSelectWeight";
            this.ckAutoSelectWeight.Size = new System.Drawing.Size(82, 18);
            this.ckAutoSelectWeight.TabIndex = 57;
            this.ckAutoSelectWeight.Text = "自动选磅";
            this.ckAutoSelectWeight.UseVisualStyleBackColor = true;
            this.ckAutoSelectWeight.CheckedChanged += new System.EventHandler(this.ckAutoSelectWeight_CheckedChanged);
            // 
            // txtHandCarWeight
            // 
            this.txtHandCarWeight.Location = new System.Drawing.Point(386, 11);
            this.txtHandCarWeight.Name = "txtHandCarWeight";
            this.txtHandCarWeight.Size = new System.Drawing.Size(78, 23);
            this.txtHandCarWeight.TabIndex = 56;
            this.txtHandCarWeight.Visible = false;
            // 
            // btnHandCarWeight
            // 
            this.btnHandCarWeight.Location = new System.Drawing.Point(386, 40);
            this.btnHandCarWeight.Name = "btnHandCarWeight";
            this.btnHandCarWeight.Size = new System.Drawing.Size(55, 38);
            this.btnHandCarWeight.TabIndex = 55;
            this.btnHandCarWeight.Text = "手动毛重";
            this.btnHandCarWeight.UseVisualStyleBackColor = true;
            this.btnHandCarWeight.Visible = false;
            this.btnHandCarWeight.Click += new System.EventHandler(this.btnHandCarWeight_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(4, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(258, 99);
            this.tabControl1.TabIndex = 54;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.ForeColor = System.Drawing.Color.Red;
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(250, 56);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "匿名A";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblWeight);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 50);
            this.panel1.TabIndex = 1;
            // 
            // lblWeight
            // 
            this.lblWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeight.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.Color.Red;
            this.lblWeight.Location = new System.Drawing.Point(0, 0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblWeight.Size = new System.Drawing.Size(244, 50);
            this.lblWeight.TabIndex = 0;
            this.lblWeight.Text = "0";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWeight.TextChanged += new System.EventHandler(this.lblWeight_TextChanged_1);
            // 
            // tabPage2
            // 
            this.tabPage2.ForeColor = System.Drawing.Color.Red;
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(250, 56);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "匿名B";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtKZPercent);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtLastKzValue);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.rdbValue);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.rdbPercent);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtKzValue);
            this.groupBox1.Location = new System.Drawing.Point(7, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 86);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // txtKZPercent
            // 
            this.txtKZPercent.DecimalPlaces = 2;
            this.txtKZPercent.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKZPercent.Location = new System.Drawing.Point(303, 54);
            this.txtKZPercent.Name = "txtKZPercent";
            this.txtKZPercent.Size = new System.Drawing.Size(82, 29);
            this.txtKZPercent.TabIndex = 47;
            this.txtKZPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKZPercent.ValueChanged += new System.EventHandler(this.txtKZPercent_ValueChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(2, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 19);
            this.label13.TabIndex = 23;
            this.label13.Text = "最后扣杂：";
            // 
            // txtLastKzValue
            // 
            this.txtLastKzValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLastKzValue.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLastKzValue.Location = new System.Drawing.Point(109, 14);
            this.txtLastKzValue.Name = "txtLastKzValue";
            this.txtLastKzValue.ReadOnly = true;
            this.txtLastKzValue.Size = new System.Drawing.Size(188, 32);
            this.txtLastKzValue.TabIndex = 24;
            this.txtLastKzValue.Text = "0";
            this.txtLastKzValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastKzValue.TextChanged += new System.EventHandler(this.txtLastKzValue_TextChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(386, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 19);
            this.label16.TabIndex = 31;
            this.label16.Text = "%";
            // 
            // rdbValue
            // 
            this.rdbValue.AutoSize = true;
            this.rdbValue.Location = new System.Drawing.Point(7, 58);
            this.rdbValue.Name = "rdbValue";
            this.rdbValue.Size = new System.Drawing.Size(53, 18);
            this.rdbValue.TabIndex = 48;
            this.rdbValue.Text = "固定";
            this.rdbValue.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(316, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 19);
            this.label11.TabIndex = 45;
            this.label11.Text = "Kg";
            // 
            // rdbPercent
            // 
            this.rdbPercent.AutoSize = true;
            this.rdbPercent.Checked = true;
            this.rdbPercent.Location = new System.Drawing.Point(219, 59);
            this.rdbPercent.Name = "rdbPercent";
            this.rdbPercent.Size = new System.Drawing.Size(67, 18);
            this.rdbPercent.TabIndex = 1;
            this.rdbPercent.TabStop = true;
            this.rdbPercent.Text = "百分比";
            this.rdbPercent.UseVisualStyleBackColor = true;
            this.rdbPercent.CheckedChanged += new System.EventHandler(this.rdbPercent_CheckedChanged);
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(163, 58);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 19);
            this.label22.TabIndex = 46;
            this.label22.Text = "Kg";
            // 
            // txtKzValue
            // 
            this.txtKzValue.Enabled = false;
            this.txtKzValue.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKzValue.Location = new System.Drawing.Point(68, 53);
            this.txtKzValue.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtKzValue.Name = "txtKzValue";
            this.txtKzValue.Size = new System.Drawing.Size(94, 29);
            this.txtKzValue.TabIndex = 0;
            this.txtKzValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKzValue.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_3);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Image = global::WeightingSystem.Properties.Resources._529365;
            this.button3.Location = new System.Drawing.Point(629, 175);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 42);
            this.button3.TabIndex = 50;
            this.button3.Text = "换罐";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(56, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 42);
            this.button1.TabIndex = 49;
            this.button1.Text = "空车过磅";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStopWeight
            // 
            this.btnStopWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStopWeight.Location = new System.Drawing.Point(264, 10);
            this.btnStopWeight.Name = "btnStopWeight";
            this.btnStopWeight.Size = new System.Drawing.Size(53, 82);
            this.btnStopWeight.TabIndex = 25;
            this.btnStopWeight.Text = "采集皮重";
            this.btnStopWeight.UseVisualStyleBackColor = true;
            this.btnStopWeight.Click += new System.EventHandler(this.btnStopWeight_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(684, 137);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(279, 32);
            this.txtRemark.TabIndex = 44;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(625, 142);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 19);
            this.label21.TabIndex = 43;
            this.label21.Text = "备注：";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(417, 195);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 18);
            this.checkBox1.TabIndex = 42;
            this.checkBox1.Text = "打印磅单";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtWeight
            // 
            this.txtWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWeight.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWeight.ForeColor = System.Drawing.Color.Black;
            this.txtWeight.Location = new System.Drawing.Point(857, 95);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(106, 32);
            this.txtWeight.TabIndex = 41;
            this.txtWeight.Text = "0";
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(796, 100);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 19);
            this.label20.TabIndex = 40;
            this.label20.Text = "净重：";
            // 
            // txtCarWeight
            // 
            this.txtCarWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarWeight.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarWeight.ForeColor = System.Drawing.Color.Red;
            this.txtCarWeight.Location = new System.Drawing.Point(684, 96);
            this.txtCarWeight.Name = "txtCarWeight";
            this.txtCarWeight.ReadOnly = true;
            this.txtCarWeight.Size = new System.Drawing.Size(106, 32);
            this.txtCarWeight.TabIndex = 39;
            this.txtCarWeight.Text = "0";
            this.txtCarWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCarWeight.TextChanged += new System.EventHandler(this.txtCarWeight_TextChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(625, 100);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 19);
            this.label19.TabIndex = 38;
            this.label19.Text = "皮重：";
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalWeight.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTotalWeight.ForeColor = System.Drawing.Color.Black;
            this.txtTotalWeight.Location = new System.Drawing.Point(857, 56);
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.ReadOnly = true;
            this.txtTotalWeight.Size = new System.Drawing.Size(106, 32);
            this.txtTotalWeight.TabIndex = 37;
            this.txtTotalWeight.Text = "0";
            this.txtTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalWeight.TextChanged += new System.EventHandler(this.txtTotalWeight_TextChanged);
            // 
            // btnCarNoSoftKeyboard
            // 
            this.btnCarNoSoftKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCarNoSoftKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCarNoSoftKeyboard.Location = new System.Drawing.Point(929, 22);
            this.btnCarNoSoftKeyboard.Name = "btnCarNoSoftKeyboard";
            this.btnCarNoSoftKeyboard.Size = new System.Drawing.Size(37, 27);
            this.btnCarNoSoftKeyboard.TabIndex = 1;
            this.btnCarNoSoftKeyboard.Text = "...";
            this.btnCarNoSoftKeyboard.UseVisualStyleBackColor = true;
            this.btnCarNoSoftKeyboard.Click += new System.EventHandler(this.btnCarNoSoftKeyboard_Click_1);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(796, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 19);
            this.label18.TabIndex = 36;
            this.label18.Text = "毛重：";
            // 
            // cbSilo
            // 
            this.cbSilo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSilo.DisplayMember = "SiloName";
            this.cbSilo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSilo.DropDownWidth = 150;
            this.cbSilo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSilo.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSilo.FormattingEnabled = true;
            this.cbSilo.Location = new System.Drawing.Point(684, 58);
            this.cbSilo.Name = "cbSilo";
            this.cbSilo.Size = new System.Drawing.Size(106, 27);
            this.cbSilo.TabIndex = 35;
            this.cbSilo.ValueMember = "SiloID";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(624, 65);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 19);
            this.label17.TabIndex = 34;
            this.label17.Text = "库位：";
            // 
            // cbCar
            // 
            this.cbCar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCar.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCar.FormattingEnabled = true;
            this.cbCar.Location = new System.Drawing.Point(684, 23);
            this.cbCar.Name = "cbCar";
            this.cbCar.Size = new System.Drawing.Size(239, 27);
            this.cbCar.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(624, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 32;
            this.label4.Text = "车号：";
            // 
            // btnStartWeight
            // 
            this.btnStartWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartWeight.Enabled = false;
            this.btnStartWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartWeight.Location = new System.Drawing.Point(323, 10);
            this.btnStartWeight.Name = "btnStartWeight";
            this.btnStartWeight.Size = new System.Drawing.Size(55, 82);
            this.btnStartWeight.TabIndex = 26;
            this.btnStartWeight.Text = "重新取皮";
            this.btnStartWeight.UseVisualStyleBackColor = true;
            this.btnStartWeight.Click += new System.EventHandler(this.btnStartWeight_Click);
            // 
            // btnSpeech
            // 
            this.btnSpeech.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpeech.BackColor = System.Drawing.Color.Transparent;
            this.btnSpeech.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSpeech.BackgroundImage")));
            this.btnSpeech.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSpeech.FlatAppearance.BorderSize = 0;
            this.btnSpeech.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpeech.Location = new System.Drawing.Point(9, 184);
            this.btnSpeech.Name = "btnSpeech";
            this.btnSpeech.Size = new System.Drawing.Size(41, 37);
            this.btnSpeech.TabIndex = 22;
            this.btnSpeech.UseVisualStyleBackColor = false;
            this.btnSpeech.Click += new System.EventHandler(this.btnSpeech_Click_1);
            // 
            // btnGetCarWeight
            // 
            this.btnGetCarWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetCarWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGetCarWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetCarWeight.Image = ((System.Drawing.Image)(resources.GetObject("btnGetCarWeight.Image")));
            this.btnGetCarWeight.Location = new System.Drawing.Point(754, 175);
            this.btnGetCarWeight.Name = "btnGetCarWeight";
            this.btnGetCarWeight.Size = new System.Drawing.Size(209, 42);
            this.btnGetCarWeight.TabIndex = 19;
            this.btnGetCarWeight.Text = "去皮";
            this.btnGetCarWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetCarWeight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGetCarWeight.UseVisualStyleBackColor = false;
            this.btnGetCarWeight.Click += new System.EventHandler(this.btnGetCarWeight_Click_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel14);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1316, 649);
            this.panel2.TabIndex = 5;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel12);
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(329, 649);
            this.panel14.TabIndex = 36;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.dataGridView1);
            this.panel12.Controls.Add(this.dgvList);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(329, 586);
            this.panel12.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 400);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(329, 186);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "StuffInID";
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CarNo";
            this.dataGridViewTextBoxColumn3.FillWeight = 15F;
            this.dataGridViewTextBoxColumn3.HeaderText = "车号";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "StuffName";
            this.dataGridViewTextBoxColumn4.FillWeight = 20F;
            this.dataGridViewTextBoxColumn4.HeaderText = "材料名";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "carweight";
            this.dataGridViewTextBoxColumn11.HeaderText = "皮重";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "TotalNum";
            this.dataGridViewTextBoxColumn5.FillWeight = 10F;
            this.dataGridViewTextBoxColumn5.HeaderText = "毛重";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "SupplyName";
            this.dataGridViewTextBoxColumn6.HeaderText = "供应商";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "OutDate";
            this.dataGridViewTextBoxColumn7.HeaderText = "去皮时间";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.ColumnHeadersHeight = 40;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FastMetage,
            this.CarNo,
            this.StuffName,
            this.供应商,
            this.TotalNum,
            this.StuffInID,
            this.规格,
            this.入仓,
            this.InDate,
            this.Pic1,
            this.ParentStuffID,
            this.Pic2,
            this.FinalStuffType,
            this.InNum,
            this.CompanyID,
            this.SourceNumber,
            this.Driver,
            this.SourceAddr,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.SpecID});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvList.RowTemplate.Height = 100;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(329, 400);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            this.dgvList.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvList_RowStateChanged);
            // 
            // FastMetage
            // 
            this.FastMetage.DataPropertyName = "FastMetage";
            this.FastMetage.HeaderText = "";
            this.FastMetage.Name = "FastMetage";
            this.FastMetage.ReadOnly = true;
            this.FastMetage.Visible = false;
            // 
            // CarNo
            // 
            this.CarNo.DataPropertyName = "CarNo";
            this.CarNo.FillWeight = 15F;
            this.CarNo.HeaderText = "车号";
            this.CarNo.Name = "CarNo";
            this.CarNo.ReadOnly = true;
            this.CarNo.Width = 65;
            // 
            // StuffName
            // 
            this.StuffName.DataPropertyName = "StuffName";
            this.StuffName.FillWeight = 20F;
            this.StuffName.HeaderText = "材料名";
            this.StuffName.Name = "StuffName";
            this.StuffName.ReadOnly = true;
            this.StuffName.Width = 78;
            // 
            // 供应商
            // 
            this.供应商.DataPropertyName = "SupplyName";
            this.供应商.HeaderText = "供应商";
            this.供应商.Name = "供应商";
            this.供应商.ReadOnly = true;
            this.供应商.Width = 110;
            // 
            // TotalNum
            // 
            this.TotalNum.DataPropertyName = "TotalNum";
            this.TotalNum.FillWeight = 10F;
            this.TotalNum.HeaderText = "毛重";
            this.TotalNum.Name = "TotalNum";
            this.TotalNum.ReadOnly = true;
            this.TotalNum.Width = 80;
            // 
            // StuffInID
            // 
            this.StuffInID.DataPropertyName = "StuffInID";
            this.StuffInID.HeaderText = "入库单号";
            this.StuffInID.Name = "StuffInID";
            this.StuffInID.ReadOnly = true;
            this.StuffInID.Width = 120;
            // 
            // 规格
            // 
            this.规格.DataPropertyName = "Spec";
            this.规格.HeaderText = "规格";
            this.规格.Name = "规格";
            this.规格.ReadOnly = true;
            // 
            // 入仓
            // 
            this.入仓.DataPropertyName = "SiloName";
            this.入仓.HeaderText = "入仓";
            this.入仓.Name = "入仓";
            this.入仓.ReadOnly = true;
            // 
            // InDate
            // 
            this.InDate.DataPropertyName = "InDate";
            this.InDate.FillWeight = 20F;
            this.InDate.HeaderText = "过磅时间";
            this.InDate.Name = "InDate";
            this.InDate.ReadOnly = true;
            this.InDate.Width = 120;
            // 
            // Pic1
            // 
            this.Pic1.DataPropertyName = "Pic1";
            this.Pic1.HeaderText = "";
            this.Pic1.Name = "Pic1";
            this.Pic1.ReadOnly = true;
            this.Pic1.Visible = false;
            // 
            // ParentStuffID
            // 
            this.ParentStuffID.DataPropertyName = "ParentID";
            this.ParentStuffID.HeaderText = "ParentID";
            this.ParentStuffID.Name = "ParentStuffID";
            this.ParentStuffID.ReadOnly = true;
            this.ParentStuffID.Visible = false;
            // 
            // Pic2
            // 
            this.Pic2.DataPropertyName = "Pic2";
            this.Pic2.HeaderText = "";
            this.Pic2.Name = "Pic2";
            this.Pic2.ReadOnly = true;
            this.Pic2.Visible = false;
            // 
            // FinalStuffType
            // 
            this.FinalStuffType.DataPropertyName = "FinalStuffType";
            this.FinalStuffType.HeaderText = "";
            this.FinalStuffType.Name = "FinalStuffType";
            this.FinalStuffType.ReadOnly = true;
            this.FinalStuffType.Visible = false;
            // 
            // InNum
            // 
            this.InNum.DataPropertyName = "InNum";
            this.InNum.HeaderText = "入库数量";
            this.InNum.Name = "InNum";
            this.InNum.ReadOnly = true;
            // 
            // CompanyID
            // 
            this.CompanyID.HeaderText = "收货单位";
            this.CompanyID.Name = "CompanyID";
            this.CompanyID.ReadOnly = true;
            // 
            // SourceNumber
            // 
            this.SourceNumber.HeaderText = "来源单据号";
            this.SourceNumber.Name = "SourceNumber";
            this.SourceNumber.ReadOnly = true;
            // 
            // Driver
            // 
            this.Driver.HeaderText = "司机";
            this.Driver.Name = "Driver";
            this.Driver.ReadOnly = true;
            // 
            // SourceAddr
            // 
            this.SourceAddr.HeaderText = "材料来源地";
            this.SourceAddr.Name = "SourceAddr";
            this.SourceAddr.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // SpecID
            // 
            this.SpecID.HeaderText = "SpecID";
            this.SpecID.Name = "SpecID";
            this.SpecID.ReadOnly = true;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.btnRefresh);
            this.panel15.Controls.Add(this.button2);
            this.panel15.Controls.Add(this.btnRemove);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel15.Location = new System.Drawing.Point(0, 586);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(329, 63);
            this.panel15.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(206, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(63, 42);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(4, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 42);
            this.button2.TabIndex = 24;
            this.button2.Text = "换材料";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(105, 11);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(95, 42);
            this.btnRemove.TabIndex = 23;
            this.btnRemove.Text = "删除";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // serialPort2
            // 
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 734);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolbarMain);
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = global::WeightingSystem.Properties.Resources.LOGO;
            this.Name = "MainForm";
            this.Text = "中联重科称重系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing_1);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolbarMain.ResumeLayout(false);
            this.toolbarMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuSpeech.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.PicGroup.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKZPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKzValue)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.panel15.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbarMain;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnectInfo;
        private System.Windows.Forms.Timer timerFlash;
        private System.Windows.Forms.ToolStripStatusLabel tlblOperator;
        private System.Windows.Forms.ContextMenuStrip menuSpeech;
        private System.Windows.Forms.ToolStripMenuItem 称重完毕祝您一路平安ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 进入厂区请安全驾驶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemCustomerSpeech;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox PicGroup;
        private System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.RadioButton rdbVideo1;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSplitButton btnDataSearch;
        private System.Windows.Forms.ToolStripMenuItem 过磅记录查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 过磅材料统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ConcreteWeight;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private ToolStripMenuItem 出厂ToolStripMenuItem;
        private ToolStripMenuItem 转退料过磅记录查询ToolStripMenuItem;
        private ToolStripButton toolStripButton8;
        private ToolStripButton toolStripButton9;
        private System.IO.Ports.SerialPort serialPort2;
        private ToolStripStatusLabel tsslConnectInfo2;
        public RadioButton rdbVideo4;
        public RadioButton rdbVideo3;
        public RadioButton rdbVideo2;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel Panel8;
        private CheckBox ckAutoSelectWeight;
        private TextBox txtHandCarWeight;
        private Button btnHandCarWeight;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel1;
        public Label lblWeight;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private NumericUpDown txtKZPercent;
        private Label label13;
        private TextBox txtLastKzValue;
        private Label label16;
        private RadioButton rdbValue;
        private Label label11;
        private RadioButton rdbPercent;
        private Label label22;
        private NumericUpDown txtKzValue;
        private Button button3;
        private Button button1;
        public Button btnStopWeight;
        private TextBox txtRemark;
        private Label label21;
        private CheckBox checkBox1;
        private TextBox txtWeight;
        private Label label20;
        private Label label19;
        private TextBox txtTotalWeight;
        private Button btnCarNoSoftKeyboard;
        private Label label18;
        private ComboBox cbSilo;
        private Label label17;
        private ComboBox cbCar;
        private Label label4;
        public Button btnStartWeight;
        private Button btnSpeech;
        private Button btnGetCarWeight;
        private TextBox textBox1;
        private Button button4;
        public TextBox txtCarWeight;
        private PictureBox pictureBox1;
        private Button btnLastCarWeight;
        private Panel Video2;
        private Panel Video1;
        private ToolStripButton toolStripButton10;
        private ToolStripButton toolStripButton11;
        private ToolStripMenuItem 材料出厂记录查询ToolStripMenuItem;
        private ToolStripButton toolStripButtonSiloStatus;
        private ToolStripMenuItem 干混出厂过磅记录查询ToolStripMenuItem;
        private Panel panel14;
        private Panel panel12;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridView dgvList;
        private Panel panel15;
        private Button btnRefresh;
        private Button button2;
        private Button btnRemove;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox PicBox2;
        private PictureBox PicBox;
        private Panel panel3;
        private RadioButton rdbPic4;
        private RadioButton rdbPic3;
        private RadioButton rdbPic2;
        private RadioButton rdbPic1;
        private DataGridViewTextBoxColumn FastMetage;
        private DataGridViewTextBoxColumn CarNo;
        private DataGridViewTextBoxColumn StuffName;
        private DataGridViewTextBoxColumn 供应商;
        private DataGridViewTextBoxColumn TotalNum;
        private DataGridViewTextBoxColumn StuffInID;
        private DataGridViewTextBoxColumn 规格;
        private DataGridViewTextBoxColumn 入仓;
        private DataGridViewTextBoxColumn InDate;
        private DataGridViewTextBoxColumn Pic1;
        private DataGridViewTextBoxColumn ParentStuffID;
        private DataGridViewTextBoxColumn Pic2;
        private DataGridViewTextBoxColumn FinalStuffType;
        private DataGridViewTextBoxColumn InNum;
        private DataGridViewTextBoxColumn CompanyID;
        private DataGridViewTextBoxColumn SourceNumber;
        private DataGridViewTextBoxColumn Driver;
        private DataGridViewTextBoxColumn SourceAddr;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn SpecID;
    }
}

