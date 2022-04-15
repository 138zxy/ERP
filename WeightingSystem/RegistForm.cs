using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WeightingSystem
{
    public partial class RegistForm : Form
    {
        public RegistForm()
        {
            InitializeComponent();
            a a = new a();
            tbMachineCode.Text = a.d();
            
            
        }

        private void btnCopyMachineCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbMachineCode.Text.Trim());
            MessageBox.Show("机器码已成功复制到剪贴板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            try
            {
               
                btnRegist.Enabled = false;
                ZLERP.Licensing.Client.License lic = ZLERP.Licensing.Client.License.GetLicenseFromString(tbRegCode.Text.Trim());
                string info = "注册成功，以下为您的产品许可信息:\r\n";
                info += "\r\n产品版本:" + lic.Edition;
                info += "\r\n授 权 给:" + lic.LicenceTo;
                info += "\r\n序 列 号:" + lic.SerialNumber;
                info += "\r\n许可有效期至:" + lic.ExpireTo.ToLongDateString();

                info += "\r\n请重新运行程序完成注册！";
                
                info += "\r\n\r\n\r\n感谢您使用中联重科正版软件产品！";
                //保存注册文件 
                lic.a(Path.Combine(MainForm.APPLICATION_PATH, "license.lic"));

                MessageBox.Show(info, "注册成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (Exception ex) {
                btnRegist.Enabled = true;
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void RegistForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
