using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using ZLERP.IRepository;
using ZLERP.Model;
using System.Collections;
using ZLERP.Model.Material;

namespace ZLERP.Business.Material
{
    /*
        此模块是原材料管理功能服务类模块，做事务处理 by  mandzy 2019-01-24
     */
    public class MaterialService : ServiceBase<M_Car>
    {
        internal MaterialService(IUnitOfWork uow) : base(uow) { }

        private object ObjLock = new object();
        #region 预付款支付
        /// <summary>
        /// 预付款支付
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PerPay(M_YingSFrec yingsf)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    M_YingSFrec YingSFrec = new M_YingSFrec();
                    YingSFrec.UnitID = yingsf.UnitID;
                    YingSFrec.Source = M_Common.OtherInfo.PerPay;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;
                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = yingsf.FinanceDate;
                    YingSFrec.FinanceMoney = yingsf.FinanceMoney;
                    YingSFrec.PayFavourable = 0;
                    YingSFrec.PayType = yingsf.PayType;
                    YingSFrec.Payer = yingsf.Payer;
                    YingSFrec.Gatheringer = yingsf.Gatheringer;
                    YingSFrec.Remark = yingsf.Remark;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_YingSFrec>().Add(YingSFrec);


                    //2.更新供应商的预付款
                    var fin = this.m_UnitOfWork.GetRepositoryBase<M_FinanceRecord>().Query().Where(t => t.FinanceType == yingsf.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(yingsf.UnitID.ToString());


                    M_FinanceRecord FinanceRecord = new M_FinanceRecord();
                    FinanceRecord.FinanceType = yingsf.PayType;
                    FinanceRecord.IsInOrOut = M_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = yingsf.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - yingsf.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = -yingsf.FinanceMoney;
                    }
                    FinanceRecord.Operater = yingsf.Builder;
                    FinanceRecord.UseType = M_Common.OtherInfo.PerPay; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "预付 " + Supplier.SupplyName;
                    this.m_UnitOfWork.GetRepositoryBase<M_FinanceRecord>().Add(FinanceRecord);

                    //3.插入资产记录的总金额 
                    if (yingsf.FinanceMoney != 0)
                    {
                        Supplier.PrePay = Supplier.PrePay + yingsf.FinanceMoney;
                        SupplierService.Update(Supplier, null);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }


        /// <summary>
        /// 运输公司预付款
        /// </summary>
        /// <param name="yingsf"></param>
        /// <returns></returns>
        public ResultInfo PerPayTran(M_TranYingSFrec yingsf)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    M_TranYingSFrec YingSFrec = new M_TranYingSFrec();
                    YingSFrec.UnitID = yingsf.UnitID;
                    YingSFrec.Source = M_Common.OtherInfo.PerPay;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;
                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = yingsf.FinanceDate;
                    YingSFrec.FinanceMoney = yingsf.FinanceMoney;
                    YingSFrec.PayFavourable = 0;
                    YingSFrec.PayType = yingsf.PayType;
                    YingSFrec.Payer = yingsf.Payer;
                    YingSFrec.Gatheringer = yingsf.Gatheringer;
                    YingSFrec.Remark = yingsf.Remark;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranYingSFrec>().Add(YingSFrec);


                    //2.更新供应商的预付款
                    var fin = this.m_UnitOfWork.GetRepositoryBase<M_TranFinanceRecord>().Query().Where(t => t.FinanceType == yingsf.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(yingsf.UnitID.ToString());


                    M_TranFinanceRecord FinanceRecord = new M_TranFinanceRecord();
                    FinanceRecord.FinanceType = yingsf.PayType;
                    FinanceRecord.IsInOrOut = M_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = yingsf.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - yingsf.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = -yingsf.FinanceMoney;
                    }
                    FinanceRecord.Operater = yingsf.Builder;
                    FinanceRecord.UseType = M_Common.OtherInfo.PerPay; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "预付 " + Supplier.SupplyName;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranFinanceRecord>().Add(FinanceRecord);

                    //3.插入资产记录的总金额 
                    if (yingsf.FinanceMoney != 0)
                    {
                        Supplier.PrePay = Supplier.PrePay + yingsf.FinanceMoney;
                        SupplierService.Update(Supplier, null);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }
        /// <summary>
        /// 货款结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo BaleBalance(List<string> ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    //排除已经生成结算单的入库单
                    var stuffids = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalanceDel>().Query().Where(t => ids.Contains(t.StuffInID)).Select(t => t.StuffInID);
                    if (stuffids != null && stuffids.Count() > 0)
                    {
                        foreach (var id in stuffids)
                        {
                            ids.Remove(id);
                        }
                    }
                    if (ids.Count <= 0)
                    {
                        return new ResultInfo() { Result = true, Message = "没有符合要求的入库单" };
                    }
                    //查询出所有入库单进行汇总作业 
                    var query = this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Query().Where(t => ids.Contains(t.ID) && t.Lifecycle==3).ToList();

                    //更新此时的入库单的单价
                    var SupplyIids = query.Select(t => t.SupplyID).Distinct().ToList();

                    var stockPact = this.m_UnitOfWork.GetRepositoryBase<StockPact>().Query().Where(t => SupplyIids.Contains(t.SupplyID)).ToList();

                    decimal AllCount = 0;
                    decimal AllMoney = 0;
                    var bale = (from q in query
                                group q by new { q.SupplyID, q.StuffID } into g
                                select new
                                {
                                    SupplyID = g.Key.SupplyID,
                                    StuffID = g.Key.StuffID
                                }).ToList();

