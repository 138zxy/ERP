using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_FareTempletDel2Controller : BaseController<B_FareTempletDel2, string>
    {
        //
        // GET: /B_CarFleet/

        public override ActionResult Index()
        {

            return base.Index();
        }

        public override ActionResult Add(B_FareTempletDel2 B_FareTempletDel2)
        {
            return base.Add(B_FareTempletDel2);
        }

        public override ActionResult Update(B_FareTempletDel2 B_FareTempletDel2)
        {
            return base.Update(B_FareTempletDel2);
        }
    }
}
