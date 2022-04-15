using System;
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
    /// 客户垫资
    /// </summary>
    public class ContractDZGH : _ContractDZGH
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        public virtual string CustomerName
        {
            get { return Customer == null ? "" : Customer.CustName; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
        [Required]
        public virtual string CustomerID
        {
            get;
            set;
        }
        
        [ScriptIgnore]
        public virtual Customer Customer
        {
            get;
            set;
        }
    }
}
