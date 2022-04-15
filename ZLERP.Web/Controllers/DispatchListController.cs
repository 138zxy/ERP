using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers
{
    public class DispatchListController : BaseController<DispatchList, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            //下拉列表车号自然排序
            var carList = this.service.Car.GetMixerCarListOrderByID().Where(p=>p.DataType==0).ToList();
             
            var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p=>p.ID); 
       
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            Company company = this.service.Company.GetCurrentCompany();
            ViewBag.PumpManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '52' AND IsUsed=1 ", "ID", true, "");
            ViewBag.CompanyInfo = company;


            IList<SelectListItem> companySelectItems =
                this.service.Company
                .Query()
                .Where(comp => comp.ShowInDispatch)
                .Select(comp => new SelectListItem() { Value = comp.ID.ToString(), Text = comp.CompName })
                .ToList();

            //companySelectItems.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Empty, Selected = true });
            ViewBag.CompanySelectItems = companySelectItems;


            var RentCarName = this.service.GetGenericService<B_CarFleet>().All("", "ID", true);
            var list = new SelectList(RentCarName, "ID", "FleetName");
            ViewBag.RentCarNames = HelperExtensions.SelectListData<B_CarFleet>("FleetName", "ID", " 1=1 ", "ID", true, "");
            //ViewBag.RentCarNames = list;
            return base.Index();
        }

        public System.Web.Mvc.ActionResult IndexSH()
        {
            //下拉列表车号自然排序
            var carList = this.service.Car.GetMixerCarListOrderByID().Where(p => p.DataType == 1).ToList();

            var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p => p.ID);

            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            Company company = this.service.Company.GetCurrentCompany();
            ViewBag.PumpManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '52' AND IsUsed=1 ", "ID", true, "");
            ViewBag.CompanyInfo = company;


            var RentCarName = this.service.GetGenericService<B_CarFleet>().All("", "ID", true);
            var list = new SelectList(RentCarName, "ID", "FleetName");
            ViewBag.RentCarNames = HelperExtensions.SelectListData<B_CarFleet>("FleetName", "ID", " 1=1 ", "ID", true, "");
            IList<SelectListItem> companySelectItems =
                this.service.Company
                .Query()
                .Where(comp => comp.ShowInDispatch)
                .Select(comp => new SelectListItem() { Value = comp.ID.ToString(), Text = comp.CompName })
                .ToList();

            //companySelectItems.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Empty, Selected = true });
            ViewBag.CompanySelectItems = companySelectItems;


            return base.Index();
        }

        /// <summary>
        /// 不带车辆排队面板
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult Index2()
        {
            //下拉列表车号自然排序
            var carList = this.service.Car.GetMixerCarListOrderByID();

            var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p => p.ID);

            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            return base.Index();
        }

        /// <summary>
        /// 保存生产调度
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// *********注意返回到前台的信息，如果返回结果为失败，并且Data为0表示 存在未处理的自动转退料记录，但是当前用户没有权限处理 *********
        /// *********返回为失败，并且Data不为0也不为空表示存在未处理的自动转退料记录，当前用户有权限处理(2013-5-15 SYW)********************
        static object obj = new object();
        static object objSH = new object();

        public ActionResult Save(SchedulerViewModel entity)
        {
            if (entity.DispatchList.DataType == 1)
            {
                lock (objSH)
                {
                    return Save1(entity);
                }
            }
            else
            {
                //混凝土
                lock (obj)
                {
                    return Save1(entity);
                }
            }
        }

        public ActionResult Save1(SchedulerViewModel entity)
        {
            try
            {
                if (bool.Parse(this.service.SysConfig.GetSysConfig("Gps").ConfigValue)) //若GPS开关打开，只能发送待命队列中的车
                {
                    Car c = this.service.Car.Get(entity.ShippingDocument.CarID);
                    if (c.CarStatus != Model.Enums.CarStatus.AllowShipCar)
                    {
                        return OperateResult(false, "在使用GPS的情况下，只能调派待命状态的车辆。", null);
                    }
                }

                string taskid = entity.ShippingDocument.TaskID;
                ProduceTask produceTask = this.service.ProduceTask.Get(taskid);

                //判断合同是否已经受限，如果受限，则不能继续生产
                if (produceTask.Contract.IsLimited == true)
                {
                    return OperateResult(false, "当前任务关联的合同[" + produceTask.ContractID + "]已受限，无法继续生产！ 请先解除合同受限", null);
                }

                //判断是否存在自动过磅并且未处理的记录
                TZRalation objchk = this.service.TZRalation.Query()
                 .Where(p => p.CarID == entity.ShippingDocument.CarID && p.IsCompleted == false).OrderByDescending(p => p.BuildTime)
                 .FirstOrDefault();
                if (objchk != null)
                {
                    if (string.IsNullOrEmpty(objchk.ActionType))
                    {
                        IList<SysFunc> FuncList = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);

                        SysFunc sf;

                        if (entity.DispatchList.DataType == 1)
                        {
                             sf = FuncList.Where(p => p.ID == "413505").FirstOrDefault();
                        }
                        else
                        {
                             sf = FuncList.Where(p => p.ID == "410505").FirstOrDefault();
                        }

                        if (sf != null)              //有权限
                        {
                            return OperateResult(false, Lang.Msg_hasAutoTz, objchk);
                        }
                        else
                        {
                            return OperateResult(false, Lang.Msg_hasAutoTz, 0);
                        }
                    }
                }


               
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowConsMixpropLimit);
                if (config != null && config.ConfigValue == "true" && produceTask.CementType == Model.Enums.CementType.CommonCement)
                {
                    string measureError = this.service.DispatchList.CheckCheckMesureScale(entity);
                    if (!string.IsNullOrEmpty(measureError))
                    {
                        return OperateResult(false, measureError, null);
                    }
                }
                //检查合同限额控制
                string msg = this.service.DispatchList.CheckCubeLimit(entity);
                if (!string.IsNullOrEmpty(msg))
                {
                    return OperateResult(true, msg, null);
                }

                if (entity.DispatchList.DataType == 1)
                {
                    //shihun 将混凝土方量与砂浆方量交换
                    entity.DispatchList.SlurryCount = entity.DispatchList.BetonCount;
                    entity.DispatchList.BetonCount = 0;
                }

                entity.DispatchList.Field2 = "";
                if (entity.DispatchList.SlurryCount > 0 && entity.DispatchList.BetonCount == 0)
                {
                    entity.DispatchList.Field2 = "1";
                }

                //判断混凝土或砂浆是否审核通过 lzl 2018-08-20
                IList<ConsMixprop> phblist = null;
                bool bt = true;
                bool st = true;
                phblist = this.service.GetGenericService<ConsMixprop>().Find("TaskID = '" + produceTask.ID + "' and ProductLineID=" + entity.DispatchList.ProductLineID, 1, 30, "ProductLineID", "ASC");

                foreach (var a in phblist)
                {
                    if (a.IsSlurry && entity.DispatchList.SlurryCount > 0)
                    {
                        if (a.AuditStatus != 1)
                        {
                            bt = false;
                        }
                    }
                    if (a.IsSlurry == false && entity.DispatchList.BetonCount > 0)
                    {
                        if (a.AuditStatus != 1)
                        {
                            st = false;
                        }
                    }
                }


                //下配比的
                if (entity.DispatchList.SlurryCount > 0)
                {
                    if (phblist.Where(p => p.IsSlurry == true).ToList().Count == 0)
                    {
                        return OperateResult(false, "未发现砂浆配比，调度失败。", null);
                    }

                }
                if (entity.DispatchList.BetonCount > 0)
                {
                    if (phblist.Where(p => p.IsSlurry == false).ToList().Count == 0)
                    {
                        return OperateResult(false, "未发现混凝土配比，调度失败。", null);
                    }
                }





                if (!bt)
                {
                    return OperateResult(false, "混凝土配比未审核或审核不通过", null);
                }
                if (!st)
                {
                    return OperateResult(false, "砂浆配比未审核或审核不通过", null);
                }

                SchedulerViewModel saveEntity = this.service.DispatchList.SaveScheduler(entity);
                return OperateResult(true, Lang.Msg_Operate_Success, saveEntity.ShippingDocument.GetId());
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }

        }


        /// <summary>
        /// 根据DJH获取可转生产线、扣补方量、车号等。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetInfoByDJH(string id)
        {
            dynamic dispatch = this.service.DispatchList.GetInfoByDJH(id);
            return this.Json(dispatch);
        }

        /// <summary>
        /// 换车操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carid"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public ActionResult ChangeCar(string id,string carid,string driver) 
        {
            try
            {
                this.service.DispatchList.ChangeCar(id,carid,driver);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 修改已供车次，方量
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvidedTimes"></param>
        /// <param name="ProvidedCube"></param>
        /// <returns></returns>
        public ActionResult UpdateProvided(string ID, int ProvidedTimes, decimal ProvidedCube)
        {
            try
            {
                this.service.DispatchList.UpdateProvided(ID, ProvidedTimes, ProvidedCube);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
            
        }
        /// <summary>
        /// 扣补方操作。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cube"></param>
        /// <returns></returns>
        public ActionResult ChangeCube(string id, decimal? cube)
        {
            try
            {
                this.service.DispatchList.ChangeCube(id, cube);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 换线生产
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult ChangeProductLine(string id, string pid)
        {
            try
            {
                this.service.DispatchList.ChangeProductLine(id, pid);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 车辆取样
        /// </summary>
        /// <param name="dispatchid"></param>
        /// <param name="carid"></param>
        /// <returns></returns>
        public ActionResult Sampling(string dispatchid,string carid) {
            try
            {
                this.service.DispatchList.Sampling(dispatchid, carid);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e) {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
				
		/// <summary>
        /// 置为完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetCompleted(string id)
        {
            try
            {
                this.service.DispatchList.SetCompleted(id);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 置为运行状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRun(string id)
        {
            try
            {
                this.service.DispatchList.SetRun(id);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
				
				
				/// <summary>
        /// 上移生产调度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MoveUp(string id)
        {
            try
            {
                this.service.DispatchList.MoveUp(id);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }

        }

        /// <summary>
        /// 看板发货
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult AddDispatch(string taskId)
        {
            ProduceTask pt = this.service.ProduceTask.Get(taskId);
            if (pt != null)
            {

                //下拉列表车号自然排序
                var carList = this.service.Car.GetMixerCarListOrderByID();

                var pumpList = this.service.Car.GetPumpList()
                    .OrderBy(p => p.ID);

                ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
                ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

                ViewBag.TaskInfo = pt;
                var DefaultManufactureCubeConfig = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.DefaultManufactureCube);
                ViewBag.DefaultManufactureCube = DefaultManufactureCubeConfig == null ? "" : DefaultManufactureCubeConfig.ConfigValue;
            }
            InitCommonViewBag();
            return View();
        }
        /// <summary>
        /// 重发调度
        /// </summary>
        /// <param name="dispatchid"></param>
        /// <param name="carid"></param>
        /// <returns></returns>
        public ActionResult RepeatSend(string dispatchid, int sendstatus)
        {
            try
            {
                this.service.DispatchList.RepeatSend(dispatchid, sendstatus);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 根据ShipdocID获取可转生产线、扣补方量、车号等。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetInfoByTaskID(string TaskId,string LineID)
        {
             
            try
            {
                var entity = this.service.DispatchList.Query().Where(a => a.TaskID == TaskId && a.ProductLineID == LineID && a.BuildTime>=(DateTime.Now.AddDays(-3))).OrderByDescending(a => a.BuildTime).First();
                if (entity != null)
                {
                    return OperateResult(true, Lang.Msg_Operate_Success, entity.PCRate);
                }
                else
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, null);
                }
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
    }
}
