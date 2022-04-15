using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_ZhangTo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ChangeNo);
            sb.Append(GoodsID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("调拨单 ")]
        [Required]
        public virtual int ChangeNo { get; set; }

        [DisplayName("商品")]
        [Required]
        public virtual int GoodsID { get; set; }


        [DisplayName("数量")]
        public virtual int Quantity { get; set; }

        [DisplayName("库存单价")]
        public virtual decimal PriceUnit { get; set; }

        [DisplayName("调库金额")]
        public virtual decimal ChangeMoney { get; set; }

        [DisplayName("批号")]
        [StringLength(50)]
        public virtual string PiNo { get; set; }
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
  


