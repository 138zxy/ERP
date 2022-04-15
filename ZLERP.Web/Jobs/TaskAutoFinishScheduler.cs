using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Configuration;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Jobs
{
    /// <summary>
    /// 混凝土任务自动完成-作业调度器
    /// </summary>
    public class TaskAutoFinishScheduler
    {
        protected ILog log = LogManager.GetLogger("TaskAutoFinishScheduler");
        private static string m_JobCron;//执行时间规则
        private static string m_JobOpen;//是否开启任务
        public string TJobCron
        {
            get { return m_JobCron ?? (m_JobCron = ConfigurationManager.AppSettings["TaskAutoFinishJobCron"]); }
        }
        public string TJobOpen
        {
            get { return m_JobOpen ?? (m_JobOpen = ConfigurationManager.AppSettings["TaskAutoFinishJobOpen"]); }
        }

        /// <summary>
        /// 定时执行
        /// </summary>
        public void Start()
        {
            try
            {
                bool JobOpen = false;
                if (!bool.TryParse(TJobOpen, out JobOpen))
                {
                    log.Error("混凝土任务自动完成Scheduler配置错误，启动失败");
                    return;
                }
                if (JobOpen)
                {
                    log.Debug("混凝土任务自动完成Scheduler开始启动，时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    QuartzHelper.ExecuteByCron<TaskAutoFinishJob>(TJobCron);
                    log.Debug("混凝土任务自动完成Scheduler启动结束，时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            catch (Exception ex)
            {
                log.Error("混凝土任务自动完成JobScheduler启动失败，错误信息：" + ex.Message.ToString());
            }
        }
    }
}