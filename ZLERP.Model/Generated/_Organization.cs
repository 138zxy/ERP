using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _Organization : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(OrganizationName);
            sb.Append(ParentId);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 组织名称
        /// </summary>
        [DisplayName("组织名称")]
        [StringLength(50)]
        public virtual string OrganizationName
        {
            get;
            set;
        }

        /// <summary>
        /// 层级
        /// </summary>
        [Required]
        [DisplayName("层级")]
        public virtual int OrganizationLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 上级组织编号
        /// </summary>
        [DisplayName("上级组织编号")]
        [StringLength(30)]
        public virtual string ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [Required]
        [DisplayName("是否叶子节点")]
        public virtual bool IsLeaf
        {
            get;
            set;
        }


        #endregion
    }
}
