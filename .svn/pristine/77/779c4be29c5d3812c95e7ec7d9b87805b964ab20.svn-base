
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  采购计划抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StockPlan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(ID);
            sb.Append(this.GetType().FullName);
            sb.Append(PlanDate);
            sb.Append(PlanMan);
            sb.Append(SourceAddr);
            sb.Append(TransportMode);
            sb.Append(PlanAmount);
            sb.Append(ExecStatus);
            sb.Append(QualityNeed);
            sb.Append(Auditor);
            sb.Append(AuditTime);
            sb.Append(AuditStatus);
            sb.Append(AuditInfo);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties


        [DisplayName("任务单号")]
        public virtual string PlanNo
        {
            get;
            set;
        }
        [DisplayName("采购合同")]
        public virtual string StockPactID
        {
            get;
            set;
        }
        /// <summary>
        /// 计划日期
        /// </summary>
        [DisplayName("开始日期")]
        public virtual System.DateTime? PlanDate
        {
            get;
            set;
        }

        /// <summary>
        /// 新计划单，正在进料，暂停进料，进料完成，作废
        /// </summary>
        [DisplayName("计划状态")]
        [StringLength(50)]
        public virtual string ExecStatus
        {
            get;
            set;
        }
        [DisplayName("材料名称")]
        public virtual string StuffID
        {
            get;
            set;
        }
        /// <summary>
        /// 质量标准
        /// </summary>
        [DisplayName("质量单号")]
        public virtual string QualityNeed
        {
            get;
            set;
        }
        [DisplayName("产地")]
        public virtual string SourceAddr
        {
            get;
            set;
        }

        [DisplayName("发货单号")]
        public virtual string PurNo
        {
            get;
            set;
        }
        [DisplayName("联系人")]
        public virtual string Linker
        {
            get;
            set;
        }

        [DisplayName("联系电话")]
        public virtual string LinkPhone
        {
            get;
            set;
        }
        [DisplayName("单据数量(T)")]
        public virtual decimal OrderNum
        {
            get;
            set;
        }

        [DisplayName("运输商")]
        public virtual string TransID { get; set; }

        [DisplayName("运输车号")]
        public virtual string CarNo { get; set; }
        [DisplayName("联系人")]
        public virtual string CarLinker
        {
            get;
            set;
        }

        [DisplayName("联系电话")]
        public virtual string CarLinkPhone
        {
            get;
            set;
        }

        [DisplayName("司机")]
        public virtual string CarDriver
        {
            get;
            set;
        }

        [DisplayName("扣除率")]
        public virtual decimal OutRate
        {
            get;
            set;
        }

        [DisplayName("存放仓库")]
        public virtual string SiloID
        {
            get;
            set;
        }
        [DisplayName("每方容量")]
        public virtual decimal OneWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 计划员
        /// </summary>
        [DisplayName("下达人员")]
        public virtual string PlanMan
        {
            get;
            set;
        }

        [DisplayName("登记人员")]
        public virtual string CheckMan
        {
            get;
            set;
        }

        [DisplayName("登记日期")]
        public virtual DateTime CheckDate
        {
            get;
            set;
        }

        /// <summary>
        /// 运输方式
        /// </summary>
        [DisplayName("运输方式")]
        [StringLength(50)]
        public virtual string TransportMode
        {
            get;
            set;
        }

        /// <summary>
        /// 计划数量
        /// </summary>
        [DisplayName("计划数量(T)")]
        [Required]
        public virtual decimal PlanAmount
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
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(128)]
        public virtual string AuditInfo
        {
            get;
            set;
        }

        #endregion
    }
}
