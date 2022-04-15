using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Helpers;

namespace WeightingSystem
{
    public partial class SystemSetting : Form
    {
        Form parentForm;
        public SystemSetting()
        {
            InitializeComponent();
        }
        public SystemSetting(Form ParentForm)
        {
            InitializeComponent();
            parentForm = ParentForm;
        }

        private void SystemSetting_Load(object sender, EventArgs e)
        {
            LocalDataProvider ldp = new LocalDataProvider();
            Config c = new Config();
            this.txtCompanyName.Text = c.CompanyName;
            this.txtPrefix.Text = c.prefixPat;
            this.txtIdLen.Text = c.idLen.ToString();
            this.txtDarkRatemode.Text = c.DarkWeightMode.ToString();
            this.ckAdminMode.Checked = c.adminMode == 1 ? true : false;
            this.ckEnablePrintStuffName.Checked = c.EnablePrintStuffName == 1 ? true : false;

            this.chkAutoPrint.Checked = c.AutoPrint;
            this.chkSoundWeight.Checked = c.SoundWeight;
            this.txtExchange.Text = c.Exchange;

            //地磅1
            this.ckWeightEnable.Checked = c.COMISEnable;
            this.txtWeightName.Text = c.COMWeightName;
            this.cbCOMList.Text = c.COM;
            this.cbBaudRate.Text = c.BaudRate;
            if (c.Positive)
            {
                rdPositive.Checked = true;
            }
            else
            {
                rdReversion.Checked = true;
            }
            this.txtDataLength.Text = c.DataLength;
            this.txtStartChar.Text = c.StartChar;
            this.txtEndChar.Text = c.EndChar;
            //地磅2
            this.ckWeightEnable2.Checked = c.COM2ISEnable;
            this.txtWeightName2.Text = c.COM2WeightName;
            this.cbCOMList2.Text = c.COM2;
            this.cbBaudRate2.Text = c.BaudRate2;
            if (c.Positive2)
            {
                rdPositive2.Checked = true;
            }
            else
            {
                rdReversion2.Checked = true;
            }
            this.txtDataLength2.Text = c.DataLength2;
            this.txtStartChar2.Text = c.StartChar2;
            this.txtEndChar2.Text = c.EndChar2;


             
            this.txtServer.Text = c.Server;
            this.txtDB.Text = c.Database;
            this.txtUid.Text = c.uid;
            this.txtPwd.Text = c.pwd;

            ckIsAminUpdate.Checked = c.isAdminUpdate;
        }

        private void SettingCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbCOMList.Text))
            {
                MessageBox.Show("请填写串口号！");
                return;
            }
            else
            {
                this.SP.PortName = this.cbCOMList.Text;
                bool closePort = false;
                try
                {

                    if (!SP.IsOpen)
                    {
                        SP.Open();
                        closePort = true;
                        MessageBox.Show("该端口可以正常使用!", "提示信息");
                    }
                }
                catch
                {
                    MessageBox.Show("该端口无法正常使用!", "提示信息");
                }
                finally
                {
                    if (closePort)
                    {
                        SP.Close();
                    }
                }
            } 
 
        }
        private void btnTestSP2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbCOMList2.Text))
            {
                MessageBox.Show("请填写串口号！");
                return;
            }
            else
            {
                this.SP2.PortName = this.cbCOMList2.Text;
                bool closePort = false;
                try
                {

                    if (!SP2.IsOpen)
                    {
                        SP2.Open();
                        closePort = true;
                        MessageBox.Show("该端口可以正常使用!", "提示信息");
                    }
                }
                catch
                {
                    MessageBox.Show("该端口无法正常使用!", "提示信息");
                }
                finally
                {
                    if (closePort)
                    {
                        SP2.Close();
                    }
                }
            } 
        }

        private void btnTestDBConnect_Click(object sender, EventArgs e)
        {
            string ConnectString = "server=" + this.txtServer.Text + ";" + "database=" + this.txtDB.Text + ";" + "uid=" + this.txtUid.Text + ";" + "pwd=" + this.txtPwd.Text + ";Connection Timeout=30;";
            SqlConnection sql = new SqlConnection(ConnectString);
           
            try
            {
                sql.Open();
                MessageBox.Show("数据库连接成功!","消息提示");
            }
            catch
            {
                MessageBox.Show("数据库连接失败!", "消息提示");
            }
            finally
            {
                sql.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showNoSynRecord();
        }

     
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showNoSynRecord();
        }


        private void showNoSynRecord()
        {
            this.linkLabel1.Text = "读取中...";
            LocalDataProvider ldp = new LocalDataProvider();
            this.linkLabel1.Text = "存在" + ldp.getNoSynRecordCount() + "条数据未同步";
        }

        private void SettingOK_Click(object sender, EventArgs e)
        {
            
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.CompanyName, this.txtCompanyName.Text);
              
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.AutoPrint, this.chkAutoPrint.Checked.ToString());
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.SoundWeight, this.chkSoundWeight.Checked.ToString());
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.StuffInputFree, this.chkStuffInputFree.Checked.ToString());
              
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.prefixPat, this.txtPrefix.Text.Trim());
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.idLen, this.txtIdLen.Text.Trim());
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.DarkRateMode, this.txtDarkRatemode.Text.ToString());
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.adminMode,ckAdminMode.Checked==true?1:0);
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.Exchange, this.txtExchange.Text.ToString());
                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.EnablePrintStuffName, ckEnablePrintStuffName.Checked == true ? 1 : 0);

                //地磅1
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.COMISEnable, this.ckWeightEnable.Checked.ToString());
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.COMWeightName, this.txtWeightName.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.Com, this.cbCOMList.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.BaudRate, this.cbBaudRate.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.DataLength, this.txtDataLength.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.StartChar, this.txtStartChar.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.EndChar, this.txtEndChar.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.Positive, this.rdPositive.Checked.ToString());
                //地磅2
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.COM2ISEnable, this.ckWeightEnable2.Checked.ToString());
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.COM2WeightName, this.txtWeightName2.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.Com2, this.cbCOMList2.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.BaudRate2, this.cbBaudRate2.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.DataLength2, this.txtDataLength2.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.StartChar2, this.txtStartChar2.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.EndChar2, this.txtEndChar2.Text);
                Config.Ini.WriteValue(Config.Section.MetageSetting, Config.ConfigKey.Positive2, this.rdPositive2.Checked.ToString());


                Config.Ini.WriteValue(Config.Section.DB, Config.ConfigKey.Server, this.txtServer.Text.Trim());
                Config.Ini.WriteValue(Config.Section.DB, Config.ConfigKey.Database, this.txtDB.Text.Trim());
                Config.Ini.WriteValue(Config.Section.DB, Config.ConfigKey.uid, this.txtUid.Text.Trim());
                Config.Ini.WriteValue(Config.Section.DB, Config.ConfigKey.pwd, this.txtPwd.Text.Trim());

                Config.Ini.WriteValue(Config.Section.BaseSetting, Config.ConfigKey.isAdminUpdate, this.ckIsAminUpdate.Checked.ToString());
                MessageBox.Show("设置保存成功!重新启用系统以加载新的设置！", "系统提示");
                
                parentForm.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SynDataProvider sdp = new SynDataProvider();
            //if (sdp.UploadData() == 1)
            //{
            //    MessageBox.Show("上传成功","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}

        }


    }
}
