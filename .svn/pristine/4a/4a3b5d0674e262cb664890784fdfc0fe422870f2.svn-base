using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_SafeSetController : HRBaseController<HR_GZ_SafeSet, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_SafeSet HR_GZ_SafeSet)
        {
            var re = base.Add(HR_GZ_SafeSet);
            UpdateSafe(HR_GZ_SafeSet.SetID.ToString());
            return re;
        }
        public override ActionResult Update(HR_GZ_SafeSet HR_GZ_SafeSet)
        {
            var re = base.Update(HR_GZ_SafeSet);
            UpdateSafe(HR_GZ_SafeSet.SetID.ToString());
            return re;
        }

        public void UpdateSafe(string id)
        {
            var safe = this.service.GetGenericService<HR_GZ_Safe>().Get(id);
            var setid = Convert.ToInt32(id);
            var PersonPayAll = this.m_ServiceBase.Query().Where(t => t.SetID == setid).Sum(t => t.PersonPay);
            safe.Summation = PersonPayAll;
            this.service.GetGenericService<HR_GZ_Safe>().Update(safe, null);
        }
    }
}
