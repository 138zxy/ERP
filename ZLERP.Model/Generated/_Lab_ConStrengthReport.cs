
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 混凝土抗压强度检测报告抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_ConStrengthReport : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Unit);
            sb.Append(CuringCondition);
            sb.Append(DetectionAge);
            sb.Append(SpecimenSize);
            sb.Append(SizeConversionCoefficient);
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
        public virtual string Lab_ConWPRecordId
        {
            get;
            set;
        }
        /// <summary>
        /// 检测依据
        /// </summary>
        [DisplayName("检测依据")]
        public virtual int? DependInfoID
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(50)]
        [DisplayName("单位")]
        public virtual string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 养护条件
        /// </summary>
        [DisplayName("养护条件")]
        [StringLength(50)]
        public virtual string CuringCondition
        {
            get;
            set;
        }
        /// <summary>
        /// 检测龄期(d)
        /// </summary>
        [DisplayName("检测龄期(d)")]
        public virtual int? DetectionAge
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
        /// 尺寸换算系数
        /// </summary>
        [DisplayName("尺寸换算系数")]
        public virtual decimal? SizeConversionCoefficient
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

        //[ScriptIgnore]
        //public virtual Lab_DependInfo Lab_DependInfo
        //{
        //    get;
        //    set;
        //}
        [ScriptIgnore]
        public virtual Lab_ConWPRecord Lab_ConWPRecord
        {
            get;
            set;
        }

        #endregion
    }
}
