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
    public abstract class _HR_KQ_Holiday : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(HolidayName);
            sb.Append(StartDate);
            sb.Append(EndDate); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("节假日名称")]
        [Required]
        [StringLength(50)]
        public virtual string HolidayName { get; set; }
        [Required]
        [DisplayName("开始时间")]
        public virtual DateTime StartDate { get; set; }
        [Required]
        [DisplayName("结束时间")]
        public virtual DateTime EndDate { get; set; }
        [StringLength(50)]
        [DisplayName("假日类型")]
        public virtual string HolidayType { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        #endregion
    }
}
