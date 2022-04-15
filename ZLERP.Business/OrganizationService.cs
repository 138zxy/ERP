using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;


namespace ZLERP.Business
{
    public class OrganizationService: ServiceBase<Balance>
    {
        internal OrganizationService(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
