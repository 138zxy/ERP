using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _Lab_CompComm : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Lab_CompCommId);
            sb.Append(ShipDocId);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 抗压委托编号
        /// </summary>
        [Required]
        [DisplayName("抗压委托编号")]
        [StringLength(50)]
        public virtual string Lab_CompCommId
        {
            get;
            set;
        }
        /// <summary>
        /// 运输单号
        /// </summary>
        [DisplayName("运输单号")]
        [StringLength(128)]
        public virtual string ShipDocId
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(350)]
        public virtual string Remark
        {
            get;
            set;
        }
        #endregion
    }
}
