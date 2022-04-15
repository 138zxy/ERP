using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using WeightingSystem.Properties;
using log4net;

namespace WeightingSystem
{
    public partial class WeightManage : Form
    {

        public Int32 m_lUserID3 = -1;
        public Int32 m_lRealHandle3 = -1;

        public Int32 m_lUserID4 = -1;
        public Int32 m_lRealHandle4 = -1;

        public Int32 m_lUserID1 = -1;
        public Int32 m_lRealHandle1 = -1;

        public Int32 m_lUserID2 = -1;
        public Int32 m_lRealHandle2 = -1;

        private StuffInfo _si;
        private MainForm _main;
        private bool Videos = false;
        private string Pic1_Path = string.Empty;            //毛重1#摄像头照片名
        private string Pic2_Path = string.Empty;            //毛重2#摄像头照片名
        private string Pic11_Path = string.Empty;            //毛重3#摄像头照片名
        private string Pic21_Path = string.Empty;            //毛重4#摄像头照片名

        static ILog log = LogManager.GetLogger(typeof(WeightManage));
        ERPDataProvider edp = new ERPDataProvider();
        decimal darkRate = 0;
        public WeightManage(StuffInfo si, StuffChoose sc, MainForm main)
        {
            sc.Close();
            InitializeComponent();
            _si = si;
            _main = main;
            lbWeightName.Text = sc._WeightName;
           
            #region 绑定初始化数据
            //绑定过磅材料信息
            lblStuffName.Text = _si.StuffName;
            lblSpec.Text = _si.Spec;
            lblSupplyName.Text = _si.SupplyName;

            //绑定筒仓信息
            ERPDataProvider provider = new ERPDataProvider();

            this.cbSilo.DisplayMember = "SiloName";
            this.cbSilo.ValueMember = "SiloID";
            this.cbSilo.DataSource = provider.GetSiloList(_si.StuffID);


            IList<ListItem> itemsCmp = edp.GetSpecValueList(_si.StuffID);
            itemsCmp.Insert(0, new ListItem());
            cBSpec.DataSource = itemsCmp;
            this.cBSpec.DisplayMember = "Text";
            this.cBSpec.ValueMember = "Value";


            //绑定车辆信息
            PublicHelper ph = new PublicHelper();
            this.cbCar.DataSource = ph.GetCarList();
            #endregion

            //设置暗扣
            Config config = new Config();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(WeightManage_KeyPress);
            lblDarkRate1.Text = si.DarkRate.ToString();

            this.Videos = Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false"));
            if (config.DarkWeightMode == 1)//1:ERP管理暗扣
            {
                panel8.Visible = true;
            }
            if (config.DarkWeightMode == 2)//2:磅房管理暗扣
            {
                panel8.Visible = false;
            }

            Config c = new Config();
            if (c.isAdminUpdate)
            {
                if (edp.isAdmin(MainForm.CurrentUserID))
                {
                    cbDarkWeight.Enabled = true;
                    txtRate.Enabled = true;
                }
                else
                {
                    cbDarkWeight.Enabled = false;
                    txtRate.Enabled = false;
                }
            }

            //绑定合同
            this.comboBox1.DisplayMember = "PactName";
            this.comboBox1.ValueMember = "StockPactID";
            this.comboBox1.DataSource = provider.GetStockPackList(_si.StuffID, _si.SupplyID);

            darkRate = provider.GetDarkRate(_si.StuffID, _si.SupplyID);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            {
                Pic1_Path = "1#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                Pic2_Path = "2#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";

                Pic11_Path = "11#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                Pic21_Path = "21#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";

                this.savePic(Pic1_Path, Pic2_Path, Pic11_Path, Pic21_Path);
            }
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            {
                Pic1_Path = "m1#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                Pic2_Path = "m2#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                _main.HKVideoSavePic(m_lUserID1, _main.lChannel1, Pic1_Path, "1");
                _main.HKVideoSavePic(m_lUserID2, _main.lChannel2, Pic2_Path, "2");

                Pic11_Path = "m3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                Pic21_Path = "m4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                _main.HKVideoSavePic(m_lUserID3, _main.lChannel3, Pic11_Path, "3");
                _main.HKVideoSavePic(m_lUserID4, _main.lChannel4, Pic21_Path, "4");

            }
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 磅房暗扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightManage_KeyPress(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F9)
            {
                this.cbDarkWeight.Visible = !this.cbDarkWeight.Visible;
                if (this.cbDarkWeight.Visible)
                {
                    cbDarkWeight.Focus();
                }

            }
        }

