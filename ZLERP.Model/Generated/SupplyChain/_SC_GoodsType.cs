using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{

    public abstract class _SC_GoodsType : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(TypeNo);
            sb.Append(ParentID);
            sb.Append(TypeName);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion

        [Required]
        [DisplayName("父类")]
        public virtual int ParentID { get; set; }

        [DisplayName("分类编号")]
        public virtual string TypeNo { get; set; }

        [DisplayName("分类名称")]
        public virtual string TypeName { get; set; }

        [DisplayName("序号")]
        public virtual int OrderNo { get; set; }

        [DisplayName("标识")]
        [Required]
        public virtual int Flag { get; set; }
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

        /// <summary>
        /// 层级
        /// </summary>
        [Required]
        [DisplayName("层级")]
        public virtual int DptLevel
        {
            get;
            set;
        }
    }
}
  


