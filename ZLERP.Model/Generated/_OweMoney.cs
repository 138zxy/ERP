using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{

    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _OweMoney : EntityBase<int?>
    {
        #region Methods
        
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();    
            
            sb.Append(this.GetType().FullName);
            sb.Append(SettlementType);
            sb.Append(SaleDate);
            sb.Append(UserID);
            sb.Append(UserName);
            sb.Append(ProjectName);
            sb.Append(OweMoney);
            sb.Append(TotalMoney);
            sb.Append(Remark);
            sb.Append(Version);
            
            return sb.ToString().GetHashCode();
        }
        
        #endregion
        
        
        
        #region Properties
        
        /// <summary>
        /// 结算方式 （1：月结；2：现金）
        /// </summary>
        [DisplayName("结算方式")]
        public virtual string SettlementType 
        { 
            get;
            set; 
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        [DisplayName("录入时间")]
        public virtual System.DateTime? SaleDate
        {
            get;
            set;
        }     
        /// <summary>
        /// 业务员ID
        /// </summary>
        [DisplayName("业务员ID")]
        public virtual string UserID 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        public virtual string UserName 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 项目名称
        /// </summary>
        [DisplayName("项目名称")]
        public virtual string ProjectName 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 欠款
        /// </summary>
        [Required]
        [DisplayName("欠款")]
        public virtual decimal? OweMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("合计")]
        public virtual decimal? TotalMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 审核
        /// </summary>
        [DisplayName("备注")]
        public virtual string Remark 
        { 
            get;
            set; 
        }        
        
        
        
        #endregion
    }
}



