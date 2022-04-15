using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_TranYingYFController : BaseController<B_CarFleet, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.FinanceType = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
            return base.Index();
        }

        public ActionResult Pay(B_TranYingSFrec B_TranYingSFrec)
        {
            if (string.IsNullOrWhiteSpace(B_TranYingSFrec.UnitID))
            {
                return OperateResult(false, "请选择供应商进行预付款", null);
            }
            if (string.IsNullOrWhiteSpace(B_TranYingSFrec.PayType))
            {
                return OperateResult(false, "请选择付款方式", null);
            }
            //if (B_TranYingSFrec.FinanceMoney <= 0)
            //{
            //    return OperateResult(false, "预付款的金额必须大于0", null);
            //}
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.PerPayTran(B_TranYingSFrec);
            return Json(resultInfo);
        }

    }
}
