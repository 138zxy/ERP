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
    public partial class StuffChoose : Form
    {
        private MainForm _main;
        private StuffIn _si ;
        public string _WeightName;//磅名

        /// <summary>
        /// 用于修改皮重原料
        /// </summary>
        /// <param name="main"></param>
        public StuffChoose(MainForm main,StuffIn StuffIn)
        {
            _main = main;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            _WeightName = main.checkWeightName;

            if (StuffIn != null)
            {

                _si = StuffIn;
                groupBox1.Visible = true;
                lblStuffName.Text = _si.StuffName;
                lblSpec.Text = _si.Spec;
                lblSupplyName.Text = _si.SupplyName;
            }
            
        }

        private void StuffChoose_Load(object sender, EventArgs e)
        {
                bindStuffType();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            ERPDataProvider edp = new ERPDataProvider();
            IList<StuffInfo> StuffInfoList = new List<StuffInfo>();
            if (comboBox1.SelectedValue != null)
            {
                if (comboBox1.SelectedValue.ToString() == "OTHER")
                {
                    StuffInfoList = edp.getStuffInfo("FinalStuffType not in('CA','SHI','SHA','FA','WA','CE','AIR','ADM') or FinalStuffType is null ");
                }
                else
                {
                    StuffInfoList = edp.getStuffInfo("FinalStuffType ='" + comboBox1.SelectedValue.ToString() + "' and  StuffName like '%"+MH.Text+"%'");
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

            if (comboBox1.SelectedValue != null)
            {
                if (comboBox1.SelectedValue.ToString() == "OTHER")
                {
                    StuffInfoList = edp.getStuffSupplyInfo("FinalStuffType not in('CA','SHI','SHA','FA','WA','CE','AIR','ADM') or FinalStuffType is null ");
                }
                else
                {
                    StuffInfoList = edp.getStuffSupplyInfo("FinalStuffType ='" + comboBox1.SelectedValue.ToString() + "' and  (StuffName like '%" + MH.Text + "%' or SupplyName like '%" + MH.Text + "%')");
                }
                dataGridView1.DataSource = StuffInfoList;
            }



        }

        private void bindStuffType()
        {
            PublicHelper ph = new PublicHelper();
            List<ListItem> itemList = ph.bindStuffType();
           
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";
            comboBox1.DataSource = itemList;
      
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            StuffInfo si = (StuffInfo)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            if (!groupBox1.Visible)         //称皮重
            {

                WeightManage wm = new WeightManage(si, this, _main);
                //this.Close();
                wm.TopMost = true;
                wm.Show();
            }
            else                            //修改皮重材料
            {
                if (si.StuffID == _si.StuffID && si.SupplyID == _si.SupplyID)
                {
                    MessageBox.Show(this, "原材料与修改后材料相同了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    SiloChoose sc = new SiloChoose(_si, si,this, _main);
                    sc.ShowDialog();
                }
            }
           
            
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                ERPDataProvider edp = new ERPDataProvider();
                IList<StuffInfo> StuffInfoList = edp.getStuffSupplyInfo("StuffInfo.StuffID ='" + comboBox2.SelectedValue.ToString() + "'");
                dataGridView1.DataSource = StuffInfoList;
            }
        }
    }
}
