using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.Model.Enums;
using ZLERP.Resources;

namespace ZLERP.Business
{
    public class TZRalationGHService : ServiceBase<TZRalationGH>
    {
        PublicService s;
        internal TZRalationGHService(IUnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// 增加转退料记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TZRalationGH Add(TZRalationGH entity)
        {
            DateTime ts = DateTime.Now;
            entity.ActionTime = ts;
            entity.ActionCube = entity.Cube;
            entity.IsLock = "0";
            entity.TzRalationFlag = getZtlDh();

            TZRalationGH z = base.Add(entity);

            //AddHistory(entity, "add", entity.CarID, entity.Cube);

            return z;
        }
        /// <summary>
        /// 生产转退料单号
        /// </summary>
        /// <returns></returns>
        private string getZtlDh()
        {
            return "TZ" + DateTime.Now.ToString("yyMMddHHmmss");
        }
       

        /// <summary>
        /// 审核退料转发记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Audit(int id)
        {
            TZRalationGH entity = this.Get(id);
            if (entity != null)
            {
                if (entity.IsCompleted && !entity.IsAudit)
                {
                    entity.IsAudit = true;
                    entity.Auditor = AuthorizationService.CurrentUserID;
                    entity.AuditTime = DateTime.Now;
                    ShippingDocumentGH sourceDoc = entity.SourceShippingDocument;

                    

                    //剩料时，转料方量=剩料方量累加
                    //签收方量不变
                    //if (entity.ReturnType == ReturnType.ShengLiao && entity.ActionType == ActionType.Transfer)
                    //{
                    //    sourceDoc.TransferCube += entity.ActionCube;
                    //}

                    ////退料时，转料方量=退料方量累加
                    ////签收方量=出票方量-退料方量
                    //if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Transfer)
                    //{
                    //    sourceDoc.TransferCube += entity.ActionCube;

                    //    sourceDoc.SignInCube =sourceDoc.SignInCube- entity.ActionCube;
                    //}

                    ////报废，报废方量=剩退方量累加
                    ////签收方量=出票方量-报废方量
                    //if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Reject)
                    //{
                    //    sourceDoc.ScrapCube += entity.ActionCube;

                    //    sourceDoc.SignInCube = sourceDoc.SignInCube - entity.ActionCube;
                    //}

                    ////整车转发，原运输单的转料方量=运输方量
                    ////签收方量=0
                    //if (entity.ReturnType == ReturnType.Forward)
                    //{
                    //    sourceDoc.TransferCube = sourceDoc.ShippingCube;
                    //    sourceDoc.SendCube = 0;
                    //    sourceDoc.SignInCube = 0;
                    //    sourceDoc.ParCube = 0;
                    //}
                    //剩料时，转料方量=剩料方量累加
                    //签收方量不变
                    if (entity.ReturnType == ReturnType.ShengLiao && entity.ActionType == ActionType.Transfer)
                    {
                        sourceDoc.TransferCube += entity.ActionCube;
                    }

                    //退料时，转料方量=退料方量累加
                    //签收方量=出票方量-退料方量
                    if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Transfer)
                    {
                        sourceDoc.TransferCube += entity.ActionCube;
                        //sourceDoc.SignInCube = sourceDoc.ParCube - (sourceDoc.ParCube - sourceDoc.SignInCube) - entity.ActionCube;
                        sourceDoc.SignInCube = sourceDoc.SignInCube - entity.ActionCube;
                    }

                    //报废，报废方量=剩退方量累加
                    //签收方量=出票方量-报废方量
                    if (entity.ReturnType == ReturnType.TuiLiao && entity.ActionType == ActionType.Reject)
                    {
                        sourceDoc.ScrapCube += entity.ActionCube;
                        //sourceDoc.SignInCube = sourceDoc.ParCube - (sourceDoc.ParCube - sourceDoc.SignInCube) - entity.ActionCube;
                        sourceDoc.SignInCube = sourceDoc.SignInCube - entity.ActionCube;
                    }

                    //整车转发，原运输单的转料方量=运输方量
                    //签收方量=0
                    if (entity.ReturnType == ReturnType.Forward)
                    {
                        sourceDoc.TransferCube = sourceDoc.ShippingCube;
                        sourceDoc.SendCube = 0;
                        sourceDoc.SignInCube = 0;
                        sourceDoc.ParCube = 0;
                    }
                    using (IGenericTransaction trans = this.m_UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            this.Update(entity, null);
                            this.m_UnitOfWork.GetRepositoryBase<ShippingDocumentGH>().Update(sourceDoc, null);
                            this.m_UnitOfWork.Flush();
                            trans.Commit();
                            return true;
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 整车转发
        /// </summary>
        /// <param name="sourceShipDocID">源运输单ID</param>
        /// <param name="targetTaskId">转至任务单</param>
        /// <param name="isOriginComplete">原任务单已完成（不需要转料后补）</param>
        /// <param name="isTrashOrigin">原任务单作废</param>
        /// <param name="returnReason">转发原因</param>
        /// <returns></returns>
        public ShippingDocumentGH Forward(string sourceShipDocID, string targetTaskId, bool isOriginComplete, bool isTrashOrigin, string returnReason, out string message, string Cube, string ReduceConStrength, string FreightSubsidy, string[] _carids)
        {
            var shipDocRepo = this.m_UnitOfWork.GetRepositoryBase<ShippingDocumentGH>();
            ShippingDocumentGH oldDoc = shipDocRepo.Get(sourceShipDocID);
            ProduceTaskGH task = this.m_UnitOfWork.GetRepositoryBase<ProduceTaskGH>().Get(targetTaskId);
            message = Lang.Msg_Operate_Success;
            if (oldDoc != null && task != null)
            {

                //UNDONE: 转发方量和生产方量是否要处理
                ShippingDocumentGH newDoc = new ShippingDocumentGH();

                //如果有_carids说明不光转发还要换车
                if (_carids == null || _carids.Length == 0 || string.IsNullOrEmpty(_carids[0]))
                    newDoc.CarID = oldDoc.CarID;
                else
                    newDoc.CarID = _carids[0];

                newDoc.IsEffective = true;
                newDoc.ContractID = task.ContractID;

                newDoc.ContractName = task.ContractGH.ContractName;
                newDoc.CustName = task.ContractGH.CustName;
                newDoc.CustomerID = task.ContractGH.CustomerID;
                //newDoc.ContractNo = task.ContractGH.ContractNo;//lzl 加个合同号

                newDoc.CustMixpropID = task.CustMixpropID;

                newDoc.ProductLineName = oldDoc.ProductLineName;
                newDoc.ProductLineID = oldDoc.ProductLineID;

                newDoc.DeliveryTime = DateTime.Now;
                //newDoc.ProduceDate = oldDoc.ProduceDate;
                newDoc.ProduceDate = DateTime.Now;

                newDoc.ConsMixpropID = oldDoc.ConsMixpropID;
                newDoc.SlurryConsMixpropID = oldDoc.SlurryConsMixpropID;
                newDoc.FormulaName = oldDoc.FormulaName;
                newDoc.ProjectName = task.ProjectName;
                newDoc.ProjectAddr = task.ProjectAddr;
                newDoc.ProjectID = task.ProjectID;
                newDoc.ShipDocType = "0";
                newDoc.ConStrength = task.ConStrength;
                newDoc.CastMode = task.CastMode;
                newDoc.ConsPos = task.ConsPos;
                newDoc.ImpGrade = task.ImpGrade;
                newDoc.ImyGrade = task.ImyGrade;
                newDoc.ImdGrade = task.ImdGrade;
                newDoc.CarpRadii = task.CarpRadii;
                newDoc.CementBreed = task.CementBreed;
                newDoc.Distance = oldDoc.Distance;
                newDoc.RealSlump = task.Slump;//oldDoc.RealSlump;此处换成目标工地的坍落度，保持与票面一致
                newDoc.BetonCount = oldDoc.BetonCount;
                newDoc.SlurryCount = oldDoc.SlurryCount;
                newDoc.OtherCube = oldDoc.OtherCube;
                newDoc.XuCube = oldDoc.XuCube;
                newDoc.RemainCube = oldDoc.RemainCube;
                newDoc.ShippingCube = oldDoc.ShippingCube;
                newDoc.PlanCube = task.PlanCube;

                //调度方量 = 混凝土 + 砂浆
                newDoc.SendCube = newDoc.BetonCount + newDoc.SlurryCount;
                //出票方量 = 调度方量 + 其它方量 + 剩余方量
                decimal? parCube = newDoc.SendCube + newDoc.OtherCube + (newDoc.RemainCube ?? 0);
                newDoc.ParCube = parCube ?? 0;
                newDoc.SignInCube = newDoc.ParCube;
                newDoc.ScrapCube = 0;
                newDoc.TransferCube = 0;
                newDoc.Surveyor = oldDoc.Surveyor;
                newDoc.Operator = oldDoc.Operator;
               
                newDoc.ForkLift = task.ForkLift; 
                //newDoc.ProduceDate = DateTime.Now;
                newDoc.SupplyUnit = task.SupplyUnit;
                newDoc.ConstructUnit = task.ConstructUnit;
                newDoc.Signer = AuthorizationService.CurrentUserID;
                newDoc.LinkMan = task.LinkMan;
                newDoc.Tel = task.Tel;
                //是否代外生产.
                newDoc.EntrustUnit = task.IsCommission ? task.SupplyUnit : "";
                newDoc.Remark = string.Format("CODEADD由运输单:{0}整车转发生成", sourceShipDocID);
                newDoc.IsProduce = oldDoc.IsProduce;

                newDoc.ID = null;
                newDoc.TaskID = targetTaskId;
                //从最后一个发货单取得累计方量和车数
                ShippingDocumentGHService sdService = new ShippingDocumentGHService(this.m_UnitOfWork);
                ShippingDocumentGH doc = sdService.Query()
                    .Where(p => p.TaskID == targetTaskId && p.IsEffective == true && p.ShipDocType == "0")
                    .OrderByDescending(p => p.BuildTime)
                    .FirstOrDefault();
                if (doc != null)
                {
                    newDoc.ProvidedTimes = doc.ProvidedTimes + 1;
                    newDoc.ProvidedCube = doc.ProvidedCube + newDoc.ParCube; 
                }
                else 
                {
                    newDoc.ProvidedTimes = 1;
                    newDoc.ProvidedCube = newDoc.ParCube;
                }
                
               

                //生产登记关系转移到新发货单

                newDoc.DispatchListsGH = new List<DispatchListGH>();
                DispatchListGHService dispatchService = new DispatchListGHService(this.m_UnitOfWork);
                oldDoc.DispatchListsGH = dispatchService.Query().Where(p => p.ShipDocID == oldDoc.ID).ToList();
                IList<DispatchListGH> oldDocDispatchLists = dispatchService.Query().Where(p => p.ShipDocID == oldDoc.ID).ToList();
                newDoc.TZRalationsGH = null;
                //foreach (var d in oldDoc.DispatchListsGH)
                //{
                //    newDoc.DispatchListsGH.Add(d);
                //}

                //原调度关系清空
                //oldDoc.DispatchListsGH.Clear();
                //原运输单是否作废
                oldDoc.IsEffective = !isTrashOrigin;
                
                //记录作废原因
                if (oldDoc.IsEffective)
                    oldDoc.Remark = (string.IsNullOrEmpty(oldDoc.Remark) ? "" : oldDoc.Remark + " ") + "CODEADD整车转发";
                else
                    oldDoc.Remark = (string.IsNullOrEmpty(oldDoc.Remark) ? "" : oldDoc.Remark + " ") + "CODEADD整车转发作废";
                decimal tagCube = Cube == null ? 0.00m : Convert.ToDecimal(Cube);
                decimal ReduceConStr = ReduceConStrength == null ? 0.00m : Convert.ToDecimal(ReduceConStrength);
                decimal FreightSubsidys = FreightSubsidy == null ? 0.00m : Convert.ToDecimal(FreightSubsidy);
                //退转料记录
                TZRalationGH tzRelation = new TZRalationGH
                {
                    SourceShipDocID = oldDoc.ID,
                    SourceCube=oldDoc.ParCube,
                    CarID = oldDoc.CarID,
                    Driver = oldDoc.Driver,
                    Cube = oldDoc.ParCube,
                    ReturnType = ZLERP.Model.Enums.ReturnType.Forward,
                    ActionType = ZLERP.Model.Enums.ActionType.Transfer,
                    IsCompleted = true,
                    ReturnReason = returnReason,
                    ReduceConStrength = ReduceConStr,
                    FreightSubsidy = FreightSubsidys
                };
                //UNDONE:目标任务单是否要更新已供和累计方量
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        oldDoc.IsProduce = false;
                        oldDoc.SignInCube = 0;
                        oldDoc.TransferCube = oldDoc.SendCube;
                        shipDocRepo.Update(oldDoc, null);
                        //this.m_UnitOfWork.Flush();
                        string newdocid = shipDocRepo.Add(newDoc).ID;
                        // this.m_UnitOfWork.Flush();
                        IRepositoryBase<DispatchListGH> dispatchListRepo = this.m_UnitOfWork.GetRepositoryBase<DispatchListGH>();
                        foreach (var d in oldDocDispatchLists)
                        {
                            d.ShipDocID = newdocid;
                            d.SynStatus = 1;
                            d.IsCompleted = true;
                            dispatchService.Update(d, null);
                            var newDispatchList = (DispatchListGH)(d.Clone());
                            newDispatchList.ID = null;
                            newDispatchList.ShipDocID = newdocid;
                            newDispatchList.TaskID = newDoc.TaskID;
                            newDispatchList.SynStatus = 1;
                            newDispatchList.IsCompleted = true;
                            dispatchListRepo.Add(newDispatchList);
                        }
                        tzRelation.TargetShipDocID = newDoc.ID;
                        //整车转发时，生产记录自动转
                        List<ProductRecord> prList = this.m_UnitOfWork.GetRepositoryBase<ProductRecord>().Query().Where(m => m.ShipDocID == oldDoc.ID).ToList();
                        foreach (ProductRecord pr in prList)
                        {
                            pr.ShipDocID = newDoc.ID;
                            this.m_UnitOfWork.GetRepositoryBase<ProductRecord>().Update(pr, null);
                        }
                        DateTime ts = DateTime.Now;
                        tzRelation.ActionTime = ts;
                        tzRelation.ActionCube = tzRelation.Cube;

                        //整车转发状态设置为3
                        tzRelation.IsLock = "3";
                        tzRelation.IsStatistics = true;
                        //转退料单号
                        tzRelation.TzRalationFlag = getZtlDh() + "ZC";

                        base.Add(tzRelation);
                        tx.Commit();
                        return newDoc;
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        tx.Rollback();
                        logger.Error(ex.Message, ex);
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(TZRalationGH entity)
        {
            if (entity.IsAudit)
                throw new Exception("已审核记录禁止删除");
            logger.Debug(string.Format("删除转退料记录,ID:{0},ReturnType:{1},ActionType:{2}", entity.ID, entity.ReturnType, entity.ActionType));
            if (entity.ReturnType == "RT0")
            {
                var shipDocRepo = this.m_UnitOfWork.GetRepositoryBase<ShippingDocumentGH>();

                ShippingDocumentGH sourceShippingDocument = entity.SourceShippingDocument;
                sourceShippingDocument.IsEffective = true;
                sourceShippingDocument.SignInCube = sourceShippingDocument.TransferCube;
                sourceShippingDocument.TransferCube = 0;
                if (!string.IsNullOrEmpty(sourceShippingDocument.Remark) && sourceShippingDocument.Remark.IndexOf("CODEADD整车转发") >= 0)
                    sourceShippingDocument.Remark = sourceShippingDocument.Remark.Remove(sourceShippingDocument.Remark.IndexOf("CODEADD整车转发"));

                ShippingDocumentGH targetShippingDocument = entity.TargetShippingDocument;
                targetShippingDocument.IsEffective = false;
                targetShippingDocument.Remark = (string.IsNullOrEmpty(targetShippingDocument.Remark) ? "" : targetShippingDocument.Remark + " ") + "整车转发记录被删除";

                shipDocRepo.Update(sourceShippingDocument, null);
                shipDocRepo.Update(targetShippingDocument, null);
            }
            base.Delete(entity);
        }

        /// <summary>
        /// 分装
        /// </summary>
        /// <param name="sourceShipDocID"></param>
        /// <param name="sourceCube"></param>
        /// <param name="returnType"></param>
        /// <param name="returnReason"></param>
        /// <param name="carIDArr"></param>
        /// <param name="carCubeArr"></param>
        /// <returns></returns>
        public bool Split(string sourceShipDocID, decimal sourceCube, string returnType, string returnReason, string[] carIDArr, string[] carCubeArr, string[] actionTypeArr)
        {
            lock (obj)
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        DateTime ts = DateTime.Now;
                        for (int i = 0; i < carIDArr.Length; ++i)
                        {
                            if (!string.IsNullOrEmpty(carIDArr[i]) && !string.IsNullOrEmpty(carCubeArr[i]) && !string.IsNullOrEmpty(actionTypeArr[i]))
                            {
                                TZRalationGH tz = new TZRalationGH();
                                tz.SourceShipDocID = sourceShipDocID;
                                tz.SourceCube = sourceCube;
                                tz.ReturnType = returnType;
                                tz.CarID = carIDArr[i];
                                tz.Cube = Decimal.Parse(carCubeArr[i]);
                                tz.ActionType = actionTypeArr[i];

                                if (tz.ActionType == Model.Enums.ActionType.Reject)
                                {
                                    tz.IsCompleted = true;

                                }
                                else
                                {
                                    tz.IsCompleted = false;
                                }
                                tz.AH = Model.Enums.Consts.Handle;
                                tz.ReturnReason = returnReason;

                                tz.ActionTime = ts;
                                tz.ActionCube = tz.Cube;
                                tz.IsLock = "1";//锁定

                                base.Add(tz);
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        logger.Error(e.Message, e); ;
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// 修改转退料
        /// </summary>
        /// <param name="sourceShipDocID"></param>
        /// <param name="sourceCube"></param>
        /// <param name="returnType"></param>
        /// <param name="returnReason"></param>
        /// <param name="carIDArr"></param>
        /// <param name="carCubeArr"></param>
        /// <returns></returns>
        private static readonly object obj = new object();
        public bool SplitByZT(string id ,string sourceShipDocID, decimal sourceCube, string returnType, string returnReason, string[] carIDArr, string[] carCubeArr, string[] actionTypeArr,string tzrFlag,ref string msg)
        {

            lock (obj)
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        msg = "";
                        PublicService ps = new PublicService();
                        //查看是否符合分车标准
                        TZRalationGH tz;
                        


                        //查看是否为完成
                        TZRalationGH _tz = new TZRalationGH();
                        _tz.ID = Convert.ToInt32(id);
                        List<TZRalationGH> list = this.Query().Where(m => m.ID == Convert.ToInt32(id)).ToList();
                        if (list == null || list.Count == 0)
                        {
                            msg += "没有找到转退料信息，请重新刷新。";
                            return false;
                        }
                        if (list[0].IsCompleted == true)
                        {
                            msg += "不能修改已完成的转退料信息。";
                            return false;
                        }
                        
                        if (list[0].IsLock == "2")
                        {
                            msg += "不能修改合车的转退料信息。";
                            return false;
                        }

                        bool isSame = false;
                        DateTime ts = DateTime.Now;
                        for (int i = 0; i < carIDArr.Length; ++i)
                        {
                            decimal sum = 0.00m;
                            bool Flag = false;

                            if (!string.IsNullOrEmpty(carIDArr[i]) && !string.IsNullOrEmpty(carCubeArr[i]) && !string.IsNullOrEmpty(actionTypeArr[i]))
                            {
                                tz = null;
                                tz = this.Query().Where(p => p.CarID == carIDArr[i] && p.IsCompleted == false && (p.IsLock != "2" || p.IsLock != "3")).FirstOrDefault();
                                if (tz != null)
                                {
                                    Flag = true;
                                    //存在转退料记录 如果目标车和源车是同一车则不能相加
                                    //if (tz.CarID == carIDArr[i])
                                    if (list[0].CarID == carIDArr[i])
                                    {
                                        //同一车
                                        sum = string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i]);
                                        isSame = true;//同车标识，有同车则不删除老的转退料。
                                    }
                                    else
                                    {
                                        sum = (string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i])) + tz.Cube;
                                    }
                                    //查找车容重
                                    Car car = ps.Car.Query().Where(p => p.ID == carIDArr[i]).FirstOrDefault();
                                    if (car == null)
                                    {
                                        msg += "没有找到" + carIDArr[i] + "号车</br>";
                                        break;
                                    }
                                    if (car.MaxCube < sum)
                                    {
                                        //不能大于车辆最大容重
                                        msg += string.Format("{0}车有转退料{1}方,超过了最大容重{2}。</br>", carIDArr[i], tz.Cube, car.MaxCube);
                                        break;
                                    }
                                }
                                else
                                {
                                    tz = new TZRalationGH();
                                    sum = string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i]);
                                    Flag = false;
                                }

                                tz.CarID = carIDArr[i];
                                tz.Cube = sum;
                                tz.ActionType = actionTypeArr[i];
                                tz.ActionCube = tz.Cube;
                                tz.TzRalationFlag = tzrFlag;

                                //tz.SourceShipDocID = sourceShipDocID;
                                //tz.SourceCube = sourceCube;
                                //tz.ReturnType = returnType;

                                //不相等说明这个有转退料 不要动里面的源信息
                                if (string.IsNullOrEmpty(tz.SourceShipDocID) && tz.SourceShipDocID != list[0].SourceShipDocID)
                                {
                                    tz.SourceShipDocID = list[0].SourceShipDocID;
                                    tz.SourceCube = list[0].SourceCube;
                                    tz.ReturnType = list[0].ReturnType;
                                } 
                                if (tz.ActionType == Model.Enums.ActionType.Reject)//如果是报废 改已完成
                                    tz.IsCompleted = true;
                                else
                                    tz.IsCompleted = false;
                                tz.AH = Model.Enums.Consts.Handle;
                                tz.ReturnReason = returnReason;
                                tz.ActionTime = ts;
                                //tz.ActionCube = string.IsNullOrEmpty(carCubeArr[i]) ? 0.00m : Convert.ToDecimal(carCubeArr[i]); //tz.SourceCube;
                                tz.IsLock = "1";//分车锁定
                                decimal? value;
                                if (string.IsNullOrEmpty(carCubeArr[i]))
                                    value = null;
                                else
                                    value = Convert.ToDecimal(carCubeArr[i]);

                                if (Flag)
                                {
                                    base.Update(tz,null);
                                }
                                else
                                {
                                    base.Add(tz);
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(msg))
                        {
                            tx.Rollback();
                            return false;
                        }

                        //删除老的          
                        if (!isSame)
                        {
                            TZRalationGH ralation = list[0];
                            ralation.IsStatistics = true;
                            ralation.IsCompleted = true;
                            base.Update(ralation, null);
                            //base.Delete(list[0]);
                            //AddHistory(list[0], "delete", list[0].CarID, list[0].Cube);
                        }

                        tx.Commit();
                        
                        return true;
                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        logger.Error(e.Message, e); ;
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sourceShipDocIDArr"></param>
        /// <param name="sourceCubeArr"></param>
        /// <param name="returnTypeArr"></param>
        /// <param name="returnReason"></param>
        /// <param name="carID"></param>
        /// <param name="cube"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public bool Merge(string[] sourceShipDocIDArr, string[] sourceCubeArr, string[] returnTypeArr, string returnReason, string carID, decimal cube, string actionType)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DateTime ts = DateTime.Now;
                    for (int i = 0; i < sourceShipDocIDArr.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(sourceShipDocIDArr[i]) && !string.IsNullOrEmpty(sourceCubeArr[i]) && !string.IsNullOrEmpty(returnTypeArr[i]))
                        {
                            TZRalationGH tz = new TZRalationGH();
                            tz.SourceShipDocID = sourceShipDocIDArr[i];
                            tz.SourceCube = decimal.Parse(sourceCubeArr[i]);
                            tz.ReturnType = returnTypeArr[i];
                            tz.CarID = carID;
                            tz.Cube = cube;
                            tz.ActionType = actionType;
                            tz.IsLock = "2";//0未锁定 1锁定 2永久锁定
                            if (tz.ActionType == Model.Enums.ActionType.Reject)
                            {
                                tz.IsCompleted = true;

                            }
                            else
                            {
                                tz.IsCompleted = false;
                            }
                            tz.AH = Model.Enums.Consts.Handle;
                            tz.ReturnReason = returnReason;

                            tz.ActionTime = ts;
                            tz.ActionCube = tz.SourceCube;

                            base.Add(tz);
                        }
                    }

                    tx.Commit();
                    return true;
                }
                catch(Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);                   
                }
            }

            return false;
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sourceShipDocIDArr">源转退料信息</param>
        /// <param name="sourceCubeArr">源转退料方量</param>
        /// <param name="returnTypeArr"></param>
        /// <param name="returnReason"></param>
        /// <param name="carID"></param>
        /// <param name="cube"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public bool MergeZT(string[] sourceShipDocIDArr, string[] sourceCubeArr, string[] returnTypeArr, string returnReason, string carID, decimal cube, string actionType, string[] returnTypeArrTarget)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    DateTime ts = DateTime.Now;
                    string SourceShipDocID = "";
                    foreach (var item in sourceShipDocIDArr)//合车：源运输单号用逗号拼接，如：001,002
                    {
                        if (item != "")
                        {
                            TZRalationGH tz = this.Get(Convert.ToInt32(item));
                            string SourceID = tz == null ? "" : tz.SourceShipDocID;
                            if (!SourceShipDocID.Contains(SourceID))//避免 重复
                            {
                                SourceShipDocID += SourceID + ",";
                            }
                        }
                    }
                    if (sourceShipDocIDArr != null)
                        SourceShipDocID = SourceShipDocID.Substring(0, SourceShipDocID.Length - 1);
                    //查找目标本身是否有转退料
                    TZRalationGH unComplete = this.Query()
                                .Where(p => p.CarID == carID && p.IsCompleted == false && p.IsLock != "3")
                                .FirstOrDefault();
                    if (unComplete != null)//本身有转退料，修改源
                    {
                        unComplete.ActionTime = ts;
                        cube += unComplete.Cube;
                        unComplete.CarID = carID;
                        unComplete.Cube = cube;
                        unComplete.ActionCube = cube;
                        unComplete.ActionType = actionType;
                        unComplete.IsLock = "2";//合车锁定
                        if (unComplete.ActionType == Model.Enums.ActionType.Reject)
                            unComplete.IsCompleted = true;
                        else
                            unComplete.IsCompleted = false;
                        unComplete.ReturnType = returnTypeArrTarget != null ? returnTypeArrTarget[0] : "";
                        unComplete.AH = Model.Enums.Consts.Handle;
                        unComplete.ReturnReason = returnReason;
                        AddHistory(unComplete, "update", unComplete.CarID, cube);
                        unComplete.SourceShipDocID = unComplete.SourceShipDocID + "," + SourceShipDocID;//拼接源运输单号
                        base.Update(unComplete, null);
                    }
                    else
                    {
                        TZRalationGH tz = new TZRalationGH();
                        tz.SourceShipDocID = SourceShipDocID;
                        tz.Cube = cube;
                        tz.CarID = carID;
                        tz.ActionCube = cube;
                        tz.ActionType = actionType;
                        tz.ActionTime = ts;
                        tz.IsLock = "2";//合车锁定
                        tz.IsCompleted = actionType == Model.Enums.ActionType.Reject ? true : false;//报废 改已完成
                        tz.ReturnType = returnTypeArrTarget != null ? returnTypeArrTarget[0] : "";
                        tz.AH = Model.Enums.Consts.Handle;
                        tz.ReturnReason = returnReason;
                        tz.SourceCube = cube;
                        tz.TzRalationFlag = getZtlDh();
                        base.Add(tz);
                    }
                    for (int i = 0; i < sourceShipDocIDArr.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(sourceShipDocIDArr[i]) && !string.IsNullOrEmpty(sourceCubeArr[i]) && !string.IsNullOrEmpty(returnTypeArr[i]))
                        {
                            TZRalationGH tz = this.Query()
                                .Where(p => p.ID == Convert.ToInt32(sourceShipDocIDArr[i]) && (p.IsLock != "2" || p.IsLock != "3") && p.IsCompleted == false)
                                .FirstOrDefault();
                            if (tz == null)
                                continue;
                            decimal ActionCube = string.IsNullOrEmpty(sourceCubeArr[i]) ? 0.00m : Convert.ToDecimal(sourceCubeArr[i]);
                            if (tz.ActionCube > ActionCube)//如果分车未将源车分完，则新增一条记录
                            {
                                tz.ActionCube = tz.ActionCube - ActionCube;
                                base.Update(tz, null);
                                tz.IsStatistics = true;//合车后的源数据 不统计
                                tz.IsCompleted = true;//合车后的源数据 已完成
                                tz.ActionCube = ActionCube;
                                base.Add(tz);
                            }
                            else
                            {
                                tz.IsStatistics = true;//合车后的源数据 不统计
                                tz.IsCompleted = true;//合车后的源数据 已完成
                                base.Update(tz, null);
                            }
                            base.Update(tz,null);
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                }
            }

            return false;
        }

