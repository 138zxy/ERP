using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using WeightingSystem.Properties;
using System.Runtime.InteropServices;
using WeightingSystem.Helpers;
using System.Threading;
using System.Drawing.Imaging;
using WeightingSystem.Models;
using FastReport;
using System.IO;
using log4net;
using FastReport.Design.StandardDesigner;

namespace WeightingSystem
{
    public partial class MainForm : Form  
    {
        bool m_bInitSDK = false;
        uint iLastErr = 0;
        string str;

        public Int32 m_lUserID1 = -1;
        Int32 m_lRealHandle1 = -1;
        public string DVRIPAddress1 = Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.IP1, ""); //设备IP地址或者域名
        public Int16 DVRPortNumber1 = Convert.ToInt16(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.Port1, ""));//设备服务端口号
        public string DVRUserName1 = Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.UserName1, "");//设备登录用户名
        public string DVRPassword1 = Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.Password1, "");//设备登录密码
        public Int16 lChannel1 = Int16.Parse(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.Channel1, "1"));//预te览的设备通道

        public Int32 m_lUserID2 = -1;
        Int32 m_lRealHandle2 = -1;
        public string DVRIPAddress2 = Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.IP2, ""); //设备IP地址或者域名
        public Int16 DVRPortNumber2 = Convert.ToInt16(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.Port2, ""));//设备服务端口号
        public string DVRUserName2 = Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.UserName2, "");//设备登录用户名
        public string DVRPassword2 = Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.Password2, "");//设备登录密码
        public Int16 lChannel2 = Int16.Parse(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.Channel2, "1"));//预te览的设备通道

        /// <summary>
        /// 许可信息
        /// </summary>
        public static ZLERP.Licensing.Client.License LicenseInfo = null;
        /// <summary>
        /// 当前路径
        /// </summary>
        public static string APPLICATION_PATH 
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath); 
            }
        }

        public static string PICSAVE_PATH
        {
            get
            {
                string p = Path.Combine(APPLICATION_PATH, "Pic");
                if (!p.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    p = p + Path.DirectorySeparatorChar.ToString();
                return p;
            }

        }
        public static string CurrentUserID;
        public static string CurrentUserName;
        #region construct
      
        delegate void SetTextCallback(byte[] text);
        delegate void SetSpeech(string txt);
                 
        public string COM = string.Empty;
        public string BaudRate = string.Empty;
        public bool Positive = true;
        public string DataLength = "0";
        public string StartChar = string.Empty;
        public string EndChar = string.Empty;
      
        public string Pic3_Path = string.Empty;//3号图片文件名
        public string Pic4_Path = string.Empty;//4号图片文件名
        public int SynMethod = 1;
        #endregion
        private IList<Silo> SiloList = new List<Silo>();
        /// <summary>
        /// 报表路径
        /// </summary>
        static public string REPORT_PATH = Path.Combine(APPLICATION_PATH, "report\\Report.frx");   //报表路径
        /// <summary>
        /// 混凝土过磅单模板
        /// </summary>
        public static string TZCONCRETEREPORT_PATH = Path.Combine(APPLICATION_PATH, "report\\TzConcreteReport.frx");
        /// <summary>
        /// 发货单模板
        /// </summary>
        public static string SHIPPINGDOCUMENT_REPORT_PATH = Path.Combine(APPLICATION_PATH, "report\\ShipDocConcreteReport.frx");
        static ILog log = LogManager.GetLogger(typeof(MainForm));

        public MainForm(string UserId,String UserName)
        {
            //FormLogin login = new FormLogin(this);
            //login.ShowDialog(this);
            LocalDataProvider ldp = new LocalDataProvider();
            ERPDataProvider edp = new ERPDataProvider();
            PublicHelper ph = new PublicHelper();
            Config c = new Config();
             

            COM = c.COM;
            BaudRate = c.BaudRate;
            Positive = c.Positive;
            DataLength = c.DataLength;
            StartChar = c.StartChar;
            EndChar = c.EndChar;
           
            CurrentUserID = UserId;
            CurrentUserName = UserName;

            InitializeComponent(); 
          
            BindCombox();
            //SetFont();
            
            RefreshStuffInList();

            //筒仓数据
            SiloList = edp.GetSiloList();

            //自定义语音菜单
            BindCustomSpeechList();

            tlblOperator.Text = string.Format("操作员：{0}", CurrentUserName);

            //过磅是否自动打印磅单
            checkBox1.Checked = c.AutoPrint;
            if (c.adminMode == 0)                       //非管理员模式无法看到磅单设计
            {
                toolStripButton3.Visible = false;
            }
            else {
                toolStripButton3.Visible = true;
            }

            if (c.RateMode == 0)
            {
                rdbPercent.Visible = false;
                txtKZPercent.Visible = false;
                label16.Visible = false;
                rdbValue.Checked = true;
            }
            else
            {
                rdbValue.Visible = false;
                txtKzValue.Visible = false;
                label22.Visible = false;
                rdbPercent.Checked = true;
               
            }
           //  DoSimulate();

            FastReport.EnvironmentSettings es = new EnvironmentSettings();
            es.UIStyle = FastReport.Utils.UIStyle.VisualStudio2005;
           // es.UseOffice2007Form = true;
            es.DesignerSettings.Icon = Resources.LOGO;
            es.PreviewSettings.Icon = Resources.LOGO;
            es.DesignerSettings.Text = "中联重科 - ";
            es.DesignerSettings.DesignerLoaded += new EventHandler(DesignerSettings_DesignerLoaded);

            //this.Text = this.Text + string.Format("-{0}({1})", MainForm.LicenseInfo.LicenceTo, MainForm.LicenseInfo.Edition);
        }

        void DesignerSettings_DesignerLoaded(object sender, EventArgs e)
        {
            (sender as DesignerControl).MainMenu.miHelp.Visible = false;
        }

        

        private void BindCombox()
        {
            ERPDataProvider provider = new ERPDataProvider();

            //car
            PublicHelper ph = new PublicHelper();
            this.cbCar.DataSource = ph.GetCarList();
            
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("是否退出系统？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                e.Cancel = true; 
                
            }
            
        }
        
       

        #region 模拟重量数字
        /// <summary>
        /// 模拟定时器
        /// </summary>
        System.Windows.Forms.Timer timerSimulate;
        void StopSimulate()
        {
            timerSimulate.Stop();
            SetWeight(0);
            //TODO: 开启串口监听
        }
        /// <summary>
        /// 模拟
        /// </summary>
        void DoSimulate()
        {
            timerSimulate = new System.Windows.Forms.Timer();
            timerSimulate.Interval = 5000;
            timerSimulate.Tick += new EventHandler(timerSimulate_Tick);
            timerSimulate.Start();
            //TODO: 停止串口监听
        }
        
        void timerSimulate_Tick(object sender, EventArgs e)
        {
            SetWeight(new Random().Next(20000)); 
        }
        #endregion


        /// <summary>
        /// 设置当前重量显示
        /// </summary>
        /// <param name="weight"></param>
        void SetWeight(int weight)
        {
            if (btnStopWeight.Enabled)
            {
                lblWeight.Text = weight.ToString();
            }
         

            foreach (Form form in Application.OpenForms)             //判断毛重界面是否被打开，若打开，同时给毛重称量赋值
            {
                if (form.Name == "WeightManage")
                {
                    WeightManage wm = (WeightManage)form;
                    if (wm.btnStop.Enabled)                         //在称量状态
                    {
                        wm.lblTrueWeight.Text = weight.ToString();
                         
                    }
                    break;
                }
                else if (form.Name == "ConcreteWeight")
                {
                    ConcreteWeight cm = (ConcreteWeight)form;
                    cm.lblWeight.Text = weight.ToString();
                    break;
                    
                }

                else if (form.Name == "EmptyCarWeighting")
                {
                    EmptyCarWeighting cm = (EmptyCarWeighting)form;
                    cm.lblWeight.Text = weight.ToString();
                    break;

                }
            }
        }


        private void btnSpeech_Click(object sender, EventArgs e)
        {
            menuSpeech.Show(btnSpeech, 0, btnSpeech.Height);
        }
       
        

        public void RefreshStuffInList() {
            dgvList.AutoGenerateColumns = false;
            ERPDataProvider edp = new ERPDataProvider();
            dgvList.DataSource = edp.GetStuffInList();
            dgvList.ClearSelection();
        }

        private void RefreshCarWeightList(string CarNo, int displayNum)
        {
            dataGridView1.AutoGenerateColumns = false;
            ERPDataProvider edp = new ERPDataProvider();
            IList<StuffIn> carWeightList = edp.GetStuffInListDetail(CarNo);
            if (displayNum > 0)
            {
                int count = carWeightList.Count;
                for (int i = count - 1; i >= displayNum; i--)
                {
                    carWeightList.RemoveAt(i);
                }
            }
            dataGridView1.DataSource = carWeightList;
            dataGridView1.ClearSelection();
        }

        void ShowError(string message)
        {
            MessageBox.Show(this, message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void ShowMessage(string message) {
            MessageBox.Show(this, message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void dgvList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {


            if ((e.Row.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                btnGetCarWeight.Enabled = true;

                //绑定行数据到表单
                ERPDataProvider edp = new ERPDataProvider();
               
                StuffIn si = (StuffIn)e.Row.DataBoundItem;
                if (si != null)
                {
                   
                    IList<Silo> _siloList = edp.GetSiloList(si.StuffID);

                    Silo EmptySilo = new Silo();
                    EmptySilo.SiloID = string.Empty;
                    EmptySilo.SiloName = string.Empty;

                    _siloList.Insert(0, EmptySilo);

                    cbSilo.DataSource = _siloList;
                    if (si.FinalStuffType == "FA" || si.FinalStuffType == "CA")
                    {
                        cbSilo.SelectedValue = si.SiloID;
                    }
                    else
                    {
                        cbSilo.SelectedValue = EmptySilo.SiloID;
                    }

                    //清空上次显示数据
                    txtLastKzValue.Text = "0";
                    txtKzValue.Text = "0";
                    txtKZPercent.Text = "0";
                    txtRemark.Text = string.Empty;

                    txtCarWeight.Text = "0";
                    txtTotalWeight.Text = "0";
                    txtWeight.Text = "0";

                    cbCar.SelectedText = string.Empty;
                    cbCar.Text = string.Empty;


                    cbCar.Text = si.CarNo;
                    txtTotalWeight.Text = Convert.ToInt32(si.TotalNum).ToString();
                    txtCarWeight.Text = Convert.ToInt32(si.CarWeight).ToString();

                    LoadWeightToView();

                    //读取照片信息
                    if (!string.IsNullOrEmpty(si.Pic1) && rdbPic1.Checked)
                    {
                        PicBox.ImageLocation =  PICSAVE_PATH + si.Pic1;

                    }
                    else if (!string.IsNullOrEmpty(si.Pic2) && rdbPic2.Checked)
                    {
                        PicBox.ImageLocation = PICSAVE_PATH + si.Pic2;
                    }

                    RefreshCarWeightList(si.CarNo, 10);
                }
            }
            else if (dgvList.SelectedRows.Count <=0)
            {
                //清空上次显示数据
                txtLastKzValue.Text = "0";
                txtKzValue.Text = "0";
                txtKZPercent.Text = "0";
                txtRemark.Text = string.Empty;

                txtCarWeight.Text = "0";
                txtTotalWeight.Text = "0";
                txtWeight.Text = "0";

                cbCar.SelectedText = string.Empty;
                cbCar.Text = string.Empty;

                IList<Silo> li = new List<Silo>();
                
                cbSilo.DataSource = li;

                dataGridView1.DataSource = null;
            }
            
            
           
        }

        #region 语音
        void BindCustomSpeechList()
        {
            List<KeyValuePair<string,string>> list = Config.Ini.GetSectionValuesAsList(Config.Section.Speech);
            if (list != null)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    ToolStripMenuItem tsmi;
                    tsmi = new ToolStripMenuItem(list[i].Value, null, SpeechMenuItem_Click);
                    tsmi.Tag = "custom";
                    menuSpeech.Items.Insert(0, tsmi);
                }
            }
        }
        /// <summary>
        /// 自定义语音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemCustomerSpeech_Click(object sender, EventArgs e)
        {
            FormSpeech form = new FormSpeech();
            form.ShowDialog();

            //清空原有自定义菜单
            IList<ToolStripMenuItem> readyToRemove = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem ti in menuSpeech.Items) {
                if (ti.Tag!=null && ti.Tag.ToString() == "custom")
                {
                    readyToRemove.Add(ti);
                }
            }
            foreach (ToolStripMenuItem ti in readyToRemove) {
                menuSpeech.Items.Remove(ti);
            }

            BindCustomSpeechList();
        }

        /// <summary>
        /// 语音播报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeechMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:语音 
            SpeakText(((ToolStripMenuItem)sender).Text);
        }

        public void SpeakText(string txt)
        {
             
            SpeechHelper sh = new SpeechHelper();
            Thread t = new Thread(sh.Speak);
            ThreadSpeak ts = new ThreadSpeak();
            ts.t = t;
            ts.SpeakTxt = txt;
            t.IsBackground = true;
            t.Start(ts);

        }

      

        #endregion

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private ImageCodecInfo GetEncoder(ImageFormat format)//获取特定的图像编解码信息
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }



        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录", "信息提示");
                return;
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("是否删除此条记录？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    string ID = dgvList.SelectedRows[0].Cells["StuffInID"].Value.ToString();
                    ERPDataProvider ldp = new ERPDataProvider();
                    ldp.delWeight(ID);
                    log.Info("删除毛重单号:" + ID );
                    RefreshStuffInList();
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SystemSetting SystemSettingForm = new SystemSetting(this);
            SystemSettingForm.ShowDialog();
        }
 
      
 
        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            QueryDataForm dataForm = new QueryDataForm();
            dataForm.ShowDialog();
        }
 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CarManage car = new CarManage();
            car.ShowDialog();
        }


        bool isFoundBeginCode = false;
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
            byte BeginCode = Byte.Parse(StartChar , System.Globalization.NumberStyles.HexNumber);
            byte EndCode = Byte.Parse( EndChar , System.Globalization.NumberStyles.HexNumber);
            byte SignCode = Byte.Parse("FF", System.Globalization.NumberStyles.HexNumber);
            
            int Count = serialPort.BytesToRead;
            

            int iDataLength = Convert.ToInt32(DataLength);
            //至少保证可读数据 >= 数据长度加起始符长度
            if (Count < iDataLength + 1) return;

            try
            {
                if (!isFoundBeginCode)
                {
                    byte firstbyte = Convert.ToByte(serialPort.ReadByte());

                    while (firstbyte != BeginCode)
                    {
                        if (serialPort.BytesToRead < iDataLength + 1)
                        {
                            isFoundBeginCode = false;
                            return;
                        }
                        firstbyte = Convert.ToByte(serialPort.ReadByte());
                    }
                    isFoundBeginCode = true;
                }

                //剩余数据小于数据长度，退出
                if (serialPort.BytesToRead < iDataLength) return;

                //MessageBox.Show(firstbyte.ToString() + "_" + BeginCode.ToString());
                //if (firstbyte == BeginCode)
                //{
                byte[] dataBuffer = new byte[iDataLength];


                serialPort.Read(dataBuffer, 0, iDataLength);


                    //int bytesread = serialPort.ReadBufferSize;
                    //byte[] bytesdata = new byte[serialPort.ReadBufferSize];
                    //byte bytedata;
                    //for (int i = 0; i <= bytesread - 1; i++)
                    //{
                    //    if (serialPort.IsOpen)
                    //    {
                    //        bytedata = Convert.ToByte(serialPort.ReadByte());
                    //        if (i >= Convert.ToInt32(DataLength))
                    //        {
                    //            break;
                    //        }
                    //        bytesdata[i] = bytedata;
                    //    }

                    //}
                SetText(dataBuffer);
                isFoundBeginCode = false;
               // }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 视频函数
        /// <summary>
        /// 预览视频
        /// </summary>
        /// <param name="iRouteID"></param>
        public void StartView(int iRouteID) {
            Video.DSStream_Initialize();
            Video.CSRect rc = new Video.CSRect();
            rc.left = 0;
            rc.top = 5;

            Video.DSStream_ConnectDevice(0, false, this.Video1.Handle);
            rc.right = this.Video1.Size.Width;
            rc.bottom = this.Video1.Size.Height;
             
            Video.DSStream_RouteInPinToOutPin(0, iRouteID, 0);
            Video.DSStream_SetWindowPos(0, rc);
            Video.DSStream_SetVideoStandard(0, Video.VideoStandard.VideoStandard_PAL_B);
        }

        

        public void savePic(string _Pic3,string _Pic4)
        {
            Pic3_Path = _Pic3;
            Pic4_Path = _Pic4;
            int videoNo = 2;
            PublicHelper ph = new PublicHelper();
            string FileName = _Pic4;
            if (rdbVideo1.Checked)
            {
                videoNo = 1;
                FileName = _Pic3;
            }

            string PicFilePath = PICSAVE_PATH;// Environment.CurrentDirectory + "\\" + "Pic\\";
            if (!Directory.Exists(PicFilePath))
            {
                Directory.CreateDirectory(PicFilePath);
            }


            Video.DSStream_SaveToJpgFile(0, PicFilePath + FileName, Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.VideoQuality, 50));

             
            Video.DSStream_DisconnectDevice(0);
           
           
            Thread t = new Thread(DelayVideo);
            t.Start(t);
            if (videoNo == 1)
            {
                StartView(2);                
            }
            else
            {
                StartView(1); 
            }
            ph.SetVidQuality(Video1.Width,Video1.Height);


            //Thread.Sleep(Config.Ini.GetInt16(Config.Section.BaseSetting, "VideoDelay", 0) + 500);
            //Video.DSStream_DisconnectDevice(0);
            //if (videoNo == 1)
            //{
            //    StartView(1);
            //}
            //else
            //{
            //    StartView(2);
            //}
           
            //ph.SetVidQuality(Video1.Width, Video1.Height);
           
        }

        private void DelayVideo(object thread)
        {
            Thread t = (Thread)thread;
            int videoNo = 1;
            string FileName = Pic3_Path;
            if (rdbVideo1.Checked)
            {
                videoNo = 2;
                FileName = Pic4_Path;
            }
            Thread.Sleep(Config.Ini.GetInt16(Config.Section.BaseSetting, "VideoDelay", 0));

            string PicFilePath = PICSAVE_PATH;// Environment.CurrentDirectory + "\\" + "Pic\\";
            if (!Directory.Exists(PicFilePath))
            {
                Directory.CreateDirectory(PicFilePath);
            }
            

            Video.DSStream_SaveToJpgFile(0, PicFilePath + FileName, Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.VideoQuality, 50));
            t.Abort();
        }


        #endregion
        

        /// <summary>
        /// 显示屏显示过磅数据
        /// </summary>
        /// <param name="text"></param>
        private void SetText(byte[] text)
        {
            //MessageBox.Show("cc");
           // MessageBox.Show("this.lblWeight.InvokeRequired"+this.lblWeight.InvokeRequired.ToString());
            if (this.lblWeight.InvokeRequired)
            {
                SetTextCallback d1 = new SetTextCallback(SetText);
                this.Invoke(d1, new object[] { text });
                
            }
            else
            {
                 
                if (btnStopWeight.Enabled)
                {
                    this.lblWeight.Text = AnalyData(text);
                }
            }

            foreach(Form form in Application.OpenForms)             //判断毛重界面是否被打开，若打开，同时给毛重称量赋值
            {
                if (form.Name == "WeightManage")
                {

                    WeightManage wm = (WeightManage)form;
                    if (wm.btnStop.Enabled)                         //在称量状态
                    {
                        if (wm.lblTrueWeight.InvokeRequired)
                        {
                            SetTextCallback d1 = new SetTextCallback(SetText);
                            this.Invoke(d1, new object[] { text });
                        }
                        else
                        {
                            wm.lblTrueWeight.Text = AnalyData(text);
                        }
                    }
                    break;
                }
                else if (form.Name == "ConcreteWeight")             //判断混凝土称重界面是否被打开，若打开，同时给毛重称量赋值
                {
                    ConcreteWeight cw = (ConcreteWeight)form;
                    if (cw.lblWeight.InvokeRequired)
                    {
                        SetTextCallback d1 = new SetTextCallback(SetText);
                        this.Invoke(d1, new object[] { text });
                    }
                    else
                    {
                        cw.lblWeight.Text = AnalyData(text);
                    }
                }
                else if (form.Name == "EmptyCarWeighting")              //空车过磅界面判断是否被打开，若打开，显示重量
                {
                    EmptyCarWeighting ecw = (EmptyCarWeighting)form;
                    if (ecw.lblWeight.InvokeRequired)
                    {
                        SetTextCallback d1 = new SetTextCallback(SetText);
                        this.Invoke(d1, new object[] { text });
                    }
                    else
                    {
                        ecw.lblWeight.Text = AnalyData(text);
                    }
                }
            }
        }

        /// <summary>
        /// 磅秤串口数据分析
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public  string AnalyData(byte[] bytes)
        {
            string InputData = "";
            string writeNum = "";
      
            
            byte SignCode = Byte.Parse("FF", System.Globalization.NumberStyles.HexNumber);
           
            if (bytes[0] == SignCode)
            {
                InputData = Encoding.Default.GetString(bytes).Substring(1, int.Parse(DataLength));
            }
            else
            {
                InputData = Encoding.Default.GetString(bytes).Substring(0, int.Parse(DataLength));
            }


            if (Positive)
            {
                writeNum = InputData.Replace(".", "").Replace(" ", "");
            }
            else
            {
                for (int i = int.Parse(DataLength) - 1; i >= 0; i--)
                {
                    if (InputData[i] != '.' && InputData[i] != ' ')
                        writeNum += InputData[i];
                }
            }
            if (writeNum.Length > 0)
            {
                if (writeNum.Substring(0,1) == "+")
                {
                    writeNum = writeNum.Remove(0, 1);
                }

            }
            int weight = 0;
            int.TryParse(writeNum,out weight);
            return weight.ToString() ;
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //UserManage um = new UserManage();
            //um.ShowDialog();
        }

        /// <summary>
        /// 判断在列表中是否存在改车辆
        /// </summary>
        /// <returns></returns>
        private StuffIn findCarNoInList(string CarNo)
        {
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                StuffIn si = (StuffIn)row.DataBoundItem;
                if (si.CarNo.Trim() == CarNo)
                {
                    return si;
                }
            }

            return null ;
        }
 

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            Application.Exit();
            //if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            //{
            //    Video.DSStream_DisconnectDevice(0);
            //}
             
        }

        private void btnDataSearch_Click(object sender, EventArgs e)
        {
            QueryDataForm qdf = new QueryDataForm();
            qdf.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (Report report = new Report())
            {
                
                report.Load(REPORT_PATH); 
                report.Design();
                 
            }
        }

        
        private void MainForm_Load(object sender, EventArgs e)
        {
            PublicHelper ph = new PublicHelper();
            //默认开启1#摄像头
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            {
                this.StartView(1);
                ph.SetVidQuality(Video1.Width, Video1.Height);
            }
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            {
                this.InitHKVideo();
            }

            if (serialPort.IsOpen)
            {
                Application.DoEvents();

            }
            else
            {
                serialPort.PortName = COM;
                serialPort.BaudRate = Convert.ToInt16(BaudRate);
                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("串口打开失败：" + ex.Message.ToString());
                   
                }
            }
        }

        public void InitHKVideo()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("视频初始化失败!");
                return;
            }

            HKLoginVideo(DVRIPAddress1, DVRPortNumber1, DVRUserName1, DVRPassword1, ref m_lUserID1,"1");
            HKOpenVideo(PicBox.Handle, lChannel1,m_lUserID1,ref m_lRealHandle1,"1");
            HKLoginVideo(DVRIPAddress2, DVRPortNumber2, DVRUserName2, DVRPassword2, ref m_lUserID2,"2");
            HKOpenVideo(Video1.Handle, lChannel2, m_lUserID2,ref m_lRealHandle2,"2");
            //LoginHKVideo(DVRIPAddress1, DVRPortNumber1, DVRUserName1, DVRPassword1, ref m_lUserID3);
            //LoginHKVideo(DVRIPAddress2, DVRPortNumber2, DVRUserName2, DVRPassword2, ref m_lUserID4);

        }

        public void HKLoginVideo(string DVRIPAddress, Int16 DVRPortNumber, string DVRUserName, string DVRPassword,ref int lUserID,string flag)
        {
            CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

            //登录设备 Login the device
            lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
            if (lUserID < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "登录摄像头"+flag+"失败, error code= " + iLastErr; //登录失败，输出错误号
                log.Error(str);
                //MessageBox.Show(str);
                return;
            }
        }

        public void HKOpenVideo(IntPtr handle, Int16 channel, int lUserID,ref int lRealHandle, string flag)
        {
            //预览设备
            CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
            lpPreviewInfo.hPlayWnd = handle;//预览窗口
            lpPreviewInfo.lChannel = channel;//预te览的设备通道
            lpPreviewInfo.dwStreamType = uint.Parse(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.CaptureMode, "1"));//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流

            CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
            IntPtr pUser = new IntPtr();//用户数据

            //打开预览 Start live view 
            lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
            if (lRealHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "预览视屏" + flag + "失败, error code= " + iLastErr; //预览失败，输出错误号
                log.Error(str);
                //MessageBox.Show(str);
                return;
            }
        }

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            StuffChoose sc = new StuffChoose(this,null);
            sc.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录", "信息提示");
                return;
            }
            else
            {
                StuffIn si = (StuffIn)dgvList.SelectedRows[0].DataBoundItem;
                StuffChoose sc = new StuffChoose(this,si);
                sc.ShowDialog(); 
            }
        }

        private void lblWeight_TextChanged_1(object sender, EventArgs e)
        {

            LoadWeightToView();
        }

        private void LoadWeightToView()
        {
            if (dgvList.SelectedRows.Count > 0)
            {
                StuffIn currentStuffIn = (StuffIn)dgvList.SelectedRows[0].DataBoundItem;
                if (!currentStuffIn.FastMetage)
                {
                    txtCarWeight.Text = int.Parse(lblWeight.Text).ToString();
                }

            }
            else
            {
                txtCarWeight.Text = int.Parse(lblWeight.Text).ToString();
            }
        }

        public void StopHKVideo(Int32 UserID,Int32 RealHandle,string flag)
        {
            //停止预览 Stop live view 
            if (!CHCNetSDK.NET_DVR_StopRealPlay(RealHandle))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = flag + "停止预览失败, error code= " + iLastErr;
                log.Error(str);
            }
            //注销登录 Logout the device
            if (!CHCNetSDK.NET_DVR_Logout(UserID))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = flag+"注销登录失败, error code= " + iLastErr;
                log.Error(str);
            }
        }


        private void numericUpDown1_ValueChanged_3(object sender, EventArgs e)
        {
           
            txtLastKzValue.Text = txtKzValue.Value.ToString();
        }

        private void txtKZPercent_ValueChanged(object sender, EventArgs e)
        {
           int kz = Convert.ToInt32((int.Parse(txtTotalWeight.Text) - int.Parse(txtCarWeight.Text)) * Convert.ToDecimal(txtKZPercent.Value) / 100);
           kz = Convert.ToInt16((kz + 5)/10 ) * 10;
           txtLastKzValue.Text = kz.ToString();
        }

        private void txtTotalWeight_TextChanged(object sender, EventArgs e)
        {
            txtWeight.Text = (int.Parse(txtTotalWeight.Text) - int.Parse(txtCarWeight.Text)).ToString();
        }

        private void btnGetCarWeight_Click_1(object sender, EventArgs e)
        {
             
            #region 提交前验证
            if (string.IsNullOrEmpty(cbCar.Text.Trim()))
            {
                MessageBox.Show(this, "请输入车号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCar.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbSilo.Text.Trim()))
            {
                MessageBox.Show(this, "请选择入库仓位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSilo.Focus();
                return;
            }
            if (int.Parse(txtCarWeight.Text) == 0)
            {
                MessageBox.Show(this, "皮重值太小", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            if (dgvList.SelectedRows.Count > 0)
            {
                ERPDataProvider edp = new ERPDataProvider();
                Config c = new Config();
                StuffIn currentStuffIn = (StuffIn)dgvList.SelectedRows[0].DataBoundItem;
                currentStuffIn = edp.Find(currentStuffIn.StuffInID);
                currentStuffIn.CarNo = cbCar.Text;
                currentStuffIn.SiloID = cbSilo.SelectedValue.ToString();
                currentStuffIn.SiloName = cbSilo.Text;
                decimal WRate = int.Parse(txtLastKzValue.Text);
                if (c.RateMode == 0)
                {
                    currentStuffIn.WRate = int.Parse(txtLastKzValue.Text);
                }
                else
                {
                    currentStuffIn.WRate = txtKZPercent.Value;
                }
               //currentStuffIn.WRate = int.Parse(txtLastKzValue.Text);
                currentStuffIn.CarWeight = int.Parse(txtCarWeight.Text);
                currentStuffIn.Remark = txtRemark.Text;
                currentStuffIn.Operator = CurrentUserID;
                currentStuffIn.InNum = currentStuffIn.TotalNum - currentStuffIn.CarWeight - WRate;
                MetageLastSureForm mlsf = new MetageLastSureForm(this,currentStuffIn,checkBox1.Checked,null,false);
                mlsf.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.AutoPrint, checkBox1.Checked.ToString());
        }

        private void btnStopWeight_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            {
                Pic3_Path = "3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                Pic4_Path = "4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                this.savePic(Pic3_Path, Pic4_Path);

            }
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            {
                Pic3_Path = "3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                Pic4_Path = "4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                HKVideoSavePic(m_lUserID1, lChannel1, Pic3_Path,"1");
                HKVideoSavePic(m_lUserID2, lChannel2, Pic4_Path,"2");
            }
            btnStopWeight.Enabled = false;
            btnStartWeight.Enabled = true;
        }

        public void HKVideoSavePic(int lUserID, Int16 Channel,string file,string flag)
        {
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = PICSAVE_PATH + file;


            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = Convert.ToUInt16(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.HKwPicQuality, "")); //图像质量  0-最好，1-较好，2-一般
            lpJpegPara.wPicSize = Convert.ToUInt16(Config.Ini.GetString(Config.Section.HIKNetConfig, Config.ConfigKey.HKwPicSize, "")); //抓图分辨率  0-CIF，1-QCIF，2-D1，3-UXGA(1600x1200)，4-SVGA(800x600)，5-HD720p(1280x720)，6-VGA，7-XVGA，8-HD900p ，9-HD1080 ， 10-2560*1920 ，11-1600*304 ， 12-2048*1536 ， 13-2448*2048 ，14-2448*1200 ， 15-2448*800 ， 16-XGA(1024*768) ， 17-SXGA(1280*1024) ，18-WD1(960*576/960*480),19-1080i

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(lUserID, Channel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "截图"+flag+"失败, error code= " + iLastErr;
                log.Error(str);
                //MessageBox.Show(str);
                return;
            }
            else
            {
                //str = "截图成功，文件为 " + sJpegPicFileName;
                //MessageBox.Show(str);
            }
            return;
        }

        private void btnStartWeight_Click(object sender, EventArgs e)
        {
            btnStopWeight.Enabled = true;
            btnStartWeight.Enabled = false;
        }

        private void btnCarNoSoftKeyboard_Click_1(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.cbCar);
            fskb.ShowDialog();
        }

        private void MainForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("是否退出系统？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                e.Cancel = true;
            }
            

        }

        private void rdbPercent_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPercent.Checked)
            {
                txtKzValue.Enabled = false;
                txtKZPercent.Enabled = true;
                txtKzValue.Value = 0;
            }
            else
            {
                txtKzValue.Enabled = true;
                txtKZPercent.Enabled = false;
                txtKZPercent.Value = 0;
            }
        }


        /// <summary>
        /// 动态计算净重与含水率
        /// </summary>
        private void CalcWeightAndWRate()
        {
            int KZ = 0;
            if (txtKZPercent.Value > 0)
            {
                KZ = Convert.ToInt32((int.Parse(txtTotalWeight.Text) - int.Parse(txtCarWeight.Text)) * Convert.ToDecimal(txtKZPercent.Value) / 100);
                KZ = Convert.ToInt16((KZ + 5) / 10) * 10;
            }
            else
            {
                KZ = int.Parse(txtKzValue.Text);
            }

            txtLastKzValue.Text = KZ.ToString();
            try
            {
                txtWeight.Text = (int.Parse(txtTotalWeight.Text) - int.Parse(txtCarWeight.Text) - KZ).ToString();
            }
            catch
            { 
                
            }
             
        }

        private void txtCarWeight_TextChanged(object sender, EventArgs e)
        {
            CalcWeightAndWRate();
        }

        private void txtLastKzValue_TextChanged(object sender, EventArgs e)
        {
            txtWeight.Text = (int.Parse(txtTotalWeight.Text) - int.Parse(txtCarWeight.Text) - int.Parse(txtLastKzValue.Text)).ToString();
        }

        private void cbCar_TextChanged(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count  == 1)
            {
               StuffIn si = (StuffIn)dgvList.SelectedRows[0].DataBoundItem;
               if(si.FastMetage)
               {
                    LocalDataProvider ldp = new LocalDataProvider();
                    IList<Car> CarList = ldp.GetCarList("CarNo='"+ cbCar.Text +"'");
                    if (CarList.Count > 0)
                    {
                        txtCarWeight.Text = Convert.ToInt32(CarList[0].Weight).ToString();
                    }
               }
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
                StuffIn si = (StuffIn)dgvList.Rows[e.RowIndex].DataBoundItem;
                if (si.FastMetage)
                {
                    dgvList.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    dgvList.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                }

        }

        private void rdbVideo1_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            checkedradio();
        }


        private void checkedradio()
        {
            PublicHelper ph = new PublicHelper();
            if (rdbVideo1.Checked)
            {
                Video.DSStream_DisconnectDevice(0);
                StartView(1); 
            }
            else
            {
                Video.DSStream_DisconnectDevice(0);
                StartView(2);     
            }
            ph.SetVidQuality(Video1.Width, Video1.Height);
            Video1.Refresh();
        }

        private void btnSpeech_Click_1(object sender, EventArgs e)
        {
            menuSpeech.Show(btnSpeech, 0, btnSpeech.Height);
        }

        private void rdbPic1_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count > 0)
            {
                StuffIn si = (StuffIn)dgvList.SelectedRows[0].DataBoundItem;
                //读取照片信息
                if (!string.IsNullOrEmpty(si.Pic1) && rdbPic1.Checked)
                {
                    PicBox.ImageLocation = PICSAVE_PATH + si.Pic1;

                }
                else if (!string.IsNullOrEmpty(si.Pic2) && rdbPic2.Checked)
                {
                    PicBox.ImageLocation = PICSAVE_PATH + si.Pic2;
                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            StuffSupply ss = new StuffSupply();
            ss.ShowDialog();
        }

        private void 过磅记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryDataForm qdf = new QueryDataForm();
            qdf.ShowDialog();
        }

        private void 过磅材料统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StuffReport sp = new StuffReport();
            sp.ShowDialog();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Config config = new Config();
            if (config.DarkWeightMode == 2)
            {
                StuffIn si = (StuffIn)dgvList.Rows[e.RowIndex].DataBoundItem;
                DarkRate dr = new DarkRate(si, this);
                dr.ShowDialog();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //if (cbCar.Text.Trim() == string.Empty)
            //{
            //    MessageBox.Show("请输入车号");
            //    cbCar.Focus();
            //    return;
            //}

            //if (DialogResult.OK == MessageBox.Show("是否执行【空车过磅】？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            //{
            //    LocalDataProvider ldp = new LocalDataProvider();
            //    IList<Car> carList = ldp.GetCarList("CarNo='" + cbCar.Text.Trim() + "'");
            //    if (carList.Count == 0)
            //    {
            //        Car car = new Car();
            //        car.CarNo = cbCar.Text.Trim();
            //        car.Weight = int.Parse(lblWeight.Text);
            //        car.MarkTime = DateTime.Now;
            //        ldp.addCar(car);

            //    }
            //    else if (carList[0].Weight != int.Parse(lblWeight.Text))
            //    {
            //        carList[0].Weight = int.Parse(lblWeight.Text);
            //        carList[0].MarkTime = DateTime.Now;
            //        ldp.updateCar(carList[0], carList[0].CarNo);
            //    }
            //    log.Info("车辆："+ cbCar.Text.Trim() + "空车过磅，重量："+lblWeight.Text + "Kg");
            //    MessageBox.Show("过磅完成！");
            //    this.btnStopWeight.Enabled = true;
            //    this.btnStartWeight.Enabled = false;
            //    this.RefreshStuffInList();
            //}
            EmptyCarWeighting ecw = new EmptyCarWeighting();
            ecw.ShowDialog();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region 提交前验证
            
            if (string.IsNullOrEmpty(cbCar.Text.Trim()))
            {
                MessageBox.Show(this, "请输入车号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCar.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbSilo.Text.Trim()))
            {
                MessageBox.Show(this, "请选择入库仓位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSilo.Focus();
                return;
            }
            if (int.Parse(txtCarWeight.Text) == 0)
            {
                MessageBox.Show(this, "皮重值太小", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            
            if (dgvList.SelectedRows.Count > 0)
            {
                ERPDataProvider edp = new ERPDataProvider();
                Config c = new Config();
                StuffIn currentStuffIn = (StuffIn)dgvList.SelectedRows[0].DataBoundItem;
                currentStuffIn = edp.Find(currentStuffIn.StuffInID);
                currentStuffIn.CarNo = cbCar.Text;
                currentStuffIn.SiloID = cbSilo.SelectedValue.ToString();
                currentStuffIn.SiloName = cbSilo.Text;
                //decimal WRate = 0;
                if (c.RateMode == 0)
                {
                    currentStuffIn.WRate = int.Parse(txtLastKzValue.Text);
                }
                else
                {
                    currentStuffIn.WRate = txtKZPercent.Value;
                }
                 
                currentStuffIn.CarWeight = int.Parse(txtCarWeight.Text);
                currentStuffIn.Remark = txtRemark.Text;
                currentStuffIn.Operator = CurrentUserID;
                currentStuffIn.InNum = currentStuffIn.TotalNum - currentStuffIn.CarWeight ;
                MetageLastSureForm mlsf = new MetageLastSureForm(this, currentStuffIn, checkBox1.Checked, null, true);
                mlsf.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshStuffInList();
        }

        private void ConcreteWeight_Click(object sender, EventArgs e)
        {
            //ZLERP.Licensing.Client.License lic = LicenseInfo;
            //string info = "注册成功，以下为您的产品许可信息:\r\n";
            //info += "\r\n产品版本:" + lic.Edition;
            //info += "\r\n授 权 给:" + lic.LicenceTo;
            //info += "\r\n序 列 号:" + lic.SerialNumber;
            //info += "\r\n许可有效期至:" + lic.ExpireTo.ToLongDateString();

            //info += "\r\n请重新运行程序完成注册！";

            //info += "\r\n\r\n\r\n感谢您使用中联重科正版软件产品！";
            //MessageBox.Show(info, "注册成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ConcreteWeight cwWindow = new ConcreteWeight(0);
            cwWindow.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ConcreteWeight cwWindow = new ConcreteWeight(1);
            cwWindow.ShowDialog();
        }

        private void 出厂ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShippingRecord sr = new ShippingRecord();
            sr.ShowDialog();
        }

        private void 转退料过磅记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TZRecord tr = new TZRecord();
            tr.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ConcreteWeight cwWindow = new ConcreteWeight(2);
            cwWindow.ShowDialog();
        }

      
      

        
    }
}
