using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarFees : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Name); 
            sb.Append(CarID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID { get; set; }



        [DisplayName("费用名称")]
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }


        [DisplayName("缴费日期")]
        public virtual DateTime FeeDate { get; set; }


        [DisplayName("缴费金额")]
        public virtual decimal FeeCount { get; set; }

        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Opreater { get; set; }

        [DisplayName("缴费方式")]
        [StringLength(50)]
        public virtual string FeeType { get; set; }

        [DisplayName("收费单位")]
        [StringLength(50)]
        public virtual string SignUnit { get; set; }

        [DisplayName("下次缴费日期")]
        public virtual DateTime NextFeeDate { get; set; }


        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; } 
    }
}
  


