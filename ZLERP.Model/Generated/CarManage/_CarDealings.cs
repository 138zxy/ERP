using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarDealings : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Code);
            sb.Append(Name);
            sb.Append(IsOff);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("编号")] 
        [StringLength(50)]
        public virtual string Code { get; set; }


        [DisplayName("单位名称")]
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }


        [DisplayName("拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }

        [DisplayName("类别")]
        [StringLength(50)]
        public virtual string DealingsType { get; set; } 
         
        [DisplayName("是否停业")]
        public virtual bool IsOff { get; set; }


        [DisplayName("联系人")]
        [StringLength(50)]
        public virtual string Linker { get; set; }

        [DisplayName("手机")]
        [StringLength(50)]
        public virtual string LinkPhone { get; set; }


        [DisplayName("电话")]
        [StringLength(50)]
        public virtual string LinkPhone2 { get; set; }


        [DisplayName("邮箱")]
        [StringLength(50)]
        public virtual string Mail { get; set; }


        [DisplayName("地址")]
        [StringLength(200)]
        public virtual string Adress { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; } 
    }
}
  


