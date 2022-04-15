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
    public abstract class _SaleDailyRecord : EntityBase<int?>
    {
        #region Methods
        
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();    
            
            sb.Append(this.GetType().FullName);
            sb.Append(CompanyName);
            sb.Append(SaleDate);
            sb.Append(ProjectName);
            sb.Append(Constrengh);
            sb.Append(MonthNum);
            sb.Append(DayNum);
            sb.Append(Price);
            sb.Append(ConstreMoney);
            sb.Append(OtherMoney);
            sb.Append(DayMoney);
            sb.Append(MonthMoney);
            sb.Append(DayBackMoney);
            sb.Append(MonthBackMoney);
            sb.Append(OweMoney);
            sb.Append(Remark);
            sb.Append(Version);
            
            return sb.ToString().GetHashCode();
        }
        
        #endregion
        
        
        
        #region Properties
        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("单位名称")]
        
        [StringLength(50)] 
        public virtual string CompanyName 
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
        /// 
        /// </summary>
        [DisplayName("工程名称")]
        
        [StringLength(50)] 
        public virtual string ProjectName 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("强度等级")]
        
        [StringLength(50)] 
        public virtual string Constrengh 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("本月累计方量（m3）")]
        public virtual decimal MonthNum 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("本日方量（m3)")]
        public virtual decimal DayNum 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("单价(元/m3)")]
        public virtual decimal Price 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("砼款销售额")]
        public virtual decimal ConstreMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("收其它销售额")]
        public virtual decimal OtherMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("本日发生额")]
        public virtual decimal DayMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("本月累计（元）")]
        public virtual decimal MonthMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("本日回款（元）")]
        public virtual decimal DayBackMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("本月累计回款（元）")]
        public virtual decimal MonthBackMoney 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("累计欠款（元）")]
        public virtual decimal OweMoney 
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



