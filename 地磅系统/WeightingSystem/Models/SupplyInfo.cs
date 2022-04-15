using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WeightingSystem.Models
{
    public class SupplyInfo
    {

        /// <summary>
        /// 简称
        /// </summary>
        [DisplayName("简称")]
        public virtual string ShortName
        {
            get;
            set;
        }
        /// <summary>
        /// 厂商类型
        /// </summary>
        [DisplayName("厂商类型")]
        public virtual string SupplyKind
        {
            get;
            set;
        }
        /// <summary>
        /// 供货厂家
        /// </summary>
        [DisplayName("厂家全称")]
        public virtual string SupplyName
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("负责人")]
        public virtual string Principal
        {
            get;
            set;
        }
        /// <summary>
        /// 厂家地址
        /// </summary>
        [DisplayName("厂家地址")]
        public virtual string SupplyAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 发票地址
        /// </summary>
        [DisplayName("发票地址")]
        public virtual string InvoiceAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 开户银行
        /// </summary>
        [DisplayName("开户银行")]
        public virtual string BankName
        {
            get;
            set;
        }
        /// <summary>
        /// 开户银行帐号
        /// </summary>
        [DisplayName("开户银行帐号")]
        public virtual string BankAccount
        {
            get;
            set;
        }
        /// <summary>
        /// 营业电话
        /// </summary>
        [DisplayName("营业电话")]
        public virtual string BusinessTel
        {
            get;
            set;
        }
        /// <summary>
        /// 营业传真
        /// </summary>
        [DisplayName("营业传真")]
        public virtual string BusinessFax
        {
            get;
            set;
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        public virtual string PostCode
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人电话
        /// </summary>
        [DisplayName("负责人电话")]
        public virtual string PrincipalTel
        {
            get;
            set;
        }
        /// <summary>
        /// 联络人
        /// </summary>
        [DisplayName("联络人")]
        public virtual string LinkMan
        {
            get;
            set;
        }
        /// <summary>
        /// 联络人手机
        /// </summary>
        [DisplayName("联络人手机")]
        public virtual string LinkTel
        {
            get;
            set;
        }
        /// <summary>
        /// 供货类型
        /// </summary>
        [DisplayName("供货类型")]
        public virtual string SupplyType
        {
            get;
            set;
        }
        /// <summary>
        /// 信誉等级
        /// </summary>
        [DisplayName("信誉等级")]
        public virtual string CreditWorthiness
        {
            get;
            set;
        }
        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email")]
        public virtual string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
            set;
        }
        /// <summary>
        /// 含税否
        /// </summary>
        [DisplayName("含税否")]
        public virtual bool IsTax
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 供货厂家编号
        /// </summary>
        [DisplayName("供货厂家编号")]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 是否内转
        /// </summary>
        [DisplayName("是否内转")]
        public virtual bool IsNz
        {
            get;
            set;
        }
    }
}
