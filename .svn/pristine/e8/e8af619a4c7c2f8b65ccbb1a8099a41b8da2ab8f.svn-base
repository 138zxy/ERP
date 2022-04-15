using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using WeightingSystem.Helpers;
namespace WeightingSystem
{
    public partial class FormSoftKeyBoard : Form
    {
        private Component m_ParentCB;
        public FormSoftKeyBoard(Component cb)
        {
            this.m_ParentCB = cb;
            InitializeComponent();

            #region 初始化快捷键信息
            button33.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.A1, string.Empty);
            button1.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.A2, string.Empty);
            button2.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.A3, string.Empty);
            button3.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.A4, string.Empty);
            button6.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.A5, string.Empty);
            button9.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.A6, string.Empty);

            button4.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.B1, string.Empty);
            button5.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.B2, string.Empty);
            button7.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.B3, string.Empty);
            button8.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.B4, string.Empty);
            button10.Text = Config.Ini.GetString(Config.Section.KeyBoard, Config.ConfigKey.B5, string.Empty);
            #endregion
            foreach (Control group in this.Controls)
            {
                if (group.GetType() == typeof(GroupBox))
                {
                    foreach (Control c in group.Controls)
                    {

                        if (c.GetType() == typeof(Button))
                        {
                            Button btn = (Button)c;
                            
                                btn.Click += new EventHandler(btn_Click);
                             
                        }
                    }

                }
            }

            textBox1.Focus();
        }

 

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            textBox1.Focus();
            textBox1.SelectedText = btn.Text;
          
        }

        private void button25_Click(object sender, EventArgs e)
        {
            
            if (this.m_ParentCB != null)
            {
                if (this.m_ParentCB.GetType() == typeof(System.Windows.Forms.ComboBox))
                {
                    ((ComboBox)this.m_ParentCB).Text = textBox1.Text;
                }
                else if (this.m_ParentCB.GetType() == typeof(System.Windows.Forms.TextBox))
                {
                    ((TextBox)this.m_ParentCB).Text = textBox1.Text;
                }
                
            }
            this.Close();
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{Backspace}");
        }
 

        private void button34_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
    }
}
