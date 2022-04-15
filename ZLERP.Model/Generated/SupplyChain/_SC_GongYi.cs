using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
 
    public abstract class _SC_GongYi : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(IsOff); 
            sb.Append(Version); 
            return sb.ToString().GetHashCode();
        }

        #endregion 
        [Required]
        [StringLength(50)]
        [DisplayName("加工名称")]
        public virtual string GYName { get; set; }

        [DisplayName("是否停用")]
        public virtual bool  IsOff { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

    }
}
  


