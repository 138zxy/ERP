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
    public abstract class _HR_Base_HRecordItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Name);
            sb.Append(Unit);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [DisplayName("名称")]
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        public virtual string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public virtual string Meno
        {
            get;
            set;
        }
        #endregion
    }
}
