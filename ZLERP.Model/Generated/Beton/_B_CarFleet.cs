using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Beton
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _B_CarFleet : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(FleetCode);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("运输单位编码")]
        [Required]
        public virtual string FleetCode { get; set; }

        [DisplayName("运输单位名称")]
        [Required]
        public virtual string FleetName { get; set; }

        [DisplayName("运输单位类别")]
        [Required]
        public virtual string FleetType { get; set; }

        [DisplayName("地址")]
        [StringLength(50)]
        public virtual string Adrress { get; set; }

        [DisplayName("联系人")]
        [StringLength(50)]
        public virtual string Linker { get; set; }

        [DisplayName("联系电话")]
        [StringLength(50)]
        public virtual string LinkPhone { get; set; }

        [DisplayName("账号")]
        [StringLength(50)]
        public virtual string AccountNo { get; set; }
        [DisplayName("税号")]
        [StringLength(50)]
        public virtual string TaxNo { get; set; }

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
        [DisplayName("期初预付额")]
        public virtual decimal PrepayInit { get; set; }

        [DisplayName("预付款")]
        public virtual decimal PrePay { get; set; }

        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }

        [DisplayName("默认运费模板")]
        public virtual int? CarTemplet { get; set; }

        [DisplayName("是否停运")]
        public virtual bool IsStop { get; set; }
        #endregion
    }
}
