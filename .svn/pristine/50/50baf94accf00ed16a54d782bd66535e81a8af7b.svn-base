using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarFeesController : BaseController<CarFees, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");
            var CarDealingsList = this.service.GetGenericService<CarDealings>().Query().Where(t => !t.IsOff).ToList();
            ViewBag.CarDealingsList = new SelectList(CarDealingsList, "ID", "Name");
            return base.Index();
        }
        public override ActionResult Add(CarFees entity)
        {
            return base.Add(entity);
        }

        public override ActionResult Update(CarFees CarFees)
        {
            return base.Update(CarFees);
        }
    }
}
