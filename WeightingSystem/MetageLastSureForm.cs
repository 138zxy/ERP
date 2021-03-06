using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using log4net;
using System.Data.SqlClient;

namespace WeightingSystem
{
    public partial class MetageLastSureForm : Form
    {
        
        private StuffIn _si;
        private bool _Print;
        private MainForm _form;
        private WeightManage _wm;
        private int EnablePrintStuffName = 0;
        private bool _changesilo;
        //private decimal ParentTotalWeight = 0;
        private decimal CurrentTotalWeight = 0;
        private decimal CurrentInnum = 0;
        static ILog log = LogManager.GetLogger(typeof(MetageLastSureForm));
        public MetageLastSureForm(MainForm form, StuffIn si,bool Print,WeightManage wm,bool changeSilo)
        {
            InitializeComponent();

            checkBox1.Checked = Print;

            ERPDataProvider edp = new ERPDataProvider();
            _si = si;
            _Print = Print;
            _form = form;
            _wm = wm;
            _changesilo = changeSilo;
         
            if (_wm != null)   //若在正常过磅中，最后确认显示扣皮输入框
            {
                CarWeightUpdate.Visible = false;
                labTitle.Text = "【一次过磅】";
            }
            else
            {
                if (_si.WRate == 0)                         //调整皮重只对于含水为 0 的可用
                {
                    CarWeightUpdate.Visible = true;
                    CarWeightUpdate.Visible = false;
                }
                else
                {
                    CarWeightUpdate.Visible = false;
                }

                if (changeSilo == false)
                {
                    labTitle.Text = "【去皮】";
                }
                else
                {
                    checkBox1.Visible = false;
                    labTitle.Text = "【换罐】";
                }
            }
            lblTrueCarWeight.Text = _si.CarWeight.ToString("0");
            labCarNo.Text = si.CarNo;

            if(string.IsNullOrEmpty(si.ParentStuffInID))
            {
                labTotalWeight.Text = _si.TotalNum.ToString("0");
                CurrentTotalWeight = _si.TotalNum;
                CurrentInnum = _si.InNum;
            }

            else if (!_changesilo)//若最后去皮
            {
                StuffIn ParentStuffIn = edp.Find(si.ParentStuffInID);
                if (ParentStuffIn != null)
                {
                    labTotalWeight.Text = ParentStuffIn.TotalNum.ToString("0");
                    CurrentTotalWeight = ParentStuffIn.TotalNum;
                    CurrentInnum = ParentStuffIn.TotalNum - si.CarWeight;

                }
            }
            else//换罐
            {                 
                labTotalWeight.Text = Convert.ToInt32(_si.TotalNum).ToString("0");
                CurrentTotalWeight = _si.TotalNum;
                CurrentInnum = _si.InNum;
                
            }

            labSilo.Text = si.SiloName;
            labCarWeight.Text = si.CarWeight.ToString("0");//
            //log.Info(DateTime.Now + "1车辆皮重:" + labCarWeight.Text);
            txtSupplyNum.Text = si.InNum.ToString();
            Config c = new Config();
            if (c.RateMode == 0)
            {
                //labKZ.Text = si.WRate.ToString();
                labKZ.Text = si.WRate.ToString() + "  %";
            }
            else
            {
                labKZ.Text = si.WRate.ToString() + "  %";
            }
            label11.Text = si.StuffName;
            label9.Text = si.Spec;
            label8.Text = si.SupplyName;
            labStuffWeight.Text = CurrentInnum.ToString("0");

            IList<ListItem> TransferList = edp.GetTransferList();
            TransferList.Insert(0, new ListItem() {Value="",Text="" });
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";
            comboBox1.DataSource = TransferList;


            IList<ListItem> SourceList = edp.GetSourceList();
            SourceList.Insert(0, new ListItem() { Value = "", Text = "" });
            comboBox4.ValueMember = "Value";
            comboBox4.DisplayMember = "Text";
            comboBox4.DataSource = SourceList;

            //公司列表
            IList<ListItem> itemsCmp = edp.GetCmpValueList();
            itemsCmp.Insert(0, new ListItem());
            cbComp.DataSource = itemsCmp;
            this.cbComp.DisplayMember = "Text";
            this.cbComp.ValueMember = "Value";
            //IList<ListItem> TransferList1 = edp.GetTransferList();
            //TransferList.Insert(0, new ListItem() { Value = "", Text = "" });
            //comboBox3.ValueMember = "Value";
            //comboBox3.DisplayMember = "Text";
            //comboBox3.DataSource = TransferList;

            EnablePrintStuffName = Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.EnablePrintStuffName, 0);
            if (EnablePrintStuffName == 1)
            {
                LocalDataProvider ldp = new LocalDataProvider();
                IList<PrintStuffInfo> printStuffList = ldp.getPrintStuffList(si.StuffID);
                comboBox2.DisplayMember = "PrintStuffName";
                comboBox2.ValueMember = "ID";
                comboBox2.DataSource = printStuffList;
                
                foreach (PrintStuffInfo printStuff in printStuffList)
                {
                    if (printStuff.DefaultSelect)
                    {
                        comboBox2.SelectedValue = printStuff.ID;
                        comboBox2.SelectedText = printStuff.PrintStuffName;
                        comboBox2.Text = printStuff.PrintStuffName;
                        break;
                    }
                }
                if (printStuffList.Count == 0)
                {
                    comboBox2.SelectedValue = _si.StuffName;
                    comboBox2.SelectedText = _si.StuffName;
                    comboBox2.Text = _si.StuffName;                        
                }
            }
            else
            {
                comboBox2.Enabled = false;
            }
            lbKz.Text = si.MingWeight.ToString();
            tbDriver.Text = si.Driver.ToString();
            //tbSourceAddr.Text = si.SourceAddr.ToString();
            tBRate.Text = si.Proportion.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;    
            //if(Convert.ToDecimal(_form.txtCarWeight.Text)!=Convert.ToDecimal(labCarWeight.Text.Trim()))
            //{
            //    MessageBox.Show("皮重值不正确,请重新去皮！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    return;
            //}
            ERPDataProvider edp = new ERPDataProvider();           
            _si.OutDate = DateTime.Now;
            _si.CarWeight = int.Parse(labCarWeight.Text.Trim());//
           
