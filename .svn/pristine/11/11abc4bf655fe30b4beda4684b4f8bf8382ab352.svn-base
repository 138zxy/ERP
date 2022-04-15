using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SystemManage
{


    public abstract class _PrintReport : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(ReportNo);
            sb.Append(ReportName); 
            sb.Append(Version); 
            return sb.ToString().GetHashCode();
        }

        #endregion
        [DisplayName("编号")]
        [Required]
        [StringLength(50)]
        public virtual string ReportNo { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("报表名称")]
        public virtual string ReportName { get; set; }
        [DisplayName("报表类型")]
        public virtual string ReportType { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        [DisplayName("数据源类型")]
        public virtual string SoucreType { get; set; }

        [DisplayName("数据源")]
        public virtual string SqlData { get; set; }

        [DisplayName("报表文件")]
        public virtual string FilePath { get; set; }
    }
}
  


