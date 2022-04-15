using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Beton
{


    public abstract class _B_YingSFrecdetail : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(OrderNo);
            sb.Append(FinanceNo);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("付款记录")]
        [Required]
        public virtual int FinanceNo { get; set; }

        [DisplayName("订单号")]
        [StringLength(50)]
        public virtual int OrderNo { get; set; }

        [DisplayName("付款金额")] 
        public virtual decimal PayMoney { get; set; }

        [DisplayName("优惠金额")]
        public virtual decimal PayFavourable { get; set; }
    }
}
  


