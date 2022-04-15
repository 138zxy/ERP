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
    public abstract class _HR_Base_SocietyRelation : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PersonID);
            sb.Append(Relation);
            sb.Append(Name);
            sb.Append(WorkCondition);
            sb.Append(Telphone);
            sb.Append(Birthday);
            sb.Append(WorkUnit);
            sb.Append(Position);
            sb.Append(Age);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 员工编码
        /// </summary>
        [DisplayName("员工编码")]
        public virtual int? PersonID
        {
            get;
            set;
        }
        /// <summary>
        /// 关系
        /// </summary>
        [DisplayName("关系")]
        [StringLength(20)]
        public virtual string Relation
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [StringLength(20)]
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 工作状况
        /// </summary>
        [DisplayName("工作状况")]
        [StringLength(20)]
        public virtual string WorkCondition
        {
            get;
            set;
        }
        /// <summary>
        /// 联系方式
        /// </summary>
        [DisplayName("联系方式")]
        [StringLength(20)]
        public virtual string Telphone
        {
            get;
            set;
        }
        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public virtual System.DateTime? Birthday
        {
            get;
            set;
        }
        /// <summary>
        /// 工作单位
        /// </summary>
        [DisplayName("工作单位")]
        [StringLength(30)]
        public virtual string WorkUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        [StringLength(20)]
        public virtual string Position
        {
            get;
            set;
        }
        /// <summary>
        /// 年龄
        /// </summary>
        [DisplayName("年龄")]
        public virtual int? Age
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Meno
        {
            get;
            set;
        }
        #endregion
    }
}
