using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport;
using WeightingSystem.Helpers;
using WeightingSystem.Models;
namespace WeightingSystem
{
    public partial class QueryDataForm : Form
    {
        public QueryDataForm()
        {
            InitializeComponent();
            BindCombox();
        }




        private void BindCombox()
        {
            ERPDataProvider provider = new ERPDataProvider();

            this.cbStuff.DisplayMember = "Text";
            this.cbStuff.ValueMember = "Value";

            IList<ListItem> list = provider.GetStuffList();
            ListItem empty = new ListItem();
            empty.Text = "";
            empty.Value = "";
            list.Insert(0, empty);
            this.cbStuff.DataSource = list;

            this.cbSupply.DisplayMember = "Text";
            this.cbSupply.ValueMember = "Value";

            list = provider.GetSupplyList();
            list.Insert(0, empty);
            this.cbSupply.DataSource = list;

       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                this.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            //this.grid1.AutoGenerateColumns = false;
            this.gird2.AutoGenerateColumns = false;
            ERPDataProvider edp = new  ERPDataProvider();

            string CarNo = this.carId.Text.Trim();
             string StuffId = this.cbStuff.SelectedValue.ToString();
             string SupplyId = this.cbSupply.SelectedValue.ToString();
             
             string BaleId = "";

             if (!(string.IsNullOrEmpty(this.cbBaleId.Text)))
             {

                 BaleId = this.cbBaleId.SelectedValue.ToString();
            
             }


             string BeginTime = this.beginTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
             string EndTime = this.endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"); 
             IList<StuffIn> siList = edp.GetStuffInListDetail(CarNo, BaleId, StuffId, SupplyId,  BeginTime, EndTime);
             this.gird2.DataSource = siList;
             int TotalCount = 0 ;
             decimal TotalWeight = 0;
             decimal TotalCarWeight = 0;
             decimal TotalInNum = 0;
             
             foreach (StuffIn si in siList)
             {
                 TotalCount += 1;
                 TotalWeight += si.TotalNum;
                 TotalCarWeight += si.CarWeight;
                 TotalInNum += si.InNum;
             }
             label8.Text =   TotalCount.ToString();
             label9.Text =  (TotalWeight/1000).ToString("0.00");
             label11.Text = (TotalCarWeight/1000).ToString("0.00");
             label13.Text = (TotalInNum/1000).ToString("0.00");
             
            
        }

        private void cbStuff_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbStuff.SelectedValue != null)
            {

                ERPDataProvider provider = new ERPDataProvider();


                this.cbBaleId.DisplayMember = "SiloName";
                this.cbBaleId.ValueMember = "SiloID";
                Silo empty = new Silo();
                empty.SiloID = "";
                empty.SiloName = "";
                IList<Silo> list = provider.GetSiloList(cbStuff.SelectedValue.ToString());
                list.Insert(0, empty);
                this.cbBaleId.DataSource = list;
                this.cbBaleId.Refresh();
            }
        }
 

        private void gird2_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            StuffIn currentStuffIn = (StuffIn)gird2.SelectedRows[0].DataBoundItem;
            PicViewBox pvb = new PicViewBox(currentStuffIn);
            pvb.ShowDialog();
        }

        private void gird2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gird2.Columns[e.ColumnIndex].Name == "Print")
            {
                StuffIn currentStuffIn = (StuffIn)gird2.SelectedRows[0].DataBoundItem;
                int EnablePrintStuffName = Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.EnablePrintStuffName, 0);
                if (EnablePrintStuffName == 1)
                {
                    PrintStuffName psn = new PrintStuffName(currentStuffIn);
                    psn.ShowDialog();
                }
                else
                {
                    PublicHelper ph = new PublicHelper();
                    ph.Print(currentStuffIn);
                }
            }
        }

       




      
    }
}
