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
    public abstract class _HR_GZ_Deduct : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(PersonID);
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

        [DisplayName("提成比例")]
        public virtual decimal UnitPrice { get; set; }

        [DisplayName("业务量")]
        public virtual decimal Quantity { get; set; }
        [DisplayName("提成金额")]
        public virtual decimal AllMoney { get; set; }

        [DisplayName("工作日")]
        public virtual DateTime WorkDate { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
