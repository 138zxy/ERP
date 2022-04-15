using System;
using System.Collections.Generic;
using System.Text;

namespace WeightingSystem.Models
{
    public class ShippingDocument
    {

        public string ShipDocID { get; set; }
        public string Tel { get; set; }
        public string ImpGrade { get; set; }
        public string TaskID { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string CustomerID { get; set; }
        public string ConstructUnit { get; set; }
        public string RealSlump { get; set; }
        public string CustName { get; set; }
        public string ProjectName { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Driver { get; set; }
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
        public int? SlurryExchange { get; set; }
        public int TotalWeight { get; set; }
        public int CarWeight { get; set; }
        public int Weight { get; set; }
        public decimal Exchange { get; set; }
        public decimal Cube { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string WeightMan { get; set; }
        public string WeightName { get; set; }

        public string ShipDocType { get; set; }

        public string Operator { get; set; }

        /// <summary>
        /// 计划方量
        /// </summary>
        public decimal PlanCube { get; set; }
        /// <summary>
        /// 砂浆方量
        /// </summary>
        public decimal SlurryCount { get; set; }
        /// <summary>
        /// 运输方量
        /// </summary>
        public decimal ShippingCube { get; set; }

        public string LinkMan { get; set; }

        /// <summary>
        /// 销售合同-业务员
        /// </summary>
        public string Salesman { get; set; }
        /// <summary>
        /// 其他要求
        /// </summary>
        public string OtherDemand { get; set; }

        /// <summary>
        /// 理论配比名称
        /// </summary>
        public string FormulaName { get; set; }

        /// <summary>
        /// 施工配比号
        /// </summary>
        public string ConsMixpropID { get; set; }
        /// <summary>
        /// 质检员
        /// </summary>
        public string Surveyor { get; set; }

        /// <summary>
        /// 调度已供方量
        /// </summary>
        public decimal ProvidedCube_Dis { get; set; }
        public int ProvidedTimes_Dis { get; set; }
        public virtual decimal? Distance { get; set; }
        public virtual int? DataType
        {
            get;
            set;
        }
    }
}
