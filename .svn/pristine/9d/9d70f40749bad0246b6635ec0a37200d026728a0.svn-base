using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_SalariesSetController : HRBaseController<HR_GZ_SalariesSet, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_SalariesSet entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.PersonID == entity.PersonID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("{0}已经存在了", query.HR_Base_Personnel.Name), null);
            }
            return base.Add(entity);
        }

        public override ActionResult Update(HR_GZ_SalariesSet entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != entity.ID && t.PersonID == entity.PersonID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("{0}已经存在了", query.HR_Base_Personnel.Name), null);
            }
            return base.Update(entity);
        }
    }
}
