using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;
using Lib.Web.Mvc.JQuery.JqGrid;　

namespace ZLERP.Business
{
    public  class ShippingDocumentGHService : ServiceBase<ShippingDocumentGH>
    {
        internal ShippingDocumentGHService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        public dynamic GetLastDocByTaskId(string taskid)
        {
            ShippingDocumentGH doc = this.Find("TaskID = '" + taskid + "' AND IsEffective = 1 AND ShipDocType in ('0','成品仓') ", 1, 1, "ID", "DESC").FirstOrDefault();



            IList<ConsMixpropGH> phblist = null;
            phblist = this.m_UnitOfWork.ConsMixpropGHRepository.Find("TaskID = '" + taskid + "' AND AuditStatus = 1 ", 1, 30, "ProductLineID", "ASC");

            IList<string> pdlist = phblist.Select(p => p.ProductLineID).Distinct().ToList();
            IList<ProductLine> productlines = null;
            IList<Company> company = null;
            SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
            if (pdlist.Count > 0)
            {
                productlines = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                    .Query().Where(p => p.IsUsed && (p.IsGH.Value == 1) && pdlist.Contains(p.ID) && (p.IsGH.Value == 1))
                    .OrderBy(p => p.ID).ToList();
                company = this.m_UnitOfWork.GetRepositoryBase<Company>()
                    .Query().OrderBy(p => p.ID).ToList();
                //获取所有未完成的单
                //修复点
                IList<DispatchListGH> dispatchListGH = this.m_UnitOfWork.GetRepositoryBase<DispatchListGH>()
                    .Query().Where(d => d.IsCompleted == false && d.BuildTime >= DateTime.Now.AddDays(-3)).OrderBy(d => d.DispatchOrder).ToList();


                SysConfig delayConfig = configService.GetSysConfig(SysConfigEnum.DelayDeliveryTime);

                int delayTime = delayConfig == null ? 8 : Convert.ToInt32(delayConfig.ConfigValue);//未找到参数，默认8
                foreach (ProductLine item in productlines)
                {
                    string id = item.ID;
                    int count = dispatchListGH.Where(d => d.ProductLineID == id).ToList().Count + 1;
                    //将预计发车时间赋值给productline的modifytime
                    int delayMinute = count * delayTime;
                    DateTime deliveryTime = DateTime.Now.AddMinutes(delayMinute);
                    item.ModifyTime = deliveryTime;
                }
            }
            //自动选中车辆 
            SysConfig config = configService.GetSysConfig(Model.Enums.SysConfigEnum.AutoSelectCarID);
            bool autoSelectCar = config == null ? true : Convert.ToBoolean(config.ConfigValue);
            string autoCarID = string.Empty;
            if (autoSelectCar)
            {
                IList<Car> carList = this.m_UnitOfWork.GetRepositoryBase<Car>().Query()
                        .Where(p => p.CarStatus == CarStatus.AllowShipCar && p.CarTypeID == CarType.Mixer && p.IsUsed)
                        .OrderBy(p => p.OrderNum).OrderBy(p => p.ID).ToList();

                Car crtCar = carList.FirstOrDefault();
                if (crtCar != null)
                {
                    autoCarID = crtCar.ID;
                }
            }

            CustomerPlanGH cp = new CustomerPlanGH(); 

            cp = new PublicService().CustomerPlanGH.Query().Where(m => m.TaskID == taskid).FirstOrDefault();


            return new
            {
                Result = true
                ,
                Message = string.Empty
                ,
                Remark = doc == null ? string.Empty : (doc.Remark != null && doc.Remark.IndexOf("CODEADD") >= 0 ? doc.Remark.Remove(doc.Remark.IndexOf("CODEADD")) : doc.Remark)

                ,
                ProvidedCube = doc == null ? 0 : doc.ProvidedCube
                ,
                PlanCube = doc == null ? 0 : doc.PlanCube
                    //累计车数
                ,
                ProvidedTimes = doc == null ? 0 : doc.ProvidedTimes
                    //生产线
                ,
                ProductLines = productlines
                ,
                Company = company
                    //自动选中的车辆
                ,
                AutoCarID = autoCarID
                ,
                CustSiloNo = (doc == null ? "" : doc.CustSiloNo)
            };
        }


        /// <summary>
        /// 作废/取消作废发货单
        /// </summary>
        /// <param name="shippingDocId"></param>
        /// <param name="status"></param>
        /// <param name="remark"></param>
        public void Garbage(string shippingDocId, bool status, string remark) {
            try {
                ShippingDocumentGH shippingDoc = this.Get(shippingDocId);
                shippingDoc.IsEffective = !status;

                string userID = GetUserId();
                if (shippingDoc.IsEffective)
                {
                    shippingDoc.Remark += string.Format("CODEADD手动取消作废，{0}{1}原因：{2}；", userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), remark);
                }
                else
                {
                    shippingDoc.Remark += string.Format("CODEADD手动作废，{0}{1}原因：{2}；", userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), remark);
                }

                base.Update(shippingDoc, null);
                
