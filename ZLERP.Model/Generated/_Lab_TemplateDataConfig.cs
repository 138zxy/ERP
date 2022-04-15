using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_TemplateDataConfig : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(LabTemplateID);
            sb.Append(Field);
            sb.Append(FieldName);
            sb.Append(ExcelRow);
            sb.Append(ExcelColumun);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 模板编码
        /// </summary>
        [DisplayName("模板编码")]
        public virtual string LabTemplateID
        {
            get;
            set;
        }
        /// <summary>
        /// 取值字段
        /// </summary>
        [DisplayName("取值字段")]
        public virtual string Field
        {
            get;
            set;
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("字段名称")]
        public virtual string FieldName
        {
            get;
            set;
        }
        /// <summary>
        /// 行位置
        /// </summary>
        [DisplayName("行位置")]
        public virtual int? ExcelRow
        {
            get;
            set;
        }
        /// <summary>
        /// 列位置
        /// </summary>
        [DisplayName("列位置")]
        public virtual int? ExcelColumun
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注")]
        public virtual string Meno
        {
            get;
            set;
        }
        #endregion
    }
}
