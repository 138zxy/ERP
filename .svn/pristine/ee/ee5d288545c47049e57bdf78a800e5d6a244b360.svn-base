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
    public abstract class _HR_GZ_Tax : EntityBase<string>
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
   
        [DisplayName("级数")]
        public virtual int OrderNum { get; set; }

        [DisplayName("起始金额")]
        [Required]
        public virtual int Startmoney { get; set; }

        [DisplayName("结算金额")]
        [Required]
        public virtual int Endmoney { get; set; }
        [DisplayName("税率%")]
        public virtual decimal Rate { get; set; }
        [DisplayName("速扣")]
        public virtual decimal Withhold { get; set; }
        #endregion
    }
}
