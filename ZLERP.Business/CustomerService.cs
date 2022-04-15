using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class CustomerService : ServiceBase<Customer>
    {
        internal CustomerService(IUnitOfWork uow) : base(uow) { }

        public bool EnableAll(string[] ids, bool enable)
        {
            bool AuditResult = true;
            try
            {
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {//防止提交空id出错
                        var customer = this.Get(id);
                        if (customer != null && customer.IsUse != enable)
                        {
                            customer.IsUse = enable;
                            this.Update(customer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("失败:" + ex.Message);
            }
            return AuditResult;
        }
    }
}
