using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarInsuranceController : BaseController<CarInsurance, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");

            var CarDealingsList = this.service.GetGenericService<CarDealings>().Query().Where(t => !t.IsOff && t.DealingsType == "保险单位").ToList();
            ViewBag.CarDealingsList = new SelectList(CarDealingsList, "ID", "Name");
            return base.Index();
        }
        public override ActionResult Add(CarInsurance CarInsurance)
        {
            return base.Add(CarInsurance);
        }

        public override ActionResult Update(CarInsurance CarInsurance)
        {
            return base.Update(CarInsurance);
        }

        public override ActionResult Delete(string[] id)
        {
            var re = base.Delete(id);
            foreach (var mainid in id)
            {
                var Dels = this.service.GetGenericService<CarInsuranceItem>().Query().Where(t => t.CarInsuranceID == mainid);
                foreach (var del in Dels)
                {
                    this.service.GetGenericService<CarInsuranceItem>().Delete(del);
                }
            }
            return re;

        }
    }
}
