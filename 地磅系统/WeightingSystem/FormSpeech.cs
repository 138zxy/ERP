using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Helpers;

namespace WeightingSystem
{
    public partial class FormSpeech : Form
    { 
        public FormSpeech()
        {
            InitializeComponent();
          
            List<KeyValuePair<string,string>> list = Config.Ini.GetSectionValuesAsList(Config.Section.Speech);
            foreach (KeyValuePair<string, string> s in list)
            {
                if (tbContent.Text.Length > 0)
                {
                    tbContent.AppendText("\r\n");
                }
                tbContent.AppendText(s.Value);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            Config.Ini.DeleteSection(Config.Section.Speech); 
            string speechText = tbContent.Text.Trim();
            string[] s = speechText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s.Length; i++)
            { 
                Config.Ini.WriteValue(Config.Section.Speech, Config.ConfigKey.CustomSpeech + i.ToString(), s[i]);
            }
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
