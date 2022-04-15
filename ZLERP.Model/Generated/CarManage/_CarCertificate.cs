using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarCertificate : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(CarID);
            sb.Append(Name);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID { get; set; }


        [DisplayName("证照名称")]
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }


        [DisplayName("证照类型")]
        [StringLength(50)]
        public virtual string CertificateType { get; set; }

        [DisplayName("下次检验日期")]
        public virtual DateTime NextDate { get; set; }

        [DisplayName("证照费用")]
        public virtual decimal CertificateCost { get; set; }


        [DisplayName("生效日期")]
        public virtual DateTime EffectDate { get; set; }

        [DisplayName("证照号码")]
        [StringLength(50)]
        public virtual string CertificateNo { get; set; }


        [DisplayName("签发单位")]
        [StringLength(50)]
        public virtual string SignUnit { get; set; }


        [DisplayName("经办日期")]
        public virtual DateTime OprateDate { get; set; }

        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Oprater { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


