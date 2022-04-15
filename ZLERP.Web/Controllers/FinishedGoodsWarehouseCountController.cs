using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class FinishedGoodsWarehouseCountController : BaseController<FinishedGoodsWarehouseCount, int>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
    }
}
