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
    public abstract class _StuffSale : EntityBase<string>
    {
        #region Methods
        
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();    
            
            sb.Append(this.GetType().FullName);
            sb.Append(CarID);
            sb.Append(CarNo);
            sb.Append(StuffID);
            sb.Append(StuffName);
            sb.Append(SupplyID);
            sb.Append(CompanyID);
            sb.Append(CompName);
            sb.Append(TotalWeight);
            sb.Append(CarWeight);
            sb.Append(Weight);
            sb.Append(WeightMan);
            sb.Append(WeightName);
            sb.Append(ArriveTime);
            sb.Append(DeliveryTime);
            sb.Append(Remark);
            sb.Append(Version);
            
            return sb.ToString().GetHashCode();
        }
        
        #endregion

        #region Properties
        
        /// <summary>
        ///  运输车号
        /// </summary>
        [DisplayName(" 运输车号")]
        [StringLength(30)] 
        public virtual string CarID 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 车牌号码
        /// </summary>
        [DisplayName("车牌号码")]
        [StringLength(50)] 
        public virtual string CarNo 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("销售原材料")]
        [StringLength(30)] 
        public virtual string StuffID 
        { 
            get;
            set; 
        }        

        /// <summary>
        ///  原料名称
        /// </summary>
        [DisplayName("原料名称")]
        [StringLength(60)] 
        public virtual string StuffName 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("收货单位")]
        [StringLength(30)] 
        public virtual string SupplyID 
        { 
            get;
            set; 
        }
       
        /// <summary>
        /// 公司ID
        /// </summary>
        [DisplayName("供货单位")]
        public virtual string CompanyID 
        { 
            get;
            set; 
        }        

        /// <summary>
        ///  公司名称
        /// </summary>
        [DisplayName(" 供货单位")]
        [StringLength(128)] 
        public virtual string CompName 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("毛重(kg)")]
        public virtual int? TotalWeight 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("皮重(kg)")]
        public virtual int? CarWeight 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("净重(kg)")]
        public virtual int? Weight 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 称重人
        /// </summary>
        [DisplayName("称重人")]
        [StringLength(50)] 
        public virtual string WeightMan 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("地磅名称")]
        [StringLength(50)] 
        public virtual string WeightName 
        { 
            get;
            set; 
        }        

        /// <summary>
        ///  到场时间
        /// </summary>
        [Required]
        [DisplayName(" 到场时间")]
        public virtual DateTime? ArriveTime 
        { 
            get;
            set; 
        }        

        /// <summary>
        ///  出站时间
        /// </summary>
        [Required]
        [DisplayName(" 出站时间")]
        public virtual DateTime? DeliveryTime 
        { 
            get;
            set; 
        }        

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)] 
        public virtual string Remark 
        { 
            get;
            set; 
        }        
        
        #endregion
    }
}



