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
    public class HR_GZ_SafePersonController : HRBaseController<HR_GZ_SafePerson, string>
    {
        public override ActionResult Index()
        {
            var query = this.service.GetGenericService<HR_GZ_Safe>().All();
            List<SelectListItem> Items = (from q in query
                                          select new SelectListItem
                                          {
                                              Text = q.SafeName,
                                              Value = q.ID.ToString()
                                          }).ToList();
            ViewBag.SetID = Items;

            List<SelectListItem> Conditions = new List<SelectListItem>{
                new SelectListItem { Text = "正常", Value = "正常"},
                new SelectListItem { Text = "停止", Value = "停止"}
            };

            ViewBag.Condition = Conditions;
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_SafePerson entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.PersonID == entity.PersonID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("{0}已经存在了", query.HR_Base_Personnel.Name), null);
            }
            entity.HR_GZ_Safe = null;
            return base.Add(entity);
        }

        public override ActionResult Update(HR_GZ_SafePerson entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != entity.ID && t.PersonID == entity.PersonID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("{0}已经存在了", query.HR_Base_Personnel.Name), null);
            }
            entity.HR_GZ_Safe = null;
            return base.Update(entity);
        }
    }
}
