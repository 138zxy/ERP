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
    public abstract class _Lab_ConWPRecordItemsGH : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Lab_ConWPRecordGHId);
            sb.Append(ReportType);
            sb.Append(DocType);
            sb.Append(ReportUrl);
            sb.Append(Remark);
            sb.Append(Version);
            sb.Append(AuditStatus);
            sb.Append(Auditor);
            sb.Append(AuditTime);

            return sb.ToString().GetHashCode();
        }

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("报告编号")]
        [Required]

        public virtual string Lab_ConWPRecordGHId
        {
            get;
            set;
        }
        /// <summary>
        /// 报告类型
        /// </summary>
        [DisplayName("报告类型")]


        public virtual string ReportType
        {
            get;
            set;
        }
        /// <summary>
        /// 文档类型
        /// </summary>
        [DisplayName("文档类型")]


        public virtual string DocType
        {
            get;
            set;
        }
        /// <summary>
        /// 报告URL
        /// </summary>
        [DisplayName("报告URL")]


        public virtual string ReportUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]


        public virtual string Remark
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
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        public virtual string Auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual DateTime? AuditTime
        {
            get;
            set;
        }


        #endregion
    }
}



