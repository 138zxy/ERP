
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
    public abstract class _TZRalationGH : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(SourceShipDocID);
            sb.Append(TargetShipDocID);
			sb.Append(Cube);
			sb.Append(CarID);
			sb.Append(Driver);
			sb.Append(ReturnType);
			sb.Append(ActionType);
            sb.Append(ActionTime);
            sb.Append(ActionCube);
			sb.Append(ReturnReason);
			sb.Append(IsAudit);
			sb.Append(IsCompleted);
			sb.Append(Auditor);
            sb.Append(AuditTime);
            sb.Append(DealMan);
            sb.Append(IsStatistics);
            sb.Append(ReduceConStrength);
            sb.Append(FreightSubsidy);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 源任务单单号
        /// </summary>
        [DisplayName("源发货单")]
        [StringLength(30)]
        public virtual string SourceShipDocID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 目标任务单号
        /// </summary>
        [DisplayName("目标发货单")]
        [StringLength(30)]
        public virtual string TargetShipDocID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 量
        /// </summary>
        [Required]
        [DisplayName("量")]
        public virtual decimal Cube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 运输车号
        /// </summary>
        [DisplayName("运输车号")]
        [StringLength(30)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 当班司机
        /// </summary>
        [DisplayName("当班司机")]
        [StringLength(30)]
        public virtual string Driver
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 剩退类别
        /// </summary>
        [DisplayName("剩退类别")]
        [StringLength(50)]
        public virtual string ReturnType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 处理方式
        /// </summary>
        [DisplayName("处理方式")]
        [StringLength(50)]
        public virtual string ActionType
        {
            get;
			set;			 
        }
        /// <summary>
        /// 剩退时间
        /// </summary>
        [DisplayName("剩退时间")]
        public virtual System.DateTime? ActionTime
        {
            get;
            set;
        }
        /// <summary>
        /// 剩退量
        /// </summary>
        [DisplayName("剩退量")]
        public virtual decimal ActionCube
        {
            get;
            set;
        }
        /// <summary>
        /// 剩退原因
        /// </summary>
        [DisplayName("剩退原因")]
        [StringLength(50)]
        public virtual string ReturnReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否审核
        /// </summary>
        [Required]
        [DisplayName("是否审核")]
        public virtual bool IsAudit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否完成
        /// </summary>
        [Required]
        [DisplayName("是否完成")]
        public virtual bool IsCompleted
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 处理人
        /// </summary>
        [DisplayName("处理人")]
        [StringLength(30)]
        public virtual string DealMan
        {
            get;
			set;			 
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        [DisplayName("处理时间")]
        public virtual DateTime? DealTime
        {
            get;
            set;
        }
        /// <summary>
        /// 转退料标志
        /// </summary>
        [DisplayName("转退料标志")]
        public virtual string TzRalationFlag
        {
            get;
            set;
        }
        /// <summary>
        /// 是否需要统计
        /// </summary>
        [DisplayName("是否需要统计")]
        public virtual bool IsStatistics
        {
            get;
            set;
        }

        /// <summary>
        /// 降标耗损
        /// </summary>
        [DisplayName("降标耗损")]
        public virtual decimal ReduceConStrength
        {
            get;
            set;
        }

        /// <summary>
        /// 运费补助
        /// </summary>
        [DisplayName("运费补助")]
        public virtual decimal FreightSubsidy
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual ShippingDocumentGH ShippingDocumentGH
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<ShippingDocumentGH> ShippingDocumentsGH
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
