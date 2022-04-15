using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_ResultController : BaseController<SC_Fixed_Result, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
        
    }
}
