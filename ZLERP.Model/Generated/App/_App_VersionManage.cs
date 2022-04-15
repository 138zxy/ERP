using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.App
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _App_VersionManage : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(AppName);
            sb.Append(AppUrl);
            sb.Append(AppVersion);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// App名称
        /// </summary>
        [StringLength(150)]
        [DisplayName("App名称")]
        public virtual string AppName
        {
            get;
            set;
        }
        /// <summary>
        /// AppUrl
        /// </summary>
        [DisplayName("AppUrl")]
        public virtual string AppUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 版本
        /// </summary>
        [StringLength(50)]
        [DisplayName("版本")]
        public virtual string AppVersion
        {
            get;
            set;
        }
        #endregion
    }
}
