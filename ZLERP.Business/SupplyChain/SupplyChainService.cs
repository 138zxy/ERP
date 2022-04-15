using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.SupplyChain;
using ZLERP.IRepository;
using ZLERP.Model;
using System.Collections;

namespace ZLERP.Business.SupplyChain
{
    /*
        此模块是供应链管理功能服务类模块，做事务处理 by  mandzy 2018-11-03
     */
    public class SupplyChainService : ServiceBase<SC_Base>
    {
        internal SupplyChainService(IUnitOfWork uow) : base(uow) { }

        #region 采购入库
        /// <summary>
        /// 采购入库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo PurIn(SC_PiaoIn piaoin, List<SC_ZhangIn> zhangIns)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    piaoin = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Add(piaoin);
                    if (Convert.ToInt32(piaoin.ID) > 0)
                    {
                        foreach (var sc in zhangIns)
                        {
                            sc.InNo = Convert.ToInt32(piaoin.ID);
                            this.m_UnitOfWork.GetRepositoryBase<SC_ZhangIn>().Add(sc);
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = piaoin.ID } };
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

        #region 库存初始化
        /// <summary>
        /// 库存初始化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo InLibIni(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var piaoin = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Get(id);
                    /*----------库存初始化 ----------*/
                    //1.插入到库存

                    //如果存在，则更新

                    //2.更新商品表的总金额

                    //3.更新库存的单价
                    foreach (var l in piaoin.SC_ZhangIns)
                    {
                        l.Indate = DateTime.Now;
                        this.m_UnitOfWork.GetRepositoryBase<SC_ZhangIn>().Update(l, null);
                    }
                    InNowLib(piaoin);

