using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.SupplyChain
{
    public class SC_Common
    {

        public class Pstatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            public static string Ini = "草稿";
            /// <summary>
            /// 已审核
            /// </summary>
            public static string Audit = "已审核";
            /// <summary>
            /// 已完成
            /// </summary>
            public static string Completed = "已完成";
        }

        public class InStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            public static string Ini = "草稿";
            /// <summary>
            /// 已入库
            /// </summary>
            public static string InLib = "已入库";

            /// <summary>
            /// 已出库
            /// </summary>
            public static string OutLib = "已出库";


            /// <summary>
            /// 已调拨
            /// </summary>
            public static string ToLib = "已调拨";
        }

        public class InType
        {
            /// <summary>
            /// 采购入库
            /// </summary>
            public static string PurIn = "采购入库";
            /// <summary>
            /// 采购退货
            /// </summary>
            public static string PurOut = "采购退货";
            /// <summary>
            /// 其他入库
            /// </summary>
            public static string OtherIn = "其他入库";

            /// <summary>
            /// 库存初始化
            /// </summary>
            public static string LibIni = "库存初始化";

            /// <summary>
            /// 报溢
            /// </summary>
            public static string Baoyi = "报溢";
            /// <summary>
            /// 报损
            /// </summary>
            public static string BaoSun = "报损";
        }

        public class OutType
        {
            /// <summary>
            /// 申领出库
            /// </summary>
            public static string QurOut = "申领出库";
            /// <summary>
            /// 申领退货
            /// </summary>
            public static string QurIn = "申领退回";
            /// <summary>
            /// 其他出库
            /// </summary>
            public static string OtherIn = "其他出库";

            /// <summary>
            /// 维修保养出库
            /// </summary>
            public static string MaintainOut = "维修出库";

            /// <summary>
            /// 剩料退还
            /// </summary>
            public static string MaintainBack = "剩料退还";
        }

        public class PayType
        {
            /// <summary>
            /// 供应商预付款
            /// </summary>
            public static string SupplyIn = "供应商预付款";
            /// <summary>
            /// 全额欠款
            /// </summary>
            public static string AllOut = "全额欠款";
        }

        public class OtherInfo
        {
            /// <summary>
            /// 收入
            /// </summary>
            public static string In = "申领";
            /// <summary>
            /// 支出
            /// </summary>
            public static string Out = "支出";
            /// <summary>
            /// 申领消耗
            /// </summary>
            public static string YingSFIn = "申领消耗";

            /// <summary>
            /// 付款
            /// </summary>
            public static string YingSFOut = "付款";

            /// <summary>
            /// 采购支出
            /// </summary>
            public static string Pur = "采购支出";
            /// <summary>
            /// 预付款
            /// </summary>
            public static string PerPay = "预付";
            /// <summary>
            /// 期初
            /// </summary>
            public static string PerPayIni = "期初";
        }
        public class FomrSource
        {
            /// <summary>
            /// 原料
            /// </summary>
            public static string Material = "原料";
            /// <summary>
            /// 产品
            /// </summary>
            public static string Product= "产品";
        }

        public class Depreciation
        {
            /// <summary>
            /// 平均年限法
            /// </summary>
            public static string DYear = "平均年限法";


            /// <summary>
            /// 年数总和法
            /// </summary>
            public static string DAll = "年数总和法";

            /// <summary>
            /// 双倍余额递减法
            /// </summary>
            public static string Duble = "双倍余额递减法";

        }
        /// <summary>
        /// 固定资产状态
        /// </summary>
        public class FxiedCondition
        {
            /// <summary>
            /// 正常
            /// </summary>
            public static string Normal = "正常";
            /// <summary>
            /// 已借出
            /// </summary>
            public static string Circulate = "已借出";

            /// <summary>
            /// 已清理
            /// </summary>
            public static string Clean = "已清理";

            /// <summary>
            /// 维修保养中
            /// </summary>
            public static string Maintain = "维修保养中";

            /// <summary>
            /// 盘亏
            /// </summary>
            public static string PanKui = "盘亏";

            /// <summary>
            /// 盘盈
            /// </summary>
            public static string PanYing = "盘盈";
        }
    }
}
