using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IGetConPriceRepository : IRepositoryBase<ContractItem>
    {
        decimal GetConPrice(string contractID, string conStrength, DateTime produceDate);
        decimal GetConPumpPrice(string contractID, string pumpName);
        decimal GetConGHPrice(string contractID, string conStrength, DateTime produceDate);
        decimal GetConSHPrice(string contractID, string conStrength, DateTime produceDate);
        
    }
}
