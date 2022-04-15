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
    public partial class PrintStuffName : Form
    {
        private StuffIn _si ;

        public PrintStuffName(StuffIn si)
        {
            InitializeComponent();
            _si = si;
            LocalDataProvider ldp = new LocalDataProvider();
            IList<PrintStuffInfo> printStuffList = ldp.getPrintStuffList(_si.StuffID);
            comboBox2.DisplayMember = "PrintStuffName";
            comboBox2.ValueMember = "ID";
            //comboBox2.DataSource = printStuffList;
            //comboBox2.SelectedValue = si.StuffName;
            //comboBox2.SelectedText = si.StuffName;
            //comboBox2.Text = si.StuffName;
            foreach (PrintStuffInfo printStuff in printStuffList)
            {
                if (printStuff.DefaultSelect)
                {
                    comboBox2.SelectedValue = printStuff.ID;
                    comboBox2.SelectedText = printStuff.PrintStuffName;
                    comboBox2.Text = printStuff.PrintStuffName;
                    break;
                }
            }
            if (printStuffList.Count == 0)
            {
                comboBox2.SelectedValue = _si.StuffName;
                comboBox2.SelectedText = _si.StuffName;
                comboBox2.Text = _si.StuffName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _si.StuffName = comboBox2.Text;
            PublicHelper ph = new PublicHelper();
            LocalDataProvider ldp = new LocalDataProvider();
            ldp.AddPrintStuffName(_si.StuffID, comboBox2.Text);
            _si.StuffName = comboBox2.Text;
            ph.Print(_si);
            this.Close();
        }
    }
}
