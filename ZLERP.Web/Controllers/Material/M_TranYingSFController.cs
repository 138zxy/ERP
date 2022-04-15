using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Material;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.JBZKZ12;

namespace ZLERP.Web.Controllers.Material
{
    public class M_TranYingSFController : BaseController<SupplyInfo, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var FinanceTypes = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
            FinanceTypes.Add(new SelectListItem
            {
                Text = M_Common.PayType.type,
                Value =  M_Common.PayType.type
            });
            ViewBag.FinanceType = FinanceTypes;  
            ViewBag.FinancePerType = FinanceTypes;
            return base.Index();
        }

        public ActionResult Pay(M_PerPay M_PerPay)
        {
            if (string.IsNullOrWhiteSpace(M_PerPay.PayType))
            {
                return OperateResult(false, "付款方式不能为空", null);
            }
            List<m_perpay> perpays = JsonHelper.Instance.Deserialize<List<m_perpay>>(M_PerPay.Records);
            if (perpays == null || perpays.Count <= 0)
            {
                return OperateResult(false, "对运输付款明细不能没有", null);
            }
            if (M_PerPay.InDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            var piaoinService = this.service.GetGenericService<M_TranBalance>();
            foreach (var p in perpays)
            {
                var piaoin = piaoinService.Get(p.ID.ToString());
                if (piaoin.PayOwing < p.PayMoney2 + p.PayFavourable2)
                {
                    return OperateResult(false, string.Format("结算单{0}的输入的金额不正确，请确认", piaoin.BaleNo), null);
                }
            }
            if (M_PerPay.PayType == M_Common.PayType.type)
            {
                var su = this.service.GetGenericService<SupplyInfo>().Get(M_PerPay.StockPactID);
                if (su.PrePay < perpays.Sum(t => t.PayMoney2))
                {
                    return OperateResult(false, "当前供应商的预付款余额不足，无法支付", null);
                }
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.PayTran(M_PerPay, perpays);
            return Json(resultInfo);
        }

        public ActionResult PayIni(M_TranYingSFrec M_TranYingSFrec)
        {
            if (string.IsNullOrWhiteSpace(M_TranYingSFrec.UnitID))
            {
                return OperateResult(false, "请选择对期初付款的运输单位", null);
            }
            var supply = this.service.GetGenericService<SupplyInfo>().Get(M_TranYingSFrec.UnitID);
            if (supply.PaidOwing < M_TranYingSFrec.FinanceMoney + M_TranYingSFrec.PayFavourable)
            {
                return OperateResult(false, "输入的金额出错，还款额不能超过欠款额", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.PayTranIni(M_TranYingSFrec);
            return Json(resultInfo);
        }
    }
}
