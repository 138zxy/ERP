using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 合同付款
    /// </summary>
    public class ContractPayGHController : BaseController<ContractPayGH, int>
    {
        public override ActionResult Add(ContractPayGH ContractPay)
        {
            string msgcontent = "合同号：[" + ContractPay.ContractNo + "] 付款：" + ContractPay.PayMoney + "元";
            SendMsg("ContractPay", msgcontent, ContractPay.ContractID.ToString(),true);
            return base.Add(ContractPay);
        }
    }
}
