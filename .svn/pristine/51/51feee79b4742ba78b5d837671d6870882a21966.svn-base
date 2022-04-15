using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using ZLERP.Model;  

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_SalariesController : HRBaseController<HR_GZ_Salaries, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_Salaries HR_GZ_Salaries)
        {
            string month = HR_GZ_Salaries.YearMonth + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            var Salaries = this.m_ServiceBase.Add(HR_GZ_Salaries);

            var id = Salaries.ID;
            var user = AuthorizationService.CurrentUserID;
            SqlServerHelper help = new SqlServerHelper();
            var re = help.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "SP_HR_GZ_Salaries", new System.Data.SqlClient.SqlParameter("@id", id), new System.Data.SqlClient.SqlParameter("@user", user));
            if (re > 0)
            {
                return OperateResult(true, "添加成功", null);
            }
            else
            {
                return OperateResult(false, "添加失败", null);
            }
        }

        public override ActionResult Update(HR_GZ_Salaries HR_GZ_Salaries)
        {
            string month = HR_GZ_Salaries.YearMonth + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            } 
            return base.Update(HR_GZ_Salaries);
        }

        public ActionResult ComputeSalar(string id,string personids)
        {
            var Salaries = this.m_ServiceBase.Get(id);
            if (Salaries == null)
            {
                return OperateResult(false, "请选择单据进行计算", null);
            }
            if (Salaries.IsSeal)
            {
                return OperateResult(false, "当前工资表已经被封存，不能再计算", null);
            }
            var user= AuthorizationService.CurrentUserID;
            SqlServerHelper help = new SqlServerHelper();
            //new System.Data.SqlClient.SqlParameter("@personids", personids),
            var re = help.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "SP_HR_GZ_Salaries", new System.Data.SqlClient.SqlParameter("@id", id), new System.Data.SqlClient.SqlParameter("@user", user));
            if (re > 0)
            {
                return OperateResult(true, "计算成功", null);
            }
            else
            {
                return OperateResult(false, "计算失败", null);
            }
        }


        public override ActionResult Delete(string[] id)
        {
            //这个删除的操作非常重要，一定要保持同步进行，主体删除，明细没有删除，入库单永远找不到为啥已标记为结算
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.HRService.DeleteSalaries(id);
            return Json(resultInfo);

        }
             
    }
}
