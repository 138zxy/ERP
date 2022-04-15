using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_PM_ContractController : HRBaseController<HR_PM_Contract, int?>
    {      
        public override ActionResult Index()
        {
            ViewBag.ContractTypeList = GetBaseData(BaseDataType.合同类型);
            return base.Index();
        }
    }
}