                    foreach (var b in bale)
                    {
                        //新增明细
                        var listDel = query.Where(t => t.SupplyID == b.SupplyID && t.StuffID == b.StuffID).ToList();
                        //新增结算单主体数据
                        M_BaleBalance BaleBalance = new M_BaleBalance();
                        lock (ObjLock)
                        {
                            System.Threading.Thread.Sleep(100);
                            string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                            BaleBalance.BaleNo = string.Format("{0}{1}", "B", orderNo);
                        }
                        BaleBalance.OrderDate = DateTime.Now;
                    
                        BaleBalance.StockPactID = b.SupplyID;
                        BaleBalance.ThatBaleMan = "";
                        BaleBalance.StuffID = b.StuffID;
                        BaleBalance.StartDate = listDel.Min(t => t.InDate);
                        BaleBalance.EndDate = listDel.Max(t => t.InDate);
                        string EndDay = base.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(a => a.ConfigName == "ChangeDay").First().ConfigValue.ToStr();
                        BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).ToString("yyyy-MM");
                        if (Convert.ToDateTime(BaleBalance.EndDate).Day > EndDay.ToInt())
                        {
                            BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).AddMonths(1).ToString("yyyy-MM");
                        }
                        BaleBalance.BaleType = "按结算重量";
                        BaleBalance.IsOnePrice = false;
                        BaleBalance.OnePrice = 0;
                        BaleBalance.IsStockPrice = true;

                        BaleBalance.BaleDate = DateTime.Now;
                        BaleBalance.BaleMan = AuthorizationService.CurrentUserName;
                        BaleBalance.Remark = "";

                        List<M_BaleBalanceDel> M_BaleBalanceDels = new List<M_BaleBalanceDel>();
                        foreach (var d in listDel)
                        {
                            decimal UnitPrice = 0;
                            decimal TotalPrice = 0;
                            var stock = stockPact.FirstOrDefault(t => t.SupplyID == d.SupplyID);
                            if (stock != null)
                            {
                                var setPrice = stock.StockPactPriceSets.Where(t => t.StuffID == d.StuffID && t.SpecID == d.SpecID && t.ChangeTime < d.InDate).OrderByDescending(t => t.ChangeTime).FirstOrDefault();
                                if (setPrice != null)
                                {
                                    UnitPrice = Convert.ToDecimal(setPrice.Price);

                                }
                                else
                                {
                                    if (d.StuffID == stock.StuffID && d.SpecID.ToStr() == stock.SpecID.ToStr())
                                    {
                                        UnitPrice = Convert.ToDecimal(stock.StockPrice);
                                    }
                                    else if (d.StuffID == stock.StuffID1 && d.SpecID.ToStr() == stock.SpecID1.ToStr())
                                    {
                                        UnitPrice = Convert.ToDecimal(stock.StockPrice1);
                                    }
                                    else if (d.StuffID == stock.StuffID2 && d.SpecID.ToStr() == stock.SpecID2.ToStr())
                                    {
                                        UnitPrice = Convert.ToDecimal(stock.StockPrice2);
                                    }
                                    else if (d.StuffID == stock.StuffID3 && d.SpecID.ToStr() == stock.SpecID3.ToStr())
                                    {
                                        UnitPrice = Convert.ToDecimal(stock.StockPrice3);
                                    }
                                    else if (d.StuffID == stock.StuffID4 && d.SpecID.ToStr() == stock.SpecID4.ToStr())
                                    {
                                        UnitPrice = Convert.ToDecimal(stock.StockPrice4);
                                    }
                                }
                                TotalPrice = UnitPrice * Convert.ToDecimal(d.FinalFootNum)/1000; 
                            }
                            AllCount += Convert.ToDecimal(d.FinalFootNum);
                            AllMoney += TotalPrice;
                            M_BaleBalanceDel BaleBalanceDel = new M_BaleBalanceDel();

                            BaleBalanceDel.StuffInID = d.ID;
                            BaleBalanceDel.SpecID = d.SpecID;
                            BaleBalanceDel.Price = UnitPrice;
                            BaleBalanceDel.OtherMoney = 0;
                            BaleBalanceDel.AllMoney = TotalPrice;
                            M_BaleBalanceDels.Add(BaleBalanceDel);

                        }
                        BaleBalance.AllCount = AllCount;
                        BaleBalance.AllMoney = AllMoney;
                        BaleBalance.AllOkCount = AllCount;
                        BaleBalance.AllOkMoney = AllMoney;
                        BaleBalance.PayMoney = 0;
                        BaleBalance.PayOwing = AllMoney;
                        BaleBalance.PayFavourable = 0;