        private void btnTotalWeight_Click(object sender, EventArgs e)
        {
            #region 提交前验证

            if (Convert.ToInt32(lblWeight.Text.Trim())>200000)
            {
                MessageBox.Show(this, "称重值异常,请重新保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if (chkFastMetage.Checked && lblCarWeight.Text == "0")
            {
                MessageBox.Show(this, "没有车皮重信息，不能进行一次过磅操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (int.Parse(lblWeight.Text) == 0)
            {
                MessageBox.Show(this, "称重值过小，无法保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            if (chkFastMetage.Checked)
            {
                if (DialogResult.Cancel == MessageBox.Show("您选择了一次过磅，是否进行过磅？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    return;
                }
            }
            btnTotalWeight.Enabled = false;

            ERPDataProvider edp = new ERPDataProvider();
            StuffIn sin = new StuffIn();
            sin.CarNo = cbCar.Text.Trim();
            sin.CarWeight = 0;
            sin.FastMetage = chkFastMetage.Checked;
            sin.SiloID = cbSilo.SelectedValue.ToString();
            sin.SiloName = cbSilo.Text;
            sin.StuffID = _si.StuffID;
            sin.SupplyID = _si.SupplyID;
            sin.TotalNum = int.Parse(lblWeight.Text);
            sin.Spec = _si.Spec;
            sin.DarkWeight = string.IsNullOrEmpty(cbDarkWeight.Text.Trim()) ? 0 : int.Parse(cbDarkWeight.Text);
            sin.WRate = 0;
            sin.InNum = 0;
            sin.Operator = MainForm.CurrentUserID;
            sin.InDate = DateTime.Now;
            sin.StuffName = _si.StuffName;
            sin.OrderNum = 0;
            sin.Proportion = _si.SopRate;
            sin.WeightName = MainForm.CurrentUserName;
            sin.Builder = MainForm.CurrentUserID;
            sin.StockPactID = comboBox1.SelectedValue==null?"":comboBox1.SelectedValue.ToString();
            sin.SourceAddr = edp.GetSourceAddrByCarID(sin.CarNo);
            sin.Driver = edp.GetDriverByCarID(sin.CarNo);
            sin.SpecID = cBSpec.SelectedValue == null ? 0 : Convert.ToInt32(cBSpec.SelectedValue.ToString()); 
            //结算数量根据原材料价格信息里选择是取值厂商数量还是净重
            sin.FinalFootNum = sin.InNum;
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            {
                if (Pic1_Path != string.Empty)
                {
                    sin.Pic1 = Pic1_Path;
                    sin.Pic2 = Pic2_Path;
                }
                else
                {
                    Pic1_Path = "1#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    Pic2_Path = "2#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    sin.Pic1 = Pic1_Path;
                    sin.Pic2 = Pic2_Path;
                    sin.Pic11 = Pic11_Path;
                    sin.Pic21 = Pic21_Path;
                    Pic11_Path = "11#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    Pic21_Path = "21#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    this.savePic(Pic1_Path, Pic2_Path, Pic11_Path, Pic21_Path);
                }
            }
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            {
                if (Pic1_Path != string.Empty)
                {
                    sin.Pic1 = Pic1_Path;
                    sin.Pic2 = Pic2_Path;
                    sin.Pic11 = Pic11_Path;
                    sin.Pic21 = Pic21_Path;
                }
                else
                {
                    Pic1_Path = "m1#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    Pic2_Path = "m2#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    Pic11_Path = "m3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    Pic21_Path = "m4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    sin.Pic1 = Pic1_Path;
                    sin.Pic2 = Pic2_Path; 

                    _main.HKVideoSavePic(m_lUserID1, _main.lChannel1, Pic1_Path, "1");
                    _main.HKVideoSavePic(m_lUserID2, _main.lChannel2, Pic2_Path, "2");
                    sin.Pic11 = Pic11_Path;
                    sin.Pic21 = Pic21_Path;
                    _main.HKVideoSavePic(m_lUserID3, _main.lChannel3, Pic11_Path, "3");
                    _main.HKVideoSavePic(m_lUserID4, _main.lChannel4, Pic21_Path, "4");
                }
            }           
            if (chkFastMetage.Checked)
            {
                sin.CarWeight = int.Parse(lblCarWeight.Text);
                sin.InNum = sin.TotalNum - sin.CarWeight;
                sin.SupplyName = _si.SupplyName;
                sin.StuffName = _si.StuffName;
                MetageLastSureForm mlsf = new MetageLastSureForm(_main, sin, true, this, false);
                mlsf.ShowDialog();
            }
            else
            {
                saveWeightInfo(sin);
                log.Info("过毛重 车号：" + sin.CarNo + " 材料：[" + sin.StuffName + "][" + sin.Spec + "][" + sin.SupplyName + "]  暗扣：" + sin.DarkWeight.ToString() + "  毛重：" + sin.TotalNum.ToString());
            } 
            btnTotalWeight.Enabled = true;
        }

        private void saveWeightInfo(StuffIn sin)
        {
            try
            {
                ERPDataProvider edp = new ERPDataProvider();
                if (edp.SaveStuffIn(sin))
                {                 
                    _main.RefreshStuffInList();
                    if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SoundWeight, string.Empty)))
                    {
                        _main.SpeakText(sin.TotalNum.ToString() + "千克");
                    }
                    _main.btnStopWeight.Enabled = true;
                    _main.btnStartWeight.Enabled = false;
                    log.Debug("保存毛重");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "称重失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void lblTrueWeight_TextChanged(object sender, EventArgs e)
        {
            int DarkWeight = 0;
            int TrueWeight = int.Parse(lblTrueWeight.Text);
            Config config = new Config();
            //如果是服务器端设置暗扣
            if (config.DarkWeightMode == 1)//1:ERP管理-按原材料基本信息处设置暗扣
            {
                int tempWeight = Convert.ToInt32(TrueWeight * decimal.Parse(lblDarkRate1.Text) / 1000) * 10;
                cbDarkWeight.Text = tempWeight.ToString();

                if (!string.IsNullOrEmpty(cbDarkWeight.Text.Trim()))
                {
                    try
                    {
                        DarkWeight = int.Parse(cbDarkWeight.Text.Trim());
                    }
                    catch
                    {
                        DarkWeight = 0;
                    }
                }
            }
            else if (config.DarkWeightMode == 2)//2:磅房系统管理暗扣
            {
                if (string.IsNullOrEmpty(cbDarkWeight.Text))
                {
                    DarkWeight = 0;
                }
                else
                {
                    try
                    {
                        DarkWeight = int.Parse(cbDarkWeight.Text.Trim());
                    }
                    catch
                    {
                        DarkWeight = 0;
                    }
                }
            }
            else if (config.DarkWeightMode == 3)//3:ERP管理-按原材料合同处设置 暗扣
            {
                if (string.IsNullOrEmpty(cbDarkWeight.Text))
                {
                    DarkWeight = 0;
                }
                else
                {
                    try
                    {
                        DarkWeight = Convert.ToInt32(Convert.ToDecimal(TrueWeight) * darkRate / 100);
                        cbDarkWeight.Text = DarkWeight + "";
                    }
                    catch
                    {
                        DarkWeight = 0;
                    }
                }
            }
            lblWeight.Text = (TrueWeight - DarkWeight).ToString();
        }

        private void cbCar_TextChanged(object sender, EventArgs e)
        {
            //return;//功能转移到了光标移除事件去了
            LocalDataProvider ldp = new LocalDataProvider();
            IList<Car> CarList = ldp.GetCarList("CarNo='" + cbCar.Text + "'");
            if (CarList.Count > 0)
            {
                lblCarWeight.Text = Convert.ToInt32(CarList[0].Weight).ToString();
            }
            else
            {
                lblCarWeight.Text = "0";
            }
        }

        private void cbCar_Leave(object sender, EventArgs e)
        {
            //LocalDataProvider ldp = new LocalDataProvider();
            //IList<Car> CarList = ldp.GetCarList("CarNo='" + cbCar.Text + "'");
            //if (CarList.Count > 0)
            //{
            //    lblCarWeight.Text = Convert.ToInt32(CarList[0].Weight).ToString();
            //}
            //else
            //{
            //    lblCarWeight.Text = "0";
            //}

        }

        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.cbCar);
            fskb.TopMost = true;
            fskb.ShowDialog();
        }

        /// <summary>
        /// 预览视频
        /// </summary>
        /// <param name="iRouteID"></param>
        //private void StartView(int iRouteID)
        //{
        //    Video.DSStream_Initialize();
        //    Video.CSRect rc = new Video.CSRect();
        //    rc.left = 0;
        //    rc.top = 5;

        //    Video.DSStream_ConnectDevice(0, false, this.BigVideo1.Handle);
        //    BigVideo1.Refresh();
        //    rc.right = this.BigVideo1.Size.Width;
        //    rc.bottom = this.BigVideo1.Size.Height;
        //    Video.DSStream_RouteInPinToOutPin(0, iRouteID, 0);
        //    Video.DSStream_SetWindowPos(0, rc);
        //    Video.DSStream_SetVideoStandard(0, Video.VideoStandard.VideoStandard_PAL_B);
        //}

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (Videos)
            //{
            //    checkedradio();
            //}
        }

        private void checkedradio()
        {
            //PublicHelper ph = new PublicHelper();
            //if (radioButton1.Checked)
            //{
            //    _main.OpenHKVideo(_main.DVRIPAddress1, _main.DVRPortNumber1, _main.DVRUserName1, _main.DVRPassword1, BigVideo1.Handle, _main.lChannel1,ref m_lUserID3, m_lRealHandle3);
            //    Video.DSStream_DisconnectDevice(0);
            //    StartView(1);
            //}
            //else
            //{
            //    _main.OpenHKVideo(_main.DVRIPAddress2, _main.DVRPortNumber2, _main.DVRUserName2, _main.DVRPassword2, BigVideo2.Handle, _main.lChannel2,ref m_lUserID4, m_lRealHandle4);
            //    Video.DSStream_DisconnectDevice(0);
            //    StartView(2);
            //}
            //ph.SetVidQuality(BigVideo1.Width,BigVideo1.Height);

        }

        private void WeightManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (Videos)
            //{
            //    Video.DSStream_DisconnectDevice(0);
            //    if (_main.rdbVideo1.Checked)
            //    {
            //        _main.StartView(1);
            //    }
            //    if (_main.rdbVideo2.Checked)
            //    {
            //        _main.StartView(2);
            //    }
            //    if (_main.rdbVideo3.Checked)
            //    {
            //        _main.StartView(3);
            //    }
            //    if (_main.rdbVideo4.Checked)
            //    {
            //        _main.StartView(4);
            //    }
            //    PublicHelper ph = new PublicHelper();
            //    ph.SetVidQuality(BigVideo1.Width, BigVideo1.Height);
            //}
            //if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            //{
            //    _main.StopHKVideo(m_lUserID3, m_lRealHandle3, "3");
            //    _main.StopHKVideo(m_lUserID4, m_lRealHandle4, "4");
            //    _main.StopHKVideo(m_lUserID1, m_lRealHandle1, "1");
            //    _main.StopHKVideo(m_lUserID2, m_lRealHandle2, "2");
            //}
        }


        public void savePic(string _Pic1, string _Pic2, string _Pic11, string _Pic21)
        {
            PublicHelper ph = new PublicHelper();
            int videoNo = 2;
            string FileName = _Pic2;
            //if (radioButton1.Checked)
            //{
            //    videoNo = 1;
            //    FileName = _Pic1;
            //}
            //if (video2.Checked)
            //{
            //    videoNo = 2;
            //    FileName = _Pic2;
            //}
            //if (video3.Checked)
            //{
            //    videoNo = 3;
            //    FileName = _Pic11;
            //}
            //if (video3.Checked)
            //{
            //    videoNo = 4;
            //    FileName = _Pic21;
            //}

            string PicFilePath = MainForm.PICSAVE_PATH;
            if (!Directory.Exists(PicFilePath))
            {
                Directory.CreateDirectory(PicFilePath);
            }
            Video.DSStream_SaveToJpgFile(0, PicFilePath + FileName, Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.VideoQuality, 50));

            //if (videoNo == 1)
            //{
            //    Pic1_Path = FileName;
            //}
            //else
            //{
            //    Pic2_Path = FileName;
            //}
            Video.DSStream_DisconnectDevice(0);
            Thread t = new Thread(DelayVideo);
            t.Start(t);
            //if (videoNo == 1)
            //{
            //    StartView(2);
            //}
            //if (videoNo == 2)
            //{
            //    StartView(1);
            //}
            //if (videoNo == 3)
            //{
            //    StartView(3);
            //}
            //if (videoNo == 4)
            //{
            //    StartView(4);
            //}
            //ph.SetVidQuality(BigVideo1.Width, BigVideo1.Height);

        }

        private void DelayVideo(object thread)
        {
            Thread t = (Thread)thread;
            int videoNo = 1;
            string FileName = Pic1_Path;
            //if (radioButton1.Checked)
            //{
            //    videoNo = 2;
            //    FileName = Pic2_Path;
            //}

            Thread.Sleep(Config.Ini.GetInt32(Config.Section.BaseSetting, "VideoDelay", 0));
            string PicFilePath = MainForm.PICSAVE_PATH;
            if (!Directory.Exists(PicFilePath))
            {
                Directory.CreateDirectory(PicFilePath);
            }

            Video.DSStream_SaveToJpgFile(0, PicFilePath + FileName, Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.VideoQuality, 50));
            t.Abort();
        }

        private void WeightManage_Load(object sender, EventArgs e)
        {
            Config config = new Config();
            //if (config.VideoNum == 1)
            //{
            //    this.tableLayoutPanel1.RowCount = 1;
            //    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));
            //    BigVideo3.Visible = false;

            //    this.tableLayoutPanel2.RowCount = 1;
            //    this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));
            //    BigVideo4.Visible = false;
            //}

            PublicHelper ph = new PublicHelper();
            //设置摄像头
            //if (Videos)
            //{
            //    Video.DSStream_DisconnectDevice(0);
            //    StartView(1);
            //    ph.SetVidQuality(BigVideo1.Width, BigVideo1.Height);
            //}
            //if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            //{
            //    //if (_main.m_lUserID3 < 0)
            //    //{
            //    //    _main.LoginHKVideo(_main.DVRIPAddress1, _main.DVRPortNumber1, _main.DVRUserName1, _main.DVRPassword1, ref _main.m_lUserID3);  
            //    //}
            //    //if (_main.m_lUserID4 < 0)
            //    //{
            //    //    _main.LoginHKVideo(_main.DVRIPAddress2, _main.DVRPortNumber2, _main.DVRUserName2, _main.DVRPassword2, ref _main.m_lUserID4);                    
            //    //}

            //    _main.HKLoginVideo(_main.DVRIPAddress1, _main.DVRPortNumber1, _main.DVRUserName1, _main.DVRPassword1, ref m_lUserID3, "3");
            //    _main.HKOpenVideo(BigVideo1.Handle, _main.lChannel1, m_lUserID3, ref m_lRealHandle3, "3");

            //    _main.HKLoginVideo(_main.DVRIPAddress2, _main.DVRPortNumber2, _main.DVRUserName2, _main.DVRPassword2, ref m_lUserID4, "4");
            //    _main.HKOpenVideo(BigVideo2.Handle, _main.lChannel2, m_lUserID4, ref m_lRealHandle4, "4");

            //    _main.HKLoginVideo(_main.DVRIPAddress3, _main.DVRPortNumber3, _main.DVRUserName3, _main.DVRPassword3, ref m_lUserID1, "1");
            //    _main.HKOpenVideo(BigVideo3.Handle, _main.lChannel3, m_lUserID1, ref m_lRealHandle1, "1");

            //    _main.HKLoginVideo(_main.DVRIPAddress4, _main.DVRPortNumber4, _main.DVRUserName4, _main.DVRPassword4, ref m_lUserID2, "2");
            //    _main.HKOpenVideo(BigVideo4.Handle, _main.lChannel4, m_lUserID2, ref m_lRealHandle2, "2");
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StuffIn si = new StuffIn();
            si.TotalNum = int.Parse(lblWeight.Text.Trim());
            si.StuffName = _si.StuffName;
            si.Spec = _si.Spec;
            si.SupplyName = _si.SupplyName;
            si.CarNo = cbCar.Text;
            PublicHelper ph = new PublicHelper();
            log.Info("打印毛重");
            ph.Print(si);
        }

        private void cbDarkWeight_TextChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cbDarkWeight.Text)<0)
            {
                MessageBox.Show("不能填小于0的数！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            Config config = new Config();
            int darkRate = 0;
            int.TryParse(cbDarkWeight.Text, out darkRate);
            
            lblDarkRate.Text = darkRate.ToString();
            lblWeight.Text = (int.Parse(lblTrueWeight.Text) - darkRate).ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Config c = new Config();
            this.cbDarkWeight.Visible = !this.cbDarkWeight.Visible;
            this.txtRate.Visible = !this.txtRate.Visible;
            if (this.cbDarkWeight.Visible)
            {
                cbDarkWeight.Focus();
            }
        }

        private void btnWeight_Click(object sender, EventArgs e)
        {
            lblWeight.Text = txtWeight.Text.Trim();
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {

            cbDarkWeight.Text = (int.Parse(txtRate.Text.Trim())*int.Parse(lblCarWeight.Text.Trim())/100).ToString();
        }



        

    }
}
