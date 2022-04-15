using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class StuffinfoSpecController : BaseController<StuffinfoSpec,int?>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
        
        [HttpPost]
        public override ActionResult Add(StuffinfoSpec _StuffinfoSpec)
        {            
            ActionResult result = base.Add(_StuffinfoSpec);
            return result;
        }
        
        [HttpPost]
        public override ActionResult Update(StuffinfoSpec _StuffinfoSpec)
        {
            ActionResult result = base.Update(_StuffinfoSpec);
            return result;
        }

   }
}
