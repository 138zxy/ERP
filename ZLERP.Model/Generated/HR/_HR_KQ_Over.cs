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
    public abstract class _HR_KQ_Over : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(PersonID);
            sb.Append(OverNo);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties



        [Required]
        [StringLength(50)]
        [DisplayName("加班单号")]
        public virtual string OverNo { get; set; }


        [DisplayName("员工")]
        public virtual int PersonID { get; set; }

        [StringLength(50)]
        [DisplayName("加班类型")]
        public virtual string OverType { get; set; }


        [DisplayName("开始日期")]
        public virtual DateTime StartTime { get; set; }
        [DisplayName("结束日期")]
        public virtual DateTime EndTime { get; set; }

        [DisplayName("加班/小时")]
        public virtual decimal DayLong { get; set; }
 

        [StringLength(500)]
        [DisplayName("加班原因")]
        public virtual string Reson { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }


        [DisplayName("状态")]
        [StringLength(50)]
        public virtual string Condition { get; set; }


        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
            set;
        }
        #endregion
    }
}