                //记录手动作废/取消作废操作
                if (shippingDoc.IsEffective)
                {
                    logger.Debug(string.Format("手动取消作废，运输单号{0}，修改人{1}，修改时间{2}", shippingDoc.ID, userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
                else
                {
                    logger.Debug(string.Format("手动作废，运输单号{0}，修改人{1}，修改时间{2}", shippingDoc.ID, userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
            }catch(Exception e){
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 运输单审核
        /// </summary>
        /// <param name="shippingDocId"></param>
        /// <param name="status"></param>
        public void Audit(string shippingDocId, bool status)
        {
            try
            {
                ShippingDocumentGH shippingDoc = this.Get(shippingDocId);
                shippingDoc.IsAudit = !shippingDoc.IsAudit;
                if (shippingDoc.IsAudit)//如果置为审核，设置审核人，审核时间
                { 
                    shippingDoc.AuditMan  = AuthorizationService.CurrentUserID;
                    shippingDoc.AuditTime = DateTime.Now;
                }

                base.Update(shippingDoc, null);
                
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }

        }
        /// <summary>
        /// 运输单批量审核(存储过程)
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool ExecMuitAudit(string idstrs, string CurrentUserID)
        {
            bool issuccess = this.m_UnitOfWork.ShippingDocumentGHRepository.ShipDocMultiAudit(idstrs, CurrentUserID);
            return issuccess;
        }

        /// <summary>
        /// 快速回厂
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public bool CarReturn(string carId) {
            //修复点
             var lastDoc = this.Query()
                   .Where(p => p.CarID == carId && p.BuildTime >= DateTime.Now.AddDays(-3))
                   .OrderByDescending(p => p.BuildTime)
                   .FirstOrDefault();

             if (lastDoc != null) {
                 lastDoc.IsBack = true;
                 lastDoc.ArriveTime = DateTime.Now;
                 lastDoc.Status = "007003";
                 this.Update(lastDoc, null);
             } 
             CarService carService = new CarService(this.m_UnitOfWork);
             carService.ShiftMixerCarStatusGH("Up", carId, CarStatus.ShippingCar);
             return true;
        }

        /// <summary>
        /// 快速回厂
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public bool CarReturnGH(string carId)
        {
            var lastDoc = this.Query()
                  .Where(p => p.CarID == carId)
                  .OrderByDescending(p => p.BuildTime)
                  .FirstOrDefault();

            if (lastDoc != null)
            {
                lastDoc.IsBack = true;
                lastDoc.ArriveTime = DateTime.Now;
                lastDoc.Status = "007003";
                this.Update(lastDoc, null);
            }
            CarService carService = new CarService(this.m_UnitOfWork);
            carService.ShiftMixerCarStatusGH("Up", carId, CarStatus.ShippingCar);
            return true;
        }

        /// <summary>
        /// 获取运输单任务
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        public IList<ShippingDocumentGH> getShippingTask(string CarId)
        {
            IList<ShippingDocumentGH> shippingDocumentList = new List<ShippingDocumentGH>();
            IList<Car> CarList = null;
            PublicService ps = new PublicService();
            if (string.IsNullOrEmpty(CarId))
            {
               
                CarList = ps.GetGenericService<Car>().Query().Where(p => p.CarStatus == ZLERP.Model.Enums.CarStatus.ShippingCar && p.IsUsed).ToList();
            }
            else
            {
                CarList = ps.GetGenericService<Car>().Query().Where(p => p.CarStatus == ZLERP.Model.Enums.CarStatus.ShippingCar && p.ID == CarId && p.IsUsed).ToList();
            }
            foreach (Car car in CarList)
            {

                ShippingDocumentGH shippingDocumentGH = this.Query().OrderByDescending(p => p.BuildTime).FirstOrDefault(p => p.CarID == car.ID && p.IsEffective);
                if (shippingDocumentGH != null)
                {
                    shippingDocumentList.Add(shippingDocumentGH);
                }
            }

            return shippingDocumentList;
        }

        public IList<DailyReport> GetShipDocList(JqGridRequest request, string condition, out int total)
        {
            string StartDateTime = condition.Substring(22, 19);
            string EndDateTime = condition.Substring(48, 19);
            return this.m_UnitOfWork.ShippingDocumentGHRepository.ExecuteShipDoc(StartDateTime, EndDateTime, out total);
        }

        public override ShippingDocumentGH Add(ShippingDocumentGH entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var result = entity;
                    if (entity.ShipDocType == "成品仓")
                    {
                        DispatchListGHService dispat = new DispatchListGHService(this.m_UnitOfWork);
                        ShippingDocumentGH ship=dispat.SaveShippingDocument(entity);
                    }
                    else
                    {
                        result = base.Add(entity);
                    }

                    tx.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// 价格调价
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public int RefreshShipDocPrice(DateTime beginDate, DateTime endDate, string contractId)
        {
            return this.m_UnitOfWork.ShippingDocumentGHRepository.RefreshShipDocPrice(beginDate, endDate, contractId);
        }

        /// <summary>
        /// 运费调价
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public int RefreshShipDocFreight(DateTime beginDate, DateTime endDate, string contractId)
        {
            return this.m_UnitOfWork.ShippingDocumentGHRepository.RefreshShipDocFreight(beginDate, endDate, contractId);
        }
  
    }
}

