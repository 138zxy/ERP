using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;
using ZLERP.Model.ViewModels;
using System.EnterpriseServices;

namespace ZLERP.Web.Controllers
{
    public class AlarmLogController : BaseController<AlarmLog, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            IList<Car> cars = this.service.Car.Query().Where(c => c.IsUsed).OrderBy(car => car.ID).ToList();
            foreach (Car c in cars)
            {
                c.CarNo = c.ID + " -- " + c.CarNo;
            }

            Company company = this.service.Company.GetCurrentCompany();

            ViewBag.CompanyInfo = company;
            ViewBag.CarList = new SelectList(cars, "ID", "CarNo");

            IList<Dic> AlarmDics = this.service.Dic.Query().Where(d => d.ParentID == "GPSAlarmType").ToList();
            ViewBag.AlarmList = new SelectList(AlarmDics, "ID", "DicName");
            return base.Index();
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Find(JqGridRequest request, string condition)
        {
            int total;
            var list = m_ServiceBase.Find(request, condition, out total).Select(alarm => new
            {
                ID = alarm.ID,
                CarID = alarm.CarID,
                CarNo = this.service.Car.Get(alarm == null ? "" : alarm.CarID).CarNo,
                AlarmTypeID = alarm.AlarmTypeID,
                DriverName = alarm.DriverName,
                AlarmData = alarm.AlarmData,
                BY1 = alarm.BY1,
                BY2 = alarm.BY2,
                BY3 = alarm.BY3,
                BY4 = alarm.BY4,
                BY5 = alarm.BY5,
                BY6 = alarm.BY6,
                AlarmTime = alarm.AlarmTime,
                SN = alarm.SN
            });

            object data = new
            {
                page = request.PageIndex,
                total = (total - 1) / request.RecordsCount + 1,
                records = total,
                userdata = "",
                rows = list
            };
            return Json(data);
        }

