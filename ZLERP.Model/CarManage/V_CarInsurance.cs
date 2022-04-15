using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.CarManage
{
    public class V_CarInsurance
    {
        public virtual CarInsurance CarInsurance { get; set; }
        public virtual CarInsuranceItem CarInsuranceItem { get; set; }
    }
}
