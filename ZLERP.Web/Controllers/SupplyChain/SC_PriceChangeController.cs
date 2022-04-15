using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Resources;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PriceChangeController : BaseController<SC_Goods, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public ActionResult PriceChange(SC_PriceChange SC_PriceChange)
        {
            if (SC_PriceChange.GoodsID <= 0)
            {
                return OperateResult(false, "请选择商品进行调价", null);
            }
            if (SC_PriceChange.AlferPrice <= 0)
            {
                return OperateResult(false, "你调整的价格不合理", null);
            }
            ResultInfo resultInfo = new ResultInfo();

            resultInfo = this.service.SupplyChainService.PriceChange(SC_PriceChange);

            return Json(resultInfo);
        }

 


    }
}
