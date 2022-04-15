using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Resources;

namespace ZLERP.Business
{
    public class ContractService : ServiceBase<Contract>
    {
        internal ContractService(IUnitOfWork uow) : base(uow) { }

        public override Contract Add(Contract entity)
        {
            base.CheckIsAutoAudit(entity);
            //此处针对之前未在数据库作合同名唯一索引的版本做出限制
            int ret = this.Query()
                .Where(p => p.CustomerID == entity.CustomerID && p.ContractName == entity.ContractName)
                .Count();
            if (ret > 0)
            {
                throw new ApplicationException(Lang.Contract_ContractName_Exists);
            }
            var result = base.Add(entity);

            if (entity.ID == null)
            {
                throw new ApplicationException(Lang.Msg_Operate_Failed);
            }
            if (entity.IsAllConStrength)
            {
                //砼强度基准信息新增
                ContractItem contractitem = new ContractItem();

                var cons = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().Where(a => a.IsBase == true).ToList();

                decimal UpOrDown = 0;

                if (entity.DataType == 1)
                {
                    cons = cons.Where(a => a.IsSH == 1).ToList();

                    ConStrength jz = cons.Where(p => p.IsBaseCon).FirstOrDefault();
                    if (entity.BasePrice > 0 && jz != null)
                    {
                        UpOrDown = entity.BasePrice - (decimal)jz.BrandPrice;
                    }
                }
                else
                {
                    cons = cons.Where(a => a.IsSH == 0).ToList();

                    ConStrength jz = cons.Where(p => p.IsBaseCon).FirstOrDefault();
                    if (entity.BasePrice > 0 && jz != null)
                    {
                        UpOrDown = entity.BasePrice - (decimal)jz.BrandPrice;
                    }
                }
                int conscount = cons.Count;
                string[] conids = new string[conscount];
                for (int i = 0; i < cons.Count; i++)
                {
                    conids[i] = cons[i].ConStrengthCode.ToStr();
                }
                if (!Import(entity.ID, conids, UpOrDown))
                {
                    throw new ApplicationException(Lang.Msg_Operate_Failed);
                }
            }
            if (entity.IsAllPumpType)
            {
                //泵车价格基准信息新增
                ContractPumpService ContractPumpService = new ContractPumpService(this.m_UnitOfWork);
                var Pumps = this.m_UnitOfWork.GetRepositoryBase<Dic>().Query().Where(a => a.ParentID == "PumpType").ToList();
                int count = Pumps.Count;
                string[] Pumpids = new string[count];
                for (int i = 0; i < Pumps.Count; i++)
                {
                    Pumpids[i] = Pumps[i].ID;
                }
                ContractPumpService.ImportPumpType(entity.ID, Pumpids);
            }
            if (entity.IsAllIdentity && entity.DataType != 1)
            {
                //特性基准信息新增
                IdentitySettingService IdentitySettingService = new IdentitySettingService(this.m_UnitOfWork);
                var Idents = this.m_UnitOfWork.GetRepositoryBase<Identity>().Query().Where(a => a.ID != null).ToList();
                int Identcount = Idents.Count;
                string[] Identids = new string[Identcount];
                for (int i = 0; i < Idents.Count; i++)
                {
                    Identids[i] = Idents[i].ID.ToStr();
                }
                IdentitySettingService.ImportIdentity(entity.ID, Identids);
            }
            ProjectService projectse = new ProjectService(this.m_UnitOfWork);
            Project project = new Project();
            project.ContractID = entity.ID;
            project.ProjectName = entity.ContractName;
            project.Distance = entity.Distance;
            project.LinkMan = entity.ALinkMan;
            project.Tel = entity.ALinkTel;
            project.ProjectAddr = entity.ProjectAddr;
            IList<Customer> list = this.m_UnitOfWork.GetRepositoryBase<Customer>().Query().Where(p=>p.ID==entity.CustomerID).ToList();
            if (list != null)
                project.ConstructUnit = list[0].CustName;
            project.BuildUnit = entity.BuildUnit;           

            projectse.Add(project);
            return result;
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="consMixprop"></param>
        public void Audit(Contract Contract)
        {
            try
            {
                Contract contract = this.Get(Contract.ID);
                string auditor = AuthorizationService.CurrentUserID;
                contract.AuditStatus = Contract.AuditStatus;
                contract.AuditInfo = Contract.AuditInfo;
                contract.Auditor = auditor;
                contract.AuditTime = DateTime.Now;
                base.Update(contract, null);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string contractID, int auditStatus)
        {
            try
            {
                Contract contract = this.Get(contractID);
                contract.AuditStatus = 0;
                contract.Auditor = AuthorizationService.CurrentUserID;
                contract.AuditTime = DateTime.Now;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }

        /// <summary>
        /// 快速解除限制
        /// </summary>
        /// <param name="contractID">合同编号</param>
        /// <param name="contractStatus">合同状态：强制更改为进行中</param>
        /// <param name="contractLimitType">合同限制条件：强制更改为不受限制</param>
        public void QuickUnfreeze(string contractID, string contractStatus, string contractLimitType)
        {
            try
            {
                Contract contract = this.Get(contractID);
                contract.ContractStatus = contractStatus;
                contract.ContractLimitType = contractLimitType;
                contract.IsLimited = false;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }

        }
        /// <summary>
        /// 快速锁定合同
        /// </summary>
        /// <param name="contractID"></param>
        public void QuickLock(string contractID, string remark)
        {
            try
            {
                Contract contract = this.Get(contractID);
                contract.Remark = remark;
                contract.IsLimited = true;
                this.m_UnitOfWork.GetRepositoryBase<Contract>().Update(contract, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }

        public bool SetComplete(string contractID)
        {
            Contract contract = this.Get(contractID);
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {

                try
                {
                    contract.ContractStatus = "3";
                    this.Update(contract, null);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                    throw;
                }
            }
        }

        public bool SetUnComplete(string contractID)
        {
            Contract contract = this.Get(contractID);
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {

                try
                {
                    contract.ContractStatus = "2";
                    this.Update(contract, null);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                    throw;
                }
            }
        }


        /// <summary>
        /// 获取特性价格
        /// </summary>
        /// <returns></returns>
        public dynamic getIdentityPrice(string identityName, string identityType)
        {

            Identity identity = this.m_UnitOfWork.GetRepositoryBase<Identity>().Query().Where(p => (p.IdentityName == identityName && p.IdentityType == identityType)).FirstOrDefault();

            dynamic identityPriceInfo = new
               {
                   Result = true
                   ,
                   IdentityPrice = identity.IdentityPrice
               };
            return identityPriceInfo;
        }

        ///// <summary>
        ///// 运输单结算
        ///// </summary>
        ///// <param name="sd"></param>
        //public void JS(ShippingDocument sd)
        //{
        //    sd.IsJS = true;
        //    decimal price = 0;
        //    ContractItem ci = this.m_UnitOfWork.GetRepositoryBase<ContractItem>().Get(this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(sd.TaskID).ContractItemsID);
        //    List<PriceSetting> list = this.m_UnitOfWork.GetRepositoryBase<PriceSetting>().All().Where(p => p.ContractItemsID == ci.ID).OrderBy(p => p.ChangeTime).ToList();
        //    if (list.Count > 0) 
        //    {
        //        int i = 0;
        //        while (list[i].ChangeTime<sd.BuildTime)
        //        {
        //            i++;
        //        }
        //        if (i == 0) {
        //            price=(ci.UnPumpPrice==null?0:(Decimal)ci.UnPumpPrice)+(ci.PumpPrice==null?0:(Decimal)ci.PumpPrice);
        //        }
        //        else {
        //            price = (list[i - 1].UnPumpPrice == null ? 0 : (Decimal)list[i - 1].UnPumpPrice) + (ci.PumpPrice == null ? 0 : (Decimal)ci.PumpPrice);
        //        }

        //    }
        //    else
        //    {
        //        price=(ci.UnPumpPrice==null?0:(Decimal)ci.UnPumpPrice)+(ci.PumpPrice==null?0:(Decimal)ci.PumpPrice);
        //    }
        //    sd.JSPrice = sd.SignInCube * price;
        //    this.m_UnitOfWork.ShippingDocumentRepository.Update(sd, null);
        //    this.m_UnitOfWork.Flush();

        //}

        public bool Import(string contractID, string[] conStrength)
        {
            if (conStrength == null)
            {
                throw new Exception(":没有合同明细需要导入");
            }
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var conStrengths = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().Where(t => conStrength.Contains(t.ConStrengthCode)).ToList();

                    IRepositoryBase<ContractItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<ContractItem>();
                    foreach (string item in conStrength)
                    {
                        var strength = conStrengths.FirstOrDefault(t => t.ConStrengthCode == item);
                        if (strength != null)
                        {
                            ContractItem temp = new ContractItem();
                            temp.ContractID = contractID;
                            temp.ConStrength = item;
                            temp.ConStrengthID = Convert.ToInt32(strength.ID);
                            temp.UnPumpPrice = strength.BrandPrice;
                            itemResp.Add(temp);
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public bool Import(string contractID, string[] conStrength, decimal updown = 0)
        {
            if (conStrength == null)
            {
                throw new Exception(":没有合同明细需要导入");
            }
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var conStrengths = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().Where(t => conStrength.Contains(t.ConStrengthCode)).ToList();

                    IRepositoryBase<ContractItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<ContractItem>();
                    foreach (string item in conStrength)
                    {
                        //if(itemResp.All().FirstOrDefault(p => p.ContractID == contractID&&p.ConStrength== item)==null){
                        var strength = conStrengths.FirstOrDefault(t => t.ConStrengthCode == item);
                        if (strength != null)
                        {
                            ContractItem temp = new ContractItem();
                            temp.ContractID = contractID;
                            temp.ConStrength = item;
                            temp.ConStrengthID = Convert.ToInt32(strength.ID);
                            temp.UnPumpPrice = strength.BrandPrice + updown;
                            itemResp.Add(temp);
                        }
                        //}
                    }
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 批量调价功能
        /// </summary>
        /// <param name="BitUpdatePrices"></param>
        /// <returns></returns>
        public ResultInfo BitUpdatePrice(List<ZLERP.Model.ViewModels.BitUpdatePrice> BitUpdatePrices)
        {
            //1.从基准价格的基础上按百分比调整
            //2.从基准价格的基础上按数额调整
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var Price in BitUpdatePrices)
                    {
                        //1.查出当前价格的基准价
                        var cons = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().ToList();
                        Contract c = this.Get(Price.BitContractID);
                        ConStrength jz = cons.Where(p =>p.IsSH==c.DataType && p.IsBaseCon).FirstOrDefault();
                        var contractItems = this.m_UnitOfWork.GetRepositoryBase<ContractItem>().Query().Where(t => t.ContractID == Price.BitContractID).ToList();
                        foreach (var item in contractItems)
                        {
                            PriceSetting setting = new PriceSetting();
                            setting.ContractItemsID = item.ID;
                            setting.ConStrength = item.ConStrength;
                            setting.ChangeTime = Price.BitUpdateDate;
                            //按照百分比调整
                            if (Price.BitUpdateType == 0)
                            {
                                setting.UnPumpPrice = item.TranPrice + item.TranPrice * Price.BitUpdateCnt / 100;
                                setting.Remark = "基准价百分比浮动百分之" + Price.BitUpdateCnt.ToString();
                            }
                            //按照数额调整
                            if (Price.BitUpdateType == 1)
                            {
                                setting.UnPumpPrice = item.TranPrice + Price.BitUpdateCnt;
                                setting.Remark = "基准价数额浮动" + Price.BitUpdateCnt.ToString();
                            }
                            if (Price.BitUpdateType == 2)
                            {
                                ConStrength con = cons.Where(p =>p.ConStrengthCode==item.ConStrength).FirstOrDefault();
                                if (jz != null && con != null)
                                {
                                    if (con.BrandPrice == null)
                                    {
                                        setting.UnPumpPrice = 0;
                                    }
                                    else
                                    {
                                        setting.UnPumpPrice = Price.BitUpdateCnt + con.BrandPrice - (jz.BrandPrice == null ? 0 : jz.BrandPrice);
                                    }
                                }
                                setting.Remark = "基准价" + Price.BitUpdateCnt.ToString();
                            }
                            setting.Builder = AuthorizationService.CurrentUserName;
                            setting.BuildTime = DateTime.Now; 
                            this.m_UnitOfWork.GetRepositoryBase<PriceSetting>().Add(setting);

                            item.UnPumpPrice = setting.UnPumpPrice;
                            this.m_UnitOfWork.GetRepositoryBase<ContractItem>().Update(item,null);
                        }
                        //按照百分比调整
                        if (Price.BitUpdateType == 0)
                        {
                            c.BasePrice = c.BasePrice + c.BasePrice * Price.BitUpdateCnt / 100;
                            this.Update(c);
                        }
                        //按照数额调整
                        if (Price.BitUpdateType == 1)
                        {
                            c.BasePrice = c.BasePrice + Price.BitUpdateCnt;
                            this.Update(c);
                        }
                        if (Price.BitUpdateType == 2)
                        {
                            c.BasePrice = Price.BitUpdateCnt;
                            this.Update(c);
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = "" };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }



        public void UpdateBalanceRecordAndItems(string contractId)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    this.m_UnitOfWork.ContractRepository.UpdateBalanceRecordAndItems(contractId);

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
