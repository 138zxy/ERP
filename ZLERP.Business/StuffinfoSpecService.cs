﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Resources;
using ZLERP.Model.ViewModels;
namespace ZLERP.Business
{
    public class StuffinfoSpecService : ServiceBase<StuffinfoSpec>
    {
        internal StuffinfoSpecService(IUnitOfWork uow)
            : base(uow)
        { }

    }
}
