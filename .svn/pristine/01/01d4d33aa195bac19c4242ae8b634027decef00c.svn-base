using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_TaxController : HRBaseController<HR_GZ_Tax, string>
    {
        public override ActionResult Index()
        {
            var set = this.service.GetGenericService<HR_GZ_TaxSet>().All().FirstOrDefault();
            if (set == null)
            {
                set = new HR_GZ_TaxSet();
            }
            ViewBag.HR_GZ_TaxSet = set;
            return base.Index();
        }

        public override ActionResult Add(HR_GZ_Tax HR_GZ_Tax)
        {
            //判断是否区间重叠了
            int Startmoney = HR_GZ_Tax.Startmoney;
            int Endmoney = HR_GZ_Tax.Endmoney;
            var query = this.m_ServiceBase.Query().Where(t => (t.Startmoney <= Startmoney && t.Endmoney > Startmoney) || (t.Startmoney <= Endmoney && t.Endmoney > Endmoney)).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("与区间{0},{1}重叠了", query.Startmoney, query.Endmoney), null);
            }
            return base.Add(HR_GZ_Tax);
        }

        public override ActionResult Update(HR_GZ_Tax HR_GZ_Tax)
        {
            ////判断是否区间重叠了
            //int Startmoney = HR_GZ_Tax.Startmoney;
            //int Endmoney = HR_GZ_Tax.Endmoney;
            //var query = this.m_ServiceBase.Query().Where(t => t.ID != HR_GZ_Tax.ID && ((t.Startmoney <= Startmoney && t.Endmoney > Startmoney) || (t.Startmoney <= Endmoney && t.Endmoney > Endmoney))).FirstOrDefault();
            //if (query != null)
            //{
            //    return OperateResult(false, string.Format("与区间{0},{1}重叠了", query.Startmoney, query.Endmoney), null);
            //}
            return base.Update(HR_GZ_Tax);
        }
    }
}
