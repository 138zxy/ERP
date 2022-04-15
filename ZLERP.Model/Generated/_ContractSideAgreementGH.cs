
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ContractSideAgreementGH : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ContractID);
            sb.Append(ContractNo);
            sb.Append(BeginDate);
            sb.Append(ConStrength);
            sb.Append(Price);
            sb.Append(Version);
            sb.Append(Remark);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(50)]
        public virtual string ContractID
        {
            get;
            set;
        }
        /// <summary>
        /// 合同号
        /// </summary>
        [DisplayName("合同号")]
        [StringLength(50)]
        public virtual string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 执行时间
        /// </summary>
        [DisplayName("执行时间")]
        public virtual System.DateTime? BeginDate
        {
            get;
            set;
        }
        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("砼强度")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
            set;
        }
        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("价格")]
        public virtual decimal? Price
        {
            get;
            set;
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        [DisplayName("是否审核")]
        public virtual bool? IsAudit
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(50)]
        public virtual string AuditMan
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual DateTime? AuditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(150)]
        public virtual string Remark
        {
            get;
            set;
        }

        
        #endregion
    }
}
