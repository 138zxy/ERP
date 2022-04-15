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
    public class B_PiaoYingSFController : BaseController<Contract, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            var query = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GetPerPayTypes();
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
                return OperateResult(false, "开票明细不能没有", null);
            }
            if (B_PerPay.InDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            var piaoinService = this.service.GetGenericService<B_Balance>();
    
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
                resultInfo = this.service.BetonService.PiaoPay(B_PerPay, perpays);
                return Json(resultInfo);
            } 
            else
            {
                resultInfo = this.service.BetonService.AuditExec(B_PerPay, perpays, B_Common.ExecModeltype.PiaoBeton);
                return Json(resultInfo);
            }
        }
        public ActionResult PayIni(B_PiaoYingSFrec B_PiaoYingSFrec)
        {
            if (string.IsNullOrWhiteSpace(B_PiaoYingSFrec.UnitID))
            {
                return OperateResult(false, "请选择对期初付款的运输单位", null);
            }
      
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.PiaoPayIni(B_PiaoYingSFrec);
            return Json(resultInfo);
        }
       
    }
}
