using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.Beton;
using System.ComponentModel;

namespace ZLERP.Model.Beton
{
    public class B_Common
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
            /// 收入
            /// </summary>
            public static string In = "收入";
            /// <summary>
            /// 付款
            /// </summary>
            public static string YingSFOut = "付款";

            /// <summary>
            /// 货款支出
            /// </summary>
            public static string Bale = "货款支出";

            public static string Tran = "运费支出";

            public static string Beton = "砼款收入";
            /// <summary>
            /// 预付款
            /// </summary>
            public static string PerPay = "预付";
            /// <summary>
            /// 期初
            /// </summary>
            public static string PerPayIni = "期初";

            public static string PiaoBeton = "砼款发票";
        }


        /// <summary>
        ///  付款视图
        /// </summary>
        public class B_V_YingSFrec
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
        }


        public class PayType
        {
            /// <summary>
            /// 预付款
            /// </summary>
            public static string type = "预付款"; 
        }

        public class BalaneType
        {
            /// <summary>
            /// 按运距
            /// </summary>
            public static string type1 = "按运距";
            /// <summary>
            /// 按趟数
            /// </summary>
            public static string type2 = "按趟数";
            /// <summary>
            /// 按方量
            /// </summary>
            public static string type3 = "按方量";
            /// <summary>
            /// 按重量
            /// </summary>
            public static string type4 = "按重量";
        }

        /// <summary>
        /// 混凝土结算模式
        /// </summary>
        public class ModelType
        {
            /// <summary>
            /// 混凝土
            /// </summary>
            public static string Beton = "混凝土";
            /// <summary>
            /// 泵送
            /// </summary>
            public static string PeSon = "泵送";

            /// <summary>
            /// 搅拌车
            /// </summary>
            public static string JbCar = "搅拌车";
            /// <summary>
            /// 泵车
            /// </summary>
            public static string PeCar = "泵车";
        }

        /// <summary>
        /// 财务核准流程模型
        /// </summary>
        public class ExecModeltype
        {
            /// <summary>
            /// 混凝土收款核准
            /// </summary>
            public static string Beton = "混凝土收款";

            /// <summary>
            /// 混凝土开票申请
            /// </summary>
            public static string PiaoBeton = "混凝土开票申请";



            /// <summary>
            /// 预付款收款
            /// </summary>
            public static string PayPer = "预付款收款";

            /// <summary>
            /// 期初应收款
            /// </summary>
            public static string IniPay = "期初应收款";
        }

        
    }

}
