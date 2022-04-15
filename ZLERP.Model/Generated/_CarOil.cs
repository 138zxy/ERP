
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车辆油耗抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarOil : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(AddDate);
			sb.Append(Amount);
			sb.Append(UnitPrice);
			sb.Append(TotalPrice);
			sb.Append(ThisKM);
			sb.Append(LastKM);
			sb.Append(KiloMeter);
			sb.Append(Memo);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 加油日期
        /// </summary>
        [Required]
        [DisplayName("加油时间")]
        public virtual System.DateTime AddDate
        {
            get;
			set;			 
        }

        /// <summary>
        /// 处理状态
        /// </summary>
        [Required]
        [DisplayName("是否确认")]
        public virtual int Status
        {
            get;
            set;
        }

        /// <summary>
        /// 加油量
        /// </summary>
        [Required]
        [DisplayName("加油量(L)")]
        public virtual decimal Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        [DisplayName("单价(元/L)")]
        public virtual decimal UnitPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总价
        /// </summary>
        [Required]
        [DisplayName("总价")]
        public virtual decimal TotalPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 本次里程表数
        /// </summary>
        [Required]
        [DisplayName("本次加油里程")]
        public virtual decimal ThisKM
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 上次量程表数
        /// </summary>
        [Required]
        [DisplayName("上次加油里程")]
        public virtual decimal LastKM
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 车辆行程
        /// </summary>
        [Required]
        [DisplayName("车辆行程")]
        public virtual decimal KiloMeter
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Memo
        {
            get;
			set;			 
        }	
        
		public virtual Car Car
        {
            get;
			set;
        }
		 
		
      
		public virtual Silo Silo
        {
            get;
			set;
        }
		 
		
         
		public virtual StuffInfo StuffInfo
        {
            get;
			set;
        } 
		
        #endregion
    }
}
