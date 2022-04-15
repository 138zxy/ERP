using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;　

namespace ZLERP.Business
{
    public class StockPactService : ServiceBase<StockPact>
    {
        internal StockPactService(IUnitOfWork uow)
            : base(uow) 
        { 
        }


        public void UpdateBalanceRecordAndItems(string stockPactId)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    this.m_UnitOfWork.StockPactRepository.UpdateBalanceRecordAndItems(stockPactId);

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }



    }
}
