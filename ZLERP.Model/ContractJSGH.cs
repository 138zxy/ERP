﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    /// <summary>
    /// 合同结算
    /// </summary>
    public class ContractJSGH : _ContractJSGH
    {
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName
        {
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


        [ScriptIgnore]
        public virtual ContractGH ContractGH
        {
            get;
            set;
        }
    }
}