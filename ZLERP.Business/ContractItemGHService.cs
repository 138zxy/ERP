using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Resources;

namespace ZLERP.Business
{
    public class ContractItemGHService : ServiceBase<ContractItemGH>
    {
        internal ContractItemGHService(IUnitOfWork uow) : base(uow) { }

        /// <summary>
        /// 审核-砼强度
        /// </summary>
        /// <param name="consMixprop"></param>
        public void Audit(ContractItemGH contractItemGH)
        {
            try
            {
                ContractItemGH cons = this.Get(contractItemGH.ID);
                string auditor = AuthorizationService.CurrentUserID;
                cons.AuditStatus = 1;
                cons.Remark = contractItemGH.Remark;
                cons.Auditor = auditor;
                cons.AuditTime = DateTime.Now;
                base.Update(cons, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }

        /// 取消审核-砼强度
        /// </summary>
        /// <param name="consMixID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string consItemID)
        {
            try
            {
                ContractItemGH contractItemGH = this.Get(Convert.ToInt32(consItemID));
                contractItemGH.AuditStatus = 0;
                contractItemGH.Auditor = "";
                contractItemGH.AuditTime = null;
                this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>().Update(contractItemGH, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
    }
}
