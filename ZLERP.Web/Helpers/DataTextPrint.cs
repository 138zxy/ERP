using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using ZLERP.Web.Controllers;

namespace ZLERP.Web.Helpers
{
    public class DataTextPrint
    {
        private static DataTextPrint initStartup = new DataTextPrint();

        public DataTextPrint()
        {
            DataTextPrint.InitDataFunMap();
        }
        
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

            Trace.Assert(SimpleDataFunMap.Count > 0, "DataFunMap isn't initialized!");

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
                DataText= "";
            }
            return DataText;
        }
        public static void InitDataFunMap()
        {
            Trace.Assert(SimpleDataFunMap == null && SpecialDataFunMap == null, "DataFunMap already initialized!");
            SimpleDataFunMap = new Dictionary<string, SimpleDataFun>();
            SpecialDataFunMap = new Dictionary<string, SpecialDataFun>();

            SimpleDataFunMap.Add("SC_PiaoInOrder", new GridReportController().PrintSC_PiaoInOrder);
        }

      
    }
}