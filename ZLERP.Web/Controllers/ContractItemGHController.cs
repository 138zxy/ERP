using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class ContractItemGHController : BaseController<ContractItemGH, int>
    {

        public override ActionResult Add(ContractItemGH contractItemGH)
        {
            return base.Add(contractItemGH);
        }
        public override ActionResult Update(ContractItemGH contractItemGH)
        {
            return base.Update(contractItemGH);
        }
        /// <summary>
        /// 判断砼强度是否属于合同里面
        /// </summary>
        /// <param name="contractid"></param>
        /// <param name="constrength"></param>
        /// <returns></returns>
        public ActionResult getContractItem(string contractid, string constrength)
        {
            var cons = this.service.GetGenericService<ContractItemGH>().Find("contractid='" + contractid + "' and constrength='" + constrength + "'", 1, 100, "", "");
            if (cons.ToList().Count > 0)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult Audit(ContractItemGH ContractItemGH)
        {
            try
            {
                this.service.ContractItemGH.Audit(ContractItemGH);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ContractItemGH.ID, null, "合同砼强度明细审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="consMixID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string consItemsID, int auditStatus)
        {
            try
            {
                this.service.ContractItemGH.UnAudit(consItemsID);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, consItemsID, null, "合同砼强度明细取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }

        /// <summary>
        /// 取得砼强度下拉列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult FindConStrength(JqGridRequest request, string condition)
        {
            IList<ContractItemGH> list = m_ServiceBase.All(condition, "ConStrength", true);
            JqGridData<ContractItemGH> data = new JqGridData<ContractItemGH>()
            {
                page = 1,
                records = list.Count,
                pageSize = list.Count,
                rows = list
            };
            return Json(data);
        }

    }
}
