
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 针对合同加入砼价格明细，控制合同生产 合同明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ContractItemGH : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrength);
			sb.Append(UnPumpPrice);
			sb.Append(PumpCost);
			sb.Append(SlurryPrice);
			sb.Append(Version);
            sb.Append(BuildTime);
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 标号
        /// </summary>
        [DisplayName("标号")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 非泵价格
        /// </summary>
        [DisplayName("非泵价格")]
        public virtual decimal? UnPumpPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵送费
        /// </summary>
        [DisplayName("泵送费")]
        public virtual decimal? PumpCost
        {
            get;
			set;			 
        }
        /// <summary>
        /// 砂浆价格
        /// </summary>
        [DisplayName("砂浆价格")]
        public virtual decimal? SlurryPrice
        {
            get;
			set;			 
        }
        /// <summary>
        /// 基准砼强度
        /// </summary>
        [DisplayName("基准砼强度")]
        public virtual string BaseConStrength
        {
            get;
            set;
        }
        /// <summary>
        /// 额外费
        /// </summary>
        [DisplayName("额外费")]
        public virtual decimal? ExMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 运费
        /// </summary>
        [DisplayName("运费")]
        public virtual decimal TranPrice
        {
            get;
            set;
        }
        ///// <summary>
        ///// 建立时间
        ///// </summary>
        //[DisplayName("建立时间")]
        //public virtual DateTime BuildTime
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int AuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual ContractGH ContractGH
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<IdentitySetting> IdentitySettings
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ProduceTaskGH> ProduceTasksGH
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
