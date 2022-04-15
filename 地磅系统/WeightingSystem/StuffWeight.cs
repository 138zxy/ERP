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

namespace WeightingSystem
{
    public partial class StuffWeight : Form
    {
        static ILog log = LogManager.GetLogger(typeof(StuffWeight));

        string _CurrentUserId;
        List<Car> carList=new List<Car>();
        Config c = new Config();
        ERPDataProvider erp = new ERPDataProvider();

        public StuffWeight(string weightname, string CurrentUserId)
        {
            InitializeComponent(); 
            _CurrentUserId = CurrentUserId;
            lbWeightName.Text = weightname;
            #region 下拉框绑值

            //供货单位绑定com表
            IList<ListIntItem> itemsCmp = erp.GetSaleCmpList();
            itemsCmp.Insert(0, new ListIntItem());
            cbCmp.DataSource = itemsCmp;
            this.cbCmp.DisplayMember = "Text";
            this.cbCmp.ValueMember = "Value";

            //收货单位绑定
            string SupplyType = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SupplyType, "");
            IList<ListItem> itemsSupply = erp.GetStuffSupplyList(SupplyType);
            itemsSupply.Insert(0, new ListItem());
            cbPJ.DataSource = itemsSupply;
            this.cbPJ.DisplayMember = "Text";
            this.cbPJ.ValueMember = "Value";

            //材料名称绑定
            IList<ListItem> itemsGoods = erp.GetGHGoods();
            itemsGoods.Insert(0, new ListItem());
            cbGoods.DataSource = itemsGoods;
            this.cbGoods.DisplayMember = "Text";
            this.cbGoods.ValueMember = "Value";

            //货车-获取本地车辆列表
            LocalDataProvider ldp = new LocalDataProvider();
            IList<Car> List = ldp.GetCarList(" 1=1 order by CarNo ");
            foreach (var item in List)
	        {
                carList.Add(item);
            }
            List.Insert(0, new Car());
            cbCar.DataSource = List;
            this.cbCar.DisplayMember = "CarNo";
            this.cbCar.ValueMember = "CarNo";


