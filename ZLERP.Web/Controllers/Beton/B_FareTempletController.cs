using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_FareTempletController : BaseController<B_FareTemplet, string>
    {
        //
        // GET: /B_CarFleet/

        public override ActionResult Index()
        {
            List<SelectListItem> BalaneType = new List<SelectListItem>();
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type1, Value = B_Common.BalaneType.type1 });
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type2, Value = B_Common.BalaneType.type2});
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type3, Value = B_Common.BalaneType.type3 });
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type4, Value = B_Common.BalaneType.type4 });

            ViewBag.BalaneType = BalaneType;
            return base.Index();
        }

        public override ActionResult Add(B_FareTemplet B_FareTemplet)
        {
            return base.Add(B_FareTemplet);
        }

        public override ActionResult Update(B_FareTemplet B_FareTemplet)
        {
            return base.Update(B_FareTemplet);
        }
    }
}
