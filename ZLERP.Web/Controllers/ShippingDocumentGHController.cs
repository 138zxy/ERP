using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Model.Enums;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using System.Collections;
using ZLERP.Model.ViewModels;

namespace ZLERP.Web.Controllers
{
    public class ShippingDocumentGHController : BaseController<ShippingDocumentGH, string>
    {
        public override ActionResult Index()
        {
            var carList = this.service.Car.GetMixerCarListOrderByIDGH();
            var pumpList = this.service.Car.GetPumpList().OrderBy(p => p.ID);
            //发货单模版列表
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p=>p.OrderNum)
                .ToList();

            ViewBag.TicketNoConfig = this.service.SysConfig.GetSysConfig(SysConfigEnum.IsAllowBlankForTicketNo);
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            ViewBag.PumpDriverList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '01' AND IsUsed=1 ", "TrueName", true, "");

            //机组信息列表
            var pllist = HelperExtensions.SelectListData<ProductLine>("ProductLineName", "ID", "IsUsed=1 and IsGH=1 ", "ID", true, "");
            ViewBag.ProductLineList = pllist;

            return base.Index();
        }


        public ActionResult SettleIndex()
        {
            base.InitCommonViewBag();
            var carList = this.service.Car.GetMixerCarListOrderByID();
            var pumpList = this.service.Car.GetPumpList().OrderBy(p => p.ID);
            //发货单模版列表
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p => p.OrderNum)
                .ToList();

            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");

