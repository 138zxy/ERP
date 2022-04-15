using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Beton
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _B_FareTempletDel1 : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(FareTempletID);
            sb.Append(FareType); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
 
       // 车辆类型，，结算方式，有效时间，（按趟数和方量数计算的单价）明细2，起，止，结算单价
        [DisplayName("模板名称")]
        [Required]
        public virtual int FareTempletID { get; set; }

        [DisplayName("车辆类型")]
        [Required]
        public virtual string FareType { get; set; }

        [DisplayName("结算方式")]
        [Required]
        public virtual string BalaneType { get; set; }

        [DisplayName("生效时间")]
        [Required]
        public virtual DateTime EffectiveTime { get; set; }

        [DisplayName("单价")]
       
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 运输补满方值
        /// </summary>
        [DisplayName("运输补方临界值")]
        public virtual decimal CarBetonNum { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
