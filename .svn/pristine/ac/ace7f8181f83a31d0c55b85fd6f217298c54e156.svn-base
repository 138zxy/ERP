using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model.Enums;

namespace ZLERP.Model.Generated
{
    public abstract class _Lab_ReportTypeConfig : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(LabReportTypeID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        /// <summary>
        /// 材料类型
        /// </summary>
        [DisplayName("材料类型")]
        public virtual string StuffTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 报告类型
        /// </summary>
        [DisplayName("报告类型")]
        public virtual string LabReportTypeID
        {
            get;
            set;
        }
        
        #endregion
        [ScriptIgnore]
        public virtual StuffType StuffType
        {
            get;
            set;
        }
    }
}
