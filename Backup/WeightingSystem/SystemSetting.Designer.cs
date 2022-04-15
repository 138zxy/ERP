namespace WeightingSystem
{
    partial class SystemSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtIdLen = new System.Windows.Forms.TextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdKZValue = new System.Windows.Forms.RadioButton();
            this.rdKZRate = new System.Windows.Forms.RadioButton();
            this.chkCarWeight = new System.Windows.Forms.CheckBox();
            this.chkStuffInputFree = new System.Windows.Forms.CheckBox();
            this.chkSoundWeight = new System.Windows.Forms.CheckBox();
            this.chkAutoPrint = new System.Windows.Forms.CheckBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ConnectInfo = new System.Windows.Forms.Label();
            this.btnTestSP = new System.Windows.Forms.Button();
            this.txtEndChar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStartChar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDataLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdReversion = new System.Windows.Forms.RadioButton();
            this.rdPositive = new System.Windows.Forms.RadioButton();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCOMList = new System.Windows.Forms.ComboBox();
            this.COMLabel = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnTestDBConnect = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdSyn2 = new System.Windows.Forms.RadioButton();
            this.rdSyn1 = new System.Windows.Forms.RadioButton();
            this.rdSyn0 = new System.Windows.Forms.RadioButton();
            this.SettingCancel = new System.Windows.Forms.Button();
            this.SP = new System.IO.Ports.SerialPort(this.components);
            this.SettingOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(437, 278);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.chkCarWeight);
            this.tabPage1.Controls.Add(this.chkStuffInputFree);
            this.tabPage1.Controls.Add(this.chkSoundWeight);
            this.tabPage1.Controls.Add(this.chkAutoPrint);
            this.tabPage1.Controls.Add(this.txtCompanyName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(429, 253);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtIdLen);
            this.groupBox5.Controls.Add(this.txtPrefix);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(131, 131);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(256, 82);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "磅单格式";
            // 
            // txtIdLen
            // 
            this.txtIdLen.Location = new System.Drawing.Point(80, 48);
            this.txtIdLen.Name = "txtIdLen";
            this.txtIdLen.Size = new System.Drawing.Size(62, 21);
            this.txtIdLen.TabIndex = 13;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(80, 19);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(161, 21);
            this.txtPrefix.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "自增长度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "前缀格式：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdKZValue);
            this.groupBox4.Controls.Add(this.rdKZRate);
            this.groupBox4.Location = new System.Drawing.Point(19, 131);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(98, 82);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "扣杂方式";
            // 
            // rdKZValue
            // 
            this.rdKZValue.AutoSize = true;
            this.rdKZValue.Location = new System.Drawing.Point(20, 53);
            this.rdKZValue.Name = "rdKZValue";
            this.rdKZValue.Size = new System.Drawing.Size(59, 16);
            this.rdKZValue.TabIndex = 1;
            this.rdKZValue.TabStop = true;
            this.rdKZValue.Text = "固定值";
            this.rdKZValue.UseVisualStyleBackColor = true;
            // 
            // rdKZRate
            // 
            this.rdKZRate.AutoSize = true;
            this.rdKZRate.Location = new System.Drawing.Point(20, 24);
            this.rdKZRate.Name = "rdKZRate";
            this.rdKZRate.Size = new System.Drawing.Size(59, 16);
            this.rdKZRate.TabIndex = 0;
            this.rdKZRate.TabStop = true;
            this.rdKZRate.Text = "百分比";
            this.rdKZRate.UseVisualStyleBackColor = true;
            // 
            // chkCarWeight
            // 
            this.chkCarWeight.AutoSize = true;
            this.chkCarWeight.Location = new System.Drawing.Point(252, 92);
            this.chkCarWeight.Name = "chkCarWeight";
            this.chkCarWeight.Size = new System.Drawing.Size(120, 16);
            this.chkCarWeight.TabIndex = 5;
            this.chkCarWeight.Text = "使用车辆预知皮重";
            this.chkCarWeight.UseVisualStyleBackColor = true;
            // 
            // chkStuffInputFree
            // 
            this.chkStuffInputFree.AutoSize = true;
            this.chkStuffInputFree.Location = new System.Drawing.Point(78, 92);
            this.chkStuffInputFree.Name = "chkStuffInputFree";
            this.chkStuffInputFree.Size = new System.Drawing.Size(120, 16);
            this.chkStuffInputFree.TabIndex = 4;
            this.chkStuffInputFree.Text = "控制材料自由输入";
            this.chkStuffInputFree.UseVisualStyleBackColor = true;
            // 
            // chkSoundWeight
            // 
            this.chkSoundWeight.AutoSize = true;
            this.chkSoundWeight.Location = new System.Drawing.Point(252, 52);
            this.chkSoundWeight.Name = "chkSoundWeight";
            this.chkSoundWeight.Size = new System.Drawing.Size(96, 16);
            this.chkSoundWeight.TabIndex = 3;
            this.chkSoundWeight.Text = "语音读出称重";
            this.chkSoundWeight.UseVisualStyleBackColor = true;
            // 
            // chkAutoPrint
            // 
            this.chkAutoPrint.AutoSize = true;
            this.chkAutoPrint.Location = new System.Drawing.Point(78, 52);
            this.chkAutoPrint.Name = "chkAutoPrint";
            this.chkAutoPrint.Size = new System.Drawing.Size(108, 16);
            this.chkAutoPrint.TabIndex = 2;
            this.chkAutoPrint.Text = "过磅后自动打印\r\n";
            this.chkAutoPrint.UseVisualStyleBackColor = true;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(78, 21);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(309, 21);
            this.txtCompanyName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "公司名称：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ConnectInfo);
            this.tabPage2.Controls.Add(this.btnTestSP);
            this.tabPage2.Controls.Add(this.txtEndChar);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtStartChar);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtDataLength);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.cbBaudRate);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbCOMList);
            this.tabPage2.Controls.Add(this.COMLabel);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(429, 253);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "磅秤连接";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ConnectInfo
            // 
            this.ConnectInfo.AutoSize = true;
            this.ConnectInfo.Location = new System.Drawing.Point(156, 215);
            this.ConnectInfo.Name = "ConnectInfo";
            this.ConnectInfo.Size = new System.Drawing.Size(0, 12);
            this.ConnectInfo.TabIndex = 12;
            // 
            // btnTestSP
            // 
            this.btnTestSP.Location = new System.Drawing.Point(158, 189);
            this.btnTestSP.Name = "btnTestSP";
            this.btnTestSP.Size = new System.Drawing.Size(86, 23);
            this.btnTestSP.TabIndex = 11;
            this.btnTestSP.Text = "测试连接";
            this.btnTestSP.UseVisualStyleBackColor = true;
            this.btnTestSP.Click += new System.EventHandler(this.btnTestSP_Click);
            // 
            // txtEndChar
            // 
            this.txtEndChar.Location = new System.Drawing.Point(274, 144);
            this.txtEndChar.Name = "txtEndChar";
            this.txtEndChar.Size = new System.Drawing.Size(100, 21);
            this.txtEndChar.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "终止位：";
            // 
            // txtStartChar
            // 
            this.txtStartChar.Location = new System.Drawing.Point(88, 144);
            this.txtStartChar.Name = "txtStartChar";
            this.txtStartChar.Size = new System.Drawing.Size(100, 21);
            this.txtStartChar.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "起始位：";
            // 
            // txtDataLength
            // 
            this.txtDataLength.Location = new System.Drawing.Point(88, 117);
            this.txtDataLength.Name = "txtDataLength";
            this.txtDataLength.Size = new System.Drawing.Size(100, 21);
            this.txtDataLength.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "数据长度：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdReversion);
            this.groupBox1.Controls.Add(this.rdPositive);
            this.groupBox1.Location = new System.Drawing.Point(207, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 75);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据顺序";
            // 
            // rdReversion
            // 
            this.rdReversion.AutoSize = true;
            this.rdReversion.Location = new System.Drawing.Point(120, 36);
            this.rdReversion.Name = "rdReversion";
            this.rdReversion.Size = new System.Drawing.Size(47, 16);
            this.rdReversion.TabIndex = 1;
            this.rdReversion.TabStop = true;
            this.rdReversion.Text = "反序";
            this.rdReversion.UseVisualStyleBackColor = true;
            // 
            // rdPositive
            // 
            this.rdPositive.AutoSize = true;
            this.rdPositive.Location = new System.Drawing.Point(24, 36);
            this.rdPositive.Name = "rdPositive";
            this.rdPositive.Size = new System.Drawing.Size(47, 16);
            this.rdPositive.TabIndex = 0;
            this.rdPositive.TabStop = true;
            this.rdPositive.Text = "正序";
            this.rdPositive.UseVisualStyleBackColor = true;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "900",
            "1200",
            "1800",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            ""});
            this.cbBaudRate.Location = new System.Drawing.Point(88, 68);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(78, 20);
            this.cbBaudRate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率：";
            // 
            // cbCOMList
            // 
            this.cbCOMList.FormattingEnabled = true;
            this.cbCOMList.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.cbCOMList.Location = new System.Drawing.Point(88, 33);
            this.cbCOMList.Name = "cbCOMList";
            this.cbCOMList.Size = new System.Drawing.Size(78, 20);
            this.cbCOMList.TabIndex = 1;
            // 
            // COMLabel
            // 
            this.COMLabel.AutoSize = true;
            this.COMLabel.Location = new System.Drawing.Point(30, 36);
            this.COMLabel.Name = "COMLabel";
            this.COMLabel.Size = new System.Drawing.Size(53, 12);
            this.COMLabel.TabIndex = 0;
            this.COMLabel.Text = "串口号：";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(429, 253);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "数据传送";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Controls.Add(this.btnTestDBConnect);
            this.groupBox3.Controls.Add(this.txtPwd);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtUid);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtDB);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtServer);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(14, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(393, 170);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库服务器";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(249, 128);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "手动立即传送";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(22, 133);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(101, 12);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "服务器连接未成功";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnTestDBConnect
            // 
            this.btnTestDBConnect.Location = new System.Drawing.Point(146, 87);
            this.btnTestDBConnect.Name = "btnTestDBConnect";
            this.btnTestDBConnect.Size = new System.Drawing.Size(86, 23);
            this.btnTestDBConnect.TabIndex = 15;
            this.btnTestDBConnect.Text = "测试连接";
            this.btnTestDBConnect.UseVisualStyleBackColor = true;
            this.btnTestDBConnect.Click += new System.EventHandler(this.btnTestDBConnect_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(256, 52);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(100, 21);
            this.txtPwd.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "密码：";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(79, 52);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(100, 21);
            this.txtUid.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "用户名：";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(256, 22);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(100, 21);
            this.txtDB.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(197, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "数据库：";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(79, 22);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 21);
            this.txtServer.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "服务器：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdSyn2);
            this.groupBox2.Controls.Add(this.rdSyn1);
            this.groupBox2.Controls.Add(this.rdSyn0);
            this.groupBox2.Location = new System.Drawing.Point(14, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 53);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "同步传送方式";
            // 
            // rdSyn2
            // 
            this.rdSyn2.AutoSize = true;
            this.rdSyn2.Location = new System.Drawing.Point(249, 22);
            this.rdSyn2.Name = "rdSyn2";
            this.rdSyn2.Size = new System.Drawing.Size(107, 16);
            this.rdSyn2.TabIndex = 3;
            this.rdSyn2.TabStop = true;
            this.rdSyn2.Text = "退出时程序传送";
            this.rdSyn2.UseVisualStyleBackColor = true;
            // 
            // rdSyn1
            // 
            this.rdSyn1.AutoSize = true;
            this.rdSyn1.Location = new System.Drawing.Point(131, 22);
            this.rdSyn1.Name = "rdSyn1";
            this.rdSyn1.Size = new System.Drawing.Size(71, 16);
            this.rdSyn1.TabIndex = 2;
            this.rdSyn1.TabStop = true;
            this.rdSyn1.Text = "同步传送";
            this.rdSyn1.UseVisualStyleBackColor = true;
            // 
            // rdSyn0
            // 
            this.rdSyn0.AutoSize = true;
            this.rdSyn0.Location = new System.Drawing.Point(24, 22);
            this.rdSyn0.Name = "rdSyn0";
            this.rdSyn0.Size = new System.Drawing.Size(59, 16);
            this.rdSyn0.TabIndex = 1;
            this.rdSyn0.TabStop = true;
            this.rdSyn0.Text = "不传送";
            this.rdSyn0.UseVisualStyleBackColor = true;
            // 
            // SettingCancel
            // 
            this.SettingCancel.Location = new System.Drawing.Point(339, 292);
            this.SettingCancel.Name = "SettingCancel";
            this.SettingCancel.Size = new System.Drawing.Size(75, 27);
            this.SettingCancel.TabIndex = 6;
            this.SettingCancel.Text = "取消";
            this.SettingCancel.UseVisualStyleBackColor = true;
            this.SettingCancel.Click += new System.EventHandler(this.SettingCancel_Click);
            // 
            // SettingOK
            // 
            this.SettingOK.Image = global::WeightingSystem.Properties.Resources._529410;
            this.SettingOK.Location = new System.Drawing.Point(243, 292);
            this.SettingOK.Name = "SettingOK";
            this.SettingOK.Size = new System.Drawing.Size(75, 27);
            this.SettingOK.TabIndex = 5;
            this.SettingOK.Text = "保存";
            this.SettingOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SettingOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SettingOK.UseVisualStyleBackColor = true;
            this.SettingOK.Click += new System.EventHandler(this.SettingOK_Click);
            // 
            // SystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 328);
            this.Controls.Add(this.SettingCancel);
            this.Controls.Add(this.SettingOK);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.SystemSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
this.Icon = global::WeightingSystem.Properties.Resources.LOGO;

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkStuffInputFree;
        private System.Windows.Forms.CheckBox chkSoundWeight;
        private System.Windows.Forms.CheckBox chkAutoPrint;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cbCOMList;
        private System.Windows.Forms.Label COMLabel;
        private System.Windows.Forms.TextBox txtEndChar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStartChar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDataLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdReversion;
        private System.Windows.Forms.RadioButton rdPositive;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ConnectInfo;
        private System.Windows.Forms.Button btnTestSP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdSyn2;
        private System.Windows.Forms.RadioButton rdSyn1;
        private System.Windows.Forms.RadioButton rdSyn0;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnTestDBConnect;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button SettingOK;
        private System.Windows.Forms.Button SettingCancel;
        private System.IO.Ports.SerialPort SP;
        private System.Windows.Forms.CheckBox chkCarWeight;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdKZValue;
        private System.Windows.Forms.RadioButton rdKZRate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtIdLen;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}