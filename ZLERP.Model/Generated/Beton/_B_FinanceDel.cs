using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Beton
{


    public abstract class _B_FinanceDel : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(UnitID);
            sb.Append(BaleID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [DisplayName("关联主键")]
        public virtual int FinanceID { get; set; }

        [DisplayName("结算单号")]
        [Required]
        public virtual int BaleID { get; set; }

        [DisplayName("结算单号")]

        public virtual string BaleNo { get; set; }


        [DisplayName("收付款方")]
        [Required]
        public virtual string UnitID { get; set; }

        [DisplayName("收付款方名称")]
        public virtual string UnitName { get; set; }

        [DisplayName("收付款对象")]
        [Required]
        public virtual string NextID { get; set; }

        [DisplayName("收付款方对象名称")]
        public virtual string NextName { get; set; }


        [DisplayName("纳入月份")]
        [StringLength(50)]
        public virtual string InMonth { get; set; }


        [DisplayName("日期")]
        public virtual DateTime OrderDate { get; set; }

        [DisplayName("结算总金额")]
        public virtual decimal? AllOkMoney { get; set; }



        [DisplayName("本次金额")]
        public virtual decimal PayMoney { get; set; }

        [DisplayName("本次优惠额")]
        public virtual decimal PayFavourable { get; set; }
                             

    }
}
  


