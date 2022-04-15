using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class GridReportController : ServiceBasedController
    {
 
        public override ActionResult Index()
        {
            //var Report = Request.QueryString["report"];
            //var dataURL = Request.QueryString["data"];
            //if (Report != "")
            //    Report = "/Reports/Z_Grf/" + Report + ".grf";

            //if (dataURL != "")
            //{
            //    dataURL = "/GridReport/PrintData?data=" + dataURL;
            //}

            //int Count = Request.QueryString.Count;
            //for (int i = 0; i < Count; ++i)
            //{
            //    string Key = Request.QueryString.GetKey(i);
            //    if (Key != "report" && Key != "data")
            //    {

            //        dataURL += "&&" + Key + "=" + Request.QueryString[i];

            //    }
            //}
            //ViewBag.Report = Report;
            //ViewBag.dataURL = dataURL;
            return View();
        }


        public string PrintData()
        {
           // HttpContext context
           // string DataText = DataTextPrint.BuildByHttpRequest(context.Request);
          //  return DataText;
            string DataText = "";
            DataText = "{'Table':[{'CustomerID':'HUNGC','CompanyName':'机械','ContactName':'苏先生','ContactTitle':'销售代表','Address':'德昌路甲 29 号','City':'大连','Region':'东北','PostalCode':'564576','Country':'中国','Phone':'(053) 5556874','Fax':'(053) 5552376'}]}";
            return DataText;
        }

        public string PrintSC_PiaoInOrder()
        {
            return "";
        }
     
    }
}
