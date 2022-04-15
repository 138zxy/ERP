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
    public abstract class _HR_KQ_ResultMain : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(KQName);
            sb.Append(YearMonth); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("考勤名称")]
        public virtual string KQName { get; set; }

        [DisplayName("对应年月")]
        public virtual string YearMonth { get; set; }

        [DisplayName("总考勤人数")]
        public virtual decimal? AllCount { get; set; }
         
        [DisplayName("是否封存")]
        public virtual bool IsSeal { get; set; }
 
        #endregion
    }
}
