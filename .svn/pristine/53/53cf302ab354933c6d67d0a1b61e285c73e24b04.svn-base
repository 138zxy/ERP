using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.JBZKZ12; 

namespace ZLERP.Web.Controllers.Beton
{
    public class B_TranYingSFController : BaseController<B_CarFleet, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var FinanceTypes = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
            FinanceTypes.Add(new SelectListItem
            {
                Text = B_Common.PayType.type,
                Value = B_Common.PayType.type
            });
            ViewBag.FinanceType = FinanceTypes;  
            ViewBag.FinancePerType = FinanceTypes;
            return base.Index();
        }

        public ActionResult Pay(B_PerPay B_PerPay)
        {
            if (string.IsNullOrWhiteSpace(B_PerPay.PayType))
            {
                return OperateResult(false, "付款方式不能为空", null);
            }
            List<b_perpay> perpays = JsonHelper.Instance.Deserialize<List<b_perpay>>(B_PerPay.Records);
            if (perpays == null || perpays.Count <= 0)
            {
                return OperateResult(false, "对采购单付款明细不能没有", null);
            }
            if (B_PerPay.InDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            var piaoinService = this.service.GetGenericService<B_TranBalance>();
            foreach (var p in perpays)
            {
                var piaoin = piaoinService.Get(p.ID.ToString());
                if (piaoin.PayOwing < p.PayMoney2 + p.PayFavourable2)
                {
                    return OperateResult(false, string.Format("结算单{0}的输入的金额不正确，请确认", piaoin.BaleNo), null);
                }
            }
            if (B_PerPay.PayType == B_Common.PayType.type)
            {
                var su = this.service.GetGenericService<B_CarFleet>().Get(B_PerPay.UnitID);
                if (su.PrePay < perpays.Sum(t => t.PayMoney2))
                {
                    return OperateResult(false, "当前供应商的预付款余额不足，无法支付", null);
                }
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.PayTran(B_PerPay, perpays);
            return Json(resultInfo);
        }

        public ActionResult PayIni(B_TranYingSFrec B_TranYingSFrec)
        {
            if (string.IsNullOrWhiteSpace(B_TranYingSFrec.UnitID))
            {
                return OperateResult(false, "请选择对期初付款的运输单位", null);
            }
            var supply = this.service.GetGenericService<B_CarFleet>().Get(B_TranYingSFrec.UnitID);
            if (supply.PaidOwing <B_TranYingSFrec.FinanceMoney + B_TranYingSFrec.PayFavourable)
            {
                return OperateResult(false, "输入的金额出错，还款额不能超过欠款额", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.PayTranIni(B_TranYingSFrec);
            return Json(resultInfo);
        }
    }
}
