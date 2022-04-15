using System;
using System.Collections.Generic;
using System.Text;

namespace WeightingSystem.Models
{
    public class CarEmptyWeight
    {

        public int ID { get; set; }
        public string CarID { get; set; }
        public int Weight { get; set; }
        public string Builder { get; set; }
        public DateTime BuildTime { get; set; }
    }
}
