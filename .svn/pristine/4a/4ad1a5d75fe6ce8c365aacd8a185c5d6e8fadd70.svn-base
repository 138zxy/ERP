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
    public abstract class _B_Balance : EntityBase<string>
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


        [DisplayName("合同号")]
        public virtual string ContractID
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
        /// <summary>
        /// 泵名称
        /// </summary>
        [DisplayName("泵名称")]
        public virtual string CastMode
        {
            get;
            set;
        }
        [DisplayName("运费模板计算")]
        public virtual int? BaleType { get; set; }

        [DisplayName("模板结算")]
        public virtual bool IsStockPrice { get; set; }

        [DisplayName("手动运费结算")]
        public virtual bool IsOnePrice { get; set; }

        [DisplayName("运费方式")]
        public virtual string OnePriceType { get; set; }

        [DisplayName("手动运费单价")]
        public virtual decimal OnePrice { get; set; }

        [DisplayName("累计砼数量")]
        public virtual decimal? AllBCount { get; set; }

        [DisplayName("累计砼金额")]
        public virtual decimal? AllBMoney { get; set; }

        [DisplayName("确认砼数量")]
        public virtual decimal? AllBOkCount { get; set; }
        [DisplayName("结算砼金额")]
        public virtual decimal? AllBOkMoney { get; set; }





        [DisplayName("累计运费数量")]
        public virtual decimal? AllTCount { get; set; }

        [DisplayName("累计运费金额")]
        public virtual decimal? AllTMoney { get; set; }

        [DisplayName("确认运费数量")]
        public virtual decimal? AllTOkCount { get; set; }
        [DisplayName("结算运费金额")]
        public virtual decimal? AllTOkMoney { get; set; }


        [DisplayName("累计泵送数量")]
        public virtual decimal? AllPCount { get; set; }

        [DisplayName("累计泵送金额")]
        public virtual decimal? AllPMoney { get; set; }

        [DisplayName("确认泵送数量")]
        public virtual decimal? AllPOkCount { get; set; }
        [DisplayName("结算泵送金额")]
        public virtual decimal? AllPOkMoney { get; set; }

        [DisplayName("累计特性数量")]
        public virtual decimal? AllICount { get; set; }

        [DisplayName("累计特性金额")]
        public virtual decimal? AllIMoney { get; set; }

        [DisplayName("确认特性数量")]
        public virtual decimal? AllIOkCount { get; set; }
        [DisplayName("结算特性金额")]
        public virtual decimal? AllIOkMoney { get; set; }


        [DisplayName("累计总金额")]
        public virtual decimal? AllMoney { get; set; }

        [DisplayName("其他金额")]
        public virtual decimal? OtherMoney { get; set; }


        [DisplayName("结算总金额")]
        public virtual decimal? AllOkMoney { get; set; }


        [DisplayName("已付金额")]
        public virtual decimal? PayMoney { get; set; }



        [DisplayName("欠款金额")]
        public virtual decimal? PayOwing { get; set; }


        [DisplayName("优惠金额")]
        public virtual decimal? PayFavourable { get; set; }





        [DisplayName("已开票额")]
        public virtual decimal? PiaoPayMoney { get; set; }



        [DisplayName("未开票金额")]
        public virtual decimal? PiaoPayOwing { get; set; }


        [DisplayName("免开票金额")]
        public virtual decimal? PiaoPayFavourable { get; set; }


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

        [DisplayName("约定付款时间")]
        public virtual System.DateTime? PiaoDate
        {
            get;
            set;
        }
        #endregion
    }
}
