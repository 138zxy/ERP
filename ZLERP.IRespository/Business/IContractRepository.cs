using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IContractRepository : IRepositoryBase<Contract>
    {
        void UpdateBalanceRecordAndItems(string contractId);
    }
}
