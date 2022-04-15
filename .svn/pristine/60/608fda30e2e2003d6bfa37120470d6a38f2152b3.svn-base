using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class StockPactPriceSetController : BaseController<StockPactPriceSet, int>
    {
        public override ActionResult Add(StockPactPriceSet StockPactPriceSet)
        {
            var id = StockPactPriceSet.StockPactID;
            var query=this.service.GetGenericService<StockPact>().Get(id);
            if (query == null)
            {
                return OperateResult(false, "请选择一个采购合同，在添加调价信息", null);
            }
            var stuffid = StockPactPriceSet.StuffID;
            if (query.StuffID != stuffid && query.StuffID1 != stuffid && query.StuffID2 != stuffid && query.StuffID3 != stuffid && query.StuffID4 != stuffid)
            {
                return OperateResult(false, "调价的材料必须在合同中存在", null);
            }
            return base.Add(StockPactPriceSet);
        }
        public override ActionResult Update(StockPactPriceSet StockPactPriceSet)
        {

            return base.Update(StockPactPriceSet);
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    StockPactPriceSet sd = this.service.GetGenericService<StockPactPriceSet>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus == 0 && sd.AuditStatus2 == 0)
                    {
                        sd.AuditStatus = 1;
                        sd.Auditor = AuthorizationService.CurrentUserID;
                        sd.AuditTime = DateTime.Now;
                        this.service.GetGenericService<StockPactPriceSet>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 批量取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit_N(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    StockPactPriceSet sd = this.service.GetGenericService<StockPactPriceSet>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus == 1 && sd.AuditStatus2 == 0)
                    {
                        sd.AuditStatus = 0;
                        sd.Auditor = AuthorizationService.CurrentUserID;
                        sd.AuditTime = DateTime.Now;
                        this.service.GetGenericService<StockPactPriceSet>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 批量二次审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit2(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    StockPactPriceSet sd = this.service.GetGenericService<StockPactPriceSet>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus2 == 0&&sd.AuditStatus==1)
                    {
                        sd.AuditStatus2 = 1;
                        sd.Auditor2 = AuthorizationService.CurrentUserID;
                        sd.AuditTime2 = DateTime.Now;
                        this.service.GetGenericService<StockPactPriceSet>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 批量取消二次审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit2_N(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    StockPactPriceSet sd = this.service.GetGenericService<StockPactPriceSet>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus2 == 1 && sd.AuditStatus == 1)
                    {
                        sd.AuditStatus2 = 0;
                        sd.Auditor2 = AuthorizationService.CurrentUserID;
                        sd.AuditTime2 = DateTime.Now;
                        this.service.GetGenericService<StockPactPriceSet>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
    }
}
