using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_YingYFController : BaseController<SC_Supply, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.FinanceType = new SupplyChainHelp().GetPerPayTypes();
            return base.Index();
        }

        public  ActionResult Pay(SC_YingSFrec SC_YingSFrec)
        {
            if (SC_YingSFrec.UnitID <= 0)
            {
                return OperateResult(false, "请选择供应商进行预付款", null);
            }
            if (string.IsNullOrWhiteSpace(SC_YingSFrec.PayType))
            {
                return OperateResult(false, "请选择付款方式", null);
            }
            if (SC_YingSFrec.FinanceMoney <= 0)
            {
                return OperateResult(false, "预付款的金额必须大于0", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.SupplyChainService.PerPay(SC_YingSFrec);
            return Json(resultInfo);
        }
    }
}
