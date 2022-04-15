using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;  

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_SubsidyController : HRBaseController<HR_GZ_Subsidy, string>
    {
        public override ActionResult Index()
        {
            ViewBag.Subsidy = GetBaseData(BaseDataType.补贴项目);
            ViewBag.TakeOff = GetBaseData(BaseDataType.扣除项目);
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_Subsidy HR_GZ_Subsidy)
        {
            string month = HR_GZ_Subsidy.Monthly + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            return base.Add(HR_GZ_Subsidy);
        }

        public override ActionResult Update(HR_GZ_Subsidy HR_GZ_Subsidy)
        {
            string month = HR_GZ_Subsidy.Monthly + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            return base.Update(HR_GZ_Subsidy);
        }
    }
}
