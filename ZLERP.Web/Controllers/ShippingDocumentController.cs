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
using System.Data;

namespace ZLERP.Web.Controllers
{
    public class ShippingDocumentController : BaseController<ShippingDocument, string>
    {
        /// <summary>
        /// 对象锁
        /// </summary>
        private object objLock = new object();

        public override System.Web.Mvc.ActionResult Index()
        {
            var carList = this.service.Car.GetMixerCarListOrderByID();
            var pumpList = this.service.Car.GetPumpList().OrderBy(p => p.ID);
            //发货单模版列表
            var PrintReportList = this.service.GetGenericService<ZLERP.Model.SystemManage.PrintReport>().All(new List<string> { "ReportNo", "ReportName" }, "ReportType='运输单' AND ReportNo LIKE '%ShippingDocument%'", "ReportNo", true);
            //ViewBag.ShipTempItems = this.service.Dic.Query()
            //    .Where(p => p.ParentID == "ShipDocTemplate")
            //    .OrderBy(p => p.OrderNum)
            //    .ToList();
            ViewBag.ShipTempItems = new SelectList(PrintReportList, "ReportNo", "ReportName");
            //ViewBag.ShipTempItems = HelperExtensions.SelectListData<ZLERP.Model.SystemManage.PrintReport>("ReportName", "ReportNo", " ReportType='运输单' AND ReportNo LIKE '%ShippingDocument%' ", "TreeCode", true, "");
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            ViewBag.ShipDocType = HelperExtensions.SelectListData<Dic>("DicName", "TreeCode", " ParentID = 'ShipDocType' ", "TreeCode", true, "");

            return base.Index();
        }

        public System.Web.Mvc.ActionResult IndexSH()
        {
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
            ViewBag.ShipDocType = HelperExtensions.SelectListData<Dic>("DicName", "TreeCode", " ParentID = 'ShipDocType' ", "TreeCode", true, "");
            return base.Index();
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            return base.Find(request, condition);
        }

