using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
    /// <summary>
    /// 出库单
    /// </summary>
    public abstract class _SC_PiaoOut : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(OutNo);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [DisplayName("使用人")]
        [StringLength(50)]
        public virtual string UserID { get; set; }

        [DisplayName("使用部门")]
        public virtual int? DepartmentID { get; set; }

        [Required]
        [DisplayName("仓库")]
        public virtual int LibID { get; set; }

        [DisplayName("出库方式")]
        [Required]
        [StringLength(50)]
        public virtual string OutType { get; set; }


        [DisplayName("出库日期")]
        public virtual DateTime? OutDate { get; set; }

        [DisplayName("出库单号")]
        [StringLength(50)]
        public virtual string OutNo { get; set; }

        [DisplayName("品种数")]
        public virtual int VarietyNum { get; set; }

        [DisplayName("出库金额")]
        public virtual decimal OutMoney { get; set; }

        [DisplayName("付款方式")]
        [StringLength(50)]
        public virtual string PayType { get; set; }

        [DisplayName("付款额")]
        public virtual decimal PayMoney { get; set; }


        [DisplayName("欠款额")]
        public virtual decimal PayOwing { get; set; }


        [DisplayName("优惠额")]
        public virtual decimal PayFavourable { get; set; }


        [DisplayName("状态")]
        [StringLength(50)]
        public virtual string Condition { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [DisplayName("单据号")]
        [StringLength(50)]
        public virtual string PiaoNo { get; set; }

        [DisplayName("评价")]
        [StringLength(200)]
        public virtual string Evaluate { get; set; }


        [DisplayName("申请单")]
        public virtual int? OutOrderID { get; set; }

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
        /// 维修关联的出库单
        /// </summary>
        [DisplayName("资产维修单")]
        public virtual int? MaintainID { get; set; }

        [DisplayName("车辆维修单")]
        public virtual int? CarMaintainID { get; set; }

    }
}
  


