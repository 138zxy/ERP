using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.Model;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Linq;
using NHibernate;

namespace ZLERP.NHibernateRepository
{
    public class ContractRepository : NHRepositoryBase<Contract>, IContractRepository
    {
        public ContractRepository(ISession session)
        :base(session)
        { 
        
        }

        public void UpdateBalanceRecordAndItems(string contractId)
        {
            string sp = "exec sp_UpdateBalanceRecordItem @c=:contractId";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("contractId", contractId);
            query.UniqueResult();


            string sp2 = "exec sp_UpdateBalanceRecord @c=:contractId";
            var query2 = this._session.CreateSQLQuery(sp2);
            query2.SetString("contractId", contractId);
            query2.UniqueResult();

        }
    }
}
