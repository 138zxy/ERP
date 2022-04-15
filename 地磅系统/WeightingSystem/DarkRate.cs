using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using log4net;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using WeightingSystem.Properties;
namespace WeightingSystem
{
    public partial class DarkRate : Form
    {

        private StuffIn _si;
        private MainForm _mf;
        static ILog log = LogManager.GetLogger(typeof(DarkRate));

        public DarkRate(StuffIn si,MainForm mf)
        {
            InitializeComponent();
            _si = si;
            _mf = mf;
            cbDarkWeight.SelectedText =  _si.DarkWeight.ToString();
            cbDarkWeight.Text = _si.DarkWeight.ToString();
            label2.Text = _si.TotalNum.ToString() ;
            cbDarkWeight.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int darkrate = 0;
            int.TryParse(cbDarkWeight.Text,out darkrate);
            if (darkrate<0)
            {
                MessageBox.Show("暗扣值不能小于0！","提示");
                return;
            }
            _si.TotalNum = _si.TotalNum + _si.DarkWeight - darkrate;
            _si.DarkWeight = darkrate;
            _si.Remark = string.Empty;

            ERPDataProvider edp = new ERPDataProvider();
            edp.SaveStuffIn(_si);
            log.Info("将单号："+ _si.StuffInID + " 的暗扣修改为:"+_si.DarkWeight.ToString());
            _mf.RefreshStuffInList();
            this.Close();
        }
    }
}
