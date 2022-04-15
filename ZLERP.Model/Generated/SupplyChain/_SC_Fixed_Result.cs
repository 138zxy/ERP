using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_Fixed_Result : EntityBase<string>
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

        [DisplayName("批号")]
        [StringLength(50)]
        public virtual string CheckNo { get; set; }

        [DisplayName("资产编号")]
        [StringLength(50)]
        public virtual string Fcode { get; set; }

        [DisplayName("资产名称")]
        [StringLength(50)]
        public virtual string Fname { get; set; }
        [DisplayName("规格型号")]
        [StringLength(50)]
        public virtual string Spec { get; set; }

        [DisplayName("配置")]
        [StringLength(500)]
        public virtual string Configure { get; set; }

        [DisplayName("存放位置")]
        [StringLength(50)]
        public virtual string Position { get; set; }

        [DisplayName("保管员")]
        [StringLength(50)]
        public virtual string Storeman { get; set; }

        [DisplayName("盘点结果")]
        [StringLength(50)]
        public virtual string CheckResult { get; set; }
        [DisplayName("类型")]
        [StringLength(50)]
        public virtual string Ftype { get; set; }

        [DisplayName("盘点数量")]
        public virtual int Quantity { get; set; }

        [DisplayName("电脑记录数量")]
        [StringLength(50)]
        public virtual int AutoQuantity { get; set; }

        [DisplayName("使用部门")]
        [StringLength(50)]
        public virtual string DepartMent { get; set; }
        [DisplayName("开始盘点时间")]
        public virtual DateTime CheckStartDate { get; set; }
        [DisplayName("完成盘点时间")]
        public virtual DateTime CheckEndDate { get; set; }

        [DisplayName("资产条形码")]
        [StringLength(50)]
        public virtual string BarCode { get; set; }

        [DisplayName("资产拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }

        [DisplayName("增加日期")]
        public virtual DateTime? AddDate { get; set; }

        [DisplayName("价格")]
        public virtual decimal FixedPirce { get; set; }
    }
}
  


