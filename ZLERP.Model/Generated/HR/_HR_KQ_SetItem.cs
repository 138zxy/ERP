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
    public abstract class _HR_KQ_SetItem : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
  
        [DisplayName("区间起(分钟)")]  
        public virtual int StartMinute { get; set; }

        [DisplayName("区间止(分钟)")]
        public virtual int EndMinute { get; set; }

        [DisplayName("处罚金额")]
        public virtual decimal PunishMoney { get; set; }
        #endregion
    }
}
