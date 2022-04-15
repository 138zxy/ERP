
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _ContractPayGH : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 付款日期
        /// </summary>
        [DisplayName("付款日期")]
        public virtual DateTime PayDate
        {
            get;
			set;			 
        }

        /// <summary>
        /// 付款类型
        /// </summary>
        [DisplayName("付款类型")]
        public virtual String PayType
        {
            get;
            set;
        }

        /// <summary>
        /// 付款金额
        /// </summary>
        [DisplayName("付款金额")]
        public virtual Decimal PayMoney
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
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        public virtual string Manager
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
        /// <summary>
        /// 对方结算人
        /// </summary>
        [DisplayName("对方结算人")]
        public virtual string OtherAccountMan
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public virtual string OtherTel
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
		
		
        #endregion
    }
}
