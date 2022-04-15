using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_SetItemController : HRBaseController<HR_KQ_SetItem, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_KQ_SetItem HR_KQ_SetItem)
        {
            //判断是否区间重叠了
            int startMinute = HR_KQ_SetItem.StartMinute;
            int endMinute = HR_KQ_SetItem.EndMinute;
            var query = this.m_ServiceBase.Query().Where(t => (t.StartMinute <= startMinute && t.EndMinute > startMinute) || (t.StartMinute <= endMinute && t.EndMinute > endMinute)).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("与区间{0},{1}重叠了", query.StartMinute, query.EndMinute), null);
            }
            return base.Add(HR_KQ_SetItem);
        }

        public override ActionResult Update(HR_KQ_SetItem HR_KQ_SetItem)
        {
            //判断是否区间重叠了
            int startMinute = HR_KQ_SetItem.StartMinute;
            int endMinute = HR_KQ_SetItem.EndMinute;
            var query = this.m_ServiceBase.Query().Where(t => t.ID != HR_KQ_SetItem.ID && ((t.StartMinute <= startMinute && t.EndMinute > startMinute) || (t.StartMinute <= endMinute && t.EndMinute > endMinute))).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("与区间{0},{1}重叠了", query.StartMinute, query.EndMinute), null);
            }
            return base.Update(HR_KQ_SetItem);
        }
    }
}
