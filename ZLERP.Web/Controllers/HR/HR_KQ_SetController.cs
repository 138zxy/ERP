using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_SetController : HRBaseController<HR_KQ_Set, string>
    {
        public override ActionResult Index()
        {
            var set = this.m_ServiceBase.All().FirstOrDefault();
            if (set == null)
            {
                set = new HR_KQ_Set();
            }
            ViewBag.HR_KQ_Set = set;
            return base.Index();
        }

        public ActionResult AddOrUpdate(HR_KQ_Set HR_KQ_Set)
        {
            try
            {
                int id = 0;
                int.TryParse(HR_KQ_Set.ID, out id);
                if (id <= 0)
                {
                    return base.Add(HR_KQ_Set);
                }
                else
                {
                    return base.Update(HR_KQ_Set);
                }
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
    }
}