            //若设置材料出厂车辆类型
            //string StuffCarType = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.StuffCarType, "");
            //IList<ListItem> itemsCar = erp.getStuffCarList(StuffCarType);
            //itemsCar.Insert(0, new ListItem());
            //cbCar.DataSource = itemsCar;
            //this.cbCar.DisplayMember = "Text";
            //this.cbCar.ValueMember = "Value";
            #endregion
            if (c.isAdminUpdate)
            {
                if (erp.isAdmin(_CurrentUserId))
                {
                    radioButton1.Enabled = true;
                    radioButton4.Enabled = true;
                }
                else
                {
                    radioButton1.Enabled = false;
                    radioButton4.Enabled = false;
                }
            }
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbCar.Text.Trim()))
                {
                    MessageBox.Show(this, "车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(cbGoods.Text.Trim()))
                {
                    MessageBox.Show(this, "材料名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Convert.ToDecimal(numericUpDown2.Value) <= 0 && Convert.ToDecimal(lblWeight.Text) <= 0)
                {
                    MessageBox.Show(this, "毛重不能小于等于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Convert.ToDecimal(numericUpDown1.Value) <= 0 && Convert.ToDecimal(lblSystemCarWeight.Text) <= 0)
                {
                    MessageBox.Show(this, "皮重不能小于等于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Convert.ToDecimal(lblLastWeight.Text)<=0)
                {
                    MessageBox.Show(this, "净重不能小于等于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                StuffSale entity = new StuffSale();
                entity.DeliveryTime = DateTime.Now;
                entity.CompName = cbCmp.Text;//供货单位
                entity.CompanyID = cbCmp.SelectedValue.ToString();//收货单位
                entity.SupplyID = cbPJ.Text;//收货单位
                entity.StuffName = cbGoods.Text;//货物名称
                entity.StuffID = cbGoods.SelectedValue.ToString();//货物名称
                entity.CarID =cbCar.Text.ToString();//车辆名
                entity.CarNo = cbCar.Text;//车辆名
                entity.Remark = textRemark.Text;//备注（填单价）
                entity.TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : Convert.ToInt32(lblWeight.Text);//毛重
                entity.CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);//皮重
                entity.Weight = Convert.ToInt32(lblLastWeight.Text);//净重
                entity.Builder = _CurrentUserId;
                entity.BuildTime = DateTime.Now;
                entity.WeightMan = _CurrentUserId;
                entity.WeightName = lbWeightName.Text;
                entity.SiloID =cbSilo.SelectedValue==null?"": cbSilo.SelectedValue.ToString();
                DateTime ArriveTime =DateTime.Now;
                for (int i = 0; i < carList.Count; i++)
                {
                    if (cbCar.Text.ToString().Trim() == carList[i].CarNo)
                    {
                        ArriveTime = carList[i].MarkTime;
                    }
                }
                entity.ArriveTime = ArriveTime;
                PublicHelper ph = new PublicHelper();
                #region 获取销售单号
                //entity.StuffSaleID = ph.getMetageNo(entity);
                AutoGenerateHelper agh = new AutoGenerateHelper();
                ERPDataProvider edp = new ERPDataProvider();
                Config c = new Config();
                entity.IDPrefix = agh.GetPrefix(c.prefixPat_Sale);
                int NextValue = agh.getLastValue_Sale("StuffSale", entity.IDPrefix);
                string MetageID = agh.GenerateID_Sale(NextValue);
                int incrementCount = 0;
                while (edp.checkExistStuffSaleID(MetageID) > 0)//校正销售单号，使得传送到ERP内的磅单号绝对不重复
                {
                    NextValue += 1;
                    incrementCount++;
                    MetageID = agh.GenerateID_Sale(NextValue);
                }
                entity.NextValue = NextValue + 1;
                entity.StuffSaleID = MetageID;

                #endregion

                if (erp.AddStuffSale(entity) > 0)
                {
                    MessageBox.Show(this, "材料出厂过磅成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (checkBox1.Checked)
                    {
                        ph.printStuffSaleReport(entity);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "材料出厂过磅操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                log.Error("材料出厂过磅操作失败:" + ex.Message);
                MessageBox.Show("错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                numericUpDown1.Enabled = true;
                label2.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
                label2.Enabled = false;
            }

            //if (radioButton1.Checked)
            //{
            //    string CanModifyWeight = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.CanModifyWeight, "");
            //    if (CanModifyWeight != "false")
            //    {
            //        lblSystemCarWeight.Enabled = true;
            //        numericUpDown1.Enabled = true;
            //        label2.Enabled = false;
            //    }
            //    else
            //    {
            //        lblSystemCarWeight.Enabled = false;
            //        numericUpDown1.Enabled = false;
            //        label2.Enabled = true;
            //    }
            //}
            //else
            //{
            //    lblSystemCarWeight.Enabled = true;
            //    numericUpDown1.Enabled = false;
            //    label2.Enabled = false;
            //}
         
            CalcCube();
        }
        private void lblWeight_TextChanged(object sender, EventArgs e)
        {
            labelSysTotalWeight.Text = lblWeight.Text;
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
        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.cbCar);
            fskb.ShowDialog();
        }

        private void CalcCube()
        {
            int TotalWeight = radioButton4.Checked ? Convert.ToInt32(numericUpDown2.Value) : int.Parse(lblWeight.Text);
            int CarWeight = radioButton1.Checked ? Convert.ToInt32(numericUpDown1.Value) : int.Parse(lblSystemCarWeight.Text);
            decimal LastWeight = TotalWeight - CarWeight;
            lblLastWeight.Text = LastWeight.ToString("0");
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string EmptyWeight = "";
            for (int i = 0; i < carList.Count; i++)
            {
                if (cbCar.Text.ToString().Trim()==carList[i].CarNo)
                {
                    EmptyWeight = carList[i].Weight.ToString();
                }
            }
            lblSystemCarWeight.Text = EmptyWeight;
            //lblSystemCarWeight.Text = erp.getStuffCarEmptyWeight(cbCar.Text.ToString().Trim()).ToString();
            if (radioButton2.Checked)
            {
                CalcCube();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                numericUpDown2.Enabled = true;
                label8.Enabled = true;
            }
            else
            {
                numericUpDown2.Enabled = false;
                label8.Enabled = false;
            }
            //if (radioButton4.Checked)
            //{
            //    string CanModifyWeight = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.CanModifyWeight, "");
            //    if (CanModifyWeight != "false")
            //    {
            //        labelSysTotalWeight.Enabled = false;
            //        labelSysTotalWeight.Text = lblWeight.Text;
            //        numericUpDown2.Enabled = true;
            //        label8.Enabled = true;
            //    }
            //}
            //else
            //{
            //    labelSysTotalWeight.Enabled = true;
            //    numericUpDown2.Enabled = false;
            //    label8.Enabled = false;
            //}
            CalcCube();
        }

        private void cbCar_DropDown(object sender, EventArgs e)
        {

        }

        private void cbCar_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cbCar_KeyUp(object sender, KeyEventArgs e)
        {
            string str = cbCar.Text;
            List<Car> list = carList.FindAll(a => a.CarNo.Contains(str));
            list.Insert(0, new Car());
            cbCar.DataSource = list;
            this.cbCar.DisplayMember = "CarNo";
            this.cbCar.ValueMember = "CarNo";
            cbCar.Text = str;
            cbCar.DroppedDown = true;
            //cbCar.Focus();
        }

        private void cbGoods_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbGoods.SelectedValue==null)
            {
                return;
            }
            string stuffid = cbGoods.SelectedValue.ToString();
            IList<Silo> _siloList = erp.GetSiloList(stuffid);
            Silo EmptySilo = new Silo();
            EmptySilo.SiloID = string.Empty;
            EmptySilo.SiloName = string.Empty;
            _siloList.Insert(0, EmptySilo);
            cbSilo.DisplayMember = "SiloName";
            cbSilo.ValueMember = "SiloID";
            cbSilo.DataSource = _siloList;
        }
    }
}
