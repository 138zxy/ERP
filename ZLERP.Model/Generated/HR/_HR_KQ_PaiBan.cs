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
    public abstract class _HR_KQ_PaiBan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ArrangeID);
            sb.Append(PersonID);
            sb.Append(WorkDate);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("员工")]
        public virtual int PersonID { get; set; }

        [DisplayName("工作日")]
        public virtual DateTime WorkDate { get; set; }

        [DisplayName("星期")]
        public virtual string WeekShow { get; set; }

        [DisplayName("班次")] 
        public virtual int  ArrangeID { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [DisplayName("上班时间")] 
        public virtual DateTime WordStartTime { get; set; }

        [DisplayName("下班时间")] 
        public virtual DateTime WordEndTime { get; set; }

        [DisplayName("有效开始签到时间")]
        public virtual DateTime UpStartTime { get; set; } 

        [DisplayName("截止结束签退时间")]
        public virtual DateTime DwonEndTime { get; set; }
 

        [DisplayName("自动签到")]
        public virtual bool AutoUp { get; set; }
        [DisplayName("自动签退")]
        public virtual bool AutoDown { get; set; }

        [DisplayName("是否跨天")]
        public virtual bool IsInnerDay { get; set; }

        #endregion
    }
}
