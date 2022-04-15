using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_SupplyController : BaseController<SC_Supply, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.SupplierType =new SupplyChainHelp().GetSupplyType();
            ViewBag.FinanceType = new SupplyChainHelp().GetPerPayTypes();
            return base.Index();
        }

        public override ActionResult Add(SC_Supply SC_Supply)
        {
            SC_Supply.PayMoney = SC_Supply.PaidIn;
            SC_Supply.PaidOwing = SC_Supply.PaidIn; 
            SC_Supply.PrePay = SC_Supply.PrepayInit;
            return base.Add(SC_Supply);
        }

        public override ActionResult Update(SC_Supply entity)
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
            su.SupplierName = entity.SupplierName;
            su.SupplierType = entity.SupplierType;
            su.Adrress = entity.Adrress;
            su.Linker = entity.Linker;
            su.LinkPhone = entity.LinkPhone;
            su.LinkPhone2 = entity.LinkPhone2;
            su.AccountNo = entity.AccountNo;
            su.TaxNo = entity.TaxNo;
            su.PaidIn = entity.PaidIn;
            su.PrepayInit = entity.PrepayInit;
            su.Discount = entity.Discount;
            su.FinanceType = entity.FinanceType;
            su.IsOff = entity.IsOff;
            su.Remark = entity.Remark;
            return base.Update(su);
        }
    }
}
