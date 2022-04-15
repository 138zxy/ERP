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
    public abstract class _HR_GZ_TakeOff : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(PersonID);
            sb.Append(ItemName);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("员工")]
        [Required]
        public virtual int PersonID { get; set; }

        [DisplayName("扣除项目")] 
        public virtual string ItemName { get; set; }

        [DisplayName("扣除金额")]
        public virtual decimal ItemMoney { get; set; }

        [DisplayName("纳入月份")]
        public virtual string Monthly { get; set; }

        [DisplayName("产生日期")]
        public virtual DateTime ItemDate { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
