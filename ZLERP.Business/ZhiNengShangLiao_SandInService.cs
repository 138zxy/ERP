using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class ZhiNengShangLiao_SandInService : ServiceBase<ZhiNengShangLiao_SandIn>
    {
        internal ZhiNengShangLiao_SandInService(IUnitOfWork uow) : base(uow) { }
    }
}
