using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.Beton;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Beton
{
    public class B_TranBalanceDel : _B_TranBalanceDel
    {

        public virtual ShippingDocument ShippingDocument { get; set; }
        [ScriptIgnore]
        public virtual B_TranBalance B_TranBalance { get; set; }
    }
}
