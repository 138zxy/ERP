using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
 
    public abstract class _SC_ZhangChange : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ChangeID);
            sb.Append(GoodsID); 
            sb.Append(Version); 
            return sb.ToString().GetHashCode();
        }

        #endregion 
        [Required] 
        [DisplayName("主体ID")]
        public virtual int  ChangeID { get; set; }

        [DisplayName("仓库")]
        [Required]
        public virtual int LibID { get; set; }


        [DisplayName("商品")]
        public virtual int GoodsID { get; set; }

        [DisplayName("数量")]
        public virtual decimal Quantity { get; set; }


        [DisplayName("单价")]
        public virtual decimal PriceUnit { get; set; }

        [DisplayName("金额")]
        public virtual decimal ChangeMoney { get; set; }
 
    }
}
  


