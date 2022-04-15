using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.App
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _App_VersionUpdate : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(AppManageID);
            sb.Append(UpdateContent);
            sb.Append(UpdateUrl);
            sb.Append(UpdateVersion);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// App编码
        /// </summary>
        [DisplayName("App编码")]
        public virtual string AppManageID
        {
            get;
            set;
        }
        /// <summary>
        /// 升级内容
        /// </summary>
        [DisplayName("升级提示内容")]
        [StringLength(150)]
        public virtual string UpdateContent
        {
            get;
            set;
        }
        /// <summary>
        /// 升级包URL
        /// </summary>
        [DisplayName("升级包URL")]
        [StringLength(150)]
        public virtual string UpdateUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        [StringLength(50)]
        [DisplayName("更新版本号")]
        public virtual string UpdateVersion
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(350)]
        [DisplayName("备注")]
        public virtual string Meno
        {
            get;
            set;
        }
        #endregion
    }
}
