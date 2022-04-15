using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using log4net;
using ZLERP.Business;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.Data;
using ZLERP.JBZKZ12;

namespace ZLERP.Web.Jobs
{
    public class TaskAutoFinishJob : IJob
    {
        /// <summary>
        /// 混凝土任务自动完成
        /// </summary>
        protected ILog log = LogManager.GetLogger("TaskAutoFinishJob");      
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                string sqlStr = "";
                DataTable dt;
                SqlServerHelper helper = new SqlServerHelper();
                sqlStr = @"select top 1 * from sysconfig where ConfigName='TaskAutoFinishDay'";// service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.TaskAutoFinishDay);
                dt = helper.ExecuteDataset(sqlStr, CommandType.Text, null).Tables[0];
                if (dt.Rows.Count == 0 || dt.Rows[0]["ConfigValue"].ToString() == "0")
                {
                    return;
                }
                sqlStr = @" SELECT * FROM(
                            SELECT TaskID,A.PlanCube,COUNT(SignInCube) yCube FROM(
                            SELECT p.TaskID,p.PlanCube,s.SignInCube,s.ProduceDate 
                            FROM ProduceTasks p INNER JOIN ShippingDocument s ON p.TaskID=s.TaskID
                            WHERE p.IsCompleted=0 and dateadd(dd,{0},s.ProduceDate)<=GETDATE() 
                            ) A
                            GROUP BY A.TaskID,A.PlanCube
                            ) b WHERE yCube>b.PlanCube ";
                dt = helper.ExecuteDataset(string.Format(sqlStr, dt.Rows[0]["ConfigValue"]), CommandType.Text, null).Tables[0];
                if(dt.Rows.Count>0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        sqlStr = @"update ProduceTasks set IsCompleted=1,ModifyTime=GETDATE(),Modifier='系统定时' where taskid='" + row["TaskID"] + "'";
                        int rows=helper.ExecuteNonQuery(CommandType.Text,sqlStr);
                        if (rows>0)
                        {
                            log.Debug("混凝土任务:"+row["taskid"]+" 自动置为完成");
                        }
                    }
                }
                
                              
            }
            catch (Exception ex)
            {
                log.Error("混凝土任务Job执行失败:" + JsonHelper.Instance.Serialize(ex));
            }
        }
    }
}