using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Material
{
    public class V_M_TranBalance
    {
        public M_TranBalance M_TranBalance { get; set; }
        public M_TranBalanceDel M_TranBalanceDel { get; set; }

        public virtual string OrderNo { get; set; }
    }
}
