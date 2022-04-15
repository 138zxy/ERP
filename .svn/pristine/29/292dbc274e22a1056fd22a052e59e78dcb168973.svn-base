using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarPeccancy : EntityBase<string>
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

        [DisplayName("驾驶员")]
        [StringLength(50)]
        public virtual string Driver { get; set; }

        [DisplayName("罚款金额")]
        public virtual decimal PeccancyCost { get; set; }

        [DisplayName("违章日期")]
        public virtual DateTime PeccancyDate { get; set; }

        [DisplayName("扣分")]
        public virtual int PeccancyNum { get; set; }

        [DisplayName("违章项目")]
        [StringLength(50)]
        public virtual string PeccancyItem { get; set; }

        [DisplayName("违章地址")]
        [StringLength(200)]
        public virtual string PeccancyAdress { get; set; }


        [DisplayName("执行单位")]
        [StringLength(50)]
        public virtual string PeccancyUnit { get; set; }


        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


