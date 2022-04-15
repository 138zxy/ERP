using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.Material
{
    public class V_M_StockPactPriceSet
    {
        public virtual StockPactPriceSet StockPactPriceSet { get; set; }
        public virtual StockPact StockPact { get; set; }
        public virtual StockPactChild StockPactChild { get; set; }

        public virtual BitStuffUpdatePrice BitStuffUpdatePrice { get; set; }
    }



    /// <summary>
    /// 批量调价模型
    /// </summary>
    public class BitStuffUpdatePrice
    {
        [DisplayName("价格单号")]
        public string BitStockPactID { get; set; }

        [DisplayName("调整材料")]
        public string BitStuffID { get; set; }

        [DisplayName("调整材料")]
        public string BitStuffName { get; set; } 

        [DisplayName("调整方式")]
        public int BitUpdateType { get; set; }
        [DisplayName("调整值")]
        public decimal BitUpdateCnt { get; set; }
        [DisplayName("生效时间")]
        public DateTime BitUpdateDate { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    public class BitStuff
    {
        [DisplayName("调整材料")]
        public string BitStuffID { get; set; }

        [DisplayName("调整材料")]
        public decimal? Price { get; set; } 
    }
}
