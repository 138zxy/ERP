using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    public abstract class _ShippingDocumentNoGH : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 搅拌线编号
        /// </summary>
        [DisplayName("搅拌线编号")]
        [StringLength(30)]
        public virtual string ProductLineID
        {
            get;
            set;
        }
        /// <summary>
        /// 前缀
        /// </summary>
        [DisplayName("前缀")]
        public virtual string IDPrefix
        {
            get;
            set;
        }
        /// <summary>
        /// 下个值
        /// </summary>
        [DisplayName("下个值")]
        public virtual int NextValue
        {
            get;
            set;
        }
        #endregion
    }
}
