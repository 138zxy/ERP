using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 验收取样记录
    /// </summary>
    public class Lab_Record : _Lab_Record
    {
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }
        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }
        public virtual string SiloName
        {
            get
            {
                return this.Silo == null ? "" : this.Silo.SiloName;
            }
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
    }
}