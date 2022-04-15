using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Helpers;
using ZLERP.Business;  

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_ResultMainController : HRBaseController<HR_KQ_ResultMain, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_KQ_ResultMain HR_KQ_ResultMain)
        {
            string month = HR_KQ_ResultMain.YearMonth + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            var Main = this.m_ServiceBase.Add(HR_KQ_ResultMain);
            string startdate = da.ToString("yyyy-MM-dd");
            string enddate = da.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            SqlServerHelper help = new SqlServerHelper();
            var re = help.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "SP_HR_KQ_Result", new System.Data.SqlClient.SqlParameter("@StartDate", startdate), new System.Data.SqlClient.SqlParameter("@EndDate", enddate));
            if (re > 0)
            {
                return OperateResult(true, "计算成功", null);
            }
            else
            {
                return OperateResult(false, "计算失败", null);
            }
             
        }

        public override ActionResult Update(HR_KQ_ResultMain HR_KQ_ResultMain)
        {
            string month = HR_KQ_ResultMain.YearMonth + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            } 
            return base.Update(HR_KQ_ResultMain);
        }

        public ActionResult ComputeSalar(string id,string personids)
        {
        
            var main = this.m_ServiceBase.Get(id); 
            if (main == null)
            {
                return OperateResult(false, "请选择单据进行计算", null);
            }
            if (main.IsSeal)
            {
                return OperateResult(false, "当前考勤表已经被封存，不能再计算", null);
            }
            string month = main.YearMonth + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            string startdate = da.ToString("yyyy-MM-dd");
            string enddate = da.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            SqlServerHelper help = new SqlServerHelper();
            var re = help.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "SP_HR_KQ_Result", new System.Data.SqlClient.SqlParameter("@StartDate", startdate), new System.Data.SqlClient.SqlParameter("@EndDate", enddate));
            if (re > 0)
            {
                return OperateResult(true, "计算成功", null);
            }
            else
            {
                return OperateResult(false, "计算失败", null);
            }
        }
    }
}
