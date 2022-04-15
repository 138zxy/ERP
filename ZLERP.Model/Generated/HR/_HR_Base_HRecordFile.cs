using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.HR
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _HR_Base_HRecordFile : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PersonID);
            sb.Append(FileItemID);
            sb.Append(FileItemName);
            sb.Append(Count);
            sb.Append(Unit);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 人员编码
        /// </summary>
        [DisplayName("人员编码")]
        public virtual int? PersonID
        {
            get;
            set;
        }
        /// <summary>
        /// 文件编码
        /// </summary>
        [DisplayName("文件编码")]
        public virtual int? FileItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        [DisplayName("文件名称")]
        public virtual string FileItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 文件数量
        /// </summary>
        [DisplayName("文件数量")]
        public virtual int? Count
        {
            get;
            set;
        }
        /// <summary>
        /// 文件单位
        /// </summary>
        [DisplayName("文件单位")]
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
