using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SC_GoodsUnit : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(GoodsID);
            sb.Append(UnitDesc);
            sb.Append(Unit);
            sb.Append(Rate);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 商品编码
        /// </summary>
        [DisplayName("商品编码")]
        [Required]
        public virtual int? GoodsID
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public virtual string UnitDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        [Required]
        public virtual string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 比率
        /// </summary>
        [DisplayName("比率")]
        [Required]
        public virtual int Rate
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
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
