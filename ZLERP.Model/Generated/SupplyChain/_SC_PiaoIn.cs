using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
    /// <summary>
    /// 入库单
    /// </summary>
    public abstract class _SC_PiaoIn : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(LibID);
            sb.Append(SupplierID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [Required]
        [DisplayName("供应商")]
        public virtual int SupplierID { get; set; }

        [Required]
        [DisplayName("仓库")]
        public virtual int LibID { get; set; }

        [DisplayName("入库方式")]
        [Required]
        [StringLength(50)]
        public virtual string InType { get; set; }


        [DisplayName("入库日期")]
        public virtual DateTime InDate { get; set; }

        [DisplayName("入库单号")]
        [StringLength(50)]
        public virtual string InNo { get; set; }

        [DisplayName("采购员")]
        [StringLength(50)]
        public virtual string Purchase { get; set; }


        [DisplayName("品种数")]
        public virtual int VarietyNum { get; set; }

        [DisplayName("入库金额")]
        public virtual decimal InMoney { get; set; }

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


        [DisplayName("采购单")]
        public virtual int? PurchaseID { get; set; }

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
      
    }
}
  


