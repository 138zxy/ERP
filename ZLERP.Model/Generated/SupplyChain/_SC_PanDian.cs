using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_PanDian : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(LibID); 
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("仓库")] 
        public virtual int LibID { get; set; }

        [DisplayName("开始时间")]
        public virtual DateTime BeginDate { get; set; }

        [DisplayName("结束时间")]
        public virtual DateTime? EndDate { get; set; }


        [DisplayName("差异数量")]
        public virtual decimal DifferenceNum { get; set; }


        [DisplayName("差异金额")]
        public virtual decimal DifferenceMoney { get; set; }

        [DisplayName("是否结束")]
        public virtual bool IsOff { get; set; }

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
  


