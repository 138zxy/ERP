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
    public class StockPactRepository : NHRepositoryBase<StockPact>, IStockPactRepository
    {
        public StockPactRepository(ISession session)
            : base(session)
        {

        }

        public void UpdateBalanceRecordAndItems(string stockPactId)
        {
            string sp = "exec sp_UpdateBalanceRecordItem @c=:stockPactId";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("stockPactId", stockPactId);
            query.UniqueResult();


            string sp2 = "exec sp_UpdateBalanceRecord @c=:stockPactId";
            var query2 = this._session.CreateSQLQuery(sp2);
            query2.SetString("stockPactId", stockPactId);
            query2.UniqueResult();

        }
    }
}