            return base.View();
        }
        /// <summary>
        /// 获取运输车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getCarNoList()
        {
            var carList = this.service.Car.GetMixerCarListOrderByIDGH();
            return Json(carList);
        }
        //获取泵名称列表
        public ActionResult getPumpList()
        {
            var carList = this.service.Car.GetPumpList().OrderBy(p => p.ID);
            return Json(carList);
        }
        //获取司机列表
        public ActionResult getDriverList()
        {
            var list = this.service.GetGenericService<User>().All().Where(p => p.UserType == "01").OrderBy(p=>p.TrueName).ToList(); 
            return Json(list);
        }
        /// <summary>
        /// 创建打印发货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult CreateDoc(ShippingDocumentGH entity)
        {
            //entity.IsEffective = true;
            //entity.DeliveryTime = DateTime.Now; 
            List<SysConfig> config = this.service.SysConfig.Query().Where(m => m.ConfigName == "EnterpriseName").ToList();
            //修改  如果是水票，拖泵票，移泵票 0是发货单
            if (entity.ShipDocType != "0" && entity.ShipDocType != "成品仓")
            {
                if (entity.ShipDocType == "1")
                    entity.ShipDocType = "水票";
                entity.SupplyUnit = (config == null || config.Count == 0 ? "" : config[0].ConfigValue) + entity.ShipDocType;
                //生产线不要显示
                //entity.ProductLineID = "";
                entity.ProductLineName = "";
               
                //浇筑方式不要显示
                entity.CastMode = "";
                //前厂工长 电话不显示
                entity.LinkMan = "";
                entity.Tel = "";
                //操作员 质检员 不要显示
                entity.Operator = "";
                entity.Surveyor = "";
                //调度员显示当前
                entity.Signer = AuthorizationService.CurrentUserID;
            }else if (entity.ShipDocType == "成品仓")
            {
                IList<FinishedGoodsWarehouseCount> FinishedGoodsList = this.service.GetGenericService<FinishedGoodsWarehouseCount>().Query().Where(a=>a.SiloNo==entity.SiloNo&&a.ProductLineID==entity.ProductLineID).ToList();
                if (FinishedGoodsList.Count>0&&entity.ShippingCube>FinishedGoodsList[0].StoreCount)
                {
                    return OperateResult(false, "运输量不得大于成品仓余量", null);
                }
                entity.SupplyUnit = (config == null || config.Count == 0 ? "" : config[0].ConfigValue) + entity.ShipDocType;
                entity.Signer = AuthorizationService.CurrentUserID;
            }

            if (!string.IsNullOrEmpty(entity.ID))
            {
                return base.Update(entity);
            }
            else 
            {
                return base.Add(entity);
            }
        }

        /// <summary>
        /// 新建单据
        /// </summary>
        /// <returns></returns>
        public ActionResult NewShipDoc()
        {
            InitCommonViewBag();
            var carList = this.service.Car.GetPrintBillCarListGH();
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            var pumpList = this.service.Car.GetPumpList();
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            //生产线
            var productline = this.service.ProductLine.All("IsUsed = 1 and IsGH = 1 ", "ID", true);
            ViewBag.productLineDics = new SelectList(productline, "ID", "ProductLineName");
            string startDate;
            string endDate;
            this.service.SysConfig.GetTodayDateRange(out startDate, out endDate);
            string condition = "IsCompleted = 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            var tasklist = this.service.ProduceTaskGH.All(condition, "ID", true);
            foreach (ProduceTaskGH item in tasklist)
            {
                item.ProjectName = string.Format("{0}-{1}-{2}-{3}", item.ProjectName, item.ConStrength, item.ConsPos, item.CastMode);
            }
            tasklist.Insert(0, new ProduceTaskGH());
            ViewBag.taskDics = new SelectList(tasklist, "ID", "ProjectName");
            //发货单模板
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p => p.OrderNum)
                .ToList();
            return View("NewShipDoc");
        }


        public ActionResult NewQitaShipDoc()
        {
            InitCommonViewBag();
            var carList = this.service.Car.GetPrintBillCarListGH().Where(a => a.CarStatus == CarStatus.AllowShipCar).ToList();
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            var pumpList = this.service.Car.GetPumpList();
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            //生产线
            var productline = this.service.ProductLine.All("IsUsed = 1 and IsGH = 1 ", "ID", true);
            ViewBag.productLineDics = new SelectList(productline, "ID", "ProductLineName");
            //成品仓
            IList<FinishedGoodsWarehouseCount> FinishedGoodsList = this.service.GetGenericService<FinishedGoodsWarehouseCount>().Query().Where(a=>a.ProductLineID=="001"
                && a.SiloNo<5).ToList();
            ViewBag.FinishedGoodsList = new SelectList(FinishedGoodsList, "ID", "SiloName");
            string startDate;
            string endDate;
            this.service.SysConfig.GetTodayDateRange(out startDate, out endDate);
            string condition = "IsCompleted = 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            var tasklist = this.service.ProduceTaskGH.All(condition, "ID", true);
            foreach (ProduceTaskGH item in tasklist)
            {
                item.ProjectName = string.Format("{0}-{1}-{2}-{3}", item.ProjectName, item.ConStrength, item.ConsPos, item.CastMode);
            }
            tasklist.Insert(0, new ProduceTaskGH());
            ViewBag.taskDics = new SelectList(tasklist, "ID", "ProjectName");
            //发货单模板
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p => p.OrderNum)
                .ToList();
            return View("NewQitaShipDoc");
        }


        public ActionResult GetFinishedGoodsByProId(string ProductLineID)
        {
            //成品仓
            IList<FinishedGoodsWarehouseCount> FinishedGoodsList = this.service.GetGenericService<FinishedGoodsWarehouseCount>().Query().Where(a => a.ProductLineID == ProductLineID&&a.SiloNo<5).ToList();
            
            if (FinishedGoodsList != null)
            {
                dynamic List = new
                {
                    Result = true
                    ,
                    Message = string.Empty
                    ,
                    FinishedGoodsList = FinishedGoodsList.Select(p => new
                    {
                        SiloNo = p.SiloNo,
                        SiloName = p.SiloName
                    }).ToList()
                };
                return this.Json(List);
            }
            return OperateResult(false, Lang.Msg_ShippingDoc_NotExist, null);
        }

        /// <summary>
        /// 获取最后一个发货单进行赋值
        /// </summary>
        /// <param name="taskid">任务单号</param>
        /// <param name="shipDocType">票据类型</param>
        /// <param name="carid">车号</param>
        /// <returns></returns>
        public ActionResult GetLastPrintDocByTaskId(string taskid, string shipDocType, string carid)
        {
            ShippingDocumentGH doc = null;
            if (shipDocType == "0")
                doc = this.service.ShippingDocumentGH.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective && s.ShipDocType == shipDocType).OrderByDescending(s => s.BuildTime).FirstOrDefault();
            else if (!string.IsNullOrEmpty(carid))
                doc = this.service.ShippingDocumentGH.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective && (s.ShipDocType == "0" || s.ShipDocType == "成品仓")).OrderByDescending(s => s.BuildTime).FirstOrDefault();
            else
                doc = this.service.ShippingDocumentGH.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective).OrderByDescending(s => s.BuildTime).FirstOrDefault();
            if (doc != null)
            {
                if (shipDocType == "成品仓" && carid == null)
                {
                    var carlist = this.service.ShippingDocumentGH.Query().Where(s => s.TaskID == taskid && s.IsEffective && s.ShipDocType == "成品仓").GroupBy(a => a.CarID).ToList();
                    var list = this.service.ShippingDocumentGH.Query().Where(s => s.TaskID == taskid && s.IsEffective && s.ShipDocType == "成品仓").ToList();
                    int? ProvidedTimes = 0;
                    decimal ProvidedCube=0.00M;
                    foreach (var item in carlist)
                    {
                        ProvidedTimes += list.Where(a => a.CarID == item.Key).OrderByDescending(a=>a.ID).FirstOrDefault().ProvidedTimes;
                        ProvidedCube += list.Where(a => a.CarID == item.Key).OrderByDescending(a => a.ID).FirstOrDefault().ProvidedCube;
                    }
                    doc.ProvidedTimes = ProvidedTimes;
                    doc.ProvidedCube = ProvidedCube;
                }
                doc.Remark = doc.Remark != null && doc.Remark.IndexOf("CODEADD") >= 0 ? doc.Remark.Remove(doc.Remark.IndexOf("CODEADD")) : doc.Remark;
                return OperateResult(true, Lang.Msg_Operate_Success, doc);
            }
            if (shipDocType == "成品仓" && carid == null)
            {
                ProduceTaskGH task=this.service.GetGenericService<ProduceTaskGH>().Query().Where(a=>a.ID==taskid).ToList()[0];
                doc = new ShippingDocumentGH();
                doc.TaskID = task.ID;
                doc.ShipDocType = "成品仓";
                doc.ProjectName = task.ProjectName;
                doc.CustMixpropID = task.CustMixpropID;
                doc.ConsMixpropID = task.ConsMixpropsGH[0].ID;
                doc.ProjectAddr = task.ProjectAddr;
                doc.ProjectID = task.ProjectID;
                doc.ProduceDate = DateTime.Now;
                doc.ContractID = task.ContractID;//合同ID
                doc.ContractName = task.ProjectName;//合同名称
                doc.DeliveryTime = DateTime.Now;//发车时间
                doc.CustomerID = task.ContractGH.CustomerID;//客户名称
                doc.CustName = task.ContractGH.CustName;//客户名称
                doc.ConStrength = task.ConStrength;//砼强度
                doc.ConstructUnit = task.ConstructUnit;//施工单位
                doc.CastMode = task.CastMode;//浇注方式
                doc.ProjectAddr = task.ProjectAddr;//项目地址
                doc.ConsPos = task.ConsPos;//施工部位
                 
                doc.PlanCube = task.PlanCube;//合同名称
                doc.LinkMan = task.LinkMan;//合同名称
                doc.Surveyor = task.Auditor;//合同名称
                //doc.EntrustUnit = task.EntrustUnit;//合同名称
                doc.SupplyUnit = task.SupplyUnit;//合同名称
                doc.Signer = AuthorizationService.CurrentUserID;//调度员
                return OperateResult(true, Lang.Msg_Operate_Success, doc);
            }
            return OperateResult(false, Lang.Msg_ShippingDoc_NotExist, null);
        }
        
        /// <summary>
        /// 根据任务单获取最后表单信息，包括生产线、已供、累计车数等。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public ActionResult GetLastDocByTaskId(string id)
        {
            dynamic lastDoc = this.service.ShippingDocumentGH.GetLastDocByTaskId(id);
            return this.Json(lastDoc);
        }

        [HandleAjaxError]
        public ActionResult GetProductLinesByCompanyID(string id,string taskid)
        {
            //获取施工配比已经下发审核的记录
            IList<ConsMixpropGH> cm = this.service.ConsMixpropGH.All().Where(p => p.TaskID == taskid && p.AuditStatus == 1).ToList();
            ArrayList arr = new ArrayList();
            foreach (ConsMixpropGH c in cm)
            {
                arr.Add(c.ProductLineID);
            }
            IList<ProductLine> ps = this.service.ProductLine.All().ToList();
            //IList<ProductLine> ps = this.service.ProductLine.All().Where(p => p.CompanyID == Convert.ToInt32(id)).ToList();
            ps = ps.Where(a=>a.ID!="0"&&arr.Contains(a.ID)).ToList();
            return this.Json(ps);
        }




        [HandleAjaxError]
        public ActionResult GetCarsByCompanyID(string id)
        {
            IList<Car> carList = this.service.Car.Query()
                .Where(c => CarType.GH.Contains(c.CarTypeID))
                .Where(c => c.IsUsed == true).Where(c => c.CompanyID == (id == "" ? 0 : Convert.ToInt32(id))).Where(c => c.CarStatus == CarStatus.AllowShipCar)
                .OrderBy(c => c.OrderNum).OrderBy(c => c.ID).ToList();
            return this.Json(carList);
        }


        public ActionResult Garbage(string id, bool status, string remark) {
            try {
                if (this.service.ShippingDocumentGH.Get(id).ShipDocType=="成品仓")
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed + ": 成品仓不允许作废", null);
                }
                this.service.ShippingDocumentGH.Garbage(id, status, remark);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            
            }catch(Exception e){
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult Audit(string id, bool status)
        {
            try
            {
                this.service.ShippingDocumentGH.Audit(id, status);
                return OperateResult(true, Lang.Msg_Operate_Success, null);

            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }


        public ActionResult FindByRecord(JqGridRequest request, string condition = null)
        {
            if (string.IsNullOrEmpty(condition))
            {
                condition = "ShipDocID IN (SELECT DISTINCT ShipDocID FROM ProductRecord)";
            }
            else
                condition += " AND ShipDocID IN (SELECT DISTINCT ShipDocID FROM ProductRecord)";
            return base.Find(request,condition);
            
        }
        

        [HttpPost, HandleAjaxError]
        public virtual ActionResult SumByRecord(JqGridRequest request, string condition)
        {

            int total;
            IList<DailyReport> list = this.service.ShippingDocumentGH.GetShipDocList(request, condition, out total);

            JqGridData<DailyReport> data = new JqGridData<DailyReport>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = list
            };
            return Json(data);

        } 

        /// <summary>
        /// 手工增加运输单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult HandAdd(ShippingDocumentGH entity)
        {
            lock (customerObj)
            {
                //查询余额
                string taskid = entity.TaskID;
                ProduceTaskGH produceTaskGH = this.service.ProduceTaskGH.Get(taskid);
                string ConStrength = produceTaskGH.ConStrength;
                ContractGH contractGH = this.service.ContractGH.Get(produceTaskGH.ContractID);
                decimal money = Convert.ToDecimal(contractGH.Balance);
                decimal price = 0;
                //获得单价
                IList<ContractItemGH> list = this.service.ContractItemGH.Query().Where(m => m.ContractID == produceTaskGH.ContractID && m.ConStrength == ConStrength).ToList<ContractItemGH>();
                if (list == null || list.Count == 0)
                    return OperateResult(false, "没有查询到合同的砼价格，请先添加.", null);
                price = list[0].UnPumpPrice.Value;

                //是否可以创建或者修改
                price = price * produceTaskGH.PlanCube;
                if (price > -money)
                {
                    return OperateResult(false, string.Format("计划需要金额{0}，用户余额{1},请减方或者通知客户经理垫资。", price, -money), null);
                }

                if (entity.TaskID != null)
                {
                    ProduceTaskGH task = this.service.ProduceTaskGH.Get(entity.TaskID);
                    entity.CustMixpropID = task.CustMixpropID;
                    entity.CarpRadii = task.CarpRadii;
                    entity.CastMode = task.CastMode;
                    entity.CementBreed = task.CementBreed;
                    entity.ConsPos = task.ConsPos;
                    entity.ConStrength = task.ConStrength;
                    entity.ConstructUnit = task.ConstructUnit;
                    entity.ContractID = task.ContractID;
                    ContractGH contractentity = this.service.ContractGH.Get(task.ContractID);
                    entity.ContractNo = contractentity.ContractNo;

                    entity.ContractName = task.ContractGH.ContractName;
                    entity.CustName = task.ContractGH.CustName;
                    entity.CustomerID = task.ContractGH.CustomerID;
                    entity.Distance = task.Distance;
                    entity.ForkLift = task.ForkLift;
                    entity.ImdGrade = task.ImdGrade;
                    entity.ImpGrade = task.ImpGrade;
                    entity.ImyGrade = task.ImyGrade;
                    entity.LinkMan = task.LinkMan;
                    entity.RealSlump = task.Slump;
                    entity.SupplyUnit = task.SupplyUnit;
                    entity.Tel = task.Tel;

                    //余额部分
                    entity.isPay = 1;
                    entity.money = price;
                    if (entity.ProductLineID!=null)
                    {
                        ProductLine pentity = this.service.ProductLine.Get(entity.ProductLineID);
                        entity.ProductLineName = pentity == null ? "" : pentity.ProductLineName;
                    }
                    
                }
                return base.Add(entity);
            }
        }

        static object obj = new object();
        public override ActionResult Update(ShippingDocumentGH entity)
        {
            lock (obj)
            {
                //修复点
                ShippingDocumentGH s = this.service.ShippingDocumentGH.Query()
                    .Where(m => m.CarID == entity.CarID && m.BuildTime >= DateTime.Now.AddDays(-3))
                    .OrderByDescending(m => m.ID).FirstOrDefault();

                if (s != null && s.ID == entity.ID && entity.IsBack)
                {
                    ShippingDocumentGH temp = this.m_ServiceBase.Get(entity.ID);
                    string CarId = temp.CarID;
                    Car car = this.service.Car.Get(CarId);
                    if (car.CarStatus == CarStatus.ShippingCar)
                    {
                        this.service.Car.ShiftMixerCarStatusGH("Up", CarId, CarStatus.ShippingCar);
                    }
                }
                
                return base.Update(entity);
            }
        }

        [HandleAjaxError]
        public JsonResult CarReturn(string carId) {

            bool ret = this.service.ShippingDocumentGH.CarReturn(carId);
            return OperateResult(ret, Lang.Msg_Operate_Success, "");
        }

        /// <summary>
        /// 出厂检测
        /// </summary>
        /// <param name="id"></param>
        /// <param name="realSlump"></param>
        /// <returns></returns>
        public ActionResult OutCheck(string ID, string RealSlump, string MachineTemperature)
        {
            var shipDoc = this.service.ShippingDocumentGH.Get(ID);
            if (shipDoc != null)
            {
                try
                {
                    //修改真实坍落度和质检员
                    shipDoc.RealSlump = RealSlump;
                    shipDoc.Surveyor = this.service.ShippingDocumentGH.GetUserId(); 
                    this.service.ShippingDocumentGH.Update(shipDoc, null);

                    return OperateResult(true, Lang.Msg_Operate_Success, null);

                }
                catch (Exception e)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
                }
            }
            else
                return OperateResult(false, Lang.Msg_ShippingDoc_NotExist, null);
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit(string[] ids)
        {
            

            string idstrs = "";
            foreach (string id in ids)
            {
                if (idstrs == "")
                {
                    idstrs = id;
                }
                else
                {
                    idstrs = idstrs + "," + id;
                }
            }
            bool is_success = this.service.ShippingDocumentGH.ExecMuitAudit(idstrs, AuthorizationService.CurrentUserID);
            if (is_success)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            }
        }

        public ActionResult MultiAuditY(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ShippingDocumentGH sd = this.service.ShippingDocumentGH.Get(id);
                    if (!sd.y_IsAudit)
                    {
                        sd.y_IsAudit = true;
                        this.service.ShippingDocumentGH.Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult AuditY(string id)
        {
            try
            {
                ShippingDocumentGH sd = this.service.ShippingDocumentGH.Get(id);
                sd.y_IsAudit = !sd.y_IsAudit;
                this.service.ShippingDocumentGH.Update(sd);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 出厂过磅方量校正
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MetageUpdate(string ID, int TotalWeight, int CarWeight, int Exchange)
        {
            try
            {
                ShippingDocumentGH sd = this.service.ShippingDocumentGH.Get(ID);
                sd.TotalWeight = TotalWeight;
                sd.CarWeight = CarWeight;
                sd.Weight = TotalWeight - CarWeight;
                sd.Exchange = Exchange;
                sd.Cube = Math.Round(Convert.ToDecimal(sd.Weight / Exchange), 2);
                base.Update(sd);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        public ActionResult AuditReport()
        {
            return View(); 
        }

        /// <summary>
        /// 手动审核
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult HandleAudit(string taskid,string begin,string end)
        {
            try
            {
                IList<ShippingDocumentGH> list = this.service.ShippingDocumentGH.Query().Where(m => m.TaskID == taskid && m.IsAudit == false && m.ProduceDate >= Convert.ToDateTime(begin) && m.ProduceDate < Convert.ToDateTime(end)).ToList();

                foreach (ShippingDocumentGH doc in list)
                {
                    string id = doc.ID;
                    this.service.ShippingDocumentGH.Audit(id, true);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 混凝土抽样
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult Sampling(string id)
        {
            try
            {
                Lab_ConWPRecord r = new Lab_ConWPRecord();
                ShippingDocumentGH s = this.service.GetGenericService<ShippingDocumentGH>().Get(id);
                int c = this.service.GetGenericService<Lab_ConWPRecord>().All().Count(p => p.ShipDocID == id);
                if (c > 0)
                {
                    return OperateResult(false, "该运输单已抽样", null);
                }
                r.ShipDocID = id;
                r.SamplingDate = DateTime.Now;
                r.ConStrength = s.ConStrength;
                r.ProjectName = s.ProjectName;
                r.ConsPos = s.ConsPos;
                r.CarNo = s.CarID;
                r.ProductLine = s.ProductLineName;
                r.Slump = s.RealSlump;
                r.ExpansionDegree ="";
                r.SamplingMan = AuthorizationService.CurrentUserName;
                
                string labid= this.service.GetGenericService<Lab_ConWPRecord>().Add(r).ID;
                //插入检测报告
                //Lab_ConStrengthReport csReport = new Lab_ConStrengthReport();
                //csReport.Lab_ConWPRecordId = labid;
                //this.service.GetGenericService<Lab_ConStrengthReport>().Add(csReport);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }

        /// <summary>
        /// 生成抗压委托单
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult CreateCompComm(string id)
        {
            try
            {
                Lab_CompComm r = new Lab_CompComm();
                ShippingDocumentGH s = this.service.GetGenericService<ShippingDocumentGH>().Get(id);
                int c = this.service.GetGenericService<Lab_CompComm>().All().Count(p => p.ShipDocId == s.ID);
                if (c > 0)
                {
                    return OperateResult(false, "该运输单已生产抗压委托单", null);
                }
                r.ShipDocId = id;
                r.Remark = "";
                r.Builder = AuthorizationService.CurrentUserName;
                r.BuildTime = DateTime.Now;
                this.service.GetGenericService<Lab_CompComm>().Add(r);
                s.IsCompComm = true;
                this.service.GetGenericService<ShippingDocumentGH>().Update(s, null);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }
        /// <summary>
        /// 生成抗渗委托单
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult CreateImpComm(string id)
        {
            try
            {
                Lab_ImpComm r = new Lab_ImpComm();
                ShippingDocumentGH s = this.service.GetGenericService<ShippingDocumentGH>().Get(id);
                int c = this.service.GetGenericService<Lab_ImpComm>().All().Count(p => p.ShipDocId == s.ID);
                if (c > 0)
                {
                    return OperateResult(false, "该运输单已生产抗渗委托单", null);
                }
                r.ShipDocId = id;
                r.Remark = "";
                r.Builder = AuthorizationService.CurrentUserName;
                r.BuildTime = DateTime.Now;
                this.service.GetGenericService<Lab_ImpComm>().Add(r);
                s.IsImpComm = true;
                this.service.GetGenericService<ShippingDocumentGH>().Update(s, null);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }

        public override ActionResult Delete(string[] id)
        {
            foreach (object item in id)
            {
                ShippingDocumentGH ship = this.m_ServiceBase.Get(item);
                if (ship.ShipDocType=="成品仓")
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed + "成品仓运输车不允许删除", "");
                }
            }
            return base.Delete(id);
        }

        /// <summary>
        /// 运输单调价
        /// </summary>
        /// <param name="BeginDate">起始时间(包含)</param>
        /// <param name="EndDate">截止时间(包含)</param>
        /// <param name="ContractId">合同编号</param>
        /// <returns>执行完影响的行数</returns>
        public ActionResult RefreshShipDocPrice(ShipDocRefreshPrice ShipDocRefreshPriceGH)
        {
            try
            {
                int affectedRow = this.service.ShippingDocumentGH.RefreshShipDocPrice(ShipDocRefreshPriceGH.BeginDate, ShipDocRefreshPriceGH.EndDate, ShipDocRefreshPriceGH.ContractID);
                return OperateResult(true, Lang.Msg_Operate_Success, "共 [" + affectedRow + "] 条运输单调价成功!");
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, ex.Message);
            }
        }


        /// <summary>
        /// 运费调价
        /// </summary>
        /// <param name="ShipDocRefreshPrice"></param>
        /// <returns></returns>
        public ActionResult RefreshShipDocFreightGH(ShipDocRefreshPriceGH ShipDocRefreshPriceGH)
        {
            try
            {
                int affectedRow = this.service.ShippingDocumentGH.RefreshShipDocFreight(ShipDocRefreshPriceGH.BeginDate, ShipDocRefreshPriceGH.EndDate, ShipDocRefreshPriceGH.ContractID);
                return OperateResult(true, Lang.Msg_Operate_Success, "共 [" + affectedRow + "] 条运输单运费调价成功!");
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, ex.Message);
            }
        }


        /// <summary>
        /// 修改运输距离，重新计算运输单价
        /// </summary>
        /// <param name="shipDocID">运输单号</param>
        /// <param name="distance">距离</param>
        /// <returns></returns>
        //public ActionResult EditDistance(string ID, string CarID, decimal Distance)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(CarID) || Distance <= 0)
        //        {
        //            return OperateResult(false, Lang.Msg_OpenCheck_Failed, "无效的运输单号或者公里数");
        //        }
        //        var doc = this.service.ShippingDocumentGH.Get(ID);
        //        doc.Distance = Distance;
        //        //计算运费单价
        //        //查询车的类型 
        //        var car = this.service.Car.Get(CarID);
        //        //查询对于距离的价格
        //        var freight = this.service.Freight.Query().Where(t => t.FreightType == car.FreightType && t.StartKiloNum <= Distance && t.EndKiloNum > Distance).FirstOrDefault();
        //        if (freight != null)
        //        {
        //            doc.CarFreight = freight.FrePrice;
        //        }
        //        this.service.ShippingDocumentGH.Update(doc, null);

        //        return OperateResult(true, Lang.Msg_Operate_Success, "修改成功!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return OperateResult(false, Lang.Msg_OpenCheck_Failed, ex.Message);
        //    }
        //}
    }
}
