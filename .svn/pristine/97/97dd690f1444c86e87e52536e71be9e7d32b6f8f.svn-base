using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Beton
{


    public abstract class _B_YingSFrec : EntityBase<string>
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
        [DisplayName("运输单位")]
        [Required]
        public virtual string  UnitID { get; set; } 

        [DisplayName("收款或付款")]
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

        [DisplayName("金额")]
        public virtual decimal FinanceMoney { get; set; }

        [DisplayName("优惠额")]
        public virtual decimal PayFavourable { get; set; }

        [DisplayName("付款方式")]
        [StringLength(50)]
        public virtual string PayType { get; set; }


        [DisplayName("付款人")]
        [StringLength(50)]
        public virtual string Payer { get; set; }


        [DisplayName("收款人")]
        [StringLength(50)]
        public virtual string Gatheringer { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


