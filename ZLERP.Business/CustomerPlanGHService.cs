using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class CustomerPlanGHService : ServiceBase<CustomerPlanGH>
    {
        internal CustomerPlanGHService(IUnitOfWork uow) : base(uow) { }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditstatus"></param>
        /// <param name="audittime"></param>
        /// <param name="auditor"></param>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public bool Auditing(CustomerPlanGH plan, decimal money, string custID, bool iskye)
        {
            // var plan = this.Get(id);
            if (plan != null)
            {
                //plan.AuditStatus = auditstatus;
                plan.AuditTime = DateTime.Now;
                plan.Auditor = AuthorizationService.CurrentUserID;
                //plan.AuditInfo = auditInfo;
                string TaskID = string.Empty;
                if (plan.AuditStatus == (int)AuditStatus.Pass)
                {
                    using (var tx = this.m_UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(plan.TaskID))
                            {
                                CreateProducePlan(plan);
                            }
                            else
                            {
                                //创建任务单并创建生产计划
                                CreateProduceTask(plan, this.m_UnitOfWork, out TaskID);
                            }

                            if (iskye)
                            {
                                //减去客户余额
                                PublicService op = new PublicService();
                                Customer customer = op.Customer.Get(custID);
                                customer.AccountBalance -= money;
                                op.Customer.Update(customer);
                            }

                            tx.Commit();
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            logger.Error("审核工地计划单失败", ex);
                            throw new ApplicationException("审核工地计划单失败:" + ex.Message);
                        }
                    }
                    //根据系统配置是否需要发送下发配比的通知
                    if (!(string.Empty == TaskID))
                    {
                        SystemMsgService systemMsgService = new SystemMsgService(this.m_UnitOfWork);
                        systemMsgService.SendMsg("SendPb", "任务单号：" + TaskID);
                    }
                }
                this.Update(plan, System.Web.HttpContext.Current.Request.Form);
                return true;
            }
            return false;
        }

        public bool MultAudit(string[] ids, decimal money, string custID, bool iskye)
        {
            bool AuditResult = true;
            try
            {
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {//防止提交空id出错
                        var plan = this.Get(id);
                        if (plan != null && plan.AuditStatus != (int)AuditStatus.Pass)
                        {
                            plan.AuditStatus = 1;
                            AuditResult = Auditing(plan, money, custID, iskye);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("审核工地计划单失败", ex);
                throw new ApplicationException("审核工地计划单失败:" + ex.Message);
            }
            return AuditResult;
        }
        //根据计划创建新任务单
        void CreateProduceTask(CustomerPlanGH cPlan, IUnitOfWork uow, out string TaskID)
        {

            var contractItemGH = CreateContractItem(cPlan, uow);
            int contractItemId = contractItemGH.ID ?? 0;
            TaskID = string.Empty; 
            if (contractItemGH != null && contractItemId > 0)
            {
                ProduceTaskGH task = new ProduceTaskGH();

                task.ContractID = cPlan.ContractID;
                task.ContractItemsID = contractItemId;

                task.ConstructUnit = cPlan.ConstructUnit;
                task.ProjectName = cPlan.ProjectName;
                task.ProjectAddr = cPlan.ProjectAddr;
                task.ConStrength = contractItemGH.ConStrength;
                task.ConsPos = cPlan.ConsPos;
                task.Slump = cPlan.Slump;
                task.CastMode = cPlan.CastMode;
                task.PlanCube = cPlan.PlanCube;
                task.PumpType = cPlan.PumpName;
                task.SupplyUnit = cPlan.SupplyUnit;
                task.BuildUnit = cPlan.BuildUnit;
                DateTime dt;
                if (!DateTime.TryParse(cPlan.PlanDate.ToString("yyyy-MM-dd") + " " + cPlan.NeedDate, out dt))
                {
                    DateTime.TryParse(cPlan.PlanDate.ToString("yyyy-MM-dd") + " 21:00:00", out dt);
                }
                task.NeedDate = dt;

                task.Tel = cPlan.Tel;
                task.LinkMan = cPlan.LinkMan;
                task.Remark = cPlan.Remark; 
                task.TaskType = TaskType.ContractTask;
                ProduceTaskGHService pts = new ProduceTaskGHService(uow);
                pts.CheckIsAutoAudit(task);
                var projectGH = pts.CreateProject(task);
                task.ProjectID = projectGH.ID;


                //string ID = "";
                //ProduceTaskGH pj = this.m_UnitOfWork.GetRepositoryBase<ProduceTaskGH>().Query().Where(p => p.ID.Contains(DateTime.Now.ToString("yyMMdd"))).OrderByDescending(p => p.ID).FirstOrDefault();
                //if (pj == null)
                //{
                //    ID = DateTime.Now.ToString("yyMMdd") + "001";
                //}
                //else
                //{
                //    ID = pj.ID.Substring(6, 3);
                //    int k = Convert.ToInt32(ID);
                //    k++;
                //    ID = DateTime.Now.ToString("yyMMdd") + (k.ToString().Length == 1 ? ("00" + k.ToString()) : (k.ToString().Length == 2 ? ("0" + k.ToString()) : k.ToString()));
                //}
                //task.ID = ID;
                task.DeliveryAddress = cPlan.DeliveryAddress;

                task = this.m_UnitOfWork.GetRepositoryBase<ProduceTaskGH>().Add(task);
                TaskID = task.ID;
                cPlan.TaskID = task.ID;
                CreateProducePlan(cPlan);
            }


        }

        /// <summary>
        /// 检查合同明细项，不存在则创建新合同明细项
        /// </summary>
        /// <param name="cPlan"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        ContractItemGH CreateContractItem(CustomerPlanGH cPlan, IUnitOfWork uow)
        {
            var itemService = new ServiceBase<ContractItemGH>(uow);
            var item = itemService.Query()
                .Where(p => p.ContractID == cPlan.ContractID && p.ConStrength == cPlan.ConStrength)
                .FirstOrDefault();

            if (item == null)
            {//添加新ContractIte m
                ConStrengthService conStrengthService = new ConStrengthService(uow);
                int conStrengthCnt = conStrengthService.Query()
                    .Where(p => p.ConStrengthCode == cPlan.ConStrength)
                    .Count();
                if (conStrengthCnt <= 0)
                {//添加砼强度
                    conStrengthService.Add(new ConStrength { ConStrengthCode = cPlan.ConStrength });
                }

                item = itemService.Add(new ContractItemGH { ConStrength = cPlan.ConStrength, ContractID = cPlan.ContractID });
            }
            return item;
        }

        /// <summary>
        /// 创建生产计划
        /// </summary>
        /// <param name="cPlan"></param>
        void CreateProducePlan(CustomerPlanGH cPlan)
        {
            var service = new ServiceBase<ProducePlanGH>(this.m_UnitOfWork);

            var plan = new ProducePlanGH
            {
                TaskID = cPlan.TaskID,
                PlanCube = cPlan.PlanCube,
                PlanDate = cPlan.PlanDate
            };
            service.Add(plan);
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool CancelAudit(string[] ids,decimal money,decimal price,string custID,bool iskye,ref string errmsg)
        {
            bool AuditResult = true;
            try
            {
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {//防止提交空id出错
                        var plan = this.Get(id);
                        if (plan != null)
                        {
                            plan.AuditStatus = 0;
                            AuditResult = CancelAuditing(plan, money, price, custID, iskye);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("审核工地计划单失败", ex); 
                errmsg = ex.Message;
                return false;
            }
            return AuditResult;
        }
        /// <summary>
        /// 取消审核计划
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="money"></param>
        /// <param name="price"></param>
        /// <param name="custID"></param>
        /// <returns></returns>
        public bool CancelAuditing(CustomerPlanGH plan, decimal money, decimal price, string custID, bool iskye)
        {
            // var plan = this.Get(id);
            if (plan != null)
            {
                ProduceTaskGH task = new ProduceTaskGH();
                task = this.m_UnitOfWork.GetRepositoryBase<ProduceTaskGH>().Get(plan.TaskID);

                ShippingDocumentGH ship = this.m_UnitOfWork.GetRepositoryBase<ShippingDocumentGH>().Query().Where(m => m.TaskID == plan.TaskID).FirstOrDefault();
                if (ship != null)
                {
                    throw new Exception("检测到发货单记录，不能取消审核！");
                }
                else
                {
                    using (var tx = this.m_UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            //更新CustomerPlanGH表记录
                            plan.AuditTime = null;
                            plan.Auditor = string.Empty;
                            string TaskID = plan.TaskID;
                            plan.TaskID = null;
                            this.Update(plan, null);

                            PublicService ps = new PublicService();
                            if (TaskID != "")
                            {
                                //删除配比
                                IList<ConsMixpropGH> l = this.m_UnitOfWork.GetRepositoryBase<ConsMixpropGH>().Query().Where(m => m.TaskID == TaskID).ToList();
                                if (l.Count>0)
                                {
                                    foreach (ConsMixpropGH _l in l)
                                    {
                                        IList<ConsMixpropItemGH> list = this.m_UnitOfWork.GetRepositoryBase<ConsMixpropItemGH>().Query().Where(m => m.ConsMixpropID == _l.ID).ToList();
                                        foreach (ConsMixpropItemGH item in list)
                                        {
                                            ps.GetGenericService<ConsMixpropItemGH>().Delete(item);
                                        }
                                        ps.GetGenericService<ConsMixpropGH>().Delete(_l);
                                    }
                                }

                                //删除ProducePla n表记录
                                ProducePlanGH pplan = new ProducePlanGH();
                                pplan = this.m_UnitOfWork.GetRepositoryBase<ProducePlanGH>().Query().Where(p => p.TaskID.Contains(TaskID)).OrderByDescending(p => p.ID).FirstOrDefault();
                                if (pplan != null)
                                {
                                    ps.GetGenericService<ProducePlanGH>().Delete(pplan);
                                }
                                //删除ProduceTask表记录
                                if (task != null)
                                {
                                    ps.GetGenericService<ProduceTaskGH>().Delete(task);
                                }
                            }

                            if (iskye)
                            {
                                //减去客户余额
                                PublicService op = new PublicService();
                                Customer customer = op.Customer.Get(custID);
                                customer.AccountBalance -= money;
                                op.Customer.Update(customer, null);
                            }
                                                      
                            tx.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            logger.Error("取消审核工地计划单失败", ex);
                            throw new ApplicationException("取消审核工地计划单失败:" + ex.Message);
                        }
                    }


                }

            }
            return false;
        }

        /// <summary>
        /// 复制计划单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditstatus"></param>
        /// <param name="audittime"></param>
        /// <param name="auditor"></param>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public bool CopyPlan(string planid, string[] ids, string conspos)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(planid))
                    {
                        return false;
                    }
                    foreach (string id in ids)
                    {
                        CustomerPlanGH cplan = new CustomerPlanGH();
                        if (!string.IsNullOrEmpty(id))
                        {

                            var planInDB = this.Get(planid);
                            ContractItemGH cons = this.m_UnitOfWork.GetRepositoryBase<ContractItemGH>().Get(Convert.ToInt32(id));                            
                            cplan.ConStrength = cons.ConStrength;

                            cplan.ContractID = planInDB.ContractID;                           
                            cplan.BuildTime = DateTime.Now;
                            cplan.ProjectName = planInDB.ProjectName;
                            cplan.ConsPos = conspos;
                            cplan.Slump = planInDB.Slump;
                            cplan.CastMode = planInDB.CastMode;
                            cplan.PlanCube = planInDB.PlanCube;
                            cplan.NeedDate = planInDB.NeedDate;
                            cplan.PlanDate = planInDB.PlanDate;

                            cplan.ContractNo = planInDB.ContractNo;
                            cplan.ConstructUnit = planInDB.ConstructUnit;
                            cplan.ProjectAddr = planInDB.ProjectAddr;
                            cplan.Tel = planInDB.Tel;
                            cplan.LinkMan = planInDB.LinkMan; 
                            cplan.SupplyUnit = planInDB.SupplyUnit;
                            cplan.DeliveryAddress = planInDB.DeliveryAddress;
                            cplan.BuildUnit = planInDB.BuildUnit;

                            this.Add(cplan);
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error("复制工地计划单失败", ex);
                    throw new ApplicationException("复制工地计划单失败:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 复制计划单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditstatus"></param>
        /// <param name="audittime"></param>
        /// <param name="auditor"></param>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public bool CopyPlanAll(string planid)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(planid))
                    {
                        return false;
                    }
                    CustomerPlanGH cplan = new CustomerPlanGH();
                    var planInDB = this.Get(planid);
                    cplan.ConStrength = planInDB.ConStrength;
                    cplan.ContractID = planInDB.ContractID;
                    cplan.BuildTime = DateTime.Now;
                    cplan.ProjectName = planInDB.ProjectName;
                    cplan.ConsPos = planInDB.ConsPos;
                    cplan.Slump = planInDB.Slump;
                    cplan.CastMode = planInDB.CastMode;
                    cplan.PlanCube = planInDB.PlanCube;
                    cplan.NeedDate = planInDB.NeedDate;
                    cplan.PlanDate = planInDB.PlanDate;

                    cplan.ContractNo = planInDB.ContractNo;
                    cplan.ConstructUnit = planInDB.ConstructUnit;
                    cplan.ProjectAddr = planInDB.ProjectAddr;
                    cplan.Tel = planInDB.Tel;
                    cplan.LinkMan = planInDB.LinkMan; 
                    cplan.SupplyUnit = planInDB.SupplyUnit;
                    cplan.DeliveryAddress = planInDB.DeliveryAddress;
                    cplan.BuildUnit = planInDB.BuildUnit;
                    cplan.PumpName = planInDB.PumpName;
                    cplan.PumpMan = planInDB.PumpMan;
                    this.Add(cplan);
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error("复制工地计划单失败", ex);
                    throw new ApplicationException("复制工地计划单失败:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取砼单价
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="conStrength"></param>
        /// <param name="produceDate"></param>
        /// <returns></returns>
        public decimal GetConGHPrice(string contractID, string conStrength, DateTime produceDate)
        {
            return this.m_UnitOfWork.GetConPriceRepository.GetConGHPrice(contractID, conStrength, produceDate);
        }
    }
}
