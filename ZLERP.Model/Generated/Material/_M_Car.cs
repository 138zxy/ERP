using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Material
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _M_Car : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(CarNo); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("运输公司")]
        [Required]
        public virtual string TransID { get; set; }

        [DisplayName("车号")]
        [Required]
        public virtual string CarNo { get; set; }

        [DisplayName("司机")] 
        public virtual string Driver { get; set; }

        [DisplayName("最大重量(kg)")]
        public virtual decimal MaxWeight { get; set; }


        [DisplayName("空车重量(kg)")]
        public virtual decimal CarWeight { get; set; }


        [DisplayName("材料类别")]
        public virtual string StuffType { get; set; }


        [DisplayName("是否停用")]
        public virtual bool Condition { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }
        #endregion
    }
}
