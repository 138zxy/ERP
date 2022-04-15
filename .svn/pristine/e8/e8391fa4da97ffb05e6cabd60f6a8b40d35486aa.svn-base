using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarTowMainController : BaseController<CarTowMain, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");

            var CarDealingsList = this.service.GetGenericService<CarDealings>().Query().Where(t => !t.IsOff).ToList();
            ViewBag.CarDealingsList = new SelectList(CarDealingsList, "ID", "Name");

            List<SelectListItem> Grades = new List<SelectListItem>();
            Grades.Add(new SelectListItem { Text = "一级", Value = "一级" });
            Grades.Add(new SelectListItem { Text = "二级", Value = "二级" });
            Grades.Add(new SelectListItem { Text = "三级", Value = "三级" }); 
            ViewBag.Grades = Grades; 
            return base.Index();
        }
        public override ActionResult Add(CarTowMain entity)
        {
            return base.Add(entity);
        }

        public override ActionResult Update(CarTowMain CarKeep)
        {
            return base.Update(CarKeep);
        }
    }
}
