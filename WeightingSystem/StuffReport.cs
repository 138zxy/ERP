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
    public partial class StuffReport : Form
    {
        public StuffReport()
        {
            InitializeComponent();
            dataGridView2.AutoGenerateColumns = false; 

            PublicHelper ph = new PublicHelper();
            List<ListItem> itemList = ph.bindStuffType();
            ListItem li = new ListItem();
            li.Value = "";
            li.Text = "";
            itemList.Insert(0,li);
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";
            comboBox1.DataSource = itemList;


            this.cbSupply.DisplayMember = "Text"; 
            this.cbSupply.ValueMember = "Value";
            ERPDataProvider provider = new ERPDataProvider();

            IList<ListItem>  list = provider.GetSupplyList();
            ListItem empty = new ListItem();
            empty.Text = "";
            empty.Value = "";
            list.Insert(0, empty);
            this.cbSupply.DataSource = list;

            comboBox1.SelectedValue = string.Empty;
            comboBox2.SelectedValue = string.Empty;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            

                ERPDataProvider edp = new ERPDataProvider();
                IList<StuffInfo> StuffInfoList = new List<StuffInfo>();
                if (comboBox1.SelectedValue != null)
                {
                    if (comboBox1.SelectedValue.ToString() == "OTHER")
                    {
                        StuffInfoList = edp.getStuffInfo("FinalStuffType not in('CA','SHI','SHA','FA','WA','CE','AIR','ADM')");
                    }
                    else
                    {
                        StuffInfoList = edp.getStuffInfo("FinalStuffType ='" + comboBox1.SelectedValue.ToString() + "'");
                    }


                    List<ListItem> itemList = new List<ListItem>();
                   
                    foreach (StuffInfo si in StuffInfoList)
                    {
                        ListItem li = new ListItem();
                        li.Value = si.StuffID;
                        li.Text = "[" + si.StuffName + "][" + si.Spec + "][" + si.SupplyName + "]";
                        itemList.Add(li);
                    }
                    comboBox2.ValueMember = "Value";
                    comboBox2.DisplayMember = "Text";
                    if (itemList.Count > 0)
                    {
                        ListItem emptyli = new ListItem();
                        emptyli.Value = string.Empty;
                        emptyli.Text = string.Empty;
                        itemList.Insert(0, emptyli);
                    }
                    comboBox2.DataSource = itemList;
                    
                    
                }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            decimal TotalNum = 0;
            ERPDataProvider edp = new ERPDataProvider();
            string sqlWhere = "1=1";
            if (comboBox1.SelectedValue .ToString() != string.Empty)
            {
                sqlWhere += " and StuffType.FinalStuffType='" + comboBox1.SelectedValue.ToString() + "'";
                if (comboBox2.SelectedValue.ToString() != string.Empty)
                {
                    sqlWhere += " and StuffInfo.stuffid='" + comboBox2.SelectedValue + "'";
                }
            }

            
            if (cbSupply.SelectedValue.ToString() != string.Empty)
            {
                sqlWhere += " and StuffSupply.SupplyID = '" + cbSupply.SelectedValue + "'";
            }
            
           

            IList<StuffInfo> siList = edp.getStuffSupplyQuery(sqlWhere,"si.OutDate between '"+dateTimePicker1.Value+"' and '"+dateTimePicker2.Value+"'");
            
            dataGridView2.DataSource = siList;
            foreach (StuffInfo si in siList)
            {
                TotalNum += si.totalNum;
                count += si.totalCount;
            }
            label5.Text = "累计入库量：" + TotalNum.ToString() + "T  " + "累计车次：" + count.ToString() + "车";
                
        }
    }
}
