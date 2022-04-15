using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_YingYFController : BaseController<Contract, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.FinanceType = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
            return base.Index();
        }

        public ActionResult Pay(B_YingSFrec B_YingSFrec)
        {
            if (string.IsNullOrWhiteSpace(B_YingSFrec.UnitID))
            {
                return OperateResult(false, "请选择供应商进行预付款", null);
            }
            if (string.IsNullOrWhiteSpace(B_YingSFrec.PayType))
            {
                return OperateResult(false, "请选择付款方式", null);
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
                resultInfo = this.service.BetonService.PerPay(B_YingSFrec);
                return Json(resultInfo);
            }
            else {
                B_PerPay perpay = new B_PerPay();
                List<b_perpay> perpays = new List<b_perpay>();

                perpay.UnitID = B_YingSFrec.UnitID;
                perpay.InDate = B_YingSFrec.FinanceDate;
                perpay.PayType = B_YingSFrec.PayType;
                perpay.Gatheringer = B_YingSFrec.Gatheringer;
                perpay.Payer = B_YingSFrec.Payer;
                perpay.Remark2 = B_YingSFrec.Remark; 

                b_perpay  per = new Model.Beton.b_perpay();
                per.PayMoney2 = B_YingSFrec.FinanceMoney;
                perpays.Add(per);

                resultInfo = this.service.BetonService.AuditExec(perpay, perpays, B_Common.ExecModeltype.PayPer);
                return Json(resultInfo);
            }
        }

    }
}
