using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarMaintainItem : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Code);
            sb.Append(ItemType);
            sb.Append(ItemName);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("编号")] 
        [StringLength(50)]
        public virtual string Code { get; set; }

        [DisplayName("项目大类")]
        [StringLength(50)]
        public virtual string ItemType { get; set; }

        [DisplayName("维修项目")]
        [Required]
        [StringLength(50)]
        public virtual string ItemName { get; set; }


        [DisplayName("拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }

        [DisplayName("材料单价")] 
        public virtual decimal MaterialPrice { get; set; }  

        [DisplayName("单位")]
        [StringLength(50)]
        public virtual string Unit { get; set; } 

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; } 
    }
}
  


