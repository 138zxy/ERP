using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_PiaoTo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(OutLibID);
            sb.Append(InLibID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("调出仓库")]
        [Required]
        public virtual int OutLibID { get; set; }

        [DisplayName("调入仓库")]
        [Required]
        public virtual int InLibID { get; set; }

        [DisplayName("日期")]
        public virtual DateTime ChangeDate { get; set; }

        [DisplayName("单号")]
        [StringLength(50)]
        public virtual string ChangeNo { get; set; }

        [DisplayName("品种数")]
        public virtual int VarietyNum { get; set; }

        [DisplayName("调库金额")]
        public virtual decimal ChangeMoney { get; set; }

        [DisplayName("状态")]
        [StringLength(50)]
        public virtual string Condition { get; set; }

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
  


