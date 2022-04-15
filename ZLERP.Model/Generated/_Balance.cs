
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  运输单退转料关系表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Balance : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(Balance);
            sb.Append(Remark);
            sb.Append(userID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        [Required]
        public virtual decimal Balance
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(200)]
        [Required]
        public virtual string Remark
        {
            get;
			set;			 
        }

        /// <summary>
        /// 人员ID
        /// </summary>
        [DisplayName("人员ID")]
        [StringLength(50)]
        public virtual string userID
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        [DisplayName("客户ID")]
        [StringLength(50)]
        public virtual string CustomerID
        {
            get;
            set;
        }
     
        /// <summary>
        /// 付款方式
        /// </summary>
        [DisplayName("付款方式")]
        [StringLength(10)]
        public virtual string PayType
        {
            get;
            set;
        }

        /// <summary>
        /// 付款日期
        /// </summary>
        [DisplayName("付款日期")]
        public virtual DateTime? PayDate
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public virtual User User
        {
            get;
            set;
        }

        /// <summary>
        /// 客户
        /// </summary>
        public virtual Customer Customer
        {
            get;
            set;
        }
		
        #endregion
    }
}
