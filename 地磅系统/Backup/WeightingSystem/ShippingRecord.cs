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
        public ShippingRecord()
        {
            InitializeComponent();
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
                ph.printShipDocConcreteReport(currentShipDoc);
                
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