            //log.Info(DateTime.Now+"2车辆皮重:" + _si.CarWeight);
            /* 拍照 */
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            {
                if (_form.Pic_Path1 != string.Empty)
                {
                    _si.Pic1 = _form.Pic_Path1;
                    _si.Pic2 = _form.Pic_Path2;
                    _si.Pic3 = _form.Pic_Path3;
                    _si.Pic4 = _form.Pic_Path4;
                }
                else
                {
                    _si.Pic1 = "1#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic2 = "2#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic3 = "3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic4 = "4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _form.savePic(_si.Pic1, _si.Pic2, _si.Pic3, _si.Pic4);
                }

            }
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            {
                if (_form.Pic_Path1 != string.Empty)
                {
                    _si.Pic1 = _form.Pic_Path1;
                    _si.Pic2 = _form.Pic_Path2;
                    _si.Pic3 = _form.Pic_Path3;
                    _si.Pic4 = _form.Pic_Path4;
                }
                else
                {
                    _si.Pic1 = "p1#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic2 = "p2#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic3 = "p3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic4 = "p4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _form.HKVideoSavePic(_form.m_lUserID1, _form.lChannel1, _si.Pic1,"1");
                    _form.HKVideoSavePic(_form.m_lUserID2, _form.lChannel2, _si.Pic2, "2");
                    _form.HKVideoSavePic(_form.m_lUserID3, _form.lChannel3, _si.Pic3, "3");
                    _form.HKVideoSavePic(_form.m_lUserID4, _form.lChannel4, _si.Pic4, "4");
                }

            }
            //_si.SourceAddr = comboBox2.Text;
            _si.TransportID =comboBox1.SelectedValue==null?"": comboBox1.SelectedValue.ToString();
            _si.TransportName = comboBox1.Text;
            if (string.IsNullOrEmpty(txtSupplyNum.Text.Trim()))
            {
                MessageBox.Show(this, "请输入厂商数量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplyNum.Focus();
                button1.Enabled = true;
                return;
            }
            decimal SupplyNum = Convert.ToDecimal(txtSupplyNum.Text.Trim());//厂商数量
            _si.SupplyNum = Convert.ToDecimal(txtSupplyNum.Text.Trim());//厂商数量

            Config c = new Config();
            if (c.SupplyNumIsT)//厂商数量默认按吨填写入数据库
                _si.SupplyNum = SupplyNum * 1000;
            _si.CompanyID = cbComp.SelectedValue == null ? "" : cbComp.SelectedValue.ToString();
            _si.CompanyName = cbComp.Text.ToString();
            _si.SourceNumber = txtSource.Text;
            _si.SourceAddr = comboBox4.SelectedValue == null ? "" : comboBox4.SelectedValue.ToString();
            _si.Driver = tbDriver.Text.Trim();
            _si.Batch = tBbatch.Text.Trim().ToString();
            if (!_changesilo)
            {
                _si.InNum = decimal.Parse(labStuffWeight.Text.Trim());
                 //Config c = new Config();
                 int kz = 0;
                 if (c.RateMode == 0)
                 {
                     kz = Convert.ToInt32(_si.MingWeight);
                     _si.InNum = _si.TotalNum - _si.CarWeight - kz;
                 }
                 if (c.RateMode == 1)
                 {
                     kz = Convert.ToInt32((_si.TotalNum - _si.CarWeight) * _si.WRate / 100);
                     kz = Convert.ToInt32((kz + 5) / 10) * 10;
                     _si.InNum = _si.TotalNum - _si.CarWeight - kz;
                 }
                if (_si.Proportion != 0)
                {
                    _si.FootNum = (_si.TotalNum - _si.CarWeight - kz) / _si.Proportion;
                }
                //结算数量根据原材料价格信息里选择是取值厂商数量还是净重
                string footfrom = edp.getFootNumBySupplyID(_si.SupplyID);
                if (footfrom == "0")
                {
                    _si.FinalFootNum = _si.SupplyNum;
                }
                else {
                    _si.FinalFootNum = _si.InNum;
                }
                

                decimal Proportion;
                _si.Proportion = decimal.TryParse(tBRate.Text.ToString(), out Proportion) ? Convert.ToDecimal(tBRate.Text.ToString()) : 1.00m;

                _si.Volume = Convert.ToDecimal(tBCube.Text.ToString());

                if (c.VolumeSupplyNum)//方量等于厂商数量
                    _si.Volume = SupplyNum;
                if (edp.SaveStuffInGP(_si))
                {
                    LocalDataProvider ldp = new LocalDataProvider();

                    /* 更新本地车皮重信息 */
                    IList<Car> carList = ldp.GetCarList("CarNo='" + _si.CarNo + "'");
                    if (carList.Count == 0)
                    {
                        Car car = new Car();
                        car.CarNo = _si.CarNo;
                        car.Weight = _si.CarWeight;
                        car.MarkTime = DateTime.Now;
                        ldp.addCar(car);

                    }
                    else if (carList[0].Weight != _si.CarWeight)
                    {
                        carList[0].Weight = _si.CarWeight;
                        carList[0].MarkTime = DateTime.Now;
                        ldp.updateCar(carList[0], carList[0].CarNo);
                    }
                    _form.btnStopWeight.Enabled = true;
                    _form.btnStartWeight.Enabled = false;
                    _form.Pic_Path1 = string.Empty;
                    _form.Pic_Path2 = string.Empty;
                    if (_wm != null)
                    {
                        _wm.Close();
                    }
                    //if (Convert.ToDecimal(labCarWeight.Text) != Convert.ToDecimal(_si.CarWeight))
                    //{
                    //    log.Info("数据不平衡:"+labCarWeight.Text+"--"+_si.CarWeight);
                    //    MessageBox.Show("数据不平衡:" + labCarWeight.Text + "--" + _si.CarWeight);
                    //}
                    //if (_Print)
                    if (checkBox1.Checked)
                    {
                        PublicHelper ph = new PublicHelper();
                        if (EnablePrintStuffName == 1)
                        {
                            ldp.AddPrintStuffName(_si.StuffID, comboBox2.Text);
                            _si.StuffName = comboBox2.Text;
                        }

                        _si.TotalNum = CurrentTotalWeight;
                        _si.InNum = decimal.Parse(labStuffWeight.Text.Trim());
                        if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintStuffinReport, "false")))
                        {
                            if (checkBox1.Checked)
                            {
                                if (!string.IsNullOrEmpty(_si.ParentStuffInID))
                                {
                                    StuffIn newSi = edp.GetStuffIn(_si.ParentStuffInID);
                                    ph.Print(newSi);
                                }
                                else
                                {
                                    ph.Print(_si);
                                }
                            }
                        }                        
                    }

