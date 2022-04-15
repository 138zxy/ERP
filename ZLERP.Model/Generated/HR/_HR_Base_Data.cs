using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.HR
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _HR_Base_Data : EntityBase<long?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ItemsType);
            sb.Append(ItemsName);
            sb.Append(ItemsNo);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("类别")]
        [Required]
        [StringLength(50)]
        public virtual string ItemsType { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("名称")]
        public virtual string ItemsName { get; set; }
        [DisplayName("序号")]
        public virtual int? ItemsNo { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        [DisplayName("是否系统项")]
        public virtual bool IsSystem { get; set; }
        #endregion
    }
}
