using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Helpers;
using WeightingSystem.Models;
using log4net;
using System.Text.RegularExpressions;


namespace WeightingSystem
{
    public partial class GHWeight : Form
    {
        static ILog log = LogManager.GetLogger(typeof(GHWeight));
        private int _FunctionFlag = 0;
        //功能标志 0：出厂过磅  1：剩料过磅
        private ShippingDocumentGH CurrentShipDoc = null;
        string _CurrentUserId;

        Config c = new Config();
        ERPDataProvider erp = new ERPDataProvider();

        public GHWeight(string weightname, string CurrentUserId)
        {
            InitializeComponent();
            _CurrentUserId = CurrentUserId;
            lbWeightName.Text = weightname;

            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarListGH();
            IList<ListItem> titleList = edp.GetTitleList();
            itemsCar.Insert(0, new ListItem());
            titleList.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";

            comboBox1.DataSource = titleList;
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";


            lblFunctionTitle.Text = "干混出厂过磅";
            this.Text = "干混出厂过磅";
            this.panel_type.Visible = false;

            radioButton1_CheckedChanged(null, null);
            radioButton4_CheckedChanged(null, null);

            if (c.isAdminUpdate)
            {
                if (erp.isAdmin(_CurrentUserId))
                {
                    radioButton1.Enabled = true;
                    radioButton4.Enabled = true;
                    lblExchange.ReadOnly = false;
                }
                else
                {
                    radioButton1.Enabled = false;
                    radioButton4.Enabled = false;
                    lblExchange.ReadOnly = true;
                }
            }

            if (c.ZtDefaultCheckPrint)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

        }

        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.cbCar);
            fskb.ShowDialog();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetControlValue();
            ShippingDocumentGH sd = ValidTzOrShipMetageGH(cbCar.Text);
            if (sd == null)
            {
                return;
            }

