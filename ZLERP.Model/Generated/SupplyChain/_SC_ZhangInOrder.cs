using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
 
    public abstract class _SC_ZhangInOrder : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(OrderNo);
            sb.Append(GoodsID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("订单票")]
        [Required] 
        public virtual int OrderNo { get; set; }
        [DisplayName("商品")]
        public virtual int GoodsID { get; set; }

        [DisplayName("数量")]
        public virtual decimal Quantity { get; set; }

        [DisplayName("单价")]
        public virtual decimal UnitPrice { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        [DisplayName("比率")]
        public virtual int UnitRate
        {
            get;
            set;
        }
        [DisplayName("单位")]
        public virtual string Unit { get; set; } 
    }
}
  


