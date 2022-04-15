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
    public abstract class _HR_KQ_Result : EntityBase<string>
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
        [DisplayName("对应年月")]
        public virtual string YearMonth { get; set; }
        [DisplayName("员工")]
        public virtual int PersonID { get; set; }
        [Required]
        [DisplayName("工作日")]
        public virtual DateTime WorkDate { get; set; }

        [DisplayName("星期")]
        public virtual string WeekShow { get; set; }
        [DisplayName("迟到分钟")]
        public virtual int LateMinute { get; set; }


        [DisplayName("早退分钟")]
        public virtual int LeaveMinute { get; set; }

        [DisplayName("请假小时")]
        public virtual decimal OnleaveHour { get; set; }

        [DisplayName("加班小时")]
        public virtual decimal OverHour { get; set; }

        [DisplayName("出差小时")]
        public virtual decimal TravelHour { get; set; }

        [DisplayName("调休调班小时")]
        public virtual decimal ChangeHour { get; set; }

        [DisplayName("是否旷工")]
        public virtual bool IsAway { get; set; }

        [DisplayName("旷工小时")]
        public virtual decimal AwayHour { get; set; }

        [DisplayName("是否异常")]
        public virtual bool IsUnusual { get; set; }
        [DisplayName("是否休息日加班")]
        public virtual bool IsAllOver { get; set; }

        [DisplayName("上班时间")]
        public virtual DateTime? WorkStartTime { get; set; }

        [DisplayName("下班时间")]
        public virtual DateTime? WorkEndTime { get; set; }

        [DisplayName("上班打卡时间")]
        public virtual DateTime? UpStartTime { get; set; }

        [DisplayName("下班打卡时间")]
        public virtual DateTime? DwonEndTime { get; set; }

        [DisplayName("开始签卡时间")]
        public virtual DateTime CheckUpTime { get; set; }

        [DisplayName("截止签退时间")]
        public virtual DateTime CheckDownTime { get; set; }

        [StringLength(200)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        #endregion
    }
}
