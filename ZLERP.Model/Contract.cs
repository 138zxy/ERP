using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    /// <summary>
    ///  合同信息
    /// </summary>
    public class Contract : _Contract
    {
        public virtual IList<Attachment> Attachments { get; set; }


        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        public virtual string CustName
        {
            get { return Customer == null ? "" : Customer.CustName; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
        [Required(ErrorMessage = "客户名称 是必须的")]
        public virtual string CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 合同其他加价
        /// </summary>
        [System.Web.Script.Serialization.ScriptIgnore]
        public virtual IList<ContractOtherPrice> OtherPrice
        {
            get;
            set;
        }


        /// <summary>
        /// 签约公司编号
        /// </summary>
        [DisplayName("签约公司编号")]
        public virtual int? SignCompanyID
        {
            get;
            set;
        }

        /// <summary>
        /// 签约公司
        /// </summary>
        [DisplayName("签约公司")]
        public virtual string SignCompany
        {
            get;
            set;
        }

        public virtual Customer Customer
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自动加载所有砼强度
        /// </summary>
        [DisplayName("基准砼强度")]
        public virtual bool IsAllConStrength
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自动加载所有泵送类型
        /// </summary>
        [DisplayName("基准泵送类型")]
        public virtual bool IsAllPumpType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否自动加载所有基准特性
        /// </summary>
        [DisplayName("基准特性")]
        public virtual bool IsAllIdentity
        {
            get;
            set;
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        [DisplayName("模板名称")]
        public virtual string FareTempletName
        {
            get
            {
                return B_FareTemplet == null ? "" : B_FareTemplet.FareTempletName;
            }
        }

        [ScriptIgnore]
        public virtual IList<AdvanceMoney> AdvanceMoneys
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ContractItem> ContractItems
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<ContractPump> ContractPumps
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<ProduceTask> ProduceTasks
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<Project> Projects
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<PumpWork> PumpWorks
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ContractLimit> ContractLimits
        {
            get;
            set;
        }




    }
}