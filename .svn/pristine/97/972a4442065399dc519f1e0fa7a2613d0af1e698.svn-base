using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.JBZKZ12;
using ZLERP.Business; 

namespace ZLERP.Web.Controllers.Beton
{
    public class B_YingSFController : BaseController<Contract, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var query = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();

            query.Add(new SelectListItem
            {
                Text = B_Common.PayType.type,
                Value = B_Common.PayType.type
            });
            ViewBag.FinanceType = query;
            ViewBag.FinancePerType = query;
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
                return OperateResult(false, "对砼款付款明细不能没有", null);
            }
            if (B_PerPay.InDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            var piaoinService = this.service.GetGenericService<B_Balance>();
            foreach (var p in perpays)
            {
                var piaoin = piaoinService.Get(p.ID.ToString());
                if (piaoin.PayOwing < p.PayMoney2 + p.PayFavourable2)
                {
                    return OperateResult(false, string.Format("结算单{0}的输入的金额不正确，请确认", piaoin.BaleNo), null);
                }
            }


            //这一步修改流程
            //财务付款开票等是否走财务核准流程
            var FinanceExec = this.service.GetGenericService<SysConfig>().Query().Where(t => t.ConfigName == "IsFinanceExec").FirstOrDefault();
            bool IsFinanceExec = false;
            if (FinanceExec != null)
            {
                bool.TryParse(FinanceExec.ConfigValue, out IsFinanceExec);
            }
            ResultInfo resultInfo = new ResultInfo();
            if (!IsFinanceExec)
            {

                if (B_PerPay.PayType == B_Common.PayType.type)
                {
                    var su = this.service.GetGenericService<Contract>().Get(B_PerPay.UnitID);
                    if (su.PrePay < perpays.Sum(t => t.PayMoney2))
                    {
                        return OperateResult(false, "当前供应商的预付款余额不足，无法支付", null);
                    }
                }
                resultInfo = this.service.BetonService.Pay(B_PerPay, perpays);
                return Json(resultInfo);
            }
            else
            {
                resultInfo = this.service.BetonService.AuditExec(B_PerPay, perpays, B_Common.ExecModeltype.Beton);
                return Json(resultInfo);
            }

        }

        public ActionResult PayIni(B_YingSFrec B_YingSFrec)
        {
            if (string.IsNullOrWhiteSpace(B_YingSFrec.UnitID))
            {
                return OperateResult(false, "请选择对期初付款的运输单位", null);
            }
            var supply = this.service.GetGenericService<Contract>().Get(B_YingSFrec.UnitID);
            if (supply.PaidOwing <B_YingSFrec.FinanceMoney + B_YingSFrec.PayFavourable)
            {
                return OperateResult(false, "输入的金额出错，还款额不能超过欠款额", null);
            }
         
                        //这一步修改流程
            //财务付款开票等是否走财务核准流程
            var FinanceExec = this.service.GetGenericService<SysConfig>().Query().Where(t => t.ConfigName == "IsFinanceExec").FirstOrDefault();
            bool IsFinanceExec = false;
            if (FinanceExec != null)
            {
                bool.TryParse(FinanceExec.ConfigValue, out IsFinanceExec);
            }
            ResultInfo resultInfo = new ResultInfo();
            if (!IsFinanceExec)
            {
                resultInfo = this.service.BetonService.PayIni(B_YingSFrec);
                return Json(resultInfo);
            }
            else
            {
                B_PerPay perpay = new B_PerPay();
                List<b_perpay> perpays = new List<b_perpay>();

                perpay.UnitID = B_YingSFrec.UnitID;
                perpay.InDate = B_YingSFrec.FinanceDate;
                perpay.PayType = B_YingSFrec.PayType;
                perpay.Gatheringer = B_YingSFrec.Gatheringer;
                perpay.Payer = B_YingSFrec.Payer;
                perpay.Remark2 = B_YingSFrec.Remark;

                b_perpay per = new Model.Beton.b_perpay();
                per.PayMoney2 = B_YingSFrec.FinanceMoney;
                per.PayFavourable2 = B_YingSFrec.PayFavourable;
                perpays.Add(per);

                resultInfo = this.service.BetonService.AuditExec(perpay, perpays, B_Common.ExecModeltype.IniPay);
                return Json(resultInfo);
            }
        }


        public ActionResult ContractPay(string id, DateTime FinanceDate, decimal FinanceMoney, decimal PayFavourable, string Payer, string Gatheringer, string Remark, string PayType)
        {
            if (string.IsNullOrWhiteSpace(PayType))
            {
                return OperateResult(false, "付款方式不能为空", null);
            }
            if (FinanceDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            Contract con = new Contract();
            //con = this.service.GetGenericService<Contract>().Get(id);

            B_YingSFrec YingSFrec = new B_YingSFrec();
            YingSFrec.UnitID = id;
            YingSFrec.Source = B_Common.OtherInfo.PerPayIni;
            YingSFrec.YingSF = B_Common.OtherInfo.In;

            Random ran = new Random();
            string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
            YingSFrec.FinanceNo = orderno;
            YingSFrec.FinanceDate = FinanceDate;
            YingSFrec.FinanceMoney = FinanceMoney;
            YingSFrec.PayFavourable = PayFavourable;
            YingSFrec.PayType = PayType;
            //YingSFrec.PiaoNo = PiaoNo;
            YingSFrec.Payer = Payer;
            YingSFrec.Gatheringer = Gatheringer;
            YingSFrec.Remark = Remark;
            YingSFrec.Builder = AuthorizationService.CurrentUserName;
            YingSFrec.BuildTime = DateTime.Now;
            this.service.GetGenericService<B_YingSFrec>().Add(YingSFrec);
            ResultInfo resultInfo = new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
            return Json(resultInfo);
        }
    }
}
