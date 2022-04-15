using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using System.Collections.Specialized;

namespace ZLERP.Business
{
    public class ContractInvoiceGHService : ServiceBase<ContractInvoiceGH>
    {
        internal ContractInvoiceGHService(IUnitOfWork uow) : base(uow) { }

        public override ContractInvoiceGH Add(ContractInvoiceGH entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<ContractGH> contractResp = this.m_UnitOfWork.GetRepositoryBase<ContractGH>();
                    IRepositoryBase<ContractInvoiceGH> invoiceResp = this.m_UnitOfWork.GetRepositoryBase<ContractInvoiceGH>();
                    ContractGH contract = contractResp.Get(entity.ContractID);
                    decimal totalInvoiceMoney = contract.TotalInvoiceMoney ?? 0m;
                    contract.TotalInvoiceMoney = totalInvoiceMoney + entity.InvoiceMoney;
                    contractResp.Update(contract, null);
                    ContractInvoiceGH entityAdd = invoiceResp.Add(entity);
                    tx.Commit();

                    return entityAdd;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public override void Update(ContractInvoiceGH entity, NameValueCollection form)
        {
            if (form.AllKeys.Contains("InvoiceMoney", StringComparer.OrdinalIgnoreCase))
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        IRepositoryBase<ContractGH> contractResp = this.m_UnitOfWork.GetRepositoryBase<ContractGH>();
                        IRepositoryBase<ContractInvoiceGH> invoiceResp = this.m_UnitOfWork.GetRepositoryBase<ContractInvoiceGH>();
                        ContractInvoiceGH dbEntity = invoiceResp.Get(entity.ID);

                        ContractGH contract = contractResp.Get(dbEntity.ContractID);
                        decimal totalInvoiceMoney = contract.TotalInvoiceMoney ?? 0m;
                        contract.TotalInvoiceMoney = totalInvoiceMoney - dbEntity.InvoiceMoney + entity.InvoiceMoney;

                        contractResp.Update(contract, null);
                        invoiceResp.Update(entity, form);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        logger.Error(ex.Message);
                        throw;
                    }
                }
            }
            else
            {
                base.Update(entity, form);
            }
        }

        public override void Delete(ContractInvoiceGH entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<ContractGH> contractResp = this.m_UnitOfWork.GetRepositoryBase<ContractGH>();
                    IRepositoryBase<ContractInvoiceGH> invoiceResp = this.m_UnitOfWork.GetRepositoryBase<ContractInvoiceGH>();
                    ContractGH contract = contractResp.Get(entity.ContractID);
                    decimal totalInvoiceMoney = contract.TotalInvoiceMoney ?? 0m;

                    contract.TotalInvoiceMoney = totalInvoiceMoney - entity.InvoiceMoney;

                    contractResp.Update(contract, null);
                    invoiceResp.Delete(entity);

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public override void Delete(object[] ids)
        {
            IRepositoryBase<ContractInvoiceGH> invoiceResp = this.m_UnitOfWork.GetRepositoryBase<ContractInvoiceGH>();
            foreach (var id in ids)
            {
                ContractInvoiceGH deleteEntity = invoiceResp.Get(id);
                Delete(deleteEntity);
            }
          
        }


    }
}
