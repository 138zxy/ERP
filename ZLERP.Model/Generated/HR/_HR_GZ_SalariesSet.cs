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
    public abstract class _HR_GZ_SalariesSet : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(PersonID); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("员工")]
        [Required]
        public virtual int PersonID { get; set; }

        [DisplayName("基本工资")]
        public virtual decimal BasePay { get; set; }

        [DisplayName("岗位津贴")]
        public virtual decimal? AllowancePay { get; set; }

        [DisplayName("级别工资")]
        public virtual decimal? LevelPay { get; set; }

        [DisplayName("工龄工资")]
        public virtual decimal? SeniorityPay { get; set; }


        [DisplayName("学历津贴")]
        public virtual decimal? EducationalPay { get; set; }


        [DisplayName("计税工资")]
        public virtual decimal? TaxPay { get; set; }
        [DisplayName("是否包括计件")]
        public virtual bool IsPiece { get; set; }

        [DisplayName("是否包括计时")]
        public virtual bool IsTiming { get; set; }

        [DisplayName("是否包括提成")]
        public virtual bool IsDeduct { get; set; }
        [DisplayName("是否按实际收入计税")]
        public virtual bool IsTaxAction { get; set; }
        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
