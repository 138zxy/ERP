using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;
using log4net; 
using ZLERP.NHibernateRepository;
using ZLERP.Web.Controllers.Attributes;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model;
using MvcContrib;
using System.Configuration;
using MvcContrib.IncludeHandling.Configuration;
using MvcContrib.IncludeHandling;
using System.Data;
using ZLERP.Web.Helpers;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;
using ZLERP.Web.Controllers;

namespace ZLERP.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {       
        ILog logger = LogManager.GetLogger(typeof(MvcApplication));
        SqlServerHelper sqlhelp = new SqlServerHelper();
        public MvcApplication() {

            EndRequest += new EventHandler(MvcApplication_EndRequest);
        }

        void MvcApplication_EndRequest(object sender, EventArgs e)
        {   //on session per request.
            if (UnitOfWork.IsStarted)
                UnitOfWork.Current.Dispose();
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {           
            filters.Add(new UrlAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Reports/{*pathInfo}");
            routes.IgnoreRoute("help/{*pathInfo}");
            //Reports route
            routes.MapRoute(
                "Reports",
                "Report.mvc/{path}/{report}",
                new {controller="Report", action = "Index"}
                );

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}.mvc/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //    , new string[] { "ZLERP.Web.Controllers" }
            //);
            routes.MapRoute(
                "Default", // Route name
                "{controller}.mvc/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional } // Parameter defaults
                , new string[] { "ZLERP.Web.Controllers" }
            );

        }
        
        /// <summary>
        /// 应用程序启动
        /// </summary>
        protected void Application_Start()
        {
            Application.Lock();
            Application["OnLineCount"] = 0;//在应用程序第一次启动时初始化在线人数为0
            Application.UnLock();

            AreaRegistration.RegisterAllAreas(); 
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //log4net配置信息
            log4net.Config.XmlConfigurator.Configure();

            var httpContextProvider = new HttpContextProvider(HttpContext.Current);
            var controllers = new[] { typeof(ZLERP.Web.Controllers.HomeController) };
            var includeHandlingSettings = (IIncludeHandlingSettings)ConfigurationManager.GetSection("includeHandling");
            DependencyResolver.SetResolver(new QnDDepResolver(httpContextProvider, includeHandlingSettings, controllers));

            //注册自定义的模型验证
            //暂时没起到作用，不需要 by:Sky 2012/03/14
           // DataAnnotationsModelValidatorProvider.RegisterAdapterFactory(typeof(RequiredAttribute), (m, c, a) => new MyRequiredAttributeAdapter(m, c, (RequiredAttribute)a));
 
            //初始化uowfactory
            IUnitOfWorkFactory factory = new UnitOfWorkFactory();
            factory.Configuration();

            

            //启动线程  
            //Thread thread = new Thread(new ThreadStart(ConnectWS));
            //thread.Start();

            ////启动保障线程
            //Thread thread1 = new Thread(new ThreadStart(ConnectWS));
            //thread1.Start();

            //启动定时任务
            new ZLERP.Web.Jobs.TaskAutoFinishScheduler().Start();

        }

        public void ReConnect()
        {
            while (true)
            {
                Thread.Sleep(60 * 1000 * 10 );
                ConnectWS();
            }
        }
        
        /// <summary>
        /// 离开应用程序启动这件事会发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End()
        {
            ILog log = LogManager.GetLogger("Application");
            try
            {
                log.Debug("应用程序池关闭");
                //针对IIS被回收的可能性进行的逻辑处理，也可以设计IIS闲置时间为0
                System.Threading.Thread.Sleep(1000);
                

                //IIS应用池回收启动Application
                string strUrl = "http://localhost:" + ConfigurationManager.AppSettings["WebPort"].ToString();
                log.Debug("网址URL：" + strUrl);
                if (!string.IsNullOrEmpty(strUrl))
                {
                    HttpWebRequest _HttpWebRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                    HttpWebResponse _HttpWebResponse = (HttpWebResponse)_HttpWebRequest.GetResponse();
                    Stream receiveStream = _HttpWebResponse.GetResponseStream();
                }
            }
            catch (Exception ex)
            {
                log.Debug("应用程序池关闭失败：" + ex.Message.ToString());
            }
        }
        
        protected void Session_Start(Object sender, EventArgs e)//客户端一连接到服务器上，这个事件就会发生
        {
            Application.Lock();//锁定后，只有这个Session能够会话
            Application["OnLineCount"] = (int)Application["OnLineCount"] + 1;
            Application.UnLock();//会话完毕后解锁
            Session.Timeout = 1;
        }
        
        protected void Session_End(Object sender, EventArgs e)
        {
            lock (ServiceBasedController.licLock)
            {
                Application.Lock();
                Application["OnLineCount"] = (int)Application["OnLineCount"] - 1;
                Application.UnLock();
                //是否在线的hash
                Hashtable hOnline = (Hashtable)Application["Online"];
                if (hOnline != null)
                {
                    if (hOnline[Session.SessionID] != null)
                    {
                        hOnline.Remove(Session.SessionID);
                        Application.Lock();
                        Application["Online"] = hOnline;
                        Application.UnLock();
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                StringBuilder sb = new StringBuilder();

                Exception ex = exception.GetBaseException();
                sb.Append(ex.Message);
                sb.Append("\r\nURL: " + Request.Url);
                sb.Append("\r\nUserHostAddress: " + Request.UserHostAddress);
                sb.Append("\r\nFORM: " + Request.Form.ToString());
                sb.Append("\r\nQUERYSTRING: " + Request.Params.ToString());

                sb.Append("\r\nSOURCE: " + exception.Source);
                sb.Append("\r\nExeptionData: " + exception.Data);
                sb.Append("\r\n引发当前异常的原因: " + exception.TargetSite);
                sb.Append("\r\n堆栈跟踪: " + exception.StackTrace);

                sb.Append("\r\nBaseException.SOURCE: " + ex.Source);
                sb.Append("\r\nBaseException.Data: " + ex.Data);
                sb.Append("\r\nBaseException.TargetSite: " + ex.TargetSite);
                sb.Append("\r\nBaseException.StackTrace: " + ex.StackTrace);
                logger.Error(sb.ToString());
                Response.Write(string.Format("<script>if(typeof(showError)!='undefined'){{showError(\"服务器错误\",\"{0}\");}} else{{alert(\"{0}\");}}</script>", Server.HtmlEncode(ex.Message).Replace("\r\n","<br/>")));
               
                //string errmsg = sb.ToString();
                //string[] parameters = {errmsg, Request.ApplicationPath};
                //Response.Write(string.Format("<script>alert(\"{0}\");window.location.href=\"{1}Login.mvc\";</script>", parameters));

                Response.End();
                Server.ClearError();
            }
        }

        /// <summary>
        /// 连接websocket
        /// </summary>
        private void ConnectWS()
        {
            if (ConfigurationManager.AppSettings["IsEnableWs"].ToString().ToLower() == "true")//启用Websocket
            {
                try
                {
                    string companyCode = "";
                    string companyName = "";
                    string machinecode;
                    string machinename;
                    string ip = "";
                    string wsUri;
                   
                    var sysconfigWsIp = sqlhelp.ExecuteScalar(CommandType.Text, @"SELECT ConfigValue FROM dbo.SysConfig WHERE ConfigName = 'ZoomlionWsIp'", null);
                    if (sysconfigWsIp != null && !string.IsNullOrWhiteSpace(sysconfigWsIp.ToString()))
                    {
                        wsUri = "ws://" + sysconfigWsIp.ToString() + "/Echo";
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["WsIp"].ToString()))
                        {
                            wsUri = "ws://" + ConfigurationManager.AppSettings["WsIp"].ToString() + "/Echo";
                        }
                        else
                        {
                            ILog log = LogManager.GetLogger("Application");
                            log.Debug("没有填写注册服务器地址。");
                            return;
                        }
                    }
                  
                    //companyCode = ConfigurationManager.AppSettings["CmpCode"].ToString();                   
                    DataTable dt;
                    dt = sqlhelp.ExecuteDataset(@"SELECT top 1 ConfigValue FROM SysConfig WHERE ConfigName='" + Model.Enums.SysConfigEnum.EnterpriseCode + "'", CommandType.Text, null).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        companyCode = dt.Rows[0]["ConfigValue"].ToString();
                    }
                    dt = sqlhelp.ExecuteDataset(@"SELECT top 1 ConfigValue FROM SysConfig WHERE ConfigName='" + Model.Enums.SysConfigEnum.EnterpriseName + "'", CommandType.Text, null).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        companyName = dt.Rows[0]["ConfigValue"].ToString();
                    }
                    machinename = System.Environment.MachineName;
                    machinecode = GetMachineCode.getMachineCode();
                    ip = GetIpAddress();
                    wsUri = wsUri + "?code=" + companyCode + "&name=" + companyName + "&machinecode=" + machinecode + "&machinename=" + machinename + "&ip=" + ip;
                    if (wsfun == null)
                        wsfun = new WebSocketFun();
                    wsfun.wsUri = wsUri;
                    wsfun.initWs();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        WebSocketFun wsfun;

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        private string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
            //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
            string ipstr = "";
            foreach (var lh in localhost.AddressList)
            {
                IPAddress localaddr = lh;
                if (ipstr == "")
                {
                    ipstr = localaddr.ToString();
                }
                else
                {
                    ipstr = ipstr + "," + localaddr.ToString();
                }
            }
            return ipstr;
        }

    }

