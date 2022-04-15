using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_LeaveController : HRBaseController<HR_KQ_Leave, string>
    {
        public override ActionResult Index()
        {
 
            ViewBag.Leave = GetBaseData(BaseDataType.请假类型);
            return base.Index();
        }

        //public override  ActionResult GenerateOrderNo()
        //{
        //    return base.GenerateOrderNo("Q");
        //}

  

        public override ActionResult Add(HR_KQ_Leave entity)
        {
            entity.Condition = PCondition.Ini;
            return base.Add(entity);
        }
        /// <summary>
        /// 状态变化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeCondition(int type, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return OperateResult(false, "请选择相应的单据", null);
            }
            int status = 0;
            switch (type)
            {
                case 1: //审核
                    if (true)
                    {
                        var qj = this.m_ServiceBase.Get(id);
                        if (qj.Condition != PCondition.Ini)
                        {
                            return OperateResult(false, "该单据不是草稿状态不能审核", null);
                        }
                        qj.Condition = PCondition.Audit;
                        qj.Auditor = AuthorizationService.CurrentUserID;
                        qj.AuditTime = DateTime.Now;
                        this.m_ServiceBase.Update(qj, null);
                        status = 0;
                    }
                    break;
                case 2: //反审
                    if (true)
                    {
                        var qj = this.m_ServiceBase.Get(id);
                        if (qj.Condition != PCondition.Audit)
                        {
                            return OperateResult(false, "该单据不是已审核状态不能反审", null);
                        } 
                        qj.Condition = PCondition.Ini;
                        qj.Auditor = "";
                        qj.AuditTime = null;
                        this.m_ServiceBase.Update(qj, null);
                        status = 0;
                    }
                    break; 
                default:
                    status = 1;
                    break;

            }
            if (status == 0)
            {
                return OperateResult(true, "操作成功", null);
            }
            else
            {
                return OperateResult(false, "参数错误", null);
            }
        }
    }
}
