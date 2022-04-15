using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.CarManage
{
    public class CarMaterial : Generated.CarManage._CarMaterial
    {
        public virtual Car Car { get; set; }
    }
}
