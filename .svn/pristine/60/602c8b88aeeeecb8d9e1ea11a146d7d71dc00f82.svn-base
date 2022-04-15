using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_TaxSetController : HRBaseController<HR_GZ_TaxSet, string>
    {
        public override ActionResult Index()
        {
        
            return base.Index();
        }

        public ActionResult AddOrUpdate(HR_GZ_TaxSet HR_GZ_TaxSet)
        {
            try
            {
                int id = 0;
                int.TryParse(HR_GZ_TaxSet.ID, out id);
                if (id <= 0)
                {
                    return base.Add(HR_GZ_TaxSet);
                }
                else
                {
                    return base.Update(HR_GZ_TaxSet);
                }
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
    }
}
