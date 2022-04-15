using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_Fixed_Maintain : EntityBase<string>
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
        [DisplayName("资产号")]
        public virtual int FixedID { get; set; }

        [DisplayName("流水号")]
        [Required]
        [StringLength(50)]
        public virtual string MaintainNo { get; set; }

        [DisplayName("资产编号")]
        [StringLength(50)]
        public virtual string Fcode { get; set; }

        [DisplayName("资产名称")]
        [StringLength(50)]
        public virtual string Fname { get; set; }

        [DisplayName("维修类型")]
        [StringLength(50)]
        public virtual string RepairType { get; set; }

        [DisplayName("维修方式")]
        [StringLength(50)]
        public virtual string RepairWay { get; set; }

        [DisplayName("送修时间")]
        public virtual DateTime? RepairDate { get; set; }

        [DisplayName("送修人员")]
        [StringLength(50)]
        public virtual string GiveMan { get; set; }


        [DisplayName("维修地点")]
        [StringLength(500)]
        public virtual string RepairAdress { get; set; }

        [DisplayName("故障描述")]
        [StringLength(500)]
        public virtual string FaultDesc { get; set; }

        [DisplayName("是否完修")]
        public virtual bool IsOver { get; set; }

        [DisplayName("完修时间")]
        public virtual DateTime? OverDate { get; set; }

        [DisplayName("维修人员")]
        [StringLength(50)]
        public virtual string RepairMan { get; set; }

        [DisplayName("维修费用(元)")]
        public virtual decimal? RepairPirce { get; set; }


        [DisplayName("维修时长(h)")]
        public virtual decimal? RepairTime { get; set; }

        [DisplayName("修理描述")]
        [StringLength(500)]
        public virtual string RepairDesc { get; set; }


        [DisplayName("资产所属部门")]
        [StringLength(50)]
        public virtual string DepartMent { get; set; }


        [DisplayName("资产条形码")]
        [StringLength(50)]
        public virtual string BarCode { get; set; }

        [DisplayName("资产拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }
    }
}
  


