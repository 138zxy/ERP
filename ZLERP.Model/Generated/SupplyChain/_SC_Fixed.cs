using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_Fixed : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Fcode);
            sb.Append(Fname);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("编号")]
        [Required]
        [StringLength(50)]
        public virtual string Fcode { get; set; }

        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public virtual string Fname { get; set; }

        [DisplayName("类型")]
        [StringLength(50)]
        public virtual string Ftype { get; set; }

        [DisplayName("规格型号")]
        [StringLength(50)]
        public virtual string Spec { get; set; }

        [DisplayName("制造厂商")]
        [StringLength(50)]
        public virtual string Manufacturer { get; set; }

        [DisplayName("供应商")]
        [StringLength(50)]
        public virtual string Supply { get; set; }

        [DisplayName("国标编码")]
        [StringLength(50)]
        public virtual string InterCode { get; set; }

        [DisplayName("配置")]
        [StringLength(300)]
        public virtual string Configure { get; set; }

        [DisplayName("出厂日期")]
        public virtual DateTime? ProductDate { get; set; }


        [DisplayName("增加方式")]
        [StringLength(50)]
        public virtual string AddType { get; set; }


        [DisplayName("增加日期")]
        public virtual DateTime AddDate { get; set; }

        [DisplayName("价格(元)")]
        public virtual decimal FixedPirce { get; set; }


        [DisplayName("存放位置")]
        [StringLength(50)]
        public virtual string Position { get; set; }

        [DisplayName("使用部门")]
        [StringLength(50)]
        public virtual string DepartMent { get; set; }

        [DisplayName("保管员")]
        [StringLength(50)]
        public virtual string Storeman { get; set; }


        [DisplayName("净残值率(%)")]
        public virtual decimal? NetSalvageRate { get; set; }


        [DisplayName("净残值")]
        public virtual decimal? NetSalvage { get; set; }

        [DisplayName("折旧方法")]
        [StringLength(50)]
        public virtual string Depreciation { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }

        [DisplayName("状态")]
        [StringLength(50)]
        public virtual string Condition { get; set; }

        [DisplayName("本月折旧额")]
        public virtual decimal? DepreciaMonth { get; set; }

        [DisplayName("本年折旧额")]
        public virtual decimal? DepreciaYear { get; set; }


        [DisplayName("累计折旧额")]
        public virtual decimal? DepreciaAll { get; set; }

        [DisplayName("净值")]
        public virtual decimal? NetWorth { get; set; }

        [DisplayName("最后借用部门")]
        [StringLength(50)]
        public virtual string BorrowDepart { get; set; }

        [DisplayName("最后借用人")]
        [StringLength(50)]
        public virtual string borrowMan { get; set; }


        [DisplayName("清理日期")]
        public virtual DateTime? ClearDate { get; set; }

        [DisplayName("条形码")]
        [StringLength(50)]
        public virtual string BarCode { get; set; }

        [DisplayName("二维码")]
        [StringLength(50)]
        public virtual string QRCode { get; set; }


        [DisplayName("拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }

        [DisplayName("可用年限")]
        public virtual int? UseYear { get; set; }

        [DisplayName("可用截止日期")]
        public virtual DateTime? UseEndDate { get; set; }

        [DisplayName("图片")]
        [StringLength(50)]
        public virtual string PicUrl { get; set; }

        [DisplayName("保管日期")]
        public virtual DateTime? StoreDate { get; set; }

        [DisplayName("月折旧率%")]
        public virtual decimal? DepreciaMonthRate { get; set; }


        [DisplayName("月标准折旧")]
        public virtual decimal? DepreciaMonthMoney { get; set; }
    }
}
  


