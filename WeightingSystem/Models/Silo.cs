using System;
using System.Collections.Generic;
using System.Text;

namespace WeightingSystem.Models
{
    public class Silo
    {
        public string StuffID { get; set; }
        public string SiloID { get; set; }
        public string SiloName { get; set; }
        public decimal Content { get; set; }


        public string SiloShortName { get; set; }
        public decimal? MinContent { get; set; }
        public decimal? MaxContent { get; set; }
        public decimal? MinWarm { get; set; }
        public decimal? MaxWarm { get; set; }
        public int OrderNum { get; set; }
        public string ProductLineID { get; set; }
        public string ProductLineName { get; set; }
        public string StuffName { get; set; }

    
    }
}
