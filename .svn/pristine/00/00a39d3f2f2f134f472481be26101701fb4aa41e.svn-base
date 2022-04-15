using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using ZLERP.IRepository;
using ZLERP.Model;
using System.Collections;
using ZLERP.Model.Beton;
using ZLERP.Model.Enums;

namespace ZLERP.Business.Beton
{
    /*
        此模块是砼结算管理功能服务类模块，做事务处理 by  mandzy 2019-02-14
     */
    public class BetonService : ServiceBase<B_CarFleet>
    {
        internal BetonService(IUnitOfWork uow) : base(uow) { }

        private object ObjLock = new object();

        #region 运费结算


        /// <summary>
        /// 运输公司预付款
        /// </summary>
        /// <param name="yingsf"></param>
        /// <returns></returns>
        public ResultInfo PerPayTran(B_TranYingSFrec yingsf)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    B_TranYingSFrec YingSFrec = new B_TranYingSFrec();
                    YingSFrec.UnitID = yingsf.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.PerPay;
                    YingSFrec.YingSF = B_Common.OtherInfo.Out;
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
                    this.m_UnitOfWork.GetRepositoryBase<B_TranYingSFrec>().Add(YingSFrec);


                    //2.更新供应商的预付款
                    var fin = this.m_UnitOfWork.GetRepositoryBase<B_TranFinanceRecord>().Query().Where(t => t.FinanceType == yingsf.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<B_CarFleet>();
                    var Supplier = SupplierService.Get(yingsf.UnitID.ToString());


                    B_TranFinanceRecord FinanceRecord = new B_TranFinanceRecord();
                    FinanceRecord.FinanceType = yingsf.PayType;
                    FinanceRecord.IsInOrOut = B_Common.OtherInfo.Out;
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
                    FinanceRecord.UseType = B_Common.OtherInfo.PerPay; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "预付 " + Supplier.FleetName;
                    this.m_UnitOfWork.GetRepositoryBase<B_TranFinanceRecord>().Add(FinanceRecord);

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
        /// 搅拌车结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo TranBalance(List<string> ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //排除已经生成结算单的运输单
                    var ShipDocIDs = this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Query().Where(t => ids.Contains(t.ShipDocID) && t.ModelType == ZLERP.Model.Beton.B_Common.ModelType.JbCar).Select(t => t.ShipDocID);
                    if (ShipDocIDs != null && ShipDocIDs.Count() > 0)
                    {
                        foreach (var id in ShipDocIDs)
                        {
                            ids.Remove(id);
                        }
                    }
                    if (ids.Count <= 0)
                    {
                        return new ResultInfo() { Result = true, Message = "没有符合要求的运输单" };
                    }
                    //查询出所有运输单进行汇总作业 
                    var query = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(t => ids.Contains(t.ID) && t.IsEffective && t.IsAudit).ToList();

                    //查询出所有运输单对应的车号
                    var carids = query.Select(t => t.CarID).Distinct().ToList();
                    var cars = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => carids.Contains(t.ID)).ToList();


                    //工地加车队
                    var bale = (from q in query
                                join c in cars on q.CarID equals c.ID
                                group q by new { c.RentCarName, q.ProjectID } into g
                                select new
                                {
                                    TransportID = g.Key.RentCarName,
                                    ProjectID = g.Key.ProjectID
                                }).ToList();

                    //获取所有车队信息
                    var CarTempletids = bale.Select(t => t.TransportID.ToString()).ToList();

                    //获取所有工地信息
                    var ProjectIDids = bale.Select(t => t.ProjectID).ToList();
                    var Projects = this.m_UnitOfWork.GetRepositoryBase<Project>().Query().Where(t => ProjectIDids.Contains(t.ID)).ToList();


                    var B_CarFleets = this.m_UnitOfWork.GetRepositoryBase<B_CarFleet>().Query().Where(t => CarTempletids.Contains(t.ID)).ToList();

                    //获取工地的所有运费计算模板  
                    var B_FareTempletids = Projects.Select(t => t.CarTemplet.ToString()).Distinct().ToList();
                    var B_FareTemplets = this.m_UnitOfWork.GetRepositoryBase<B_FareTemplet>().Query().Where(t => B_FareTempletids.Contains(t.ID)).ToList();

                    //获取模板的所有数据 
                    var B_FareTempletDel1ids = B_FareTemplets.Select(t => Convert.ToInt32(t.ID)).Distinct().ToList();
                    var B_FareTempletDel1s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel1>().Query().Where(t => B_FareTempletDel1ids.Contains(t.FareTempletID)).ToList();

                    //获取所有模板的明细数据 
                    var B_FareTempletDel2ids = B_FareTempletDel1s.Select(t => Convert.ToInt32(t.ID)).Distinct().ToList();
                    var B_FareTempletDel2s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel2>().Query().Where(t => B_FareTempletDel2ids.Contains(t.FareTempletDelID)).ToList();

                    foreach (var b in bale)
                    {
                        if (b.TransportID <= 0 || string.IsNullOrWhiteSpace(b.ProjectID))
                        {
                            continue;
                        }
                        //当前工程的运费模板
                        var pro = Projects.FirstOrDefault(t => t.ID == b.ProjectID);
                        var B_FareTemplet = new B_FareTemplet();
                        if (pro != null)
                        {
                            B_FareTemplet = pro.B_FareTemplet;
                        }
                        else
                        {
                            B_FareTemplet = null;
                        }
                        //根据车队获取所有的车号 
                        var carlist = cars.Where(t => t.RentCarName == b.TransportID).Select(t => t.ID).ToList();
                        //根据车号获取运输单明细
                        var listDel = query.Where(t => t.ProjectID == b.ProjectID && carlist.Contains(t.CarID)).ToList();

                        //新增结算单主体数据
                        B_TranBalance BaleBalance = new B_TranBalance();
                        lock (ObjLock)
                        {
                            System.Threading.Thread.Sleep(100);
                            string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                            BaleBalance.BaleNo = string.Format("{0}{1}", "T", orderNo);
                        }
                        BaleBalance.OrderDate = DateTime.Now;

                        BaleBalance.TranID = Convert.ToInt32(b.TransportID);
                        BaleBalance.ThatBaleMan = "";
                        BaleBalance.ModelType = ZLERP.Model.Beton.B_Common.ModelType.JbCar;
                        BaleBalance.ProjectID = b.ProjectID;
                        BaleBalance.StartDate = listDel.Min(t => t.ProduceDate);
                        BaleBalance.EndDate = listDel.Max(t => t.ProduceDate);
                        string EndDay = base.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(a => a.ConfigName == "ChangeDay").First().ConfigValue.ToStr();
                        BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).ToString("yyyy-MM");
                        if (Convert.ToDateTime(BaleBalance.EndDate).Day>EndDay.ToInt())
                        {
                            BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).AddMonths(1).ToString("yyyy-MM");
                        }
                        if (B_FareTemplet != null)
                        {
                            BaleBalance.BaleType = Convert.ToInt32(B_FareTemplet.ID);
                        }
                        else
                        {
                            //获取第一条运费模板
                            var B_FareTempletFirst = this.m_UnitOfWork.GetRepositoryBase<B_FareTemplet>().Query().OrderByDescending(t => t.BuildTime).First();
                            BaleBalance.BaleType = Convert.ToInt32(B_FareTempletFirst.ID);
                        }
                        BaleBalance.IsOnePrice = false;
                        BaleBalance.OnePriceType = B_Common.BalaneType.type3;
                        BaleBalance.IsStockPrice = true;

                        BaleBalance.AuditStatus = 0;
                        BaleBalance.BaleDate = DateTime.Now;
                        BaleBalance.BaleMan = AuthorizationService.CurrentUserName;
                        BaleBalance.Remark = "自动生成的结算单";

                        List<B_TranBalanceDel> B_TranBalanceDelList = new List<B_TranBalanceDel>();
                        decimal AllCount = 0;
                        decimal AllMoney = 0;

