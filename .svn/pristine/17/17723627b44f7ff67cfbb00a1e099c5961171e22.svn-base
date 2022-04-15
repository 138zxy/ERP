using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.CarManage
{


    public abstract class _CarMaintainDel : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ItemID);
            sb.Append(MaintainID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("维修单")]
        [StringLength(50)]
        public virtual string MaintainID { get; set; }

        [DisplayName("维修项目")]
        [StringLength(50)]
        public virtual string ItemID { get; set; }



        [DisplayName("材料数量")]
        public virtual int MaterialNum { get; set; }



        [DisplayName("材料单价")]
        public virtual decimal MaterialPrice { get; set; }

        [DisplayName("材料费")]
        public virtual decimal MaterialCost { get; set; }

        [DisplayName("工时(H)")]
        public virtual decimal ManHour { get; set; }

        [DisplayName("工时费")]
        public virtual decimal ManHourCost { get; set; }

        [DisplayName("合计")]
        public virtual decimal AllCost { get; set; }

        [StringLength(500)]
        [DisplayName("摘要")]
        public virtual string Remark { get; set; }
    }
}
  


