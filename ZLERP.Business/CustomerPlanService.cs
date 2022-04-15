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
    public class CustomerPlanService : ServiceBase<CustomerPlan>
    {
        internal CustomerPlanService(IUnitOfWork uow) : base(uow) { }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditstatus"></param>
        /// <param name="audittime"></param>
        /// <param name="auditor"></param>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public bool Auditing(CustomerPlan plan)
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

                                //将工地计划的附件复制给生产任务
                                IRepositoryBase<Attachment> attRepo = this.m_UnitOfWork.GetRepositoryBase<Attachment>();
                                var attList = attRepo.Query().Where(att => att.ObjectId == plan.ID).ToList();
                                foreach (var att in attList)
                                {
                                    Attachment addEntity = new Attachment();
                                    addEntity.Title = att.Title;
                                    addEntity.FileType = att.FileType;
                                    addEntity.FileUrl = att.FileUrl;
                                    addEntity.FileSize = att.FileSize;
                                    addEntity.ObjectType = "ProduceTask";
                                    addEntity.ObjectId = TaskID;

                                    attRepo.Add(addEntity);
                                }
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

                CustomerPlan entity = Get(plan.ID);
                entity.AuditStatus = plan.AuditStatus;
                entity.AuditTime = DateTime.Now;
                entity.Auditor = AuthorizationService.CurrentUserID;
                entity.AuditInfo = plan.AuditInfo;
                entity.ConstructUnit = plan.ConstructUnit;
                entity.ProjectName = plan.ProjectName;
                entity.ProjectAddr = plan.ProjectAddr;
                entity.ConsPos = plan.ConsPos;
                entity.ConStrength = plan.ConStrength;
                entity.Slump = plan.Slump;
                entity.CastMode = plan.CastMode;
                entity.PumpName = plan.PumpName;
                entity.PiepLen = plan.PiepLen;
                entity.PumpMan = plan.PumpMan;
                entity.PlanCube = plan.PlanCube;
                entity.NeedDate = plan.NeedDate;
                entity.PlanDate = plan.PlanDate;
                entity.Tel = plan.Tel;
                entity.IdentityValue = plan.IdentityValue;
                entity.LinkMan = plan.LinkMan;
                entity.Remark = plan.Remark;
                entity.ContractID = plan.ContractID;
                entity.TaskID = plan.TaskID;
                entity.SupplyUnit = plan.SupplyUnit;
                entity.OtherDemand = plan.OtherDemand;
                entity.Distance = plan.Distance;
                entity.CarpGrade = plan.CarpGrade;
                entity.TaskNo = plan.TaskNo;
                entity.ChildProject = plan.ChildProject;
                entity.ProjectIDApp = plan.ProjectIDApp;
                this.Update(entity, null);
                //this.Update(plan, System.Web.HttpContext.Current.Request.Form);
                return true;
            }
            return false;
        }
        public bool MultAudit(string[] ids)
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
                            AuditResult = Auditing(plan);
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
        void CreateProduceTask(CustomerPlan cPlan, IUnitOfWork uow, out string TaskID)
        {
            try
            {
                var contractItem = CreateContractItem(cPlan, uow);
                int contractItemId = contractItem.ID ?? 0;
                TaskID = string.Empty; ;
                if (contractItem != null && contractItemId > 0)
                {
                    ProduceTask task = new ProduceTask();

                    task.ContractID = cPlan.ContractID;
                    task.ContractItemsID = contractItemId;
                    task.DataType = cPlan.DataType;
                    task.ConstructUnit = cPlan.ConstructUnit;
                    task.ProjectName = cPlan.ProjectName;
                    task.ProjectAddr = cPlan.ProjectAddr;
                    task.ConStrength = contractItem.ConStrength;
                    task.ConsPos = cPlan.ConsPos;
                    task.Slump = cPlan.Slump;
                    task.CastMode = cPlan.CastMode;
                    task.PlanCube = cPlan.PlanCube;
                    task.PumpType = cPlan.PumpName;
                    task.SupplyUnit = cPlan.SupplyUnit;
                    DateTime dt;
                    if (!DateTime.TryParse(cPlan.PlanDate.ToString("yyyy-MM-dd") + " " + cPlan.NeedDate, out dt))
                    {
                        DateTime.TryParse(cPlan.PlanDate.ToString("yyyy-MM-dd") + " 21:00:00", out dt);
                    }
                    task.NeedDate = dt;

                    task.Tel = cPlan.Tel;
                    task.LinkMan = cPlan.LinkMan;
                    task.Remark = cPlan.Remark; 
                    task.OtherDemand = cPlan.OtherDemand;
                    task.Distance = cPlan.Distance;
                    task.IdentityValue = cPlan.IdentityValue;
                    if (string.IsNullOrWhiteSpace(cPlan.IdentityValue))
                    {
                        task.BetonTag = cPlan.ConStrength;
                    }
                    else
                    {
                        task.BetonTag = cPlan.ConStrength + task.IdentityValue.Replace(",", "");
                    }
                    task.TaskType = TaskType.ContractTask;
                    ProduceTaskService pts = new ProduceTaskService(uow);
                    pts.CheckIsAutoAudit(task);
                    var project = pts.CreateProject(task);
                    task.ProjectID = project.ID;
                    task.CarpGrade = cPlan.CarpGrade;
                    task.TaskNo = cPlan.TaskNo;
                    task.ChildProject = cPlan.ChildProject;

                    //string ID = "";
                    //ProduceTask pj = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Query().Where(p => p.ID.Contains(DateTime.Now.ToString("yyMMdd"))).OrderByDescending(p => p.ID).FirstOrDefault();
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

                    task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Add(task);
                    TaskID = task.ID;
                    cPlan.TaskID = task.ID;
                    cPlan.ProjectIDApp = project.ID;
                    CreateProducePlan(cPlan);

        
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 检查合同明细项，不存在则创建新合同明细项
        /// </summary>
        /// <param name="cPlan"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        ContractItem CreateContractItem(CustomerPlan cPlan, IUnitOfWork uow)
        {
            var itemService = new ServiceBase<ContractItem>(uow);
            var item = itemService.Query()
                .Where(p => p.ContractID == cPlan.ContractID && p.ConStrength == cPlan.ConStrength)
                .FirstOrDefault();

            if (item == null)
            {
                //添加新ContractItem
                ConStrengthService conStrengthService = new ConStrengthService(uow);
                var conStrength = conStrengthService.Query()
                    .Where(p => p.ConStrengthCode == cPlan.ConStrength).FirstOrDefault();
                if (conStrength == null)
                {//添加砼强度
                    conStrength = conStrengthService.Add(new ConStrength { ConStrengthCode = cPlan.ConStrength,IsBase=true });
                }
                item = itemService.Add(new ContractItem { ConStrength = cPlan.ConStrength, ContractID = cPlan.ContractID, ConStrengthID = Convert.ToInt32(conStrength.ID), UnPumpPrice = conStrength.BrandPrice });
            }
            return item;
        }

        /// <summary>
        /// 创建生产计划
        /// </summary>
        /// <param name="cPlan"></param>
        void CreateProducePlan(CustomerPlan cPlan)
        {
            var service = new ServiceBase<ProducePlan>(this.m_UnitOfWork);

            var plan = new ProducePlan
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
        public bool CancelAudit(string[] ids,ref string errmsg)
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
                            AuditResult = CancelAuditing(plan);
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
        public bool CancelAuditing(CustomerPlan plan)
        {
            // var plan = this.Get(id);
            if (plan != null)
            {
                if (plan.TaskID == null)
                {
                    //更新CustomerPlan表记录
                    plan.AuditTime = null;
                    plan.Auditor = string.Empty;
                    this.Update(plan, null);
                    return true;
                }
                ProduceTask task = new ProduceTask();
                task = this.m_UnitOfWork.GetRepositoryBase<ProduceTask>().Get(plan.TaskID);
                if (task.IsFormulaSend)
                {
                    throw new Exception("单据已下发配比，不能取消审核！");
                }
                else
                {
                    //更新CustomerPlan表记录
                    plan.AuditTime = null;
                    plan.Auditor = string.Empty;
                    string TaskID = plan.TaskID;
                    plan.TaskID = null;
                    this.Update(plan, null);

                    PublicService ps = new PublicService();
                    if (TaskID != "")
                    {
                        //删除ProducePlan表记录
                        ProducePlan pplan = new ProducePlan();
                        pplan = this.m_UnitOfWork.GetRepositoryBase<ProducePlan>().Query().Where(p => p.TaskID.Contains(TaskID)).OrderByDescending(p => p.ID).FirstOrDefault();
                        if (pplan != null)
                        {
                            ps.GetGenericService<ProducePlan>().Delete(pplan);
                        }
                        //删除ProduceTask表记录
                        if (task != null)
                        {
                            ps.GetGenericService<ProduceTask>().Delete(task);
                        }
                    }

                    return true;
                }

            }
            return false;
        }
    }
}
