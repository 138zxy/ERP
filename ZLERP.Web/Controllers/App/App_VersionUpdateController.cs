using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.App;

namespace ZLERP.Web.Controllers.App
{
    public class App_VersionUpdateController : BaseController<App_VersionUpdate, int?>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
    }
}
