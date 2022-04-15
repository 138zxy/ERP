using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarMaintain : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [DisplayName("流水号")]
        [Required]
        [StringLength(50)]
        public virtual string MaintainNo { get; set; } 


        [DisplayName("车号")]
        [StringLength(50)]
        public virtual string CarID { get; set; }


        [DisplayName("维修方式")]
        [StringLength(50)]
        public virtual string RepairWay { get; set; }


        [DisplayName("送修日期")]
        public virtual DateTime RepairDate { get; set; }


        [DisplayName("送修里程")]
        public virtual int  RepairMilo { get; set; }

        [DisplayName("送修人员")]
        [StringLength(50)]
        public virtual string GiveMan { get; set; }


        [DisplayName("维修单位")]
        [StringLength(50)]
        public virtual string RepairUnit { get; set; }

        [DisplayName("故障描述")]
        [StringLength(500)]
        public virtual string FaultDesc { get; set; }

        [DisplayName("预计取车日期")] 
        public virtual DateTime  ForecastDate { get; set; }

        [DisplayName("取车人")]
        [StringLength(50)]
        public virtual string GetCarMan { get; set; }

        [DisplayName("取车日期")]
        public virtual DateTime? GetCarDate { get; set; }


        [DisplayName("是否完修")]
        public virtual bool IsOver { get; set; }

        [DisplayName("完修时间")]
        public virtual DateTime? OverDate { get; set; }

        [DisplayName("维修人员")]
        [StringLength(50)]
        public virtual string RepairMan { get; set; }

        [DisplayName("维修地点")]
        [StringLength(50)]
        public virtual string RepairAdress { get; set; }  

        [DisplayName("总维修费用(元)")]
        public virtual decimal? RepairPirce { get; set; }


        [DisplayName("总维修时长(h)")]
        public virtual decimal? RepairTime { get; set; }

        [DisplayName("修理描述")]
        [StringLength(500)]
        public virtual string RepairDesc { get; set; }

         
    }
}
  