                        var m_bale = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalance>().Add(BaleBalance);
                        foreach (var BaleBalanceDel in M_BaleBalanceDels)
                        {
                            BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            this.m_UnitOfWork.GetRepositoryBase<M_BaleBalanceDel>().Add(BaleBalanceDel);
                        }
                    }
                    tx.Commit();
                    if (bale != null && bale.Count > 0)
                    {
                        return new ResultInfo() { Result = true, Message = string.Format("操作成功,共产生结算单:{0}个", bale.Count.ToString()) };
                    }
                    else
                    {
                        return new ResultInfo() { Result = false, Message = string.Format("操作失败，无有效的单据能产生结算单") };
                    }
            
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }
        /// <summary>
        /// 运费结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo TranBalance(List<string> ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    //排除已经生成结算单的入库单
                    var stuffids = this.m_UnitOfWork.GetRepositoryBase<M_TranBalanceDel>().Query().Where(t => ids.Contains(t.StuffInID)).Select(t => t.StuffInID);
                    if (stuffids != null && stuffids.Count() > 0)
                    {
                        foreach (var id in stuffids)
                        {
                            ids.Remove(id);
                        }
                    }
                    if (ids.Count <= 0)
                    {
                        return new ResultInfo() { Result = true, Message = "没有符合要求的入库单" };
                    }
                    //查询出所有入库单进行汇总作业 
                    var query = this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Query().Where(t => ids.Contains(t.ID) && t.Lifecycle == 3).ToList();

                    //更新此时的入库单的运输单价
                    var Transportid = query.Select(t => t.TransportID).Distinct().ToList();

                    var transPrice = this.m_UnitOfWork.GetRepositoryBase<TransPrice>().Query().Where(t => Transportid.Contains(t.TransportID)).ToList();

                    var bale = from q in query
                               group q by new { q.TransportID, q.StuffID } into g
                               select new
                               {
                                   TransportID = g.Key.TransportID,
                                   StuffID = g.Key.StuffID
                               };

                    foreach (var b in bale)
                    {
                        //新增明细
                        var listDel = query.Where(t => t.TransportID == b.TransportID && t.StuffID == b.StuffID).ToList();
                        //新增结算单主体数据
                        M_TranBalance BaleBalance = new M_TranBalance();
                        lock (ObjLock)
                        {
                            System.Threading.Thread.Sleep(100);
                            string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                            BaleBalance.BaleNo = string.Format("{0}{1}", "T", orderNo);
                        }
                        BaleBalance.OrderDate = DateTime.Now;

                        BaleBalance.TranID = b.TransportID;
                        BaleBalance.ThatBaleMan = "";
                        BaleBalance.StuffID = b.StuffID;
                        BaleBalance.StartDate = listDel.Min(t => t.InDate);
                        BaleBalance.EndDate = listDel.Max(t => t.InDate);
                        string EndDay = base.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(a => a.ConfigName == "ChangeDay").First().ConfigValue.ToStr();
                        BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).ToString("yyyy-MM");
                        if (Convert.ToDateTime(BaleBalance.EndDate).Day > EndDay.ToInt())
                        {
                            BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).AddMonths(1).ToString("yyyy-MM");
                        }
                        BaleBalance.BaleType = "按重量";
                        BaleBalance.IsOnePrice = false;
                        BaleBalance.OnePrice = 0;
                        BaleBalance.IsStockPrice = true;


                        BaleBalance.BaleDate = DateTime.Now;
                        BaleBalance.BaleMan = AuthorizationService.CurrentUserName;
                        BaleBalance.Remark = "自动生成的结算单";

                        List<M_TranBalanceDel> M_TranBalanceDels = new List<M_TranBalanceDel>();

                        decimal AllCount = 0;
                        decimal AllMoney = 0;
                        foreach (var d in listDel)
                        {
                            decimal UnitPrice = 0;
                            decimal TotalPrice = 0;
                            var tran = transPrice.FirstOrDefault(t => t.TransportID == d.TransportID && t.StuffID == d.StuffID && t.SupplyID == d.SupplyID);
                            if (tran != null)
                            {
                                var setPrice = tran.M_TransPrices.Where(t => t.PriceDate < d.InDate).OrderByDescending(t => t.PriceDate).FirstOrDefault();
                                if (setPrice != null)
                                {
                                    UnitPrice = setPrice.UnitPrice;
                                }
                                else
                                {
                                    UnitPrice = tran.UnitPrice;
                                }
                                TotalPrice = UnitPrice * Convert.ToDecimal(d.FinalFootNum) / 1000;
                            }

                            M_TranBalanceDel BaleBalanceDel = new M_TranBalanceDel();
                            BaleBalanceDel.StuffInID = d.ID;
                            BaleBalanceDel.Price = UnitPrice;
                            BaleBalanceDel.OtherMoney = 0;
                            BaleBalanceDel.AllMoney = TotalPrice;
                            M_TranBalanceDels.Add(BaleBalanceDel);
                            AllMoney += TotalPrice;
                            AllCount += Convert.ToDecimal(d.FinalFootNum);
                        }

                        BaleBalance.AllCount = AllCount;
                        BaleBalance.AllMoney = AllMoney;
                        BaleBalance.AllOkCount = AllCount;
                        BaleBalance.AllOkMoney = AllMoney;
                        BaleBalance.PayMoney = 0;
                        BaleBalance.PayOwing = AllMoney;
                        BaleBalance.PayFavourable = 0;
                        var m_bale = this.m_UnitOfWork.GetRepositoryBase<M_TranBalance>().Add(BaleBalance);
                        foreach (var BaleBalanceDel in M_TranBalanceDels)
                        {
                            BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            this.m_UnitOfWork.GetRepositoryBase<M_TranBalanceDel>().Add(BaleBalanceDel);
                        }
                    }
                    tx.Commit();
                    if (bale != null && bale.Count() > 0)
                    {
                        return new ResultInfo() { Result = true, Message = string.Format("操作成功,共产生结算单:{0}个", bale.Count().ToString()) };
                    }
                    else
                    {
                        return new ResultInfo() { Result = false, Message = string.Format("操作失败，无有效的单据能产生结算单") };
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }
        #endregion


        #region 删除结算单

        /// <summary>
        /// 删除结算单的时候，必须同步删除明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo DeleteBaleBalance(string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var service = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalance>();
                    var serviceDel = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalanceDel>();
                    foreach (var id in ids)
                    {
                        var bale = service.Get(id);
                        service.Delete(bale);
                        var BaleBalanceID = Convert.ToInt32(id);
                        var query = serviceDel.Query().Where(t => t.BaleBalanceID == BaleBalanceID);
                        foreach (var q in query)
                        {
                            serviceDel.Delete(q);
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功" };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }

        }
        /// <summary>
        /// 删除结算单的时候，必须同步删除明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo DeleteTranBalance(string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var service = this.m_UnitOfWork.GetRepositoryBase<M_TranBalance>();
                    var serviceDel = this.m_UnitOfWork.GetRepositoryBase<M_TranBalanceDel>();
                    foreach (var id in ids)
                    {
                        var bale = service.Get(id);
                        service.Delete(bale);
                        var BaleBalanceID = Convert.ToInt32(id);
                        var query = serviceDel.Query().Where(t => t.BaleBalanceID == BaleBalanceID);
                        foreach (var q in query)
                        {
                            serviceDel.Delete(q);
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功" };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }

        }
        #endregion
        /// <summary>
        ///重新计算结算单，包括更新入库单的价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo Compute(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var bale = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalance>().Get(id);
                    var BaleBalanceID = Convert.ToInt32(id);
                    var M_Del = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalanceDel>().Query().Where(t => t.BaleBalanceID == BaleBalanceID);
                    decimal AllCount = 0;
                    decimal AllMoney = 0;
                    #region 如果是合同价结算
                    if (bale.IsStockPrice)
                    {
                        //获取合同信息 ,并重新计算入库单材料的单据
                        var stock = this.m_UnitOfWork.GetRepositoryBase<StockPact>().Query().Where(t => t.SupplyID == bale.StockPactID).FirstOrDefault();
                        if (stock == null)
                        {
                            return new ResultInfo() { Result = false, Message = "未设置原材料定价信息" };
                        }
                        foreach (var del in M_Del)
                        {
                            var q = this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Get(del.StuffInID);
                            decimal UnitPrice = 0;
                            decimal TotalPrice = 0;
                            var setPrice = stock.StockPactPriceSets.Where(t => t.StuffID == q.StuffID && t.SpecID == q.SpecID && t.ChangeTime < q.InDate).OrderByDescending(t => t.ChangeTime).FirstOrDefault();

                            if (setPrice != null)
                            {
                                UnitPrice = Convert.ToDecimal(setPrice.Price);
                            }
                            else
                            {
                                if (q.StuffID == stock.StuffID && q.SpecID.ToStr() == stock.SpecID.ToStr())
                                {
                                    UnitPrice = Convert.ToDecimal(stock.StockPrice);
                                }
                                else if (q.StuffID == stock.StuffID1 && q.SpecID.ToStr() == stock.SpecID1.ToStr())
                                {
                                    UnitPrice = Convert.ToDecimal(stock.StockPrice1);
                                }
                                else if (q.StuffID == stock.StuffID2 && q.SpecID.ToStr() == stock.SpecID2.ToStr())
                                {
                                    UnitPrice = Convert.ToDecimal(stock.StockPrice2);
                                }
                                else if (q.StuffID == stock.StuffID3 && q.SpecID.ToStr() == stock.SpecID3.ToStr())
                                {
                                    UnitPrice = Convert.ToDecimal(stock.StockPrice3);
                                }
                                else if (q.StuffID == stock.StuffID4 && q.SpecID.ToStr() == stock.SpecID4.ToStr())
                                {
                                    UnitPrice = Convert.ToDecimal(stock.StockPrice4);
                                }
                            }
                            if (bale.BaleType == "按结算重量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.FinalFootNum) / 1000;
                            }
                            if (bale.BaleType == "按厂商数量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.SupplyNum) / 1000;
                            }
                            if (bale.BaleType == "按方量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.Volume);
                            }
                            if (bale.BaleType == "按结算重量")
                            {
                                AllCount += Convert.ToDecimal(q.FinalFootNum);
                            }
                            if (bale.BaleType == "按厂商数量")
                            {
                                AllCount += Convert.ToDecimal(q.SupplyNum);
                            }
                            if (bale.BaleType == "按方量")
                            {
                                AllCount += Convert.ToDecimal(q.Volume);
                            }
                             
                            del.Price = UnitPrice;
                            del.AllMoney = TotalPrice + del.OtherMoney;
                            this.m_UnitOfWork.GetRepositoryBase<M_BaleBalanceDel>().Update(del, null);
                            AllMoney += del.AllMoney;
                        }

                    }
                    #endregion
                    #region 如果是单一价格计算  
                    if (bale.IsOnePrice)
                    {
                        foreach (var del in M_Del)
                        {
                            var q = this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Get(del.StuffInID);
                            decimal UnitPrice = 0;
                            decimal TotalPrice = 0;
                            UnitPrice = bale.OnePrice;
                            if (bale.BaleType == "按结算重量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.FinalFootNum) / 1000;
                            }
                            if (bale.BaleType == "按厂商数量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.SupplyNum) / 1000;
                            }
                            if (bale.BaleType == "按方量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.Volume);
                            }
                            if (bale.BaleType == "按结算重量")
                            {
                                AllCount += Convert.ToDecimal(q.FinalFootNum);
                            }
                            if (bale.BaleType == "按厂商数量")
                            {
                                AllCount += Convert.ToDecimal(q.SupplyNum);
                            }
                            if (bale.BaleType == "按方量")
                            {
                                AllCount += Convert.ToDecimal(q.Volume);
                            }
                            del.Price = UnitPrice;
                            del.AllMoney = TotalPrice + del.OtherMoney;
                            this.m_UnitOfWork.GetRepositoryBase<M_BaleBalanceDel>().Update(del, null);
                            AllMoney += del.AllMoney;
                        }
                    }
                    #endregion
                    bale.AllCount = AllCount ;
                    bale.AllMoney = AllMoney;
                    bale.AllOkCount = AllCount;
                    bale.AllOkMoney = AllMoney;
                    bale.PayMoney = 0;
                    bale.PayOwing = AllMoney;
                    bale.PayFavourable = 0;
                    bale.Modifier = AuthorizationService.CurrentUserName;
                    bale.ModifyTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_BaleBalance>().Update(bale, null);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功" };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }


        /// <summary>
        ///重新计算运费结算单，包括更新运费的价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo ComputeTran(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var bale = this.m_UnitOfWork.GetRepositoryBase<M_TranBalance>().Get(id);
                    var BaleBalanceID = Convert.ToInt32(id);
                    var M_Del = this.m_UnitOfWork.GetRepositoryBase<M_TranBalanceDel>().Query().Where(t => t.BaleBalanceID == BaleBalanceID).ToList();

                    //获取所有有关的运费设定信息
                    var transPrice = this.m_UnitOfWork.GetRepositoryBase<TransPrice>().Query().Where(t => t.TransportID == bale.TranID && t.StuffID == bale.StuffID).ToList();
                    decimal AllCount = 0;
                    decimal AllMoney = 0;
                    #region  如果是运费设定结算 
                    if (bale.IsStockPrice)
                    {
                        foreach (var del in M_Del)
                        {
                            var StuffIn = this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Get(del.StuffInID);
                            var tran = transPrice.FirstOrDefault(t => t.SupplyID == StuffIn.SupplyID);
                            decimal UnitPrice = 0;
                            decimal TotalPrice = 0;
                            if (tran != null)
                            {
                                var setPrice = tran.M_TransPrices.Where(t => t.PriceDate < StuffIn.InDate).OrderByDescending(t => t.PriceDate).FirstOrDefault();
                                if (setPrice != null)
                                {
                                    UnitPrice = setPrice.UnitPrice;
                                }
                                else
                                {
                                    UnitPrice = tran.UnitPrice;
                                }
                            }
                            if (bale.BaleType == "按重量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(StuffIn.FinalFootNum) / 1000;
                            }
                            if (bale.BaleType == "按趟数")
                            {
                                TotalPrice = UnitPrice;
                            }

                            if (bale.BaleType == "按重量")
                            {
                                AllCount += Convert.ToDecimal(StuffIn.FinalFootNum);
                            }

                            if (bale.BaleType == "按趟数")
                            {
                                AllCount++;
                            }
                            del.Price = UnitPrice;
                            del.AllMoney = TotalPrice + del.OtherMoney;
                            this.m_UnitOfWork.GetRepositoryBase<M_TranBalanceDel>().Update(del, null);
                            AllMoney += del.AllMoney;
                           
                        }
                    }
                    #endregion
                    #region 如果是单一价格计算 
                    if (bale.IsOnePrice)
                    {
                        foreach (var del in M_Del)
                        {
                            var q = this.m_UnitOfWork.GetRepositoryBase<StuffIn>().Get(del.StuffInID);
                            decimal UnitPrice =0;
                            decimal TotalPrice = 0;
                            UnitPrice = bale.OnePrice;
                            if (bale.BaleType == "按重量")
                            {
                                TotalPrice = UnitPrice * Convert.ToDecimal(q.FinalFootNum) / 1000;
                            } 
                            if (bale.BaleType == "按趟数")
                            {
                                TotalPrice = UnitPrice;
                            }
                          
                            if (bale.BaleType == "按重量")
                            {
                                AllCount += Convert.ToDecimal(q.FinalFootNum);
                            } 
                            if (bale.BaleType == "按趟数")
                            {
                                AllCount++;
                            }
                            del.Price = UnitPrice;
                            del.AllMoney = TotalPrice + del.OtherMoney;
                            this.m_UnitOfWork.GetRepositoryBase<M_TranBalanceDel>().Update(del, null);
                            AllMoney += del.AllMoney;
                        } 
                    }
                    #endregion
                    bale.AllCount = AllCount;
                    bale.AllMoney = AllMoney;
                    bale.AllOkCount = AllCount;
                    bale.AllOkMoney = AllMoney;
                    bale.PayMoney = 0;
                    bale.PayOwing = AllMoney;
                    bale.PayFavourable = 0;
                    bale.Modifier = AuthorizationService.CurrentUserName;
                    bale.ModifyTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranBalance>().Update(bale, null); 

                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功" };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }

        #region 货款应付款付款
        /// <summary>
        /// 应付款付款
        /// </summary>
        /// <param name="M_PerPay"></param>
        /// <param name="m_perpays"></param>
        /// <returns></returns>
        public ResultInfo PayBale(M_PerPay M_PerPay, List<m_perpay> m_perpays)
        {
            //1.插入到付款记录

            //2.插入到付款明细
            //3.更新供应商的应付款

            //4.更新资产
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var payall = m_perpays.Sum(t => t.PayMoney2);
                    var payper = m_perpays.Sum(t => t.PayFavourable2);
                    //1.插入应收应付的明细
                    M_YingSFrec YingSFrec = new M_YingSFrec();
                    YingSFrec.UnitID = M_PerPay.StockPactID;
                    YingSFrec.Source = M_Common.OtherInfo.Bale;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = M_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType = M_PerPay.PayType;
                    YingSFrec.Payer = M_PerPay.Payer;
                    YingSFrec.Gatheringer = M_PerPay.Gatheringer;
                    YingSFrec.Remark = M_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_YingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<M_YingSFrecdetail>();
                    var BaleBalanceService = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalance>();
                    //插入记录明细 
                    foreach (var p in m_perpays)
                    {
                        M_YingSFrecdetail YingSFrecetail = new M_YingSFrecdetail();
                        YingSFrecetail.FinanceNo = Convert.ToInt32(YingSFrec.ID);
                        YingSFrecetail.OrderNo = Convert.ToInt32(p.ID);
                        YingSFrecetail.PayMoney = p.PayMoney2;
                        YingSFrecetail.PayFavourable = p.PayFavourable2;
                        YingSFrecdetailService.Add(YingSFrecetail);

                        //更新结算单的付款情况
                        var piaoin = BaleBalanceService.Get(p.ID.ToString());
                        piaoin.PayMoney = piaoin.PayMoney + p.PayMoney2;
                        piaoin.PayFavourable = piaoin.PayFavourable + p.PayFavourable2;
                        piaoin.PayOwing = piaoin.PayOwing - p.PayMoney2 - p.PayFavourable2;
                        BaleBalanceService.Update(piaoin, null);
                    }
                    //2.更新供货厂商的付款
                    
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(M_PerPay.StockPactID);
                    if (payall > 0)
                    {
                        Supplier.PayMoney = Supplier.PayMoney - payall - payper;
                        if (M_PerPay.PayType == M_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - payall;
                        }
                        SupplierService.Update(Supplier, null);
                    }
                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<M_FinanceRecord>().Query().Where(t => t.FinanceType == M_PerPay.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    M_FinanceRecord FinanceRecord = new M_FinanceRecord();
                    FinanceRecord.FinanceType = M_PerPay.PayType;
                    FinanceRecord.IsInOrOut = M_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = payall;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - payall;
                    }
                    else
                    {
                        FinanceRecord.Balance = -payall;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = M_Common.OtherInfo.Bale; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "货款付款 " + Supplier.SupplyID;
                    this.m_UnitOfWork.GetRepositoryBase<M_FinanceRecord>().Add(FinanceRecord);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }

        /// <summary>
        /// 对运费付款
        /// </summary>
        /// <param name="M_PerPay"></param>
        /// <param name="m_perpays"></param>
        /// <returns></returns>
        public ResultInfo PayTran(M_PerPay M_PerPay, List<m_perpay> m_perpays)
        {
            //1.插入到付款记录

            //2.插入到付款明细
            //3.更新供应商的应付款

            //4.更新资产
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var payall = m_perpays.Sum(t => t.PayMoney2);
                    var payper = m_perpays.Sum(t => t.PayFavourable2);
                    //1.插入应收应付的明细
                    M_TranYingSFrec YingSFrec = new M_TranYingSFrec();
                    YingSFrec.UnitID = M_PerPay.StockPactID;
                    YingSFrec.Source = M_Common.OtherInfo.Tran;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = M_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType = M_PerPay.PayType;
                    YingSFrec.Payer = M_PerPay.Payer;
                    YingSFrec.Gatheringer = M_PerPay.Gatheringer;
                    YingSFrec.Remark = M_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranYingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<M_TranYingSFrecdetail>();
                    var BaleBalanceService = this.m_UnitOfWork.GetRepositoryBase<M_TranBalance>();
                    //插入记录明细 
                    foreach (var p in m_perpays)
                    {
                        M_TranYingSFrecdetail YingSFrecetail = new M_TranYingSFrecdetail();
                        YingSFrecetail.FinanceNo = Convert.ToInt32(YingSFrec.ID);
                        YingSFrecetail.OrderNo = Convert.ToInt32(p.ID);
                        YingSFrecetail.PayMoney = p.PayMoney2;
                        YingSFrecetail.PayFavourable = p.PayFavourable2;
                        YingSFrecdetailService.Add(YingSFrecetail);

                        //更新结算单的付款情况
                        var piaoin = BaleBalanceService.Get(p.ID.ToString());
                        piaoin.PayMoney = piaoin.PayMoney + p.PayMoney2;
                        piaoin.PayFavourable = piaoin.PayFavourable + p.PayFavourable2;
                        piaoin.PayOwing = piaoin.PayOwing - p.PayMoney2 - p.PayFavourable2;
                        BaleBalanceService.Update(piaoin, null);
                    }
                    //2.更新供货厂商的付款
                    //根据合同找到供货厂商
                     
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(M_PerPay.StockPactID);
                    if (payall > 0)
                    {
                        Supplier.PayMoney = Supplier.PayMoney - payall - payper;
                        if (M_PerPay.PayType == M_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - payall;
                        }
                        SupplierService.Update(Supplier, null);
                    }
                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<M_TranFinanceRecord>().Query().Where(t => t.FinanceType == M_PerPay.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    M_TranFinanceRecord FinanceRecord = new M_TranFinanceRecord();
                    FinanceRecord.FinanceType = M_PerPay.PayType;
                    FinanceRecord.IsInOrOut = M_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = payall;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - payall;
                    }
                    else
                    {
                        FinanceRecord.Balance = -payall;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = M_Common.OtherInfo.Tran; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "运费付款 " + Supplier.SupplyID;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranFinanceRecord>().Add(FinanceRecord);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }

        //发票问题
        public ResultInfo PiaoPay(M_PerPay M_PerPay, List<m_perpay> b_perpays)
        {
            //1.插入到付款记录

            //2.插入到付款明细
            //3.更新供应商的应付款

            //4.更新资产
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var payall = b_perpays.Sum(t => t.PayMoney2);
                    var payper = b_perpays.Sum(t => t.PayFavourable2);
                    //1.插入开票的明细
                    M_PiaoYingSFrec YingSFrec = new M_PiaoYingSFrec();
                    YingSFrec.UnitID = M_PerPay.StockPactID;
                    YingSFrec.Source = M_Common.OtherInfo.PiaoMaterial;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.PiaoNo = M_PerPay.PiaoNo;
                    YingSFrec.FinanceDate = M_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType =M_PerPay.PayType;
                    YingSFrec.Payer = M_PerPay.Payer;
                    YingSFrec.Gatheringer = M_PerPay.Gatheringer;
                    YingSFrec.Remark = M_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_PiaoYingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<M_PiaoYingSFrecdetail>();
                    var BaleBalanceService = this.m_UnitOfWork.GetRepositoryBase<M_BaleBalance>();
                    //插入记录明细 
                    foreach (var p in b_perpays)
                    {
                        M_PiaoYingSFrecdetail YingSFrecetail = new M_PiaoYingSFrecdetail();
                        YingSFrecetail.FinanceNo = Convert.ToInt32(YingSFrec.ID);
                        YingSFrecetail.OrderNo = Convert.ToInt32(p.ID);
                        YingSFrecetail.PayMoney = p.PayMoney2;
                        YingSFrecetail.PayFavourable = p.PayFavourable2;
                        YingSFrecdetailService.Add(YingSFrecetail);

                        //更新结算单的付款情况
                        var piaoin = BaleBalanceService.Get(p.ID.ToString());
                        piaoin.PiaoPayMoney = Convert.ToDecimal((piaoin.PiaoPayMoney.ToDecimal())) + p.PayMoney2;
                        piaoin.PiaoPayFavourable = piaoin.PayFavourable + p.PayFavourable2;
                        piaoin.PiaoPayOwing = piaoin.PiaoPayOwing - p.PayMoney2 - p.PayFavourable2;
                        BaleBalanceService.Update(piaoin, null);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = YingSFrec.ID };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }
        #endregion


        #region 对期初付款
        /// <summary>
        /// 对期初付款
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PayIni(M_YingSFrec M_YingSFrec)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    M_YingSFrec YingSFrec = new M_YingSFrec();
                    YingSFrec.UnitID = M_YingSFrec.UnitID;
                    YingSFrec.Source = M_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = M_YingSFrec.FinanceDate;
                    YingSFrec.FinanceMoney = M_YingSFrec.FinanceMoney;
                    YingSFrec.PayFavourable = M_YingSFrec.PayFavourable;
                    YingSFrec.PayType = M_YingSFrec.PayType;
                    YingSFrec.Payer = M_YingSFrec.Payer;
                    YingSFrec.Gatheringer = M_YingSFrec.Gatheringer;
                    YingSFrec.Remark = M_YingSFrec.Remark;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_YingSFrec>().Add(YingSFrec);


                    //2.更新供应商的付款
                    //通过合同找到供货厂商 
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(M_YingSFrec.UnitID);
                    if (M_YingSFrec.FinanceMoney > 0)
                    {
                        Supplier.PaidOut = Supplier.PaidOut + M_YingSFrec.FinanceMoney;
                        Supplier.PaidFavourable = Supplier.PaidFavourable + M_YingSFrec.PayFavourable;
                        Supplier.PaidOwing = Supplier.PaidOwing - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        Supplier.PayMoney = Supplier.PayMoney - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        if (M_YingSFrec.PayType == M_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - M_YingSFrec.FinanceMoney;
                        }
                        SupplierService.Update(Supplier, null);
                    }

                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<M_FinanceRecord>().Query().Where(t => t.FinanceType == M_YingSFrec.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    M_FinanceRecord FinanceRecord = new M_FinanceRecord();
                    FinanceRecord.FinanceType = M_YingSFrec.PayType;
                    FinanceRecord.IsInOrOut = M_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = M_YingSFrec.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - M_YingSFrec.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = -M_YingSFrec.FinanceMoney;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = M_Common.OtherInfo.PerPayIni; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "期初付款 " + Supplier.SupplyName;
                    this.m_UnitOfWork.GetRepositoryBase<M_FinanceRecord>().Add(FinanceRecord);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }

        /// <summary>
        /// 对运费期初付款
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PayTranIni(M_TranYingSFrec M_YingSFrec)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    M_TranYingSFrec YingSFrec = new M_TranYingSFrec();
                    YingSFrec.UnitID = M_YingSFrec.UnitID;
                    YingSFrec.Source = M_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = M_YingSFrec.FinanceDate;
                    YingSFrec.FinanceMoney = M_YingSFrec.FinanceMoney;
                    YingSFrec.PayFavourable = M_YingSFrec.PayFavourable;
                    YingSFrec.PayType = M_YingSFrec.PayType;
                    YingSFrec.Payer = M_YingSFrec.Payer;
                    YingSFrec.Gatheringer = M_YingSFrec.Gatheringer;
                    YingSFrec.Remark = M_YingSFrec.Remark;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranYingSFrec>().Add(YingSFrec);


                    //2.更新供应商的付款 
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(M_YingSFrec.UnitID);
                    if (M_YingSFrec.FinanceMoney > 0)
                    {
                        Supplier.PaidOut = Supplier.PaidOut + M_YingSFrec.FinanceMoney;
                        Supplier.PaidFavourable = Supplier.PaidFavourable + M_YingSFrec.PayFavourable;
                        Supplier.PaidOwing = Supplier.PaidOwing - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        Supplier.PayMoney = Supplier.PayMoney - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        if (M_YingSFrec.PayType == M_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - M_YingSFrec.FinanceMoney;
                        }
                        SupplierService.Update(Supplier, null);
                    }

                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<M_TranFinanceRecord>().Query().Where(t => t.FinanceType == M_YingSFrec.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    M_TranFinanceRecord FinanceRecord = new M_TranFinanceRecord();
                    FinanceRecord.FinanceType = M_YingSFrec.PayType;
                    FinanceRecord.IsInOrOut = M_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = M_YingSFrec.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - M_YingSFrec.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = -M_YingSFrec.FinanceMoney;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = M_Common.OtherInfo.PerPayIni; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "期初付款 " + Supplier.SupplyName;
                    this.m_UnitOfWork.GetRepositoryBase<M_TranFinanceRecord>().Add(FinanceRecord);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }


        /// <summary>
        /// 对发票期初付款
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PiaoPayIni(M_PiaoYingSFrec M_YingSFrec)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    M_PiaoYingSFrec YingSFrec = new M_PiaoYingSFrec();
                    YingSFrec.UnitID = M_YingSFrec.UnitID;
                    YingSFrec.Source = M_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = M_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = M_YingSFrec.FinanceDate;
                    YingSFrec.FinanceMoney = M_YingSFrec.FinanceMoney;
                    YingSFrec.PayFavourable = M_YingSFrec.PayFavourable;
                    YingSFrec.PayType = M_YingSFrec.PayType;
                    YingSFrec.PiaoNo = M_YingSFrec.PiaoNo;
                    YingSFrec.Payer = M_YingSFrec.Payer;
                    YingSFrec.Gatheringer = M_YingSFrec.Gatheringer;
                    YingSFrec.Remark = M_YingSFrec.Remark;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<M_PiaoYingSFrec>().Add(YingSFrec);


                    //2.更新供应商的付款
                    //通过合同找到供货厂商 
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SupplyInfo>();
                    var Supplier = SupplierService.Get(M_YingSFrec.UnitID);
                    if (M_YingSFrec.FinanceMoney > 0)
                    {
                        Supplier.PiaoPaidOut = Supplier.PiaoPaidOut + M_YingSFrec.FinanceMoney;
                        Supplier.PiaoPaidFavourable = Supplier.PiaoPaidFavourable + M_YingSFrec.PayFavourable;
                        Supplier.PiaoPaidOwing = Supplier.PiaoPaidOwing - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        Supplier.PiaoPayMoney = Supplier.PiaoPayMoney - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                    
                        SupplierService.Update(Supplier, null);
                    } 
                    
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = YingSFrec.ID } };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }
        #endregion 

        #region 调价
        /// <summary>
        /// 批量调价功能
        /// </summary>
        /// <param name="BitUpdatePrices"></param>
        /// <returns></returns>
        public ResultInfo BitUpdatePrice(List<BitStuffUpdatePrice> BitUpdatePrices)
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
                        var StockPact = this.m_UnitOfWork.GetRepositoryBase<StockPact>().Query().Where(t => t.ID == Price.BitStockPactID).FirstOrDefault();
                        List<BitStuff> BitStuffs = new List<BitStuff>();
                        if (!string.IsNullOrEmpty(StockPact.StuffID) && Price.BitStuffID == StockPact.StuffID)
                        {
                            BitStuff BitStuff = new BitStuff();
                            BitStuff.BitStuffID = StockPact.StuffID;
                            BitStuff.Price = StockPact.StockPrice;
                            BitStuffs.Add(BitStuff);
                        }
                        if (!string.IsNullOrEmpty(StockPact.StuffID1) && Price.BitStuffID == StockPact.StuffID1)
                        {
                            BitStuff BitStuff = new BitStuff();
                            BitStuff.BitStuffID = StockPact.StuffID1;
                            BitStuff.Price = StockPact.StockPrice1;
                            BitStuffs.Add(BitStuff);
                        }
                        if (!string.IsNullOrEmpty(StockPact.StuffID2) && Price.BitStuffID == StockPact.StuffID2)
                        {
                            BitStuff BitStuff = new BitStuff();
                            BitStuff.BitStuffID = StockPact.StuffID2;
                            BitStuff.Price = StockPact.StockPrice2;
                            BitStuffs.Add(BitStuff);
                        }
                        if (!string.IsNullOrEmpty(StockPact.StuffID3) && Price.BitStuffID == StockPact.StuffID3)
                        {
                            BitStuff BitStuff = new BitStuff();
                            BitStuff.BitStuffID = StockPact.StuffID3;
                            BitStuff.Price = StockPact.StockPrice3;
                            BitStuffs.Add(BitStuff);
                        }
                        if (!string.IsNullOrEmpty(StockPact.StuffID4) && Price.BitStuffID == StockPact.StuffID4)
                        {
                            BitStuff BitStuff = new BitStuff();
                            BitStuff.BitStuffID = StockPact.StuffID4;
                            BitStuff.Price = StockPact.StockPrice4;
                            BitStuffs.Add(BitStuff);
                        }
                        foreach (var stuff in BitStuffs)
                        {
                            StockPactPriceSet setting = new StockPactPriceSet();
                            setting.StockPactID = Price.BitStockPactID;
                            setting.StuffID = stuff.BitStuffID;
                            setting.ChangeTime = Price.BitUpdateDate;
                            //按照百分比调整
                            if (Price.BitUpdateType == 0)
                            {
                                setting.Price = stuff.Price + stuff.Price * Price.BitUpdateCnt / 100;
                                setting.Remark = "基准价百分比浮动百分之" + Price.BitUpdateCnt.ToString();
                            }
                            //按照数额调整
                            if (Price.BitUpdateType == 1)
                            {
                                setting.Price = stuff.Price + Price.BitUpdateCnt;
                                setting.Remark = "基准价数额浮动" + Price.BitUpdateCnt.ToString();
                            }
                            setting.Builder = AuthorizationService.CurrentUserName;
                            setting.BuildTime = DateTime.Now;
                            this.m_UnitOfWork.GetRepositoryBase<StockPactPriceSet>().Add(setting);
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
        #endregion


    }
}
