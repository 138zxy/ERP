using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.Beton;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Beton
{
    public class B_Balance : _B_Balance
    {

        /// <summary>
        /// 对应的工地
        /// </summary>
        public virtual Project Project
        {
            get;
            set;
        }
        /// <summary>
        /// 对应的是运输公司
        /// </summary>
        public virtual Contract Contract
        {
            get;
            set;
        }
        /// <summary>
        /// 运费模板
        /// </summary>
        public virtual B_FareTemplet B_FareTemplet
        {
            get;
            set;
        }
      

        #region 付款用的冗余字段
        [DisplayName("付款方式")]
        public virtual string PayType { get; set; }
        [DisplayName("结算单号")]
        public virtual int PayInID { get; set; }

        [DisplayName("付款人")]
        [StringLength(50)]
        public virtual string Payer { get; set; }


        [DisplayName("收款人")]
        [StringLength(50)]
        public virtual string Gatheringer { get; set; }
        [DisplayName("票号")]
        public virtual string PiaoNo { get; set; }
        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark2 { get; set; }

        #endregion
        #region 应付管理使用的冗余字段
        [DisplayName("付款额")]
        public virtual decimal? PayMoney2
        {
            get
            {
                return PayOwing;
            }

        }

        [DisplayName("开票额")]
        public virtual decimal? PiaoPayMoney2
        {
            get
            {
                return PiaoPayOwing;
            }

        }

        [DisplayName("优惠额")]
        public virtual decimal PayFavourable2
        {
            get
            {
                return 0;
            }
        }
        [DisplayName("免开票额")]
        public virtual decimal PiaoPayFavourable2
        {
            get
            {
                return 0;
            }
        }

        
        #endregion
    }
}
