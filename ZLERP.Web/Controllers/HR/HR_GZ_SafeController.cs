using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_SafeController : HRBaseController<HR_GZ_Safe, string>
    {
        public override ActionResult Index()
        {
    
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_Safe HR_GZ_Safe)
        {
            return base.Add(HR_GZ_Safe);
        }

        public override ActionResult Update(HR_GZ_Safe HR_GZ_Safe)
        {
            return base.Update(HR_GZ_Safe);
        }
    }
}
