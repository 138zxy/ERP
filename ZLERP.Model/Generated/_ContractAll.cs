
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  运输单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ContractAll : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ContractNo);
            sb.Append(ContractName);
            sb.Append(BuildUnit);
            sb.Append(ProjectAddr);
            sb.Append(Remark);
            sb.Append(ContractStatus);

            sb.Append(Auditor);
            sb.Append(AuditTime);
            sb.Append(DataType);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties


        [DisplayName("数据类型")]
        public virtual int? DataType { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
			set;			 
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(128)]
        public virtual string ContractNo
        {
            get;
            set;
        }	
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        [StringLength(128)]
        public virtual string ContractName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        [StringLength(30)]
        public virtual string CustName
        {
            get;
			set;			 
        }
        /// <summary>
        /// 建设单位
        /// </summary>
        [DisplayName("建设单位")]
        [StringLength(30)]
        public virtual string BuildUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 进行中、审核中、已完成、锁定 合同状态
        /// </summary>
        [DisplayName("合同状态")]
        [StringLength(50)]
        public virtual string ContractStatus
        {
            get;
            set;
        }	
        /// <summary>
        /// 项目地址
        /// </summary>
        [DisplayName("项目地址")]
        [StringLength(30)]
        public virtual string ProjectAddr
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
        public virtual DateTime? AuditTime
        {
            get;
            set;
        }


        
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
            set;
        }

        [DisplayName("预付款")]
        public virtual decimal PrePay { get; set; }

        [DisplayName("期初应收额")]
        public virtual decimal PaidIn { get; set; }
        [DisplayName("期初已收额")]
        public virtual decimal PaidOut { get; set; }
        [DisplayName("期初欠款额")]
        public virtual decimal PaidOwing { get; set; }
        [DisplayName("期初优惠额")]
        public virtual decimal PaidFavourable { get; set; }
        [DisplayName("应收额")]
        public virtual decimal PayMoney { get; set; }

        #endregion



    }
}
