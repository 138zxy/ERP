
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 混凝土抗压强度检验记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_ConStrengthRecord : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(SpecimenSize);
            sb.Append(K7d_TestDate);
            sb.Append(K7d_LoadValue);
            sb.Append(K7d_SingleBlockValue);
            sb.Append(K7d_RepValue);
            sb.Append(K7d_Inspector);
            sb.Append(K28d_TestDate);
            sb.Append(K28d_LoadValue);
            sb.Append(K28d_SingleBlockValue);
            sb.Append(K28d_RepValue);
            sb.Append(K28d_Inspector);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties
        /// <summary>
        /// 试件编号
        /// </summary>
        [DisplayName("试件编号")]
        [StringLength(50)]
        public virtual string Lab_ConWPRecordId
        {
            get;
            set;
        }
        /// <summary>
        /// 试件尺寸
        /// </summary>
        [DisplayName("试件尺寸")]
        [StringLength(50)]
        public virtual string SpecimenSize
        {
            get;
            set;
        }
        /// <summary>
        /// 7天检测日期
        /// </summary>
        [DisplayName("检测日期")]
        public virtual System.DateTime? K7d_TestDate
        {
            get;
            set;
        }
        /// <summary>
        /// 荷载（KN)
        /// </summary>
        [DisplayName("荷载(KN)")]
        public virtual decimal? K7d_LoadValue
        {
            get;
            set;
        }
        /// <summary>
        /// 单块值（MPa）
        /// </summary>
        [DisplayName("单块值(MPa)")]
        public virtual decimal? K7d_SingleBlockValue
        {
            get;
            set;
        }
        /// <summary>
        /// 代表值（MPa）
        /// </summary>
        [DisplayName("代表值(MPa)")]
        public virtual decimal? K7d_RepValue
        {
            get;
            set;
        }
        /// <summary>
        /// 检测员
        /// </summary>
        [DisplayName("检测员")]
        [StringLength(50)]
        public virtual string K7d_Inspector
        {
            get;
            set;
        }
        /// <summary>
        /// 28天检测日期
        /// </summary>
        [DisplayName("检测日期")]
        public virtual System.DateTime? K28d_TestDate
        {
            get;
            set;
        }
        /// <summary>
        /// 荷载（KN)
        /// </summary>
        [DisplayName("荷载(KN)")]
        public virtual decimal? K28d_LoadValue
        {
            get;
            set;
        }
        /// <summary>
        /// 单块值（MPa）
        /// </summary>
        [DisplayName("单块值(MPa)")]
        public virtual decimal? K28d_SingleBlockValue
        {
            get;
            set;
        }
        /// <summary>
        /// 代表值（MPa）
        /// </summary>
        [DisplayName("代表值(MPa)")]
        public virtual decimal? K28d_RepValue
        {
            get;
            set;
        }
        /// <summary>
        /// 检测员
        /// </summary>
        [DisplayName("检测员")]
        [StringLength(50)]
        public virtual string K28d_Inspector
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(350)]
        public virtual string Remark
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Lab_ConWPRecord Lab_ConWPRecord
        {
            get;
            set;
        }

        #endregion
    }
}
