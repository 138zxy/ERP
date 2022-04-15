using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_Template : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(LabTemplateName);
            sb.Append(LabTemplateType);
            sb.Append(LabTemplatePath);
            sb.Append(DocType);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 模板名称
        /// </summary>
        [DisplayName("模板名称")]
        [StringLength(150)]
        public virtual string LabTemplateName
        {
            get;
            set;
        }
        /// <summary>
        /// 模板类型
        /// </summary>
        [DisplayName("模板类型")]
        [StringLength(50)]
        public virtual string LabTemplateType
        {
            get;
            set;
        }
        /// <summary>
        /// 模板路径
        /// </summary>
        [DisplayName("模板路径")]
        [StringLength(350)]
        public virtual string LabTemplatePath
        {
            get;
            set;
        }
        /// <summary>
        /// 文档类型
        /// </summary>
        [DisplayName("文档类型")]
        [StringLength(50)]
        public virtual string DocType
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Meno
        {
            get;
            set;
        }
                
        #endregion

        [ScriptIgnore]
        public virtual Dic Dic
        {
            get;
            set;
        }
    }
}
