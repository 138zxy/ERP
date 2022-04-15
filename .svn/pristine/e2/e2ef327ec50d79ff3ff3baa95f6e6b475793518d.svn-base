using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.ViewModels
{
    public class ContractAndItems
    {
        public Contract Contract { get; set; }
        public ContractItem ContractItem { get; set; }
        public Project Project { get; set; }
        public IdentitySetting IdentitySetting { get; set; }
        public PriceSetting PriceSetting { get; set; }
        public ContractOtherPrice ContractOtherPrice { get; set; }
        public ContractJSTZ ContractJSTZ { get; set; }
        public ContractDZ ContractDZ { get; set; }
        public ContractPay ContractPay { get; set; }
        public ProduceTask ProduceTask { get; set; }
        public ContractProduct ContractProduct { get; set; }
        public ExtraPump ExtraPump { get; set; }
        public ContractLimit ContractLimit { get; set; }
        public BitUpdatePrice BitUpdatePrice { get; set; }
        public ContractPump ContractPump { get; set; }

        public ShipDocRefreshPrice ShipDocRefreshPrice { get; set; }
    }
    public class ContractAndItemsGH
    {
        public ContractGH Contract { get; set; }
        public ContractItemGH ContractItem { get; set; }
        public IdentitySetting IdentitySetting { get; set; }
        public PriceSettingGH PriceSettingGH { get; set; }
        public ContractOtherPriceGH ContractOtherPrice { get; set; }
        public ContractJSTZGH ContractJSTZ { get; set; }
        public ContractDZGH ContractDZ { get; set; }
        public ContractPayGH ContractPay { get; set; }
        public ProduceTaskGH ProduceTask { get; set; }
        public ContractProductGH ContractProduct { get; set; }
        public ExtraPump ExtraPump { get; set; }
        public ContractPumpGH ContractPump { get; set; }
        public ContractInvoiceGH ContractInvoice { get; set; }

        public ShipDocRefreshPriceGH ShipDocRefreshPrice { get; set; }
    }

    public class ShipDocRefreshPrice
    {
        [DisplayName("调价起始时间")]
        public DateTime BeginDate { get; set; }
        [DisplayName("调价截止时间")]
        public DateTime EndDate { get; set; }
        [DisplayName("合同编号")]
        public string ContractID { get; set; }
    }


    /// <summary>
    /// 批量调价模型
    /// </summary>
    public class BitUpdatePrice
    {
        [DisplayName("合同编号")]
        public string BitContractID { get; set; }

        [DisplayName("调整方式")]
        public int BitUpdateType { get; set; }
        [DisplayName("调整值")]
        public decimal BitUpdateCnt { get; set; }
        [DisplayName("生效时间")]
        public DateTime BitUpdateDate { get; set; }

    }
}