        [Description("获取车辆报警信息列表")]
        //public ActionResult GetAlarmInfos(JqGridRequest request, string condition)
        //{
        //    var cars = this.service.Car.Query().Where(car => car.IsUsed && car.TerminalID != null).ToList();
        //    List<GPS_CarAlarmInfo> jqlist = new List<GPS_CarAlarmInfo>();
        //    var gpsinfos = this.service.GetGenericService<LastestGpsInfo>().Query().ToList();
        //    var OneTimeAgo = DateTime.Now.AddHours(-1);
        //    var allogs = this.service.GetGenericService<AlarmLog>().Query().Where(p => p.AlarmTime >= OneTimeAgo && p.Lifecycle == 0).ToList().OrderBy(e => e.AlarmTypeID);
        //    int idindex = 0;
        //    var carid = cars.Select(t => t.ID).ToList();
        //    var docList = this.service.ShippingDocument.getShippingTaskAll(carid);
        //    foreach (var car in cars)
        //    {
        //        var alarms = this.service.AlarmLog.GetAlarams(car.TerminalID, cars, gpsinfos, allogs);
        //        var doc = docList.OrderByDescending(p => p.BuildTime).FirstOrDefault(t => t.CarID == car.ID);
        //        if (alarms.Count() > 0)
        //        {
        //            foreach (var alarm in alarms)
        //            {
        //                idindex++;
        //                jqlist.Add(new GPS_CarAlarmInfo()
        //                {
        //                    id = alarm.id,//id = idindex,
        //                    carid = car.ID,
        //                    carNo = car.CarNo,
        //                    driver = doc == null ? string.Empty : doc.Driver,
        //                    alarmInfo = alarm.alarmInfo,
        //                    alarmTypeID = alarm.alarmTypeID,
        //                    alarmTime = alarm.alarmTime,
        //                    SN = car.TerminalID
        //                });
        //            }
        //        }
        //    }
        //    if (request.SearchingFilters != null)
        //    {
        //        foreach (JqGridRequestSearchingFilter filter in request.SearchingFilters.Filters)
        //        {
        //            switch (filter.SearchingName)
        //            {
        //                case "carid":
        //                    {
        //                        if (filter.SearchingValue.Trim() != string.Empty)
        //                            jqlist = jqlist.Where(p => p.carid.Contains(filter.SearchingValue)).ToList();
        //                        break;
        //                    }
        //                case "carNo":
        //                    {
        //                        if (filter.SearchingValue.Trim() != string.Empty)
        //                            jqlist = jqlist.Where(p => p.carNo.Contains(filter.SearchingValue)).ToList();
        //                        break;
        //                    }
        //                case "driver":
        //                    {
        //                        if (filter.SearchingValue.Trim() != string.Empty)
        //                            jqlist = jqlist.Where(p => p.driver.Contains(filter.SearchingValue)).ToList();
        //                        break;
        //                    }
        //                case "alarmInfo":
        //                    {
        //                        if (filter.SearchingValue.Trim() != string.Empty)
        //                            jqlist = jqlist.Where(p => p.alarmInfo.Contains(filter.SearchingValue)).ToList();
        //                        break;
        //                    }
        //                case "alarmTypeID":
        //                    {
        //                        if (filter.SearchingValue.Trim() != string.Empty)
        //                            jqlist = jqlist.Where(p => p.alarmTypeID == filter.SearchingValue).ToList();
        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //    object data = new
        //    {
        //        page = 1,
        //        total = 1,
        //        records = jqlist.Count,
        //        userdata = "",
        //        rows = jqlist
        //    };
        //    return Json(data);
        //}
        public ActionResult GetAlarmInfos(JqGridRequest request, string condition)
        {
            var cars = this.service.Car.Query().Where(car => car.IsUsed && car.TerminalID != null).ToList();
            List<GPS_CarAlarmInfo> jqlist = new List<GPS_CarAlarmInfo>();
            var gpsinfos = this.service.GetGenericService<LastestGpsInfo>().Query().ToList();
            var OneTimeAgo = DateTime.Now.AddHours(-1);
            var allogs = this.service.GetGenericService<AlarmLog>().Query().Where(p => p.AlarmTime >= OneTimeAgo).ToList().OrderBy(e => e.AlarmTypeID);
            int idindex = 0;
            foreach (var car in cars)
            {
                var alarms = this.service.AlarmLog.GetAlarams(car.TerminalID, cars, gpsinfos, allogs);
                if (alarms.Count() > 0)
                {
                    foreach (var alarm in alarms)
                    {
                        idindex++;
                        jqlist.Add(new GPS_CarAlarmInfo()
                        {
                            id = idindex,
                            carid = car.ID,
                            carNo = car.CarNo,
                            driver = this.service.ShippingDocument.getShippingTask(car.ID).Count == 0 ? string.Empty : this.service.ShippingDocument.getShippingTask(car.ID)[0].Driver,
                            alarmInfo = alarm.alarmInfo,
                            alarmTypeID = alarm.alarmTypeID,
                            alarmTime = alarm.alarmTime
                        });
                    }
                }
            }
            if (request.SearchingFilters != null)
            {
                foreach (JqGridRequestSearchingFilter filter in request.SearchingFilters.Filters)
                {
                    switch (filter.SearchingName)
                    {
                        case "carid":
                            {
                                if (filter.SearchingValue.Trim() != string.Empty)
                                    jqlist = jqlist.Where(p => p.carid.Contains(filter.SearchingValue)).ToList();
                                break;
                            }
                        case "carNo":
                            {
                                if (filter.SearchingValue.Trim() != string.Empty)
                                    jqlist = jqlist.Where(p => p.carNo.Contains(filter.SearchingValue)).ToList();
                                break;
                            }
                        case "driver":
                            {
                                if (filter.SearchingValue.Trim() != string.Empty)
                                    jqlist = jqlist.Where(p => p.driver.Contains(filter.SearchingValue)).ToList();
                                break;
                            }
                        case "alarmInfo":
                            {
                                if (filter.SearchingValue.Trim() != string.Empty)
                                    jqlist = jqlist.Where(p => p.alarmInfo.Contains(filter.SearchingValue)).ToList();
                                break;
                            }
                        case "alarmTypeID":
                            {
                                if (filter.SearchingValue.Trim() != string.Empty)
                                    jqlist = jqlist.Where(p => p.alarmTypeID == filter.SearchingValue).ToList();
                                break;
                            }
                    }
                }
            }
            object data = new
            {
                page = 1,
                total = 1,
                records = jqlist.Count,
                userdata = "",
                rows = jqlist
            };
            return Json(data);
        }

        /// <summary>
        /// 修改报警状态
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateAlarmLog(int logid)
        {
            try
            {
                AlarmLog log = this.service.AlarmLog.Get(logid);
                log.Lifecycle = 1;
                this.service.AlarmLog.Update(log, null);
                return OperateResult(true, Lang.Msg_Operate_Success, "报警状态更改成功");
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }
    }
}
