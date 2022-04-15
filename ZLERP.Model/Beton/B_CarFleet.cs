using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.Beton;

namespace ZLERP.Model.Beton
{
    public class B_CarFleet : _B_CarFleet
    {
        public virtual B_FareTemplet B_FareTemplet { get; set; }
    }
}
