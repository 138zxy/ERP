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
    public partial class StuffSupply : Form
    {
        CheckBox HeaderCheckBox = null;
        public StuffSupply()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                HeaderCheckBox = new CheckBox();
                HeaderCheckBox.Size = new Size(15, 15);
                this.SupplyGrid.Controls.Add(HeaderCheckBox);

                HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
                HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
                SupplyGrid.CurrentCellDirtyStateChanged += new EventHandler(dgvSelectAll_CurrentCellDirtyStateChanged);
                SupplyGrid.CellPainting += new DataGridViewCellPaintingEventHandler(dgvSelectAll_CellPainting);
            }

            HeaderCheckBox.Enabled = false;
             
        }

        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            foreach (DataGridViewRow Row in SupplyGrid.Rows)
            {
                ((DataGridViewCheckBoxCell)Row.Cells["chk"]).Value = HCheckBox.Checked;
            }
            SupplyGrid.RefreshEdit();
        }


        private void dgvSelectAll_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            Rectangle oRectangle = this.SupplyGrid.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);
            Point oPoint = new Point();
            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;
            HeaderCheckBox.Location = oPoint;
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }


        private void dgvSelectAll_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SupplyGrid.CurrentCell is DataGridViewCheckBoxCell)
                SupplyGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveRecord();   
        }

        private void saveRecord()
        {
            if (StuffGrid.SelectedRows.Count > 0)
            {
                string stuffID = StuffGrid.SelectedRows[0].Cells["StuffID"].Value.ToString();
                IList<ListItem> liList = new List<ListItem>();
                foreach (DataGridViewRow Row in SupplyGrid.Rows)
                {
                    if (Convert.ToBoolean(((DataGridViewCheckBoxCell)Row.Cells["chk"]).Value))
                    {
                        ListItem li = new ListItem();
                        li.Value = Row.Cells["ID"].Value.ToString();
                        liList.Add(li);
                    }
                }
                ERPDataProvider edp = new ERPDataProvider();
                edp.SaveStuffSupply(stuffID, liList);
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
        }

        private void StuffSupply_Load(object sender, EventArgs e)
        {
            StuffGrid.AutoGenerateColumns = false;
            SupplyGrid.AutoGenerateColumns = false;
            ERPDataProvider edp = new ERPDataProvider();
            IList<StuffInfo> siList = edp.getStuffInfo("1=1");
            IList<StuffInfo> siList2 =new List<StuffInfo>();
            foreach(var s in siList)
            {
                s.Inventory = Math.Round(s.Inventory / 1000,2); ;
                siList2.Add(s);
            }
            StuffGrid.DataSource = siList2;
        }

        private void StuffGrid_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {           
            refreshSupplyGrid();
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            HeaderCheckBox.Checked = false;          
        }


        private void refreshSupplyGrid()
        {
            ERPDataProvider edp = new ERPDataProvider();
            IList<ListItem> SupplyList = edp.GetSupplyList();
            SupplyGrid.DataSource = SupplyList;
            if (StuffGrid.SelectedRows.Count > 0)
            {
                string StuffID = StuffGrid.SelectedRows[0].Cells["StuffID"].Value.ToString();
                IList<StuffInfo> StuffSupplyList = edp.getStuffSupplyInfo("StuffSupply.StuffID='" + StuffID +"'");
                foreach (DataGridViewRow Row in SupplyGrid.Rows)
                {
                    foreach (StuffInfo si in StuffSupplyList)
                    {
                        if (Row.Cells["ID"].Value.ToString() == si.SupplyID)
                        {
                            ((DataGridViewCheckBoxCell)Row.Cells["chk"]).Value = true;
                            break;
                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StuffGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            HeaderCheckBox.Enabled = true;
            ((DataGridViewCheckBoxColumn)SupplyGrid.Columns["chk"]).ReadOnly = false;
        }

        private void btnUpdate_EnabledChanged(object sender, EventArgs e)
        {            
            HeaderCheckBox.Enabled = !btnUpdate.Enabled;
            ((DataGridViewCheckBoxColumn)SupplyGrid.Columns["chk"]).ReadOnly = btnUpdate.Enabled;
        }


        private void MarkTheRow_Stuff(string StuffName)
        {

            if (string.IsNullOrEmpty(StuffName.Trim()))
            {
                return;
            }


            DataGridViewRow currentRow = StuffGrid.CurrentRow;
            int currentRowIndex = 0;
            if (currentRow != null)
            {

                currentRowIndex = currentRow.Index;

            }
            int i = currentRowIndex + 1;
            while (StuffGrid.Rows.Count > 0)
            {
                if (i >= StuffGrid.Rows.Count)
                {
                    i = 0;
                }
                if (StuffGrid.Rows[i].Cells["StuffName"].Value.ToString().Contains(StuffName))
                {
                    StuffGrid.Rows[i].Selected = true;
                    StuffGrid.CurrentCell = StuffGrid[2, i];
                    StuffGrid.FirstDisplayedScrollingRowIndex = i;
                    return;
                }
                if (i == currentRowIndex)
                {
                    return;
                }
                i++;

            }


        }

        private void MarkTheRow_SupplyInfo(string SupplyName)
        {

            if (string.IsNullOrEmpty(SupplyName.Trim()))
            {
                return;
            }

            DataGridViewRow currentRow = SupplyGrid.CurrentRow;
            int currentRowIndex = 0;
            if (currentRow != null)
            {

                currentRowIndex = currentRow.Index;

            }
            int i = currentRowIndex + 1;
            while (SupplyGrid.Rows.Count > 0)
            {
                if (i >= SupplyGrid.Rows.Count)
                {
                    i = 0;
                }
                if (SupplyGrid.Rows[i].Cells["Supply"].Value.ToString().Contains(SupplyName))
                {
                    SupplyGrid.Rows[i].Selected = true;
                    SupplyGrid.CurrentCell = SupplyGrid[1, i];
                    SupplyGrid.FirstDisplayedScrollingRowIndex = i;
                    return;
                }
                if (i == currentRowIndex)
                {
                    return;
                }
                i++;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MarkTheRow_Stuff(textBox1.Text.Trim());
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                MarkTheRow_Stuff(textBox1.Text.Trim());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MarkTheRow_SupplyInfo(textBox2.Text.Trim());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                MarkTheRow_SupplyInfo(textBox2.Text.Trim());
            }
        }

        private void b_add_Click(object sender, EventArgs e)
        {
            SupplyAdd sa = new SupplyAdd("","增加供应商","");
            sa.ShowDialog();
            refreshSupplyGrid();
        }

        private void b_edit_Click(object sender, EventArgs e)
        {
            if(SupplyGrid.SelectedRows.Count==1){
                SupplyAdd sa = new SupplyAdd(SupplyGrid.SelectedRows[0].Cells["Supply"].Value.ToString(),
                    "修改供应商", SupplyGrid.SelectedRows[0].Cells["ID"].Value.ToString());
                sa.ShowDialog();
                refreshSupplyGrid();
            }
            else{
                MessageBox.Show("请选择一个供应商进行操作");
            }
        }

        private void b_delete_Click(object sender, EventArgs e)
        {
            if(SupplyGrid.SelectedRows.Count==1){
                ERPDataProvider edp = new ERPDataProvider();
                string esql = String.Format("Update SupplyInfo set IsUsed=0 where SupplyID='{0}'", SupplyGrid.SelectedRows[0].Cells["ID"].Value.ToString());
                if (edp.ExecuteNonQuery(esql) > 0)
                {
                    MessageBox.Show("停用供应商成功");
                    refreshSupplyGrid();
                }
                else
                {
                    MessageBox.Show("停用供应商失败");
                }
            }
            else{
                MessageBox.Show("请选择一个供应商进行操作");
            }
        }

        private void StuffGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
