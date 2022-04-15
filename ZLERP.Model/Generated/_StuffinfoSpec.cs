﻿using System;
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
    public abstract class _StuffinfoSpec : EntityBase<int?>
    {
        #region Methods
        
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();    
            
            sb.Append(this.GetType().FullName);
            sb.Append(SpecName);
            sb.Append(StuffID);
            sb.Append(StuffName);
            sb.Append(Remark);
            
            return sb.ToString().GetHashCode();
        }
        
        #endregion
        
        
        
        #region Properties
        
        /// <summary>
        /// 材料规格
        /// </summary>
        [DisplayName("材料规格")]
        [StringLength(50)]
        public virtual string SpecName 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 原材料ID
        /// </summary>
        [DisplayName("原材料ID")]
        
        [StringLength(30)] 
        public virtual string StuffID 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 材料名称
        /// </summary>
        [DisplayName("材料名称")]
        
        [StringLength(20)] 
        public virtual string StuffName 
        { 
            get;
            set; 
        }        
        /// <summary>
        /// 
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



