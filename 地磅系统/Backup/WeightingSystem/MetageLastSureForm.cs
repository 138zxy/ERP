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

            else if (!_changesilo)           //若最后去皮
            {
                StuffIn ParentStuffIn = edp.Find(si.ParentStuffInID);
                if (ParentStuffIn != null)
                {
                    labTotalWeight.Text = ParentStuffIn.TotalNum.ToString("0");
                    CurrentTotalWeight = ParentStuffIn.TotalNum;
                    CurrentInnum = ParentStuffIn.TotalNum - si.CarWeight;

                }
            }
            else                        //换罐
            {
                 
                    labTotalWeight.Text = Convert.ToInt32(_si.TotalNum).ToString("0");
                    CurrentTotalWeight = _si.TotalNum;
                    CurrentInnum = _si.InNum;
                
            }

            labSilo.Text = si.SiloName;
            labCarWeight.Text = si.CarWeight.ToString("0");
            Config c = new Config();
            if (c.RateMode == 0)
            {
                labKZ.Text = si.WRate.ToString();
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
            EnablePrintStuffName = Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.EnablePrintStuffName, 0);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
                   
            ERPDataProvider edp = new ERPDataProvider();
            
            _si.OutDate = DateTime.Now;
            _si.CarWeight = int.Parse(labCarWeight.Text.Trim());
            
            /* 拍照 */
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.Videos, "false")))
            {
                if (_form.Pic3_Path != string.Empty)
                {
                    _si.Pic3 = _form.Pic3_Path;
                    _si.Pic4 = _form.Pic4_Path;
                }
                else
                {
                    _si.Pic3 = "3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic4 = "4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";

                    _form.savePic(_si.Pic3, _si.Pic4);
                }

            }
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.HKVideos, "false")))
            {
                if (_form.Pic3_Path != string.Empty)
                {
                    _si.Pic3 = _form.Pic3_Path;
                    _si.Pic4 = _form.Pic4_Path;
                }
                else
                {
                    _si.Pic3 = "3#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _si.Pic4 = "4#" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    _form.Pic3_Path = _si.Pic3;
                    _form.Pic4_Path = _si.Pic4;
                    _form.HKVideoSavePic(_form.m_lUserID1, _form.lChannel1, _si.Pic3,"1");
                    _form.HKVideoSavePic(_form.m_lUserID2, _form.lChannel1, _si.Pic4,"2");
                }

            }
            _si.SourceAddr = comboBox2.Text;
            _si.TransportID = comboBox1.SelectedValue.ToString();
            _si.TransportName = comboBox1.Text;
            if (!_changesilo)
            {
                //_si.InNum = decimal.Parse(labStuffWeight.Text.Trim());
                int kz = Convert.ToInt32((_si.TotalNum - _si.CarWeight) * _si.WRate / 100);
                kz = Convert.ToInt16((kz + 5) / 10) * 10;
                _si.InNum = _si.TotalNum - _si.CarWeight - kz;
                if (_si.Proportion != 0)
                {
                    _si.FootNum = (_si.TotalNum - _si.CarWeight - kz) / _si.Proportion;
                }
                if (edp.SaveStuffIn(_si))
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

                    _form.Pic3_Path = string.Empty;
                    _form.Pic4_Path = string.Empty;

                    if (_wm != null)
                    {
                        _wm.Close();
                    }

                    if (_Print)
                    {
                        PublicHelper ph = new PublicHelper();
                        if (EnablePrintStuffName == 1)
                        {
                            ldp.AddPrintStuffName(_si.StuffID, comboBox2.Text);
                            _si.StuffName = comboBox2.Text;
                        }

                        _si.TotalNum = CurrentTotalWeight;
                        _si.InNum = decimal.Parse(labStuffWeight.Text.Trim());
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

                    log.Info("单号："+ _si.StuffInID + " 去皮完成，毛重："+_si.TotalNum.ToString() + ",皮重："+ _si.CarWeight.ToString() +",暗扣：" + _si.DarkWeight.ToString() + ",扣含水：" + _si.WRate.ToString() + "%");
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

                    _form.Pic3_Path = string.Empty;
                    _form.Pic4_Path = string.Empty;

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

    }
}
