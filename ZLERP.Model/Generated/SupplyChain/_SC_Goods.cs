using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    public abstract class _SC_Goods : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(GoodsName);
            sb.Append(Spec);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [DisplayName("商品编码")]
        [StringLength(50)]
        public virtual string GoodsCode { get; set; }


        [DisplayName("品名")]
        [Required]
        [StringLength(50)]
        public virtual string GoodsName { get; set; }

        [DisplayName("条码")]
        [StringLength(50)]
        public virtual string GoodsNo { get; set; }

        [DisplayName("拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }


        [DisplayName("规格")]
        [StringLength(50)]
        public virtual string Spec { get; set; }

        [DisplayName("最小计量单位")]
        [StringLength(50)]
        public virtual string Unit { get; set; }

        [DisplayName("预警下限")]
        public virtual decimal MinWarning { get; set; }
        [DisplayName("预警上限")]
        public virtual decimal MaxWarning { get; set; }

        [DisplayName("品牌")]
        [StringLength(50)]
        public virtual string Brand { get; set; }

        [DisplayName("分类")]
        public virtual int TypeNo { get; set; }

        [DisplayName("分类")]
        [StringLength(50)]
        public virtual string TypeString { get; set; }

        [DisplayName("最近购买价")]
        public virtual decimal Price { get; set; }

        [DisplayName("参考价1")]
        public virtual decimal? Price2 { get; set; }

        [DisplayName("参考价2")]
        public virtual decimal? Price3 { get; set; }

        [DisplayName("库存总金额")]
        public virtual decimal LibMoney { get; set; }

        [DisplayName("商家编码")]
        [StringLength(50)]
        public virtual string SellerNo { get; set; }

        [DisplayName("商品图片")]
        [StringLength(50)]
        public virtual string PicUrl { get; set; }


        [DisplayName("是否停用")]
        public virtual bool IsOff { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        [DisplayName("启用辅助单位")]
        public virtual bool IsAuxiliaryUnit { get; set; }

    }
}
  


