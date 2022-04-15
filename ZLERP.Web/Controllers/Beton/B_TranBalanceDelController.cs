using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_TranBalanceDelController : BaseController<B_TranBalanceDel, string>
    {


        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(B_TranBalanceDel B_TranBalanceDel)
        {
            return base.Add(B_TranBalanceDel);
        }
        public override ActionResult Update(B_TranBalanceDel B_TranBalanceDel)
        {
            var del = this.m_ServiceBase.Get(B_TranBalanceDel.ID);
            string BaleBalanceID = del.BaleBalanceID.ToString();
            var query = this.service.GetGenericService<B_TranBalance>().Query().FirstOrDefault(t => t.ID == BaleBalanceID && t.AuditStatus == 1);
            if (query != null)
            {
                return OperateResult(false, "当前结算单已经审核，无法操作", null);
            }
            return base.Update(B_TranBalanceDel);
        }
        public override ActionResult Delete(string[] id)
        {
            foreach (var i in id)
            {
                var del = this.m_ServiceBase.Get(i);
                string BaleBalanceID = del.BaleBalanceID.ToString();
                var query = this.service.GetGenericService<B_TranBalance>().Query().FirstOrDefault(t => t.ID == BaleBalanceID && t.AuditStatus == 1);
                if (query != null)
                {
                    return OperateResult(false, "当前结算单已经审核，无法操作", null);
                }
            }
            return base.Delete(id);
        }
    }
}
