using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    /// <summary>
    /// 销售发票
    /// </summary>
    public class ContractInvoiceGH : EntityBase<string>
    {

        #region Methods

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().FullName)
              .Append(this.ContractInvoiceNo)
              .Append(this.ContractID)
              .Append(this.InvoiceMoney)
              .Append(this.InvoiceDate)
              .Append(this.Manager);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        /// <summary>
        /// 发票系统编号
        /// </summary>
        [DisplayName("发票系统编号")]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        /// <summary>
        /// 发票编号
        /// </summary>
        [DisplayName("发票编号")]
        public virtual string ContractInvoiceNo { get; set; }

        /// <summary>
        /// 销售合同系统编号
        /// </summary>
        [DisplayName("合同编号")]
        public virtual string ContractID { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        [DisplayName("发票金额")]
        public virtual decimal InvoiceMoney { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        [DisplayName("税额")]
        public virtual decimal TaxMoney { get; set; }

        /// <summary>
        /// 发票日期
        /// </summary>
        [DisplayName("发票日期")]
        public virtual DateTime? InvoiceDate { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        public virtual string Manager { get; set; }


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
        public virtual DateTime? AuditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int AuditStatus
        {
            get;
            set;
        }



        //导航属性

        /// <summary>
        /// 原材料销售合同
        /// </summary>
        [ScriptIgnore]
        public virtual ContractGH ContractGH { get; set; }


        #endregion

    }
}