            ERPDataProvider edp = new ERPDataProvider();
            decimal HourInterval = edp.getCarHourInterval();
            if (sd.ServerTime != null)
            {
                TimeSpan ts = Convert.ToDateTime(sd.ServerTime).Subtract(Convert.ToDateTime(sd.ProduceDate));

                if (Convert.ToDecimal(ts.TotalHours) > HourInterval)                 //若找到的最近发货时间超过了ERP设置的回厂时间差
                {
                    if (DialogResult.Cancel == MessageBox.Show("找到了最近的发货记录，但是该车回厂(出厂)时间已经超过系统设置时间【" + HourInterval.ToString() + "】小时,是否继续进行操作？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
            }
            if (sd.Cube > 0)
            {
                if (MessageBox.Show("该车已过磅,是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            lblSystemCarWeight.Text = edp.getCarEmptyWeight(cbCar.Text.Trim()).ToString();
            lblCarTitle.Text = sd.CarID;
            lbTWeight.Text = lblWeight.Text.ToString();
            if (!c.IsGetConStrengthExchange)
            {
                //若设置了统一换算率
                int Exchange = Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.Exchange, 0);
                if (Exchange > 0)
                {
                    sd.ConstrengthExchange = Exchange;
                }
            }
            else//获取砼强度对应的换算率
            {
                int Exchange = edp.getConStrengthExchange(sd.ConStrength);
                sd.ConstrengthExchange = Exchange;
            }
            lblExchange.Text = sd.ConstrengthExchange.ToString();
            txtCustomer.Text = lblCustomer.Text = sd.CustName;
            txtProject.Text = lblProject.Text = sd.ProjectName;
            txtConstrength.Text = lblConstrength.Text = sd.ConStrength;
            txtCastMode.Text = lblCastMode.Text = (string.IsNullOrEmpty(sd.PumpName) ? sd.CastMode : sd.PumpName);
            txtConsPos.Text = lblConsPos.Text = sd.ConsPos;
            txtCar.Text = lblCar.Text = sd.CarID;
            txtScdata.Text = lblScdata.Text = sd.ProduceDate.ToString();
            //lblProvidedTimes.Text = sd.ProvidedTimes.ToString();
            //lblProvidedCube.Text = sd.ProvidedCube.ToString();

            //txtProvidedTimes.Text = sd.ProvidedTimes.ToString();
            //txtProvidedCube.Text = sd.ProvidedCube.ToString();

            lblProvidedTimes.Text = edp.getProvidedTimesGH(sd.TaskID).ToString();
            lblProvidedCube.Text = edp.getProvidedCubeGH(sd.TaskID).ToString();

            txtProvidedTimes.Text = lblProvidedTimes.Text;
            txtProvidedCube.Text = lblProvidedCube.Text;

            txtProvidedTimes2.Text = sd.ProvidedTimes.ToString();
            txtSendCube.Text = sd.SendCube.ToString();
            txtRemark.Text = sd.Remark;
            //CurrentShipDocId = sd.ShipDocID;
            //CurrentTaskId = sd.TaskID;
            txtShippDocId.Text = sd.ShipDocID;
            txtSlurrycount.Text = sd.SlurryCount.ToString();
            txtShippingCube.Text = sd.ShippingCube.ToString();

            CurrentShipDoc = sd;
            //LocalDataProvider ldp = new LocalDataProvider();
            //IList<Car> CarList = ldp.GetCarList("CarNo='" + cbCar.Text.Trim() + "'");
            //if (CarList.Count > 0)
            //{
            //    lblSystemCarWeight.Text = Convert.ToInt32(CarList[0].Weight).ToString();
            //}

            CalcCube();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                lblSystemCarWeight.Enabled = false;
                numericUpDown1.Enabled = true;
                label2.Enabled = true;
            }
            else
            {
                lblSystemCarWeight.Enabled = true;
                numericUpDown1.Enabled = false;
                label2.Enabled = false;
            }

            CalcCube();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                numericUpDown2.Enabled = true;
                label16.Enabled = true;
            }
            else
            {
                numericUpDown2.Enabled = false;
                label16.Enabled = false;
            }

            CalcCube();
        }

        private void CalcCube()
        {
            if (!string.IsNullOrEmpty(lblCarTitle.Text))
            {
                int TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lbTWeight.Text);
                int CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                decimal LastWeight = TotalWeight - CarWeight;
                lblLastWeight.Text = LastWeight.ToString("0");
                decimal Cube = Math.Round(LastWeight / (Convert.ToInt32(lblExchange.Text) == 0 ? 1 : Convert.ToInt32(lblExchange.Text)), 2);
                lblCube.Text = Cube.ToString();
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(numericUpDown2.Value.ToString()) != 0 && Convert.ToDecimal(numericUpDown2.Value.ToString()) < Convert.ToDecimal(lbTWeight.Text.Trim()))
            {
                MessageBox.Show("自填毛重不能小于系统毛重！");
            }
            ERPDataProvider edp = new ERPDataProvider();
            if (!string.IsNullOrEmpty(lblCarTitle.Text))
            {

                if (decimal.Parse(lblCube.Text) <= 0)
                {
                    MessageBox.Show(this, "换算方量不能小于等于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //if (comboBox1.Text == "")
                //{
                //    MessageBox.Show(this, "打印台头不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                double a = Convert.ToDouble(lblCube.Text.Trim());
                double b = Convert.ToDouble(txtSendCube.Text.Trim());
                double c = a - b;
                double carcube = Convert.ToDouble(txtShippingCube.Text.Trim());
                if (carcube < a)
                {
                    if (DialogResult.OK != MessageBox.Show("换算方量不能大于运输方量，你确定要继续吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                if (Math.Abs(c) >= 0.5)
                {
                    if (DialogResult.OK != MessageBox.Show("方量与调度方量差值大于正负0.5，你确定要继续吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }

                if (DialogResult.OK == MessageBox.Show("是否确定该操作？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {

                    //提交时，再次验证
                    ShippingDocumentGH validSd = ValidTzOrShipMetageGH(cbCar.Text);
                    if (validSd == null)
                    {
                        return;
                    }

                    PublicHelper ph = new PublicHelper();
                        ShippingDocumentGH sd = CurrentShipDoc;
                        sd.ShipDocID = CurrentShipDoc.ShipDocID;

                        sd.TaskID = CurrentShipDoc.TaskID;
                        //毛重
                        sd.TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lbTWeight.Text);//
                        //皮重
                        sd.CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                        //净重
                        sd.Weight = int.Parse(lblLastWeight.Text);//

                        sd.Exchange = int.Parse(lblExchange.Text);
                        sd.Cube = decimal.Parse(lblCube.Text);
                        sd.Title = comboBox1.Text;
                        sd.ProvidedTimes = Convert.ToInt32(txtProvidedTimes.Text.Trim()) + 1;
                        sd.ProvidedCube = Convert.ToDecimal(txtProvidedCube.Text.Trim()) + sd.Cube; // Convert.ToDecimal(txtProvidedCube.Text.Trim()) + Convert.ToDecimal(txtSendCube.Text);
                        sd.Remark = txtRemark.Text;
                        sd.WeightName = lbWeightName.Text;
                        sd.DeliveryTime = DateTime.Now;
                        sd.CarID = cbCar.Text.Trim();
                        sd.WeightMan = MainForm.CurrentUserName;

                        //log.Error("磅名:" + sd.WeightName +"称重人："+sd.WeightMan);
                        try
                        {
                            if (edp.UpdateShippingDocument2GH(sd))
                            {
                                MessageBox.Show(this, "出厂过磅操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintShipDocReport, "false")))
                                {
                                    if (checkBox1.Checked)
                                    {
                                        //sd.Tel = edp.getTelByShippingDocumentGH(sd.ShipDocID);
                                        //sd = ValidTzOrShipMetage(cbCar.Text);
                                        ph.printShipDocConcreteReportGH(sd);
                                    }
                                }
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(this, "出厂过磅操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("出厂过磅操作失败:" + ex.Message);
                            MessageBox.Show("错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                }
            }
        }

        private void lblWeight_TextChanged(object sender, EventArgs e)
        {
            CalcCube();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CalcCube();
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            CalcCube();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CalcCube();
        }

        private void numericUpDown2_KeyUp(object sender, KeyEventArgs e)
        {
            CalcCube();
        }
        /// <summary>
        /// 过磅条件合法性验证
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        private ShippingDocumentGH ValidTzOrShipMetageGH(string CarId)
        {
            #region 过磅条件合法性验证
            if (CarId.Trim() == string.Empty)          //若没输出车号点击检索
            {

                MessageBox.Show(this, "请输入车号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCar.Focus();
                ResetControlValue();
                return null;
            }

            ERPDataProvider edp = new ERPDataProvider();
            ShippingDocumentGH sd = edp.GetShippingDocumentByCarIdGH(CarId.Trim());

            if (sd == null)
            {
                MessageBox.Show(this, "没有找到该车的发料记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
            {
                if ((sd.ConstrengthExchange == null || sd.ConstrengthExchange == 0) && Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.Exchange, 0) == 0)
                {
                    MessageBox.Show(this, "强度【" + sd.ConStrength + "】 没有设置换算率，不能进行过磅！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            return sd;
            #endregion
        }
        /// <summary>
        /// 重置控件值
        /// </summary>
        private void ResetControlValue()
        {
            lblCarTitle.Text = string.Empty;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            lblSystemCarWeight.Text = "0";
            lblLastWeight.Text = "0";
            lblExchange.Text = "0";
            lblCube.Text = "0";

            lblCustomer.Text = string.Empty;
            lblProject.Text = string.Empty;
            lblConstrength.Text = string.Empty;
            lblCastMode.Text = string.Empty;
            lblConsPos.Text = string.Empty;
            lblScdata.Text = string.Empty;
            lblCar.Text = string.Empty;
            lblProvidedTimes.Text = string.Empty;
            lblProvidedCube.Text = string.Empty;

            CurrentShipDoc = null;
        }

        private void lblExchange_Leave(object sender, EventArgs e)
        {

        }

        private void lblExchange_KeyUp(object sender, KeyEventArgs e)
        {
            CalcCube();
        }
        /// <summary>
        /// 反算换算率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCube_KeyUp(object sender, KeyEventArgs e)
        {
            Regex x = new Regex(@"^-?\d+\.\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (IsNumeric(lblCube.Text.Trim()) || x.Match(lblCube.Text.Trim()).Success)
            {

            }
            else
            {
                return;
            }
            if (!string.IsNullOrEmpty(lblCarTitle.Text))
            {
                int TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lbTWeight.Text);
                int CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                decimal LastWeight = TotalWeight - CarWeight;
                lblLastWeight.Text = LastWeight.ToString("0");
                decimal Cube = Math.Round(LastWeight / Convert.ToDecimal(lblCube.Text), 0);
                lblExchange.Text = Cube.ToString();
            }
        }

        private void lblCube_Leave(object sender, EventArgs e)
        {

        }

        bool IsNumeric(string str) //接收一个string类型的参数,保存到str里
        {
            if (str == null || str.Length == 0)    //验证这个参数是否为空
                return false;                           //是，就返回False
            ASCIIEncoding ascii = new ASCIIEncoding();//new ASCIIEncoding 的实例
            byte[] bytestr = ascii.GetBytes(str);         //把string类型的参数保存到数组里

            foreach (byte c in bytestr)                   //遍历这个数组里的内容
            {
                if (c < 48 || c > 57)                          //判断是否为数字
                {
                    return false;                              //不是，就返回False
                }
            }
            return true;                                        //是，就返回True
        }   

    }
}
