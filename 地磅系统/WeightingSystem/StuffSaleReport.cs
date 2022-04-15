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
    public partial class StuffSaleReport : Form
    {
        IList<Car> carList;
        ERPDataProvider erp = new ERPDataProvider();
        public string _CurrentUserId;
        public StuffSaleReport(string CurrentUserId)
        {
            InitializeComponent(); 
            _CurrentUserId = CurrentUserId;
            Grid2.AutoGenerateColumns = false; 
            PublicHelper ph = new PublicHelper();
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            //货车-获取本地车辆列表
            LocalDataProvider ldp = new LocalDataProvider();
            carList = ldp.GetCarList(" 1=1 order by CarNo ");
            cbCar.DataSource = carList;
            this.cbCar.DisplayMember = "CarNo";
            this.cbCar.ValueMember = "CarNo";

            //供货单位列表
            IList<ListIntItem> itemsCmp = erp.GetSaleCmpList();
            itemsCmp.Insert(0, new ListIntItem());
            cbComName.DataSource = itemsCmp;
            this.cbComName.DisplayMember = "Text";
            this.cbComName.ValueMember = "Value";

            //材料名称列表
            IList<ListItem> itemsGoods = erp.GetGHGoods();
            itemsGoods.Insert(0, new ListItem());
            cbStuffName.DataSource = itemsGoods;
            this.cbStuffName.DisplayMember = "Text";
            this.cbStuffName.ValueMember = "Value";

            //收货单位
            string SupplyType = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SupplyType, "");
            IList<ListItem> itemsSupply = erp.GetStuffSupplyList(SupplyType);
            itemsSupply.Insert(0, new ListItem());
            cbSupply.DataSource = itemsSupply;
            this.cbSupply.DisplayMember = "Text";
            this.cbSupply.ValueMember = "Value";

            cbCar.SelectedValue = string.Empty;
            cbStuffName.SelectedValue = string.Empty;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
                ERPDataProvider edp = new ERPDataProvider();
                IList<StuffInfo> StuffInfoList = new List<StuffInfo>();
                if (cbCar.SelectedValue != null)
                {
                    if (cbCar.SelectedValue.ToString() == "OTHER")
                    {
                        StuffInfoList = edp.getStuffInfo("FinalStuffType not in('CA','SHI','SHA','FA','WA','CE','AIR','ADM')");
                    }
                    else
                    {
                        StuffInfoList = edp.getStuffInfo("FinalStuffType ='" + cbCar.SelectedValue.ToString() + "'");
                    }
                    List<ListItem> itemList = new List<ListItem>();
                    foreach (StuffInfo si in StuffInfoList)
                    {
                        ListItem li = new ListItem();
                        li.Value = si.StuffID;
                        li.Text = "[" + si.StuffName + "][" + si.Spec + "][" + si.SupplyName + "]";
                        itemList.Add(li);
                    }
                    cbStuffName.ValueMember = "Value";
                    cbStuffName.DisplayMember = "Text";
                    if (itemList.Count > 0)
                    {
                        ListItem emptyli = new ListItem();
                        emptyli.Value = string.Empty;
                        emptyli.Text = string.Empty;
                        itemList.Insert(0, emptyli);
                    }
                    cbStuffName.DataSource = itemList;
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlWhere = " where 1=1";
            if (cbCar.SelectedValue  != null)
            {
                sqlWhere += " and CarNo='" + cbCar.SelectedValue.ToString() + "'";
            }
            if (cbSupply.SelectedValue != null)
            {
                sqlWhere += " and SupplyID = '" + cbSupply.SelectedValue.ToString() + "'";
            }
            if (cbComName.SelectedValue.ToString() != "0")
            {
                sqlWhere += " and CompanyID = '" + cbComName.SelectedValue.ToString() + "'";
            }
            if (cbStuffName.SelectedValue != null)
            {
                sqlWhere += " and StuffID = '" + cbStuffName.SelectedValue.ToString() + "'";
            }
            sqlWhere += " and DeliveryTime between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value + "'";
            IList<StuffSale> siList = erp.getStuffSaleQuery(sqlWhere);
            Grid2.DataSource = siList;
            //int count = 0;
            //decimal TotalNum = 0;
            //foreach (StuffInfo si in siList)
            //{
            //    TotalNum += si.totalNum;
            //    count += si.totalCount;
            //}
            //label5.Text = "累计入库量：" + TotalNum.ToString() + "T  " + "累计车次：" + count.ToString() + "车";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid2.Columns[e.ColumnIndex].Name == "Print")//打印
            {
                StuffSale entity = (StuffSale)Grid2.SelectedRows[0].DataBoundItem;
                PublicHelper ph = new PublicHelper();
                ph.printStuffSaleReport(entity);
            }
            if (Grid2.Columns[e.ColumnIndex].Name == "Modify")//修改
            {
                Config c = new Config();
                if (c.isAdminUpdate)
                {
                    ERPDataProvider erp = new ERPDataProvider();
                    if (!erp.isAdmin(_CurrentUserId))
                    {
                        MessageBox.Show("此账号无修改权限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                StuffSale currentStuffIn = (StuffSale)Grid2.SelectedRows[0].DataBoundItem;
                Frm_StuffSaleModify stuffinfrm = new Frm_StuffSaleModify(currentStuffIn, this);
                stuffinfrm.ShowDialog();
            }
        }
    }
}
