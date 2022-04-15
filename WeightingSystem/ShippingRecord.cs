using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Helpers;
using WeightingSystem.Models;
namespace WeightingSystem
{
    public partial class ShippingRecord : Form
    {
        string _CurrentUserId;
        public ShippingRecord(string CurrentUserId)
        {
            InitializeComponent();
            _CurrentUserId = CurrentUserId;
            this.beginTime.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

            #region 初始化下拉框数据
            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarList();
            itemsCar.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";
            this.cbCar.Text = string.Empty;

            IList<ListItem> itemsConstrength = edp.getConstrengthList();
            itemsConstrength.Insert(0, new ListItem());
            cbConstrength.DataSource = itemsConstrength;
            this.cbConstrength.DisplayMember = "Text";
            this.cbConstrength.ValueMember = "Value";
            this.cbConstrength.Text = string.Empty;

            #endregion

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ERPDataProvider edp = new ERPDataProvider();
            dataGridViewX1.AutoGenerateColumns = false;
            IList<ShippingDocument> shippingList = edp.getShippingRecord(beginTime.Value,endTime.Value,cbCar.Text,cbConstrength.Text);
            dataGridViewX1.DataSource = shippingList;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX1.Columns[e.ColumnIndex].Name == "Print")
            {
                ShippingDocument currentShipDoc = (ShippingDocument)dataGridViewX1.SelectedRows[0].DataBoundItem;
                PublicHelper ph = new PublicHelper();

                ERPDataProvider edp = new ERPDataProvider();
                ShippingDocument sd = edp.GetShippingDocumentByShipId(currentShipDoc.ShipDocID);
                sd.ProvidedCube_Dis = sd.ProvidedCube;
                sd.ProvidedTimes_Dis = sd.ProvidedTimes;
                ph.printShipDocConcreteReport(sd);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Config c = new Config();
            //if (c.isAdminUpdate)
            //{
            //    ERPDataProvider erp = new ERPDataProvider();
            //    if (!erp.isAdmin(_CurrentUserId))
            //    {
            //        MessageBox.Show("此账号无修改权限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}
            ShippingDocument currentShipDoc = (ShippingDocument)dataGridViewX1.SelectedRows[0].DataBoundItem;
            UpdateShippingRecord usr = new UpdateShippingRecord(currentShipDoc,_CurrentUserId);
            usr.ShowDialog();
        }
    }
}
