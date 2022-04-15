using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarMaintainDelController : BaseController<CarMaintainDel, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {

            return base.Index();
        }
        public override ActionResult Add(CarMaintainDel CarMaintainDel)
        {
            var carMaintain = this.service.GetGenericService<CarMaintain>().Get(CarMaintainDel.MaintainID);
            if (carMaintain.IsOver)
            {
                return OperateResult(false, "维修已经完工不能操作", null);
            }
            var re = base.Add(CarMaintainDel);
            UpdateMain(carMaintain);
            return re;
        }

        public override ActionResult Update(CarMaintainDel CarMaintainDel)
        {
            var carMaintain = this.service.GetGenericService<CarMaintain>().Get(CarMaintainDel.MaintainID);
            if (carMaintain.IsOver)
            {
                return OperateResult(false, "维修已经完工不能操作", null);
            }
            var re = base.Update(CarMaintainDel);
            UpdateMain(carMaintain);
            return re;
        }

        public override ActionResult Delete(string[] id)
        {
            var obj = this.m_ServiceBase.Get(id[0]);
            var carMaintain = this.service.GetGenericService<CarMaintain>().Get(obj.MaintainID);
            if (carMaintain.IsOver)
            {
                return OperateResult(false, "维修已经完工不能操作", null);
            }
            var re = base.Delete(id);
            UpdateMain(carMaintain);
            return re;
        }

        private void UpdateMain(CarMaintain carMaintain)
        {
            var list = this.m_ServiceBase.Query().Where(t => t.MaintainID == carMaintain.ID);
            var hourMan = list.Sum(t => t.ManHour);
            var allCost = list.Sum(t => t.AllCost);
            carMaintain.RepairTime = hourMan;
            carMaintain.RepairPirce = allCost;
            this.service.GetGenericService<CarMaintain>().Update(carMaintain, null);
        }
    }
}
