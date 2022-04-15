using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Business;
using ZLERP.Web.Helpers; 

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_SalariesItemController : HRBaseController<HR_GZ_SalariesItem, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_SalariesItem HR_GZ_SalariesItem)
        {
            var Salaries = this.service.GetGenericService<HR_GZ_Salaries>().Get(HR_GZ_SalariesItem.SalariesID.ToString());
            if (Salaries == null)
            {
                return OperateResult(false, "请选择单据进行计算", null);
            }
            if (Salaries.IsSeal)
            {
                return OperateResult(false, "当前工资表已经被封存，不能再计算", null);
            }
            var query = this.m_ServiceBase.Query().Where(t => t.PersonID == HR_GZ_SalariesItem.PersonID && t.SalariesID == HR_GZ_SalariesItem.SalariesID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("{0}已经存在了", query.HR_Base_Personnel.Name), null);
            }
            OverrideUpdate(HR_GZ_SalariesItem);
            var re = base.Add(HR_GZ_SalariesItem);
            UpdateAll(HR_GZ_SalariesItem.SalariesID);
            return re;
        }

        public override ActionResult Update(HR_GZ_SalariesItem HR_GZ_SalariesItem)
        {
            var Salaries = this.service.GetGenericService<HR_GZ_Salaries>().Get(HR_GZ_SalariesItem.SalariesID.ToString());
            if (Salaries == null)
            {
                return OperateResult(false, "请选择单据进行计算", null);
            }
            if (Salaries.IsSeal)
            {
                return OperateResult(false, "当前工资表已经被封存，不能再计算", null);
            }
            var query = this.m_ServiceBase.Query().Where(t => t.ID != HR_GZ_SalariesItem.ID && t.PersonID == HR_GZ_SalariesItem.PersonID && t.SalariesID == HR_GZ_SalariesItem.SalariesID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("{0}已经存在了", query.HR_Base_Personnel.Name), null);
            }
            OverrideUpdate(HR_GZ_SalariesItem);
            var re = base.Update(HR_GZ_SalariesItem);

            UpdateAll(HR_GZ_SalariesItem.SalariesID);
            return re;
        }


        public void OverrideUpdate(HR_GZ_SalariesItem item)
        {
            item.AllPay = item.BasePay
                + (item.AllowancePay == null ? 0 : item.AllowancePay)
                + (item.LevelPay == null ? 0 : item.LevelPay)
                + (item.SeniorityPay == null ? 0 : item.SeniorityPay)
                + (item.EducationalPay == null ? 0 : item.EducationalPay)
                + (item.PerformancePay == null ? 0 : item.PerformancePay)
                + (item.PiecePay == null ? 0 : item.PiecePay)
                + (item.TimingPay == null ? 0 : item.TimingPay)
                + (item.DeductPay == null ? 0 : item.DeductPay)
                + (item.OverPay == null ? 0 : item.OverPay)
                + (item.SubsidyPay == null ? 0 : item.SubsidyPay);
            item.OutPay = (item.SocialPay == null ? 0 : item.SocialPay)
                + (item.TaxPay == null ? 0 : item.TaxPay)
                + (item.LeavePay == null ? 0 : item.LeavePay)
                + (item.LatePay == null ? 0 : item.LatePay)
                + (item.StayAwayPay == null ? 0 : item.StayAwayPay)
                + (item.TakeOffPay == null ? 0 : item.TakeOffPay);
            item.ActionPay = (item.AllPay == null ? 0 : item.AllPay)
                            - (item.OutPay == null ? 0 : item.OutPay);
        }

        /// <summary>
        /// 更新工资总额
        /// </summary>
        /// <param name="id"></param>
        public void UpdateAll(int id)
        {
            var actionpay = this.m_ServiceBase.Query().Where(t => t.SalariesID == id).Sum(t => t.ActionPay);
            var query = this.service.GetGenericService<HR_GZ_Salaries>().Get(id.ToString());
            query.AllMoney = actionpay;
            this.service.GetGenericService<HR_GZ_Salaries>().Update(query, null);
        }

        public ActionResult Compute(List<string> ids)
        {
            var query = this.m_ServiceBase.Query().Where(t => ids.Contains(t.ID));
            if (query == null)
            {
                return OperateResult(false, "请选择单据进行计算", null);
            }

            var Salaries = this.service.GetGenericService<HR_GZ_Salaries>().Get(query.First().SalariesID.ToString());
            if (Salaries == null)
            {
                return OperateResult(false, "请选择单据进行计算", null);
            }
            if (Salaries.IsSeal)
            {
                return OperateResult(false, "当前工资表已经被封存，不能再计算", null);
            }

            int id = query.First().SalariesID;
            var list = query.Select(t => t.PersonID).ToList().ToArray();
            string personids = string.Join(",", list);

            var user = AuthorizationService.CurrentUserID;
            SqlServerHelper help = new SqlServerHelper();
            var re = help.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "SP_HR_GZ_Salaries", new System.Data.SqlClient.SqlParameter("@id", id), new System.Data.SqlClient.SqlParameter("@user", user), new System.Data.SqlClient.SqlParameter("@personids", personids));
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
