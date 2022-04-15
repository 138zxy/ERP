using System;
using System.Collections.Generic;
using System.Text;

namespace WeightingSystem.Models
{
    public class StuffIn
    {
        public string StuffInID { get; set; }
        public string StuffID { get; set; }
        public string StuffName { get; set; }
        public string SiloID { get; set; }
        public string SiloName { get; set; }
        public string StockPactID { get; set; }
        public string SupplyID { get; set; }
        public string SupplyName { get; set; }
        public string TransportID { get; set; }
        public string TransportName { get; set; }
        public string CustName {get; set; }
        public string GageUnit {get; set;}
        public decimal TransportNum { get; set; }
        public decimal SupplyNum { get; set; }
        public decimal TotalNum { get; set; }
        public decimal CarWeight { get; set; }
        public decimal StockNum { get; set; }
        public decimal WRate { get; set; }
        public decimal InNum { get; set; }
        public decimal Proportion { get; set; }
        public decimal FootNum { get; set; }
        public string Driver {get ;set ;}
        public string SourceAddr {get ;set ;}
        public DateTime? InDate {get ;set;}
        public DateTime? OutDate {get ;set;}
        public string AH {get ;set;}
        public bool IsBack {get ;set;}
        public string Remark {get ;set;}
        public string CarNo{get ;set;}
        public string Operator {get ;set;}
        public int FootStatus { get ;set;}
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string Pic4 { get; set; }
        public string Spec { get; set; }
        public bool FastMetage { get; set; }
        public decimal DarkWeight { get; set; }
        public int NextValue { get; set; }
        public string ParentStuffInID { get; set; }       //父入库单号，用于换罐
        public int OrderNum { get; set; }                 //用于换罐
        public string FinalStuffType { get; set; }
        public string WeightName { get; set; }

        public string Pic11 { get; set; }
        public string Pic21 { get; set; }
        public string Pic31 { get; set; }
        public string Pic41 { get; set; }
        /// <summary>
        /// 明扣重
        /// </summary>
        public decimal MingWeight { get; set; }
        /// <summary>
        /// 扣杂方式
        /// </summary>
        public int RateMode { get; set; }

        public string IDPrefix { get; set; }

        public string CompanyID { get; set; }
        public string CompanyName { get; set; }

        public string SourceNumber { get; set; }

        public decimal? UnitPrice { get; set; }
        public string Builder { get; set; }

        public decimal? FinalFootNum { get; set; }
        public decimal? Volume { get; set; }

        public int? SpecID { get; set; }
        public string Batch { get; set; }
    }
}
