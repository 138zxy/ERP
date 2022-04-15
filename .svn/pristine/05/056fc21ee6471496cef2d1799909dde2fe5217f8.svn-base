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
    public partial class FHWeight : Form
    {
        static ILog log = LogManager.GetLogger(typeof(FHWeight));

        string _CurrentUserId;

        Config c = new Config();
        ERPDataProvider erp = new ERPDataProvider();
        public FHWeight(string weightname, string CurrentUserId)
        {
            InitializeComponent();
            _CurrentUserId = CurrentUserId;
            lbWeightName.Text = weightname;

            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarList();
            IList<ListItem> titleList = edp.GetTitleList();
            itemsCar.Insert(0, new ListItem());
            titleList.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";

            lblFunctionTitle.Text = "分合车过磅";
            this.Text = "分合车过磅";

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
            TZrelation tz = ValidFHMetage(cbCar.Text);
            if (tz == null)
            {
                return;
            }

            ERPDataProvider edp = new ERPDataProvider();
            lblSystemCarWeight.Text = edp.getCarEmptyWeight(cbCar.Text.Trim()).ToString();
            lblCarTitle.Text = cbCar.Text;

            int Exchange = Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.Exchange, 0);

            lblExchange.Text = Exchange.ToString();

            tTZRalationID.Text = tz.TZRalationID;
            tType.Text = tz.IsLock==1 ? "分车" : "合车";
            tCube.Text = tz.Cube.ToString();

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
                int TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lblWeight.Text);
                int CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                decimal LastWeight = TotalWeight - CarWeight;
                lblLastWeight.Text = LastWeight.ToString("0");
                decimal Cube = Math.Round(LastWeight / (Convert.ToInt32(lblExchange.Text) == 0 ? 1 : Convert.ToInt32(lblExchange.Text)), 1);
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
            if (Convert.ToDecimal(numericUpDown2.Value.ToString()) != 0 && Convert.ToDecimal(numericUpDown2.Value.ToString()) < Convert.ToDecimal(lblWeight.Text.Trim()))
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

                if (DialogResult.OK == MessageBox.Show("是否确定该操作？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    TZrelation tz = new TZrelation();
                    tz.Cube = decimal.Parse(lblCube.Text);
                    tz.TZRalationID = tTZRalationID.Text;
                    //毛重
                    tz.TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lblWeight.Text);
                    //皮重
                    tz.CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
                    //净重
                    tz.Weight = int.Parse(lblLastWeight.Text);
                    tz.Exchange = int.Parse(lblExchange.Text);
                    tz.Type = "";


                    PublicHelper ph = new PublicHelper();

                    tz.WeightName = lbWeightName.Text;
                    int addId = edp.AlterTzRelation(tz);
                    if (addId > 0)
                    {
                        MessageBox.Show(this, "分合车过磅操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "分合车过磅操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            edp.checkCarTzCompleted(CarId.Trim());

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
        private TZrelation ValidFHMetage(string CarId)
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

            //edp.checkCarTzCompleted(CarId.Trim());

            TZrelation tz = edp.GetFHTzRelationByCarId(CarId.Trim());

            if (tz == null)
            {
                MessageBox.Show(this, "没有找到该车的分合车记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return tz;
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

            tCube.Text = string.Empty;
            tTZRalationID.Text = string.Empty;
            tType.Text = string.Empty;

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
                int TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lblWeight.Text);
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
