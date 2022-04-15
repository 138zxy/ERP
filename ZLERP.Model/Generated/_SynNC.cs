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
    public abstract class _SynNC : EntityBase<int?>
    {
        #region Methods
        
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();    
            
            sb.Append(this.GetType().FullName);
            sb.Append(Type);
            sb.Append(NO);
            sb.Append(IsPositive);
            sb.Append(Status);
            sb.Append(SynDate);
            
            return sb.ToString().GetHashCode();
        }
        
        #endregion
        
        
        
        #region Properties
        
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
        /// True为正数、False为负数
        /// </summary>
        [DisplayName("True为正数、False为负数")]
        
        
        public virtual bool? IsPositive 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 同步状态 -1上传失败、0未上传、1已上传
        /// </summary>
        [DisplayName("同步状态 -1上传失败、0未上传、1已上传")]
        
        
        public virtual int? Status 
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

        [ScriptIgnore]
        public virtual IList<SynInfoNC> SynInfoNCs
        {
            get;
            set;
        }
        
        #endregion
    }
}



