using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_PanDianDetail : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(PanID);
            sb.Append(GoodsID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("盘点单")]
        [Required]
        public virtual int PanID { get; set; }

        [DisplayName("商品")]
        public virtual int GoodsID { get; set; }

        [DisplayName("库存数量")]
        public virtual decimal LibNum { get; set; }


        [DisplayName("盘点数量")]
        public virtual decimal PanNum { get; set; }

         [DisplayName("差异数量")]
        public virtual decimal DifferenceNum { get; set; }
         
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
 
    }
}
  


