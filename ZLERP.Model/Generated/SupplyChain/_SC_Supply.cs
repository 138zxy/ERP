using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    public abstract class _SC_Supply : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(SupplierName);
            sb.Append(SupplierType);
            sb.Append(Adrress);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [DisplayName("供应商名称")]
        [StringLength(50)]
        [Required]
        public virtual string SupplierName { get; set; }

        [DisplayName("供应商分类")]
        [StringLength(50)]
        [Required]
        public virtual string SupplierType { get; set; }
        [DisplayName("地址")]
        [StringLength(50)]
        public virtual string Adrress { get; set; }

        [DisplayName("联系人")]
        [StringLength(50)]
        public virtual string Linker { get; set; }

        [DisplayName("联系电话")]
        [StringLength(50)]
        public virtual string LinkPhone { get; set; }

        [DisplayName("期初应付额")]
        public virtual decimal PaidIn { get; set; }
        [DisplayName("期初已付额")]
        public virtual decimal PaidOut { get; set; }
        [DisplayName("期初欠款额")]
        public virtual decimal PaidOwing { get; set; }
        [DisplayName("期初优惠额")]
        public virtual decimal PaidFavourable { get; set; }
        [DisplayName("应付额")]
        public virtual decimal PayMoney { get; set; }
        [DisplayName("已停用")]
        public virtual bool IsOff { get; set; }
        [DisplayName("账号")]
        [StringLength(50)]
        public virtual string AccountNo { get; set; }
        [DisplayName("税号")]
        [StringLength(50)]
        public virtual string TaxNo { get; set; }
        [DisplayName("预付款")]
        public virtual decimal PrePay { get; set; }
        [DisplayName("付款方式")]
        [StringLength(50)]
        public virtual string FinanceType { get; set; }

        [DisplayName("联系电话2")]
        [StringLength(50)]
        public virtual string LinkPhone2 { get; set; }
        [DisplayName("采购折扣")]
        public virtual int? Discount { get; set; }
        [DisplayName("期初预付额")]
        public virtual decimal PrepayInit { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [DisplayName("最近采购日")]
        public virtual DateTime? NearPurDate { get; set; }
    }
}
  


