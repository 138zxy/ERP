using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarAccident : EntityBase<string>
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

        [DisplayName("事故日期")]
        public virtual DateTime AccidentDate { get; set; }


        [DisplayName("现场交警")]
        [StringLength(50)]
        public virtual string TrafficPolice { get; set; }

        [DisplayName("损失金额")]
        public virtual decimal LossCost { get; set; }

        [DisplayName("理赔金额")]
        public virtual decimal SettleCost { get; set; }


        [DisplayName("损坏程度")]
        [StringLength(50)]
        public virtual string SettleDegree { get; set; }


        [DisplayName("保险单位")] 
        [StringLength(50)]
        public virtual string InsuranceUnit { get; set; }

        [DisplayName("保单号")]
        [StringLength(50)]
        public virtual string InsuranceNo { get; set; }


        [DisplayName("对方车牌号")]
        [StringLength(50)]
        public virtual string SideCarNo { get; set; }

        [DisplayName("对方姓名")]
        [StringLength(50)]
        public virtual string SideName { get; set; }

        [DisplayName("对方电话")]
        [StringLength(50)]
        public virtual string SidePhone { get; set; }

        [DisplayName("对方车型")]
        [StringLength(50)]
        public virtual string SideCarType { get; set; }

        [DisplayName("对方单位")]
        [StringLength(50)]
        public virtual string SideUnit { get; set; }


        [DisplayName("对方损坏程度")]
        [StringLength(50)]
        public virtual string SideDegree { get; set; }

        [StringLength(500)]
        [DisplayName("事故描述")]
        public virtual string AccidentDec { get; set; }

        [StringLength(200)]
        [DisplayName("人员损伤")]
        public virtual string PeopleDec { get; set; }

        [StringLength(100)]
        [DisplayName("事故地点")]
        public virtual string AccidentAdress { get; set; } 

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; } 
    }
}
  


