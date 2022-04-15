using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using log4net;
using System.Threading;
using System.IO;

namespace WeightingSystem
{
    public partial class FormLogin : Form
    {
        private MainForm m_ParentForm;
        private IList<User> UserList = new List<User>();
        static ILog log = LogManager.GetLogger(typeof(FormLogin)); 
        
        public FormLogin()
        { 
            InitializeComponent();
            //btnLogin.Enabled = false;

            //this.Text = "登录 - 正在检测程序配置,请稍候...";
            //Thread t = new Thread(new ThreadStart(LoadLicenseInfo));
            //t.IsBackground = true;
            //t.Start(); 
            EnableLoginButton();  
        }

        #region License
        delegate void SetTextDelegate(string msg);
        void LoadLicenseInfo() {
            try
            {
                //ZLERP.Licensing.Client.License lic =  MainForm.LicenseInfo = ZLERP.Licensing.Client.License.GetLicense(); 
                //SetText(string.Format("{0}({1})", lic.LicenceTo, lic.Edition));
                EnableLoginButton();  
            }
            catch (Exception ex) {
                SetText(ex.Message);
                log.Error(ex.Message, ex);
                //if (ex is ZLERP.Licensing.Client.LicenseNotValidException)
                //{
                //    //显示注册窗口
                //    ShowRegistForm();
                //}
                return;
               
            }

           
           
        }
        /// <summary>
        /// 保存申请注册文件
        /// </summary>
        void SaveHWLicenseFile() {
            a a = new a();
            string reqFile = Path.Combine(MainForm.APPLICATION_PATH, string.Format("WSREQ-{0}-{1:yyyyMMdd}.txt", Environment.MachineName, DateTime.Now));
            using ( StreamWriter sw = new StreamWriter(File.Create(reqFile))) { 
                sw.Write(a.d());
                sw.Flush();
                sw.Close();
            }
        }

        delegate void HideFormDelegate();
        void ShowRegistForm()
        {
            if (btnLogin.InvokeRequired)
            {
                HideFormDelegate d = new HideFormDelegate(ShowRegistForm);
                this.Invoke(d);
            }
            else
            {
                RegistForm form = new RegistForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                //   form.TopMost = true;
                form.ShowDialog();
            }
        }

        delegate void EnableLoginButtonDelegate();
        void EnableLoginButton() {
            //if (btnLogin.InvokeRequired) 
            //{
            //    EnableLoginButtonDelegate d = new EnableLoginButtonDelegate(EnableLoginButton);
            //    this.Invoke(d);
            //}
            //else {
               
                ERPDataProvider edp = new ERPDataProvider();
                UserList = edp.GetUserList("UserType='42' and IsUsed=1"); 
                cbUserName.ValueMember = "UserID";
                cbUserName.DisplayMember = "UserName";
                cbUserName.DataSource = UserList  ;

                tbPassword.Focus();

                //lblError.Visible = MainForm.LicenseInfo.DaysLeftInTrial <= 10;
                //lblError.Text = string.Format(lblError.Text, MainForm.LicenseInfo.ExpireTo.ToString("yyyy年M月d日"));

                btnLogin.Enabled = true;
            //}
        }
        void SetText(string msg)
        {
            if (this.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);
                this.Invoke(d,   msg);
            }
            else {
                this.Text = "登录 - " + msg;
            }
        }

        #endregion

        public FormLogin(MainForm form)
            :this()
        { 
            this.m_ParentForm = form;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login(); 
        }

        private void login() {

            if (cbUserName.SelectedValue == null)
            {
                MessageBox.Show("请选择登录用户", "系统提示");
                return;
            }
            foreach (User user in UserList)
            {
                if (user.UserID == cbUserName.SelectedValue.ToString() && user.Password == getSHA1(tbPassword.Text.Trim()))
                {

                    log.Info("用户"+ cbUserName.SelectedText + "登录成功！");
                    MainForm main = new MainForm(cbUserName.SelectedValue.ToString(), cbUserName.Text);
                    main.Show();

                    this.Hide();
                    return;
                }
            }
            MessageBox.Show("密码错误，请重新输入", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }
        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && btnLogin.Enabled)
            {
                 
                login();
            
            }
        }

        /// <summary>
        /// 计算sha1
        /// </summary>
        /// <returns></returns>
        public string getSHA1(string str)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider SHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            //将mystr转换成byte[]
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);

            //Hash运算
            byte[] dataHashed = SHA1.ComputeHash(dataToHash);

            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
