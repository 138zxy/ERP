using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_PiaoChange : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ChangeName);
            sb.Append(ChangNo); 
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion
        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public virtual string ChangeName { get; set; }
        [DisplayName("日期")]
        public virtual DateTime ChangeDate { get; set; }
        [DisplayName("单号")]
        [Required]
        [StringLength(50)]
        public virtual string ChangNo { get; set; }

        [DisplayName("经办人")] 
        [StringLength(50)]
        public virtual string Operater { get; set; }

        [DisplayName("数量")]
        public virtual int Quantity { get; set; } 

        [DisplayName("单价")]
        public virtual decimal PriceUnit { get; set; }

        [DisplayName("金额")]
        public virtual decimal ChangeMoney { get; set; }


        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

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
  


