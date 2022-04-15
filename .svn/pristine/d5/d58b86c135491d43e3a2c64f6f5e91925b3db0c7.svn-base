using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Enums;
using System.Web;
using ZLERP.Resources;
using ZLERP.Model;
using ZLERP.IRepository;
using System.Collections.Specialized;

namespace ZLERP.Business
{
    public class StuffSellService : ServiceBase<StuffSell>
    {
        internal StuffSellService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
   

        public override StuffSell Add(StuffSell entity)
        {

            using (IGenericTransaction transaction = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var stuffSellRepo = this.m_UnitOfWork.GetRepositoryBase<StuffSell>();
                    var result = stuffSellRepo.Add(entity);

                    if (entity.IsReduce)
                    {
                        IRepositoryBase<Silo> siloRepo = this.m_UnitOfWork.GetRepositoryBase<Silo>();
                        var dbSilo = siloRepo.Get(entity.SiloID);
                        dbSilo.Content -= entity.SellName ?? 0;
                        siloRepo.Update(dbSilo);
                    }

                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Update(NameValueCollection form)
        {
            using (IGenericTransaction transaction = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var stuffSellRepo = this.m_UnitOfWork.GetRepositoryBase<StuffSell>();
                    var siloRepo = this.m_UnitOfWork.GetRepositoryBase<Silo>();

                    var dbEntity = stuffSellRepo.Get(form["ID"]);
                    var dbEntitySilo = siloRepo.Get(dbEntity.SiloID);
                    if (dbEntity.IsReduce)
                    {
                        dbEntitySilo.Content += dbEntity.SellName ?? 0;
                        siloRepo.Update(dbEntitySilo);
                    }

                    stuffSellRepo.Update(dbEntity, form);

                    if (dbEntity.IsReduce)
                    {
                        var entitySilo = siloRepo.Get(dbEntity.SiloID);
                        entitySilo.Content -= dbEntity.SellName ?? 0;
                        siloRepo.Update(entitySilo);
                    }

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public override void Delete(object[] ids)
        {
            using (IGenericTransaction transaction = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var stuffSellRepo = this.m_UnitOfWork.GetRepositoryBase<StuffSell>();
                    var siloRepo = this.m_UnitOfWork.GetRepositoryBase<Silo>();

                    foreach (var deleteEntityId in ids)
                    {
                        var deleteStuffSell = stuffSellRepo.Get(deleteEntityId);
                        stuffSellRepo.Delete(deleteStuffSell);

                        if (deleteStuffSell.IsReduce)
                        {
                            var dbSilo = siloRepo.Get(deleteStuffSell.SiloID);
                            dbSilo.Content += deleteStuffSell.SellName ?? 0;
                            siloRepo.Update(dbSilo);
                        }
                    }

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
