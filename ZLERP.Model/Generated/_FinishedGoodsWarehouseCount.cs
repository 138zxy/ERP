
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  理论配比子项表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _FinishedGoodsWarehouseCount : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ProductLineID);
            sb.Append(SiloNo);
            sb.Append(SiloName);
            sb.Append(StoreCount);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 生产线
        /// </summary>
        [Required]
        [DisplayName("生产线")]
        public virtual string ProductLineID
        {
            get;
            set;
        }
        /// <summary>
        /// 筒仓号
        /// </summary>
        [Required]
        [DisplayName("筒仓号")]
        public virtual int SiloNo
        {
            get;
            set;
        }
        /// <summary>
        /// 筒仓名称
        /// </summary>
        [Required]
        [DisplayName("筒仓名称")]
        public virtual string SiloName
        {
            get;
			set;			 
        }
        /// <summary>
        /// 存量（千克）
        /// </summary>
        [Required]
        [DisplayName("存量（千克）")]
        public virtual decimal StoreCount
        {
            get;
            set;
        }
        #endregion
    }
}
