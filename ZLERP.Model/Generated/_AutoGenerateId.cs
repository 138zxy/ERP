using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    public abstract class _AutoGenerateId : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 表名
        /// </summary>
        [DisplayName("表名")]
        public virtual string Table
        {
            get;
            set;
        }
        /// <summary>
        /// 前缀
        /// </summary>
        [Required]
        [DisplayName("前缀")]
        public virtual string IDPrefix
        {
            get;
            set;
        }
        /// <summary>
        /// 下一值
        /// </summary>
        [DisplayName("下一值")]
        public virtual int NextValue
        {
            get;
            set;
        }
        
        #endregion
    }
}
