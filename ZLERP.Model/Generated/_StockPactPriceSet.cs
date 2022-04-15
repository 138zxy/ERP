﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    public abstract class _StockPactPriceSet : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(AuditStatus);
            sb.Append(AuditTime);
            sb.Append(AuditInfo);
            sb.Append(Auditor);
            sb.Append(AuditStatus2);
            sb.Append(AuditTime2);
            sb.Append(AuditInfo2);
            sb.Append(Auditor2);
            sb.Append(SpecID);
            sb.Append(TaxRate);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 改价时间
        /// </summary>
        [DisplayName("改价时间")]
        public virtual DateTime ChangeTime
        {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? Price
        {
            get;
            set;
        }
        /// <summary>
        /// 采购合同编号
        /// </summary>
        [DisplayName("价格编号")]
        public virtual string StockPactID { get; set; }

        /// <summary>
        /// 材料编号
        /// </summary>
        [DisplayName("材料编号")]
        public virtual string StuffID { get; set; }

        [DisplayName("备注")]
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

        /// <summary>
        /// 材料规格
        /// </summary>
        [DisplayName("材料规格")]
        public virtual int? SpecID
        {
            get;
            set;
        }
        /// <summary>
        /// 税率
        /// </summary>
        [DisplayName("税率")]
        public virtual decimal? TaxRate
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual StuffinfoSpec StuffinfoSpec
        {
            get;
            set;
        }
        #endregion
    }
}
