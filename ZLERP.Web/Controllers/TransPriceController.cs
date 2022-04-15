
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class TransPriceController : BaseController<TransPrice, int?>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(TransPrice TransPrice)
        {
            return base.Add(TransPrice);
        }

        public override ActionResult Update(TransPrice TransPrice)
        {
            return base.Update(TransPrice);
        }
    }
}
