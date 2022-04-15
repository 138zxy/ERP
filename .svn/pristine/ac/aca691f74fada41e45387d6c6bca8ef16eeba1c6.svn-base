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
    public abstract class _SynInfoNC : EntityBase<int?>
    {
        #region Methods
        
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();    
            
            sb.Append(this.GetType().FullName);
            sb.Append(SynNCID);
            sb.Append(NO);
            sb.Append(Type);
            sb.Append(ParamsInfo);
            sb.Append(ReturnInfo);
            sb.Append(SynDate);
            sb.Append(NcResultCode);
            sb.Append(NcResultDescription);
            sb.Append(NcContent);
            
            return sb.ToString().GetHashCode();
        }
        
        #endregion
        
        
        
        #region Properties
        
        /// <summary>
        /// synNC的自编号ID
        /// </summary>
        [DisplayName("synNC的自编号ID")]
        
        
        public virtual int? SynNCID 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 单据编号
        /// </summary>
        [DisplayName("单据编号")]
        
        [StringLength(50)] 
        public virtual string NO 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 字段类型，0采购订单、1产成品入库单、2其它入库单、3销售出库单、4材料出库单、5其它出库单
        /// </summary>
        [DisplayName("字段类型，0采购订单、1产成品入库单、2其它入库单、3销售出库单、4材料出库单、5其它出库单")]
        
        
        public virtual int? Type 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 参数报文信息
        /// </summary>
        [DisplayName("参数报文信息")]
        
        [StringLength(-1)] 
        public virtual string ParamsInfo 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// NC接口返回的报文信息
        /// </summary>
        [DisplayName("NC接口返回的报文信息")]
        
        [StringLength(-1)] 
        public virtual string ReturnInfo 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 同步时间，上传成功的时间
        /// </summary>
        [DisplayName("同步时间，上传成功的时间")]
        
        
        public virtual DateTime? SynDate 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        
        [StringLength(50)] 
        public virtual string NcResultCode 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        
        [StringLength(-1)] 
        public virtual string NcResultDescription 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        
        [StringLength(200)] 
        public virtual string NcContent 
        { 
            get;
            set; 
        }        
        
        
        
        #endregion
    }
}