        /// <summary>
        /// 插入历史数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operation"></param>
        /// <param name="opertionnum">真实源车车号</param>
        /// <param name="num">方量</param>
        public void AddHistory(TZRalationGH entity, string operation, string opertionnum, decimal? operationcube)
        {
            //插入历史数据
            TZRalationHistoryGH history = new TZRalationHistoryGH();
            history.ActionCube = entity.ActionCube;
            history.ActionTime = entity.ActionTime;
            history.ActionType = entity.ActionType;
            history.AH = entity.AH;
            history.Auditor = entity.Auditor;
            history.AuditTime = entity.AuditTime;
            history.Builder = entity.Builder;
            history.BuildTime = entity.BuildTime;
            history.CarID = entity.CarID;
            history.CarWeight = entity.CarWeight;
            history.Cube = entity.Cube;
            history.DealMan = entity.DealMan;
            history.DealTime = entity.DealTime;
            history.Driver = entity.Driver;
            history.DriverUser = entity.DriverUser;
            history.Exchange = entity.Exchange;
            history.ID = entity.ID;
            history.IsAudit = entity.IsAudit;
            history.IsCompleted = entity.IsCompleted;

            if (operation == "delete")
                history.IsLock = "-1";
            else
                history.IsLock = entity.IsLock;

            history.Lifecycle = entity.Lifecycle;
            history.Modifier = entity.Modifier;
            history.ModifyTime = entity.ModifyTime;
            history.ReturnReason = entity.ReturnReason;
            history.ReturnType = entity.ReturnType;
            history.ShippingDocumentGH = entity.ShippingDocumentGH;
            history.ShippingDocumentsGH = entity.ShippingDocumentsGH;
            //history.SourceCarID = entity.SourceCarID;
            //history.SourceConStrength = entity.SourceConStrength;
            history.SourceCube = entity.SourceCube;
            //history.SourceProjectName = entity.SourceProjectName;
            history.SourceShipDocID = entity.SourceShipDocID;
            history.SourceShippingDocument = entity.SourceShippingDocument;
            //history.TargetConStrength = entity.TargetConStrength;
            //history.TargetProjectName = entity.TargetProjectName;
            history.TargetShipDocID = entity.TargetShipDocID;
            history.TargetShippingDocument = entity.TargetShippingDocument;
            history.TotalWeight = entity.TotalWeight;
            history.Version = entity.Version;
            history.Weight = entity.Weight;
            history.ParentID = entity.ID;
            history.operation = operation;
            history.operationnum = opertionnum;
            history.operationcube = operationcube;
            history.TzRalationFlag = entity.TzRalationFlag;

            s = (s == null ? (new PublicService()) : s);
            s.TZRalationHistoryGH.Add(history);
        }
    }
}
