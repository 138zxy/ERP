using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarInsurance : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(CarID);
            sb.Append(InsuranceType);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID { get; set; }


        [DisplayName("保单类别")]
        [StringLength(50)]
        public virtual string InsuranceType { get; set; }


        [DisplayName("保险单位")]
        [Required]
        [StringLength(50)]
        public virtual string InsuranceUnit { get; set; }

        [DisplayName("联系人")]
        [StringLength(50)]
        public virtual string Linker { get; set; }

        [DisplayName("联系电话")]
        [StringLength(50)]
        public virtual string LinkPhone { get; set; }


        [DisplayName("投保人")]
        [StringLength(50)]
        public virtual string InsuranceMan { get; set; }

        [DisplayName("保单号")]
        [StringLength(50)]
        public virtual string InsuranceNo { get; set; }


        [DisplayName("是否交强险")] 
        public virtual bool IsMust { get; set; }


        [DisplayName("车船费")]
        public virtual decimal OtherCost { get; set; }

        [DisplayName("总费用")]
        public virtual decimal MoneyAll { get; set; }

        [DisplayName("起始日期")]
        public virtual DateTime StartDate { get; set; }

        [DisplayName("终止日期")]
        public virtual DateTime EndDate { get; set; }

        [DisplayName("经办人")]
        public virtual string Opreater { get; set; }


        [DisplayName("经办日期")]
        public virtual DateTime OpreateDate { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


