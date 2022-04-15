using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Models;
using WeightingSystem.Helpers;

namespace WeightingSystem
{
    public partial class CarManage : Form
    {
        public CarManage()
        {
            InitializeComponent();
            refreshCarList();

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox)  )
                {
                    TextBox tx = (TextBox)c;
                    c.TextChanged += new EventHandler(valueChange); 
                    
                }
                else if (c.GetType() == typeof(DateTimePicker))
                {
                    DateTimePicker dt = (DateTimePicker)c;
                    dt.ValueChanged += new EventHandler(valueChange);
                }

            }

        }


        private void valueChange(object sender, EventArgs e)
        {
            if ( !btnUpdateCar.Enabled && dgvCar.SelectedRows.Count == 1)
            {
                btnAddCar.Enabled = true;
                btnUpdateCar.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard keyboard = new FormSoftKeyBoard(this.txtCarNo);
            keyboard.TopMost = true;
            keyboard.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtCarNo.Text.Trim() == string.Empty)
            {
               MessageBox.Show(this, "请输入车号！", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
               return;
            }
            int carWeight = 0;
            Car car = new Car();
            car.CarNo = txtCarNo.Text.Trim();
            car.Driver1 = txtDriver1.Text.Trim();
            car.Driver2 = txtDriver2.Text.Trim();
            car.Driver3 = txtDriver3.Text.Trim();
            car.Driver4 = txtDriver4.Text.Trim();

            LocalDataProvider ldp = new LocalDataProvider();
            IList<Car> CarList = ldp.GetCarList("CarNo ='"+car.CarNo+"'");
            if (CarList.Count > 0)
            {
                MessageBox.Show("已经存在该车号，请更改新的车号","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (txtCarWeight.Text.Trim() != string.Empty)
            {
                carWeight = Convert.ToInt32(txtCarWeight.Text.Trim());
            }
            car.Weight = carWeight;
            car.MarkTime = dtMarkTime.Value;
            
            ldp.addCar(car);
            refreshCarList();
            
        }


        private void refreshCarList()
        {
            LocalDataProvider ldp = new LocalDataProvider();
            this.dgvCar.AutoGenerateColumns = false;
            this.dgvCar.DataSource = ldp.GetCarList("1=1");
        }

        private void dgvCar_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if ((e.Row.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
              
                //绑定行数据到表单

                Car car = (Car)e.Row.DataBoundItem;
                if (car != null)
                {
                    txtCarNo.Text = car.CarNo;
                    txtCarWeight.Text = car.Weight.ToString();
                    txtDriver1.Text = car.Driver1;
                    txtDriver2.Text = car.Driver2;
                    txtDriver3.Text = car.Driver3;
                    txtDriver4.Text = car.Driver4;
                    dtMarkTime.Value = car.MarkTime;
                }
               
            }
            
            btnUpdateCar.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (dgvCar.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录", "信息提示");
                return;
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("是否删除此条记录？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    string CarNo = dgvCar.SelectedRows[0].Cells["CarNo"].Value.ToString();
                    LocalDataProvider ldp = new LocalDataProvider();
                    ldp.delCar(CarNo);
                    refreshCarList();
                }
            }
        }

        private void btnUpdateCar_Click(object sender, EventArgs e)
        {
            if (txtCarNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show(this, "请输入车号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string CarNo = dgvCar.SelectedRows[0].Cells["CarNo"].Value.ToString();
            LocalDataProvider ldp = new LocalDataProvider();
            if (CarNo != txtCarNo.Text.Trim())
            { 
                int count = ldp.CheckExistCarNo(txtCarNo.Text.Trim());
                if (count > 0)
                {
                    MessageBox.Show("输入的车号已经存在！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            Car car = new Car();
            car.CarNo = txtCarNo.Text.Trim();
            car.Driver1 = txtDriver1.Text.Trim();
            car.Driver2 = txtDriver2.Text.Trim();
            car.Driver3 = txtDriver3.Text.Trim();
            car.Driver4 = txtDriver4.Text.Trim();
            int carWeight = 0;
            if (!string.IsNullOrEmpty(txtCarWeight.Text.Trim()))
            {
                carWeight = int.Parse(txtCarWeight.Text.Trim());
            }
            car.Weight = carWeight;
            car.MarkTime = dtMarkTime.Value;
            ldp.updateCar(car, CarNo);
            MessageBox.Show("车辆修改成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshCarList();
        }
    }
}
