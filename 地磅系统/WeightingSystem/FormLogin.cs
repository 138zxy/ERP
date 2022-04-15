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
using System.Security.Cryptography;
using System.Globalization;

namespace WeightingSystem
{
    public partial class FormLogin : Form
    {
        private MainForm m_ParentForm;
        private IList<User> UserList = new List<User>();
        static ILog log = LogManager.GetLogger(typeof(FormLogin));
        private string lastLoginUser = "",lastLoginPwd="";
        public FormLogin()
        { 
            InitializeComponent();
            //btnLogin.Enabled = false;

            //this.Text = "登录 - 正在检测程序配置,请稍候...";
            //Thread t = new Thread(new ThreadStart(LoadLicenseInfo));
            //t.IsBackground = true;
            //t.Start(); 
            EnableLoginButton();

            Config c = new Config();
            ckRPwd.Checked = c.IsRpwd;
            lastLoginUser=cbUserName.Text = c.LoginUser;
            lastLoginPwd = tbPassword.Text =c.LoginPwd==""?"": Decrypt(c.LoginPwd);

            lbVersion.Text ="版本："+ ProductVersion;
        }

        #region License
        delegate void SetTextDelegate(string msg);
        void LoadLicenseInfo() {
            try
            {
                ZLERP.Licensing.Client.License lic =  MainForm.LicenseInfo = ZLERP.Licensing.Client.License.GetLicense(); 
                SetText(string.Format("{0}({1})", lic.LicenceTo, lic.Edition));
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

            try
            {
                ERPDataProvider edp = new ERPDataProvider();
                UserList = edp.GetUserList("(UserType='42' and IsUsed=1) or usertype='06'");
                cbUserName.ValueMember = "UserID";
                cbUserName.DisplayMember = "UserName";
                cbUserName.DataSource = UserList;
            }
            catch(Exception ex)
            {
                MessageBox.Show("失败:"+ex.Message, "提示", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
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
                if (user.UserID == cbUserName.SelectedValue.ToString() && user.Password == getPass(tbPassword.Text.Trim()))
                {
                    log.Info("用户"+ cbUserName.SelectedText + "登录成功！");
                    MainForm main = new MainForm(cbUserName.SelectedValue.ToString(), cbUserName.Text);
                    main.Show();

                    this.Hide();

                    if (ckRPwd.Checked)
                    {
                        Config.Ini.WriteValue(Config.Section.LoginSetting, Config.ConfigKey.IsRpwd, this.ckRPwd.Checked.ToString());
                        Config.Ini.WriteValue(Config.Section.LoginSetting, Config.ConfigKey.LoginUser, this.cbUserName.Text);
                        Config.Ini.WriteValue(Config.Section.LoginSetting, Config.ConfigKey.LoginPwd, Encryption(this.tbPassword.Text));
                    }
                    else
                    {
                        Config.Ini.WriteValue(Config.Section.LoginSetting, Config.ConfigKey.IsRpwd, this.ckRPwd.Checked.ToString());
                        Config.Ini.WriteValue(Config.Section.LoginSetting, Config.ConfigKey.LoginUser, "");
                        Config.Ini.WriteValue(Config.Section.LoginSetting, Config.ConfigKey.LoginPwd, "");
                    }
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
        public string getPass(string str)
        {
            Config c = new Config();
            if (c.PassType.ToLower() == "sha1")
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
            else if (c.PassType.ToLower() == "md5")
            {
                string md5=getMd5Sum(str);
                return md5;
            }
            else
            {
                MessageBox.Show("配置文件正确填写密码加密方式!");
                return "";
            }
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string getMd5Sum(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                pwd = pwd + s[i].ToString("X2");

            }
            return pwd;
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            //byte BeginCode = Byte.Parse("03", NumberStyles.HexNumber);
            Application.Exit();  
            //string s= Decrypt("DHoc+XuAkAd6ZKzu1l4WfUEik1qSqgk25dQJPjWmo1nU7e98Z9BI4kJuEC8fpcP1URBNrlX6xa3v+FmcCyck8jHjUVQDS/YIow+Dn/Yi08MKNOuoioqVPduVgPnKRrViOOlUzzF5Ekp4VxmKilcGAnlMAg5a950vTTwvcBesCjY=");
        }
        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected   override   bool   ProcessCmdKey(ref Message  msg,Keys keyData)  
        {  
                if   (keyData   ==   (Keys.F1))  
                {
                    SystemSetting frm = new SystemSetting(this);
                    frm.ShowDialog();
                    return true;
                }   
                return   base.ProcessCmdKey(ref msg,keyData);  
        }

        private void cbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastLoginUser == cbUserName.Text.Trim())
            {
                tbPassword.Text = lastLoginPwd;
            }
            else
            {
                tbPassword.Text = "";
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        private string Encryption(string express)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "zl_erp_weight";//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        private string Decrypt(string ciphertext)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "zl_erp_weight";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }
    }
}
