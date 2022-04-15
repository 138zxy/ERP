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
    public abstract class _HR_KQ_Arrange : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ArrangeName);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("班次名称")]
        [Required]
        [StringLength(50)]
        public virtual string ArrangeName { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [DisplayName("上班时间(hh:mm:ss)")]
        [StringLength(50)]
        public virtual string WordStartTime { get; set; }

        [DisplayName("下班时间(hh:mm:ss)")]
        [StringLength(50)]
        public virtual string WordEndTime { get; set; }

        [DisplayName("签到提前区间(h)")]
        public virtual int UpInterval { get; set; }
        [DisplayName("签退延后区间(h)")]
        public virtual int DownInterval { get; set; }

        [DisplayName("自动签到")]
        public virtual bool AutoUp { get; set; }
        [DisplayName("自动签退")]
        public virtual bool AutoDown { get; set; }

        [DisplayName("是否跨天")]
        public virtual bool IsInnerDay { get; set; }

        #endregion
    }
}
