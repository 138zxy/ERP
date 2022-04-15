using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Material
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _M_TransPrice : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(TransPriceID);
            sb.Append(AuditStatus);
            sb.Append(AuditTime);
            sb.Append(AuditInfo);
            sb.Append(Auditor);
            sb.Append(AuditStatus2);
            sb.Append(AuditTime2);
            sb.Append(AuditInfo2);
            sb.Append(Auditor2);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("运输ID")]
        [Required]
        public virtual int TransPriceID { get; set; }

        [DisplayName("日期")]
        public virtual DateTime PriceDate { get; set; }

        [DisplayName("单价")]
        public virtual decimal UnitPrice { get; set; } 

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("一审状态")]
        public virtual int? AuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("一审时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("一审意见")]
        [StringLength(128)]
        public virtual string AuditInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("一审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("二审状态")]
        public virtual int? AuditStatus2
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("二审时间")]
        public virtual System.DateTime? AuditTime2
        {
            get;
            set;
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("二审意见")]
        [StringLength(128)]
        public virtual string AuditInfo2
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("二审核人")]
        [StringLength(30)]
        public virtual string Auditor2
        {
            get;
            set;
        }
        #endregion
    }
}
