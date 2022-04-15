using System;
using System.Collections.Generic;
using System.Text;

namespace WeightingSystem.Models
{
    public class StuffSale
    {
        /// <summary>
        ///
        /// </summary>
        public String StuffSaleID { set; get; }
        /// <summary>
        /// 运输车号
        /// </summary>
        public String CarID { set; get; }
        /// <summary>
        ///车牌号码
        /// </summary>
        public String CarNo { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String StuffID { set; get; }
        /// <summary>
        /// 原料名称
        /// </summary>
        public String StuffName { set; get; }
        /// <summary>
        ///收货单位
        /// </summary>
        public String SupplyID { set; get; }
        /// <summary>
        ///供货单位
        /// </summary>
        public String CompanyID { set; get; }
        /// <summary>
        /// 供货单位
        /// </summary>
        public String CompName { set; get; }
        /// <summary>
        ///
        /// </summary>
        public Int32? TotalWeight { set; get; }
        /// <summary>
        ///
        /// </summary>
        public Int32? CarWeight { set; get; }
        /// <summary>
        ///
        /// </summary>
        public Int32? Weight { set; get; }
        /// <summary>
        ///称重人
        /// </summary>
        public String WeightMan { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String WeightName { set; get; }
        /// <summary>
        /// 到场时间
        /// </summary>
        public DateTime? ArriveTime { set; get; }
        /// <summary>
        /// 出站时间
        /// </summary>
        public DateTime? DeliveryTime { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String Remark { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String Builder { set; get; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? BuildTime { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String Modifier { set; get; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? ModifyTime { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String Version { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String Lifecycle { set; get; }
        /// <summary>
        ///
        /// </summary>
        public String SiloID { set; get; }

        public int NextValue { get; set; }

        public string IDPrefix { get; set; }
    }
}
