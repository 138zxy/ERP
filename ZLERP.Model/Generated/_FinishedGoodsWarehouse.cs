
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  成品仓进出记录表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _FinishedGoodsWarehouse : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ProductLineID);
            sb.Append(SiloNo);
            sb.Append(SiloName);
            sb.Append(InOutTime);
            sb.Append(OrderNumber);
            sb.Append(ShipDocID);
            sb.Append(PropertNumber);
            sb.Append(InOutCount);
            sb.Append(UserName);
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
        /// 成品仓号（1/2/3/4）
        /// </summary>
        [Required]
        [DisplayName("成品仓号")]
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
        /// 进出料时间
        /// </summary>
        [Required]
        [DisplayName("进出料时间")]
        public virtual System.DateTime? InOutTime
        {
            get;
            set;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        [DisplayName("订单号")]
        public virtual string OrderNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 运输单号
        /// </summary>
        [DisplayName("运输单号")]
        public virtual string ShipDocID
        {
            get;
            set;
        }
        /// <summary>
        /// 配比号
        /// </summary>
        [DisplayName("配比号")]
        public virtual string PropertNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 当次出入量
        /// </summary>
        [DisplayName("当次出入量")]
        public virtual decimal InOutCount
        {
            get;
            set;
        }
        /// <summary>
        /// 操作者名称
        /// </summary>
        [DisplayName("操作者名称")]
        public virtual string UserName
        {
            get;
            set;
        }
        #endregion
    }
}
