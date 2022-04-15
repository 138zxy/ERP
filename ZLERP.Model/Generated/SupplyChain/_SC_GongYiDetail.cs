using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    public abstract class _SC_GongYiDetail : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(GongYiID);
            sb.Append(GoodsID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion
        [DisplayName("主体ID")]
        public virtual int GongYiID { get; set; }

        [DisplayName("商品")]
        public virtual int GoodsID { get; set; }

        [DisplayName("数量")]
        public virtual int Quantity { get; set; }
         

        [DisplayName("来源")]
        public virtual string FormSource { get; set; }


    }
}
  


