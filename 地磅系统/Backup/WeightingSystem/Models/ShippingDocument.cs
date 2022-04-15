using System;
using System.Collections.Generic;
using System.Text;

namespace WeightingSystem.Models
{
    public class ShippingDocument
    {

        public string ShipDocID { get; set; }
        public string TaskID { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string CustomerID { get; set; }
        public string ConstructUnit { get; set; }
        public string RealSlump { get; set; }
        public string CustName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAddr { get; set; }
        public string ConStrength { get; set; }
        public string ProductLineName { get; set; }
        public string CastMode { get; set; }
        public string ConsPos { get; set; }
        public string PumpName { get; set; }
        public decimal ParCube { get; set; }
        public decimal SendCube { get; set; }
        public decimal ProvidedCube { get; set; }
        public int ProvidedTimes { get; set; }
        public DateTime? ProduceDate { get; set; }

        public string CarID { get; set; }

        public DateTime? ServerTime { get; set; }
        public int? ConstrengthExchange { get; set; }
        public int TotalWeight { get; set; }
        public int CarWeight { get; set; }
        public int Weight { get; set; }
        public int Exchange { get; set; }
        public decimal Cube { get; set; }
    }
}
