﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using NHibernate;
using System.Data;
using System.Reflection;

namespace ZLERP.NHibernateRepository
{
    /// <summary>
    /// UnitOfWork实现类
    /// </summary>
    public class UnitOfWorkImpl : IUnitOfWork
    {
        #region UnitOfWork方法
        private readonly IUnitOfWorkFactory _factory;
        private readonly ISession _session;

        public UnitOfWorkImpl(IUnitOfWorkFactory factory, ISession session)
        {
            _factory = factory;
            _session = session;
        }

        public void Dispose()
        {
           _factory.DisposeUnitOfWork(this);
          _session.Dispose();
        }

        public void IncrementUsages()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public bool IsInActiveTransaction
        {
            get
            {
                return _session.Transaction.IsActive;
            }
        }

        public IUnitOfWorkFactory Factory
        {
            get { return _factory; }
        }

        public ISession Session
        {
            get { return _session; }
        }

        public IGenericTransaction BeginTransaction()
        {
            return new GenericTransaction(_session.BeginTransaction());
        }

        public IGenericTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new GenericTransaction(_session.BeginTransaction(isolationLevel));
        }
        #endregion

        #region Repositories
        //private object m_RepoBase;
        /// <summary>
        /// 通用Repository
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepositoryBase<TEntity> GetRepositoryBase<TEntity>()
        {
            //UNDONE:使用反射查找对应model的service,有则返回，否则返回ServiceBase
            //此处用以在具体的实体Service中重写ServiceBase中的方法
            string repoPropertyName = typeof(TEntity).Name + "Repository";
            PropertyInfo pi = this.GetType().GetProperty(repoPropertyName);
            if (pi != null)
            {
                var svc = this.GetType().InvokeMember(repoPropertyName, BindingFlags.GetProperty, null, this, null);
                if (svc is NHRepositoryBase<TEntity>)
                    return svc as NHRepositoryBase<TEntity>;
            }        
            return new NHRepositoryBase<TEntity>(this._session);
             
        }

        #region 业务相关Repository
        private IConsMixpropRepository m_ConsMixpropRepository;
        public IConsMixpropRepository ConsMixpropRepository
        {
            get
            {
                if (m_ConsMixpropRepository == null)
                {
                    m_ConsMixpropRepository = new ConsMixpropRepository(this._session);
                }
                return this.m_ConsMixpropRepository;
            }
        }

        
        private IConsMixpropItemRepository m_ConsMixpropItemRepository;
        public IConsMixpropItemRepository ConsMixpropItemRepository
        {
            get
            {
                if (m_ConsMixpropItemRepository == null)
                {
                    m_ConsMixpropItemRepository = new ConsMixpropItemRepository(this._session);
                }
                return this.m_ConsMixpropItemRepository;
            }
        }

        private IConsMixpropGHRepository m_ConsMixpropGHRepository;
        public IConsMixpropGHRepository ConsMixpropGHRepository
        {
            get
            {
                if (m_ConsMixpropGHRepository == null)
                {
                    m_ConsMixpropGHRepository = new ConsMixpropGHRepository(this._session);
                }
                return this.m_ConsMixpropGHRepository;
            }
        }
        private IConsMixpropItemGHRepository m_ConsMixpropItemGHRepository;
        public IConsMixpropItemGHRepository ConsMixpropItemGHRepository
        {
            get
            {
                if (m_ConsMixpropItemGHRepository == null)
                {
                    m_ConsMixpropItemGHRepository = new ConsMixpropItemGHRepository(this._session);
                }
                return this.m_ConsMixpropItemGHRepository;
            }
        }
        private IShippingDocumentGHRepository m_ShippingDocumentGHRepository;
        public IShippingDocumentGHRepository ShippingDocumentGHRepository
        {
            get
            {
                if (m_ShippingDocumentGHRepository == null)
                {
                    m_ShippingDocumentGHRepository = new ShippingDocumentGHRepository(this._session);
                }
                return this.m_ShippingDocumentGHRepository;
            }
        }
 

        private IStuffInRepository m_StuffInRepository;
        public IStuffInRepository StuffInRepository
        {
            get
            {
                if (m_StuffInRepository == null)
                {
                    m_StuffInRepository = new StuffInRepository(this._session);
                }
                return this.m_StuffInRepository;
            }
        }
        private IShippingDocumentRepository m_ShippingDocumentRepository;
        public IShippingDocumentRepository ShippingDocumentRepository
        {
            get
            {
                if (m_ShippingDocumentRepository == null)
                {
                    m_ShippingDocumentRepository = new ShippingDocumentRepository(this._session);
                }
                return this.m_ShippingDocumentRepository;
            }
        }

        private IYearAccountRepository m_YearAccountRepository;
        public IYearAccountRepository YearAccountRepository
        {
            get
            {
                if (m_YearAccountRepository == null)
                {
                    m_YearAccountRepository = new YearAccountRepository(this._session);
                }
                return this.m_YearAccountRepository;
            }
        }
        private IMonthAccountRepository m_MonthAccountRepository;
        public IMonthAccountRepository MonthAccountRepository
        {
            get
            {
                if (m_MonthAccountRepository == null)
                {
                    m_MonthAccountRepository = new MonthAccountRepository(this._session);
                }
                return this.m_MonthAccountRepository;
            }
        }
        private IStuffInPriceAdjustRepository m_StuffInPriceAdjustRepository;
        public IStuffInPriceAdjustRepository StuffInPriceAdjustRepository
        {
            get
            {
                if (m_StuffInPriceAdjustRepository == null)
                {
                    m_StuffInPriceAdjustRepository = new StuffInPriceAdjustRepository(this._session);
                }
                return this.m_StuffInPriceAdjustRepository;
            }
        }
        private IGetConPriceRepository m_GetConPriceRepository;
        public IGetConPriceRepository GetConPriceRepository
        {
            get
            {
                if (m_GetConPriceRepository == null)
                {
                    m_GetConPriceRepository = new GetConPriceRepository(this._session);
                }
                return this.m_GetConPriceRepository;
            }
        }
        
        private IContractRepository m_ContractRepository;
        public IContractRepository ContractRepository
        {
            get
            {
                if (m_ContractRepository == null)
                {
                    m_ContractRepository = new ContractRepository(this._session);
                }
                return this.m_ContractRepository;
            }
        }

        private IStockPactRepository m_StockPactRepository;
        public IStockPactRepository StockPactRepository
        {
            get
            {
                if (m_StockPactRepository == null)
                {
                    m_StockPactRepository = new StockPactRepository(this._session);
                }
                return this.m_StockPactRepository;
            }
        }



        #endregion

        #endregion

    }
}