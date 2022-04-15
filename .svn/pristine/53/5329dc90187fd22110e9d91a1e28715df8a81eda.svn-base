using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated.Material
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _M_BaleBalanceDel : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(BaleBalanceID);
            sb.Append(SpecID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("主体ID")]
        public virtual int BaleBalanceID { get; set; }

        [DisplayName("进料单号")]
        public virtual string StuffInID { get; set; }

        [DisplayName("单价")]
        public virtual decimal Price { get; set; }

        [DisplayName("其他费用")]
        public virtual decimal OtherMoney { get; set; }

        [DisplayName("总价")]
        public virtual decimal AllMoney { get; set; }

        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 材料规格
        /// </summary>
        [DisplayName("材料规格")]
        public virtual int? SpecID
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual StuffinfoSpec StuffinfoSpec
        {
            get;
            set;
        }
        #endregion
    }
}