                    log.Info("操作员："+MainForm.CurrentUserName+"("+MainForm.CurrentUserID +")单号："+ _si.StuffInID + " 去皮完成，毛重："+_si.TotalNum.ToString() + ",皮重："+ _si.CarWeight.ToString() +",暗扣：" + _si.DarkWeight.ToString() + ",扣含水：" + _si.WRate.ToString() + "%");
                    this.Close();
                    if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SoundWeight, string.Empty)))
                    {
                        _form.SpeakText(_si.CarWeight.ToString() + "千克");
                    }
                }
                else
                {
                    MessageBox.Show("过磅失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                if (edp.ChangeSilo(_si))
                {
                    LocalDataProvider ldp = new LocalDataProvider();

                    _form.btnStopWeight.Enabled = true;
                    _form.btnStartWeight.Enabled = false;

                    _form.Pic_Path1 = string.Empty;
                    _form.Pic_Path2 = string.Empty;

                    if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SoundWeight, string.Empty)))
                    {
                        _form.SpeakText(_si.CarWeight.ToString() + "千克");
                    }
                    MessageBox.Show("换罐成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("过磅失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _form.RefreshStuffInList();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CarWeightUpdate_TextChanged(object sender, EventArgs e)
        {
            int reduceCarWeight = 0;
            if (CarWeightUpdate.Text.Trim() != string.Empty)
            {
            
                try                                             //若输入非数字，则不进行修改
                {
                    reduceCarWeight = int.Parse(CarWeightUpdate.Text.Trim());
                }
                catch
                {
                    return;
                }
            }
        
            int CarWeight = int.Parse(lblTrueCarWeight.Text.Trim());
            labCarWeight.Text = (CarWeight + reduceCarWeight).ToString();
            labStuffWeight.Text = (Convert.ToInt32(CurrentInnum) - reduceCarWeight).ToString();           
        }

        //string str = "uid=sa;pwd=123;server=.;database=ERP4CT;";
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string sql = "select SupplyName from SupplyInfo where Version=5";
            //SqlConnection con = new SqlConnection(str);
            //con.Open();
            //SqlCommand cmd = new SqlCommand(sql,con);
            //DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(ds,"comboBox3");
            //int j = ds.Tables[0].Rows.Count;
            //comboBox3.DataSource = ds.Tables["comboBox3"];
            //comboBox3.DisplayMember = "SupplyName";
            //con.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    if (CarWeightUpdate.Visible)
                    {
                        CarWeightUpdate.Visible = false;
                    }
                    else
                    {
                        CarWeightUpdate.Visible = true;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);//这里return true 否则控件焦点会跟着方向键改变
        }

        private void tBRate_TextChanged(object sender, EventArgs e)
        {
            try 
	        {
                decimal rate = Convert.ToDecimal(tBRate.Text.ToString());
                decimal innum =labStuffWeight.Text==""?0: decimal.Parse(labStuffWeight.Text.Trim());
                tBCube.Text = Convert.ToDecimal((innum / rate)).ToString("0.00");
	        }
	        catch (Exception)
	        {
                //MessageBox.Show("请正确输入字符！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
		        //throw;
                tBRate.Text = "1.00";
	        }
        }
    }
}
