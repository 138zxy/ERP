using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    public abstract class _SC_NowLib : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(GoodsID);
            sb.Append(LibID); 
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion
        [DisplayName("商品")]
        [Required]
        public virtual int GoodsID { get; set; }

        [DisplayName("仓库")]
        [Required]
        public virtual int LibID { get; set; }

        [DisplayName("在库库存单价")]
        public virtual decimal PirceUnit { get; set; }

        [DisplayName("数量")]
        public virtual decimal Quantity { get; set; }


        [DisplayName("最后入库异动日期")]
        public virtual DateTime?  InDate{ get; set; }

        [DisplayName("最后出库异动日期")]
        public virtual DateTime? OutDate { get; set; } 
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

    }
}
  


