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
    public partial class UpdateShippingRecordGH : Form
    {
        private ShippingDocumentGH ShipDoc;
        private IList<User> UserList = new List<User>();
        ERPDataProvider edp = new ERPDataProvider();
        string _CurrentUserId;

        decimal qfl = 0;
        public UpdateShippingRecordGH(ShippingDocumentGH ShipDoc, string CurrentUserId)
        {
            InitializeComponent();
            _CurrentUserId = CurrentUserId;
            ERPDataProvider edp = new ERPDataProvider();
            UserList = edp.GetUserList("UserType='42' and IsUsed=1");
            cbUserName.ValueMember = "UserID";
            cbUserName.DisplayMember = "UserName";
            cbUserName.DataSource = UserList;

            this.ShipDoc = ShipDoc;
            label2.Text = ShipDoc.ShipDocID;
            textBox1.Text = ShipDoc.CarID;
            textBox2.Text = ShipDoc.TotalWeight.ToString();
            textBox3.Text = ShipDoc.CarWeight.ToString();

            txtLjcs.Text = ShipDoc.ProvidedTimes.ToString();
            txtLjfl.Text = ShipDoc.ProvidedCube.ToString();
            tbCustSiloNo.Text = ShipDoc.CustSiloNo == null ? "" : ShipDoc.CustSiloNo.ToString();
            label10.Text = ShipDoc.Weight.ToString();
            label11.Text = ShipDoc.Exchange.ToString();
            label12.Text = ShipDoc.Cube.ToString();
            dateTimePicker1.Text = ShipDoc.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss");
            txtRemark.Text = ShipDoc.Remark;

            cbUserName.Text = ShipDoc.WeightMan;

            IList<ListItem> titleList = edp.GetTitleList();

            titleList.Insert(0, new ListItem());
            comboBox1.DataSource = titleList;
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";

            comboBox1.Text = ShipDoc.Title.ToString();

            qfl = ShipDoc.Cube;
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateShippingRecordGH_Load(object sender, EventArgs e)
        {
            Config c = new Config();
            if (c.isAdminUpdate)
            {
                if (edp.isAdmin(_CurrentUserId))
                {
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                }
                else
                {
                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShippingDocumentGH sd = ShipDoc;
            decimal AddCube = Convert.ToDecimal(label12.Text) - sd.Cube;
            sd.CarID = textBox1.Text;
            sd.TotalWeight = Convert.ToInt32(textBox2.Text);
            sd.CarWeight = Convert.ToInt32(textBox3.Text);
            sd.Weight = sd.TotalWeight - sd.CarWeight;
           
            sd.Cube = Convert.ToDecimal(label12.Text);
            sd.Title = comboBox1.Text;
            sd.DeliveryTime = Convert.ToDateTime(dateTimePicker1.Text);
            sd.ProvidedTimes = Convert.ToInt32(txtLjcs.Text);
            decimal a = sd.Cube - qfl;//修改前后方量差
            sd.ProvidedCube = Convert.ToDecimal(txtLjfl.Text)+a;
            sd.WeightMan = cbUserName.Text.Trim();
            sd.Remark = txtRemark.Text.Trim();
            sd.CustSiloNo = tbCustSiloNo.Text.Trim();

            if (edp.UpdateShippingDocument2GH(sd))
            {
                MessageBox.Show(this, "操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            calculate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void calculate()
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox3.Text != "" && label11.Text != "label11")
            {
                decimal weight = Decimal.Parse(textBox2.Text) - Decimal.Parse(textBox3.Text);
                decimal ex = Decimal.Parse(label11.Text);
                decimal cube = Math.Round(weight / ex, 2);
                label10.Text = weight.ToString();
                label12.Text = cube.ToString();
            }
        }

        

    }
}
