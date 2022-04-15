using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_ZhangOut : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(OutNo);
            sb.Append(GoodsID); 
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion


        [DisplayName("出库单号")]
        public virtual int OutNo { get; set; }

        [DisplayName("商品")]
        public virtual int GoodsID { get; set; }
         

        [DisplayName("序号")]
        public virtual int? Sequence { get; set; }
         

        [DisplayName("出库时间")]
        public virtual DateTime? OutDate { get; set; }
 

        [DisplayName("数量")]
        public virtual decimal Quantity { get; set; }


        [DisplayName("单价")]
        public virtual decimal PriceUnit { get; set; }

        [DisplayName("金额")]
        public virtual decimal OutMoney { get; set; }
        [StringLength(50)]
        [DisplayName("批号")]
        public virtual string PiNo { get; set; } 

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
  


