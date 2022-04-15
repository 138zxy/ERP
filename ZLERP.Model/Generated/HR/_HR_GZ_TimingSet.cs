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
    public abstract class _HR_GZ_TimingSet : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Code);
            sb.Append(Name); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
 
        [DisplayName("项目编码")]
        [Required]
        [StringLength(50)]
        public virtual string Code { get; set; }

        [DisplayName("项目名称")]
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [DisplayName("计时单价")]
        [Required]
        public virtual decimal UnitPrice { get; set; }
        #endregion
    }
}
