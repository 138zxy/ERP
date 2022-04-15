using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_PanDianController : BaseController<SC_Fixed_PanDian, string>
    {
        public override ActionResult Index()
        {
            int IsInCheck = 0;
            var query = this.m_ServiceBase.Query().FirstOrDefault();
            if (query != null)
            {
                IsInCheck = 1;
            }
            ViewBag.IsInCheck = IsInCheck;
            return base.Index();
        }

        public override ActionResult Update(SC_Fixed_PanDian entity)
        {
            var pan = this.m_ServiceBase.Get(entity.ID);
            pan.Quantity = entity.Quantity;
            if (pan.Quantity > 0)
            {
                pan.Quantity = 1;
            }
            if (pan.Quantity < 0)
            {
                pan.Quantity = 0;
            }
            if (pan.AutoQuantity == 1)
            {
                if (pan.Quantity < pan.AutoQuantity)
                {
                    pan.CheckResult = SC_Common.FxiedCondition.PanKui;
                }
                else
                {
                    pan.CheckResult = SC_Common.FxiedCondition.Normal;
                }
            }
            else
            {
                if (pan.Quantity > pan.AutoQuantity)
                {
                    pan.CheckResult = SC_Common.FxiedCondition.PanYing;
                }
                else
                {
                    pan.CheckResult = SC_Common.FxiedCondition.PanKui;
                }
            }

            return base.Update(pan);
        }
        /// <summary>
        /// 开始盘点
        /// </summary>
        /// <returns></returns>
        public ActionResult Pandian()
        {
            var res = this.service.SupplyChainService.Pandian();
            return Json(res);
        }
        /// <summary>
        /// 取消操作
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            var res = this.service.SupplyChainService.Cancel();
            return Json(res);
        }
        /// <summary>
        /// 完成盘点操作
        /// </summary>
        /// <returns></returns>
        public ActionResult Over()
        {
            var query = this.m_ServiceBase.Query().Where(t => t.Condition == SC_Common.FxiedCondition.Maintain || t.Condition == SC_Common.FxiedCondition.Circulate).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "存在维修中和已借出的资产，暂不能盘点，请先处理", "");
            }
            var res = this.service.SupplyChainService.Over();
            return Json(res);
        }
    }
}