                        foreach (var d in listDel)
                        {
                            B_TranBalanceDel BaleBalanceDel = new B_TranBalanceDel();
                            //  BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            BaleBalanceDel.ShipDocID = d.ID;
                            BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.JbCar;
                            if (B_FareTemplet != null)
                            {
                                //计算单价
                                var car = cars.FirstOrDefault(t => t.ID == d.CarID);

                                var B_FareTempletDel1 = B_FareTempletDel1s.Where(t => t.FareTempletID == Convert.ToInt32(B_FareTemplet.ID) && t.FareType == car.CarTypeID && t.BalaneType == B_Common.BalaneType.type3 && t.EffectiveTime <= d.ProduceDate).OrderByDescending(t => t.EffectiveTime).FirstOrDefault();
                                if (B_FareTempletDel1 != null)
                                {
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type1)
                                    {
                                        var B_FareTempletDel2 = B_FareTempletDel2s.Where(t => t.FareTempletDelID == Convert.ToInt32(B_FareTempletDel1.ID) && t.UpKilometre <= d.Distance && t.DwonKilometre > d.Distance).FirstOrDefault();
                                        if (B_FareTempletDel2 != null)
                                        {
                                            BaleBalanceDel.Price = B_FareTempletDel2.Price;
                                            BaleBalanceDel.AllMoney = Convert.ToDecimal(d.Distance) * BaleBalanceDel.Price;
                                            AllCount += Convert.ToDecimal(d.Distance);
                                        }
                                        else
                                        {
                                            BaleBalanceDel.Price = 0;
                                            BaleBalanceDel.AllMoney = 0;
                                        }
                                    }
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type2)
                                    {
                                        BaleBalanceDel.Price = B_FareTempletDel1.Price;
                                        BaleBalanceDel.AllMoney = BaleBalanceDel.Price;
                                        AllCount++;
                                    }
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type3)
                                    {
                                        if (B_FareTempletDel1.CarBetonNum > 0 && B_FareTempletDel1.CarBetonNum > d.ShippingCube)
                                        {
                                            BaleBalanceDel.CarBetonNum = B_FareTempletDel1.CarBetonNum - d.ShippingCube;
                                        }
                                        BaleBalanceDel.Price = B_FareTempletDel1.Price;
                                        BaleBalanceDel.AllMoney = (d.ShippingCube + BaleBalanceDel.CarBetonNum) * BaleBalanceDel.Price;
                                        AllCount += d.ShippingCube;
                                    }
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type4)
                                    {
                                        BaleBalanceDel.Price = B_FareTempletDel1.Price;
                                        BaleBalanceDel.AllMoney = d.Weight / 1000 * BaleBalanceDel.Price;
                                        AllCount += d.Weight;
                                    }
                                    AllMoney += BaleBalanceDel.AllMoney;
                                }
                                else
                                {
                                    BaleBalanceDel.Price = 0;
                                    BaleBalanceDel.AllMoney = 0;
                                }
                            }
                            B_TranBalanceDelList.Add(BaleBalanceDel);

                        }
                        BaleBalance.AllCount = AllCount;
                        BaleBalance.AllMoney = AllMoney;
                        BaleBalance.AllOkCount = AllCount;
                        BaleBalance.AllOkMoney = AllMoney;
                        BaleBalance.PayMoney = 0;
                        BaleBalance.OtherMoney = 0;
                        BaleBalance.PayOwing = AllMoney;
                        BaleBalance.PayFavourable = 0;
                        var m_bale = this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>().Add(BaleBalance);

                        foreach (var BaleBalanceDel in B_TranBalanceDelList)
                        {
                            BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Add(BaleBalanceDel);
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
        /// 泵车结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo PeCarBalance(List<string> ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //排除已经生成结算单的运输单
                    var ShipDocIDs = this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Query().Where(t => ids.Contains(t.ShipDocID) && t.ModelType == ZLERP.Model.Beton.B_Common.ModelType.PeCar).Select(t => t.ShipDocID);
                    if (ShipDocIDs != null && ShipDocIDs.Count() > 0)
                    {
                        foreach (var id in ShipDocIDs)
                        {
                            ids.Remove(id);
                        }
                    }
                    if (ids.Count <= 0)
                    {
                        return new ResultInfo() { Result = true, Message = "没有符合要求的运输单" };
                    }
                    //查询出所有运输单进行汇总作业 
                    var query = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(t => ids.Contains(t.ID) && t.IsEffective && t.IsAudit).ToList();

                    //查出所有泵车的id
                    var carids = query.Select(t => t.PumpName).Distinct().ToList();
                    var pumpCars = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => carids.Contains(t.ID)).ToList();


                    //工地加车队
                    var bale = (from q in query
                                join c in pumpCars on q.PumpName equals c.ID
                                group q by new { c.RentCarName, q.ProjectID } into g
                                select new
                                {
                                    TransportID = g.Key.RentCarName,
                                    ProjectID = g.Key.ProjectID
                                }).ToList();

                    //获取所有车队信息
                    var CarTempletids = bale.Select(t => t.TransportID.ToString()).ToList();

                    //获取所有工地信息
                    var ProjectIDids = bale.Select(t => t.ProjectID).ToList();
                    var Projects = this.m_UnitOfWork.GetRepositoryBase<Project>().Query().Where(t => ProjectIDids.Contains(t.ID)).ToList();


                    //获取所有合同的明细砼信息 
                    var contratids = query.Select(t => t.ContractID).Distinct().ToList();

                    //获取所有合同的泵送价格信息 
                    var contractPumps = this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Query().Where(t => contratids.Contains(t.ContractID)).ToList();


                    foreach (var b in bale)
                    {
                        if (b.TransportID <= 0 || string.IsNullOrWhiteSpace(b.ProjectID))
                        {
                            continue;
                        }

                        //根据车队获取所有的泵车
                        var carlist = pumpCars.Where(t => t.RentCarName == b.TransportID).Select(t => t.ID).ToList();
                        //根据车号获取运输单明细
                        var listDel = query.Where(t => t.ProjectID == b.ProjectID && carlist.Contains(t.PumpName)).ToList();

                        //新增结算单主体数据
                        B_TranBalance BaleBalance = new B_TranBalance();
                        lock (ObjLock)
                        {
                            System.Threading.Thread.Sleep(100);
                            string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                            BaleBalance.BaleNo = string.Format("{0}{1}", "T", orderNo);
                        }
                        BaleBalance.OrderDate = DateTime.Now;

                        BaleBalance.TranID = Convert.ToInt32(b.TransportID);
                        BaleBalance.ThatBaleMan = "";
                        BaleBalance.ModelType = ZLERP.Model.Beton.B_Common.ModelType.PeCar;
                        BaleBalance.ProjectID = b.ProjectID;
                        BaleBalance.StartDate = listDel.Min(t => t.ProduceDate);
                        BaleBalance.EndDate = listDel.Max(t => t.ProduceDate);
                        string EndDay = base.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(a => a.ConfigName == "ChangeDay").First().ConfigValue.ToStr();
                        BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).ToString("yyyy-MM");
                        if (Convert.ToDateTime(BaleBalance.EndDate).Day > EndDay.ToInt())
                        {
                            BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).AddMonths(1).ToString("yyyy-MM");
                        }
                        BaleBalance.BaleType = 0;

                        BaleBalance.IsOnePrice = false;
                        BaleBalance.OnePriceType = B_Common.BalaneType.type3;
                        BaleBalance.IsStockPrice = true;

                        BaleBalance.AuditStatus = 0;
                        BaleBalance.BaleDate = DateTime.Now;
                        BaleBalance.BaleMan = AuthorizationService.CurrentUserName;
                        BaleBalance.Remark = "自动生成的结算单";

                        List<B_TranBalanceDel> B_TranBalanceDelList = new List<B_TranBalanceDel>();
                        decimal AllCount = 0;
                        decimal AllMoney = 0;

                        foreach (var d in listDel)
                        {
                            B_TranBalanceDel BaleBalanceDel = new B_TranBalanceDel();
                            //  BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            BaleBalanceDel.ShipDocID = d.ID;
                            BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.PeCar;

                            #region 计算泵送金额

                            //var pumpcar = pumpCars.Where(t => t.ID == d.PumpName).FirstOrDefault();
                            //if (pumpcar != null)
                            //{
                                var pump = contractPumps.Where(t => t.ContractID == d.ContractID && t.PumpType == d.CastMode && t.SetDate <= d.ProduceDate).OrderByDescending(t => t.SetDate).FirstOrDefault();
                                if (pump != null)
                                {
                                    BaleBalanceDel.Price = Convert.ToDecimal(pump.PumpPrice);
                                    BaleBalanceDel.AllMoney = d.SignInCube * BaleBalanceDel.Price;
                                    AllCount += d.SignInCube;
                                    AllMoney += BaleBalanceDel.AllMoney;
                                }
                            //}

                            #endregion
                            B_TranBalanceDelList.Add(BaleBalanceDel);

                        }
                        BaleBalance.AllCount = AllCount;
                        BaleBalance.AllMoney = AllMoney;
                        BaleBalance.AllOkCount = AllCount;
                        BaleBalance.AllOkMoney = AllMoney;
                        BaleBalance.PayMoney = 0;
                        BaleBalance.OtherMoney = 0;
                        BaleBalance.PayOwing = AllMoney;
                        BaleBalance.PayFavourable = 0;
                        var m_bale = this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>().Add(BaleBalance);

                        foreach (var BaleBalanceDel in B_TranBalanceDelList)
                        {
                            BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Add(BaleBalanceDel);
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
        /// 删除结算单的时候，必须同步删除明细删除结算单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo DeleteTranBalance(string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var service = this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>();
                    var serviceDel = this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>();
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
                    var bale = this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>().Get(id);
                    var BaleBalanceID = Convert.ToInt32(id);
                    var M_Del = this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Query().Where(t => t.BaleBalanceID == BaleBalanceID).ToList();

                    //获取运费模板
                    var B_FareTemplets = this.m_UnitOfWork.GetRepositoryBase<B_FareTemplet>().Query().Where(t => t.ID == bale.BaleType.ToString()).FirstOrDefault();

                    //获取模板的所有数据 
                    var tempid = Convert.ToInt32(B_FareTemplets.ID);
                    var B_FareTempletDel1s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel1>().Query().Where(t => t.FareTempletID == tempid).ToList();

                    //获取所有模板的明细数据 
                    var B_FareTempletDel2ids = B_FareTempletDel1s.Select(t => Convert.ToInt32(t.ID)).ToList();
                    var B_FareTempletDel2s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel2>().Query().Where(t => B_FareTempletDel2ids.Contains(t.FareTempletDelID)).ToList();

                    //获取所有有关的运费设定信息
                    decimal? AllCount = 0;
                    decimal? AllMoney = 0;
                    #region  如果是运费设定结算
                    if (bale.IsStockPrice)
                    {

                        var car = new Car();
                        foreach (var del in M_Del)
                        {
                            //计算单价
                            var shippingDocument = del.ShippingDocument;
                            if (shippingDocument == null)
                            {
                                shippingDocument = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(del.ShipDocID);
                            }
                            var carid = shippingDocument.CarID;
                            if (car == null || carid != car.ID)
                            {
                                car = this.m_UnitOfWork.GetRepositoryBase<Car>().Get(carid);
                            }
                            var B_FareTempletDel1 = B_FareTempletDel1s.Where(t => t.FareTempletID == bale.BaleType && t.FareType == car.CarTypeID && t.BalaneType == bale.OnePriceType && t.EffectiveTime <= shippingDocument.ProduceDate).OrderByDescending(t => t.EffectiveTime).FirstOrDefault();
                            if (B_FareTempletDel1 != null)
                            {
                                if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type1)
                                {
                                    var B_FareTempletDel2 = B_FareTempletDel2s.Where(t => t.FareTempletDelID == Convert.ToInt32(B_FareTempletDel1.ID) && t.UpKilometre <= shippingDocument.Distance && t.DwonKilometre > shippingDocument.Distance).FirstOrDefault();
                                    if (B_FareTempletDel2 != null)
                                    {
                                        del.Price = B_FareTempletDel2.Price;
                                        del.AllMoney = Convert.ToDecimal(shippingDocument.Distance) * del.Price;
                                        AllCount += shippingDocument.Distance;
                                    }
                                    else
                                    {
                                        del.Price = 0;
                                        del.AllMoney = 0;
                                    }
                                }
                                if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type2)
                                {
                                    del.Price = B_FareTempletDel1.Price;
                                    del.AllMoney = del.Price;
                                    AllCount++;
                                }
                                if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type3)
                                {
                                    del.Price = B_FareTempletDel1.Price;
                                    if (del.CarBetonNum == 0)
                                    {
                                        if (B_FareTempletDel1.CarBetonNum > 0 && B_FareTempletDel1.CarBetonNum > shippingDocument.ShippingCube)
                                        {
                                            del.CarBetonNum = B_FareTempletDel1.CarBetonNum - shippingDocument.ShippingCube;
                                        }
                                    }
                                    del.AllMoney = (shippingDocument.SignInCube + del.CarBetonNum) * del.Price;
                                    if (del.CarBetonNum < 0)
                                    {
                                        del.AllMoney = (shippingDocument.SignInCube) * del.Price;
                                    }
                                    AllCount += shippingDocument.SignInCube + del.CarBetonNum;
                                }
                                if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type4)
                                {
                                    del.Price = B_FareTempletDel1.Price;
                                    del.AllMoney = shippingDocument.Weight / 1000 * del.Price;
                                    AllCount += shippingDocument.Weight;
                                }
                                del.AllMoney = del.AllMoney + del.OutTimeFee + del.OutDistanceFee + del.CustBetonfee + del.OtherFee;
                                AllMoney += del.AllMoney;
                            }
                            else
                            {
                                del.Price = 0;
                                del.AllMoney = 0;
                            }
                            this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Update(del, null);
                        }
                    }

                    #endregion
                    #region 如果是单一价格计算
                    if (bale.IsOnePrice)
                    {
                        var car = new Car();
                        foreach (var del in M_Del)
                        {
                            var shippingDocument = del.ShippingDocument;
                            if (shippingDocument == null)
                            {
                                shippingDocument = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(del.ShipDocID);
                            }
                            var carid = shippingDocument.CarID;
                            if (car == null || carid != car.ID)
                            {
                                car = this.m_UnitOfWork.GetRepositoryBase<Car>().Get(carid);
                            }
                            del.Price = Convert.ToDecimal(bale.OnePrice);
                            var B_FareTempletDel1 = B_FareTempletDel1s.Where(t => t.FareTempletID == bale.BaleType && t.FareType == car.CarTypeID && t.BalaneType == bale.OnePriceType && t.EffectiveTime <= shippingDocument.ProduceDate).OrderByDescending(t => t.EffectiveTime).FirstOrDefault();

                            if (bale.OnePriceType == B_Common.BalaneType.type1)
                            {
                                AllCount += shippingDocument.Distance;
                                del.AllMoney = Convert.ToDecimal(shippingDocument.Distance) * del.Price;
                            }
                            if (bale.OnePriceType == B_Common.BalaneType.type2)
                            {
                                del.AllMoney = del.Price;
                                AllCount++;
                            }
                            if (bale.OnePriceType == B_Common.BalaneType.type3)
                            {
                                if (del.CarBetonNum == 0)
                                {
                                    if (B_FareTempletDel1.CarBetonNum > 0 && B_FareTempletDel1.CarBetonNum > shippingDocument.ShippingCube)
                                    {
                                        del.CarBetonNum = B_FareTempletDel1.CarBetonNum - shippingDocument.ShippingCube;
                                    }
                                }
                                del.AllMoney = (shippingDocument.SignInCube + del.CarBetonNum) * del.Price;
                                if (del.CarBetonNum < 0)
                                {
                                    del.AllMoney = (shippingDocument.SignInCube) * del.Price;
                                }

                                AllCount += shippingDocument.SignInCube + del.CarBetonNum;
                            }
                            if (bale.OnePriceType == B_Common.BalaneType.type4)
                            {
                                del.AllMoney = shippingDocument.Weight / 1000 * del.Price;
                                AllCount += shippingDocument.Weight;
                            }
                            del.AllMoney = del.AllMoney + del.OutTimeFee + del.OutDistanceFee + del.CustBetonfee + del.OtherFee;
                            AllMoney += del.AllMoney;

                        }
                    }
                    #endregion

                    bale.AllCount = AllCount;
                    bale.AllMoney = AllMoney;
                    bale.AllOkCount = AllCount;
                    bale.AllOkMoney = AllMoney + Convert.ToDecimal(bale.OtherMoney);
                    bale.PayMoney = 0;
                    bale.PayOwing = AllMoney;
                    bale.PayFavourable = 0;
                    bale.Modifier = AuthorizationService.CurrentUserName;
                    bale.ModifyTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>().Update(bale, null);

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


        public ResultInfo ComputePeCar(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var bale = this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>().Get(id);
                    var BaleBalanceID = Convert.ToInt32(id);
                    var M_Del = this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Query().Where(t => t.BaleBalanceID == BaleBalanceID).ToList();


                    //获取所有有关的运费设定信息
                    decimal? AllCount = 0;
                    decimal? AllMoney = 0;
                    foreach (var del in M_Del)
                    {
                        var shippingDocument = del.ShippingDocument;
                        if (shippingDocument == null)
                        {
                            shippingDocument = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(del.ShipDocID);
                        }
                        #region 计算泵送金额
                        if (bale.IsStockPrice)
                        {
                            //var car = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => t.ID == shippingDocument.PumpName).FirstOrDefault();
                            //if (car != null)
                            //{
                                var pump = this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Query().Where(t => t.ContractID == shippingDocument.ContractID && t.PumpType == shippingDocument.CastMode && t.SetDate <= shippingDocument.ProduceDate).OrderByDescending(t => t.SetDate).FirstOrDefault();
                                if (pump != null)
                                {
                                    del.Price = Convert.ToDecimal(pump.PumpPrice);
                                    del.AllMoney = (shippingDocument.SignInCube + del.CarBetonNum) * del.Price;
                                    AllCount += shippingDocument.SignInCube;
                                }
                                else
                                {
                                    del.Price = 0;
                                    del.AllMoney = 0;
                                }
                            //}

                            del.AllMoney = del.AllMoney + del.CustBetonfee + del.OtherFee;
                            AllMoney += del.AllMoney;
                            this.m_UnitOfWork.GetRepositoryBase<B_TranBalanceDel>().Update(del, null);
                        }
                        #endregion

                        #region 如果是单一价格计算
                        if (bale.IsOnePrice)
                        {
                            del.Price = Convert.ToDecimal(bale.OnePrice);
                            del.AllMoney = (shippingDocument.SignInCube + del.CarBetonNum) * del.Price;
                            del.AllMoney = del.AllMoney + del.CustBetonfee + del.OtherFee;
                            AllMoney += del.AllMoney;
                        }
                        #endregion
                    }
                    bale.AllCount = AllCount;
                    bale.AllMoney = AllMoney;
                    bale.AllOkCount = AllCount;
                    bale.AllOkMoney = AllMoney + Convert.ToDecimal(bale.OtherMoney);
                    bale.PayMoney = 0;
                    bale.PayOwing = AllMoney;
                    bale.PayFavourable = 0;
                    bale.Modifier = AuthorizationService.CurrentUserName;
                    bale.ModifyTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>().Update(bale, null);

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
        /// 对运费付款货款应付款付款
        /// </summary>
        /// <param name="B_PerPay"></param>
        /// <param name="B_PerPays"></param>
        /// <returns></returns>
        public ResultInfo PayTran(B_PerPay B_PerPay, List<b_perpay> b_perpays)
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
                    //1.插入应收应付的明细
                    B_TranYingSFrec YingSFrec = new B_TranYingSFrec();
                    YingSFrec.UnitID = B_PerPay.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.Tran;
                    YingSFrec.YingSF = B_Common.OtherInfo.Out;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = B_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType = B_PerPay.PayType;
                    YingSFrec.Payer = B_PerPay.Payer;
                    YingSFrec.Gatheringer = B_PerPay.Gatheringer;
                    YingSFrec.Remark = B_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_TranYingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<B_TranYingSFrecdetail>();
                    var BaleBalanceService = this.m_UnitOfWork.GetRepositoryBase<B_TranBalance>();
                    //插入记录明细 
                    foreach (var p in b_perpays)
                    {
                        B_TranYingSFrecdetail YingSFrecetail = new B_TranYingSFrecdetail();
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

                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<B_CarFleet>();
                    var Supplier = SupplierService.Get(B_PerPay.UnitID);
                    if (payall > 0)
                    {
                        Supplier.PayMoney = Supplier.PayMoney - payall - payper;
                        if (B_PerPay.PayType == B_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - payall;
                        }
                        SupplierService.Update(Supplier, null);
                    }
                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<B_TranFinanceRecord>().Query().Where(t => t.FinanceType == B_PerPay.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    B_TranFinanceRecord FinanceRecord = new B_TranFinanceRecord();
                    FinanceRecord.FinanceType = B_PerPay.PayType;
                    FinanceRecord.IsInOrOut = B_Common.OtherInfo.Out;
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
                    FinanceRecord.UseType = B_Common.OtherInfo.Tran; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "运费付款-" + Supplier.FleetName;
                    this.m_UnitOfWork.GetRepositoryBase<B_TranFinanceRecord>().Add(FinanceRecord);
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
        /// 对运费期初付款对期初付款
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PayTranIni(B_TranYingSFrec M_YingSFrec)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    B_TranYingSFrec YingSFrec = new B_TranYingSFrec();
                    YingSFrec.UnitID = M_YingSFrec.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = B_Common.OtherInfo.Out;

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
                    this.m_UnitOfWork.GetRepositoryBase<B_TranYingSFrec>().Add(YingSFrec);


                    //2.更新供应商的付款 
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<B_CarFleet>();
                    var Supplier = SupplierService.Get(M_YingSFrec.UnitID);
                    if (M_YingSFrec.FinanceMoney > 0)
                    {
                        Supplier.PaidOut = Supplier.PaidOut + M_YingSFrec.FinanceMoney;
                        Supplier.PaidFavourable = Supplier.PaidFavourable + M_YingSFrec.PayFavourable;
                        Supplier.PaidOwing = Supplier.PaidOwing - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        Supplier.PayMoney = Supplier.PayMoney - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        if (M_YingSFrec.PayType == B_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - M_YingSFrec.FinanceMoney;
                        }
                        SupplierService.Update(Supplier, null);
                    }

                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<B_TranFinanceRecord>().Query().Where(t => t.FinanceType == M_YingSFrec.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    B_TranFinanceRecord FinanceRecord = new B_TranFinanceRecord();
                    FinanceRecord.FinanceType = M_YingSFrec.PayType;
                    FinanceRecord.IsInOrOut = B_Common.OtherInfo.Out;
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
                    FinanceRecord.UseType = B_Common.OtherInfo.PerPayIni; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "期初付款 " + Supplier.FleetName;
                    this.m_UnitOfWork.GetRepositoryBase<B_TranFinanceRecord>().Add(FinanceRecord);
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

        #region 砼款结算


        /// <summary>
        /// 砼款结算(按合同和工地进行划分)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo Balance(List<string> ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //排除已经生成结算单的运输单,为混凝土模式
                    var ShipDocIDs = this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Query().Where(t => ids.Contains(t.ShipDocID) && t.ModelType == ZLERP.Model.Beton.B_Common.ModelType.Beton).Select(t => t.ShipDocID);
                    if (ShipDocIDs != null && ShipDocIDs.Count() > 0)
                    {
                        foreach (var id in ShipDocIDs)
                        {
                            ids.Remove(id);
                        }
                    }
                    if (ids.Count <= 0)
                    {
                        return new ResultInfo() { Result = true, Message = "没有符合要求的运输单" };
                    }
                    //查询出所有运输单进行汇总作业 
                    var query = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(t => ids.Contains(t.ID) && t.IsEffective && t.IsAudit).ToList();

                    //合同和工地
                    var bale = (from q in query
                                group q by new { q.ContractID, q.ProjectID } into g
                                select new
                                {
                                    ContractID = g.Key.ContractID,
                                    ProjectID = g.Key.ProjectID
                                }).ToList();

                    //获取所有工地信息
                    var ProjectIDids = bale.Select(t => t.ProjectID).ToList();
                    var Projects = this.m_UnitOfWork.GetRepositoryBase<Project>().Query().Where(t => ProjectIDids.Contains(t.ID)).ToList();
                    var carids = query.Select(t => t.CarID).Distinct().ToList();
                    //查询出所有的车辆信息
                    var cars = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => carids.Contains(t.ID)).ToList();


                    //查询所有的车辆

                    var pumps = query.Select(t => t.PumpName).Distinct().ToList();

                    var pumpCars = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => carids.Contains(t.ID)).ToList();

                    //混泥土模式下是否计算运费
                    var ComputeTran = this.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(t => t.ConfigName == "IsComputeTran").FirstOrDefault();
                    bool IsComputeTran = false;
                    if (ComputeTran != null)
                    {
                        bool.TryParse(ComputeTran.ConfigValue, out IsComputeTran);
                    }
                    //混凝土结算混凝土模式下是否计算泵送费
                    var ComputePon = this.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(t => t.ConfigName == "IsComputePon").FirstOrDefault();
                    bool IsComputePon = false;
                    if (ComputePon != null)
                    {
                        bool.TryParse(ComputePon.ConfigValue, out IsComputePon);
                    }
                    //获取工地的所有运费计算模板 
                    var B_FareTempletids = Projects.Select(t => t.CarTemplet.ToString()).Distinct().ToList();
                    var B_FareTemplets = this.m_UnitOfWork.GetRepositoryBase<B_FareTemplet>().Query().Where(t => B_FareTempletids.Contains(t.ID)).ToList();

                    //获取模板的所有数据 
                    var B_FareTempletDel1ids = B_FareTemplets.Select(t => Convert.ToInt32(t.ID)).Distinct().ToList();
                    var B_FareTempletDel1s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel1>().Query().Where(t => B_FareTempletDel1ids.Contains(t.FareTempletID)).ToList();

                    //获取所有模板的明细数据 
                    var B_FareTempletDel2ids = B_FareTempletDel1s.Select(t => Convert.ToInt32(t.ID)).Distinct().ToList();
                    var B_FareTempletDel2s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel2>().Query().Where(t => B_FareTempletDel2ids.Contains(t.FareTempletDelID)).ToList();


                    //获取所有合同的明细砼信息 
                    var contratids = query.Select(t => t.ContractID).Distinct().ToList();
                    var contractItems = this.m_UnitOfWork.GetRepositoryBase<ContractItem>().Query().Where(t => contratids.Contains(t.ContractID)).ToList();

                    //获取所有合同明细的调价记录信息

                    var Itemids = contractItems.Select(t => t.ID).Distinct().ToList();
                    var priceSets = this.m_UnitOfWork.GetRepositoryBase<PriceSetting>().Query().Where(t => Itemids.Contains(t.ContractItemsID)).ToList();

                    //获取所有合同的泵送价格信息 
                    var contractPumps = this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Query().Where(t => contratids.Contains(t.ContractID)).ToList();

                    //获取合同的所有特异性数据
                    var IdentitySettings = this.m_UnitOfWork.GetRepositoryBase<IdentitySetting>().Query().Where(t => contratids.Contains(t.ContractID)).ToList();

                    //获取所有运输单的任务单
                    var taskids = query.Select(t => t.TaskID).Distinct().ToList();

                    var produceTasks = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Query().Where(t => taskids.Contains(t.ID)).ToList();


                    foreach (var b in bale)
                    {
                        if (string.IsNullOrWhiteSpace(b.ContractID) || string.IsNullOrWhiteSpace(b.ProjectID))
                        {
                            continue;
                        }

                        // 根据合同和工地查出当前的所有运输单
                        var listDel = query.Where(t => t.ProjectID == b.ProjectID && t.ContractID == b.ContractID).ToList();

                        //计算运费
                        //当前工程的运费模板
                        var pro = Projects.FirstOrDefault(t => t.ID == b.ProjectID);
                        var B_FareTemplet = new B_FareTemplet();
                        if (pro != null)
                        {
                            B_FareTemplet = pro.B_FareTemplet;
                        }
                        else
                        {
                            B_FareTemplet = null;
                        }

                        //新增结算单主体数据
                        B_Balance BaleBalance = new B_Balance();
                        //线程锁加线程休眠保证单号不重复
                        lock (ObjLock)
                        {
                            System.Threading.Thread.Sleep(100);
                            string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                            BaleBalance.BaleNo = string.Format("{0}{1}", "T", orderNo);
                        }
                        BaleBalance.OrderDate = DateTime.Now;
                        BaleBalance.ModelType = ZLERP.Model.Beton.B_Common.ModelType.Beton;
                        BaleBalance.ContractID = b.ContractID;
                        BaleBalance.ThatBaleMan = "";
                        BaleBalance.ProjectID = b.ProjectID;
                        BaleBalance.StartDate = listDel.Min(t => t.ProduceDate);
                        BaleBalance.EndDate = listDel.Max(t => t.ProduceDate);
                        string EndDay = base.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(a => a.ConfigName == "ChangeDay").First().ConfigValue.ToStr();
                        BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).ToString("yyyy-MM");
                        if (Convert.ToDateTime(BaleBalance.EndDate).Day > EndDay.ToInt())
                        {
                            BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).AddMonths(1).ToString("yyyy-MM");
                        }
                        if (B_FareTemplet != null)
                        {
                            BaleBalance.BaleType = Convert.ToInt32(B_FareTemplet.ID);
                        }
                        else
                        {
                            BaleBalance.BaleType = 0;
                        }
                        BaleBalance.IsOnePrice = false;
                        BaleBalance.OnePriceType = B_Common.BalaneType.type3;
                        BaleBalance.IsStockPrice = true;

                        BaleBalance.AuditStatus = 0;
                        BaleBalance.BaleDate = DateTime.Now;
                        BaleBalance.BaleMan = AuthorizationService.CurrentUserName;
                        BaleBalance.Remark = "自动生成的结算单";

                        List<B_BalanceDel> B_BalanceDelList = new List<B_BalanceDel>();
                        decimal AllTCount = 0;
                        decimal AllTMoney = 0;

                        decimal AllBCount = 0;
                        decimal AllBMoney = 0;

                        decimal AllPCount = 0;
                        decimal AllPMoney = 0;

                        decimal AllICount = 0;
                        decimal AllIMoney = 0;

                        decimal AllMoney = 0;
                        foreach (var d in listDel)
                        {
                            B_BalanceDel BaleBalanceDel = new B_BalanceDel();
                            BaleBalanceDel.ShipDocID = d.ID;
                            BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.Beton;
                            #region  计算运费单价
                            if (IsComputeTran)
                            {
                                if (B_FareTemplet != null)
                                {
                                    var car = cars.FirstOrDefault(t => t.ID == d.CarID);
                                    var B_FareTempletDel1 = B_FareTempletDel1s.Where(t => t.FareTempletID == Convert.ToInt32(B_FareTemplet.ID) && t.FareType == car.CarTypeID && t.EffectiveTime <= BaleBalance.OrderDate && t.BalaneType == BaleBalance.OnePriceType).OrderByDescending(t => t.EffectiveTime).FirstOrDefault();
                                    if (B_FareTempletDel1 != null)
                                    {
                                        if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type1)
                                        {
                                            var B_FareTempletDel2 = B_FareTempletDel2s.Where(t => t.FareTempletDelID == Convert.ToInt32(B_FareTempletDel1.ID) && t.UpKilometre <= d.Distance && t.DwonKilometre > d.Distance).FirstOrDefault();
                                            if (B_FareTempletDel2 != null)
                                            {
                                                BaleBalanceDel.TPrice = B_FareTempletDel2.Price;
                                                BaleBalanceDel.AllTMoney = Convert.ToDecimal(d.Distance) * BaleBalanceDel.TPrice;
                                                AllTCount += Convert.ToDecimal(d.Distance);
                                            }
                                            else
                                            {
                                                BaleBalanceDel.TPrice = 0;
                                                BaleBalanceDel.AllTMoney = 0;
                                            }
                                        }
                                        if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type2)
                                        {
                                            BaleBalanceDel.TPrice = B_FareTempletDel1.Price;
                                            BaleBalanceDel.AllTMoney = BaleBalanceDel.TPrice;
                                            AllTCount++;
                                        }
                                        if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type3)
                                        {
                                            if (B_FareTempletDel1.CarBetonNum > 0 && B_FareTempletDel1.CarBetonNum > d.ShippingCube)
                                            {
                                                BaleBalanceDel.CarBetonNum = B_FareTempletDel1.CarBetonNum - d.ShippingCube;
                                            }
                                            BaleBalanceDel.TPrice = B_FareTempletDel1.Price;
                                            BaleBalanceDel.AllTMoney = (d.ShippingCube + BaleBalanceDel.CarBetonNum) * BaleBalanceDel.TPrice;
                                            AllTCount += d.ShippingCube;
                                        }
                                        if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type4)
                                        {
                                            BaleBalanceDel.TPrice = B_FareTempletDel1.Price;
                                            BaleBalanceDel.AllTMoney = d.Weight / 1000 * BaleBalanceDel.TPrice;
                                            AllTCount += d.Weight;
                                        }
                                        AllTMoney += BaleBalanceDel.AllTMoney;
                                    }
                                    else
                                    {
                                        BaleBalanceDel.TPrice = 0;
                                        BaleBalanceDel.AllTMoney = 0;
                                    }
                                }
                            }
                            #endregion
                            #region 计算砼金额，按时间区间进行（有调价按照调价，没有调价按照默认最新单价）
                            var item = contractItems.FirstOrDefault(t => t.ContractID == b.ContractID && t.ConStrength == d.ConStrength);
                            if (item != null)
                            {
                                decimal UnPumpPrice = 0;
                                //查看是否有调价记录
                                var priceset = priceSets.Where(t => t.ContractItemsID == item.ID && t.ChangeTime <= d.ProduceDate).OrderByDescending(t => t.ChangeTime).FirstOrDefault();
                                if (priceset != null)
                                {
                                    UnPumpPrice = Convert.ToDecimal(priceset.UnPumpPrice);
                                }
                                else
                                {
                                    //如果最新单价没有，则拿基准单价进行计算
                                    if (Convert.ToDecimal(item.UnPumpPrice) > 0)
                                    {
                                        UnPumpPrice = Convert.ToDecimal(item.UnPumpPrice);
                                    }
                                    else
                                    {
                                        UnPumpPrice = Convert.ToDecimal(item.TranPrice);
                                    }
                                }

                                BaleBalanceDel.BPrice = UnPumpPrice + Convert.ToDecimal(item.ExMoney);
                                BaleBalanceDel.AllBMoney = d.SignInCube * BaleBalanceDel.BPrice;
                                AllBCount += d.SignInCube;
                                AllBMoney += BaleBalanceDel.AllBMoney;
                            }
                            #endregion
                            #region 计算泵送金额
                            if (IsComputePon)
                            {
                                //var pumpcar = pumpCars.Where(t => t.ID == d.PumpName).FirstOrDefault();
                                //if (pumpcar != null)
                                //{
                                    var pump = contractPumps.Where(t => t.ContractID == d.ContractID && t.PumpType == d.CastMode && t.SetDate <= d.ProduceDate).OrderByDescending(t => t.SetDate).FirstOrDefault();
                                    if (pump != null)
                                    {
                                        BaleBalanceDel.PPrice = Convert.ToDecimal(pump.PumpPrice);
                                        BaleBalanceDel.AllPMoney = d.SignInCube * BaleBalanceDel.PPrice;
                                        AllPCount += d.SignInCube;
                                        AllPMoney += BaleBalanceDel.AllPMoney;
                                    }
                                //}
                            }
                            #endregion
                            #region 计算特异性金额

                            decimal Iprice = 0;

                            //查询出当前运输单的任务单而获取特异性
                            var protask = produceTasks.FirstOrDefault(t => t.ID == d.TaskID);
                            if (protask != null && !string.IsNullOrWhiteSpace(protask.IdentityValue))
                            {
                                var identityValue = protask.IdentityValue.Split(',').ToList();

                                foreach (var v in identityValue)
                                {
                                    string identtystring = v;
                                    var IdentitySetting = IdentitySettings.FirstOrDefault(t => t.IdentityName == identtystring && t.SetDate <= d.ProduceDate);
                                    if (IdentitySetting != null)
                                    {
                                        Iprice = Iprice + Convert.ToDecimal(IdentitySetting.IdentityPrice);
                                    }
                                }

                                BaleBalanceDel.IPrice = Iprice;
                                BaleBalanceDel.AllIMoney = d.SignInCube * BaleBalanceDel.IPrice;
                                AllICount += d.SignInCube;
                                AllIMoney += BaleBalanceDel.AllIMoney;
                            }
                            #endregion
                            BaleBalanceDel.CustBetonfee = d.EmptyFee;
                            BaleBalanceDel.OutTimeFee = d.OverTimeFee;
                            BaleBalanceDel.OtherFee = d.OtherFee;
                            BaleBalanceDel.AllMoney = BaleBalanceDel.AllTMoney + BaleBalanceDel.AllBMoney + BaleBalanceDel.AllPMoney + BaleBalanceDel.AllIMoney +
                            BaleBalanceDel.CustBetonfee +BaleBalanceDel.OutTimeFee +BaleBalanceDel.OtherFee;
                            AllMoney += BaleBalanceDel.AllMoney;
                            B_BalanceDelList.Add(BaleBalanceDel);

                        }
                        BaleBalance.AllBCount = AllBCount;
                        BaleBalance.AllBMoney = AllBMoney;
                        BaleBalance.AllBOkCount = AllBCount;
                        BaleBalance.AllBOkMoney = AllBMoney;

                        BaleBalance.AllICount = AllICount;
                        BaleBalance.AllIMoney = AllIMoney;
                        BaleBalance.AllIOkCount = AllICount;
                        BaleBalance.AllIOkMoney = AllIMoney;

                        BaleBalance.AllTCount = AllTCount;
                        BaleBalance.AllTMoney = AllTMoney;

                        BaleBalance.AllTOkCount = AllTCount;
                        BaleBalance.AllTOkMoney = AllTMoney;

                        BaleBalance.AllPCount = AllPCount;
                        BaleBalance.AllPMoney = AllPMoney;

                        BaleBalance.AllPOkCount = AllPCount;
                        BaleBalance.AllPOkMoney = AllPMoney;

                        BaleBalance.AllOkMoney = AllMoney;
                        BaleBalance.PayMoney = 0;
                        BaleBalance.PayOwing = BaleBalance.AllOkMoney;
                        BaleBalance.PayFavourable = 0;
                        var m_bale = this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Add(BaleBalance);

                        foreach (var BaleBalanceDel in B_BalanceDelList)
                        {
                            BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Add(BaleBalanceDel);
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
        /// 泵送费结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo PotonBalance(List<string> ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //排除已经生成结算单的运输单,为混凝土模式
                    var ShipDocIDs = this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Query().Where(t => ids.Contains(t.ShipDocID) && t.ModelType == ZLERP.Model.Beton.B_Common.ModelType.PeSon).Select(t => t.ShipDocID);
                    if (ShipDocIDs != null && ShipDocIDs.Count() > 0)
                    {
                        foreach (var id in ShipDocIDs)
                        {
                            ids.Remove(id);
                        }
                    }
                    if (ids.Count <= 0)
                    {
                        return new ResultInfo() { Result = true, Message = "没有符合要求的运输单" };
                    }
                    //查询出所有运输单进行汇总作业 
                    var query = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Query().Where(t => ids.Contains(t.ID) && t.IsEffective && t.CastMode != "" && t.IsAudit).ToList();

                    //合同和工地
                    var bale = (from q in query
                                group q by new { q.ContractID, q.ProjectID, q.PumpName } into g
                                select new
                                {
                                    ContractID = g.Key.ContractID,
                                    ProjectID = g.Key.ProjectID,
                                    PumpName = g.Key.PumpName
                                }).ToList();

                    //获取所有工地信息
                    //var ProjectIDids = bale.Select(t => t.ProjectID).ToList();
                    //var Projects = this.m_UnitOfWork.GetRepositoryBase<Project>().Query().Where(t => ProjectIDids.Contains(t.ID)).ToList(); 
                    ////获取所有合同的明细砼信息 
                    var contratids = query.Select(t => t.ContractID).Distinct().ToList();

                    //获取所有合同的泵送价格信息 
                    var contractPumps = this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Query().Where(t => contratids.Contains(t.ContractID)).ToList();

                    //查询所有的车辆

                    var carids = query.Select(t => t.PumpName).Distinct().ToList();

                    var cars = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => carids.Contains(t.ID)).ToList();
                    bale = bale.Where(a => string.IsNullOrWhiteSpace(a.PumpName) && string.IsNullOrWhiteSpace(a.ProjectID) && string.IsNullOrWhiteSpace(a.ContractID)).ToList();
                    foreach (var b in bale)
                    {
                        if (string.IsNullOrWhiteSpace(b.ContractID) || string.IsNullOrWhiteSpace(b.ProjectID) || string.IsNullOrWhiteSpace(b.PumpName))
                        {
                            continue;
                        }

                        // 根据合同和工地查出当前的所有运输单
                        var listDel = query.Where(t => t.ProjectID == b.ProjectID && t.ContractID == b.ContractID && t.PumpName == b.PumpName).ToList();
                        //新增结算单主体数据
                        B_Balance BaleBalance = new B_Balance();
                        //线程锁加线程休眠保证单号不重复
                        lock (ObjLock)
                        {
                            System.Threading.Thread.Sleep(100);
                            string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                            BaleBalance.BaleNo = string.Format("{0}{1}", "T", orderNo);
                        }
                        BaleBalance.OrderDate = DateTime.Now;
                        BaleBalance.ModelType = ZLERP.Model.Beton.B_Common.ModelType.PeSon;
                        BaleBalance.ContractID = b.ContractID;
                        BaleBalance.ThatBaleMan = "";
                        BaleBalance.ProjectID = b.ProjectID;
                        BaleBalance.CastMode = b.PumpName;
                        BaleBalance.StartDate = listDel.Min(t => t.ProduceDate);
                        BaleBalance.EndDate = listDel.Max(t => t.ProduceDate);
                        string EndDay = base.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(a => a.ConfigName == "ChangeDay").First().ConfigValue.ToStr();
                        BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).ToString("yyyy-MM");
                        if (Convert.ToDateTime(BaleBalance.EndDate).Day > EndDay.ToInt())
                        {
                            BaleBalance.InMonth = Convert.ToDateTime(BaleBalance.EndDate).AddMonths(1).ToString("yyyy-MM");
                        }
                        BaleBalance.IsOnePrice = false;
                        BaleBalance.OnePriceType = "";
                        BaleBalance.IsStockPrice = true;
                        BaleBalance.OnePrice = 0;
                        BaleBalance.AuditStatus = 0;
                        BaleBalance.BaleDate = DateTime.Now;
                        BaleBalance.BaleMan = AuthorizationService.CurrentUserName;
                        BaleBalance.Remark = "自动生成的结算单";

                        List<B_BalanceDel> B_BalanceDelList = new List<B_BalanceDel>();

                        decimal AllPCount = 0;
                        decimal AllPMoney = 0;
                        foreach (var d in listDel)
                        {
                            B_BalanceDel BaleBalanceDel = new B_BalanceDel();
                            BaleBalanceDel.ShipDocID = d.ID;
                            BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.PeSon;


                            #region 计算泵送金额
                            //根据泵名称查出车辆类型 
                            //var car = cars.Where(t => t.ID == d.PumpName).FirstOrDefault();
                            //if (car != null)
                            //{
                                var pump = contractPumps.Where(t => t.ContractID == d.ContractID && t.PumpType == d.CastMode && t.SetDate <= d.ProduceDate).OrderByDescending(t => t.SetDate).FirstOrDefault();
                                if (pump != null)
                                {
                                    BaleBalanceDel.PPrice = Convert.ToDecimal(pump.PumpPrice);
                                    BaleBalanceDel.AllPMoney = d.SignInCube * BaleBalanceDel.PPrice;
                                    AllPMoney += BaleBalanceDel.AllPMoney;
                                    AllPCount += d.SignInCube;
                                }
                            //}
                            #endregion
                            BaleBalanceDel.AllMoney = BaleBalanceDel.AllPMoney + BaleBalanceDel.OtherFee;
                            B_BalanceDelList.Add(BaleBalanceDel);

                        }
                        BaleBalance.AllPCount = AllPCount;
                        BaleBalance.AllPMoney = AllPMoney;
                        BaleBalance.AllPOkCount = AllPCount;
                        BaleBalance.AllPOkMoney = AllPMoney;

                        BaleBalance.AllOkMoney = AllPMoney;
                        BaleBalance.PayMoney = 0;
                        BaleBalance.PayOwing = BaleBalance.AllOkMoney;
                        BaleBalance.PayFavourable = 0;
                        var m_bale = this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Add(BaleBalance);

                        foreach (var BaleBalanceDel in B_BalanceDelList)
                        {
                            BaleBalanceDel.BaleBalanceID = Convert.ToInt32(m_bale.ID);
                            this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Add(BaleBalanceDel);
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
        ///重新计算砼款结算单，包括更新砼款的价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo ComputePonBeton(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var bale = this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Get(id);
                    var BaleBalanceID = Convert.ToInt32(id);
                    var M_Del = this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Query().Where(t => t.BaleBalanceID == BaleBalanceID).ToList();


                    //获取所有有关的运费设定信息 
                    decimal AllPCount = 0;
                    decimal AllPMoney = 0;
                    #region  计算砼明细

                    foreach (var del in M_Del)
                    {

                        var shippingDocument = del.ShippingDocument;
                        if (shippingDocument == null)
                        {
                            shippingDocument = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(del.ShipDocID);
                        }
                        if (bale.IsStockPrice)
                        {
                            #region 计算泵送金额
                            //var car = this.m_UnitOfWork.GetRepositoryBase<Car>().Query().Where(t => t.ID == shippingDocument.PumpName).FirstOrDefault();
                            //if (car != null)
                            //{
                            var pump = this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Query().Where(t => t.ContractID == bale.ContractID && t.PumpType == shippingDocument.CastMode && t.SetDate <= shippingDocument.ProduceDate).OrderByDescending(t => t.SetDate).FirstOrDefault();
                                if (pump != null)
                                {
                                    del.PPrice = Convert.ToDecimal(pump.PumpPrice);
                                    del.AllPMoney = shippingDocument.SignInCube * del.PPrice;
                                    AllPCount += shippingDocument.SignInCube;
                                }
                            //}
                            #endregion
                        }
                        if (bale.IsOnePrice)
                        {
                            del.PPrice = bale.OnePrice;
                            del.AllPMoney = shippingDocument.SignInCube * del.PPrice;
                        }
                        del.AllMoney = del.AllPMoney + del.OtherFee;
                        AllPMoney += del.AllMoney;
                        this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Update(del, null);
                    }


                    #endregion


                    bale.AllPCount = AllPCount;
                    bale.AllPMoney = AllPMoney;
                    bale.AllPOkCount = AllPCount;
                    bale.AllPOkMoney = AllPMoney;

                    bale.AllOkMoney = AllPMoney + Convert.ToDecimal(bale.OtherMoney);
                    bale.PayMoney = 0;
                    bale.PayOwing = bale.AllOkMoney;
                    bale.PayFavourable = 0;
                    bale.Modifier = AuthorizationService.CurrentUserName;
                    bale.ModifyTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Update(bale, null);

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
        ///重新计算砼款结算单，包括更新砼款的价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo ComputeBeton(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var bale = this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Get(id);
                    var BaleBalanceID = Convert.ToInt32(id);
                    var M_Del = this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Query().Where(t => t.BaleBalanceID == BaleBalanceID).ToList();

                    //获取当前结算单的合同明细
                    var ContractItems = this.m_UnitOfWork.GetRepositoryBase<ContractItem>().Query().Where(t => t.ContractID == bale.ContractID).ToList();

                    //获取当前合同的所有调价信息
                    var itemids = ContractItems.Select(t => t.ID).ToList();
                    var pricesets = this.m_UnitOfWork.GetRepositoryBase<PriceSetting>().Query().Where(t => itemids.Contains(t.ContractItemsID)).ToList();

                    //获取车队的所有运费计算模板  
                    var B_FareTemplets = this.m_UnitOfWork.GetRepositoryBase<B_FareTemplet>().Query().Where(t => t.ID == bale.BaleType.ToString());

                    //获取模板的所有数据 
                    var B_FareTempletDel1ids = B_FareTemplets.Select(t => Convert.ToInt32(t.ID)).ToList();
                    var B_FareTempletDel1s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel1>().Query().Where(t => B_FareTempletDel1ids.Contains(t.FareTempletID));

                    //获取所有模板的明细数据 
                    var B_FareTempletDel2ids = B_FareTempletDel1s.Select(t => Convert.ToInt32(t.ID)).ToList();
                    var B_FareTempletDel2s = this.m_UnitOfWork.GetRepositoryBase<B_FareTempletDel2>().Query().Where(t => B_FareTempletDel2ids.Contains(t.FareTempletDelID));

                    //获取所有有关的运费设定信息
                    decimal AllTCount = 0;
                    decimal AllTMoney = 0;

                    decimal AllBCount = 0;
                    decimal AllBMoney = 0;

                    decimal AllPCount = 0;
                    decimal AllPMoney = 0;

                    decimal AllICount = 0;
                    decimal AllIMoney = 0;

                    decimal AllMoney = 0;
                    #region  计算砼明细
                    //混泥土模式下是否计算运费
                    var ComputeTran = this.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(t => t.ConfigName == "IsComputeTran").FirstOrDefault();
                    bool IsComputeTran = false;
                    if (ComputeTran != null)
                    {
                        bool.TryParse(ComputeTran.ConfigValue, out IsComputeTran);
                    }
                    //混凝土结算混凝土模式下是否计算泵送费
                    var ComputePon = this.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(t => t.ConfigName == "IsComputePon").FirstOrDefault();
                    bool IsComputePon = false;
                    if (ComputePon != null)
                    {
                        bool.TryParse(ComputePon.ConfigValue, out IsComputePon);
                    }

                    decimal CustBetonfee = 0;
                    foreach (var del in M_Del)
                    {
                        CustBetonfee += del.CustBetonfee;
                        var shippingDocument = del.ShippingDocument;
                        if (shippingDocument == null)
                        {
                            shippingDocument = this.m_UnitOfWork.GetRepositoryBase<ShippingDocument>().Get(del.ShipDocID);
                        }
                        #region  如果是运费设定结算
                        if (IsComputeTran)
                        {
                            if (bale.IsStockPrice && B_FareTemplets != null)
                            {
                                var car = new Car();
                                var carid = shippingDocument.CarID;
                                if (car == null || carid != car.ID)
                                {
                                    car = this.m_UnitOfWork.GetRepositoryBase<Car>().Get(carid);
                                }
                                var B_FareTempletDel1 = B_FareTempletDel1s.Where(t => t.FareTempletID == bale.BaleType && t.FareType == car.CarTypeID && t.EffectiveTime <= bale.OrderDate && t.BalaneType == bale.OnePriceType).OrderByDescending(t => t.EffectiveTime).FirstOrDefault();
                                if (B_FareTempletDel1 != null)
                                {
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type1)
                                    {
                                        var B_FareTempletDel2 = B_FareTempletDel2s.Where(t => t.FareTempletDelID == Convert.ToInt32(B_FareTempletDel1.ID) && t.UpKilometre <= shippingDocument.Distance && t.DwonKilometre > shippingDocument.Distance).FirstOrDefault();
                                        if (B_FareTempletDel2 != null)
                                        {
                                            del.TPrice = B_FareTempletDel2.Price;
                                            del.AllTMoney = Convert.ToDecimal(shippingDocument.Distance) * del.TPrice;
                                            AllTCount += Convert.ToDecimal(shippingDocument.Distance);
                                        }
                                        else
                                        {
                                            del.TPrice = 0;
                                            del.AllTMoney = 0;
                                        }
                                    }
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type2)
                                    {
                                        del.TPrice = B_FareTempletDel1.Price;
                                        del.AllTMoney = del.TPrice;
                                        AllTCount++;
                                    }
                                    if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type3)
                                    {
                                        del.TPrice = B_FareTempletDel1.Price;
                                        if (del.CarBetonNum == 0)
                                        {
                                            if (B_FareTempletDel1.CarBetonNum > 0 && B_FareTempletDel1.CarBetonNum > shippingDocument.ShippingCube)
                                            {
                                                del.CarBetonNum = B_FareTempletDel1.CarBetonNum - shippingDocument.ShippingCube;
                                            }
                                        }
                                        del.AllTMoney = (shippingDocument.ShippingCube + del.CarBetonNum) * del.TPrice;
                                        if (del.CarBetonNum < 0)
                                        {
                                            del.AllMoney = (shippingDocument.SignInCube) * del.TPrice;
                                        }
                                        AllTCount += shippingDocument.ShippingCube;

                                        if (B_FareTempletDel1.BalaneType == B_Common.BalaneType.type4)
                                        {
                                            del.TPrice = B_FareTempletDel1.Price;
                                            del.AllTMoney = shippingDocument.Weight / 1000 * del.TPrice;
                                            AllTCount += shippingDocument.Weight;
                                        }
                                        del.AllTMoney = del.AllTMoney + del.CustBetonfee;
                                        AllTMoney += del.AllTMoney;

                                    }
                                }
                                else
                                {
                                    del.TPrice = 0;
                                    del.AllTMoney = del.CustBetonfee;
                                }
                                if (bale.IsOnePrice)
                                {
                                    if (bale.OnePriceType == B_Common.BalaneType.type1)
                                    {
                                        del.TPrice = bale.OnePrice;
                                        del.AllTMoney = bale.OnePrice * Convert.ToDecimal(shippingDocument.Distance);
                                        AllTCount += Convert.ToDecimal(shippingDocument.Distance);
                                    }
                                    if (bale.OnePriceType == B_Common.BalaneType.type2)
                                    {
                                        del.TPrice = bale.OnePrice;
                                        del.AllTMoney = bale.OnePrice;
                                        AllTCount++;
                                    }
                                    if (bale.OnePriceType == B_Common.BalaneType.type3)
                                    {
                                        del.TPrice = bale.OnePrice;
                                        if (del.CarBetonNum == 0)
                                        {
                                            if (B_FareTempletDel1.CarBetonNum > 0 && B_FareTempletDel1.CarBetonNum > shippingDocument.ShippingCube)
                                            {
                                                del.CarBetonNum = B_FareTempletDel1.CarBetonNum - shippingDocument.ShippingCube;
                                            }
                                        }
                                        del.AllTMoney = bale.OnePrice * Convert.ToDecimal(shippingDocument.ShippingCube + del.CarBetonNum);
                                        if (del.CarBetonNum < 0)
                                        {
                                            del.AllMoney = (shippingDocument.SignInCube) * bale.OnePrice;
                                        }


                                        AllTCount += shippingDocument.ShippingCube;
                                    }
                                    if (bale.OnePriceType == B_Common.BalaneType.type4)
                                    {
                                        del.TPrice = bale.OnePrice;
                                        del.AllTMoney = bale.OnePrice * shippingDocument.Weight / 1000;
                                        AllTCount += shippingDocument.Weight / 1000;
                                    }
                                    del.AllTMoney = del.AllTMoney + del.CustBetonfee;
                                    AllTMoney += del.AllTMoney;

                                }
                            }
                        }
                        #endregion
                        #region 计算砼金额
                        var item = ContractItems.FirstOrDefault(t => t.ContractID == bale.ContractID && t.ConStrength == shippingDocument.ConStrength);
                        if (item != null)
                        {
                            decimal UnPumpPrice = 0;
                            //查看是否有调价记录
                            var price = pricesets.Where(t => t.ContractItemsID == item.ID && t.ChangeTime < shippingDocument.ProduceDate).OrderByDescending(t => t.ChangeTime).FirstOrDefault();
                            if (price != null)
                            {
                                UnPumpPrice = Convert.ToDecimal(price.UnPumpPrice);
                            }
                            else
                            {
                                //如果最新单价没有，则拿基准单价进行计算
                                if (Convert.ToDecimal(item.UnPumpPrice) > 0)
                                {
                                    UnPumpPrice = Convert.ToDecimal(item.UnPumpPrice);
                                }
                                else
                                {
                                    UnPumpPrice = Convert.ToDecimal(item.TranPrice);
                                }
                            }

                            del.BPrice = UnPumpPrice + Convert.ToDecimal(item.ExMoney);
                            del.AllBMoney = (shippingDocument.SignInCube + del.CustBetonNum) * del.BPrice;
                            AllBCount += shippingDocument.SignInCube + del.CustBetonNum;
                            AllBMoney += del.AllBMoney;
                        }
                        #endregion
                        #region 计算泵送费
                        if (IsComputePon)
                        {
                                var pump = this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Query().Where(t => t.ContractID == bale.ContractID && t.PumpType == shippingDocument.CastMode && t.SetDate <= shippingDocument.ProduceDate).OrderByDescending(t => t.SetDate).FirstOrDefault();
                                if (pump != null)
                                {
                                    del.PPrice = Convert.ToDecimal(pump.PumpPrice);
                                    del.AllPMoney = shippingDocument.SignInCube * del.PPrice;
                                    AllPCount += shippingDocument.SignInCube;
                                }
                                AllPMoney += del.AllPMoney;
                        }
                        #endregion

                        #region 计算特异性金额

                        decimal Iprice = 0;


                        //获取合同的所有特异性数据
                        var IdentitySettings = this.m_UnitOfWork.GetRepositoryBase<IdentitySetting>().Query().Where(t => t.ContractID == bale.ContractID).ToList();

                        //查询出当前运输单的任务单而获取特异性
                        var protask = shippingDocument.ProduceTask;
                        if (protask == null)
                        {
                            protask = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(shippingDocument.TaskID);
                        }
                        if (protask != null && !string.IsNullOrWhiteSpace(protask.IdentityValue))
                        {
                            var identityValue = protask.IdentityValue.Split(',').ToList();

                            foreach (var v in identityValue)
                            {
                                var identtystring =v;
                                var IdentitySetting = IdentitySettings.FirstOrDefault(t => t.IdentityName == identtystring && t.SetDate <= shippingDocument.ProduceDate);
                                if (IdentitySetting != null)
                                {
                                    Iprice = Iprice + Convert.ToDecimal(IdentitySetting.IdentityPrice);
                                }
                            }

                            del.IPrice = Iprice;
                            del.AllIMoney = shippingDocument.SignInCube * del.IPrice;
                            AllICount += shippingDocument.SignInCube;
                            AllIMoney += del.AllIMoney;
                        }
                        else
                        {
                            del.IPrice = 0;
                            del.AllIMoney = 0;
                        }
                        #endregion
                        del.AllMoney = del.AllBMoney + del.AllIMoney + del.AllTMoney + del.AllPMoney + del.OtherFee + del.OutTimeFee + del.OutDistanceFee;
                        AllMoney += del.AllMoney;
                        this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>().Update(del, null);
                    }


                    #endregion

                    AllTMoney += CustBetonfee;
                    AllMoney += CustBetonfee;

                    bale.AllBCount = AllBCount;
                    bale.AllBMoney = AllBMoney;

                    bale.AllBOkCount = AllBCount;
                    bale.AllBOkMoney = AllBMoney;

                    bale.AllICount = AllICount;
                    bale.AllIMoney = AllIMoney;
                    bale.AllIOkCount = AllICount;
                    bale.AllIOkMoney = AllIMoney;

                    bale.AllTCount = AllTCount;
                    bale.AllTMoney = AllTMoney;
                    bale.AllTOkCount = AllTCount;
                    bale.AllTOkMoney = AllTMoney;

                    bale.AllPCount = AllPCount;
                    bale.AllPMoney = AllPMoney;
                    bale.AllPOkCount = AllPCount;
                    bale.AllPOkMoney = AllPMoney;

                    bale.AllOkMoney = AllMoney + Convert.ToDecimal(bale.OtherMoney);
                    bale.PayMoney = 0;
                    bale.PayOwing = bale.AllOkMoney;
                    bale.PayFavourable = 0;
                    bale.Modifier = AuthorizationService.CurrentUserName;
                    bale.ModifyTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Update(bale, null);

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
        /// 对付款货款应付款付款
        /// </summary>
        /// <param name="B_PerPay"></param>
        /// <param name="B_PerPays"></param>
        /// <returns></returns>
        public ResultInfo Pay(B_PerPay B_PerPay, List<b_perpay> b_perpays, string B_FinanceId = "")
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
                    //1.插入应收应付的明细
                    B_YingSFrec YingSFrec = new B_YingSFrec();
                    YingSFrec.UnitID = B_PerPay.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.Beton;
                    YingSFrec.YingSF = B_Common.OtherInfo.In;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.FinanceDate = B_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType = B_PerPay.PayType;
                    YingSFrec.Payer = B_PerPay.Payer;
                    YingSFrec.Gatheringer = B_PerPay.Gatheringer;
                    YingSFrec.Remark = B_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_YingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<B_YingSFrecdetail>();
                    var BaleBalanceService = this.m_UnitOfWork.GetRepositoryBase<B_Balance>();
                    //插入记录明细 
                    foreach (var p in b_perpays)
                    {
                        B_YingSFrecdetail YingSFrecetail = new B_YingSFrecdetail();
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

                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<Contract>();
                    var Supplier = SupplierService.Get(B_PerPay.UnitID);
                    if (payall > 0)
                    {
                        Supplier.PayMoney = Supplier.PayMoney - payall - payper;
                        if (B_PerPay.PayType == B_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - payall;
                        }
                        SupplierService.Update(Supplier, null);
                    }
                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<B_FinanceRecord>().Query().Where(t => t.FinanceType == B_PerPay.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    B_FinanceRecord FinanceRecord = new B_FinanceRecord();
                    FinanceRecord.FinanceType = B_PerPay.PayType;
                    FinanceRecord.IsInOrOut = B_Common.OtherInfo.In;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = payall;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance + payall;
                    }
                    else
                    {
                        FinanceRecord.Balance = payall;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = B_Common.OtherInfo.Beton;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "砼款付款-" + Supplier.ContractName;
                    this.m_UnitOfWork.GetRepositoryBase<B_FinanceRecord>().Add(FinanceRecord);

                    UpdateB_Finance(B_FinanceId);

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
        /// 对砼款期初付款对期初付款
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PayIni(B_YingSFrec M_YingSFrec, string B_FinanceId = "")
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    B_YingSFrec YingSFrec = new B_YingSFrec();
                    YingSFrec.UnitID = M_YingSFrec.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = B_Common.OtherInfo.In;

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
                    this.m_UnitOfWork.GetRepositoryBase<B_YingSFrec>().Add(YingSFrec);


                    //2.更新供应商的付款 
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<Contract>();
                    var Supplier = SupplierService.Get(M_YingSFrec.UnitID);
                    if (M_YingSFrec.FinanceMoney > 0)
                    {
                        Supplier.PaidOut = Supplier.PaidOut + M_YingSFrec.FinanceMoney;
                        Supplier.PaidFavourable = Supplier.PaidFavourable + M_YingSFrec.PayFavourable;
                        Supplier.PaidOwing = Supplier.PaidOwing - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        Supplier.PayMoney = Supplier.PayMoney - M_YingSFrec.FinanceMoney - M_YingSFrec.PayFavourable;
                        if (M_YingSFrec.PayType == B_Common.PayType.type)
                        {
                            Supplier.PrePay = Supplier.PrePay - M_YingSFrec.FinanceMoney;
                        }
                        SupplierService.Update(Supplier, null);
                    }

                    //3.插入资产记录的总金额 
                    var fin = this.m_UnitOfWork.GetRepositoryBase<B_FinanceRecord>().Query().Where(t => t.FinanceType == M_YingSFrec.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    B_FinanceRecord FinanceRecord = new B_FinanceRecord();
                    FinanceRecord.FinanceType = M_YingSFrec.PayType;
                    FinanceRecord.IsInOrOut = B_Common.OtherInfo.In;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = M_YingSFrec.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance + M_YingSFrec.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = M_YingSFrec.FinanceMoney;
                    }
                    FinanceRecord.Operater = AuthorizationService.CurrentUserName;
                    FinanceRecord.UseType = B_Common.OtherInfo.PerPayIni; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "期初付款 " + Supplier.ContractName;
                    this.m_UnitOfWork.GetRepositoryBase<B_FinanceRecord>().Add(FinanceRecord);

                    UpdateB_Finance(B_FinanceId);

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
        /// 预付款
        /// </summary>
        /// <param name="yingsf"></param>
        /// <returns></returns>
        public ResultInfo PerPay(B_YingSFrec yingsf, string B_FinanceId = "")
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    B_YingSFrec YingSFrec = new B_YingSFrec();
                    YingSFrec.UnitID = yingsf.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.PerPay;
                    YingSFrec.YingSF = B_Common.OtherInfo.In;
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
                    this.m_UnitOfWork.GetRepositoryBase<B_YingSFrec>().Add(YingSFrec);


                    //2.更新供应商的预付款
                    var fin = this.m_UnitOfWork.GetRepositoryBase<B_FinanceRecord>().Query().Where(t => t.FinanceType == yingsf.PayType).OrderByDescending(t => t.ID).FirstOrDefault();
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<Contract>();
                    var Supplier = SupplierService.Get(yingsf.UnitID.ToString());


                    B_FinanceRecord FinanceRecord = new B_FinanceRecord();
                    FinanceRecord.FinanceType = yingsf.PayType;
                    FinanceRecord.IsInOrOut = B_Common.OtherInfo.In;
                    FinanceRecord.FinanceDate = DateTime.Now;
                    FinanceRecord.FinanceMoney = yingsf.FinanceMoney;
                    if (fin != null)
                    {
                        FinanceRecord.Balance = fin.Balance + yingsf.FinanceMoney;
                    }
                    else
                    {
                        FinanceRecord.Balance = yingsf.FinanceMoney;
                    }
                    FinanceRecord.Operater = yingsf.Builder;
                    FinanceRecord.UseType = B_Common.OtherInfo.PerPay; ;
                    FinanceRecord.FinanceNo = Convert.ToInt16(YingSFrec.ID);
                    FinanceRecord.Remark = "预付 " + Supplier.ContractName;
                    this.m_UnitOfWork.GetRepositoryBase<B_FinanceRecord>().Add(FinanceRecord);

                    //3.插入资产记录的总金额 
                    if (yingsf.FinanceMoney != 0)
                    {
                        Supplier.PrePay = Supplier.PrePay + yingsf.FinanceMoney;
                        SupplierService.Update(Supplier, null);
                    }

                    UpdateB_Finance(B_FinanceId);
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
        /// 删除结算单的时候，必须同步删除明细删除结算单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo DeleteBalance(string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var service = this.m_UnitOfWork.GetRepositoryBase<B_Balance>();
                    var serviceDel = this.m_UnitOfWork.GetRepositoryBase<B_BalanceDel>();
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
        public ResultInfo PiaoPay(B_PerPay B_PerPay, List<b_perpay> b_perpays, string B_FinanceId = "")
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
                    B_PiaoYingSFrec YingSFrec = new B_PiaoYingSFrec();
                    YingSFrec.UnitID = B_PerPay.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.PiaoBeton;
                    YingSFrec.YingSF = B_Common.OtherInfo.In;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;
                    YingSFrec.PiaoNo = B_PerPay.PiaoNo;
                    YingSFrec.FinanceDate = B_PerPay.InDate;
                    YingSFrec.FinanceMoney = payall;
                    YingSFrec.PayFavourable = payper;
                    YingSFrec.PayType = B_PerPay.PayType;
                    YingSFrec.Payer = B_PerPay.Payer;
                    YingSFrec.Gatheringer = B_PerPay.Gatheringer;
                    YingSFrec.Remark = B_PerPay.Remark2;
                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_PiaoYingSFrec>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<B_PiaoYingSFrecdetail>();
                    var BaleBalanceService = this.m_UnitOfWork.GetRepositoryBase<B_Balance>();
                    //插入记录明细 
                    foreach (var p in b_perpays)
                    {
                        B_PiaoYingSFrecdetail YingSFrecetail = new B_PiaoYingSFrecdetail();
                        YingSFrecetail.FinanceNo = Convert.ToInt32(YingSFrec.ID);
                        YingSFrecetail.OrderNo = Convert.ToInt32(p.ID);
                        YingSFrecetail.PayMoney = p.PayMoney2;
                        YingSFrecetail.PayFavourable = p.PayFavourable2;
                        YingSFrecdetailService.Add(YingSFrecetail);

                        //更新结算单的付款情况
                        var piaoin = BaleBalanceService.Get(p.ID.ToString());
                        piaoin.PiaoPayMoney = piaoin.PiaoPayMoney + p.PayMoney2;
                        piaoin.PiaoPayFavourable = piaoin.PayFavourable + p.PayFavourable2;
                        piaoin.PiaoPayOwing = piaoin.PiaoPayOwing - p.PayMoney2 - p.PayFavourable2;
                        BaleBalanceService.Update(piaoin, null);
                    }

                    UpdateB_Finance(B_FinanceId);
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



        /// <summary>
        /// 对砼款期初付款对期初付款
        /// </summary>
        /// <param name="M_YingSFrec"></param>
        /// <returns></returns>
        public ResultInfo PiaoPayIni(B_PiaoYingSFrec M_YingSFrec)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1.插入应收应付的明细
                    B_PiaoYingSFrec YingSFrec = new B_PiaoYingSFrec();
                    YingSFrec.UnitID = M_YingSFrec.UnitID;
                    YingSFrec.Source = B_Common.OtherInfo.PerPayIni;
                    YingSFrec.YingSF = B_Common.OtherInfo.In;

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
                    this.m_UnitOfWork.GetRepositoryBase<B_PiaoYingSFrec>().Add(YingSFrec);


                    //2.更新合同的开票 
                    var SupplierService = this.m_UnitOfWork.GetRepositoryBase<Contract>();
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

        #region 财务核准流程

        public ResultInfo AuditExec(B_PerPay B_PerPay, List<b_perpay> b_perpays, string Modeltype)
        {

            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var FinanceMoney = b_perpays.Sum(t => t.PayMoney2); //付款总金额
                    var PayFavourable = b_perpays.Sum(t => t.PayFavourable2); //优惠总金额
                    //1.插入应收应付的明细
                    B_Finance YingSFrec = new B_Finance();

                    YingSFrec.Modeltype = Modeltype;
                    YingSFrec.FinanceDate = B_PerPay.InDate;
                    YingSFrec.UnitID = B_PerPay.UnitID;

                    string UnitName = string.Empty;
                    if (Modeltype == B_Common.ExecModeltype.Beton || Modeltype == B_Common.ExecModeltype.PiaoBeton || Modeltype == B_Common.ExecModeltype.PayPer || Modeltype == B_Common.ExecModeltype.IniPay)
                    {
                        var contract = this.m_UnitOfWork.GetRepositoryBase<Contract>().Get(B_PerPay.UnitID);
                        if (contract != null)
                        {
                            UnitName = contract.ContractName;
                        }
                        if (Modeltype == B_Common.ExecModeltype.PayPer || Modeltype == B_Common.ExecModeltype.IniPay)
                        {
                            //清空明细
                            b_perpays= new List<b_perpay>();
                        }
                    }

                    YingSFrec.UnitName = UnitName;
                    YingSFrec.PayType = B_PerPay.PayType;
                    YingSFrec.Payer = B_PerPay.Payer;
                    YingSFrec.Gatheringer = B_PerPay.Gatheringer;
                    YingSFrec.FinanceMoney = FinanceMoney;
                    YingSFrec.PayFavourable = PayFavourable;

                    Random ran = new Random();
                    string orderno = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
                    YingSFrec.FinanceNo = orderno;

                    YingSFrec.Remark = B_PerPay.Remark2;

                    YingSFrec.Builder = AuthorizationService.CurrentUserName;
                    YingSFrec.BuildTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_Finance>().Add(YingSFrec);

                    var YingSFrecdetailService = this.m_UnitOfWork.GetRepositoryBase<B_FinanceDel>();

                    //插入记录明细 
                    foreach (var p in b_perpays)
                    {
                        B_FinanceDel YingSFrecetail = new B_FinanceDel();
                        YingSFrecetail.FinanceID = Convert.ToInt32(YingSFrec.ID);

                        YingSFrecetail.BaleID = Convert.ToInt32(p.ID);
                        if (Modeltype == B_Common.ExecModeltype.Beton || Modeltype == B_Common.ExecModeltype.PiaoBeton)
                        {
                            var Bale = this.m_UnitOfWork.GetRepositoryBase<B_Balance>().Get(YingSFrecetail.BaleID.ToString());
                            YingSFrecetail.BaleNo = Bale.BaleNo;
                            YingSFrecetail.UnitID = Bale.ContractID;
                            YingSFrecetail.UnitName = Bale.Contract.ContractName;
                            YingSFrecetail.NextID = Bale.ProjectID;
                            YingSFrecetail.NextName = Bale.Project.ProjectName;
                            YingSFrecetail.InMonth = Bale.InMonth;
                            YingSFrecetail.OrderDate = Bale.OrderDate;
                            YingSFrecetail.AllOkMoney = Bale.AllOkMoney;


                        }
                        YingSFrecetail.PayMoney = p.PayMoney2;
                        YingSFrecetail.PayFavourable = p.PayFavourable2;
                        YingSFrecdetailService.Add(YingSFrecetail);
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
        /// 财务核准流程
        /// </summary>
        /// <param name="B_FinanceId"></param>
        private void UpdateB_Finance(string B_FinanceId)
        {
            //更新核准对象 
            if (!string.IsNullOrWhiteSpace(B_FinanceId))
            {
                var B_Finance = this.m_UnitOfWork.GetRepositoryBase<B_Finance>().Get(B_FinanceId);
                if (B_Finance != null)
                {
                    B_Finance.AuditStatus = 1;
                    B_Finance.Auditor = AuthorizationService.CurrentUserName;
                    B_Finance.AuditTime = DateTime.Now;
                    this.m_UnitOfWork.GetRepositoryBase<B_Finance>().Update(B_Finance);
                }
            }
        }
        #endregion
    }
}
