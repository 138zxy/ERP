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
    public partial class Frm_StuffInModify : Form
    {
        StuffIn _stuffin;
        private IList<User> UserList = new List<User>();
        ERPDataProvider provider = new ERPDataProvider();
        QueryDataForm _frm;
        public Frm_StuffInModify(StuffIn stuffin,QueryDataForm frm)
        {
            ERPDataProvider edp = new ERPDataProvider();
            InitializeComponent();
            _frm = frm;
            _stuffin = stuffin;
            BindCombox();
           
            lbWeightNo.Text = stuffin.StuffInID;
            cbCar.Text = stuffin.CarNo;
            cbStuff.SelectedValue = stuffin.StuffID; cbStuff.Text = stuffin.StuffName;//

            cbBaleId.SelectedValue = stuffin.SiloID; cbBaleId.Text = stuffin.SiloName;//
            txtTotalNum.Text = stuffin.TotalNum.ToString();
            txtCarWeight.Text = stuffin.CarWeight.ToString();
            cbSupply.SelectedValue = stuffin.SupplyID; cbSupply.Text = stuffin.SupplyName;//

            dateTimePicker1.Text = Convert.ToString(stuffin.OutDate);
            txtWRate.Text = stuffin.WRate.ToString();
            txtKz.Text = stuffin.MingWeight.ToString();
            cbUserName.Text = stuffin.Operator;
            txtRemark.Text = stuffin.Remark;
            
            //公司列表
            IList<ListItem> itemsCmp = edp.GetCmpValueList();
            itemsCmp.Insert(0, new ListItem());
            cbComp.DataSource = itemsCmp;
            this.cbComp.DisplayMember = "Text";
            this.cbComp.ValueMember = "Value";
            txtSource.Text = stuffin.SourceNumber;
            if (stuffin.CompanyID!=null)
            {
                cbComp.SelectedValue = stuffin.CompanyID; 
            }
            
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

            CaluWeight();
        }
        
        private void BindCombox()
        {

            //操作员列表
            UserList = provider.GetUserList("UserType='42' and IsUsed=1");
            cbUserName.ValueMember = "UserID";
            cbUserName.DisplayMember = "UserName";
            cbUserName.DataSource = UserList;

            //材料列表
            this.cbStuff.DisplayMember = "Text";
            this.cbStuff.ValueMember = "Value";
            IList<ListItem> list = provider.GetStuffList();
            ListItem empty = new ListItem();
            empty.Text = "";
            empty.Value = "";
            list.Insert(0, empty);
            this.cbStuff.DataSource = list;
            //供应商列表
            this.cbSupply.DisplayMember = "Text";
            this.cbSupply.ValueMember = "Value";
            list = provider.GetSupplyList();
            list.Insert(0, empty);
            this.cbSupply.DataSource = list;

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
            StuffIn si = _stuffin;
            si.CarNo = cbCar.Text;
            si.StuffID = cbStuff.SelectedValue.ToString(); si.StuffName = cbStuff.Text.Trim();

            si.SiloID = cbBaleId.SelectedValue.ToString(); si.SiloName = cbBaleId.Text.Trim();
            si.TotalNum = Convert.ToDecimal(txtTotalNum.Text.Trim());
            si.CarWeight = Convert.ToDecimal(txtCarWeight.Text.Trim());
            si.InNum =Convert.ToDecimal(txtWeight.Text) ;//Convert.ToDecimal(txtTotalNum.Text.Trim()) - Convert.ToDecimal(txtCarWeight.Text.Trim());
            si.SupplyID = cbSupply.SelectedValue.ToString(); si.SupplyName = cbSupply.Text.Trim();
            si.OutDate =Convert.ToDateTime(dateTimePicker1.Text);
            si.WRate =  Convert.ToDecimal(txtWRate.Text.Trim());
            si.MingWeight = Convert.ToDecimal(txtKz.Text.Trim());
            si.Operator = cbUserName.Text.Trim();
            si.Remark = txtRemark.Text.Trim();
            si.CompanyID = cbComp.SelectedValue == null ? "" : cbComp.SelectedValue.ToString();
            si.SourceNumber = txtSource.Text;
            
            Config c = new Config();
            int kz = 0;
            if (c.RateMode == 0)
            {
                kz = Convert.ToInt32(si.MingWeight);
            }
            if (c.RateMode == 1)
            {
                kz = Convert.ToInt32((si.TotalNum - si.CarWeight) * si.WRate / 100);
                kz = Convert.ToInt32((kz + 5) / 10) * 10;
                si.InNum = si.TotalNum - si.CarWeight - kz;
            }
            if (si.Proportion != 0)
            {
                si.FootNum = (si.TotalNum - si.CarWeight - kz) / si.Proportion;
            }

            if (provider.UpdateStuffInNoKc(si))
            {
                MessageBox.Show(this, "修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _frm.gird2.Refresh();
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
        /// <summary>
        /// 过滤材料对应库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void btnCarNoSoftKeyboard_Click(object sender, EventArgs e)
        {
            FormSoftKeyBoard fskb = new FormSoftKeyBoard(this.cbCar);
            fskb.ShowDialog();
        }

        private void CaluWeight()
        {
            txtWeight.Text = Convert.ToDecimal(txtTotalNum.Text.Trim()) - Convert.ToDecimal(txtCarWeight.Text.Trim()) - Convert.ToDecimal(txtKz.Text) + "";
        }

        private void txtKz_MouseLeave(object sender, EventArgs e)
        {
            decimal a = Convert.ToDecimal(txtTotalNum.Text.Trim()) - Convert.ToDecimal(txtCarWeight.Text.Trim());
            if (a == 0)
            {
                return;
            }
            txtWRate.Text = Math.Round(Convert.ToDecimal(txtKz.Text) * 100 / a, 2) + "";
            CaluWeight();
        }

        private void txtWRate_MouseLeave(object sender, EventArgs e)
        {
            decimal a = (Convert.ToDecimal(txtTotalNum.Text) - Convert.ToDecimal(txtCarWeight.Text));
            txtKz.Text = a * Convert.ToDecimal(txtWRate.Text) / 100 + "";
            CaluWeight();
        }
    }
}
