using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers.Material
{
    public class M_TranYingYFController : BaseController<SupplyInfo, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.FinanceType = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
            return base.Index();
        }

        public ActionResult Pay(M_TranYingSFrec M_TranYingSFrec)
        {
            if (string.IsNullOrWhiteSpace(M_TranYingSFrec.UnitID))
            {
                return OperateResult(false, "请选择供应商进行预付款", null);
            }
            if (string.IsNullOrWhiteSpace(M_TranYingSFrec.PayType))
            {
                return OperateResult(false, "请选择付款方式", null);
            }
        
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.PerPayTran(M_TranYingSFrec);
            return Json(resultInfo);
        }

    }
}
