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
    public abstract class _B_TranBalance : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(BaleNo);  
            sb.Append(Version); 
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("结算单号")]
        [Required]
        public virtual string BaleNo { get; set; }

        [DisplayName("单据日期")] 
        public virtual DateTime OrderDate { get; set; }
 

        [DisplayName("车队ID")]
        public virtual int TranID
        {
            get;
            set;
        }
        [DisplayName("对方结算人")] 
        public virtual string ThatBaleMan { get; set; }

        [DisplayName("工程编码")]
        public virtual string ProjectID
        {
            get;
            set;
        }
        [DisplayName("开始日期")]
        public virtual DateTime? StartDate
        {
            get;
            set;
        }
        [DisplayName("结束日期")]
        public virtual DateTime? EndDate
        {
            get;
            set;
        }

        [DisplayName("纳入月份")]
        public virtual string InMonth
        {
            get;
            set;
        }
        [DisplayName("模式")]
        public virtual string ModelType
        {
            get;
            set;
        }

        [DisplayName("结算模板")]
        public virtual int? BaleType { get; set; } 

        [DisplayName("系统结算")]
        public virtual bool IsStockPrice { get; set; }

        [DisplayName("手动结算")]
        public virtual bool IsOnePrice { get; set; }

        [DisplayName("结算方式")]
        public virtual string OnePriceType { get; set; }

        [DisplayName("手动设置价")]
        public virtual decimal?  OnePrice { get; set; }

        [DisplayName("累计数量")]
        public virtual decimal? AllCount { get; set; }
        [DisplayName("累计金额")]
        public virtual decimal? AllMoney { get; set; }

        [DisplayName("确认数量")]
        public virtual decimal? AllOkCount { get; set; }
        [DisplayName("确认金额")]
        public virtual decimal? AllOkMoney { get; set; }


        [DisplayName("其他金额")]
        public virtual decimal? OtherMoney { get; set; }

        [DisplayName("已付金额")]
        public virtual decimal? PayMoney { get; set; }
         


        [DisplayName("欠款金额")]
        public virtual decimal? PayOwing { get; set; }


        [DisplayName("优惠金额")]
        public virtual decimal? PayFavourable { get; set; }

        [DisplayName("结算日期")]
        public virtual DateTime BaleDate { get; set; }
        [DisplayName("结算人")]
        public virtual string BaleMan { get; set; }

 
        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
            set;
        }	
        #endregion
    }
}
