///下面两个编译条件参数指定产生报表数据的格式。如果都不定义，则产生 XML 形式的报表数据
///编译条件参数定义在项目属性的“生成->条件编译符号”里更合适，这样可以为整个项目使用
///_XML_REPORT_DATA：指定产生 XML 形式的报表数据
///_JSON_REPORT_DATA：指定产生 JSON 形式的报表数据。
//#define _XML_REPORT_DATA
#define _JSON_REPORT_DATA

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Diagnostics;

namespace GridReport
{

#if _JSON_REPORT_DATA
    using MyDbReportData = DatabaseJsonReportData;
    using ZLERP.Business;
    using ZLERP.Model.SupplyChain;
    using ZLERP.Web.Helpers;
    using System.Data.SqlClient;
    using System.Data;
#else
    using MyDbReportData = DatabaseXmlReportData;
#endif

    /// <summary>
    /// 在这里集中产生整个项目的所有报表需要的 XML 或 JSON 文本数据 
    /// </summary>
    public class DataTextProvider
    {
        //创建一个静态的DataTextProvider对象,在构造函数里执行初始化任务
        private static DataTextProvider initStartup = new DataTextProvider();
        public DataTextProvider()
        {
            DataTextProvider.InitDataFunMap();
        }

        /// <summary>
        /// 根据查询SQL语句产生报表数据
        /// </summary>
        public static string Build(string QuerySQL)
        {
            return MyDbReportData.TextFromOneSQL(QuerySQL);
        }

        /// <summary>
        /// 根据多条查询SQL语句产生报表数据，数据对应多记录集
        /// </summary>
        public static string Build(ArrayList QueryList)
        {
            return MyDbReportData.TextFromMultiSQL(QueryList);
        }




        #region 根据 HTTP 请求中的参数生成报表数据，主要是为例子报表自动分配合适的数据生成函数
        /// <summary>
        /// 为了避免 switch 语句的使用，建立数据名称与数据函数的映射(map)
        /// 在 Global.asax 中创建映射，即在WEB服务启动时初始化映射数据
        /// </summary>

        //简单无参数报表数据的名称与函数映射表
        private delegate string SimpleDataFun();
        private static Dictionary<string, SimpleDataFun> SimpleDataFunMap;

        //有参数报表数据的名称与函数映射表，参数来自 HttpRequest
        private delegate string SpecialDataFun(HttpRequest Request);
        private static Dictionary<string, SpecialDataFun> SpecialDataFunMap;

        public static string BuildByHttpRequest(HttpRequest Request)
        {
            string DataText;
            string DataName = Request.QueryString["data"];
            if (string.IsNullOrWhiteSpace(DataName))
            {
                DataName = "GetSqlData"; //默认获取的数据的SQL方法
            }  
            if (DataName != null && DataName != "")
            {
                //根据数据名称查找映射表，如果找到，执行对应的报表数据函数获取数据
                SimpleDataFun simpleFun;
                SpecialDataFun specialFun;
                if (SimpleDataFunMap.TryGetValue(DataName, out simpleFun))
                {
                    DataText = simpleFun();
                }
                else if (SpecialDataFunMap.TryGetValue(DataName, out specialFun))
                {
                    DataText = specialFun(Request);
                }
                else
                {
                    throw new Exception(string.Format("没有为报表数据 '{0}' 分配处理程序！", DataName));
                }
            }
            else
            {
                string QuerySQL = Request.QueryString["QuerySQL"];
                if (QuerySQL != null)
                {
                    //根据传递的 HTTP 请求中的查询SQL获取数据
                    DataText = DataTextProvider.Build(QuerySQL);
                }
                else if (Request.TotalBytes > 0)
                {
                    //从客户端发送的数据包中获取报表查询参数，URL有长度限制，当要传递的参数数据量比较大时，应该采用这样的方式
                    //这里演示了用这样的方式传递一个超长查询SQL语句。
                    byte[] FormData = Request.BinaryRead(Request.TotalBytes);
                    UTF8Encoding Unicode = new UTF8Encoding();
                    int charCount = Unicode.GetCharCount(FormData, 0, Request.TotalBytes);
                    char[] chars = new Char[charCount];
                    int charsDecodedCount = Unicode.GetChars(FormData, 0, Request.TotalBytes, chars, 0);

                    QuerySQL = new String(chars);

                    DataText = DataTextProvider.Build(QuerySQL);
                }
                else
                {
                    DataText = "";
                }
            }

            return DataText;
        }