    public class QnDDepResolver : IDependencyResolver
    {
        private readonly IDictionary<Type, Func<object>> types;

        public QnDDepResolver(IHttpContextProvider httpContextProvider, IIncludeHandlingSettings settings, Type[] controllers)
        {
            types = new Dictionary<Type, Func<object>>
			{
				{ typeof (IHttpContextProvider),() => httpContextProvider },
				{ typeof (IKeyGenerator), () => new KeyGenerator() },
				{ typeof (IIncludeHandlingSettings), () => settings }
			};
            types.Add(typeof(IIncludeReader), () => new FileSystemIncludeReader(GetImplementationOf<IHttpContextProvider>()));

            types.Add(typeof(IIncludeStorage), () => new StaticIncludeStorage(GetImplementationOf<IKeyGenerator>()));

            types.Add(typeof(IIncludeCombiner), () => new IncludeCombiner(settings, GetImplementationOf<IIncludeReader>(), GetImplementationOf<IIncludeStorage>(), GetImplementationOf<IHttpContextProvider>()));

            types.Add(typeof(IncludeController), () => new IncludeController(settings, GetImplementationOf<IIncludeCombiner>()));
            foreach (var controller in controllers)
            {
                var controllerType = controller;
                types.Add(controllerType, () => Activator.CreateInstance(controllerType));
            }
        }

        private Interface GetImplementationOf<Interface>()
        {
            return (Interface)GetService(typeof(Interface));
        }

        public object GetService(Type serviceType)
        {
            if (types.ContainsKey(serviceType))
            {
                return types[serviceType]();
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            yield break;
        }





    }
}