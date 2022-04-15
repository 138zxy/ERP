using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_Fixed_Del : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(FixedID);
            sb.Append(EntryName);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
         [DisplayName("资产")]
        public virtual int FixedID { get; set; }

        [DisplayName("项目名称")]
        [Required]
        [StringLength(50)]
         public virtual string EntryName { get; set; }

        [DisplayName("项目说明")]
        [Required]
        [StringLength(50)]
        public virtual string EntryDec { get; set; }

        [DisplayName("备注")] 
        [StringLength(500)]
        public virtual string Remark { get; set; }

        [DisplayName("图片")]
        [StringLength(50)]
        public virtual string PicUrl { get; set; }
    }
}
  


