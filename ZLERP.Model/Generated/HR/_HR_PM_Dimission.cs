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
    public abstract class _HR_PM_Dimission : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PersonID);
            sb.Append(PersonName);
            sb.Append(DepartmentID);
            sb.Append(DepartmentName);
            sb.Append(DimissionType);
            sb.Append(DimissionReason);
            sb.Append(DimissionDate);
            sb.Append(WageAmount);
            sb.Append(Amount);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// 单据号
        /// </summary>
        [DisplayName("单据号")]
        [Required]
        public virtual string DimissionNo
        {
            get;
            set;
        }
        /// <summary>
        /// 员工编码
        /// </summary>
        [DisplayName("员工编码")]
        [Required]
        public virtual int? PersonID
        {
            get;
            set;
        }
        /// <summary>
        /// 员工名称
        /// </summary>
        [DisplayName("员工名称")]
        public virtual string PersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 部门编码
        /// </summary>
        [DisplayName("部门编码")]
        public virtual int? DepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public virtual string DepartmentName
        {
            get;
            set;
        }
        /// <summary>
        /// 离职类型
        /// </summary>
        [DisplayName("离职类型")]
        public virtual string DimissionType
        {
            get;
            set;
        }
        /// <summary>
        /// 离职原因
        /// </summary>
        [DisplayName("离职原因")]
        public virtual string DimissionReason
        {
            get;
            set;
        }
        /// <summary>
        /// 离职时间
        /// </summary>
        [DisplayName("离职时间")]
        [Required]
        public virtual System.DateTime? DimissionDate
        {
            get;
            set;
        }
        /// <summary>
        /// 离职工资结算
        /// </summary>
        [DisplayName("离职工资结算")]
        [Range(0, double.MaxValue, ErrorMessage = "必须是大于等于0的数字")]
        public virtual decimal? WageAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 离职费用
        /// </summary>
        [DisplayName("离职费用")]
        [Range(0, double.MaxValue, ErrorMessage = "必须是大于等于0的数字")]
        public virtual decimal? Amount
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
        /// <summary>
        /// 删除标识
        /// </summary>
        [DisplayName("删除标识")]
        public virtual bool Delflag
        {
            get;
            set;
        }

        [DisplayName("状态")]
        public virtual string State { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor { get; set; }

        #endregion
    }
}
