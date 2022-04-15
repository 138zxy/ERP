using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    /*--------------
     供应链基础信息表,包括单位，付款方式，供应类别，品牌等基础信息
     --------------------------*/
    public abstract class _SC_Base : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ItemsType);
            sb.Append(ItemsName);
            sb.Append(ItemsNo); 
            sb.Append(Version); 
            return sb.ToString().GetHashCode();
        }

        #endregion
        [DisplayName("类别")]
        [Required]
        [StringLength(50)]
        public virtual string ItemsType { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("名称")]
        public virtual string ItemsName { get; set; }
        [DisplayName("序号")]
        public virtual int?  ItemsNo { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

    }
}
  


