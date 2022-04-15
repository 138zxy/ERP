using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Beton
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _B_FareTemplet : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(FareTempletName); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
 

        [DisplayName("模板名称")]
        [Required]
        public virtual string FareTempletName { get; set; } 

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
