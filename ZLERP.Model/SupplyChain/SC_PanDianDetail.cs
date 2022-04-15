using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.SupplyChain
{
    public class SC_PanDianDetail : Generated.SupplyChain._SC_PanDianDetail
    {
 
    
        public virtual SC_Goods SC_Goods { get; set; }

        /// <summary>
        /// 库存单价
        /// </summary>
        public virtual decimal Price { get; set; }
    }
}
