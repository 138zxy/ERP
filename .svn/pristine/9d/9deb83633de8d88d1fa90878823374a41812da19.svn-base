using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_HolidayController : HRBaseController<HR_KQ_Holiday, string>
    {
        public override ActionResult Index()
        {
            ViewBag.HolidayType = GetBaseData(BaseDataType.假期类型);
            return base.Index();
        }
    }
}
