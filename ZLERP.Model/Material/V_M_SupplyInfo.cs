using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Material
{
    public class V_M_SupplyInfo
    {
        public virtual SupplyInfo SupplyInfo { get; set; }
        public virtual M_Car M_Car { get; set; }
    }
}
