using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class IdentitySettingService : ServiceBase<IdentitySetting>
    {
        internal IdentitySettingService(IUnitOfWork uow) : base(uow) { }

        public void ImportIdentity(string contractID, string[] ids)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (string identity in ids)
                    {
                        Identity ide = this.m_UnitOfWork.GetRepositoryBase<Identity>().Get(Convert.ToInt32(identity));
                        IdentitySetting IdentitySetting = new IdentitySetting();
                        IdentitySetting.ContractID = contractID;
                        IdentitySetting.IdentityName = ide.IdentityName;
                        IdentitySetting.IdentityType = ide.IdentityType;
                        IdentitySetting.IdentityPrice = ide.IdentityPrice;
                        IdentitySetting.SetDate = DateTime.Now;
                        this.m_UnitOfWork.GetRepositoryBase<IdentitySetting>().Add(IdentitySetting);
                        this.m_UnitOfWork.Flush();
                    }
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                    throw e;
                }
            }
        }
    }
}
