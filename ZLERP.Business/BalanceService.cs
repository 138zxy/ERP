using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class BalanceService : ServiceBase<Balance>
    {
        internal BalanceService(IUnitOfWork uow) : base(uow) { }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendMsg(string msg)
        {
            SystemMsgService systemMsgService = new SystemMsgService(this.m_UnitOfWork);
            bool issuccess = systemMsgService.SendMsg("CustomerRecharge", msg);
            return issuccess;
        }
    }
}
