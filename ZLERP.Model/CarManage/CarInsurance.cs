﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.CarManage
{
    public class CarInsurance : Generated.CarManage._CarInsurance
    {
        public virtual Car Car { get; set; }


        public virtual CarDealings CarDealings { get; set; }
    }
}