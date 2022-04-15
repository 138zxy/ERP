using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace WeightingSystem
{
    public partial class Frm_StuffSaleModify : Form
    {
        StuffSale _stuffsale;
        private IList<User> UserList = new List<User>();
        ERPDataProvider provider = new ERPDataProvider();
        StuffSaleReport _frm;
        public Frm_StuffSaleModify(StuffSale stuffsale, StuffSaleReport frm)
        {
            InitializeComponent();
            _frm = frm;
            _stuffsale = stuffsale;
            BindCombox();
           
            lbWeightNo.Text = stuffsale.StuffSaleID.ToString();
            cbCar.Text = stuffsale.CarNo;
            cbStuff.SelectedValue = stuffsale.StuffID; cbStuff.Text = stuffsale.StuffName;//原材料名称

            txtTotalNum.Text = stuffsale.TotalWeight.ToString();//毛重
            txtCarWeight.Text = stuffsale.CarWeight.ToString();//净重
            //cbSupply.SelectedValue = stuffsale.SupplyID; 
            cbSupply.SelectedValue = stuffsale.SupplyID;//收货单位
            cbComName.SelectedValue = stuffsale.CompanyID; //供货单位
            cbComName.SelectedText = stuffsale.CompName;//供货单位
            dateTimePicker1.Text = Convert.ToString(stuffsale.ArriveTime);//入厂时间
            textDeTime.Text = Convert.ToString(stuffsale.DeliveryTime);//出场时间
            textWeightMan.Text = stuffsale.Builder;
            txtRemark.Text = stuffsale.Remark;


            cbSupply.SelectedText = stuffsale.SupplyID;
            cbSupply.Text = stuffsale.SupplyID;
            //cbSupply.SelectedText = stuffsale.SupplyID;
            //cbSupply.Text = stuffsale.SupplyID;

            cbComName.SelectedValue = stuffsale.CompanyID;
            cbComName.SelectedText = stuffsale.CompName;
            cbComName.Text = stuffsale.CompName;

            Config c = new Config();
            if (c.isAdminUpdate)
            {
                ERPDataProvider erp = new ERPDataProvider();
                if (erp.isAdmin(frm._CurrentUserId))
                {
                    txtCarWeight.ReadOnly = false;
                    txtTotalNum.ReadOnly = false;
                }
                else
                {
                    txtCarWeight.ReadOnly = true;
                    txtTotalNum.ReadOnly = true;
                }
            }
        }
        
        private void BindCombox()
        {
            //材料列表
            this.cbStuff.DisplayMember = "Text";
            this.cbStuff.ValueMember = "Value";
            IList<ListItem> list = provider.GetGHGoods();
            ListItem empty = new ListItem();
            empty.Text = "";
            empty.Value = "";
            list.Insert(0, empty);
            this.cbStuff.DataSource = list;

            //材料出厂供货单位
            IList<ListIntItem> itemsCmp = provider.GetSaleCmpList();
            itemsCmp.Insert(0, new ListIntItem());
            cbComName.DataSource = itemsCmp;
            this.cbComName.DisplayMember = "Text";
            this.cbComName.ValueMember = "Value";

            //收货单位
            string SupplyType = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SupplyType, "");
            IList<ListItem> itemsSupply = provider.GetStuffSupplyList(SupplyType);
            itemsSupply.Insert(0, new ListItem());
            cbSupply.DataSource = itemsSupply;
            this.cbSupply.DisplayMember = "Text";
            this.cbSupply.ValueMember = "Value";

        }
        private void Frm_StuffInModify_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            StuffSale entity = _stuffsale;
            entity.CarNo = cbCar.Text;
            entity.StuffID = cbStuff.SelectedValue.ToString(); 
            entity.StuffName = cbStuff.Text.Trim();
            entity.TotalWeight = Convert.ToInt32(txtTotalNum.Text.Trim());
            entity.CarWeight = Convert.ToInt32(txtCarWeight.Text.Trim());
            entity.Weight = Convert.ToInt32(txtTotalNum.Text.Trim()) - Convert.ToInt32(txtCarWeight.Text.Trim());          
            entity.Remark = txtRemark.Text.Trim();
            entity.Modifier = _frm._CurrentUserId;

            entity.SupplyID = cbSupply.Text.ToString();
            entity.CompName = cbComName.Text.ToString();
            entity.CompanyID = cbComName.SelectedValue.ToString(); 
            Config c = new Config();

            if (provider.UpdateStuffSale(entity))
            {
                MessageBox.Show(this, "修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //_frm.Grid2.Refresh();
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.cbCar);
            fskb.ShowDialog();
        }
    }
}
