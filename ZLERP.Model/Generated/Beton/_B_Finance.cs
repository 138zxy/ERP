using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Beton
{


    public abstract class _B_Finance : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(UnitID);
            sb.Append(FinanceNo);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [DisplayName("核准单号")]
        [Required]
        public virtual string FinanceNo { get; set; }

        [DisplayName("核准类型")]
    
        public virtual string Modeltype { get; set; }  

        [DisplayName("收付款方")]
        [Required]
        public virtual string  UnitID { get; set; }

        [DisplayName("收付款方名称")] 
        public virtual string UnitName { get; set; }


        [DisplayName("付款方式")]
        [StringLength(50)]
        public virtual string PayType { get; set; }


        [DisplayName("日期")]
        public virtual DateTime FinanceDate { get; set; }

 

        [DisplayName("金额")]
        public virtual decimal FinanceMoney { get; set; }

        [DisplayName("优惠额")]
        public virtual decimal PayFavourable { get; set; } 


        [DisplayName("付款人")]
        [StringLength(50)]
        public virtual string Payer { get; set; }


        [DisplayName("收款人")]
        [StringLength(50)]
        public virtual string Gatheringer { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
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
  


