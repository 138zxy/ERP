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
    public partial class EmptyCarWeighting : Form
    {
        ILog log = LogManager.GetLogger(typeof(EmptyCarWeighting));
        List<Car> carlists = new List<Car>();
        public EmptyCarWeighting()
        {
            InitializeComponent();
            //搅拌车-获取ERP车辆列表
            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarList();
            itemsCar.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";

            //货车-获取本地车辆列表
            LocalDataProvider ldp = new LocalDataProvider();
            IList<Car> List = ldp.GetCarList(" 1=1 order by CarNo ");
            foreach (var item in List)
            {
                carlists.Add(item);
            }
            List.Insert(0, new Car());
            comboBox1.DataSource = List;
            this.comboBox1.DisplayMember = "CarNo";
            this.comboBox1.ValueMember = "CarNo";

            radioButton2_CheckedChanged(null, null);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = true;
                btnCarNoSoftKeyboard.Enabled = true;
                cbCar.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = false;
                btnCarNoSoftKeyboard.Enabled = false;
                cbCar.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                if (comboBox1.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入车号");
                    comboBox1.Focus();
                    return;
                }

                if (DialogResult.OK == MessageBox.Show("是否执行【空车过磅】？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    LocalDataProvider ldp = new LocalDataProvider();
                    IList<Car> carList = ldp.GetCarList("CarNo='" + comboBox1.Text.Trim() + "'");
                    if (carList.Count == 0)//不存在则增加
                    {
                        Car car = new Car();
                        car.CarNo = comboBox1.Text.Trim();
                        car.Weight = int.Parse(lblWeight.Text);
                        car.MarkTime = DateTime.Now;
                        ldp.addCar(car);

                    }
                    else if (carList[0].Weight != int.Parse(lblWeight.Text))
                    {
                        carList[0].Weight = int.Parse(lblWeight.Text);
                        carList[0].MarkTime = DateTime.Now;
                        ldp.updateCar(carList[0], carList[0].CarNo);
                    }
                    log.Info("车辆：" + comboBox1.Text.Trim() + "空车过磅，重量：" + lblWeight.Text + "Kg");
                    MessageBox.Show("过磅完成！");
                    this.Close();
                  
                }   
            }
            else//搅拌车
            {
                if (cbCar.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入车号");
                    comboBox1.Focus();
                    return;
                }
                if (DialogResult.OK == MessageBox.Show("是否执行【空车过磅】？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    ERPDataProvider edp = new ERPDataProvider();
                    CarEmptyWeight cew = new CarEmptyWeight();
                    cew.CarID = cbCar.Text;
                    cew.Weight = int.Parse(lblWeight.Text);
                    cew.Builder = MainForm.CurrentUserName;
                    cew.BuildTime = DateTime.Now;
                    if (edp.AddCarEmptyWeight(cew) > 0)
                    {
                        log.Info("车辆：" + cbCar.Text.Trim() + "空车过磅，重量：" + lblWeight.Text + "Kg");
                        MessageBox.Show("过磅完成！");
                        this.Close();
                    }

                    if(ckUpdateArriveTime.Checked)//更新回厂时间
                    {
                        ShippingDocument sd = ValidTzOrShipMetage(cbCar.Text);
                        if (sd == null)
                        {
                            return;
                        }

                        //decimal HourInterval = edp.getCarHourInterval();
                        //if (sd.ServerTime != null)
                        //{
                        //    TimeSpan ts = Convert.ToDateTime(sd.ServerTime).Subtract(Convert.ToDateTime(sd.ProduceDate));
                        //    if (Convert.ToDecimal(ts.TotalHours) > HourInterval)                 //若找到的最近发货时间超过了ERP设置的回厂时间差
                        //    {
                        //        if (DialogResult.Cancel == MessageBox.Show("找到了最近的发货记录，但是该车回厂(出厂)时间已经超过系统设置时间【" + HourInterval.ToString() + "】小时,是否继续进行操作？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        //        {
                        //            return;
                        //        }
                        //    }
                        //}
                        //更新回厂时间为当前时间                        
                        bool issuccee=edp.UpdateArriveTime(sd.ShipDocID);
                        if (issuccee)
                        {
                            MessageBox.Show("更新回厂时间失败！");
                        }
                    }
                }
            }
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
                return null;
            }

            ERPDataProvider edp = new ERPDataProvider();
            
            ShippingDocument sd = edp.GetShippingDocumentByCarId(CarId.Trim());

            if (sd == null)
            {
                MessageBox.Show(this, "没有找到该车的发料记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return sd;
            #endregion

        }

        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.comboBox1);
            fskb.ShowDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2_CheckedChanged(null,null);
        }

        LocalDataProvider ldp = new LocalDataProvider();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {          
            IList<Car> carList = ldp.GetCarList("CarNo='" + comboBox1.Text.Trim() + "'");
            if (carList.Count == 0)//不存在则增加
            {
                lbPreWeight.Text = "0";
                lbC.Text = "0";
            }
            else 
            {
                lbPreWeight.Text = carList[0].Weight.ToString();
                decimal c = Convert.ToDecimal(lblWeight.Text) - carList[0].Weight;
                lbC.Text = c.ToString();
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string str = comboBox1.Text;
            List<Car> list = carlists.FindAll(a => a.CarNo.Contains(str));
            list.Insert(0, new Car());
            comboBox1.DataSource = list;
            this.comboBox1.DisplayMember = "CarNo";
            this.comboBox1.ValueMember = "CarNo";
            comboBox1.Text = str;
        }
        private void cbCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ERPDataProvider edp = new ERPDataProvider();
            string CarWeight = edp.getCarWeightByCarID(cbCar.Text.Trim());
            lbPreWeight.Text = CarWeight;

            decimal newCarWeight = Convert.ToDecimal(lblWeight.Text);
            decimal cha = Math.Abs(Convert.ToDecimal((newCarWeight - Convert.ToDecimal(CarWeight))));
            lbC.Text = (cha).ToString();
        }
    }
}
