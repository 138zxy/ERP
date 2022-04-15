using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using log4net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Management;
using ZLERP.Web.Helpers;
using WebSocketCS;
using ZLERP.JBZKZ12;

namespace ZLERP.Web
{
    public class WebSocketFun
    {
        public ILog log = LogManager.GetLogger(typeof(WebSocketFun));
        static WebSocket Ws;
        public string wsUri = "";
        public static WebSocketFun webSocketFun;
        SqlServerHelper sqlhelper = new SqlServerHelper();
        public WebSocketFun()
        {
            webSocketFun = this;
        }
        /// <summary>
        /// 初始化websocket
        /// </summary>
        public void initWs()
        {
            Ws = new WebSocket(wsUri);
            Ws.Compression = CompressionMethod.Deflate;//支持每个消息压缩
            Ws.SetCredentials("zlzkerp", "zlzk.123", true);//发送资格证书给HTTP身份验证（基本/摘要）
            Ws.OnOpen += (sender, e) => OnOpen(e);
            Ws.OnMessage += (sender, e) => OnMessage(e.Data);
            Ws.EmitOnPing = true;
            Ws.OnMessage += (sender, e) =>
            {
                if (e.IsPing)
                {
                    return;
                }
            };
            
            Ws.OnClose += (sender, e) => OnClose(e);
            Ws.OnError += (sender, e) => OnError(e);
            
            Ws.ConnectAsync();
        }
        /// <summary>
        /// 接收服务端消息
        /// </summary>
        /// <param name="data"></param>
        public void OnMessage(string data)
        {
            if(string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            data = DESEncrypt.Decrypt(data);
            BaseResultJson re = JsonHelper.Instance.Deserialize<BaseResultJson>(data);
            if (re.success)
            {
                MessageInfo msgmodel = JsonHelper.Instance.Deserialize<MessageInfo>(re.data.ToString());
                string a = MsgCmdType.SqlCmd.ToString();
                if (msgmodel.Type == MsgCmdType.SqlCmd.ToString())//sql命令
                {
                    using (SqlDataReader sdr = sqlhelper.ExecuteReader(msgmodel.Msg))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);

                        string json = JsonHelper.Instance.Serialize(dt);
                        SendMessage(MsgCmdType.SqlCmd.ToString(), json, msgmodel.DispatchCode, "");
                    }
                }
                else if (msgmodel.Type == MsgCmdType.RegCmd.ToString())//注册命令
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(msgmodel.Msg);
                    string regtype = jo["RegType"] == null ? "" : jo["RegType"].ToString();
                    string regdate = jo["Data"] == null ? "" : jo["Data"].ToString();
                    if (regtype == "GetMCode")
                    {
                        string mcode = GetMachineCode.getMachineCode();
                        string jsonstr = @"{'RegType':'GetMCode','State':'True','Data':'" + mcode + "'}";
                        SendMessage(MsgCmdType.RegCmd.ToString(), jsonstr, msgmodel.DispatchCode, "");
                    }
                    else if (regtype == "RegCode")
                    {
                        WriteContent(regdate);
                        string jsonstr = @"{'RegType':'RegCode','State':'True','Data':'" + msgmodel.CompanyName + "注册成功'}";
                        SendMessage(MsgCmdType.RegCmd.ToString(), jsonstr, msgmodel.DispatchCode, "");
                    }
                }
                else if (msgmodel.Type == MsgCmdType.TextCmd.ToString())//文本命令
                {
                    SendMessage(MsgCmdType.TextCmd.ToString(), "文本发送成功！"+msgmodel.Msg, msgmodel.DispatchCode, "");
                }
            }
        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="e"></param>
        public void OnOpen(EventArgs e)
        {
            log.Info("连接上服务端....");
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="e"></param>
        public void OnError(WebSocketCS.ErrorEventArgs e)
        {
            SendMessage(MsgCmdType.TextCmd.ToString(), e.Exception.Message, "", "");
            log.Info(e.Exception);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="e"></param>
        public void OnClose(CloseEventArgs e)
        {            
            string msg = "";
            switch (e.Code)
            {
                case 1000:
                    msg=("指示正常关闭");
                    break;
                case 1002:
                    msg=("由于协议错误终止连接！");
                    break;
                case 1003:
                    msg=("终止连接，因为它已经接收到一种类型的数据不能接受！");
                    break;
                case 1004:
                    msg=("仍然未定义。保留值！");
                    break;
                case 1005:
                    msg=("指示连接是关闭异常。保留值！");
                    break;
                case 1006:
                    msg=("指示连接异常关闭！");
                    break;
                case 1007:
                    msg=("由于协议错误终止连接！");
                    break;
                case 1008:
                    msg=("终止连接，因为它已经收到消息违反其规则！");
                    break;
                case 1009:
                    msg=("指示端点为终止连接，因为它已经收到消息太大，无法加工");
                    break;
                case 1010:
                    msg=("指示客户端是终止连接，因为它预期服务器协商一个或多个扩展，但服务器没有返回它们在握手响应中！！");
                    break;
                case 1011:
                    msg=("终止连接，因为它遇到了意想不到的意外条件阻止其履行请求！");
                    break;
                case 1015:
                    msg=("指示连接是由于未能执行TLS握手而关闭。保留");
                    break;
                default:
                    
                    break;
            }
            log.Info(msg);
            initWs();    
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cmdtype">类型</param>
        /// <param name="data">发送数据</param>
        public void SendMessage(string cmdtype, string data, string dispatchcode, string machineCode)
        {
            try
            {
                MessageInfo msginfo = new MessageInfo();
                msginfo.CompanyCode = "";
                msginfo.CompanyName = "";
                msginfo.Type = cmdtype;
                msginfo.Msg =data;
                msginfo.MsgTime = DateTime.Now;
                msginfo.CmdCode = "";
                msginfo.DispatchCode = dispatchcode;
                msginfo.MachineCode = machineCode;

                BaseResultJson re = new BaseResultJson();
                re.success = true;
                re.data =Newtonsoft.Json.JsonConvert.SerializeObject(msginfo);
                string result = Newtonsoft.Json.JsonConvert.SerializeObject(re);
                Action<bool> completed = null;
                Ws.SendAsync(DESEncrypt.Encrypt(result), completed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 写入文本文件  
        /// </summary>  
        /// <param name="content">内容</param>  
        public static void WriteContent(string content)
        {
            string fileurl = AppDomain.CurrentDomain.BaseDirectory + "\\license.lic";
            FileStream _file = new FileStream(fileurl, FileMode.Create, FileAccess.ReadWrite);
            using (StreamWriter writer1 = new StreamWriter(_file))
            {
                writer1.WriteLine(content);
                writer1.Flush();
                writer1.Close();
                _file.Close();
            }
        }      
    }
    /// <summary>
    /// 消息
    /// </summary>
    public class MessageInfo
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string Msg { get; set; }
        public DateTime MsgTime { get; set; }
        public string CmdCode { get; set; }
        public string DispatchCode { get; set; }
        public string MachineCode { get; set; }        
    }
}