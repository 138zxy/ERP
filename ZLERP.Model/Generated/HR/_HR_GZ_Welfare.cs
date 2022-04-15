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
    public abstract class _HR_GZ_Welfare : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(SetID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("员工")]
        [Required]
        public virtual int PersonID { get; set; }

        [DisplayName("项目名称")]
        [Required]
        public virtual int SetID { get; set; }

        [DisplayName("实际费用")]
        public virtual decimal Cost { get; set; }

        [DisplayName("开始日期")]
        public virtual DateTime StartDate { get; set; }
        [DisplayName("结束日期")]
        public virtual DateTime EndDate { get; set; }

        [DisplayName("终止福利")]
        public virtual bool IsStop { get; set; }

        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        #endregion
    }
}
