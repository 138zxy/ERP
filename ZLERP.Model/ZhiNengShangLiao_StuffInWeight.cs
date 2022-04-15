
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    /// <summary>
    /// 自制机制砂入库
    /// </summary>
    public class ZhiNengShangLiao_StuffInWeight : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(SiloID);
            sb.Append(StuffID);
            sb.Append(WeightNum);
            sb.Append(StockControlID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 筒仓编号
        /// </summary>
        [DisplayName("筒仓编号")]
        public virtual string SiloID
        {
            get;
            set;
        }


        /// <summary>
        /// 材料编号
        /// </summary>
        [DisplayName("材料编号")]
        public virtual string StuffID
        {
            get;
            set;
        }

        /// <summary>
        /// 称重数量(KG)
        /// </summary>
        [DisplayName("称重数量(KG)")]
        public virtual decimal? WeightNum
        {
            get;
            set;
        }

        /// <summary>
        /// 料场称重编号
        /// </summary>
        [DisplayName("料场称重编号")]
        public virtual int? StockControlID
        {
            get;
            set;
        }

        /// <summary>
        /// 材料名称
        /// </summary>
        [DisplayName("材料名称")]
        public virtual string StuffName
        {
            get { return StuffInfo == null ? "" : StuffInfo.StuffName; }
        }


        public virtual Silo Silo
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }


        #endregion
    }
}
