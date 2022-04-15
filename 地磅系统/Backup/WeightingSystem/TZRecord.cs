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
    public partial class TZRecord : Form
    {
        public TZRecord()
        {
            InitializeComponent();
            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> itemsCar = edp.getCarList();
            itemsCar.Insert(0, new ListItem());
            cbCar.DataSource = itemsCar;
            this.cbCar.DisplayMember = "Text";
            this.cbCar.ValueMember = "Value";
            
            
            IList<ListItem> itemsConstrength = edp.getConstrengthList();
            itemsConstrength.Insert(0, new ListItem());
            cbConstrength.DataSource = itemsConstrength;
          
            this.cbConstrength.DisplayMember = "Text";
            this.cbConstrength.ValueMember = "Value";
            this.cbConstrength.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridViewX1.AutoGenerateColumns = false;
            ERPDataProvider edp = new ERPDataProvider();
            IList<TZrelation> tzRecordList = edp.getTZRecord(beginTime.Value, endTime.Value, cbCar.Text, cbConstrength.Text, txtShipDocID.Text);
            dataGridViewX1.DataSource = tzRecordList;
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX1.Columns[e.ColumnIndex].Name == "Print")
            {
                TZrelation currentTzRelation = (TZrelation)dataGridViewX1.SelectedRows[0].DataBoundItem;
                PublicHelper ph = new PublicHelper();
                ph.printTzConcreteReport(currentTzRelation);

            }
        }
    }
}
