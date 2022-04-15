using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.JBZKZ12;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_YingSFController : BaseController<SC_Supply, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.FinanceType = new SupplyChainHelp().GetFinanceTypes();
            ViewBag.FinancePerType = new SupplyChainHelp().GetPerPayTypes(); 
            return base.Index();
        }

        public ActionResult Pay(V_PerPay V_PerPay)
        {
            if (string.IsNullOrWhiteSpace(V_PerPay.PayType))
            {
                return OperateResult(false, "付款方式不能为空", null);
            }
            List<perpay> perpays = JsonHelper.Instance.Deserialize<List<perpay>>(V_PerPay.Records);
            if (perpays == null || perpays.Count <= 0)
            {
                return OperateResult(false, "对采购单付款明细不能没有", null);
            }
            if (V_PerPay.InDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            var piaoinService = this.service.GetGenericService<SC_PiaoIn>();
            foreach (var p in perpays)
            {
                var piaoin = piaoinService.Get(p.ID.ToString());
                if (piaoin.PayOwing < p.PayMoney2 + p.PayFavourable2)
                {
                    return OperateResult(false, string.Format("入库单{0}的输入的金额不正确，请确认", piaoin.InNo), null);
                }
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.SupplyChainService.PayPur(V_PerPay, perpays);
            return Json(resultInfo);
        }

        public ActionResult PayIni(SC_YingSFrec SC_YingSFrec)
        {
            if (SC_YingSFrec.UnitID <= 0)
            {
                return OperateResult(false, "请选择对期初付款的供应商", null);
            }
            var supply = this.service.GetGenericService<SC_Supply>().Get(SC_YingSFrec.UnitID.ToString());
            if (supply.PaidOwing < SC_YingSFrec.FinanceMoney + SC_YingSFrec.PayFavourable)
            {
                return OperateResult(false, "输入的金额出错，还款额不能超过欠款额", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.SupplyChainService.PayIni(SC_YingSFrec);
            return Json(resultInfo);
        }
    }
}
