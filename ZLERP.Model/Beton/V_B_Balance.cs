using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Beton
{
    public class V_B_Balance
    {
        public B_Balance B_Balance { get; set; }
        public B_BalanceDel B_BalanceDel { get; set; }

        public virtual string OrderNo { get; set; }
    }
}