        //初始化映射表(map)，在 Global.asax 中被调用
        public static void InitDataFunMap()
        {
            Trace.Assert(SimpleDataFunMap == null && SpecialDataFunMap == null, "DataFunMap already initialized!");
            SimpleDataFunMap = new Dictionary<string, SimpleDataFun>();
            SpecialDataFunMap = new Dictionary<string, SpecialDataFun>();

            SpecialDataFunMap.Add("GetSqlData", DataTextProvider.GetSqlData); 
            SpecialDataFunMap.Add("GetPiaoInOrder", DataTextProvider.GetPiaoInOrder);
            SpecialDataFunMap.Add("GetPiaoIn", DataTextProvider.GetPiaoIn);
            SpecialDataFunMap.Add("GetPiaoOutOrder", DataTextProvider.GetPiaoOutOrder);
            SpecialDataFunMap.Add("GetPiaoOut", DataTextProvider.GetPiaoOut);
            SpecialDataFunMap.Add("GetPiaoTo", DataTextProvider.GetPiaoTo);
            SpecialDataFunMap.Add("GetPanDian", DataTextProvider.GetPanDian);
            SpecialDataFunMap.Add("GetYingSF", DataTextProvider.GetYingSF);
            SpecialDataFunMap.Add("GetBase_Personnel", DataTextProvider.GetBase_Personnel);

        }

        public static string GetSqlData(HttpRequest Request)
        {

            string DataName = Request.QueryString["report"];
            int Count = Request.QueryString.Count;
            //判断有多少个参数
            int j = 0;
            for (int i = 0; i < Count; ++i)
            {
                string Key = Request.QueryString.GetKey(i);
                if (Key != "report" && Key != "data")
                {
                    j++;
                }
            }
            SqlParameter[] parameters = new SqlParameter[j];
            int z = 0;
            for (int i = 0; i < Count; ++i)
            {
                string Key = Request.QueryString.GetKey(i);
                if (Key != "report" && Key != "data")
                {

                    string value = Request.QueryString[i];
                    parameters[z] = new SqlParameter("@" + Key, SqlDbType.NVarChar);
                    parameters[z].Value = value;
                    z++;
                }
            }

            var re = MyDbReportData.TextFromSQL(DataName, parameters);
            return re;
        }

