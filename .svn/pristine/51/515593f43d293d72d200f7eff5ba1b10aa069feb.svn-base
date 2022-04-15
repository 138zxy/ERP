using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_Fixed_Deprecia : EntityBase<string>
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

        [DisplayName("资产编号")]
        [StringLength(50)]
        public virtual string Fcode { get; set; }

        [DisplayName("资产名称")]
        [StringLength(50)]
        public virtual string Fname { get; set; }


        [DisplayName("资产条形码")]
        [StringLength(50)]
        public virtual string BarCode { get; set; }

        [DisplayName("类型")]
        [StringLength(50)]
        public virtual string Ftype { get; set; }

        [DisplayName("资产拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }


        [DisplayName("价格")]
        public virtual decimal? FixedPirce { get; set; }


        [DisplayName("增加日期")]
        public virtual DateTime? AddDate { get; set; }


        [DisplayName("使用部门")]
        [StringLength(50)]
        public virtual string DepartMent { get; set; }



        [DisplayName("净残值率(%)")]
        public virtual decimal? NetSalvageRate { get; set; }


        [DisplayName("净残值")]
        public virtual decimal? NetSalvage { get; set; }

        [DisplayName("折旧方法")]
        [StringLength(50)]
        public virtual string Depreciation { get; set; }


        [DisplayName("清理日期")]
        public virtual DateTime? ClearDate { get; set; }


        [DisplayName("本月折旧额")]
        public virtual decimal? DepreciaMonth { get; set; }


        [DisplayName("本年折旧额")]
        public virtual decimal? DepreciaYear { get; set; }

        [DisplayName("累计折旧额")]
        public virtual decimal? DepreciaAll { get; set; }
        [DisplayName("净值")]
        public virtual decimal? NetWorth { get; set; }

        [DisplayName("状态")]
        [StringLength(50)]
        public virtual string Condition { get; set; }

        [DisplayName("可用年限")]
        public virtual int? UseYear { get; set; }



        [DisplayName("可用截止日期")]
        public virtual DateTime? UseEndDate { get; set; }


        [DisplayName("分析截止月份")] 
        public virtual DateTime AnalysisMonth { get; set; }

        [DisplayName("实际截止月份")] 
        public virtual DateTime ActualMonth { get; set; }


        [DisplayName("本年有效折旧月数")]
        public virtual int? DepreciaUseMonth { get; set; }


        [DisplayName("累计有效折旧月数")]
        public virtual int? DepreciaAllMonth { get; set; }
        [DisplayName("累计有效折旧年数")]
        public virtual int DepreciaAllYear { get; set; }

        [DisplayName("规格型号")]
        [StringLength(50)]
        public virtual string Spec { get; set; }
        [DisplayName("月折旧率(%)")]
        public virtual decimal? DepreciaMonthRate { get; set; }

        [DisplayName("月标准折旧")]
        public virtual decimal? DepreciaMonthMoney { get; set; }


        [DisplayName("清理转售金额")]
        public virtual decimal? ClearMoney { get; set; }
    }
}
  


