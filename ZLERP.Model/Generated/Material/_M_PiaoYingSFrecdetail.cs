using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Material
{


    public abstract class _M_PiaoYingSFrecdetail : EntityBase<string>
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
        [DisplayName("开票记录")]
        [Required]
        public virtual int FinanceNo { get; set; }

        [DisplayName("订单号")]
        [StringLength(50)]
        public virtual int OrderNo { get; set; }

        [DisplayName("开票金额")] 
        public virtual decimal PayMoney { get; set; }

        [DisplayName("免开金额")]
        public virtual decimal PayFavourable { get; set; }
    }
}
  


