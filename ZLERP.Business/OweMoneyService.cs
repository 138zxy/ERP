using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class OweMoneyService : ServiceBase<OweMoney>
    {
        internal OweMoneyService(IUnitOfWork uow)
            : base(uow)
        {
        }

    }
}
