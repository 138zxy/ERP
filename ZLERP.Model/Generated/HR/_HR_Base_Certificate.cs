using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.HR
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _HR_Base_Certificates : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PersonID);
			sb.Append(CertificatesNo);
			sb.Append(CertificatesName);
			sb.Append(CertificatesType);
			sb.Append(GrantUnit);
			sb.Append(GrantDate);
			sb.Append(StartDate);
			sb.Append(EndDate);
			sb.Append(PhotoPath);
			sb.Append(Meno);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 人员编码
        /// </summary>
        [DisplayName("人员编码")]
        public virtual int? PersonID
        {
            get;
            set;
        }
        /// <summary>
        /// 证件编号
        /// </summary>
        [DisplayName("证件编号")]
        [StringLength(20)]
        public virtual string CertificatesNo
        {
            get;
            set;
        }
        /// <summary>
        /// 证件名称
        /// </summary>
        [DisplayName("证件名称")]
        [StringLength(20)]
        public virtual string CertificatesName
        {
            get;
            set;
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        [DisplayName("证件类型")]
        [StringLength(20)]
        public virtual string CertificatesType
        {
            get;
            set;
        }
        /// <summary>
        /// 发证机构
        /// </summary>
        [DisplayName("发证机构")]
        [StringLength(20)]
        public virtual string GrantUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 发证日期
        /// </summary>
        [DisplayName("发证日期")]
        public virtual System.DateTime? GrantDate
        {
            get;
            set;
        }
        /// <summary>
        /// 有效日期
        /// </summary>
        [DisplayName("有效日期")]
        public virtual System.DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 到期日期
        /// </summary>
        [DisplayName("到期日期")]
        public virtual System.DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 照片
        /// </summary>
        [DisplayName("照片")]
        [StringLength(150)]
        public virtual string PhotoPath
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(30)]
        public virtual string Meno
        {
            get;
            set;
        }	
        #endregion
    }

}
