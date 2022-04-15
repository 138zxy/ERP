using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class CarOilPriceSettingController : BaseController<CarOilPriceSetting, string>
    {
        public override ActionResult Index()
        {
            //燃料类型
            var StuffTypes = this.service.GetGenericService<StuffType>().Query().Where(t => t.TypeID == StuffTypeEnum.Oil.ToString());
            ViewBag.StuffTypes = new SelectList(StuffTypes, "ID", "StuffTypeName");
            
            return base.Index();
        }

    }
}
