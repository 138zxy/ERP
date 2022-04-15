using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_PriceChange : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(GoodsID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("商品")]
        public virtual int GoodsID { get; set; }
        [DisplayName("当时数量")]
        public virtual decimal CurNum { get; set; }
        [DisplayName("调整金额")]
        public virtual decimal CurMoney { get; set; }
        [DisplayName("调前进价")]
        public virtual decimal CurPrice { get; set; }
        [DisplayName("调后进价")]
        public virtual decimal AlferPrice { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


