using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.Material
{
    public abstract class _M_TranFinanceRecord : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(FinanceNo);
            sb.Append(ID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("资产分类")]
        [Required]
        [StringLength(50)]
        public virtual string FinanceType { get; set; }

        [DisplayName("收入或支出")]
        [StringLength(50)]
        public virtual string IsInOrOut { get; set; }

        [DisplayName("日期")]
        public virtual DateTime FinanceDate { get; set; }

        [DisplayName("金额")]
        public virtual decimal FinanceMoney { get; set; }

        [DisplayName("余额")]
        public virtual decimal Balance { get; set; }

        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Operater{ get; set; }

        [DisplayName("经营性分类")]
        [StringLength(50)]
        public virtual string UseType { get; set; }

        [DisplayName("付款记录")]
        public virtual int FinanceNo { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }
    }
}
  


