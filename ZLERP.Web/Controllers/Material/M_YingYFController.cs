using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers.Material
{
    public class M_YingYFController : BaseController<SupplyInfo, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.FinanceType = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
            return base.Index();
        }

        public ActionResult Pay(M_YingSFrec M_YingSFrec)
        {
            if (string.IsNullOrWhiteSpace(M_YingSFrec.UnitID))
            {
                return OperateResult(false, "请选择供应商进行预付款", null);
            }
            if (string.IsNullOrWhiteSpace(M_YingSFrec.PayType))
            {
                return OperateResult(false, "请选择付款方式", null);
            }
            //if (M_YingSFrec.FinanceMoney <= 0)
            //{
            //    return OperateResult(false, "预付款的金额必须大于0", null);
            //}
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.PerPay(M_YingSFrec);
            return Json(resultInfo);
        }

    }
}
