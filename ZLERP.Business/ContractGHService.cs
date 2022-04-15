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
    public class ContractGHService : ServiceBase<ContractGH>
    {
        internal ContractGHService(IUnitOfWork uow) : base(uow) { }

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
                        ContractGH c = this.Get(Price.BitContractID);
                        ConStrength jz = cons.Where(p => p.IsSH == 2 && p.IsBaseCon).FirstOrDefault();
                        var contractItems = this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>().Query().Where(t => t.ContractID == Price.BitContractID).ToList();
                        foreach (var item in contractItems)
                        {
                            PriceSettingGH setting = new PriceSettingGH();
                            setting.ContractItemsID = item.ID;
                            setting.ConStrength = item.ConStrength;
                            setting.ChangeTime = Price.BitUpdateDate;
                            //按照百分比调整
                            if (Price.BitUpdateType == 0)
                            {
                                setting.UnPumpPrice = item.TranPrice + item.TranPrice * Price.BitUpdateCnt / 100;
                                //setting.Remark = "基准价百分比浮动百分之" + Price.BitUpdateCnt.ToString();
                            }
                            //按照数额调整
                            if (Price.BitUpdateType == 1)
                            {
                                setting.UnPumpPrice = item.TranPrice + Price.BitUpdateCnt;
                                //setting.Remark = "基准价数额浮动" + Price.BitUpdateCnt.ToString();
                            } 
                            if (Price.BitUpdateType == 2)
                            {
                                ConStrength con = cons.Where(p => p.ConStrengthCode == item.ConStrength).FirstOrDefault();
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
                                //setting.Remark = "基准价" + Price.BitUpdateCnt.ToString();
                            }
                            setting.Builder = AuthorizationService.CurrentUserName;
                            setting.BuildTime = DateTime.Now;
                            this.m_UnitOfWork.GetRepositoryBase<PriceSettingGH>().Add(setting);

                            item.UnPumpPrice = setting.UnPumpPrice;
                            item.AuditStatus = 1;
                            this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>().Update(item, null);
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


        public override ContractGH Add(ContractGH entity)
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

            ////砼强度基准信息新增
            //ContractItem contractitem = new ContractItem();
            //var cons = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().Where(a => a.IsBase == true).ToList();

            //int conscount = cons.Count;
            //string[] conids = new string[conscount];
            //for (int i = 0; i < cons.Count; i++)
            //{
            //    conids[i] = cons[i].ConStrengthCode.ToStr();
            //}
            //if (!Import(entity.ID, conids))
            //{
            //    throw new ApplicationException(Lang.Msg_Operate_Failed);
            //}
            //砼强度基准信息新增
            ContractItemGH contractitem = new ContractItemGH();

            var cons = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().Where(a => a.IsBase == true && a.IsSH == 2).ToList();

            decimal UpOrDown = 0;

            ConStrength jz = cons.Where(p => p.IsBaseCon).FirstOrDefault();
                if (entity.BasePrice > 0 && jz != null)
                {
                    UpOrDown = entity.BasePrice - (decimal)jz.BrandPrice;
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

            ProjectGHService projectse = new ProjectGHService(this.m_UnitOfWork);
            ProjectGH project = new ProjectGH();
            project.ContractID = entity.ID;
            project.ProjectName = entity.ContractName;
            project.Distance = entity.Distance;
            project.LinkMan = entity.ALinkMan;
            project.Tel = entity.ALinkTel;
            project.ProjectAddr = entity.ProjectAddr;

            projectse.Add(project);
            return result;
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

                    IRepositoryBase<ContractItemGH> itemResp = this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>();
                    foreach (string item in conStrength)
                    {
                        var strength = conStrengths.FirstOrDefault(t => t.ConStrengthCode == item);
                        if (strength != null)
                        {
                            ContractItemGH temp = new ContractItemGH();
                            temp.ContractID = contractID;
                            temp.ConStrength = item;
                            temp.UnPumpPrice = strength.BrandPrice + updown;
                            temp.AuditStatus = 1;
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

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        public void UnAudit(string contractID, int auditStatus)
        {
            try
            {
                ContractGH contractGH = this.Get(contractID);
                contractGH.AuditStatus = 0;
                contractGH.Auditor = AuthorizationService.CurrentUserID;
                contractGH.AuditTime = DateTime.Now;
                this.m_UnitOfWork.GetRepositoryBase<ContractGH>().Update(contractGH, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="consMixprop"></param>
        public bool Audit(ContractGH ContractGH)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ContractGH contractGH = this.Get(ContractGH.ID);
                    string auditor = AuthorizationService.CurrentUserID;
                    contractGH.AuditStatus = ContractGH.AuditStatus;
                    contractGH.AuditInfo = ContractGH.AuditInfo;
                    contractGH.Auditor = auditor;
                    contractGH.AuditTime = DateTime.Now;
                    base.Update(contractGH, null);

                    //批量审核通过
                    ContractItemGHService s = new ContractItemGHService(this.m_UnitOfWork);
                    IList<ContractItemGH> list = s.Query().Where(m => m.ContractID == contractGH.ID).ToList();
                    foreach (ContractItemGH item in list)
                    {
                        item.AuditStatus = 1;
                        s.Update(item);
                    }

                    tx.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                    throw e;
                }
            }
        }

        #region 合同审核模块
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool CancelAudit(string[] ids, ref string errmsg)
        {
            bool AuditResult = true;
            try
            {
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {//防止提交空id出错
                        var contractGH = this.Get(id);
                        if (contractGH != null)
                        {
                            contractGH.AuditStatus = 0;
                            AuditResult = CancelAuditing(contractGH);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("合同取消审核失败", ex);
                errmsg = ex.Message;
                return false;
            }
            return AuditResult;
        }

        public bool CancelAuditing(ContractGH contractGH)
        {
            if (contractGH != null)
            {
                //有工地计划单就不能修改了
                CustomerPlanGH plan = this.m_UnitOfWork.GetRepositoryBase<CustomerPlanGH>().Query().Where(m => m.ContractID == contractGH.ID).FirstOrDefault();
                if (plan != null)
                {
                    throw new Exception("合同已经下计划，不能取消审核！");
                }
                else
                {
                    //更新contractGH表记录
                    contractGH.AuditTime = null;
                    contractGH.Auditor = string.Empty;
                    this.Update(contractGH, null);
                    return true;
                }
            }
            return false;
        }




        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Audit(string[] ids, ref string errmsg)
        {
            bool AuditResult = true;
            try
            {
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {//防止提交空id出错
                        var contractGH = this.Get(id);
                        if (contractGH != null)
                        {
                            contractGH.AuditStatus = 1;
                            AuditResult = Auditing(contractGH);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("合同取消审核失败", ex);
                errmsg = ex.Message;
                return false;
            }
            return AuditResult;
        }

        public bool Auditing(ContractGH contractGH)
        {
            if (contractGH != null)
            {
                //更新contractGH表记录
                contractGH.AuditTime = DateTime.Now;
                contractGH.Auditor = AuthorizationService.CurrentUserID;
                this.Update(contractGH, null);
                return true;

            }
            return false;
        }


        #endregion
      
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
                ContractGH contractGH = this.Get(contractID);
                contractGH.ContractStatus = contractStatus;
                contractGH.ContractLimitType = contractLimitType;
                contractGH.IsLimited = false;
                this.m_UnitOfWork.GetRepositoryBase<ContractGH>().Update(contractGH, null);
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
                ContractGH contractGH = this.Get(contractID);
                contractGH.Remark = remark;
                contractGH.IsLimited = true;
                this.m_UnitOfWork.GetRepositoryBase<ContractGH>().Update(contractGH, null);
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
            ContractGH contractGH = this.Get(contractID);
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {

                try
                {
                    contractGH.ContractStatus = "3";
                    this.Update(contractGH, null);
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
            ContractGH contractGH = this.Get(contractID);
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {

                try
                {
                    contractGH.ContractStatus = "2";
                    this.Update(contractGH, null);
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

        /// <summary>
        /// 运输单结算
        /// </summary>
        /// <param name="sd"></param>
        public void JS(ShippingDocumentGH sd)
        {
            sd.IsJS = true;
            decimal price = 0;
            ContractItemGH ci = this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>().Get(this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(sd.TaskID).ContractItemsID);
            List<PriceSetting> list = this.m_UnitOfWork.GetRepositoryBase<PriceSetting>().All().Where(p => p.ContractItemsID == ci.ID).OrderBy(p => p.ChangeTime).ToList();
            if (list.Count > 0) 
            {
                int i = 0;
                while (list[i].ChangeTime<sd.BuildTime)
                {
                    i++;
                }
                if (i == 0) {
                    price=(ci.UnPumpPrice==null?0:(Decimal)ci.UnPumpPrice)+(ci.PumpPrice==null?0:(Decimal)ci.PumpPrice);
                }
                else {
                    price = (list[i - 1].UnPumpPrice == null ? 0 : (Decimal)list[i - 1].UnPumpPrice) + (ci.PumpPrice == null ? 0 : (Decimal)ci.PumpPrice);
                }

            }
            else
            {
                price=(ci.UnPumpPrice==null?0:(Decimal)ci.UnPumpPrice)+(ci.PumpPrice==null?0:(Decimal)ci.PumpPrice);
            }
            sd.JSPrice = sd.SignInCube * price;
            this.m_UnitOfWork.ShippingDocumentGHRepository.Update(sd, null);
            this.m_UnitOfWork.Flush();
            
        }

        //public bool Import(string contractID, string[] conStrength)
        //{
        //    if (conStrength == null)
        //    {
        //        throw new Exception(":没有合同明细需要导入");
        //    }
        //    using (var tx = this.m_UnitOfWork.BeginTransaction())
        //    {
        //        try
        //        {
        //            IRepositoryBase<ContractItemGH> itemResp = this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>();
        //            foreach (string item in conStrength)
        //            {
        //                if (item != "")
        //                {
        //                    ContractItemGH temp = new ContractItemGH();
        //                    temp.ContractID = contractID;
        //                    temp.ConStrength = item;
        //                    itemResp.Add(temp);
        //                }
        //            }
        //            tx.Commit();
        //            return true;
        //        }
        //        catch
        //        {
        //            tx.Rollback();
        //            throw;
        //        }
        //    }
        //}
        
    }
}
