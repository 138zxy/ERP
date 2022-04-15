using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    public abstract class _SC_PiaoInOrder : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(OrderNo);
            sb.Append(SupplierID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("订单号")]
        [Required]
        [StringLength(50)]
        public virtual string OrderNo { get; set; }

        [DisplayName("供应商")]
        public virtual int SupplierID { get; set; }

        [DisplayName("交货日期")]
        public virtual DateTime? DeliveryDate { get; set; }

        [DisplayName("经办人")]
        public virtual string Operator { get; set; }

        [DisplayName("品种数")]
        public virtual int VarietyNum { get; set; }

        [DisplayName("订单金额")]
        public virtual decimal OrderMoney { get; set; }

        [DisplayName("仓库")]
        public virtual int? LibID { get; set; }

        [DisplayName("状态")]
        public virtual string Condition { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [DisplayName("单据号")]
        public virtual string PiaoNo { get; set; }

        [DisplayName("日期")]
        public virtual DateTime? PiaoDate { get; set; }

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
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(128)]
        public virtual string AuditInfo
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
  


