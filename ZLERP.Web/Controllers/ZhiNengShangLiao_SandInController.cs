using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model.Beton;
using System.Collections.Specialized;


namespace ZLERP.Web.Controllers
{
    public class ZhiNengShangLiao_SandInController : BaseController<ZhiNengShangLiao_SandIn, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
    }
}