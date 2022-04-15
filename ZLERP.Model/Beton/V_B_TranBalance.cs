using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Beton
{
    public class V_B_TranBalance
    {
        public B_TranBalance B_TranBalance { get; set; }
        public B_TranBalanceDel B_TranBalanceDel { get; set; }

        public virtual string OrderNo { get; set; }
    }
}
