﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers.Material
{
    public class M_FinanceRecordController : BaseController<M_FinanceRecord, string>
    {
        //
        // GET: /M_Base/

        public override ActionResult Index()
        {
            return base.Index();
        } 
    }
}