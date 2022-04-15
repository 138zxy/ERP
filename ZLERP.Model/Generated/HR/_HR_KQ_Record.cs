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
    public abstract class _HR_KQ_Record : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(PersonID);
            sb.Append(CheckTime);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("员工")] 
        public virtual int  PersonID { get; set; }

        [DisplayName("自动签卡")]
        public virtual bool AutoCheck { get; set; }

        [Required]
        [DisplayName("签卡时间")]
        public virtual DateTime CheckTime { get; set; }

        [StringLength(50)]
        [DisplayName("计算机名")]
        public virtual string ComputeName { get; set; }


        [StringLength(50)]
        [DisplayName("地址")]
        public virtual string IP { get; set; }

        [StringLength(50)]
        [DisplayName("MAC地址")]
        public virtual string MacAdress { get; set; }

         [StringLength(50)]
        [DisplayName("数据来源")]
        public virtual string DataSource { get; set; } 
        #endregion
    }
}
