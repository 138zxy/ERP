using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class FormulaGHService : ServiceBase<FormulaGH>
    {
        internal FormulaGHService(IUnitOfWork uow)
            : base(uow) 
        {
 
        }

        /// <summary>
        /// 配比复制功能
        /// 暂时只实现理论配比到施工配比的复制
        /// </summary>
        /// <param name="op">操作</param>
        /// <param name="sid">源ID</param>
        /// <returns></returns>
        public bool StartCopy(string op, string sid,string did)
        {
            bool result = false;
            switch (op)
            {
                case "FO2CO":
                    result = FO2CO(sid,did);
                    break;
                case "FO2CU":
                    result = FO2CU(sid, did);
                    break;
                case "CO2FO":
                    break;
                case "CO2CU":
                    break;
                case "CU2CO":
                    result = CU2CO(sid, did);
                    break;
                case "CU2FO":
                    result = CU2FO(sid, did);
                    break;
            }
            return result;
        }

        /// <summary>
        /// FormulaGH表数据导入到ConsMixprop表
        /// </summary>
        /// <param name="sid">FormulaGH表主键ID</param>
        /// <returns></returns>
        public bool FO2CO(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    FormulaGH obj = this.Get(sid);
                    IConsMixpropGHRepository m_cons = this.m_UnitOfWork.ConsMixpropGHRepository;
                    IList<ProductLine> plList = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                            .Query()
                            .Where(m => m.IsUsed)
                            .ToList();
                    ConsMixpropGH newobj = new ConsMixpropGH();
                    foreach (ProductLine pl in plList)
                    {
                        ConsMixpropGH tempobj = new ConsMixpropGH();
                        if (did.Length > 0)
                            tempobj.ID = did;
                        tempobj.ConStrength = obj.ConStrength;
                        tempobj.FlexStrength = obj.FlexStrength;
                        tempobj.FormulaID = obj.ID;
                        tempobj.IsSlurry = obj.FormulaGHType == "混凝土" ? false : true;
                        tempobj.ImpGrade = obj.ImpGrade;
                        tempobj.MixingTime = obj.MixingTime;
                        tempobj.Remark = obj.Remark;
                        tempobj.SCRate = obj.SCRate;
                        tempobj.SeasonType = obj.SeasonType;
                        tempobj.ProductLineID = pl.ID;

                        newobj = m_cons.Add(tempobj);

                        IList<FormulaGHItem> list = obj.FormulaGHItems;
                        foreach (FormulaGHItem item in list)
                        {
                            foreach (SiloProductLine sp in pl.SiloProductLines)
                            {
                                ConsMixpropItemGH temp = new ConsMixpropItemGH();
                                temp.ConsMixpropGH = tempobj;
                                temp.ConsMixpropID = tempobj.ID;
                                if (sp.Silo.StuffInfo.StuffType.ID == item.StuffType.ID)
                                {
                                    temp.Amount = item.StuffAmount * (decimal)sp.Rate;
                                    temp.Silo = sp.Silo;
                                    temp.SiloID = sp.Silo.ID;
                                    this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItemGH>().Add(temp);
                                }
                            }
                        }
                    }
                    tx.Commit();
                    return true;
                }

                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// FormulaGH表数据导入到CustMixprop表
        /// </summary>
        /// <param name="sid">FormulaGH表主键ID</param>
        /// <returns></returns>
        public bool FO2CU(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    FormulaGH obj = this.Get(sid);
                    CustMixprop newobj = new CustMixprop();
                    if (did.Length > 0)
                        newobj.ID = did;
                    newobj.CarpRadii = obj.CarpRadii;
                    newobj.CementType = obj.CementType;
                    newobj.ConStrength = obj.ConStrength;
                    newobj.ImpGrade = obj.ImpGrade;
                    newobj.Mesh = obj.Mesh;
                    newobj.MixpropCode = obj.FormulaGHName;
                    newobj.SCRate = obj.SCRate;
                    newobj.WCRate = obj.WCRate;
                    newobj.Weight = obj.Weight;

                    newobj = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Add(newobj);

                    IList<FormulaGHItem> list = obj.FormulaGHItems;
                    IList<StuffInfo> slist = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>().Query().Where(m => m.IsUsed).ToList();
                    foreach (FormulaGHItem item in list)
                    {                        
                        foreach (StuffInfo stuff in slist)
                        {
                            if (stuff.StuffTypeID.ToString() == item.StuffTypeID.ToString())
                            {                                
                                CustMixpropItem temp = new CustMixpropItem();
                                temp.Amount = item.StuffAmount;
                                temp.StandardAmount = item.StandardAmount;
                                temp.CustMixprop = newobj;
                                temp.CustMixpropID = newobj.ID;
                                temp.StuffInfo = stuff;
                                temp.StuffID = stuff.ID;
                                this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>().Add(temp);
                            }
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// CustMixprop表数据导入到FormulaGH表
        /// </summary>
        /// <param name="sid">CustMixprop表主键ID</param>
        /// <returns></returns>
        public bool CU2FO(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    CustMixprop obj = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Get(sid);
                    FormulaGH newobj = new FormulaGH();
                    if (did.Length > 0)
                        newobj.ID = did;
                    newobj.CarpRadii = obj.CarpRadii;
                    newobj.CementType = obj.CementType;
                    newobj.ConStrength = obj.ConStrength;
                    newobj.ImpGrade = obj.ImpGrade;
                    newobj.Mesh = obj.Mesh;
                    newobj.FormulaGHName = obj.MixpropCode;
                    newobj.SCRate = obj.SCRate;
                    newobj.WCRate = obj.WCRate;
                    newobj.Weight = obj.Weight;
                    newobj = this.m_UnitOfWork.GetRepositoryBase<FormulaGH>().Add(newobj);

                    IList<CustMixpropItem> list = obj.CustMixpropItems;
                    foreach (CustMixpropItem item in list)
                    {
                        IList<StuffType> slist = this.m_UnitOfWork.GetRepositoryBase<StuffType>().All();
                        foreach (StuffType stype in slist)
                        {
                            if (item.StuffInfo.StuffType.ID == stype.ID)
                            {
                                FormulaGHItem temp = new FormulaGHItem();
                                temp.StandardAmount = item.StandardAmount ?? 0;
                                temp.StuffAmount = item.Amount??0;
                                temp.StuffType = stype;
                                temp.StuffTypeID = stype.ID;
                                temp.FormulaGH = newobj;
                                temp.FormulaGHID = newobj.ID;
                                this.m_UnitOfWork.GetRepositoryBase<FormulaGHItem>().Add(temp);
                            }
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// CustMixprop表数据导入到ConsMixprop表
        /// </summary>
        /// <param name="sid">CustMixprop表主键ID</param>
        /// <returns></returns>
        public bool CU2CO(string sid, string did)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    CustMixprop obj = this.m_UnitOfWork.GetRepositoryBase<CustMixprop>().Get(sid);
                    IConsMixpropGHRepository m_cons = this.m_UnitOfWork.ConsMixpropGHRepository;
                    IList<ProductLine> plList = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                            .Query()
                            .Where(m => m.IsUsed)
                            .ToList();
                    ConsMixpropGH newobj = new ConsMixpropGH();
                    foreach (ProductLine pl in plList)
                    {
                        ConsMixpropGH tempobj = new ConsMixpropGH();
                        if (did.Length > 0)
                            tempobj.ID = did;
                        tempobj.ConStrength = obj.ConStrength;
                        tempobj.FormulaID = obj.ID;
                        tempobj.IsSlurry = obj.CementType == "混凝土" ? false : true;
                        tempobj.ImpGrade = obj.ImpGrade;
                        tempobj.SCRate = obj.SCRate;
                        tempobj.ProductLineID = pl.ID;

                        newobj = m_cons.Add(tempobj);

                        IList<CustMixpropItem> list = obj.CustMixpropItems;
                        foreach (CustMixpropItem item in list)
                        {
                            foreach (SiloProductLine sp in pl.SiloProductLines)
                            {
                                ConsMixpropItemGH temp = new ConsMixpropItemGH();
                                temp.ConsMixpropGH = tempobj;
                                temp.ConsMixpropID = tempobj.ID;
                                if (sp.Silo.StuffInfo.StuffType.ID == item.StuffInfo.StuffType.ID)
                                {
                                    temp.Amount = (decimal)item.Amount * (decimal)sp.Rate;
                                    temp.Silo = sp.Silo;
                                    temp.SiloID = sp.Silo.ID;
                                    this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItemGH>().Add(temp);
                                }
                            }
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public bool ExportStuff(string FormulaGHid)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<FormulaGHItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<FormulaGHItem>();
                    IRepositoryBase<StuffType> typeResp = this.m_UnitOfWork.GetRepositoryBase<StuffType>();
                    IList<StuffType> list = typeResp.Query().Where(m=>m.TypeID=="StuffType"&&m.StuffTypeName.Contains("干混")).ToList();

                    IList<FormulaGHItem> items = itemResp.Query()
                        .Where(m => m.FormulaGHID == FormulaGHid)
                        .ToList();
                    foreach (FormulaGHItem item in items)
                    {
                        itemResp.Delete(item);
                    }
                    foreach (StuffType item in list)
                    {
                        FormulaGHItem temp = new FormulaGHItem();
                        temp.StuffTypeID = item.ID;
                        temp.StuffAmount = 0;
                        temp.StandardAmount = 0;
                        temp.FormulaGHID = FormulaGHid;
                        itemResp.Add(temp);                        
                    }
                    tx.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    return false;
                }
            }

        }

        public override void Delete(FormulaGH entity)
        {
           
                try
                {

                    this.m_UnitOfWork.GetRepositoryBase<FormulaGH>().Update(entity, null);
                    base.Delete(entity);
                   
                }
                catch (Exception ex)
                {
                   
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            
        }

        public override FormulaGH Add(FormulaGH entity)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                FormulaGH FormulaGH = null;
                try
                {
                    FormulaGH = base.Add(entity);
                    this.m_UnitOfWork.Flush();

                    IRepositoryBase<FormulaGHItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<FormulaGHItem>();
                    IRepositoryBase<StuffType> typeResp = this.m_UnitOfWork.GetRepositoryBase<StuffType>();
                    IList<StuffType> list = typeResp.Query().Where(m => (m.TypeID == "StuffType" && m.IsUsed && m.StuffTypeName.Contains("干混"))).OrderBy(m => m.OrderNum).ToList();

                    foreach (StuffType item in list)
                    {
                        FormulaGHItem temp = new FormulaGHItem();
                        temp.StuffTypeID = item.ID;
                        temp.StuffAmount = 0;
                        temp.StandardAmount = 0;
                        temp.FormulaGHID = FormulaGH.ID;
                        itemResp.Add(temp);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
                return FormulaGH;
            }
        }
    }
}
