using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model
{
    /// <summary>
    ///  运输单
    /// </summary>
    public class ShippingDocumentGH : _ShippingDocumentGH
    {
        /// <summary>
        /// 客户筒仓号
        /// </summary>
        [DisplayName("客户筒仓号")]
        [StringLength(50)]
        public virtual string CustSiloNo
        {
            get;
            set;
        }

        /// <summary>
        /// 过磅时间
        /// </summary>
        [DisplayName("过磅时间")]
        public virtual DateTime? GB_Date
        {
            get;
            set;
        }

        [DisplayName("施工部位")]
        public override string ConsPos
        {
            get
            {
                return base.ConsPos;
            }
            set
            {
                base.ConsPos = value;
            }
        }

        //comment by:Sky
        //此处代码由邓海波注释，如无特别原因（某功能不能使用）
        //请不要恢复该代码，该处严重影响发货单页面效率，多出很多不必要的关联查询
        //date: 2012-12-10
        ///// <summary>
        ///// 生产任务
        ///// </summary>
        public virtual ProduceTaskGH ProduceTaskGH
        {
            get;
            set;
        }

        [DisplayName("小票票号")]
        public virtual string TicketNO
        {
            get;
            set;
        }

        [DisplayName("异常信息")]
        public virtual string ExceptionInfo
        {
            get;
            set;
        }

        [DisplayName("理论配比名称")]
        public virtual string FormulaName
        {
            get;
            set;
        }
        /// <summary>
        /// 合同号
        /// </summary>
        [DisplayName("合同号")]
        public virtual string ContractNo
        {
            get;
            set;
        }

        [DisplayName("交货地址")]
        public virtual string DeliveryAddress
        {
            get
            {
                return this.ProduceTaskGH != null ? this.ProduceTaskGH.DeliveryAddress : "";
            }
        }

        [DisplayName("结算备注")]
        public virtual string RemarkJS
        {
            get;
            set;
        }
        [DisplayName("运输超时费用")]
        public virtual decimal y_OuttimeMoney
        {
            get;
            set;
        }
        [DisplayName("运输补量")]
        public virtual decimal y_ExtraMoney
        {
            get;
            set;
        }
        [DisplayName("运输其他费用")]
        public virtual decimal y_OtherMoney
        {
            get;
            set;
        }
        [DisplayName("运费备注")]
        public virtual string y_Remark
        {
            get;
            set;
        }
	}
}