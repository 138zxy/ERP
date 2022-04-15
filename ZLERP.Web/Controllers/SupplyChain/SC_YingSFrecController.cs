using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_YingSFrecController : BaseController<SC_YingSFrec, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        } 
    }
}
