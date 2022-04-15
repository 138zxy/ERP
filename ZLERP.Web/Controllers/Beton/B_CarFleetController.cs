using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_CarFleetController : BaseController<B_CarFleet, string>
    {
        //
        // GET: /B_CarFleet/

        public override ActionResult Index()
        {
            var CarTemplet = this.service.GetGenericService<B_FareTemplet>().All("", "ID", true);

            ViewBag.CarTemplet = new SelectList(CarTemplet, "ID", "FareTempletName");
            return base.Index();
        }


        public override ActionResult Add(B_CarFleet entity)
        {
            entity.PayMoney = entity.PaidIn;
            entity.PaidOwing = entity.PaidIn;
            entity.PrePay = entity.PrepayInit;
            return base.Add(entity);
        }


        public override ActionResult Update(B_CarFleet entity)
        {
            var su = this.m_ServiceBase.Get(entity.ID);

            //如果期初应付金额发生变化
            if (entity.PaidIn != su.PaidIn)
            {
                decimal exch = su.PaidIn - entity.PaidIn;
                //期初欠款=期初应付-已付
                su.PaidOwing = entity.PaidIn - su.PaidOut;
                su.PayMoney = su.PayMoney - exch;
            }
            if (entity.PrepayInit != su.PrepayInit)
            {
                var exchIni = su.PrepayInit - entity.PrepayInit;
                su.PrePay = su.PrePay - exchIni;
            }
            su.FleetCode = entity.FleetCode;
            su.FleetName = entity.FleetName;
            su.FleetType = entity.FleetType;
            su.Adrress = entity.Adrress;
            su.Linker = entity.Linker;
            su.LinkPhone = entity.LinkPhone; 
            su.AccountNo = entity.AccountNo;
            su.TaxNo = entity.TaxNo;
            su.PaidIn = entity.PaidIn;
            su.PrepayInit = entity.PrepayInit; 
            su.Remark = entity.Remark;
            su.CarTemplet = entity.CarTemplet;
            return base.Update(su);
        } 

    }
}
