using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PanDianLibController : BaseController<SC_PiaoIn, string>
    {
        //
        // GET: /SC_Base/
 
        public override ActionResult Index()
        {
            return base.Index();
        }
        
   
    }
}
