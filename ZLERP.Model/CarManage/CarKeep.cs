﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.CarManage
{
    public class CarKeep : Generated.CarManage._CarKeep
    {
        public virtual Car Car { get; set; }


        public virtual CarDealings CarDealings { get; set; }
    }
}
