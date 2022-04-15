using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Reflection;
using ZLERP.License.Client;
using ZLERP.Business.SupplyChain;
using ZLERP.Business.HR;
using ZLERP.Business.Material;
using ZLERP.Business.Beton;
namespace ZLERP.Business
{
    /// <summary>
    /// 公共服务类
    /// </summary>
    public class PublicService  :IDisposable
    {
        #region 公共属性
        private IUnitOfWork m_UnitOfWork;

        //public PublicService(IUnitOfWork uow) {
        //    this.m_UnitOfWork = uow;
        //}
        #endregion

        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public PublicService()
        {
            if (UnitOfWork.IsStarted)
                this.m_UnitOfWork = UnitOfWork.Current;
            else
            {
                this.m_UnitOfWork = UnitOfWork.Start();
            }
            _LicenseInfo = GetLic(); //zjy
        }
        #endregion

        #region 注册服务
        public License.Client.License _LicenseInfo;
        public IList<string> LicensedFuncIds()
        {
            if (true)
                return new string[] { };//zjy
            //return _LicenseInfo.UserData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        private static object licLock = new object();
        License.Client.License GetLic()
        {
            string cacheKey = "erp_license";
            string licensefile=System.Web.HttpContext.Current.Server.MapPath("~/license.lic");
            if (System.IO.File.Exists(licensefile))
            {
                return CacheHelper.GetCacheItem<License.Client.License>(cacheKey,
                    licLock,
                    System.Web.HttpContext.Current.Server.MapPath("~/license.lic"),
                    delegate
                    {
                        return License.Client._License.GetLicense();
                    });
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 通用服务，提供基本的数据操作
        /// <summary>
        /// 通用服务，提供基本的数据操作
        /// </summary>
        public ServiceBase<TEntity> GetGenericService<TEntity>() where TEntity: Entity
        { 
            //UNDONE:使用反射查找对应model的service,有则返回，否则返回ServiceBase
            //此处用以在具体的实体Service中重写ServiceBase中的方法
            PropertyInfo pi = this.GetType().GetProperty(typeof(TEntity).Name);
            if (pi != null) {
                var svc = this.GetType().InvokeMember(typeof(TEntity).Name, BindingFlags.GetProperty, null, this, null);
                if (svc is ServiceBase<TEntity>)
                    return svc as ServiceBase<TEntity>;
            }             
            return new ServiceBase<TEntity>(m_UnitOfWork); 
              
        }
        #endregion

        #region 系统

        private UserService m_User;
        /// <summary>
        /// 用户
        /// </summary>
        public UserService User
        {
            get
            {
                if (this.m_User == null)
                {
                    this.m_User = new UserService(this.m_UnitOfWork);
                }
                return this.m_User;
            }
        }

        private SysConfigService m_SysConfig;
        /// <summary>
        /// 系统配置项
        /// </summary>
        public SysConfigService SysConfig
        {
            get
            {
                if (this.m_SysConfig == null)
                {
                    this.m_SysConfig = new SysConfigService(this.m_UnitOfWork);
                }
                return this.m_SysConfig;
            }
        }


        private DicService m_Dic;
        /// <summary>
        /// 字典
        /// </summary>
        public DicService Dic
        {
            get
            {
                if (this.m_Dic == null)
                {
                    this.m_Dic = new DicService(this.m_UnitOfWork);
                }
                return this.m_Dic;
            }
        }

        private SysFuncService m_SysFunc;
        /// <summary>
        /// 系统功能模块
        /// </summary>
        public SysFuncService SysFunc
        {
            get
            {
                if (this.m_SysFunc == null)
                {
                    this.m_SysFunc = new SysFuncService(this.m_UnitOfWork);
                }

                return this.m_SysFunc;
            }
        }

        private SysFuncAPPService m_SysFuncAPP;
        /// <summary>
        /// 系统功能模块
        /// </summary>
        public SysFuncAPPService SysFuncAPP
        {
            get
            {
                if (this.m_SysFuncAPP == null)
                {
                    this.m_SysFuncAPP = new SysFuncAPPService(this.m_UnitOfWork);
                }

                return this.m_SysFuncAPP;
            }
        }
        private RoleService m_Role;
        /// <summary>
        /// 角色
        /// </summary>
        public RoleService Role
        {
            get
            {
                if (this.m_Role == null)
                {
                    this.m_Role = new RoleService(this.m_UnitOfWork);
                }
                return this.m_Role;
            }
        }
        private UserRoleService m_UserRole;
        /// <summary>
        /// 角色用户
        /// </summary>
        public UserRoleService UserRole
        {
            get
            {
                if (this.m_UserRole == null)
                {
                    this.m_UserRole = new UserRoleService(this.m_UnitOfWork);
                }
                return this.m_UserRole;
            }
        }

        private StuffSaleService m_StuffSale;
        /// <summary>
        /// 原材料销售模块
        /// </summary>
        public StuffSaleService StuffSale
        {
            get
            {
                if (this.m_StuffSale == null)
                {
                    this.m_StuffSale = new StuffSaleService(this.m_UnitOfWork);
                }

                return this.m_StuffSale;
            }
        }


        private SynNCService m_SynNC;
        /// <summary>
        /// 原材料销售模块
        /// </summary>
        public SynNCService SynNC
        {
            get
            {
                if (this.m_SynNC == null)
                {
                    this.m_SynNC = new SynNCService(this.m_UnitOfWork);
                }

                return this.m_SynNC;
            }
        }


        private SynInfoNCService m_SynInfoNC;
        /// <summary>
        /// NC
        /// </summary>
        public SynInfoNCService SynInfoNC
        {
            get
            {
                if (this.m_SynInfoNC == null)
                {
                    this.m_SynInfoNC = new SynInfoNCService(this.m_UnitOfWork);
                }

                return this.m_SynInfoNC;
            }
        }


        private StuffinfoSpecService m_StuffinfoSpec;
        /// <summary>
        /// 原材料规格
        /// </summary>
        public StuffinfoSpecService StuffinfoSpec
        {
            get
            {
                if (this.m_StuffinfoSpec == null)
                {
                    this.m_StuffinfoSpec = new StuffinfoSpecService(this.m_UnitOfWork);
                }

                return this.m_StuffinfoSpec;
            }
        }
        #endregion

        #region 业务

        private SaleDailyRecordService m_SaleDailyRecordService;
        /// <summary>
        /// 日结算单
        /// </summary>
        public SaleDailyRecordService SaleDailyRecord
        {
            get
            {
                if (this.m_SaleDailyRecordService == null)
                {
                    this.m_SaleDailyRecordService = new SaleDailyRecordService(this.m_UnitOfWork);
                }
                return this.m_SaleDailyRecordService;
            }
        }

        private OweMoneyService m_OweMoneyService;
        /// <summary>
        /// 结算单
        /// </summary>
        public OweMoneyService OweMoney
        {
            get
            {
                if (this.m_OweMoneyService == null)
                {
                    this.m_OweMoneyService = new OweMoneyService(this.m_UnitOfWork);
                }
                return this.m_OweMoneyService;
            }
        }


        private StuffInfoService m_StuffInfo;
        /// <summary>
        /// 材料
        /// </summary>
        public StuffInfoService StuffInfo
        {
            get
            {
                if (this.m_StuffInfo == null)
                {
                    this.m_StuffInfo = new StuffInfoService(this.m_UnitOfWork);
                }
                return this.m_StuffInfo;
            }
        }
         

        private CompanyService m_Company;
        /// <summary>
        /// 公司
        /// </summary>
        public CompanyService Company {
            get {
                if (this.m_Company == null) {
                    this.m_Company = new CompanyService(this.m_UnitOfWork);
                }
                return this.m_Company;
            }
        }

        private ProduceTaskService m_ProduceTask;
        /// <summary>
        /// 生产任务单
        /// </summary>
        public ProduceTaskService ProduceTask
        {
            get
            {
                if (this.m_ProduceTask == null)
                {
                    this.m_ProduceTask = new ProduceTaskService(this.m_UnitOfWork);
                }
                return this.m_ProduceTask;
            }
        }

        private CustomerService m_Customer;
        /// <summary>
        /// 客户
        /// </summary>
        public CustomerService Customer {
            get {
                if (this.m_Customer == null) {
                    this.m_Customer = new CustomerService(this.m_UnitOfWork);
                }

                return this.m_Customer;
            }
        }

        private TyreChangeService m_TyreChange;
        /// <summary>
        /// 轮胎更换
        /// </summary>
        public TyreChangeService TyreChange
        {
            get
            {
                if (this.m_TyreChange == null)
                {
                    this.m_TyreChange = new TyreChangeService(this.m_UnitOfWork);
                }

                return this.m_TyreChange;
            }
        }

        private TyreRepairService m_TyreRepair;
        /// <summary>
        /// 轮胎维修
        /// </summary>
        public TyreRepairService TyreRepair
        {
            get
            {
                if (this.m_TyreRepair == null)
                {
                    this.m_TyreRepair = new TyreRepairService(this.m_UnitOfWork);
                }

                return this.m_TyreRepair;
            }
        }

        private ContractService m_Contract;
        /// <summary>
        /// 合同
        /// </summary>
        public ContractService Contract {
            get {
                if (this.m_Contract == null) {
                    this.m_Contract = new ContractService(this.m_UnitOfWork);
                }

                return this.m_Contract;
            }
        }

        private ContractOtherPriceService m_ContractOtherPrice;
        /// <summary>
        /// 合同
        /// </summary>
        public ContractOtherPriceService ContractOtherPrice
        {
            get
            {
                if (this.m_ContractOtherPrice == null)
                {
                    this.m_ContractOtherPrice = new ContractOtherPriceService(this.m_UnitOfWork);
                }

                return this.m_ContractOtherPrice;
            }
        }

        private ConStrengthService m_ConStrength;
        /// <summary>
        /// 砼强度数据
        /// </summary>
        public ConStrengthService ConStrength {
            get
            {
                if (this.m_ConStrength == null)
                {
                    this.m_ConStrength = new ConStrengthService(this.m_UnitOfWork);
                }

                return this.m_ConStrength;
            }
        }


        private ProductLineService m_ProductLineService;
        /// <summary>
        /// 生产线
        /// </summary>
        public ProductLineService ProductLine
        {
            get
            {
                if (this.m_ProductLineService == null)
                {
                    this.m_ProductLineService = new ProductLineService(this.m_UnitOfWork);
                }
                return this.m_ProductLineService;
            }
        }


        private DispatchListService m_DispatchListService;
        /// <summary>
        /// 生产调度
        /// </summary>
        public DispatchListService DispatchList
        {
            get
            {
                if (this.m_DispatchListService == null)
                {
                    this.m_DispatchListService = new DispatchListService(this.m_UnitOfWork);
                }
                return this.m_DispatchListService;
            }
        }


        private CarService m_CarService;
        /// <summary>
        /// 生产调度
        /// </summary>
        public CarService Car
        {
            get
            {
                if (this.m_CarService == null)
                {
                    this.m_CarService = new CarService(this.m_UnitOfWork);
                }
                return this.m_CarService;
            }
        }

        private AlarmLogService m_AlarmLogService;
        /// <summary>
        /// 报警日志
        /// </summary>
        public AlarmLogService AlarmLog
        {
            get
            {
                if (this.m_AlarmLogService == null)
                {
                    this.m_AlarmLogService = new AlarmLogService(this.m_UnitOfWork);
                }
                return this.m_AlarmLogService;
            }
        }

 



        private ShippingDocumentService m_ShippingDocumentService;
        /// <summary>
        /// 发货单 service
        /// </summary>
        public ShippingDocumentService ShippingDocument
        {
            get
            {
                if (this.m_ShippingDocumentService == null)
                {
                    this.m_ShippingDocumentService = new ShippingDocumentService(this.m_UnitOfWork);
                }
                return this.m_ShippingDocumentService;
            }
        }



        private FormulaItemService m_FormulaItemService;
        /// <summary>
        /// 标准配比细表
        /// </summary>
        public FormulaItemService FormulaItem
        {
            get
            {
                if (this.m_FormulaItemService == null)
                {
                    this.m_FormulaItemService = new FormulaItemService(this.m_UnitOfWork);
                }
                return this.m_FormulaItemService;
            }
        }

        private SiloService m_SiloService;
        /// <summary>
        /// 材料
        /// </summary>
        public SiloService Silo
        {
            get
            {
                if (this.m_SiloService == null)
                {
                    this.m_SiloService = new SiloService(this.m_UnitOfWork);
                }
                return this.m_SiloService;
            }
        }

        private TZRalationService m_TZRalation;
        /// <summary>
        /// 退料转发
        /// </summary>
        public TZRalationService TZRalation
        {
            get
            {
                if (this.m_TZRalation == null)
                {
                    this.m_TZRalation = new TZRalationService(this.m_UnitOfWork);
                }
                return this.m_TZRalation;
            }
        }

        private FormulaService m_FormulaService;
        /// <summary>
        /// 标准配比主表
        /// </summary>
        public FormulaService Formula
        {
            get
            {
                if (this.m_FormulaService == null)
                {
                    this.m_FormulaService = new FormulaService(this.m_UnitOfWork);
                }
                return this.m_FormulaService;
            }
        }

        private ContractPumpService m_ContractPumpService;
        /// <summary>
        /// 泵车价格设定
        /// </summary>
        public ContractPumpService ContractPumpService
        {
            get
            {
                if (this.m_ContractPumpService == null)
                {
                    this.m_ContractPumpService = new ContractPumpService(this.m_UnitOfWork);
                }
                return this.m_ContractPumpService;
            }
        }
 

        private CustMixpropItemService m_CustMixpropItemService;
        /// <summary>
        /// 客户配比用量
        /// </summary>
        public CustMixpropItemService CustMixpropItem
        {
            get
            {
                if (this.m_CustMixpropItemService == null)
                {
                    this.m_CustMixpropItemService = new CustMixpropItemService(this.m_UnitOfWork);
                }
                return this.m_CustMixpropItemService;
            }
        }
         
        private ConsMixpropService m_ConsMixpropService;
        /// <summary>
        /// 施工配比
        /// </summary>
        public ConsMixpropService ConsMixprop 
        {
            get
            {
                if (this.m_ConsMixpropService == null)
                {
                    this.m_ConsMixpropService = new ConsMixpropService(this.m_UnitOfWork);
                }
                return this.m_ConsMixpropService;
            }
        }

        private CommissionItemService m_CommissionItemService;
        /// <summary>
        /// 委托项目明细
        /// </summary>
        public CommissionItemService CommissionItem
        {
            get
            {
                if (this.m_CommissionItemService == null)
                {
                    this.m_CommissionItemService = new CommissionItemService(this.m_UnitOfWork);
                }
                return this.m_CommissionItemService;
            }
        }
        private SysLogService m_SysLogService;
        /// <summary>
        /// 系统日志
        /// </summary>
        public SysLogService SysLog
        {
            get
            {
                if (this.m_SysLogService == null)
                {
                    this.m_SysLogService = new SysLogService(this.m_UnitOfWork);
                }
                return this.m_SysLogService;
            }
        }
        private AttachmentService m_AttachmentService;
        /// <summary>
        /// 附件
        /// </summary>
        public AttachmentService Attachment
        {
            get
            {
                if (this.m_AttachmentService == null)
                {
                    this.m_AttachmentService = new AttachmentService(this.m_UnitOfWork);
                }
                return this.m_AttachmentService;
            }
        }

        private PumpWorkService m_PumpWorkService;
        public PumpWorkService PumpWork
        {
            get
            {
                if (this.m_PumpWorkService == null)
                {
                    this.m_PumpWorkService = new PumpWorkService(this.m_UnitOfWork);
                }
                return this.m_PumpWorkService;
            }
        }
        /// <summary>
        /// 强度等级评定
        /// </summary>
        private ConStrAssessService m_ConStrAssessService;
        public ConStrAssessService ConStrAssess
        {
            get
            {
                if (this.m_ConStrAssessService == null)
                {
                    this.m_ConStrAssessService = new ConStrAssessService(this.m_UnitOfWork);
                }
                return this.m_ConStrAssessService;
            }
        }

        /// <summary>
        /// 施工配比子项
        /// </summary>
        private ConsMixpropItemService m_ConsMixpropItemService;
        public ConsMixpropItemService ConsMixpropItem
        {
            get
            {
                if (this.m_ConsMixpropItemService == null)
                {
                    this.m_ConsMixpropItemService = new ConsMixpropItemService(this.m_UnitOfWork);
                }
                return this.m_ConsMixpropItemService;
            }
        }
        private CarOilService m_CarOilService;
        /// <summary>
        /// 车辆油耗
        /// </summary>
        public CarOilService CarOil
        {
            get
            {
                if (this.m_CarOilService == null)
                {
                    this.m_CarOilService = new CarOilService(this.m_UnitOfWork);
                }
                return this.m_CarOilService;
            }
        }


        private ProjectRouteService m_ProjectRouteService;
        /// <summary>
        /// 工地路线
        /// </summary>
        public ProjectRouteService ProjectRoute
        {
            get
            {
                if (this.m_ProjectRouteService == null)
                {
                    this.m_ProjectRouteService = new ProjectRouteService(this.m_UnitOfWork);
                }

                return this.m_ProjectRouteService;
            }
        }

        private StuffInService m_StuffInService;
        /// <summary>
        /// 材料入库
        /// </summary>
        public StuffInService StuffIn
        {
            get
            {
                if (this.m_StuffInService == null)
                {
                    this.m_StuffInService = new StuffInService(this.m_UnitOfWork);
                }
                return this.m_StuffInService;
            }
        }
         

        private CustomerPlanService m_CustomerPlanService;
        /// <summary>
        /// 工地计划
        /// </summary>
        public CustomerPlanService CustomerPlan
        {
            get
            {
                if (this.m_CustomerPlanService == null)
                {
                    this.m_CustomerPlanService = new CustomerPlanService(this.m_UnitOfWork);
                }
                return this.m_CustomerPlanService;
            }
        }

        private ProjectService m_Project;
        /// <summary>
        /// 工地
        /// </summary>
        public ProjectService Project
        {
            get
            {
                if (this.m_Project == null)
                {
                    this.m_Project = new ProjectService(this.m_UnitOfWork);
                }


                return this.m_Project;
            }
        }
        private CEExamService m_CEExamServic;
        /// <summary>
        /// 水泥试验
        /// </summary>
        public CEExamService CEExam
        {
            get
            {
                if (this.m_CEExamServic == null)
                {
                    this.m_CEExamServic = new CEExamService(this.m_UnitOfWork);
                }
                return this.m_CEExamServic;
            }
        }
        private FAExamService m_FAExamServic;
        /// <summary>
        /// 细骨料试验
        /// </summary>
        public FAExamService FAExam
        {
            get
            {
                if (this.m_FAExamServic == null)
                {
                    this.m_FAExamServic = new FAExamService(this.m_UnitOfWork);
                }
                return this.m_FAExamServic;
            }
        }

        private CAExamService m_CAExamServic;
        /// <summary>
        /// 粗骨料试验
        /// </summary>
        public CAExamService CAExam
        {
            get
            {
                if (this.m_CAExamServic == null)
                {
                    this.m_CAExamServic = new CAExamService(this.m_UnitOfWork);
                }
                return this.m_CAExamServic;
            }
        }

        private AIR1ExamService m_AIR1ExamServic;
        /// <summary>
        /// 粉煤灰试验
        /// </summary>
        public AIR1ExamService AIR1Exam
        {
            get
            {
                if (this.m_AIR1ExamServic == null)
                {
                    this.m_AIR1ExamServic = new AIR1ExamService(this.m_UnitOfWork);
                }
                return this.m_AIR1ExamServic;
            }
        }
        private AIR2ExamService m_AIR2ExamServic;
        /// <summary>
        /// 矿粉试验
        /// </summary>
        public AIR2ExamService AIR2Exam
        {
            get
            {
                if (this.m_AIR2ExamServic == null)
                {
                    this.m_AIR2ExamServic = new AIR2ExamService(this.m_UnitOfWork);
                }
                return this.m_AIR2ExamServic;
            }
        }

        private ADMExamService m_ADMExamServic;
        /// <summary>
        ///外加剂试验
        /// </summary>
        public ADMExamService ADMExam
        {
            get
            {
                if (this.m_ADMExamServic == null)
                {
                    this.m_ADMExamServic = new ADMExamService(this.m_UnitOfWork);
                }
                return this.m_ADMExamServic;
            }
        }

        private ADM2ExamService m_ADM2ExamServic;
        /// <summary>
        ///外加剂试验
        /// </summary>
        public ADM2ExamService ADM2Exam
        {
            get
            {
                if (this.m_ADM2ExamServic == null)
                {
                    this.m_ADM2ExamServic = new ADM2ExamService(this.m_UnitOfWork);
                }
                return this.m_ADM2ExamServic;
            }
        }
        private QualityExamService m_QualityExamService;
        /// <summary>
        ///品质试验
        /// </summary>
        public QualityExamService QualityExam
        {
            get
            {
                if (this.m_QualityExamService == null)
                {
                    this.m_QualityExamService = new QualityExamService(this.m_UnitOfWork);
                }
                return this.m_QualityExamService;
            }
        }

        private CustMixpropService m_CustMixpropService;
        /// <summary>
        ///客户配比信息
        /// </summary>
        public CustMixpropService CustMixprop
        {
            get
            {
                if (this.m_CustMixpropService == null)
                {
                    this.m_CustMixpropService = new CustMixpropService(this.m_UnitOfWork);
                }
                return this.m_CustMixpropService;
            }
        }

    

        private ProductRecordItemService m_ProductRecordItemService;
        /// <summary>
        ///工具借用信息
        /// </summary>
        public ProductRecordItemService ProductRecordItemService
        {
            get
            {
                if (this.m_ProductRecordItemService == null)
                {
                    this.m_ProductRecordItemService = new ProductRecordItemService(this.m_UnitOfWork);
                }
                return this.m_ProductRecordItemService;
            }
        }

        private IdentitySettingService m_IdentitySettingService;
        /// <summary>
        /// 特性设定
        /// </summary>
        public IdentitySettingService IdentitySettingService
        {
            get
            {
                if (this.m_IdentitySettingService == null)
                {
                    this.m_IdentitySettingService = new IdentitySettingService(this.m_UnitOfWork);
                }
                return this.m_IdentitySettingService;
            }
        }

        private SystemMsgService m_SystemMsgService;
        /// <summary>
        /// 消息主体
        /// </summary>
        public SystemMsgService SystemMsg
        {
            get
            {
                if (this.m_SystemMsgService == null)
                {
                    this.m_SystemMsgService = new SystemMsgService(this.m_UnitOfWork);
                }
                return this.m_SystemMsgService;
            }
        }

        private MyMsgService m_MyMsgService;
        /// <summary>
        /// 个人消息
        /// </summary>
        public MyMsgService MyMsg
        {
            get
            {
                if (this.m_MyMsgService == null)
                {
                    this.m_MyMsgService = new MyMsgService(this.m_UnitOfWork);
                }
                return this.m_MyMsgService;
            }
        }

        private YearAccountService m_YearAccountService;
        /// <summary>
        /// 年帐套
        /// </summary>
        public YearAccountService YearAccount
        {
            get
            {
                if (this.m_YearAccountService == null)
                {
                    this.m_YearAccountService = new YearAccountService(this.m_UnitOfWork);
                }
                return this.m_YearAccountService;
            }
        }

        private ProductRecordService m_ProductRecordService;
        /// <summary>
        /// 生产记录
        /// </summary>
        public ProductRecordService ProductRecord
        {
            get
            {
                if (this.m_ProductRecordService == null)
                {
                    this.m_ProductRecordService = new ProductRecordService(this.m_UnitOfWork);
                }
                return this.m_ProductRecordService;
            }
        }

        private CarHistoryService m_CarHistoryService;
        /// <summary>
        /// 车辆修改历史记录
        /// </summary>
        public CarHistoryService CarHistory
        {
            get
            {
                if (this.m_CarHistoryService == null)
                {
                    this.m_CarHistoryService = new CarHistoryService(this.m_UnitOfWork);
                }
                return this.m_CarHistoryService;
            }
        }

        private DutyPlanService m_DutyPlan;
        /// <summary>
        /// 值班计划管理
        /// </summary>
        public DutyPlanService DutyPlan
        {
            get
            {
                if (this.m_DutyPlan == null)
                {
                    this.m_DutyPlan = new DutyPlanService(this.m_UnitOfWork);
                }
                return this.m_DutyPlan;
            }
        }

        /// <summary>
        /// 提醒信息
        /// </summary>
        private RemindinfoService m_remindinfo;
        public RemindinfoService Remindinfo
        {
            get
            {
                if (this.m_remindinfo == null)
                {
                    this.m_remindinfo = new RemindinfoService(this.m_UnitOfWork);
                }
                return this.m_remindinfo;
            }
        }

        /// <summary>
        /// 手动生产记录
        /// </summary>
        private ManualProduceService m_ManualProduceService;
        public ManualProduceService ManualProduce
        {
            get
            {
                if (this.m_ManualProduceService == null)
                {
                    this.m_ManualProduceService = new ManualProduceService(this.m_UnitOfWork);
                }
                return this.m_ManualProduceService;
            }
        }


        private TZRalationHistoryService m_TZRalationHistoryService;
        /// <summary>
        /// 车辆修改历史记录
        /// </summary>
        public TZRalationHistoryService TZRalationHistory
        {
            get
            {
                if (this.m_TZRalationHistoryService == null)
                {
                    this.m_TZRalationHistoryService = new TZRalationHistoryService(this.m_UnitOfWork);
                }
                return this.m_TZRalationHistoryService;
            }
        }

        private SynmonitorService m_Synmonitor;
        /// <summary>
        /// syn table
        /// </summary>
        public SynmonitorService Synmonitor
        {
            get
            {
                if (this.m_Synmonitor == null)
                {
                    this.m_Synmonitor = new SynmonitorService(this.m_UnitOfWork);
                }
                return this.m_Synmonitor;
            }
        }
         
        private ThreadTableService m_ThreadTableService;
        /// <summary>
        /// 
        /// </summary>
        public ThreadTableService ThreadTable
        {
            get
            {
                if (this.m_ThreadTableService == null)
                {
                    this.m_ThreadTableService = new ThreadTableService(this.m_UnitOfWork);
                }
                return this.m_ThreadTableService;
            }
        }

  

        private StuffInfoPriceService m_StuffInfoPriceService;
        /// <summary>
        /// 原材料价格
        /// </summary>
        public StuffInfoPriceService StuffInfoPrice
        {
            get
            {
                if (this.m_StuffInfoPriceService == null)
                {
                    this.m_StuffInfoPriceService = new StuffInfoPriceService(this.m_UnitOfWork);
                }
                return this.m_StuffInfoPriceService;
            }
        }

        private StockPactService m_StockPactService;
        /// <summary>
        /// 采购合同
        /// </summary>
        public StockPactService StockPact
        {
            get
            {
                if (this.m_StockPactService == null)
                {
                    this.m_StockPactService = new StockPactService(this.m_UnitOfWork);
                }
                return this.m_StockPactService;
            }
        }
         
        private CarRepairService m_CarRepairService;
        /// <summary>
        /// 车维修
        /// </summary>
        public CarRepairService CarRepair
        {
            get
            {
                if (this.m_CarRepairService == null)
                {
                    this.m_CarRepairService = new CarRepairService(this.m_UnitOfWork);
                }
                return this.m_CarRepairService;
            }
        }



        private MonthAccountService m_MonthAccount;
        /// <summary>
        /// 月结处理
        /// </summary>
        public MonthAccountService MonthAccount
        {
            get
            {
                if (this.m_MonthAccount == null)
                {
                    this.m_MonthAccount = new MonthAccountService(this.m_UnitOfWork);
                }
                return this.m_MonthAccount;
            }
        }

        private StuffInPriceAdjustService m_StuffInPriceAdjust;
        /// <summary>
        /// 入库记录单价调整
        /// </summary>
        public StuffInPriceAdjustService StuffInPriceAdjust
        {
            get
            {
                if (this.m_StuffInPriceAdjust == null)
                {
                    this.m_StuffInPriceAdjust = new StuffInPriceAdjustService(this.m_UnitOfWork);
                }
                return this.m_StuffInPriceAdjust;
            }
        }

        private ThreadIDService m_ThreadIDService;
        /// <summary>
        /// 
        /// </summary>
        public ThreadIDService ThreadID
        {
            get
            {
                if (this.m_ThreadIDService == null)
                {
                    this.m_ThreadIDService = new ThreadIDService(this.m_UnitOfWork);
                }
                return this.m_ThreadIDService;
            }
        }

        private SupplyChainService m_SupplyChainService;
        /// <summary>
        /// 供应链管理公共基础事物服务类
        /// </summary>
        public SupplyChainService SupplyChainService
        {
            get
            {
                if (this.m_SupplyChainService == null)
                {
                    this.m_SupplyChainService = new SupplyChainService(this.m_UnitOfWork);
                }
                return this.m_SupplyChainService;
            }
        }

        private HRService  m_HRService;
        /// <summary>
        /// 人力资源基础事物服务类
        /// </summary>
        public HRService HRService
        {
            get
            {
                if (this.m_HRService == null)
                {
                    this.m_HRService = new HRService(this.m_UnitOfWork);
                }
                return this.m_HRService;
            }
        }

        private MaterialService m_MaterialService;
        /// <summary>
        /// 原材料基础事物服务类
        /// </summary>
        public MaterialService MaterialService
        {
            get
            {
                if (this.m_MaterialService == null)
                {
                    this.m_MaterialService = new MaterialService(this.m_UnitOfWork);
                }
                return this.m_MaterialService;
            }
        }

        private BetonService m_BetonService;
        /// <summary>
        /// 砼结算基础事物服务类
        /// </summary>
        public BetonService BetonService
        {
            get
            {
                if (this.m_BetonService == null)
                {
                    this.m_BetonService = new BetonService(this.m_UnitOfWork);
                }
                return this.m_BetonService;
            }
        }
        private ProduceTaskGHService m_ProduceTaskGH;
        /// <summary>
        /// 干混砂浆生产任务单
        /// </summary>
        public ProduceTaskGHService ProduceTaskGH
        {
            get
            {
                if (this.m_ProduceTaskGH == null)
                {
                    this.m_ProduceTaskGH = new ProduceTaskGHService(this.m_UnitOfWork);
                }
                return this.m_ProduceTaskGH;
            }
        }
        private ContractItemGHService m_ContractItemGH;
        /// <summary>
        /// 干混砂浆合同砼价格
        /// </summary>
        public ContractItemGHService ContractItemGH
        {
            get
            {
                if (this.m_ContractItemGH == null)
                {
                    this.m_ContractItemGH = new ContractItemGHService(this.m_UnitOfWork);
                }

                return this.m_ContractItemGH;
            }
        }

        private ContractGHService m_ContractGH;
        /// <summary>
        /// 干混砂浆合同
        /// </summary>
        public ContractGHService ContractGH
        {
            get
            {
                if (this.m_ContractGH == null)
                {
                    this.m_ContractGH = new ContractGHService(this.m_UnitOfWork);
                }

                return this.m_ContractGH;
            }
        }
        private DispatchListGHService m_DispatchListGHService;
        /// <summary>
        /// 生产调度
        /// </summary>
        public DispatchListGHService DispatchListGH
        {
            get
            {
                if (this.m_DispatchListGHService == null)
                {
                    this.m_DispatchListGHService = new DispatchListGHService(this.m_UnitOfWork);
                }
                return this.m_DispatchListGHService;
            }
        }

        private ShippingDocumentGHService m_ShippingDocumentGHService;
        /// <summary>
        /// 发货单 service
        /// </summary>
        public ShippingDocumentGHService ShippingDocumentGH
        {
            get
            {
                if (this.m_ShippingDocumentGHService == null)
                {
                    this.m_ShippingDocumentGHService = new ShippingDocumentGHService(this.m_UnitOfWork);
                }
                return this.m_ShippingDocumentGHService;
            }
        }
        private TZRalationGHService m_TZRalationGH;
        /// <summary>
        /// 退料转发
        /// </summary>
        public TZRalationGHService TZRalationGH
        {
            get
            {
                if (this.m_TZRalationGH == null)
                {
                    this.m_TZRalationGH = new TZRalationGHService(this.m_UnitOfWork);
                }
                return this.m_TZRalationGH;
            }
        }

        private ConsMixpropGHService m_ConsMixpropGHService;
        /// <summary>
        /// 施工配比
        /// </summary>
        public ConsMixpropGHService ConsMixpropGH
        {
            get
            {
                if (this.m_ConsMixpropGHService == null)
                {
                    this.m_ConsMixpropGHService = new ConsMixpropGHService(this.m_UnitOfWork);
                }
                return this.m_ConsMixpropGHService;
            }
        }

        /// <summary>
        /// 施工配比子项
        /// </summary>
        private ConsMixpropItemGHService m_ConsMixpropItemGHService;
        public ConsMixpropItemGHService ConsMixpropItemGH
        {
            get
            {
                if (this.m_ConsMixpropItemGHService == null)
                {
                    this.m_ConsMixpropItemGHService = new ConsMixpropItemGHService(this.m_UnitOfWork);
                }
                return this.m_ConsMixpropItemGHService;
            }
        }

        private CustomerPlanGHService m_CustomerPlanGHService;
        /// <summary>
        /// 干混砂浆工地计划
        /// </summary>
        public CustomerPlanGHService CustomerPlanGH
        {
            get
            {
                if (this.m_CustomerPlanGHService == null)
                {
                    this.m_CustomerPlanGHService = new CustomerPlanGHService(this.m_UnitOfWork);
                }
                return this.m_CustomerPlanGHService;
            }
        }
        private ProjectGHService m_ProjectGH;
        /// <summary>
        /// 干混砂浆工地
        /// </summary>
        public ProjectGHService ProjectGH
        {
            get
            {
                if (this.m_ProjectGH == null)
                {
                    this.m_ProjectGH = new ProjectGHService(this.m_UnitOfWork);
                }


                return this.m_ProjectGH;
            }
        }

        private TZRalationHistoryGHService m_TZRalationHistoryGHService;
        /// <summary>
        /// 车辆修改历史记录
        /// </summary>
        public TZRalationHistoryGHService TZRalationHistoryGH
        {
            get
            {
                if (this.m_TZRalationHistoryGHService == null)
                {
                    this.m_TZRalationHistoryGHService = new TZRalationHistoryGHService(this.m_UnitOfWork);
                }
                return this.m_TZRalationHistoryGHService;
            }
        }

        private ShippingDocumentNoGHService m_ShippingDocumentNoGHService;
        /// <summary>
        /// 
        /// </summary>
        public ShippingDocumentNoGHService ShippingDocumentNoGH
        {
            get
            {
                if (this.m_ShippingDocumentNoGHService == null)
                {
                    this.m_ShippingDocumentNoGHService = new ShippingDocumentNoGHService(this.m_UnitOfWork);
                }
                return this.m_ShippingDocumentNoGHService;
            }
        }

        private ContractInvoiceGHService m_ContractInvoiceGH;
        /// <summary>
        /// 销售发票
        /// </summary>
        public ContractInvoiceGHService ContractInvoiceGH
        {
            get
            {
                if (this.m_ContractInvoiceGH == null)
                {
                    this.m_ContractInvoiceGH = new ContractInvoiceGHService(this.m_UnitOfWork);
                }
                return this.m_ContractInvoiceGH;
            }
        }
        private FormulaGHItemService m_FormulaGHItemService;
        /// <summary>
        /// 标准配比细表(干混)
        /// </summary>
        public FormulaGHItemService FormulaGHItem
        {
            get
            {
                if (this.m_FormulaGHItemService == null)
                {
                    this.m_FormulaGHItemService = new FormulaGHItemService(this.m_UnitOfWork);
                }
                return this.m_FormulaGHItemService;
            }
        }

        private FormulaGHService m_FormulaGHService;
        /// <summary>
        /// 标准配比主表(干混)
        /// </summary>
        public FormulaGHService FormulaGH
        {
            get
            {
                if (this.m_FormulaGHService == null)
                {
                    this.m_FormulaGHService = new FormulaGHService(this.m_UnitOfWork);
                }
                return this.m_FormulaGHService;
            }
        }
    
        private FinishedGoodsWarehouseCountService m_FinishedGoodsWarehouseCountService;
        /// <summary>
        /// 成品仓当前数量表
        /// </summary>
        public FinishedGoodsWarehouseCountService FinishedGoodsWarehouseCount
        {
            get
            {
                if (this.m_FinishedGoodsWarehouseCountService == null)
                {
                    this.m_FinishedGoodsWarehouseCountService = new FinishedGoodsWarehouseCountService(this.m_UnitOfWork);
                }
                return this.m_FinishedGoodsWarehouseCountService;
            }
        }

        private FinishedGoodsWarehouseService m_FinishedGoodsWarehouseService;
        /// <summary>
        /// 成品仓进出记录表
        /// </summary>
        public FinishedGoodsWarehouseService FinishedGoodsWarehouse
        {
            get
            {
                if (this.m_FinishedGoodsWarehouseService == null)
                {
                    this.m_FinishedGoodsWarehouseService = new FinishedGoodsWarehouseService(this.m_UnitOfWork);
                }
                return this.m_FinishedGoodsWarehouseService;
            }
        }
        private BalanceService m_Balance;
        /// <summary>
        /// 余额
        /// </summary>
        public BalanceService Balance
        {
            get
            {
                if (this.m_Balance == null)
                {
                    this.m_Balance = new BalanceService(this.m_UnitOfWork);
                }

                return this.m_Balance;
            }
        }

        private StuffSellService m_StuffSell;
        /// <summary>
        /// 余额
        /// </summary>
        public StuffSellService StuffSell
        {
            get
            {
                if (this.m_StuffSell == null)
                {
                    this.m_StuffSell = new StuffSellService(this.m_UnitOfWork);
                }

                return this.m_StuffSell;
            }
        }

        private RoleOrganizationService m_RoleOrganization;
        /// <summary>
        /// 角色组织
        /// </summary>
        public RoleOrganizationService RoleOrganization
        {
            get
            {
                if (this.m_RoleOrganization == null)
                {
                    this.m_RoleOrganization = new RoleOrganizationService(this.m_UnitOfWork);
                }

                return this.m_RoleOrganization;
            }
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }
}
