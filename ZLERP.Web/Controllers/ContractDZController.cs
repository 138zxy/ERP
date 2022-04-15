﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class ContractDZController : BaseController<ContractDZ, int>
    {

        public override ActionResult Add(ContractDZ ContractDZ)
        {
            return base.Add(ContractDZ);
        }

    }
}