using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Material
{

    public class M_PerPay
    {
        //注意这里和供应链有点区别，这里是对合同付款，这里应该是合同号,对运费付款，之类是运输公司ID
        /// <summary>
        /// 供应商编码或者运输公司ID
        /// </summary>
        public virtual string StockPactID { get; set; }

        public virtual string PayType { get; set; }

        public virtual DateTime InDate { get; set; }
        public virtual string Payer { get; set; }
        public virtual string PiaoNo { get; set; }
        public virtual string Gatheringer { get; set; }

        public virtual string Remark2 { get; set; }

        public string Records { get; set; }
    }

    public class m_perpay
    {
        public int ID { get; set; }
        public decimal PayMoney2 { get; set; }
        public decimal PayFavourable2 { get; set; }
    }

}
