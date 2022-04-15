using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Material
{


    public abstract class _M_PiaoYingSFrec : EntityBase<string>
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
        [DisplayName("供应商")]
        [Required]
        public virtual string UnitID { get; set; }

        [DisplayName("开票")]
        [StringLength(50)]
        public virtual string YingSF { get; set; }

        [DisplayName("来源")]
        [StringLength(50)]
        public virtual string Source { get; set; }

        [DisplayName("单据号")]
        [StringLength(50)]
        public virtual string FinanceNo { get; set; }

        [DisplayName("日期")]
        public virtual DateTime FinanceDate { get; set; }

        [DisplayName("开票金额")]
        public virtual decimal FinanceMoney { get; set; }

        [DisplayName("免开金额")]
        public virtual decimal PayFavourable { get; set; }

        [DisplayName("开票方式")]
        [StringLength(50)]
        public virtual string PayType { get; set; }


        [DisplayName("开票人")]
        [StringLength(50)]
        public virtual string Payer { get; set; }


        [DisplayName("收票人")]
        [StringLength(50)]
        public virtual string Gatheringer { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [StringLength(50)]
        [DisplayName("票号")]
        public virtual string PiaoNo { get; set; }

    }
}
  


