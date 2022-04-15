using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model.Material;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers.Material
{
    public class M_TransPriceController : BaseController<M_TransPrice, string>
    {


        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(M_TransPrice M_TransPrice)
        {
            return base.Add(M_TransPrice);
        }
        public override ActionResult Update(M_TransPrice M_TransPrice)
        {
            return base.Update(M_TransPrice);
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
                    M_TransPrice sd = this.service.GetGenericService<M_TransPrice>().Get(id);
                    if (sd.AuditStatus == 0 && sd.AuditStatus2 == 0)
                    {
                        sd.AuditStatus = 1;
                        sd.Auditor = AuthorizationService.CurrentUserID;
                        sd.AuditTime = DateTime.Now;
                        this.service.GetGenericService<M_TransPrice>().Update(sd);
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
                    M_TransPrice sd = this.service.GetGenericService<M_TransPrice>().Get(id);
                    if (sd.AuditStatus == 1 && sd.AuditStatus2 == 0)
                    {
                        sd.AuditStatus = 0;
                        sd.Auditor = AuthorizationService.CurrentUserID;
                        sd.AuditTime = DateTime.Now;
                        this.service.GetGenericService<M_TransPrice>().Update(sd);
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
                    M_TransPrice sd = this.service.GetGenericService<M_TransPrice>().Get(id);
                    if (sd.AuditStatus2 == 0 && sd.AuditStatus == 1)
                    {
                        sd.AuditStatus2 = 1;
                        sd.Auditor2 = AuthorizationService.CurrentUserID;
                        sd.AuditTime2 = DateTime.Now;
                        this.service.GetGenericService<M_TransPrice>().Update(sd);
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
                    M_TransPrice sd = this.service.GetGenericService<M_TransPrice>().Get(id);
                    if (sd.AuditStatus2 == 1 && sd.AuditStatus == 1)
                    {
                        sd.AuditStatus2 = 0;
                        sd.Auditor2 = AuthorizationService.CurrentUserID;
                        sd.AuditTime2 = DateTime.Now;
                        this.service.GetGenericService<M_TransPrice>().Update(sd);
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
