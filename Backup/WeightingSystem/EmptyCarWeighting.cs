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
        public EmptyCarWeighting()
        {
            InitializeComponent();
            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarList();
            itemsCar.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";
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
                    if (carList.Count == 0)
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
            else
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
                }
            }
        }

        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.comboBox1);
            fskb.ShowDialog();
        }
    }
}
