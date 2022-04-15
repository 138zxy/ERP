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
    public abstract class _HR_GZ_SalariesItem : EntityBase<string>
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

        [DisplayName("工资套账")]
        public virtual int SalariesID { get; set; }

        [DisplayName("对应年月")]
        public virtual string YearMonth { get; set; }
        //------------------------------支付部分------------------
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


        [DisplayName("绩效奖金")]
        public virtual decimal? PerformancePay { get; set; }

        [DisplayName("计件工资")]
        public virtual decimal? PiecePay { get; set; }
        [DisplayName("计时工资")]
        public virtual decimal? TimingPay { get; set; }
        [DisplayName("计提工资")]
        public virtual decimal? DeductPay { get; set; }

        [DisplayName("加班奖金")]
        public virtual decimal? OverPay { get; set; }

        [DisplayName("其他补贴")]
        public virtual decimal? SubsidyPay { get; set; }

        [DisplayName("应发合计")]
        public virtual decimal? AllPay { get; set; }

        //------------------------------扣款部分------------------
        [DisplayName("社保扣款")]
        public virtual decimal? SocialPay { get; set; }
        [DisplayName("个人所得税")]
        public virtual decimal? TaxPay { get; set; }
        [DisplayName("请假扣款")]
        public virtual decimal? LeavePay { get; set; }

        [DisplayName("迟到扣款")]
        public virtual decimal? LatePay { get; set; }

        [DisplayName("旷工扣款")]
        public virtual decimal? StayAwayPay { get; set; }
      

        [DisplayName("其他扣款")]
        public virtual decimal? TakeOffPay { get; set; }

        [DisplayName("应扣合计")]
        public virtual decimal? OutPay { get; set; }

       //-----------------
        [DisplayName("实际应发")]
        public virtual decimal? ActionPay { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }


        #endregion
    }
}
