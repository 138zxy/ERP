﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class TZRalationController : BaseController<TZRalation, int>
    {
        public override ActionResult Index()
        {
            var data = this.service.Car.GetMixerCarListOrderByID();
            data.Insert(0, new Car { ID = "", CarNo = Lang.Car_SelectCar });
            ViewBag.CarList = new SelectList(data, "ID", "CarNo");

            ViewBag.ActionTypeList = HelperExtensions.SelectListData<Dic>("DicName", "TreeCode", " ParentID='ActionType' ", "TreeCode", true, "");
            ViewBag.ReturnTypeList = HelperExtensions.SelectListData<Dic>("DicName", "TreeCode", " ParentID='ReturnType' ", "TreeCode", true, "");

            return base.Index();
        }

        public ActionResult IndexSH()
        {
            var data = this.service.Car.GetMixerCarListOrderByID();
            data.Insert(0, new Car { ID = "", CarNo = Lang.Car_SelectCar });
            ViewBag.CarList = new SelectList(data, "ID", "CarNo");

            ViewBag.ActionTypeList = HelperExtensions.SelectListData<Dic>("DicName", "TreeCode", " ParentID='ActionType' ", "TreeCode", true, "");
            ViewBag.ReturnTypeList = HelperExtensions.SelectListData<Dic>("DicName", "TreeCode", " ParentID='ReturnType' ", "TreeCode", true, "");

            return base.Index();
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            ActionResult r = base.FindNew(request, condition);
            string shipdocids = "";
            foreach (TZRalation p in ((ZLERP.Model.JqGridData<TZRalation>)((JsonResult)r).Data).rows)
            {
                if (string.IsNullOrEmpty(p.SourceShipDocID))
                    continue;
                shipdocids = shipdocids + p.SourceShipDocID + ",";
            }
            List<ShippingDocument> shiplists = this.service.ShippingDocument.Query().Where(a => shipdocids.Contains(a.ID)).ToList();
            if (r != null && shiplists != null)
            {
                foreach (TZRalation p in ((ZLERP.Model.JqGridData<TZRalation>)((JsonResult)r).Data).rows)
                {
                    if (string.IsNullOrEmpty(p.SourceShipDocID))
                        continue;
                    var ships = shiplists.Where(a => p.SourceShipDocID.Contains(a.ID));//获取运输单列表 因为SourceShipDocID可能存在xxx,xxx格式
                    string SourceProjectName = "";
                    string SourceConStrength = "";
                    string SourceCarID = "";
                    if (ships.ToList().Count > 0)
                    {
                        foreach (var item in ships)//合车：源运输单号用逗号拼接，如：001,002
                        {
                            SourceProjectName += item.ProjectName + ",";
                            SourceConStrength += item.ConStrength + ",";
                            SourceCarID += item.CarID + ",";
                        }
                        SourceProjectName = SourceProjectName.Substring(0, SourceProjectName.Length - 1);
                        SourceConStrength = SourceConStrength.Substring(0, SourceConStrength.Length - 1);
                        SourceCarID = SourceCarID.Substring(0, SourceCarID.Length - 1);
                        p.SourceProjectName = SourceProjectName;
                        p.SourceConStrength = SourceConStrength;
                        p.SourceCarID = SourceCarID;
                    }
                }
            }
            return r;
        }

        /// <summary>
        /// 根据车号取得最后一条发货单
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLastDocByCarId(string carId,int? dataType)
        {
            if (dataType == null)
                dataType = 0;

            TZRalation tz = this.service.TZRalation.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false && p.DataType == dataType)
                                .FirstOrDefault();

            if (tz != null)
            {//有未完成的退转料记录

                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords_NO, null);
            }
            else
            {
                ShippingDocument doc = this.service.ShippingDocument.Query()
                    .Where(p => p.CarID == carId && p.IsEffective && p.DataType == dataType)
                    .OrderByDescending(p => p.ProduceDate)
                    .FirstOrDefault();
                if (doc != null && this.service.TZRalation.Query()
                                    .Where(p => p.SourceShipDocID.Contains(doc.ID) && p.IsCompleted == false)
                                    .Count() > 0)
                {
                    return OperateResult(false, Lang.TZRelation_Error_HasTZRelationRecords, null);
                }
                return OperateResult(doc != null, "", doc);
            }
        }

        /// <summary>
        /// 根据车号取得转退料记录
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLastDocByCarId1(string carId, int? dataType)
        {
            if (dataType == null)
                dataType = 0;
            //isLock为0,1才能合车
            TZRalation tz = this.service.TZRalation.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false && p.DataType == dataType)
                                .FirstOrDefault();

            if (tz == null)
            {//没有退转料记录

                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords1, null);
            }
            else
            {
                if (tz.IsLock == "2")
                    return OperateResult(false, "该车存在合车记录，不可再合车！", null);
                if (tz.IsLock == "1")
                    return OperateResult(false, "该车存在分车记录，不可再合车！", null);
                return OperateResult(true, "", tz);
            }
        }


        /// <summary>
        /// 根据车号取得最后一条发货单
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLastDocAutoByCarId(string carId, int? dataType)
        {
            if (dataType == null)
                dataType = 0;
            TZRalation tz = this.service.TZRalation.Query()
                                .Where(p =>p.DataType==dataType && p.CarID == carId && p.IsCompleted == false && p.AH == Model.Enums.Consts.Auto && (p.ActionType == string.Empty || p.ActionType == null))
                                .OrderByDescending(p => p.BuildTime)
                                .FirstOrDefault();

            if (tz != null)
            {
                ShippingDocument doc = this.service.ShippingDocument.Query().Where(p => p.ID == tz.SourceShipDocID).FirstOrDefault();
                return OperateResult(true, string.Empty, doc);
            }
            else
            {
                return OperateResult(false, string.Empty, null);
            }
        }

        [HttpPost]
        public JsonResult GetRemaincubeByCarId(string carId,string taskid,string lineid, int? dataType)
        {
            if (dataType == null)
                dataType = 0;
            //ShippingDocument doc = this.service.ShippingDocument.Query()
            //        .Where(p => p.CarID == carId && p.IsEffective)
            //        .OrderByDescending(p => p.BuildTime)
            //        .FirstOrDefault();
            //if (doc != null)
            //{
            //判断该车是否有未处理的转退料（自动过磅才用到）
            TZRalation objchk = this.service.TZRalation.Query()
               .Where(p => p.CarID == carId && p.IsCompleted == false && p.DataType == dataType).OrderByDescending(p => p.BuildTime)
               .FirstOrDefault();

            if (objchk != null)
            {
                if (string.IsNullOrEmpty(objchk.ActionType))
                {
                    //判断用户是否有“转退料方式处理”的权限
                    IList<SysFunc> FuncList = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);
                    SysFunc sf = FuncList.Where(p => p.ID == "410505").FirstOrDefault();
                    if (sf != null)              //有权限
                    {
                        return OperateResult(false, "", objchk);
                    }
                    else
                    {
                        return OperateResult(false, "", null);
                    }
                }

            }

            TZRalation obj = this.service.TZRalation.Query()
                .Where(p => p.CarID == carId && p.IsCompleted == false && p.ActionType == "AT1" && p.DataType == dataType).FirstOrDefault();
            if (obj == null)
            {
                return OperateResult(false, "", 0);
            }
            else
            {
                if ((!(taskid==""))&&!(lineid==""))
	            {
                    ProduceTask task=this.service.ProduceTask.Get(taskid);
                    ConsMixprop cons = this.service.ConsMixprop.Query().Where(a => a.TaskID == taskid && a.ProductLineID == lineid).First();
                    if (cons!=null)
                    {
                        obj.Cube = cons.Weight==null?0.00m:Math.Round(obj.Weight/Convert.ToDecimal(cons.Weight),2);
                    }
	            }
                return OperateResult(true, "本车有转退料记录，方量为:" + obj.Cube.ToString(), obj.Cube);
            }
            //}
            //else
            //{
            //    return OperateResult(false, "", 0);
            //}

        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Audit(int id)
        {
            bool ret = this.service.TZRalation.Audit(id);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, null);
        }

        /// <summary>
        /// 整车转发
        /// </summary>
        /// <param name="sourceShipDocID"></param>
        /// <param name="targetTaskId"></param>
        /// <param name="isOriginComplete"></param>
        /// <param name="isTrashOrigin"></param>
        /// <param name="returnReason">原因</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Forward(string sourceShipDocID, string targetTaskId, bool isOriginComplete, bool isTrashOrigin, string returnReason, string Cube, string ReduceConStrength, string FreightSubsidy, string[] _caridz)
        {
            string message;
            //List<CustomerBalance> list = this.service.GetGenericService<CustomerBalance>().Query().Where(a => a.ShipDocID == sourceShipDocID).ToList();
            //if (list.Count > 0)
            //{
            //    return OperateResult(false, "已结算的运输单不可转发，请重新选择！", null);
            //}
            ShippingDocument ret = this.service.TZRalation.Forward(sourceShipDocID, targetTaskId, isOriginComplete, isTrashOrigin, returnReason, out message, Cube, ReduceConStrength, FreightSubsidy, _caridz);
            return OperateResult(ret != null, message, ret);
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
        [HttpPost]
        public ActionResult Split(string sourceShipDocID, decimal sourceCube, string returnType, string returnReason, string _TzRFlag, string[] carIDArr, string[] carCubeArr, string[] actionTypeArr)
        {
            try
            {
                bool ret = this.service.TZRalation.Split(sourceShipDocID, sourceCube, returnType, returnReason, carIDArr, carCubeArr, actionTypeArr);
                return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, null);

            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
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
        [HttpPost]
        public ActionResult Merge(string[] sourceShipDocIDArr, string[] sourceCubeArr, string[] returnTypeArr, string returnReason, string carID, decimal cube, string actionType)
        {
            try
            {
                bool ret = this.service.TZRalation.Merge(sourceShipDocIDArr, sourceCubeArr, returnTypeArr, returnReason, carID, cube, actionType);
                return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 合并(新)
        /// </summary>
        /// <param name="sourceShipDocIDArr">转退料的ID</param>
        /// <param name="sourceCubeArr">源方量</param>
        /// <param name="returnTypeArr">转类型</param>
        /// <param name="returnReason">原因</param>
        /// <param name="carID">目标车id</param>
        /// <param name="cube">目标方量</param>
        /// <param name="actionType">目标类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MergeZT(string[] sourceShipDocIDArr, string[] sourceCubeArr, string[] returnTypeArr, string[] returnTypeArrTarget, string returnReason, string carID, decimal cube, string actionType)
        {
            try
            {
                bool ret = this.service.TZRalation.MergeZT(sourceShipDocIDArr, sourceCubeArr, returnTypeArr, returnReason, carID, cube, actionType, returnTypeArrTarget);
                return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUnCompletedByCarId(string carId)
        {
            int unComplete = this.service.TZRalation.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false)
                                .Count();

            if (unComplete > 0)
            {//有未完成的退转料记录
                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords, null);
            }
            else
            {
                return OperateResult(true, "该车可以添加退转料记录", null);
            }
        }



        [HttpPost]
        public ActionResult MarkAction(TZRalation tzRalation)
        {
            //若按照车号处理转退料方式
            if (tzRalation.ID == null)
            {
                TZRalation objchk = this.service.TZRalation.Query()
                   .Where(p => p.CarID == tzRalation.CarID && p.IsCompleted == false).OrderByDescending(p => p.BuildTime)
                   .FirstOrDefault();
                if (objchk != null)
                {
                    tzRalation.ActionType = objchk.ActionType;
                    tzRalation.ReturnType = objchk.ReturnType;
                    tzRalation.ReturnReason = objchk.ReturnReason;
                }
            }

            if (tzRalation.ActionType == Model.Enums.ActionType.Reject)     //若选择报废，则处理完成
            {
                tzRalation.IsCompleted = true;
            }

            tzRalation.IsLock = "0";
            ActionResult r = base.Update(tzRalation);

            TZRalation op = null;
            s = s == null ? (new PublicService()) : s;
            if (tzRalation.ID != null)
                op = this.service.TZRalation.Query()
                  .Where(p => p.ID == tzRalation.ID && p.IsCompleted == false).OrderByDescending(p => p.BuildTime)
                  .FirstOrDefault();
            if (op != null)
            {
                s.TZRalationHistory.AddHistory(op, "update", op.CarID, op.Cube, "3");
            }
            else
            {
                s.TZRalationHistory.AddHistory(tzRalation, "update", tzRalation.CarID, tzRalation.Cube, "3");
            }

            return r;
        }

        public override ActionResult Add(TZRalation entity)
        {
            if (entity.ActionType == Model.Enums.ActionType.Reject)
            {
                entity.IsCompleted = true;

            }
            entity.AH = Model.Enums.Consts.Handle;

            //增加剩退料记录时，源剩退方量等于目标剩退方量
            entity.SourceCube = entity.Cube;
            entity.IsLock = "0";

            ActionResult r = base.Add(entity);

            ////插入历史数据
            //TZRalationHistory history = new TZRalationHistory();
            //history.ActionCube = entity.ActionCube;
            //history.ActionTime = entity.ActionTime;
            //history.ActionType = entity.ActionType;
            //history.AH = entity.AH;
            //history.Auditor = entity.Auditor;
            //history.AuditTime = entity.AuditTime;
            //history.Builder = entity.Builder;
            //history.BuildTime = entity.BuildTime;
            //history.CarID = entity.CarID;
            //history.CarWeight = entity.CarWeight;
            //history.Cube = entity.Cube;
            //history.DealMan = entity.DealMan;
            //history.DealTime = entity.DealTime;
            //history.Driver = entity.Driver;
            //history.DriverUser = entity.DriverUser;
            //history.Exchange = entity.Exchange;
            //history.ID = entity.ID;
            //history.IsAudit = entity.IsAudit;
            //history.IsCompleted = entity.IsCompleted;
            //history.IsLock = entity.IsLock;
            //history.Lifecycle = entity.Lifecycle;
            //history.Modifier = entity.Modifier;
            //history.ModifyTime = entity.ModifyTime;
            //history.ReturnReason = entity.ReturnReason;
            //history.ReturnType = entity.ReturnType;
            //history.ShippingDocument = entity.ShippingDocument;
            //history.ShippingDocuments = entity.ShippingDocuments;
            ////history.SourceCarID = entity.SourceCarID;
            ////history.SourceConStrength = entity.SourceConStrength;
            //history.SourceCube = entity.SourceCube;
            ////history.SourceProjectName = entity.SourceProjectName;
            //history.SourceShipDocID = entity.SourceShipDocID;
            //history.SourceShippingDocument = entity.SourceShippingDocument;
            ////history.TargetConStrength = entity.TargetConStrength;
            ////history.TargetProjectName = entity.TargetProjectName;
            //history.TargetShipDocID = entity.TargetShipDocID;
            //history.TargetShippingDocument = entity.TargetShippingDocument;
            //history.TotalWeight = entity.TotalWeight;
            //history.Version = entity.Version;
            //history.Weight = entity.Weight;
            //history.ParentID = entity.ID;

            //PublicService s = new PublicService();
            //s.TZRalationHistory.Add(history);


            return r;
        }

        public override ActionResult Update(TZRalation entity)
        {
            if (entity.ActionType == Model.Enums.ActionType.Reject)
            {
                entity.IsCompleted = true;
            }
            return base.Update(entity);
        }

        /// <summary>
        /// 合车的目标车
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUnCompletedByCarId1(string carId)
        {
            TZRalation unComplete = this.service.TZRalation.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false)
                                .FirstOrDefault();

            if (unComplete != null)
            {//有未完成的退转料合车记录
                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords, null);
            }
            else
            {
                unComplete = this.service.TZRalation.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false && p.IsLock != "2")
                                .FirstOrDefault();

                //没有转退料记录则返回车的容重
                Car car = this.service.Car.Query().Where(p => p.ID == carId).FirstOrDefault();
                if (car == null)
                    return OperateResult(false, "没有找到此车的车辆信息", null);
                //如果有转退料那么就需要计算最大容重
                car.MaxCube = car.MaxCube == null ? 0.00m : (car.MaxCube - (unComplete == null ? 0.00m : unComplete.Cube));
                //告诉前台此车有转退料记录未处理
                if (unComplete != null)
                {
                    car.Modifier = "yes";
                }
                return OperateResult(true, "该车可以添加退转料记录", car);
            }
        }

        /// <summary>
        /// 分车选择目标车
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUnCompletedByCarId2(string carId)
        {
            //只有未完成的合车记录是不能作为分车目标车。
            int unComplete = this.service.TZRalation.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false && p.IsLock == "2")
                                .Count();

            if (unComplete > 0)
            {//有未完成的合车退转料记录
                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords, null);
            }
            else
            {
                return OperateResult(true, "该车可以添加退转料记录", null);
            }
        }


        /// <summary>
        /// 转退料修改
        /// </summary>
        /// <param name="sourceShipDocID"></param>
        /// <param name="sourceCube"></param>
        /// <param name="returnType"></param>
        /// <param name="returnReason"></param>
        /// <param name="carIDArr"></param>
        /// <param name="carCubeArr"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SplitByZT(string ID, string sourceShipDocID, string scube, string returnType, string returnReason, string[] carIDArr, string[] carCubeArr, string[] actionTypeArr, string tzrFlag)
        {
            try
            {
                decimal _sourceCube = string.IsNullOrEmpty(scube) ? 0.00m : Convert.ToDecimal(scube);
                string msg = "";
                bool ret = this.service.TZRalation.SplitByZT(ID, sourceShipDocID, _sourceCube, returnType, returnReason, carIDArr, carCubeArr, actionTypeArr, tzrFlag, ref msg);
                ////return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, null);
                return OperateResult(ret, ret ? Lang.Msg_Operate_Success : msg, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }


        public virtual ActionResult GetTagCube(string carid, string targetTaskId)
        {
            ShippingDocument ship = this.service.ShippingDocument.Query()
                    .Where(p => p.CarID == carid && p.IsEffective)
                    .OrderByDescending(p => p.BuildTime)
                    .FirstOrDefault();//获取源运输单
            if (ship != null && this.service.TZRalation.Query()
                                .Where(p => p.SourceShipDocID.Contains(ship.ID) && p.IsCompleted == false)
                                .Count() > 0)
            {
                return OperateResult(false, Lang.TZRelation_Error_HasTZRelationRecords, null);
            }
            ProduceTask task = this.service.ProduceTask.Get(targetTaskId);
            ContractItem contract = task.Contract.ContractItems.Where(a => a.ConStrength == task.ConStrength).OrderByDescending(p => p.BuildTime).FirstOrDefault();
            ContractItem oldContr = ship.ProduceTask.Contract.ContractItems.Where(a => a.ConStrength == ship.ProduceTask.ConStrength).OrderByDescending(p => p.BuildTime).FirstOrDefault();
            //decimal ExMoney = contract.ExMoney == null ? 0.00m : Convert.ToDecimal(contract.ExMoney);
            List<PriceSetting> pricesettings = this.service.GetGenericService<PriceSetting>().Query().Where(a => a.ContractItemsID == contract.ID && a.ChangeTime <= DateTime.Now).ToList();
            List<PriceSetting> OldPricesettings = this.service.GetGenericService<PriceSetting>().Query().Where(a => a.ContractItemsID == oldContr.ID && a.ChangeTime <= ship.ProduceDate).ToList();
            decimal UnPumpPrice = 0.00m;
            decimal OldUnPumpPrice = 0.00m;
            if (pricesettings.Count > 0)
            {
                UnPumpPrice = Convert.ToDecimal(pricesettings.OrderByDescending(a => a.ChangeTime).First().UnPumpPrice);
            }
            else
            {
                UnPumpPrice = contract.UnPumpPrice == null ? 0.00m : Convert.ToDecimal(contract.UnPumpPrice);
            }
            if (OldPricesettings.Count > 0)
            {
                OldUnPumpPrice = Convert.ToDecimal(OldPricesettings.OrderByDescending(a => a.ChangeTime).First().UnPumpPrice);
            }
            else
            {
                OldUnPumpPrice = oldContr.UnPumpPrice == null ? 0.00m : Convert.ToDecimal(oldContr.UnPumpPrice);
            }
            //ContractItem contract=this.service.ContractItem.Query().Where(a=>a.ContractID=task.Contract.contra)
            TZRalation tzralation = new TZRalation();
            tzralation.Cube = ship.ParCube;
            tzralation.ReduceConStrength = OldUnPumpPrice - UnPumpPrice;
            tzralation.FreightSubsidy = 0.00m;
            return OperateResult(true, "", tzralation);
        }


        PublicService s;
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="operation"></param>
        ///// <param name="opertionnum">真实源车车号</param>
        ///// <param name="num">方量</param>
        //public void AddHistory(TZRalation entity, string operation, string opertionnum, decimal? operationcube)
        //{
        //    //插入历史数据
        //    TZRalationHistory history = new TZRalationHistory();
        //    history.ActionCube = entity.ActionCube;
        //    history.ActionTime = entity.ActionTime;
        //    history.ActionType = entity.ActionType;
        //    history.AH = entity.AH;
        //    history.Auditor = entity.Auditor;
        //    history.AuditTime = entity.AuditTime;
        //    history.Builder = entity.Builder;
        //    history.BuildTime = entity.BuildTime;
        //    history.CarID = entity.CarID;
        //    history.CarWeight = entity.CarWeight;
        //    history.Cube = entity.Cube;
        //    history.DealMan = entity.DealMan;
        //    history.DealTime = entity.DealTime;
        //    history.Driver = entity.Driver;
        //    history.DriverUser = entity.DriverUser;
        //    history.Exchange = entity.Exchange;
        //    history.ID = entity.ID;
        //    history.IsAudit = entity.IsAudit;
        //    history.IsCompleted = entity.IsCompleted;

        //    if (operation == "delete")
        //        history.IsLock = "-1";
        //    else
        //        history.IsLock = entity.IsLock;

        //    history.Lifecycle = entity.Lifecycle;
        //    history.Modifier = entity.Modifier;
        //    history.ModifyTime = entity.ModifyTime;
        //    history.ReturnReason = entity.ReturnReason;
        //    history.ReturnType = entity.ReturnType;
        //    history.ShippingDocument = entity.ShippingDocument;
        //    history.ShippingDocuments = entity.ShippingDocuments;
        //    //history.SourceCarID = entity.SourceCarID;
        //    //history.SourceConStrength = entity.SourceConStrength;
        //    history.SourceCube = entity.SourceCube;
        //    //history.SourceProjectName = entity.SourceProjectName;
        //    history.SourceShipDocID = entity.SourceShipDocID;
        //    history.SourceShippingDocument = entity.SourceShippingDocument;
        //    //history.TargetConStrength = entity.TargetConStrength;
        //    //history.TargetProjectName = entity.TargetProjectName;
        //    history.TargetShipDocID = entity.TargetShipDocID;
        //    history.TargetShippingDocument = entity.TargetShippingDocument;
        //    history.TotalWeight = entity.TotalWeight;
        //    history.Version = entity.Version;
        //    history.Weight = entity.Weight;
        //    history.ParentID = entity.ID;
        //    history.operation = operation;
        //    history.operationnum = opertionnum;
        //    history.operationcube = operationcube;

        //    s = (s == null ? (new PublicService()) : s);
        //    s.TZRalationHistory.Add(history);
        //}
    }
}
