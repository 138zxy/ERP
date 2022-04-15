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
    public abstract class _B_FareTempletDel2 : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(FareTempletDelID); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
 
       // 车辆类型，，结算方式，有效时间，（按趟数和方量数计算的单价）明细2，起，止，结算单价
        [DisplayName("模板名称")]
        [Required]
        public virtual int FareTempletDelID { get; set; }

        [DisplayName("起止公里")]
        [Required]
        public virtual decimal UpKilometre { get; set; }

        [DisplayName("结束公里")]
        [Required]
        public virtual decimal DwonKilometre { get; set; }

 
        [DisplayName("单价")] 
        public virtual decimal Price { get; set; } 

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
