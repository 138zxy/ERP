using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarMaterial : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(CarID);
            sb.Append(MaterialName);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID { get; set; }


        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Oprater { get; set; }


        [DisplayName("产品名称")]
        [Required]
        [StringLength(50)]
        public virtual string MaterialName { get; set; }

        [DisplayName("购置日期")]
        public virtual DateTime BuyDate { get; set; }

        [DisplayName("规格型号")]
        [StringLength(50)]
        public virtual string Spec { get; set; }

        [DisplayName("单位")]
        [StringLength(50)]
        public virtual string Unit { get; set; }

        [DisplayName("数量")]
        public virtual int Quantity { get; set; }


        [DisplayName("单价")]
        public virtual decimal Price { get; set; }

        [DisplayName("金额")]
        public virtual decimal MoneyAll { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


