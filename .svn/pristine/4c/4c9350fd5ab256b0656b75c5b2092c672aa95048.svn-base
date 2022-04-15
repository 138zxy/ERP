using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarKeep : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID); 
            sb.Append(CarID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID { get; set; } 

        [DisplayName("保养里程")]
        public virtual decimal KeepMileage { get; set; }


        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Opreater { get; set; }


        [DisplayName("保养日期")]
        public virtual DateTime KeepDate { get; set; }


        [DisplayName("保养金额")]
        public virtual decimal KeepCount { get; set; }

        [DisplayName("保养单位")]
        [StringLength(50)]
        public virtual string KeepUnit { get; set; }

        [DisplayName("保养项目")]
        [StringLength(50)]
        public virtual string KeepItem { get; set; } 

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; } 
    }
}
  


