using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarInsuranceItem : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(CarInsuranceID);
            sb.Append(CarInsuranceName);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("保险单号")]
        [StringLength(50)]
        public virtual string CarInsuranceID { get; set; }


        [DisplayName("保险名称")]
        [StringLength(50)]
        public virtual string CarInsuranceName { get; set; }

        [DisplayName("费用")]
        public virtual decimal ItemMoney { get; set; }


    }
}
  


