using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_FareTempletDel1Controller : BaseController<B_FareTempletDel1, string>
    {
        //
        // GET: /B_CarFleet/

        public override ActionResult Index()
        {

            return base.Index();
        }

        public override ActionResult Add(B_FareTempletDel1 B_FareTempletDel1)
        {
            return base.Add(B_FareTempletDel1);
        }

        public override ActionResult Update(B_FareTempletDel1 B_FareTempletDel1)
        {
            return base.Update(B_FareTempletDel1);
        }
    }
}
