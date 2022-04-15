
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class HandleRecordController : BaseController<HandleRecord, int>
    {
        public override ActionResult Index()
        {
            IList<ProductLine> items = base.service.ProductLine.All("IsUsed = 1", "ID", true);
            ((dynamic)base.ViewBag).ProductLineList = new SelectList(items, "ID", "ProductLineName");
            //材料列表
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName",
                "ID",
                "IsUsed=1",
                "StuffName",
                true, "");

            return base.Index();
        }
    }    
}