        /// <summary>
        /// 获取运输车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getCarNoList()
        {
            var carList = this.service.Car.GetMixerCarListOrderByID();
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
            var list = this.service.GetGenericService<User>().All().Where(p => p.UserType == "01").ToList();
            return Json(list);
        }
        /// <summary>
        /// 创建打印发货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult CreateDoc(ShippingDocument entity)
        {
            //entity.IsEffective = true;
            //entity.DeliveryTime = DateTime.Now; 

            //修改  如果是水票，拖泵票，移泵票 0是发货单
            if (entity.ShipDocType != "0")
            {
                if (entity.ShipDocType == "1")
                    entity.ShipDocType = "水票";

                List<SysConfig> config = this.service.SysConfig.Query().Where(m => m.ConfigName == "EnterpriseName").ToList();
                entity.SupplyUnit = (config == null || config.Count == 0 ? "" : config[0].ConfigValue) + entity.ShipDocType;
                //生产线不要显示
                //entity.ProductLineID = "";
                //entity.ProductLineName = "";
                ////泵名称不要显示
                //entity.PumpName = "";
                ////浇筑方式不要显示
                //entity.CastMode = "";
                ////前厂工长 电话不显示
                //entity.LinkMan = "";
                //entity.Tel = "";
                ////操作员 质检员 不要显示
                //entity.Operator = "";
                //entity.Surveyor = "";
                //调度员显示当前
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
            var carList = this.service.Car.GetPrintBillCarList();
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            var pumpList = this.service.Car.GetPumpList();
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            //生产线
            var productline = this.service.ProductLine.All("IsUsed = 1", "ID", true);
            ViewBag.productLineDics = new SelectList(productline, "ID", "ProductLineName");
            string startDate;
            string endDate;
            this.service.SysConfig.GetTodayDateRange(out startDate, out endDate);
            string condition = "IsCompleted = 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            var tasklist = this.service.ProduceTask.All(condition, "ID", true);
            foreach (ProduceTask item in tasklist)
            {
                item.ProjectName = string.Format("{0}-{1}-{2}-{3}", item.ProjectName, item.ConStrength, item.ConsPos, item.CastMode);
            }
            tasklist.Insert(0, new ProduceTask());
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
            var carList = this.service.Car.GetPrintBillCarList();
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            var pumpList = this.service.Car.GetPumpList();
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            //生产线
            var productline = this.service.ProductLine.All("IsUsed = 1", "ID", true);
            ViewBag.productLineDics = new SelectList(productline, "ID", "ProductLineName");
            string startDate;
            string endDate;
            this.service.SysConfig.GetTodayDateRange(out startDate, out endDate);
            string condition = "IsCompleted = 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            var tasklist = this.service.ProduceTask.All(condition, "ID", true);
            foreach (ProduceTask item in tasklist)
            {
                item.ProjectName = string.Format("{0}-{1}-{2}-{3}", item.ProjectName, item.ConStrength, item.ConsPos, item.CastMode);
            }
            tasklist.Insert(0, new ProduceTask());
            ViewBag.taskDics = new SelectList(tasklist, "ID", "ProjectName");
            //发货单模板
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p => p.OrderNum)
                .ToList();
            return View("NewQitaShipDoc");
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
            ShippingDocument doc = null;
            if (shipDocType == "0")
                doc = this.service.ShippingDocument.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective && s.ShipDocType == shipDocType).OrderByDescending(s => s.ID).FirstOrDefault();
            else if (!string.IsNullOrEmpty(carid))
                doc = this.service.ShippingDocument.Query()
                .Where(s => s.CarID == carid && s.IsEffective && s.ShipDocType == shipDocType).OrderByDescending(s => s.ID).FirstOrDefault();
            else
                doc = this.service.ShippingDocument.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective).OrderByDescending(s => s.ID).FirstOrDefault();
            var task = this.service.ProduceTask.Query().FirstOrDefault(t => t.ID == taskid);
            if (doc != null)
            {
                doc.ExceptionInfo = doc.ExceptionInfo != null && doc.ExceptionInfo.IndexOf("CODEADD") >= 0 ? doc.ExceptionInfo.Remove(doc.ExceptionInfo.IndexOf("CODEADD")) : doc.ExceptionInfo;
                if (task != null)
                {
                    doc.ConStrength = task.ConStrength == null ? doc.ConStrength : task.ConStrength;
                    doc.CastMode = task.CastMode == null ? doc.CastMode : task.CastMode;
                    doc.ConsPos = task.ConsPos == null ? doc.ConsPos : task.ConsPos;
                    doc.RealSlump = task.RealSlump == null ? doc.RealSlump : task.RealSlump;
                }
                //doc.ConStrength
                return OperateResult(true, Lang.Msg_Operate_Success, doc);
            }
            else
            {
                //var task = this.service.ProduceTask.Query().FirstOrDefault(t => t.ID == taskid);
                if (task != null)
                {
                    doc = new ShippingDocument();
                    doc.ShipDocType = shipDocType;
                    doc.ProjectName = task.ProjectName;
                    doc.ProduceDate = DateTime.Now;
                    doc.ContractName = task.Contract.ContractName;
                    doc.DeliveryTime = DateTime.Now;
                    doc.CustName = task.CustName;
                    doc.ConStrength = task.ConStrength;
                    doc.ConstructUnit = task.ConstructUnit;
                    doc.CastMode = task.CastMode;
                    doc.ProjectAddr = task.ProjectAddr;
                    doc.ConsPos = task.ConsPos;
                    doc.RealSlump = task.RealSlump;
                    doc.LinkMan = task.LinkMan;
                    doc.Tel = task.Tel;
                    doc.SupplyUnit = task.SupplyUnit;
                    doc.TaskID = task.ID;
                    doc.ProjectID = task.ProjectID;
                    doc.CustomerID = task.Contract.CustomerID;
                    doc.ContractID = task.ContractID;
                    doc.CustMixpropID = task.CustMixpropID;
                    doc.IsEffective = true;
                    doc.ProvidedTimes = 0;
                    return OperateResult(true, Lang.Msg_Operate_Success, doc);

                }
            }
            return OperateResult(false, Lang.Msg_ShippingDoc_NotExist, null);
        }


        /// <summary>
        /// 根据任务单获取最后表单信息，包括生产线、已供、累计车数等。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HandleAjaxError]
        //public ActionResult GetLastDocByTaskId(string id, int? companyId)
        //{ 
        //    dynamic lastDoc = this.service.ShippingDocument.GetLastDocByTaskId(id, companyId);

        //    return this.Json(lastDoc);
        //}

        [HandleAjaxError]
        public ActionResult GetLastDocByTaskId(string id, int? companyId, int? dataType = 0)
        {
            dynamic lastDoc = this.service.ShippingDocument.GetLastDocByTaskId(id, companyId, dataType);

            return this.Json(lastDoc);
        }

        public ActionResult Garbage(string id, bool status, string remark)
        {
            try
            {
                this.service.ShippingDocument.Garbage(id, status, remark);
                return OperateResult(true, Lang.Msg_Operate_Success, null);

            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult Audit(string id, bool status)
        {
            try
            {
                //status==true 表示，有审核状态变为反审
                if (status)
                {
                    var query = this.service.GetGenericService<ZLERP.Model.Beton.B_BalanceDel>().Query().FirstOrDefault(t => t.ShipDocID == id);
                    if (query != null)
                    {
                        return OperateResult(false, "已经生成砼款结算单，不能反审", null);
                    }
                }
                this.service.ShippingDocument.Audit(id, status);
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
            return base.Find(request, condition);

        }


        [HttpPost, HandleAjaxError]
        public virtual ActionResult SumByRecord(JqGridRequest request, string condition)
        {

            int total;
            IList<DailyReport> list = this.service.ShippingDocument.GetShipDocList(request, condition, out total);

            JqGridData<DailyReport> data = new JqGridData<DailyReport>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = list
            };
            return Json(data);

        }


        public ActionResult HandAdd(ShippingDocument entity)
        {
            if (entity.TaskID != null)
            {
                ProduceTask task = this.service.ProduceTask.Get(entity.TaskID);
                entity.CustMixpropID = task.CustMixpropID;
                entity.CarpRadii = task.CarpRadii;
                entity.CastMode = task.CastMode;
                entity.CementBreed = task.CementBreed;
                entity.ConsPos = task.ConsPos;
                entity.ConStrength = task.ConStrength;
                entity.ConstructUnit = task.ConstructUnit;
                entity.ContractID = task.ContractID;
                entity.ContractName = task.Contract.ContractName;
                entity.CustName = task.Contract.CustName;
                entity.CustomerID = task.Contract.CustomerID;
                entity.Distance = task.Distance;
                entity.ForkLift = task.ForkLift;
                entity.ImdGrade = task.ImdGrade;
                entity.ImpGrade = task.ImpGrade;
                entity.ImyGrade = task.ImyGrade;
                entity.LinkMan = task.LinkMan;
                entity.RealSlump = task.Slump;
                entity.SupplyUnit = task.SupplyUnit;
                entity.Tel = task.Tel;
            }
            return base.Add(entity);
        }

        static object obj = new object();
        public override ActionResult Update(ShippingDocument entity)
        {
            lock (obj)
            {
                var ship = this.service.ShippingDocument.Query().FirstOrDefault(t => t.ID == entity.ID);
                if (ship != null && ship.IsAudit)
                {
                    return OperateResult(false, "已审核的单据不能再修改", null);
                }
                //修复点
                ShippingDocument s = this.service.ShippingDocument.Query()
                    .Where(m => m.CarID == entity.CarID && m.BuildTime >= DateTime.Now.AddDays(-3))
                    .OrderByDescending(m => m.ID).FirstOrDefault();

                if (s != null && s.ID == entity.ID && entity.IsBack)
                {
                    ShippingDocument temp = this.m_ServiceBase.Get(entity.ID);
                    string CarId = temp.CarID;
                    Car car = this.service.Car.Get(CarId);
                    if (car.CarStatus == CarStatus.ShippingCar)
                    {
                        this.service.Car.ShiftMixerCarStatus("Up", CarId, CarStatus.ShippingCar);
                    }
                }
                return base.Update(entity);
            }
        }

        public ActionResult UpdateY(ShippingDocument entity)
        {
            lock (obj)
            {
                var ship = this.service.ShippingDocument.Query().FirstOrDefault(t => t.ID == entity.ID);
                if (ship != null && ship.y_IsAudit)
                {
                    return OperateResult(false, "已审核的单据不能再修改", null);
                }
                return base.Update(entity);
            }
        }
        [HandleAjaxError]
        public JsonResult CarReturn(string carId)
        {

            bool ret = this.service.ShippingDocument.CarReturn(carId);
            return OperateResult(ret, Lang.Msg_Operate_Success, "");
        }

        /// <summary>
        /// 出厂检测
        /// </summary>
        /// <param name="id"></param>
        /// <param name="realSlump"></param>
        /// <returns></returns>
        public ActionResult OutCheck(string id, string realSlump)
        {
            var shipDoc = this.service.ShippingDocument.Get(id);
            if (shipDoc != null)
            {
                try
                {
                    //修改真实坍落度和质检员
                    shipDoc.RealSlump = realSlump;
                    shipDoc.Surveyor = this.service.ShippingDocument.GetUserId();
                    this.service.ShippingDocument.Update(shipDoc);
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
            //try
            //{
            //    foreach (string id in ids)
            //    {
            //         this.service.ShippingDocument.Audit(id, false);
            //    }
            //    return OperateResult(true, Lang.Msg_Operate_Success, null);
            //}
            //catch (Exception e)
            //{
            //    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            //}

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
            bool is_success = this.service.ShippingDocument.ExecMuitAudit(idstrs, AuthorizationService.CurrentUserID);
            if (is_success)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            }
        }

        /// <summary>
        /// 二次审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit2(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ShippingDocument sd = this.service.ShippingDocument.Get(id);
                    if (sd.AuditStatus2==0)
                    {
                        sd.AuditStatus2 = 1;
                        this.service.ShippingDocument.Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult MultiAuditY(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ShippingDocument sd = this.service.ShippingDocument.Get(id);
                    if (!sd.y_IsAudit)
                    {
                        sd.y_IsAudit = true;
                        this.service.ShippingDocument.Update(sd);
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
                ShippingDocument sd = this.service.ShippingDocument.Get(id);
                sd.y_IsAudit = !sd.y_IsAudit;
                this.service.ShippingDocument.Update(sd);
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
                ShippingDocument sd = this.service.ShippingDocument.Get(ID);
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
        public ActionResult HandleAudit(string taskid, string begin, string end)
        {
            try
            {
                IList<ShippingDocument> list = this.service.ShippingDocument.Query().Where(m => m.TaskID == taskid && m.IsAudit == false && m.ProduceDate >= Convert.ToDateTime(begin) && m.ProduceDate < Convert.ToDateTime(end)).ToList();

                foreach (ShippingDocument doc in list)
                {
                    string id = doc.ID;
                    this.service.ShippingDocument.Audit(id, true);
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
        public ActionResult Sampling(string id, string type)
        {
            try
            {
                Lab_ConWPRecord r = new Lab_ConWPRecord();
                ShippingDocument s = this.service.GetGenericService<ShippingDocument>().Get(id);
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
                r.ExpansionDegree = "";
                r.SamplingMan = AuthorizationService.CurrentUserName;

                string labid = this.service.GetGenericService<Lab_ConWPRecord>().Add(r).ID;
                //插入检测报告

                string[] ReportTypes = type.Split(',');
                for (int i = 0; i < ReportTypes.Length; i++)
                {
                    if (ReportTypes[i] != null && ReportTypes[i].ToString() != "")
                    {
                        Lab_ConWPRecordItems Lab_Itmes = new Lab_ConWPRecordItems();
                        Lab_Itmes.Lab_ConWPRecordId = labid;
                        Lab_Itmes.ReportType = ReportTypes[i].ToString();
                        this.service.GetGenericService<Lab_ConWPRecordItems>().Add(Lab_Itmes);
                    }
                }
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
        public ActionResult SettleIndex(int dataType)
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
            if (dataType == 0)
            {
                ViewBag.dataType = 0;
                ViewBag.yunshu = 0;
            }
            if (dataType == 1)
            {
                ViewBag.dataType = 1;
                ViewBag.yunshu = 0;
            }
            if (dataType == 2)
            {
                ViewBag.dataType = 0;
                ViewBag.yunshu = 1;
            }
            if (dataType == 3)
            {
                ViewBag.dataType = 1;
                ViewBag.yunshu = 1;
            }
            return base.View();
        }
        /// <summary>
        /// 搅拌车结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult TranBalance(List<string> ids)
        {
            if (ids.Count <= 0)
            {
                return OperateResult(false, "请选择单据", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            lock (objLock)
            {
                resultInfo = this.service.BetonService.TranBalance(ids);
            }
            return Json(resultInfo);
        }
        /// <summary>
        /// 泵车结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult PeCarBalance(List<string> ids)
        {
            if (ids.Count <= 0)
            {
                return OperateResult(false, "请选择单据", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            lock (objLock)
            {
                resultInfo = this.service.BetonService.PeCarBalance(ids);
            }
            return Json(resultInfo);
        }

        /// <summary>
        /// 砼款结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult Balance(List<string> ids)
        {
            if (ids.Count <= 0)
            {
                return OperateResult(false, "请选择单据", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            lock (objLock)
            {
                resultInfo = this.service.BetonService.Balance(ids);
            }
            return Json(resultInfo);
        }

        /// <summary>
        /// 泵送费结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult PotonBalance(List<string> ids)
        {
            if (ids.Count <= 0)
            {
                return OperateResult(false, "请选择单据", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            lock (objLock)
            {
                resultInfo = this.service.BetonService.PotonBalance(ids);
            }
            return Json(resultInfo);
        }
        [HttpPost]
        public JsonResult GetYM()
        {
            SqlServerHelper helper = new SqlServerHelper();

            DataSet ds = helper.ExecuteDataset("select Convert(varchar(4),Year(ProduceDate))+RIGHT('00'+Convert(varchar(2),MONTH(ProduceDate)),2) as ym,sum(SignInCube) as huncount from ShippingDocument where IsEffective=1 group by  Year(ProduceDate),MONTH(ProduceDate) order by ym ", CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] ym = new string[lenght];
            string[] huncount = new string[lenght];
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    ym[i] = dt0.Rows[i]["ym"].ToString();
                    huncount[i] = dt0.Rows[i]["huncount"].ToString();
                }
            }
            return Json(new
            {
                ym = ym,
                huncount = huncount
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProjectByYM(string ym)
        {
            SqlServerHelper helper = new SqlServerHelper();

            DataSet ds = helper.ExecuteDataset("select CustName as project,sum(SignInCube) as huncount from ShippingDocument  where IsEffective=1 and Convert(varchar(4),Year(ProduceDate))+RIGHT('00'+Convert(varchar(2),MONTH(ProduceDate)),2)='" + ym + "'  group by CustName order by huncount", CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] project = new string[lenght];
            string[] huncount = new string[lenght];
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    project[i] = dt0.Rows[i]["project"].ToString();
                    huncount[i] = dt0.Rows[i]["huncount"].ToString();
                }
            }
            return Json(new
            {
                project = project,
                huncount = huncount
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDetailByProject(string project)
        {
            SqlServerHelper helper = new SqlServerHelper();

            DataSet ds = helper.ExecuteDataset("select CONVERT(varchar(100), ProduceDate, 112) time,sum(SignInCube) as huncount from ShippingDocument where IsEffective=1 and CustName='" + project + "' group by CONVERT(varchar(100), ProduceDate, 112) order by time", CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] time = new string[lenght];
            string[] huncount = new string[lenght];
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    time[i] = dt0.Rows[i]["time"].ToString();
                    huncount[i] = dt0.Rows[i]["huncount"].ToString();
                }
            }
            return Json(new
            {
                time = time,
                huncount = huncount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProjectBJ(string project)
        {
            SqlServerHelper helper = new SqlServerHelper();

            DataSet ds = helper.ExecuteDataset("select year(ProduceDate) as y,month(ProduceDate) as m,sum(SignInCube) as huncount from ShippingDocument where IsEffective=1 and CustName='" + project + "'  group by year(ProduceDate),month(ProduceDate)", CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            int year = DateTime.Now.Year;
            string[] nowyear = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            string[] lastyear = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    if (dt0.Rows[i]["y"].ToString() == year.ToString())
                    {
                        nowyear[Convert.ToInt32(dt0.Rows[i]["m"]) - 1] = dt0.Rows[i]["huncount"].ToString();
                    }
                    else if (dt0.Rows[i]["y"].ToString() == (year - 1).ToString())
                    {
                        lastyear[Convert.ToInt32(dt0.Rows[i]["m"]) - 1] = dt0.Rows[i]["huncount"].ToString();
                    }
                }
            }
            return Json(new
            {
                nowyear = nowyear,
                lastyear = lastyear
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult QualityInspect(string shipDocId)
        {
            try
            {
                ShippingDocument shipDoc = this.service.ShippingDocument.Get(shipDocId);
                shipDoc.IsQualityInspected = !shipDoc.IsQualityInspected;
                this.service.ShippingDocument.Update(shipDoc, null);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(true, Lang.Msg_Operate_Failed, ex.Message);
            }
        }

        /// <summary>
        /// 批量回单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiBack(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ShippingDocument entity = this.service.ShippingDocument.Get(id);
                    entity.IsBack = true;
                    entity.DeliveryTime = DateTime.Now;
                    this.service.ShippingDocument.Update(entity);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + ex.Message, Lang.Error_ParameterRequired);
            }
        }



    }
}
