using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers.Material
{
    public class M_TranBalanceDelController : BaseController<M_TranBalanceDel, string>
    {


        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(M_TranBalanceDel M_TranBalanceDel)
        {
            return base.Add(M_TranBalanceDel);
        }
        public override ActionResult Update(M_TranBalanceDel M_TranBalanceDel)
        {
            var del = this.m_ServiceBase.Get(M_TranBalanceDel.ID);
            string BaleBalanceID = del.BaleBalanceID.ToString();
            var query = this.service.GetGenericService<M_TranBalance>().Query().FirstOrDefault(t => t.ID == BaleBalanceID);
            if (query != null && query.AuditStatus == 1)
            {
                return OperateResult(false, "当前结算单已经审核，无法操作", null);
            }
            return base.Update(M_TranBalanceDel);
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
