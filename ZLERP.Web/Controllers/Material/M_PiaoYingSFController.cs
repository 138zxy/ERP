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
    public class M_PiaoYingSFController : BaseController<SupplyInfo, string>
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

        public ActionResult Pay(M_PerPay B_PerPay)
        {
            if (string.IsNullOrWhiteSpace(B_PerPay.PayType))
            {
                return OperateResult(false, "付款方式不能为空", null);
            }
            List<m_perpay> perpays = JsonHelper.Instance.Deserialize<List<m_perpay>>(B_PerPay.Records);
            if (perpays == null || perpays.Count <= 0)
            {
                return OperateResult(false, "开票明细不能没有", null);
            }
            if (B_PerPay.InDate < DateTime.Now.AddYears(-30))
            {
                return OperateResult(false, "你输入的时间有问题", null);
            }
            var piaoinService = this.service.GetGenericService<M_BaleBalance>();
    
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.PiaoPay(B_PerPay, perpays);
            return Json(resultInfo);
        }

        public ActionResult PayIni(M_PiaoYingSFrec M_PiaoYingSFrec)
        {
            if (string.IsNullOrWhiteSpace(M_PiaoYingSFrec.UnitID))
            {
                return OperateResult(false, "请选择对期初付款的供应商", null);
            }
         
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.PiaoPayIni(M_PiaoYingSFrec);
            return Json(resultInfo);
        }
    }
}
