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
    public partial class SupplyAdd : Form
    {
        private IList<Dic> siList = new List<Dic>();
        string _supplyid;
        public SupplyAdd(string text, string title, string supplyid)
        {
            InitializeComponent();

            ERPDataProvider edp = new ERPDataProvider();
            siList = edp.GetDicList(" ParentID='SuType'");
            comboBox1.ValueMember = "DicID";
            comboBox1.DisplayMember = "DicName";
            comboBox1.DataSource = siList;

            textBox1.Text = text;
            this.Text = title;
            label2.Text = supplyid;
            _supplyid = supplyid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string supplyname = textBox1.Text;
            if (supplyname.Length > 0)
            {
                ERPDataProvider edp = new ERPDataProvider();
                string sql = "select * from SupplyInfo where SupplyName='" + supplyname + "' and SupplyID<>'" + _supplyid + "'";
                DataTable dt = edp.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("供应商已存在");
                }
                else
                {
                    if (this.Text == "增加供应商")
                    {
                        string ssql = @"SELECT TOP 1 a.SupplyID FROM (
SELECT CAST(SUBSTRING(SupplyID,3,LEN(SupplyID)-2) AS INT) AS xh,* 
FROM SupplyInfo where SupplyID like 'BF%' 
) a order by xh DESC";
                        DataTable sdt = edp.GetDataTable(ssql);
                        string supplyid = "BF1";
                        if (sdt.Rows.Count > 0)
                        {
                            supplyid = sdt.Rows[0][0].ToString();
                            int x = Convert.ToInt32(supplyid.Substring(2, supplyid.Length - 2)) + 1;
                            supplyid = "BF" + x;
                        }
                        else
                        {
                            supplyid = "BF1";
                        }

                        string asql = String.Format("insert into SupplyInfo(SupplyID,ShortName,SupplyKind,SupplyName,IsUsed,Builder,BuildTime) values ('{0}','{1}','{3}','{1}',1,'BF','{2}')", supplyid, supplyname, DateTime.Now.ToString(), comboBox1.SelectedValue.ToString());
                        if (edp.ExecuteNonQuery(asql) > 0)
                        {
                            MessageBox.Show("增加供应商成功");
                            this.Close();
                        }
                        else {
                            MessageBox.Show("增加供应商失败");
                        }
                    }
                    else if (this.Text == "修改供应商")
                    {
                        string supplyid = label2.Text;
                        string esql = String.Format("Update SupplyInfo set SupplyName='{0}',ShortName='{0}',SupplyKind='{2}' where SupplyID='{1}'", supplyname, supplyid, comboBox1.SelectedValue.ToString());
                        if (edp.ExecuteNonQuery(esql) > 0)
                        {
                            MessageBox.Show("修改供应商成功");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("修改供应商失败");
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
