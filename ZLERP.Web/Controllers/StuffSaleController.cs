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
    public class StuffSaleController : BaseController<StuffSale, string>
    {
        public override ActionResult Index()
        {
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);
            ViewBag.SupplyList = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "ID", "SupplyName", true);
            ViewBag.CompanyList = HelperExtensions.SelectListData<Company>("CompName", "ID", "CompName", true);
            ViewBag.CarList = HelperExtensions.SelectListData<Car>("CarNo", "ID", "CarNo", true);
            ViewBag.UserList = HelperExtensions.SelectListData<User>("TrueName", "ID", "TrueName", true);
            return base.Index();
        }

        [HttpPost]
        public override ActionResult Add(StuffSale _StuffSale)
        {
            ActionResult result = base.Add(_StuffSale);
            return result;
        }

        [HttpPost]
        public override ActionResult Update(StuffSale _StuffSale)
        {
            ActionResult result = base.Update(_StuffSale);
            return result;
        }

        [HttpPost]
        public override ActionResult Delete(string[] id)
        {
            ActionResult result = base.Delete(id);
            return result;
        }
    }
}
