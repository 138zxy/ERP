using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using ZLERP.Model.Generated.Material;
using System.ComponentModel;

namespace ZLERP.Model.Material
{
    public class M_Common
    {
        public class PlanStatus
        {
            //新计划单，正在进料，暂停进料，进料完成，作废
       
            public static string Ini = "新计划单";
    
            public static string On = "正在进料";
         
            public static string Stop = "暂停进料";
            public static string Over = "进料完成";

            public static string Cancel = "作废";
        }

        public class OtherInfo
        {
 
            /// <summary>
            /// 支出
            /// </summary>
            public static string Out = "支出";
 

            /// <summary>
            /// 付款
            /// </summary>
            public static string YingSFOut = "付款";

            /// <summary>
            /// 货款支出
            /// </summary>
            public static string Bale = "货款支出";

            public static string Tran = "运费支出";
            /// <summary>
            /// 预付款
            /// </summary>
            public static string PerPay = "预付";
            /// <summary>
            /// 期初
            /// </summary>
            public static string PerPayIni = "期初";

            public static string PiaoMaterial = "原材料发票";
        }


        /// <summary>
        ///  付款视图
        /// </summary>
        public class M_V_YingSFrec
        {

            [DisplayName("供应商")]
            public virtual int UnitID { get; set; }


            [DisplayName("日期")]
            public virtual DateTime FinanceDate { get; set; }


            [DisplayName("单据号")]
            public virtual string FinanceNo { get; set; }

            [DisplayName("记录类型")]
            public virtual string Source { get; set; }

            [DisplayName("付款方式")]
            public virtual string PayType { get; set; }


            [DisplayName("预付额")]
            public virtual decimal FinanceMoney { get; set; }

            [DisplayName("预付付款额")]
            public virtual decimal YFinanceMoney { get; set; }

            [DisplayName("优惠额")]
            public virtual decimal PayFavourable { get; set; }

            
            [DisplayName("操作人")]
            public virtual string Builder { get; set; }
        }


        public class PayType
        {
            /// <summary>
            /// 预付款
            /// </summary>
            public static string type = "预付款";
      
        }
    }

}
