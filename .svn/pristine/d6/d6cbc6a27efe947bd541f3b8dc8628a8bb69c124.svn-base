using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using log4net;
using System.IO;
using System.Globalization;

namespace WeightingSystem
{
    static class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));
        private const int WS_SHOWNORMAL = 1;
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Process instance = GetRunningInstance();
                if (instance == null)
                {
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    //处理未捕获的异常   
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //处理UI线程异常   
                    Application.ThreadException += Application_ThreadException;
                    //处理非UI线程异常   
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                    //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    Application.Run(new FormLogin());
                    //try
                    //{
                    //    ZLERP.Licensing.Client.License lic = MainForm.LicenseInfo;
                    //    Application.Run(new FormLogin());
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //    Application.Run(new RegistForm());
                    //}                                          
                }
                else
                {
                    HandleRunningInstance(instance);
                }
            }
            catch (Exception ex)
            {
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
                string str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n堆栈信息：{2}\r\n", ex.GetType().Name, ex.Message, ex.StackTrace);
                log.Error(str);
                MessageBox.Show(@"发生未知错误，请查看相关错误|！", @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
            }
            finally
            {
                GC.Collect();
            }
        }
        
        /// <summary>
        /// 处理未捕获的线程异常
        /// 请在Program.cs文件中调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string str;
            string strDateInfo = "出现未处理线程异常：" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n";
            Exception error = e.Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n堆栈信息：{2}\r\n", error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("出现未处理线程异常:{0}", e);
            }

            log.Error(str);
            MessageBox.Show(error.Message + "\r\n请查看相关错误日志！", @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 处理应用程序未捕获的异常
        /// 请在Program.cs文件中调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str;
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n";

            if (error != null)
            {
                str = string.Format(strDateInfo + "出现应用程序未处理的异常:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("出现应用程序未处理的异常:{0}", e);
            }

            log.Error(str);
            MessageBox.Show(error.Message + "\r\n请查看相关错误日志！", @"系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 获取当前是否具有相同进程。
        /// </summary>
        /// <returns></returns>
        public static Process GetRunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历正在有相同名字运行的例程   
            foreach (Process process in processes)
            {
                //忽略现有的例程   
                if (process.Id != current.Id)
                    //确保例程从EXE文件运行 
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                        return process;
            }
            return null;
        }
        /// <summary>
        /// 激活原有的进程。
        /// </summary>
        /// <param name="instance"></param>
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
      
        ///// <summary>
        ///// 异常捕获
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    try
        //    {
        //        log.Error(e, e.ExceptionObject as Exception);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}

        //static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        //{
        //    log.Error(e, e.Exception);
        //    if (e.Exception != null)
        //    {
        //        MessageBox.Show(e.Exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }
}
