using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_FinanceController : BaseController<B_Finance, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public ActionResult AuditExec(string id)
        {
            var b_Finance = this.m_ServiceBase.Get(id);
            if (b_Finance == null)
            {
                return OperateResult(false, "无法找到当前申请单", null);
            }
            if (b_Finance.AuditStatus != 0)
            {
                return OperateResult(false, "只有未审核的单据才能操作", null);
            }
            B_PerPay B_PerPay = new B_PerPay();

            B_PerPay.UnitID = b_Finance.UnitID;
            B_PerPay.PayType = b_Finance.PayType;
            B_PerPay.InDate = b_Finance.FinanceDate;
            B_PerPay.Payer = b_Finance.Payer;
            B_PerPay.Gatheringer = b_Finance.Gatheringer;
            B_PerPay.Remark2 = b_Finance.Remark;

            List<b_perpay> perpays = new List<b_perpay>();
            int FinanceID = Convert.ToInt32(id);
            var b_FinanceDels = this.service.GetGenericService<B_FinanceDel>().Query().Where(T => T.FinanceID == FinanceID).ToList();
            if (b_FinanceDels != null && b_FinanceDels.Count > 0)
            {
                foreach (var f in b_FinanceDels)
                {
                    b_perpay b = new b_perpay();
                    b.ID = f.BaleID;
                    b.PayMoney2 = f.PayMoney;
                    b.PayFavourable2 = f.PayFavourable;
                    perpays.Add(b);
                }
            }
            else
            {
                if (b_Finance.Modeltype != B_Common.ExecModeltype.PayPer && b_Finance.Modeltype != B_Common.ExecModeltype.IniPay)
                {
                    return OperateResult(false, "当前申请明细为空，无法执行核准", null);
                }
            }
            ResultInfo resultInfo = new ResultInfo();
            if (b_Finance.Modeltype == B_Common.ExecModeltype.Beton)
            {
                var piaoinService = this.service.GetGenericService<B_Balance>();
                foreach (var p in perpays)
                {
                    var piaoin = piaoinService.Get(p.ID.ToString());
                    if (piaoin.PayOwing < p.PayMoney2 + p.PayFavourable2)
                    {
                        return OperateResult(false, string.Format("结算单{0}的输入的总金额已超出欠款额，请确认", piaoin.BaleNo), null);
                    }
                }
                if (B_PerPay.PayType == B_Common.PayType.type)
                {
                    var su = this.service.GetGenericService<Contract>().Get(B_PerPay.UnitID);
                    if (su.PrePay < perpays.Sum(t => t.PayMoney2))
                    {
                        return OperateResult(false, "当前供应商的预付款余额不足，无法支付", null);
                    }
                } 
                resultInfo = this.service.BetonService.Pay(B_PerPay, perpays, id);
            }
            if (b_Finance.Modeltype == B_Common.ExecModeltype.PiaoBeton)
            {
                resultInfo = this.service.BetonService.PiaoPay(B_PerPay, perpays, id);
            }

            if (b_Finance.Modeltype == B_Common.ExecModeltype.PayPer)
            { 
                B_YingSFrec b_YingSFrec = new B_YingSFrec();
                b_YingSFrec.UnitID = b_Finance.UnitID;
                b_YingSFrec.PayType = b_Finance.PayType;
                b_YingSFrec.FinanceDate = b_Finance.FinanceDate;
                b_YingSFrec.Payer = b_Finance.Payer;
                b_YingSFrec.Gatheringer = b_Finance.Gatheringer;
                b_YingSFrec.Remark = b_Finance.Remark;
                b_YingSFrec.FinanceMoney = b_Finance.FinanceMoney;
                b_YingSFrec.PayFavourable = b_Finance.PayFavourable;
                resultInfo = this.service.BetonService.PerPay(b_YingSFrec, id);
            }

            if (b_Finance.Modeltype == B_Common.ExecModeltype.IniPay)
            {
                B_YingSFrec b_YingSFrec = new B_YingSFrec();
                b_YingSFrec.UnitID = b_Finance.UnitID;
                b_YingSFrec.PayType = b_Finance.PayType;
                b_YingSFrec.FinanceDate = b_Finance.FinanceDate;
                b_YingSFrec.Payer = b_Finance.Payer;
                b_YingSFrec.Gatheringer = b_Finance.Gatheringer;
                b_YingSFrec.Remark = b_Finance.Remark;
                b_YingSFrec.FinanceMoney = b_Finance.FinanceMoney;
                b_YingSFrec.PayFavourable = b_Finance.PayFavourable;
                resultInfo = this.service.BetonService.PayIni(b_YingSFrec, id);
            }
            return Json(resultInfo);
        }

        public ActionResult UnExec(string id)
        {
            var b_Finance = this.m_ServiceBase.Get(id);
            if (b_Finance == null)
            {
                return OperateResult(false, "无法找到当前申请单", null);
            }
            if (b_Finance.AuditStatus != 0)
            {
                return OperateResult(false, "只有未审核的单据才能操作", null);
            }
            b_Finance.AuditStatus = -1;
            b_Finance.Auditor = AuthorizationService.CurrentUserID;
            b_Finance.AuditTime = DateTime.Now;
            this.m_ServiceBase.Update(b_Finance, null);
            return OperateResult(true, "驳回成功", null);
        }
    }
}
