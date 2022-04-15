using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarMaterialController : BaseController<CarMaterial, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");
            return base.Index();
        }
        public override ActionResult Add(CarMaterial entity)
        {
            return base.Add(entity);
        }

        public override ActionResult Update(CarMaterial CarMaterial)
        {
            return base.Update(CarMaterial);
        }
    }
}
