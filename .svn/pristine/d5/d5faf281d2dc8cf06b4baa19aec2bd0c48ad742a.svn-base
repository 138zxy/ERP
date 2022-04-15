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
    public partial class SiloChoose : Form
    {
        private StuffIn _si;
        private StuffInfo _stuffinfo;
        private MainForm _mf;

        public SiloChoose(StuffIn si ,StuffInfo stuffinfo, StuffChoose sc,MainForm mainform)
        {
            InitializeComponent();
            if (sc != null)
            {
                sc.Close();
            }
            _si = si;
            _stuffinfo = stuffinfo;
            if (mainform != null)
            {
                _mf = mainform;

            }
            #region 初始化更改材料信息
            lblStuffName.Text = _stuffinfo.StuffName;
            lblSpec.Text = _stuffinfo.Spec;
            lblSupplyName.Text = _stuffinfo.SupplyName;

            ERPDataProvider provider = new ERPDataProvider();

            this.cbSilo.DisplayMember = "SiloName";
            this.cbSilo.ValueMember = "SiloID";
            this.cbSilo.DataSource = provider.GetSiloList(_stuffinfo.StuffID);
           
            #endregion
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbSilo.Text.Trim()))
            {
                MessageBox.Show(this, "请选择入库仓位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSilo.Focus();
                return;
            }
            _si.StuffID = _stuffinfo.StuffID;
            _si.StuffName = _stuffinfo.StuffName;
            _si.Spec = _stuffinfo.Spec;
            _si.SupplyID = _stuffinfo.SupplyID;
            _si.SupplyName = _stuffinfo.SupplyName;
            _si.SiloID = cbSilo.SelectedValue.ToString();
            _si.SiloName = cbSilo.SelectedText;
            _si.Remark = string.Empty;
            ERPDataProvider edp = new ERPDataProvider();
            edp.SaveStuffIn(_si);
            if (_mf != null)
            {
                _mf.RefreshStuffInList();
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
