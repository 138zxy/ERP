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
    public abstract class _HR_PM_Contract : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PersonID);
            sb.Append(PersonName);
            sb.Append(PersonPym);
            sb.Append(DepartmentID);
            sb.Append(DepartmentName);
            sb.Append(ContractNo);
            sb.Append(ContractName);
            sb.Append(ContractType);
            sb.Append(IsTerm);
            sb.Append(ContractTermYear);
            sb.Append(ContractTermMonth);
            sb.Append(SignDate);
            sb.Append(ProbationMonth);
            sb.Append(ProbationDay);
            sb.Append(ProbationStart);
            sb.Append(ProbationEnd);
            sb.Append(ProbationWage);
            sb.Append(ValidDate);
            sb.Append(MatureDate);
            sb.Append(CorrectionWage);
            sb.Append(State);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 人员编码
        /// </summary>
        [DisplayName("人员")]
        [Required]
        public virtual int PersonID
        {
            get;
            set;
        }
        /// <summary>
        /// 人员名称
        /// </summary>
        [DisplayName("人员名称")]
        public virtual string PersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 人员拼音码
        /// </summary>
        [DisplayName("人员拼音码")]
        public virtual string PersonPym
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
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        public virtual string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName
        {
            get;
            set;
        }
        /// <summary>
        /// 合同类型
        /// </summary>
        [DisplayName("合同类型")]
        [Required]
        public virtual string ContractType
        {
            get;
            set;
        }
        /// <summary>
        /// 有无期限
        /// </summary>
        [DisplayName("有无期限")]
        public virtual bool IsTerm
        {
            get;
            set;
        }
        /// <summary>
        /// 合同期限(年)
        /// </summary>
        [DisplayName("合同期限(年)")]
        public virtual int? ContractTermYear
        {
            get;
            set;
        }
        /// <summary>
        /// 合同期限(月)
        /// </summary>
        [DisplayName("合同期限(月)")]
        public virtual int? ContractTermMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 签订日期
        /// </summary>
        [DisplayName("签订日期")]
        public virtual System.DateTime? SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 试用期(月)
        /// </summary>
        [DisplayName("试用期(月)")]
        public virtual int? ProbationMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 试用期(天)
        /// </summary>
        [DisplayName("试用期(天)")]
        public virtual int? ProbationDay
        {
            get;
            set;
        }
        /// <summary>
        /// 试用开始日期
        /// </summary>
        [DisplayName("试用开始日期")]
        public virtual System.DateTime? ProbationStart
        {
            get;
            set;
        }
        /// <summary>
        /// 试用结束日期
        /// </summary>
        [DisplayName("试用结束日期")]
        public virtual System.DateTime? ProbationEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 试用工资
        /// </summary>
        [DisplayName("试用工资")]
        public virtual decimal? ProbationWage
        {
            get;
            set;
        }
        /// <summary>
        /// 生效日期
        /// </summary>
        [DisplayName("生效日期")]
        public virtual System.DateTime? ValidDate
        {
            get;
            set;
        }
        /// <summary>
        /// 到期日期
        /// </summary>
        [DisplayName("到期日期")]
        public virtual System.DateTime? MatureDate
        {
            get;
            set;
        }
        /// <summary>
        /// 转正工资
        /// </summary>
        [DisplayName("转正工资")]
        public virtual decimal? CorrectionWage
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public virtual int? State
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
        #endregion
    }
}