        /// <summary>
        /// 采购订单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetPiaoInOrder(HttpRequest Request)
        {
            string ID = Request.QueryString["key"];
            string SC_PiaoInOrderQuerySQL = @"SELECT  SC_PiaoInOrder.* ,
                                                    SC_Supply.* ,
                                                    SC_Lib.*
                                            FROM    SC_PiaoInOrder
                                                    LEFT JOIN SC_Supply ON SC_Supply.ID = SC_PiaoInOrder.SupplierID
                                                    LEFT JOIN dbo.SC_Lib ON SC_Lib.ID = SC_PiaoInOrder.LibID
                                                    WHERE SC_PiaoInOrder.ID='" + ID + "'"; ;
            string SC_ZhangInOrderQuerySQL = @"  SELECT  SC_ZhangInOrder.* ,
                                           SC_Goods.*,SC_ZhangInOrder.UnitPrice*SC_ZhangInOrder.Quantity AS  ZhangMoney
                                    FROM    dbo.SC_ZhangInOrder
                                            LEFT JOIN dbo.SC_Goods ON SC_Goods.ID = SC_ZhangInOrder.GoodsID
                                            WHERE SC_ZhangInOrder.OrderNo='" + ID + "'"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_PiaoInOrderQuerySQL, "SC_PiaoInOrder"));
            QueryList.Add(new ReportQueryItem(SC_ZhangInOrderQuerySQL, "SC_ZhangInOrder"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }
        /// <summary>
        /// 入库单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetPiaoIn(HttpRequest Request)
        {
            string ID = Request.QueryString["key"];
            string SC_PiaoInQuerySQL = @"SELECT SC_PiaoIn.*,SC_Supply.*,SC_Lib.* FROM dbo.SC_PiaoIn
                                              LEFT JOIN dbo.SC_Supply  ON SC_Supply.ID= SC_PiaoIn.SupplierID
                                              LEFT JOIN dbo.SC_Lib  ON SC_Lib.ID=SC_PiaoIn.LibID
                                              WHERE SC_PiaoIn.ID='" + ID + "'"; ;
            string SC_ZhangInQuerySQL = @"SELECT  SC_ZhangIn.*,SC_Goods.*  FROM dbo.SC_ZhangIn
                                               LEFT JOIN dbo.SC_Goods ON SC_Goods.ID=SC_ZhangIn.GoodsID
                                               WHERE SC_ZhangIn.InNo='" + ID + "'"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_PiaoInQuerySQL, "SC_PiaoIn"));
            QueryList.Add(new ReportQueryItem(SC_ZhangInQuerySQL, "SC_ZhangIn"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }
        /// <summary>
        /// 申请单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetPiaoOutOrder(HttpRequest Request)
        {
            string ID = Request.QueryString["key"];
            string SC_PiaoOutOrderQuerySQL = @"SELECT  SC_PiaoOutOrder.*,Department.DepartmentName FROM dbo.SC_PiaoOutOrder
                                            LEFT JOIN dbo.Department ON Department.DepartmentID= SC_PiaoOutOrder.DepartmentID
                                              WHERE SC_PiaoOutOrder.ID='" + ID + "'"; ;
            string SC_ZhangOutOrderQuerySQL = @" SELECT * FROM dbo.SC_ZhangOutOrder
                                            LEFT JOIN dbo.SC_Goods ON SC_ZhangOutOrder.GoodsID=SC_Goods.ID
                                               WHERE SC_ZhangOutOrder.OrderNo='" + ID + "'"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_PiaoOutOrderQuerySQL, "SC_PiaoOutOrder"));
            QueryList.Add(new ReportQueryItem(SC_ZhangOutOrderQuerySQL, "SC_ZhangOutOrder"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }
        /// <summary>
        /// 出库单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetPiaoOut(HttpRequest Request)
        {
            string ID = Request.QueryString["key"];
            string SC_PiaoOutQuerySQL = @"SELECT SC_PiaoOut.*,Department.DepartmentName,SC_Lib.LibName FROM dbo.SC_PiaoOut
                                        LEFT JOIN dbo.Department ON Department.DepartmentID= SC_PiaoOut.DepartmentID
                                        LEFT JOIN dbo.SC_Lib ON SC_PiaoOut.LibID=dbo.SC_Lib.ID
                                        WHERE SC_PiaoOut.ID='" + ID + "'"; ;
            string SC_ZhangOutQuerySQL = @"SELECT SC_ZhangOut.*,SC_Goods.*  FROM dbo.SC_ZhangOut
                                        LEFT JOIN dbo.SC_Goods ON SC_ZhangOut.GoodsID=SC_Goods.ID
                                        WHERE SC_ZhangOut.OutNo='" + ID + "'"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_PiaoOutQuerySQL, "SC_PiaoOut"));
            QueryList.Add(new ReportQueryItem(SC_ZhangOutQuerySQL, "SC_ZhangOut"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }
        /// <summary>
        /// 调库单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetPiaoTo(HttpRequest Request)
        {
            string ID = Request.QueryString["key"];
            string SC_PiaoToQuerySQL = @"SELECT p.*,a.LibName,b.LibName AS LibNameIn FROM SC_PiaoTo AS P
                                            LEFT JOIN dbo.SC_Lib AS A ON A.ID=P.OutLibID
                                            LEFT JOIN dbo.SC_Lib AS B ON B.ID=P.InLibID
                                        WHERE p.ID='" + ID + "'"; ;
            string SC_ZhangToQuerySQL = @" SELECT SC_ZhangTo.*,SC_Goods.*  FROM dbo.SC_ZhangTo
                                             LEFT JOIN dbo.SC_Goods ON SC_ZhangTo.GoodsID=SC_Goods.ID
                                        WHERE SC_ZhangTo.ChangeNo='" + ID + "'"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_PiaoToQuerySQL, "SC_PiaoTo"));
            QueryList.Add(new ReportQueryItem(SC_ZhangToQuerySQL, "SC_ZhangTo"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }
        /// <summary>
        /// 盘点单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetPanDian(HttpRequest Request)
        {
            string ID = Request.QueryString["key"];
            string SC_PanDianQuerySQL = @"SELECT p.*,a.LibName  FROM SC_PanDian AS P
                                        LEFT JOIN dbo.SC_Lib AS A ON A.ID=P.LibID 
                                        WHERE p.ID='" + ID + "'"; ;
            string SC_PanDianDetailQuerySQL = @"SELECT SC_PanDianDetail.*,SC_Goods.* FROM dbo.SC_PanDianDetail
                                        LEFT JOIN dbo.SC_Goods ON SC_Goods.ID=SC_PanDianDetail.GoodsID
                                        WHERE SC_PanDianDetail.PanID='" + ID + "'"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_PanDianQuerySQL, "SC_PanDian"));
            QueryList.Add(new ReportQueryItem(SC_PanDianDetailQuerySQL, "SC_PanDianDetail"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }

        /// <summary>
        /// 采购清单
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetYingSF(HttpRequest Request)
        {
            string keys = Request.QueryString["key"];
            string supplyid = Request.QueryString["supplyid"];
            string SC_SupplyQuerySQL = @"SELECT * FROM dbo.SC_Supply WHERE ID='" + supplyid + "'"; ;
            string SC_PiaoInQuerySQL = @"SELECT SC_PiaoIn.*,SC_ZhangIn.*,SC_Goods.*,SC_Lib.LibName FROM dbo.SC_PiaoIn
                                    INNER JOIN dbo.SC_ZhangIn ON SC_ZhangIn.InNo= SC_PiaoIn.ID
                                    LEFT JOIN dbo.SC_Goods ON SC_Goods.ID=SC_ZhangIn.GoodsID
                                    LEFT JOIN  dbo.SC_Lib ON SC_Lib.ID=SC_PiaoIn.LibID
                                    WHERE SC_PiaoIn.ID IN (" + keys + ")"; ;

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(SC_SupplyQuerySQL, "SC_Supply"));
            QueryList.Add(new ReportQueryItem(SC_PiaoInQuerySQL, "SC_PiaoIn"));

            var re = MyDbReportData.TextFromMultiSQL(QueryList);
            return re;
        }

        /// <summary>
        /// 人员档案信息
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetBase_Personnel(HttpRequest Request) 
        {
            string key = Request.QueryString["key"];
            string Base_PersonnelQuerySQL = @"SELECT HR_Base_Personnel.*,Department.DepartmentName FROM HR_Base_Personnel
                                    LEFT JOIN dbo.Department ON Department.DepartmentID=HR_Base_Personnel.DepartmentID
                                    WHERE HR_Base_Personnel.ID=" + key + ""; ;
            var re = MyDbReportData.TextFromOneSQL(Base_PersonnelQuerySQL);
            return re;
        }
        
        #endregion
    }
}