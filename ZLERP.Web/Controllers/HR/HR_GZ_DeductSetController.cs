﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_DeductSetController : HRBaseController<HR_GZ_DeductSet, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
    }
}