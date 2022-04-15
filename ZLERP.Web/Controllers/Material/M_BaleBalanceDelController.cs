using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers.Material
{
    public class M_BaleBalanceDelController : BaseController<M_BaleBalanceDel, string>
    {


        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(M_BaleBalanceDel M_BaleBalanceDel)
        {
            return base.Add(M_BaleBalanceDel);
        }
        public override ActionResult Update(M_BaleBalanceDel M_BaleBalanceDel)
        {
            var del = this.m_ServiceBase.Get(M_BaleBalanceDel.ID);
            string BaleBalanceID = del.BaleBalanceID.ToString();
            var query = this.service.GetGenericService<M_BaleBalance>().Query().FirstOrDefault(t => t.ID == BaleBalanceID && t.AuditStatus == 1);
            if (query != null)
            {
                return OperateResult(false, "当前结算单已经审核，无法操作", null);
            }
            return base.Update(M_BaleBalanceDel);
        }
        public override ActionResult Delete(string[] id)
        {
            foreach (var i in id)
            {
                var del = this.m_ServiceBase.Get(i);
                string BaleBalanceID = del.BaleBalanceID.ToString();
                var query = this.service.GetGenericService<M_BaleBalance>().Query().FirstOrDefault(t => t.ID == BaleBalanceID && t.AuditStatus == 1);
                if (query != null)
                {
                    return OperateResult(false, "当前结算单已经审核，无法操作", null);
                }
            }
            return base.Delete(id);
        }
    }
}
