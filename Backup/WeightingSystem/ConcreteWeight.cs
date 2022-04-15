using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Helpers;
using WeightingSystem.Models;


namespace WeightingSystem
{
    public partial class ConcreteWeight : Form
    {
        private int _FunctionFlag = 0;              //功能标志 0：出厂过磅  1：剩料过磅
        //private string CurrentShipDocId = string.Empty;
        //private string CurrentTaskId = string.Empty;
        private ShippingDocument CurrentShipDoc = null;
        public ConcreteWeight(int FunctionFlag)
        {
            InitializeComponent();


            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarList();
            itemsCar.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";

            _FunctionFlag = FunctionFlag;
            if (_FunctionFlag == 0)
            {
                lblFunctionTitle.Text = "出厂过磅";
                this.Text = "出厂过磅";
                this.panel_type.Visible = false;
            }
            else if (_FunctionFlag == 2)
            {
                lblFunctionTitle.Text = "分合车过磅";
                this.Text = "分合车过磅";
                this.panel_type.Visible = false;
            }
            else
            {
                lblFunctionTitle.Text = "剩退料过磅";
                this.Text = "剩退料过磅";
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

            ShippingDocument sd =   ValidTzOrShipMetage(cbCar.Text);
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
            lblSystemCarWeight.Text = edp.getCarEmptyWeight(cbCar.Text.Trim()).ToString();
            lblCarTitle.Text = sd.CarID;
            //若设置了统一换算率
            int Exchange = Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.Exchange, 0);
            if (Exchange > 0)
            {
                sd.ConstrengthExchange = Exchange;
            }
            lblExchange.Text = sd.ConstrengthExchange.ToString();
            lblCustomer.Text = sd.CustName;
            lblProject.Text = sd.ProjectName;
            lblConstrength.Text = sd.ConStrength;
            lblCastMode.Text = (string.IsNullOrEmpty(sd.PumpName) ? sd.CastMode : sd.PumpName);
            lblConsPos.Text = sd.ConsPos;
            lblCar.Text = sd.CarID;
            lblScdata.Text = sd.ProduceDate.ToString();
            lblProvidedTimes.Text = sd.ProvidedTimes.ToString();
            lblProvidedCube.Text = sd.ProvidedCube.ToString();
            //CurrentShipDocId = sd.ShipDocID;
            //CurrentTaskId = sd.TaskID;
            CurrentShipDoc = sd;
            LocalDataProvider ldp = new LocalDataProvider();
            IList<Car> CarList = ldp.GetCarList("CarNo='" + cbCar.Text.Trim() + "'");
            if (CarList.Count > 0)
            {
                lblSystemCarWeight.Text = Convert.ToInt32(CarList[0].Weight).ToString();
            }

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


        private void CalcCube()
        {
            if (!string.IsNullOrEmpty(lblCarTitle.Text))
            {
                int TotalWeight = int.Parse(lblWeight.Text);
                int CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                decimal LastWeight = TotalWeight - CarWeight;
                lblLastWeight.Text = LastWeight.ToString("0");
                decimal Cube = Math.Round(LastWeight / Convert.ToInt32(lblExchange.Text), 2);
                lblCube.Text = Cube.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
          

            ERPDataProvider edp =  new ERPDataProvider();
            if (!string.IsNullOrEmpty(lblCarTitle.Text))
            {

                if (decimal.Parse(lblCube.Text) <= 0)
                {
                    MessageBox.Show(this, "换算方量不能小于等于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (DialogResult.OK == MessageBox.Show("是否确定该操作？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {

                    //提交时，再次验证
                    ShippingDocument validSd = ValidTzOrShipMetage(cbCar.Text);
                    TZrelation tz = new TZrelation();
                    tz.SourceShipDocID = CurrentShipDoc.ShipDocID;
                    tz.Cube = decimal.Parse(lblCube.Text);
                    tz.CarID = cbCar.Text;
                    tz.TotalWeight = int.Parse(lblWeight.Text);
                    tz.CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                    tz.Weight = int.Parse(lblLastWeight.Text);
                    tz.Exchange = int.Parse(lblExchange.Text);
                    tz.Type = "";
                    if (validSd == null)
                    {
                        return;
                    }

                    PublicHelper ph = new PublicHelper();
                    if (_FunctionFlag == 0)                 //出厂过磅
                    {
                        ShippingDocument sd = CurrentShipDoc;
                        sd.ShipDocID = CurrentShipDoc.ShipDocID;
                        sd.TotalWeight = int.Parse(lblWeight.Text);
                        sd.TaskID = CurrentShipDoc.TaskID;
                        sd.CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                        sd.Weight = int.Parse(lblLastWeight.Text);
                        sd.Exchange = int.Parse(lblExchange.Text);
                        sd.Cube = decimal.Parse(lblCube.Text);
                        if (edp.UpdateShippingDocument(sd))
                        {
                            MessageBox.Show(this, "出厂过磅操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintShipDocReport, "false")))
                            {
                                if (checkBox1.Checked)
                                {
                                    ph.printShipDocConcreteReport(sd);
                                }
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "出厂过磅操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (_FunctionFlag == 2)                 //分车过磅
                    {
                        int addId = edp.AlterTzRelation(tz);
                        if (addId > 0)
                        {
                            MessageBox.Show(this, "分合车过磅操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tz = edp.GetTzRelation(addId);

                            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintFCReport, "false")))
                            {
                                if (checkBox1.Checked)
                                {
                                    ph.printTzConcreteReport(tz);
                                }
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "出厂过磅操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else                                    //剩退料过磅
                    {
                        int type = this.comboBox_type.SelectedIndex;
                        string typeString="";
                        if (type == 1) 
                        {
                            typeString = "RT1";
                        }
                        else if (type == 2)
                        {
                            typeString = "RT2";
                        }
                        else
                        {
                            MessageBox.Show(this, "请选择剩退方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        tz.Type = typeString;
                        int addId = edp.AddTzRelation(tz);
                        if (addId > 0)
                        {
                            MessageBox.Show(this, "退转料操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tz = edp.GetTzRelation(addId);

                            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintTZReport, "false")))
                            {
                                if (checkBox1.Checked)
                                {
                                    ph.printTzConcreteReport(tz);
                                }
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "出厂过磅操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

        //过磅条件合法性验证
        private ShippingDocument ValidTzOrShipMetage(string CarId)
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

            if (_FunctionFlag == 1 && edp.checkCarTzCompleted(CarId.Trim()))             //若为转退料操作，判断该车是否存在未完成的转退料记录
            {
                MessageBox.Show(this, "该车存在未完成的转退料记录，不能再次进行转退料操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (_FunctionFlag == 2 && !edp.checkCarTzCompleted(CarId.Trim()))             //若为分车操作，判断该车是否存在未完成的转退料记录
            {
                MessageBox.Show(this, "该车不存在未完成的转退料记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (_FunctionFlag == 2) 
            {
                CarId = edp.FindSourceCarID(CarId.Trim()).ToString();
                if (CarId == "0") 
                {
                    MessageBox.Show(this, "无法获取源车辆信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            ShippingDocument sd = edp.GetShippingDocumentByCarId(CarId.Trim());

            if (sd == null)
            {
                MessageBox.Show(this, "没有找到该车的发料记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
            {

                //if (edp.ExistSourceShipDocTz(sd.ShipDocID))
                //{
                //    MessageBox.Show(this, "该车对应的源运输单已经存在退转料记录，不能重复添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return null;
                //}

                if ((sd.ConstrengthExchange == null || sd.ConstrengthExchange == 0) && Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.Exchange, 0) == 0)
                {

                    MessageBox.Show(this, "砼强度【" + sd.ConStrength + "】 没有设置换算率，不能进行过磅！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }

            return sd;
            #endregion
            
        }

        private void ResetControlValue()
        {
            lblCarTitle.Text = string.Empty;
            numericUpDown1.Value = 0;
            lblSystemCarWeight.Text = "0" ;
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

    }
}
