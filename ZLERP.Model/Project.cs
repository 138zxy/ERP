using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model.Beton;
namespace ZLERP.Model
{
    /// <summary>
    ///  工程信息
    /// </summary>
    public class Project : _Project
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        [Required]
        [DisplayName("合同编号")]
        public virtual string ContractID { get; set; }
        
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName
        {
            get { return Contract == null ? "" : Contract.ContractName; }
        }
       
        /// <summary>
        /// 模板名称
        /// </summary>
        [DisplayName("模板名称")]
        public virtual string FareTempletName
        {
            get { return B_FareTemplet == null ? "" : B_FareTemplet.FareTempletName; }
        }

        public virtual string CustName
        {
            get { return Contract == null ? "" : Contract.CustName; }
        }
    }
}