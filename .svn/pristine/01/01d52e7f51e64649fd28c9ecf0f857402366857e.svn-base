using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using System.Web.Script.Serialization;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_ResultController : HRBaseController<HR_KQ_Result, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
        /// <summary>
        /// 计算最近月份
        /// </summary>
        /// <returns></returns>
        public ActionResult ComputeNearMonth()
        {
            string startdate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            string enddate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string month1 = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            string month2 = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
            var query = this.service.GetGenericService<HR_KQ_ResultMain>().Query().Where(t => t.YearMonth == month1 && t.IsSeal).FirstOrDefault();
            if (query != null)
            {
                startdate = Convert.ToDateTime(month1 + "-01").AddMonths(1).ToString("yyyy-MM-dd");
            }
            if (month1 != month2)
            {
                var query2 = this.service.GetGenericService<HR_KQ_ResultMain>().Query().Where(t => t.YearMonth == month2 && t.IsSeal).FirstOrDefault();
                if (query2 != null)
                {
                    return OperateResult(false, "当前月份已经被封存", null);
                }
            }
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
        /// <summary>
        /// 计算结果
        /// </summary>
        /// <param name="IsSelectDepart"></param>
        /// <param name="departmentid"></param>
        /// <param name="Pesonid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public ActionResult ComputeReslut(bool IsSelectDepart, List<int> departmentid, List<string> Pesonid, string startdate, string enddate)
        {
            DateTime dt1;
            DateTime dt2;
            if (!DateTime.TryParse(startdate, out dt1) || !DateTime.TryParse(enddate, out dt2))
            {
                return OperateResult(false, "请输入正确的起止时间", null);
            }
            string personStr = string.Empty;
            if (IsSelectDepart)
            {
                if (departmentid == null || departmentid.Count <= 0)
                {
                    return OperateResult(false, "请选择部门", null);
                }
                var query = this.service.GetGenericService<HR_Base_Personnel>().Query().Where(t => departmentid.Contains(t.DepartmentID));
                var arry = query.Select(t => t.ID).ToArray();
                personStr = string.Join(",", arry);
            }
            else
            {
                if (Pesonid == null || Pesonid.Count <= 0)
                {
                    return OperateResult(false, "请选择员工", null);
                }
                var arry = Pesonid.ToArray();
                personStr = string.Join(",", arry);
            }
            if (dt2 >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                dt2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(-1);
            }
            if ((dt2 - dt1).Days > 31)
            {
                return OperateResult(false, "选择的时间跨度不能超过31天", null);
            }
            string month1 = dt1.AddMonths(-1).ToString("yyyy-MM");
            string month2 = dt2.AddDays(-1).ToString("yyyy-MM");
            var query1 = this.service.GetGenericService<HR_KQ_ResultMain>().Query().Where(t => t.YearMonth == month1 && t.IsSeal).FirstOrDefault();
            if (query1 != null)
            {
                startdate = Convert.ToDateTime(month1 + "-01").AddMonths(1).ToString("yyyy-MM-dd");
            }
            if (month1 != month2)
            {
                var query2 = this.service.GetGenericService<HR_KQ_ResultMain>().Query().Where(t => t.YearMonth == month2 && t.IsSeal).FirstOrDefault();
                if (query2 != null)
                {
                    return OperateResult(false, "当前月份已经被封存", null);
                }
            }
            SqlServerHelper help = new SqlServerHelper();
            var re = help.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "SP_HR_KQ_Result", new System.Data.SqlClient.SqlParameter("@StartDate", startdate), new System.Data.SqlClient.SqlParameter("@EndDate", enddate), new System.Data.SqlClient.SqlParameter("@PersonIds", personStr));
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
