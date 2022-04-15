using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 针对合同加入砼价格明细，控制合同生产 合同明细
    /// </summary>
	public class ContractItem : _ContractItem
    {
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName {
            get { return Contract == null ? "" : Contract.ContractName; }
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
        /// <summary>
        /// 泵送价格
        /// </summary>
        [DisplayName("泵送价格")]
        public virtual decimal? PumpPrice
        {
            get {
                return base.UnPumpPrice + base.PumpCost;
            }
            set {
                PumpPrice = value;
            }
        }


        /// <summary>
        /// 基准价
        /// </summary>
        [DisplayName("基准价")]
        public virtual decimal TranPrice
        {
            get
            {
                if (ConStrengthClass != null)
                {
                    return Convert.ToDecimal(ConStrengthClass.BrandPrice);
                }
                return 0;
            }


        }


        [ScriptIgnore]
        public virtual ConStrength ConStrengthClass
        {
            get;
            set;
        }
	}
}