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
    public class TZRalationGHController : BaseController<TZRalationGH, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var data = this.service.Car.GetMixerCarListOrderByIDGH();
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
            if (r != null)
            {
                foreach (TZRalationGH p in ((ZLERP.Model.JqGridData<TZRalationGH>)((JsonResult)r).Data).rows)
                {
                    if (string.IsNullOrEmpty(p.SourceShipDocID))
                        continue;
                    shipdocids = shipdocids + p.SourceShipDocID + ",";
                }
            }
            List<ShippingDocumentGH> shiplists = this.service.ShippingDocumentGH.Query().Where(a => shipdocids.Contains(a.ID)).ToList();
            if (r != null && shiplists != null)
            {
                foreach (TZRalationGH p in ((ZLERP.Model.JqGridData<TZRalationGH>)((JsonResult)r).Data).rows)
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
        public JsonResult GetLastDocByCarId(string carId)
        {
            TZRalationGH tz = this.service.TZRalationGH.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false)
                                .FirstOrDefault();

            if (tz != null)
            {//有未完成的退转料记录

                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords_NO, null);
            }
            else
            {
                ShippingDocumentGH doc = this.service.ShippingDocumentGH.Query()
                    .Where(p => p.CarID == carId && p.IsEffective)
                    .OrderByDescending(p => p.BuildTime)
                    .FirstOrDefault();
                if (doc != null && this.service.TZRalationGH.Query()
                                    .Where(p => p.SourceShipDocID == doc.ID)
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
        public JsonResult GetLastDocByCarId1(string carId)
        {

            //isLock为0,1才能合车
            TZRalationGH tz = this.service.TZRalationGH.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false)
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
        public JsonResult GetLastDocAutoByCarId(string carId)
        {
            TZRalationGH tz = this.service.TZRalationGH.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false && p.AH == Model.Enums.Consts.Auto && (p.ActionType == string.Empty || p.ActionType == null))
                                .OrderByDescending(p => p.BuildTime)
                                .FirstOrDefault();

            if (tz != null)
            {
                ShippingDocumentGH doc = this.service.ShippingDocumentGH.Query().Where(p => p.ID == tz.SourceShipDocID).FirstOrDefault();
                return OperateResult(true, string.Empty , doc);
            }
            else
            {
                return OperateResult(false, string.Empty , null);
            }
        }



        [HttpPost]
        public JsonResult GetRemaincubeByCarId(string carId)
        {
           
                //判断该车是否有未处理的转退料（自动过磅才用到）
            TZRalationGH objchk = this.service.TZRalationGH.Query()
                   .Where(p => p.CarID == carId && p.IsCompleted == false ).OrderByDescending(p => p.BuildTime)
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
                            return OperateResult(false, "",objchk );
                        }
                        else
                        {
                            return OperateResult(false, "", null);
                        }
                    }

                }

                TZRalationGH obj = this.service.TZRalationGH.Query()
                    .Where(p => p.CarID == carId && p.IsCompleted == false && p.ActionType == "AT1").FirstOrDefault();
                if (obj == null)
                {
                    return OperateResult(false, "", 0);
                }
                else
                {
                    return OperateResult(true, "本车有转退料记录，方量为:" + obj.Cube.ToString(), obj.Cube);
                }

        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Audit(int id)
        {
            bool ret = this.service.TZRalationGH.Audit(id);
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
  
            ShippingDocumentGH ret = this.service.TZRalationGH.Forward(sourceShipDocID, targetTaskId, isOriginComplete, isTrashOrigin, returnReason, out message, Cube, ReduceConStrength, FreightSubsidy, _caridz);
            
            return OperateResult(ret!=null, message, ret);
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
                bool ret = this.service.TZRalationGH.Split(sourceShipDocID, sourceCube, returnType, returnReason, carIDArr, carCubeArr, actionTypeArr);
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
                bool ret = this.service.TZRalationGH.Merge(sourceShipDocIDArr, sourceCubeArr, returnTypeArr, returnReason, carID, cube, actionType);
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
                bool ret = this.service.TZRalationGH.MergeZT(sourceShipDocIDArr, sourceCubeArr, returnTypeArr, returnReason, carID, cube, actionType, returnTypeArrTarget);
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
            int unComplete = this.service.TZRalationGH.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false )
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
        public ActionResult MarkAction(TZRalationGH tzRalationGH)
        {
            //若按照车号处理转退料方式
            if (tzRalationGH.ID == null)
            {
                TZRalationGH objchk = this.service.TZRalationGH.Query()
                   .Where(p => p.CarID == tzRalationGH.CarID && p.IsCompleted == false).OrderByDescending(p => p.BuildTime)
                   .FirstOrDefault();
                if(objchk != null)
                {
                    tzRalationGH.ActionType = objchk.ActionType;
                    tzRalationGH.ReturnType = objchk.ReturnType;
                    tzRalationGH.ReturnReason = objchk.ReturnReason;
                }
            }

            if (tzRalationGH.ActionType == Model.Enums.ActionType.Reject)     //若选择报废，则处理完成
            {
                tzRalationGH.IsCompleted = true;
            }

            tzRalationGH.IsLock = "0";
            ActionResult r = base.Update(tzRalationGH);

            TZRalationGH op = null;
            s = s == null ? (new PublicService()) : s;
            if (tzRalationGH.ID != null)
                op = this.service.TZRalationGH.Query()
                   .Where(p => p.ID == tzRalationGH.ID && p.IsCompleted == false).OrderByDescending(p => p.BuildTime)
                   .FirstOrDefault();
            if (op != null)
            {
                s.TZRalationHistoryGH.AddHistory(op, "update", op.CarID, op.Cube, "3");
            }
            else
            {
                s.TZRalationHistoryGH.AddHistory(tzRalationGH, "update", tzRalationGH.CarID, tzRalationGH.Cube, "3");
            }

            return r;
        }

        public override ActionResult Add(TZRalationGH entity)
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


            return r;
        }

        public override ActionResult Update(TZRalationGH entity)
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
            TZRalationGH unComplete = this.service.TZRalationGH.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false && p.IsLock == "2")
                                .FirstOrDefault();

            if (unComplete != null)
            {//有未完成的退转料合车记录
                return OperateResult(false, Lang.TZRelation_Error_HasUnCompletedRecords, null);
            }
            else
            {
                unComplete = this.service.TZRalationGH.Query()
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
            int unComplete = this.service.TZRalationGH.Query()
                                .Where(p => p.CarID == carId && p.IsCompleted == false&&p.IsLock=="2")
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
                bool ret = this.service.TZRalationGH.SplitByZT(ID, sourceShipDocID, _sourceCube, returnType, returnReason, carIDArr, carCubeArr, actionTypeArr, tzrFlag, ref msg);

                return OperateResult(ret, ret ? Lang.Msg_Operate_Success : msg, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }


        public virtual ActionResult GetTagCube(string carid, string targetTaskId)
        {
            ShippingDocumentGH ship = this.service.ShippingDocumentGH.Query()
                    .Where(p => p.CarID == carid && p.IsEffective)
                    .OrderByDescending(p => p.BuildTime)
                    .FirstOrDefault();//获取源运输单
            if (ship != null && this.service.TZRalationGH.Query()
                                .Where(p => p.SourceShipDocID.Contains(ship.ID) && p.IsCompleted == false)
                                .Count() > 0)
            {
                return OperateResult(false, Lang.TZRelation_Error_HasTZRelationRecords, null);
            }
            ProduceTaskGH task = this.service.ProduceTaskGH.Get(targetTaskId);
            ContractItemGH contract = task.ContractGH.ContractItemsGH.Where(a => a.ConStrength == task.ConStrength).OrderByDescending(p => p.BuildTime).FirstOrDefault();
            ContractItemGH oldContr = ship.ProduceTaskGH.ContractGH.ContractItemsGH.Where(a => a.ConStrength == ship.ProduceTaskGH.ConStrength).OrderByDescending(p => p.BuildTime).FirstOrDefault();
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
            TZRalationGH tzralation = new TZRalationGH();
            tzralation.Cube = ship.ParCube;
            tzralation.ReduceConStrength = OldUnPumpPrice - UnPumpPrice;
            tzralation.FreightSubsidy = 0.00m;
            return OperateResult(true, "", tzralation);
        }


        PublicService s;
        
    }
}