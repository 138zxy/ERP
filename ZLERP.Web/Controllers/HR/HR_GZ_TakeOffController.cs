using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums; 

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_TakeOffController : HRBaseController<HR_GZ_TakeOff, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_TakeOff HR_GZ_TakeOff)
        {
            string month = HR_GZ_TakeOff.Monthly + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            return base.Add(HR_GZ_TakeOff);
        }

        public override ActionResult Update(HR_GZ_TakeOff HR_GZ_TakeOff)
        {
            string month = HR_GZ_TakeOff.Monthly + "-01";
            DateTime da;
            if (!DateTime.TryParse(month, out da))
            {
                return OperateResult(false, "输入的纳入月份有误", null);
            }
            return base.Update(HR_GZ_TakeOff);
        }
    }
}
