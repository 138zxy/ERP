using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 合同付款
    /// </summary>
    public class ContractPayGH : _ContractPayGH
    {
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName {
            get { return ContractGH == null ? "" : ContractGH.ContractName; }
        }
        /// <summary>
        /// 合同编码
        /// </summary>
        [DisplayName("合同编码")]
        [Required]
        public virtual string ContractID
        {
            get;
            set;
        }
	}
}