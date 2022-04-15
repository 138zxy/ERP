using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Beton
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _B_TranBalanceDel : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(BaleBalanceID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("主体ID")]
        public virtual int BaleBalanceID { get; set; }

        [DisplayName("运输单号")]
        public virtual string ShipDocID { get; set; }

        [DisplayName("单价")]
        public virtual decimal Price { get; set; } 
 
        [DisplayName("车队补方数")]
        public virtual decimal CarBetonNum { get; set; }


     
        [DisplayName("车队超时费")]
        public virtual decimal OutTimeFee { get; set; }

        [DisplayName("车队超距距离")]
        public virtual decimal OutDistance { get; set; }

        [DisplayName("车队超距费")]
        public virtual decimal OutDistanceFee { get; set; }


        [DisplayName("其他费用")]
        public virtual decimal OtherFee { get; set; }
        [DisplayName("总费用")]
        public virtual decimal AllMoney { get; set; }



        [DisplayName("客户补运费")]
        public virtual decimal CustBetonfee { get; set; }
 

        [DisplayName("模式")]
        public virtual string ModelType
        {
            get;
            set;
        }

        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        #endregion
    }
}