                    //更新入库单的状态
                    piaoin.Condition = SC_Common.InStatus.InLib;
                    piaoin.Auditor = AuthorizationService.CurrentUserName;
                    piaoin.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Update(piaoin, null);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = piaoin.ID } };
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

        #region 采购审核审核入库
        /// <summary>
        /// 采购审核审核入库(需要考虑到采购退货的情况)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo PurInLib(SC_PiaoIn PayPiaoIn)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    string id = PayPiaoIn.PayInID.ToString();
                    var piaoin = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Get(id);
                    /*----------初始化 ----------*/
                    piaoin.PayType = PayPiaoIn.PayType;
                    piaoin.PayMoney = PayPiaoIn.PayMoney;
                    piaoin.PayOwing = PayPiaoIn.PayOwing;
                    piaoin.PayFavourable = PayPiaoIn.PayFavourable;
                    piaoin.Payer = PayPiaoIn.Payer;
                    piaoin.Gatheringer = PayPiaoIn.Gatheringer;
                    piaoin.Remark2 = PayPiaoIn.Remark2;
                    piaoin.Condition = SC_Common.InStatus.InLib;
                    piaoin.Auditor = AuthorizationService.CurrentUserName;
                    piaoin.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Update(piaoin, null);

                    foreach (var l in piaoin.SC_ZhangIns)
                    {
                        l.Indate = DateTime.Now;
                        this.m_UnitOfWork.GetRepositoryBase<SC_ZhangIn>().Update(l, null);
                    }
                    //1.插入到库存

                    //2.更新商品表的总金额

                    //3.更新库存的单价

                    /*----------采购审核入库
                     1.更新入库单的款项
                     2.更新明细的入库时间
                     3.更新入库状态
                     4.判断库存中有无此商品，没有就insert
                     5.更新商品总金额
                     6.更新库存的进价
                     7.插入付款记录以及明细
                     8.插入经营记录
                     9.更新资产总记录
                     10.更新供应商的应付额
                     * ----------*/
                    InNowLib(piaoin);
                    if (piaoin.PayMoney != 0)
                    {
                        //插入到收款记录 
                        SC_YingSFrec YingSFrec = new SC_YingSFrec();
                        YingSFrec.UnitID = piaoin.SupplierID;
                        YingSFrec.Source = piaoin.InType;
                        YingSFrec.YingSF = SC_Common.OtherInfo.YingSFOut;

                        Random ran = new Random();
                        string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                        YingSFrec.FinanceNo = orderno;
                        YingSFrec.FinanceDate = DateTime.Now;
                        YingSFrec.FinanceMoney = piaoin.PayMoney;
                        YingSFrec.PayFavourable = piaoin.PayFavourable;
                        YingSFrec.PayType = piaoin.PayType;
                        YingSFrec.Payer = piaoin.Payer;
                        YingSFrec.Gatheringer = piaoin.Gatheringer;
                        YingSFrec.Remark = piaoin.Remark2;
                        YingSFrec.Builder = AuthorizationService.CurrentUserName;
                        YingSFrec.BuildTime = DateTime.Now;
                        this.m_UnitOfWork.GetRepositoryBase<SC_YingSFrec>().Add(YingSFrec);

                        //插入到收款记录明细 
                        SC_YingSFrecdetail YingSFrecetail = new SC_YingSFrecdetail();
                        YingSFrecetail.FinanceNo = Convert.ToInt32(YingSFrec.ID);
                        YingSFrecetail.OrderNo = Convert.ToInt32(piaoin.ID);
                        YingSFrecetail.PayMoney = piaoin.PayMoney;
                        YingSFrecetail.PayFavourable = piaoin.PayFavourable;
                        this.m_UnitOfWork.GetRepositoryBase<SC_YingSFrecdetail>().Add(YingSFrecetail);

                        //插入到资产消费记录表中
                        var fin = this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Query().Where(t => t.FinanceType == piaoin.PayType).OrderByDescending(t => t.ID).FirstOrDefault();

                        SC_FinanceRecord FinanceRecord = new SC_FinanceRecord();
                        FinanceRecord.FinanceType = piaoin.PayType;
                        FinanceRecord.IsInOrOut = SC_Common.OtherInfo.Out;
                        FinanceRecord.FinanceDate = DateTime.Now;
                        FinanceRecord.FinanceMoney = -piaoin.PayMoney;
                        if (fin != null)
                        {
                            FinanceRecord.Balance = fin.Balance - piaoin.PayMoney;
                        }
                        else
                        {
                            FinanceRecord.Balance = -piaoin.PayMoney;
                        }
                        FinanceRecord.Operater = piaoin.Purchase;
                        FinanceRecord.UseType = SC_Common.OtherInfo.Pur; ;
                        FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                        FinanceRecord.Remark = "采购付款 " + piaoin.SC_Supply.SupplierName;
                        this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Add(FinanceRecord);
                    }
                    //更新供应商的应付款
                    if (piaoin.PayOwing != 0 || (piaoin.PayMoney != 0 && piaoin.PayType == SC_Common.PayType.SupplyIn))
                    {
                        var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SC_Supply>();
                        var Supplier = SupplierService.Get(piaoin.SupplierID.ToString());
                        if (piaoin.PayOwing > 0)
                        {
                            Supplier.PayMoney = Supplier.PayMoney + piaoin.PayOwing;
                        }
                        if (piaoin.PayMoney > 0 && piaoin.PayType == SC_Common.PayType.SupplyIn)
                        {
                            Supplier.PrePay = Supplier.PrePay - piaoin.PayMoney;
                        }
                        Supplier.NearPurDate = piaoin.InDate;
                        SupplierService.Update(Supplier, null);
                    }
                    //更新采购单的状态
                    if (piaoin.InType == SC_Common.InType.PurIn)
                    {
                        var purid = Convert.ToInt32(piaoin.PurchaseID);
                        var purOrder = this.m_UnitOfWork.GetRepositoryBase<SC_ZhangInOrder>().Query().Where(t => t.OrderNo == purid).ToList();
                        //1.查出为入库的采购的数量
                        var purOrder1 = purOrder.Where(t => t.Quantity - t.InQuantity - t.WaitQuantity > 0).ToList();
                        if (purOrder1 == null || purOrder1.Count <= 0)
                        {
                            var purorderService = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoInOrder>();
                            var purorder = purorderService.Get(purid.ToString());
                            if (purorder != null)
                            {
                                purorder.Condition = SC_Common.Pstatus.Completed;
                                purorderService.Update(purorder, null);
                            }
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = piaoin.ID } };
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

        #region 库存调拨
        /// <summary>
        /// 库存调拨
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo ChangeLibIn(string id)
        {

            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var libService = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>();
                    var piaoTo = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoTo>().Get(id);
                    foreach (var to in piaoTo.SC_ZhangTos)
                    {
                        //从调出仓库--》调入仓库
                        var outlib = libService.Query().Where(t => t.LibID == piaoTo.OutLibID && t.GoodsID == to.GoodsID).FirstOrDefault();
                        if (outlib != null)
                        {
                            outlib.Quantity = outlib.Quantity - to.Quantity * to.UnitRate;
                            outlib.OutDate = DateTime.Now;
                            libService.Update(outlib, null);
                        }
                        var inlib = libService.Query().Where(t => t.LibID == piaoTo.InLibID && t.GoodsID == to.GoodsID).FirstOrDefault();
                        if (inlib != null)
                        {
                            inlib.Quantity = inlib.Quantity + to.Quantity * to.UnitRate;
                            inlib.InDate = DateTime.Now;
                            libService.Update(inlib, null);
                        }
                        else
                        {
                            SC_NowLib nowlib = new SC_NowLib();
                            nowlib.LibID = piaoTo.InLibID;
                            nowlib.GoodsID = to.GoodsID;
                            nowlib.PirceUnit = outlib.PirceUnit;
                            nowlib.Quantity = to.Quantity * to.UnitRate;
                            nowlib.InDate = DateTime.Now;
                            libService.Add(nowlib);
                        }
                    }
                    //更新调拨单的状态
                    piaoTo.Condition = SC_Common.InStatus.ToLib;
                    piaoTo.Auditor = AuthorizationService.CurrentUserName;
                    piaoTo.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PiaoTo>().Update(piaoTo, null);
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = piaoTo.ID } };
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

        #region 入库标准化操作
        /// <summary>
        /// 入库标准化操作
        /// </summary>
        /// <param name="piaoin"></param>
        private void InNowLib(SC_PiaoIn piaoin)
        {
            //3.更新库存的单价
            var zhangService = this.m_UnitOfWork.GetRepositoryBase<SC_ZhangIn>();
            var newLibService = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>();
            var GoodsService = this.m_UnitOfWork.GetRepositoryBase<SC_Goods>();
            foreach (var p in piaoin.SC_ZhangIns)
            {
                //1.判断是否有库存
                var inItem = newLibService.Query().Where(t => t.LibID == piaoin.LibID && t.GoodsID == p.GoodsID).FirstOrDefault();
                //2.如果没有库存就insert一条记录
                //插入数据之前，计算当前总库存，和总金额
                var libgoods = newLibService.Query().Where(t => t.GoodsID == p.GoodsID).ToList();
                var Quantity = libgoods.Sum(t => t.Quantity);
                var libMoney = libgoods.Sum(t => t.Quantity * t.PirceUnit);
                //计算增加的库存以及金额
                Quantity = Quantity + p.Quantity * (decimal)p.UnitRate;
                libMoney = libMoney + p.InMoney;
                decimal price = 0;
                if (Quantity > 0)
                {
                    price = libMoney / Quantity;
                }
                if (inItem == null)
                {
                    SC_NowLib NowLib = new SC_NowLib();
                    NowLib.GoodsID = p.GoodsID;
                    NowLib.LibID = piaoin.LibID;
                    NowLib.Quantity = p.Quantity * (decimal)p.UnitRate;
                    NowLib.PirceUnit = price; //移动加权的单价，针对的是所有的仓库，所有的供应商一起加权
                    NowLib.InDate = DateTime.Now;
                    newLibService.Add(NowLib);
                }
                else //否则的话，更新
                {
                    inItem.Quantity = inItem.Quantity + p.Quantity * p.UnitRate;
                    inItem.PirceUnit = price;
                    inItem.InDate = DateTime.Now;
                    newLibService.Update(inItem, null);
                }
                //更新商品总价
                var good = GoodsService.Get(p.GoodsID.ToString());
                good.LibMoney = good.LibMoney + p.Quantity * p.PriceUnit;
                GoodsService.Update(good, null);
                //更新当前商品的所有库存以及单价
                foreach (var l in libgoods)
                {
                    l.PirceUnit = price;
                    newLibService.Update(l, null);
                }
            }
        }
        #endregion

        #region 盘点
        /// <summary>
        /// 新增盘点单
        /// </summary>
        /// <param name="PanDian"></param>
        /// <returns></returns>
        public ResultInfo InsertPanDian(SC_PanDian PanDian)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //插入主体表
                    PanDian.BeginDate = DateTime.Now;
                    PanDian.DifferenceNum = 0;
                    PanDian.DifferenceMoney = 0;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PanDian>().Add(PanDian);
                    //插入盘点明细表
                    var libGoods = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>().Query().Where(t => t.LibID == PanDian.LibID && t.SC_Goods.IsOff == false).ToList();
                    var SC_PanDianDetailService = this.m_UnitOfWork.GetRepositoryBase<SC_PanDianDetail>();
                    foreach (var g in libGoods)
                    {
                        SC_PanDianDetail PanDianDetail = new SC_PanDianDetail();
                        PanDianDetail.PanID = Convert.ToInt32(PanDian.ID);
                        PanDianDetail.GoodsID = g.GoodsID;
                        PanDianDetail.LibNum = g.Quantity;
                        PanDianDetail.PanNum = g.Quantity;
                        PanDianDetail.DifferenceNum = 0;
                        PanDianDetail.Builder = AuthorizationService.CurrentUserName;
                        PanDianDetail.BuildTime = DateTime.Now;
                        SC_PanDianDetailService.Add(PanDianDetail);
                    }

                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = PanDian.ID };
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
        /// 盘点修正入库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo PanDianIn(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    //修改盘点表
                    var PanDian = this.m_UnitOfWork.GetRepositoryBase<SC_PanDian>().Get(id);
                    var libGoods = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>().Query().Where(t => t.LibID == PanDian.LibID && t.SC_Goods.IsOff == false).ToList();

                    PanDian.EndDate = DateTime.Now;
                    int panid = Convert.ToInt32(id);
                    var listDetail = this.m_UnitOfWork.GetRepositoryBase<SC_PanDianDetail>().Query().Where(t => t.PanID == panid);
                    var diffNum = 0.00m;
                    decimal diffMoney = 0;
                    foreach (var l in listDetail)
                    {
                        diffNum += l.DifferenceNum;
                        var price = libGoods.Where(t => t.GoodsID == l.GoodsID).FirstOrDefault();
                        if (price != null)
                        {
                            diffMoney += l.DifferenceNum * price.PirceUnit;
                            l.Price = price.PirceUnit;
                        }
                    }

                    PanDian.DifferenceNum = diffNum;
                    PanDian.DifferenceMoney = diffMoney;
                    PanDian.IsOff = true;
                    PanDian.Auditor = AuthorizationService.CurrentUserName;
                    PanDian.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PanDian>().Update(PanDian, null);
                    //更新库存
                    //1.新增报溢单 
                    var baoyi = listDetail.Where(t => t.DifferenceNum > 0).ToList();
                    if (baoyi != null && baoyi.Count > 0)
                    {
                        SC_PiaoIn PiaoIn1 = new SC_PiaoIn();
                        PiaoIn1.PurchaseID = 0;
                        PiaoIn1.SupplierID = 0;
                        PiaoIn1.LibID = PanDian.LibID;
                        PiaoIn1.InType = SC_Common.InType.Baoyi;
                        PiaoIn1.InDate = DateTime.Now;
                        Random ran = new Random();
                        string InNo = string.Format("{0}{1}{2}", "P", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                        PiaoIn1.InNo = InNo;
                        PiaoIn1.Purchase = "";
                        PiaoIn1.VarietyNum = 0; //要算
                        PiaoIn1.InMoney = 0; //要算
                        PiaoIn1.PayType = "";
                        PiaoIn1.Condition = SC_Common.InStatus.InLib;
                        PiaoIn1.Remark = "库存盘点";

                        int j = 0;
                        //3.生产入库单明细
                        var ZhangIns1 = (from q in baoyi
                                         select new SC_ZhangIn
                                         {
                                             GoodsID = Convert.ToInt32(q.GoodsID),
                                             Sequence = j++,
                                             Indate = DateTime.Now,
                                             Quantity = q.DifferenceNum,
                                             PriceUnit = q.Price,
                                             InMoney = q.DifferenceNum * q.Price,
                                             PiNo = "",
                                             UnitRate = 1
                                         }).ToList();

                        PiaoIn1.VarietyNum = ZhangIns1.Count();
                        PiaoIn1.InMoney = ZhangIns1.Sum(t => t.InMoney);
                        PiaoIn1 = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Add(PiaoIn1);
                        if (Convert.ToInt32(PiaoIn1.ID) > 0)
                        {
                            foreach (var sc in ZhangIns1)
                            {
                                sc.InNo = Convert.ToInt32(PiaoIn1.ID);
                                this.m_UnitOfWork.GetRepositoryBase<SC_ZhangIn>().Add(sc);
                            }
                        }
                        PiaoIn1.SC_ZhangIns = ZhangIns1;
                        InNowLib(PiaoIn1);

                    }
                    //1.新增报损单 
                    var baosun = listDetail.Where(t => t.DifferenceNum < 0).ToList();
                    if (baosun != null && baosun.Count > 0)
                    {
                        SC_PiaoIn PiaoIn2 = new SC_PiaoIn();
                        PiaoIn2.PurchaseID = 0;
                        PiaoIn2.SupplierID = 0;
                        PiaoIn2.LibID = PanDian.LibID;
                        PiaoIn2.InType = SC_Common.InType.BaoSun;
                        PiaoIn2.InDate = DateTime.Now;
                        Random ran = new Random();
                        string InNo = string.Format("{0}{1}{2}", "P", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                        PiaoIn2.InNo = InNo;
                        PiaoIn2.Purchase = "";
                        PiaoIn2.VarietyNum = 0; //要算
                        PiaoIn2.InMoney = 0; //要算
                        PiaoIn2.PayType = "";
                        PiaoIn2.Condition = SC_Common.InStatus.InLib;
                        PiaoIn2.Remark = "库存盘点";

                        int j = 0;
                        //3.生产入库单明细
                        var ZhangIns2 = (from q in baosun
                                         select new SC_ZhangIn
                                         {
                                             GoodsID = Convert.ToInt32(q.GoodsID),
                                             Sequence = j++,
                                             Indate = DateTime.Now,
                                             Quantity = q.DifferenceNum,
                                             PriceUnit = q.Price,
                                             InMoney = q.DifferenceNum * q.Price,
                                             PiNo = "",
                                             UnitRate = 1
                                         }).ToList();

                        PiaoIn2.VarietyNum = ZhangIns2.Count();
                        PiaoIn2.InMoney = ZhangIns2.Sum(t => t.InMoney);
                        PiaoIn2 = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>().Add(PiaoIn2);
                        if (Convert.ToInt32(PiaoIn2.ID) > 0)
                        {
                            foreach (var sc in ZhangIns2)
                            {
                                sc.InNo = Convert.ToInt32(PiaoIn2.ID);
                                this.m_UnitOfWork.GetRepositoryBase<SC_ZhangIn>().Add(sc);
                            }
                        }
                        PiaoIn2.SC_ZhangIns = ZhangIns2;
                        InNowLib(PiaoIn2);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = PanDian.ID } };
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

        #region 价格调整
        /// <summary>
        /// 价格调整
        /// </summary>
        /// <param name="Change"></param>
        /// <returns></returns>
        public ResultInfo PriceChange(SC_PriceChange Change)
        {

            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var good = this.m_UnitOfWork.GetRepositoryBase<SC_Goods>().Get(Change.GoodsID.ToString());
                    Change.CurNum = good.AllNum;
                    Change.CurPrice = good.LibPrice;
                    Change.CurMoney = (Change.AlferPrice - Change.CurPrice) * Change.CurNum;
                    Change.Builder = AuthorizationService.CurrentUserName;
                    Change.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PriceChange>().Add(Change);

                    //更新商品的库存总金额 
                    good.LibMoney = good.LibMoney + Change.CurMoney;
                    this.m_UnitOfWork.GetRepositoryBase<SC_Goods>().Update(good, null);

                    //更新库存价格
                    var nowlibService = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>();
                    var nowlib = nowlibService.Query().Where(t => t.GoodsID == Change.GoodsID);
                    foreach (var l in nowlib)
                    {
                        l.PirceUnit = Change.AlferPrice;
                        nowlibService.Update(l, null);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = Change.ID } };
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

        #region 出库
        /// <summary>
        /// 生产领料出库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo PurOut(SC_PiaoOut piaoOut, List<SC_ZhangOut> zhangOuts)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    piaoOut = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoOut>().Add(piaoOut);
                    if (Convert.ToInt32(piaoOut.ID) > 0)
                    {
                        foreach (var sc in zhangOuts)
                        {
                            sc.OutNo = Convert.ToInt32(piaoOut.ID);
                            this.m_UnitOfWork.GetRepositoryBase<SC_ZhangOut>().Add(sc);
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = piaoOut.ID } };
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
        /// 出库的操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo PurOutLib(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var piaoOut = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoOut>().Get(id);
                    //更新出库单的信息
                    var goodsid = piaoOut.SC_ZhangOuts.Select(t => t.GoodsID).ToList();
                    var zhangoutService = this.m_UnitOfWork.GetRepositoryBase<SC_ZhangOut>();
                    var nowlibService = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>();
                    foreach (var z in piaoOut.SC_ZhangOuts)
                    {
                        var nowlib = nowlibService.Query().Where(t => t.LibID == piaoOut.LibID && t.GoodsID == z.GoodsID).FirstOrDefault();
                        if (nowlib != null)
                        {
                            //更新出库单的单价 
                            if (z.PriceUnit != nowlib.PirceUnit)
                            {
                                z.PriceUnit = nowlib.PirceUnit;
                                z.OutMoney = z.PriceUnit * z.Quantity;
                            }
                            z.OutDate = DateTime.Now;
                            zhangoutService.Update(z, null);
                            nowlib.Quantity = nowlib.Quantity - z.Quantity * (decimal)z.UnitRate;
                            nowlib.OutDate = DateTime.Now;
                            nowlibService.Update(nowlib, null);
                        }
                    }
                    //更新主体信息
                    piaoOut.OutMoney = piaoOut.SC_ZhangOuts.Sum(t => t.OutMoney);

                    piaoOut.PayMoney = piaoOut.OutMoney;
                    piaoOut.PayOwing = 0;
                    piaoOut.PayFavourable = 0;

                    piaoOut.Condition = SC_Common.InStatus.OutLib;
                    piaoOut.Auditor = AuthorizationService.CurrentUserName;
                    piaoOut.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PiaoOut>().Update(piaoOut, null);

                    //更新商品总库存额
                    var goods = from q in piaoOut.SC_ZhangOuts
                                group q by q.GoodsID into g
                                select new
                                {
                                    GoodsID = g.Key,
                                    OutMoney = g.Sum(t => t.OutMoney)
                                };
                    var gService = this.m_UnitOfWork.GetRepositoryBase<SC_Goods>();
                    foreach (var g in goods.ToList())
                    {
                        var good = gService.Get(g.GoodsID.ToString());
                        good.LibMoney = good.LibMoney - g.OutMoney;
                        gService.Update(good, null);
                    }

                    //插入到资产消费记录表中
                    var fin = this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Query().Where(t => t.FinanceType == piaoOut.PayType).OrderByDescending(t => t.ID).FirstOrDefault();

                    SC_FinanceRecord FinanceRecord = new SC_FinanceRecord();
                    FinanceRecord.FinanceType = piaoOut.PayType;
                    FinanceRecord.IsInOrOut = SC_Common.OtherInfo.In;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = piaoOut.PayMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance + piaoOut.PayMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = piaoOut.PayMoney;
                    }
                    FinanceRecord.Operater = piaoOut.UserID;

                    FinanceRecord.UseType = SC_Common.OtherInfo.YingSFIn; ;
                    // FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.FinanceNo = 0;
                    FinanceRecord.Remark = "申领 " + piaoOut.DepartmentName;

                    this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Add(FinanceRecord);

                    if (piaoOut.OutType == SC_Common.OutType.QurOut)
                    {
                        //更新申请单
                        var outOrderID = Convert.ToInt32(piaoOut.OutOrderID);
                        var purOrder = this.m_UnitOfWork.GetRepositoryBase<SC_ZhangOutOrder>().Query().Where(t => t.OrderNo == outOrderID).ToList();
                        //1.查出为出库的申请的数量
                        var purOrder1 = purOrder.Where(t => t.Quantity - t.InQuantity - t.WaitQuantity > 0).ToList();
                        if (purOrder1 == null || purOrder1.Count <= 0)
                        {
                            var OutOrder = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoOutOrder>().Get(outOrderID.ToString());
                            if (OutOrder != null)
                            {
                                OutOrder.Condition = SC_Common.Pstatus.Completed;
                                this.m_UnitOfWork.GetRepositoryBase<SC_PiaoOutOrder>().Update(OutOrder, null);
                            }
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = new { ID = piaoOut.ID } };
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

        #region 预付款支付
        /// <summary>
        /// 预付款支付
        /// </summary>
        /// <param name="SC_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PerPay(SC_YingSFrec yingsf)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    SC_YingSFrec YingSFrec = new SC_YingSFrec();
                    YingSFrec.UnitID = yingsf.UnitID;
                    YingSFrec.Source = SC_Common.OtherInfo.PerPay;
                    YingSFrec.YingSF = SC_Common.OtherInfo.Out;
                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
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
                    this.m_UnitOfWork.GetRepositoryBase<SC_YingSFrec>().Add(YingSFrec);


                    //2.更新供应商的预付款
                    var fin = this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Query().Where(t => t.FinanceType == yingsf.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SC_Supply>();
                    var Supplier = SupplierService.Get(yingsf.UnitID.ToString());


                    SC_FinanceRecord FinanceRecord = new SC_FinanceRecord();
                    FinanceRecord.FinanceType = yingsf.PayType;
                    FinanceRecord.IsInOrOut = SC_Common.OtherInfo.Out;
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
                    FinanceRecord.UseType = SC_Common.OtherInfo.PerPay; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "预付 " + Supplier.SupplierName;
                    this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Add(FinanceRecord);

                    //3.插入资产记录的总金额 
                    if (yingsf.FinanceMoney > 0)
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
        #endregion

        #region 应付款付款
        /// <summary>
        /// 应付款付款
        /// </summary>
        /// <param name="V_PerPay"></param>
        /// <param name="perpays"></param>
        /// <returns></returns>
        public ResultInfo PayPur(V_PerPay V_PerPay, List<perpay> perpays)
        {
            //1.插入到付款记录

            //2.插入到付款明细
            //3.更新供应商的应付款

            //4.更新资产
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var payall = perpays.Sum(t => t.PayMoney2);
                    var payper = perpays.Sum(t => t.PayFavourable2);
                    //1.插入应收应付的明细
                    SC_YingSFrec YingSFrec = new SC_YingSFrec();
                    YingSFrec.UnitID = V_PerPay.SupplierID;
                    YingSFrec.Source = SC_Common.OtherInfo.Pur;
                    YingSFrec.YingSF = SC_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = V_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType = V_PerPay.PayType;
                    YingSFrec.Payer = V_PerPay.Payer;
                    YingSFrec.Gatheringer = V_PerPay.Gatheringer;
                    YingSFrec.Remark = V_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_YingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<SC_YingSFrecdetail>();
                    var PiaoInService = this.m_UnitOfWork.GetRepositoryBase<SC_PiaoIn>();
                    //插入记录明细 
                    foreach (var p in perpays)
                    {
                        SC_YingSFrecdetail YingSFrecetail = new SC_YingSFrecdetail();
                        YingSFrecetail.FinanceNo = Convert.ToInt32(YingSFrec.ID);
                        YingSFrecetail.OrderNo = Convert.ToInt32(p.ID);
                        YingSFrecetail.PayMoney = p.PayMoney2;
                        YingSFrecetail.PayFavourable = p.PayFavourable2;
                        YingSFrecdetailService.Add(YingSFrecetail);

                        //更新采购入库单的付款情况
                        var piaoin = PiaoInService.Get(p.ID.ToString());
                        piaoin.PayMoney = piaoin.PayMoney + p.PayMoney2;
                        piaoin.PayFavourable = piaoin.PayFavourable + p.PayFavourable2;
                        piaoin.PayOwing = piaoin.PayOwing - p.PayMoney2 - p.PayFavourable2;
                        PiaoInService.Update(piaoin, null);
                    }
                    //2.更新供应商的付款
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SC_Supply>();
                    var Supplier = SupplierService.Get(V_PerPay.SupplierID.ToString());
                    if (payall > 0)
                    {
                        Supplier.PayMoney = Supplier.PayMoney - payall;
                        SupplierService.Update(Supplier, null);
                    }

                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Query().Where(t => t.FinanceType == V_PerPay.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    SC_FinanceRecord FinanceRecord = new SC_FinanceRecord();
                    FinanceRecord.FinanceType = V_PerPay.PayType;
                    FinanceRecord.IsInOrOut = SC_Common.OtherInfo.Out;
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
                    FinanceRecord.UseType = SC_Common.OtherInfo.Pur; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "采购付款 " + Supplier.SupplierName;
                    this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Add(FinanceRecord);
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

        #region 对期初付款
        /// <summary>
        /// 对期初付款
        /// </summary>
        /// <param name="SC_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PayIni(SC_YingSFrec SC_YingSFrec)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    SC_YingSFrec YingSFrec = new SC_YingSFrec();
                    YingSFrec.UnitID = SC_YingSFrec.UnitID;
                    YingSFrec.Source = SC_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = SC_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = SC_YingSFrec.FinanceDate;
                    YingSFrec.FinanceMoney = SC_YingSFrec.FinanceMoney;
                    YingSFrec.PayFavourable = SC_YingSFrec.PayFavourable;
                    YingSFrec.PayType = SC_YingSFrec.PayType;
                    YingSFrec.Payer = SC_YingSFrec.Payer;
                    YingSFrec.Gatheringer = SC_YingSFrec.Gatheringer;
                    YingSFrec.Remark = SC_YingSFrec.Remark;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_YingSFrec>().Add(YingSFrec);


                    //2.更新供应商的付款
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<SC_Supply>();
                    var Supplier = SupplierService.Get(SC_YingSFrec.UnitID.ToString());
                    if (SC_YingSFrec.FinanceMoney > 0)
                    {
                        Supplier.PaidOut = Supplier.PaidOut + SC_YingSFrec.FinanceMoney;
                        Supplier.PaidFavourable = Supplier.PaidFavourable + SC_YingSFrec.PayFavourable;
                        Supplier.PaidOwing = Supplier.PaidOwing - SC_YingSFrec.FinanceMoney - SC_YingSFrec.PayFavourable;
                        Supplier.PayMoney = Supplier.PayMoney - SC_YingSFrec.FinanceMoney - SC_YingSFrec.PayFavourable;
                        SupplierService.Update(Supplier, null);
                    }

                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Query().Where(t => t.FinanceType == SC_YingSFrec.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    SC_FinanceRecord FinanceRecord = new SC_FinanceRecord();
                    FinanceRecord.FinanceType = SC_YingSFrec.PayType;
                    FinanceRecord.IsInOrOut = SC_Common.OtherInfo.Out;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = SC_YingSFrec.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance - SC_YingSFrec.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = -SC_YingSFrec.FinanceMoney;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = SC_Common.OtherInfo.PerPayIni; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "期初付款 " + Supplier.SupplierName;
                    this.m_UnitOfWork.GetRepositoryBase<SC_FinanceRecord>().Add(FinanceRecord);
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

        #region 商品加工
        /// <summary>
        /// 商品加工
        /// </summary>
        /// <param name="PiaoChangeModel"></param>
        /// <returns></returns>
        public ResultInfo ChangeInlib(PiaoChangeModel PiaoChangeModel)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var modle = this.m_UnitOfWork.GetRepositoryBase<SC_GongYi>().Get(PiaoChangeModel.id.ToString());
                    SC_PiaoChange PiaoChange = new SC_PiaoChange();
                    Random ran = new Random();
                    string orderno = string.Format("C{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                    PiaoChange.ChangNo = orderno;
                    PiaoChange.ChangeName = modle.GYName;
                    PiaoChange.ChangeDate = DateTime.Now;
                    PiaoChange.ChangeMoney = 0;
                    PiaoChange.Quantity = 0;
                    PiaoChange.PriceUnit = 0;
                    PiaoChange.Remark = PiaoChangeModel.remark;
                    PiaoChange.Operater = AuthorizationService.CurrentUserName;
                    PiaoChange.Auditor = AuthorizationService.CurrentUserName;
                    PiaoChange.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<SC_PiaoChange>().Add(PiaoChange);

                    var nowlibService = this.m_UnitOfWork.GetRepositoryBase<SC_NowLib>();
                    var gService = this.m_UnitOfWork.GetRepositoryBase<SC_Goods>();
                    foreach (var q in PiaoChangeModel.records3)
                    {
                        SC_ZhangChange ZhangChange = new SC_ZhangChange();
                        ZhangChange.ChangeID = Convert.ToInt32(PiaoChange.ID);
                        ZhangChange.LibID = q.lib;
                        ZhangChange.GoodsID = q.GoodsID;
                        ZhangChange.Quantity = -q.Quantity;
                        var nowlib = nowlibService.Query().Where(t => t.LibID == q.lib && t.GoodsID == q.GoodsID).FirstOrDefault();
                        ZhangChange.PriceUnit = nowlib.PirceUnit;
                        ZhangChange.ChangeMoney = ZhangChange.Quantity * ZhangChange.PriceUnit;
                        this.m_UnitOfWork.GetRepositoryBase<SC_ZhangChange>().Add(ZhangChange);

                        //更新库存
                        nowlib.Quantity = nowlib.Quantity - q.Quantity;
                        nowlib.OutDate = DateTime.Now;
                        nowlibService.Update(nowlib, null);

                        //更新商品总金额
                        var good = gService.Get(q.GoodsID.ToString());
                        good.LibMoney = good.LibMoney - ZhangChange.ChangeMoney;
                        gService.Update(good, null);
                    }

                    foreach (var q in PiaoChangeModel.records4)
                    {
                        SC_ZhangChange ZhangChange = new SC_ZhangChange();
                        ZhangChange.ChangeID = Convert.ToInt32(PiaoChange.ID);
                        ZhangChange.LibID = PiaoChangeModel.lib;
                        ZhangChange.GoodsID = q.GoodsID;
                        ZhangChange.Quantity = q.Quantity;
                        ZhangChange.PriceUnit = q.Pirce;
                        ZhangChange.ChangeMoney = ZhangChange.Quantity * ZhangChange.PriceUnit;
                        this.m_UnitOfWork.GetRepositoryBase<SC_ZhangChange>().Add(ZhangChange);

                        var nowlib = nowlibService.Query().Where(t => t.LibID == PiaoChangeModel.lib && t.GoodsID == q.GoodsID).FirstOrDefault();

                        //2.如果没有库存就insert一条记录
                        //插入数据之前，计算当前总库存，和总金额
                        var libgoods = nowlibService.Query().Where(t => t.GoodsID == q.GoodsID).ToList();
                        var Quantity = libgoods.Sum(t => t.Quantity);
                        var libMoney = libgoods.Sum(t => t.Quantity * t.PirceUnit);
                        //计算增加的库存以及金额
                        Quantity = Quantity + ZhangChange.Quantity;
                        libMoney = libMoney + ZhangChange.ChangeMoney;
                        var price = libMoney / Quantity;

                        if (nowlib == null)
                        {
                            SC_NowLib NowLib = new SC_NowLib();
                            NowLib.GoodsID = q.GoodsID;
                            NowLib.LibID = PiaoChangeModel.lib;
                            NowLib.Quantity = q.Quantity;
                            NowLib.PirceUnit = price; //移动加权的单价，针对的是所有的仓库，所有的供应商一起加权
                            NowLib.InDate = DateTime.Now;
                            nowlibService.Add(NowLib);
                        }
                        else
                        {
                            nowlib.PirceUnit = price;
                            nowlib.Quantity = nowlib.Quantity + q.Quantity;
                            nowlib.OutDate = DateTime.Now;
                            nowlibService.Update(nowlib, null);
                        }

                        //更新商品总金额
                        var good = gService.Get(q.GoodsID.ToString());
                        good.LibMoney = good.LibMoney + ZhangChange.ChangeMoney;
                        gService.Update(good, null);

                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null };
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

        #region 更新资产的状态
        /// <summary>
        /// 更新资产的状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void UpdateFixedCondition(int fixedID)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                var fix = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed>().Get(fixedID.ToString());
                string condition = fix.Condition;
                if (fix != null)
                {
                    //1.判断是否借出
                    var Circulate = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_Circulate>().Query().Where(t => t.FixedID == fixedID && !t.IsBack).FirstOrDefault();
                    if (Circulate != null)
                    {
                        fix.Condition = SC_Common.FxiedCondition.Circulate;
                    }
                    else
                    {
                        var Maintain = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_Maintain>().Query().Where(t => t.FixedID == fixedID && !t.IsOver).FirstOrDefault();
                        if (Maintain != null)
                        {
                            fix.Condition = SC_Common.FxiedCondition.Maintain;
                        }
                        else
                        {
                            var Clean = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_Clean>().Query().Where(t => t.FixedID == fixedID).FirstOrDefault();
                            if (Clean != null)
                            {
                                if (string.IsNullOrEmpty(Clean.CleanType))
                                {
                                    fix.Condition = SC_Common.FxiedCondition.Clean;
                                }
                                else
                                {
                                    fix.Condition = Clean.CleanType;
                                }
                            }
                            else
                            {
                                var Pan = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_Result>().Query().Where(t => t.FixedID == fixedID && t.CheckResult == SC_Common.FxiedCondition.PanKui).OrderByDescending(t => t.ID).FirstOrDefault();
                                if (Pan != null && Pan.CheckResult == SC_Common.FxiedCondition.PanKui)
                                {
                                    fix.Condition = SC_Common.FxiedCondition.PanKui;
                                }
                                else
                                {
                                    fix.Condition = SC_Common.FxiedCondition.Normal;
                                }
                            }
                        }
                    }

                    fix.NetSalvage = fix.FixedPirce * fix.NetSalvageRate / 100;
                    if (fix.UseYear > 0)
                    {
                        fix.UseEndDate = fix.AddDate.AddYears(Convert.ToInt32(fix.UseYear));
                        if (fix.Depreciation == SC_Common.Depreciation.DYear)
                        {
                            fix.DepreciaMonthRate = (1 - fix.NetSalvageRate / 100) / fix.UseYear / 12;
                            fix.DepreciaMonthMoney = fix.FixedPirce * fix.DepreciaMonthRate;
                        }
                    } 
                    this.m_UnitOfWork.GetRepositoryBase<SC_Fixed>().Update(fix, null);

                    var pan = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_PanDian>().Query().Where(t => t.FixedID == fixedID).FirstOrDefault();
                    if (pan != null && pan.Condition != fix.Condition)
                    {
                        pan.Condition = fix.Condition;
                        this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_PanDian>().Update(pan, null);
                    }

                    tx.Commit();
                }
            }
        }
        #endregion

        #region 资产盘点
        /// <summary>
        /// 开始资产盘点，新增资产盘点数据
        /// </summary>
        /// <returns></returns>
        public ResultInfo Pandian()
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //先删除左右盘点数据
                    var panService = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_PanDian>();
                    var pan = panService.All();
                    foreach (var p in pan)
                    {
                        panService.Delete(p);
                    }
                    //插入对应的资产数据到盘点表
                    var fixeds = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed>().Query().Where(t => t.Condition == SC_Common.FxiedCondition.Normal
                       || t.Condition == SC_Common.FxiedCondition.Circulate
                       || t.Condition == SC_Common.FxiedCondition.Maintain
                       || t.Condition == SC_Common.FxiedCondition.PanKui
                       || t.Condition == ""
                     );
                    foreach (var f in fixeds)
                    {

                        SC_Fixed_PanDian fp = new SC_Fixed_PanDian();
                        fp.FixedID = Convert.ToInt32(f.ID);
                        fp.Fcode = f.Fcode;
                        fp.Fname = f.Fname;
                        fp.BarCode = f.BarCode;
                        fp.BrevityCode = f.BrevityCode;
                        fp.Ftype = f.TypeName;
                        fp.Spec = f.Spec;
                        fp.AddDate = f.AddDate;
                        fp.FixedPirce = f.FixedPirce;
                        fp.Position = f.Position;
                        fp.DepartMent = f.DepartMent;
                        fp.Storeman = f.Storeman;
                        fp.Condition = f.Condition;
                        if (fp.Condition != SC_Common.FxiedCondition.PanKui)
                        {
                            fp.AutoQuantity = 1;
                            fp.CheckResult = SC_Common.FxiedCondition.Normal;
                        }
                        else
                        {
                            fp.AutoQuantity = 0;
                            fp.CheckResult = SC_Common.FxiedCondition.PanKui;
                        }
                        fp.Quantity = fp.AutoQuantity;
                      

                        panService.Add(fp);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null };
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
        /// 盘点取消操作
        /// </summary>
        /// <returns></returns>
        public ResultInfo Cancel()
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var panService = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_PanDian>();
                    var pan = panService.All();
                    foreach (var p in pan)
                    {
                        panService.Delete(p);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null };
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
        /// 完成盘点操作
        /// </summary>
        /// <returns></returns>
        public ResultInfo Over()
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var panService = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_PanDian>();
                    var fixedService = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed>();

                    var reslutService = this.m_UnitOfWork.GetRepositoryBase<SC_Fixed_Result>();
                    //查出盘点数目与电脑实际记录数目不一致的
                    Random ran = new Random();
                    string orderno = string.Format("P{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
                    var queryPan = panService.Query().Where(t => t.Quantity != t.AutoQuantity);
                    foreach (var q in queryPan)
                    {
                        var fix = fixedService.Get(q.FixedID.ToString());
                        SC_Fixed_Result re = new SC_Fixed_Result();
                        re.CheckNo = orderno;
                        re.FixedID = q.FixedID;
                        re.Fcode = q.Fcode;
                        re.Fname = q.Fname;
                        re.BarCode = q.BarCode;

                        re.FixedPirce = q.FixedPirce;
                        re.AutoQuantity = q.AutoQuantity;
                        re.Quantity = q.Quantity;
                        re.CheckResult = q.CheckResult;
                        re.CheckStartDate = q.BuildTime;
                        re.CheckEndDate = DateTime.Now;

                        re.BrevityCode = fix.BrevityCode;
                        re.Ftype = fix.TypeName;
                        re.Spec = fix.Spec;
                        re.AddDate = fix.AddDate;
                        re.FixedPirce = fix.FixedPirce;
                        re.Storeman = fix.Storeman;
                        re.Position = fix.Position;
                        re.DepartMent = fix.DepartMent;
                        re.Configure = fix.Configure;
                        if (q.CheckResult == SC_Common.FxiedCondition.PanYing)
                        {
                            fix.Condition = SC_Common.FxiedCondition.Normal;
                        }
                        if (q.CheckResult == SC_Common.FxiedCondition.PanKui)
                        {
                            fix.Condition = SC_Common.FxiedCondition.PanKui;
                        }
                        reslutService.Add(re);
                        fixedService.Update(fix, null);

                    }
                    var pan = panService.All();
                    foreach (var p in pan)
                    {
                        panService.Delete(p);
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null };
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

        #region 商品导入
        public ResultInfo InsertRecord(List<SC_Goods> SC_Goodss)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var recordService = this.m_UnitOfWork.GetRepositoryBase<SC_Goods>();
                    foreach (var s in SC_Goodss)
                    {
                        var piaoin = recordService.Add(s);

                        //增加辅助单位
                        var goodsid = Convert.ToInt32(piaoin.ID); 
                        SC_GoodsUnit umodel = new SC_GoodsUnit();
                        umodel.Unit = piaoin.Unit;
                        umodel.Rate = 1;
                        umodel.GoodsID = goodsid;
                        umodel.UnitDesc = "最小计量单位";
                        umodel.Meno = "系统生成";
                        this.m_UnitOfWork.GetRepositoryBase<SC_GoodsUnit>().Add(umodel);

                    }

                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null };
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
