﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_TranYingSFrecController : BaseController<B_TranYingSFrec, string>
    {
        //
        // GET: /M_Base/

        public override ActionResult Index()
        {
            return base.Index();
        } 
    }
}