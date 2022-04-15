using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class SaleDailyRecordService : ServiceBase<SaleDailyRecord>
    {
        internal SaleDailyRecordService(IUnitOfWork uow)
            : base(uow)
        {
        }

    }
}
