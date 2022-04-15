using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;
using System.Web.Helpers;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Model.Material;
namespace ZLERP.Web.Controllers
{
    public class StockPlanController : BaseController<StockPlan,string>
    {
        //
        // GET: /StockPlan/

        public override System.Web.Mvc.ActionResult Index()
        {
            //新计划单，正在进料，暂停进料，进料完成，作废
            List<SelectListItem> ItemsTypes = new List<SelectListItem>();
            ItemsTypes.Add(new SelectListItem { Text = M_Common.PlanStatus.Ini, Value = M_Common.PlanStatus.Ini });
            ItemsTypes.Add(new SelectListItem { Text = M_Common.PlanStatus.On, Value = M_Common.PlanStatus.On });
            ItemsTypes.Add(new SelectListItem { Text = M_Common.PlanStatus.Over, Value = M_Common.PlanStatus.Over });
            ItemsTypes.Add(new SelectListItem { Text = M_Common.PlanStatus.Stop, Value = M_Common.PlanStatus.Stop });
            ItemsTypes.Add(new SelectListItem { Text = M_Common.PlanStatus.Cancel, Value = M_Common.PlanStatus.Cancel });
            ViewBag.ExecStatus = ItemsTypes;
            return base.Index();
        }
        public ActionResult GenerateOrderNo()
        {
            string orderNo = GetGenerateOrderNo();
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public string GetGenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateOrderNo();
            return string.Format("{0}{1}", "S", orderNo);
        }

        public override ActionResult Add(StockPlan entity)
        {
            return base.Add(entity);
        }

        public ActionResult ChangeCondition(string id, int type)
        {
            var query = this.m_ServiceBase.Get(id);
            switch (type)
            {
                case 0:
                    //审核
                    query.AuditStatus = 0;
                    break;
                case 1:
                    //审核
                    query.AuditStatus = 1;
                    query.AuditTime = DateTime.Now;
                    query.Auditor = AuthorizationService.CurrentUserName;
                    break;
                case 2:
                    //正在进料
                    query.ExecStatus = M_Common.PlanStatus.On;
                    break;
                case 3:
                    //暂停进料
                    query.ExecStatus = M_Common.PlanStatus.Stop;
                    break;
                case 4:
                    //进料完成
                    query.ExecStatus = M_Common.PlanStatus.Over;
                    break;
                case 5:
                    //作废
                    query.ExecStatus = M_Common.PlanStatus.Cancel;
                    break;
                default:
                    return OperateResult(false, "操作失败", null); ;

            }
            query.Modifier = AuthorizationService.CurrentUserName;
            query.ModifyTime = DateTime.Now;
            this.m_ServiceBase.Update(query, null);
            return OperateResult(true, "操作成功", null);
        }

   
       
    }
}
