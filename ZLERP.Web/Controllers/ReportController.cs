using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZLERP.JBZKZ12;
using System.Data;
using Newtonsoft.Json.Linq;

namespace ZLERP.Web.Controllers
{
    public class ReportController : Controller
    {

        public ActionResult Index(string path, string report)
        {
            ViewBag.ReportUrl = string.Format("~/Reports/{0}/{1}.aspx", path, report);
            if (!System.IO.File.Exists(Server.MapPath(ViewBag.ReportUrl)))
            {//避免提示xxx.aspx不存在的问题
                ViewBag.ReportUrl = "javascript:void 0";

            }
            return View();
        }

        public ActionResult Report()
        {
            string currentid = Request.QueryString["f"];
            return View();
        }


        public ActionResult Print(string ShipDocID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                ZLERP.Web.Helpers.SqlServerHelper helper = new ZLERP.Web.Helpers.SqlServerHelper();
                string sql = string.Format("SELECT * FROM view_ShipDocPrint WHERE shipdocid='{0}'", ShipDocID);
                System.Data.DataSet ds = helper.ExecuteDataset(sql, System.Data.CommandType.Text, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                JsonHelper help = new JsonHelper();
                string json = help.Serialize(dt);
                string sss = JsonConvert.SerializeObject(dt.Rows[0], new DataTableConverter());
                return OperateResult(true, json, null);
                //return Json(ds);
            }
            catch (Exception)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
                throw;
            }
        }
        /// <summary>
        /// 操作结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual JsonResult OperateResult(bool result, string messages, object data)
        {
            JsonResult jsonresult = Json(new ResultInfo { Result = result, Message = messages, Data = data }, JsonRequestBehavior.AllowGet);
            return jsonresult;
        }

        [HttpPost]
        public JsonResult GetPlanCube()
        {
            SqlServerHelper helper = new SqlServerHelper();
            string SQL = "SELECT  SUM(PlanCube) PlanCube FROM CustomerPlan WHERE PlanDate>=convert(varchar(10), getdate(), 120)+' 00:00:00' AND PlanDate<convert(varchar(10), getdate (), 120)+' 23:59:59' SELECT SUM(TotalProduceCube) TotalProduceCube FROM ShippingDocument WHERE  TaskID IN  ( SELECT TaskID FROM CustomerPlan  WHERE PlanDate>=convert(varchar(10), getdate(), 120)+' 00:00:00' AND PlanDate<convert(varchar(10), getdate(), 120)+' 23:59:59' )   AND IsEffective=1";
            DataSet ds = helper.ExecuteDataset(SQL, CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] Cube = new string[2];
            if (dt0.Rows.Count > 0)
            {
                string PlanCube = dt0.Rows[0]["PlanCube"].ToString();
                string TotalProduceCube = ds.Tables[1].Rows[0]["TotalProduceCube"].ToString();
                Cube[0] = PlanCube;
                Cube[1] = TotalProduceCube;
            }
            return Json(new
            {
                Cube = Cube
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTotalProduceCube()
        {
            SqlServerHelper helper = new SqlServerHelper();
            string SQL = " SELECT ISNULL(SUM(TotalProduceCube),0.00) TotalProduceCube FROM ShippingDocument WHERE ProduceDate >CONVERT(varchar(7), GETDATE(), 120)+'-01 00:00:00'   AND IsEffective=1  UNION ALL SELECT SUM(TotalProduceCube) TotalProduceCube FROM ShippingDocument WHERE ProduceDate >CONVERT(varchar(4), GETDATE(), 120)+'-01-01 00:00:00' AND ProduceDate <=CONVERT(varchar(7), GETDATE(), 120)+'-01 00:00:00'   AND IsEffective=1 ";
            DataSet ds = helper.ExecuteDataset(SQL, CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] Cube = new string[2];
            if (dt0.Rows.Count > 0)
            {
                string MonthCube = dt0.Rows[0]["TotalProduceCube"].ToString();
                string YearProduceCube = dt0.Rows[1]["TotalProduceCube"].ToString();
                Cube[0] = MonthCube;
                Cube[1] = YearProduceCube;
            }
            return Json(new
            {
                Cube = Cube
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetTop5ConStrength()
        {
            SqlServerHelper helper = new SqlServerHelper();
            string SQL = " SELECT TOP 5 ConStrength,SUM(TotalProduceCube ) Cube FROM ShippingDocument WHERE ProduceDate>CONVERT(varchar(7),dateadd (month,-4,getdate()), 120)+'-01 00:00:00'   AND IsEffective=1 GROUP BY ConStrength ORDER BY Cube DESC  ";
            DataSet ds = helper.ExecuteDataset(SQL, CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] ConStrength = new string[lenght];
            string[] Cube = new string[lenght];
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    ConStrength[i] = dt0.Rows[i]["ConStrength"].ToString();
                    Cube[i] = dt0.Rows[i]["Cube"].ToString();
                }
            }
            return Json(new
            {
                ConStrength = ConStrength,
                Cube = Cube
            }, JsonRequestBehavior.AllowGet);
        }

        public class LineAndCube
        {
            public string ProductLineName { get; set; }
            public string[] Cube { get; set; }
        }
        public class DataJson
        {
            public List<LineAndCube> list { get; set; }
            public List<string> listMonth { get; set; }
            public List<string> listLine { get; set; }
        }
        [HttpPost]
        public JsonResult GetCubeBYLine()
        {
            SqlServerHelper helper = new SqlServerHelper();
            string SQL = "SELECT ProductLineName,CONVERT(varchar(7),ProduceDate, 120) ProduceDate,SUM(TotalProduceCube ) Cube  FROM ShippingDocument WHERE ProduceDate>CONVERT(varchar(7),dateadd (month,-6,getdate()), 120)+'-01 00:00:00' AND IsEffective=1 AND ProductLineName IS NOT NULL GROUP BY ProductLineName,CONVERT(varchar(7),ProduceDate, 120) ORDER BY ProduceDate, ProductLineName  ";
            DataSet ds = helper.ExecuteDataset(SQL, CommandType.Text);
            DataTable dt0 = ds.Tables[0];
            var lenght = dt0.Rows.Count;
            string[] ProductLineName = new string[lenght];
            string[] ProduceDate = new string[lenght];
            List<string> listLine = new List<string>();
            List<string> listMonth = new List<string>();
            DataJson json = new DataJson();
            //获取生产线
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    if (!ProductLineName.Contains(dt0.Rows[i]["ProductLineName"].ToString()))
                    {
                        ProductLineName[i] = dt0.Rows[i]["ProductLineName"].ToString();
                        listLine.Add(dt0.Rows[i]["ProductLineName"].ToString());
                    }
                    if (!ProduceDate.Contains(dt0.Rows[i]["ProduceDate"].ToString()))
                    {
                        ProduceDate[i] = dt0.Rows[i]["ProduceDate"].ToString();
                        listMonth.Add(dt0.Rows[i]["ProduceDate"].ToString());
                    }
                }
            }
            List<LineAndCube> list = new List<LineAndCube>();
            JArray JA_Order = new JArray();
            foreach (var item in listLine)
            {
                LineAndCube LineAndCube = new LineAndCube();
                string[] Cube = new string[listMonth.Count];
                for (int j = 0; j < listMonth.Count; j++)
                {
                    if (dt0.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt0.Rows.Count; i++)
                        {
                            if (dt0.Rows[i]["ProductLineName"].ToString() == item && dt0.Rows[i]["ProduceDate"].ToString() == listMonth[j])
                            {
                                Cube[j] = dt0.Rows[i]["Cube"].ToString();
                                break;
                            }
                            else
                            {
                                Cube[j] = "0";
                            }
                        }
                    }
                }
                LineAndCube.Cube = Cube;
                LineAndCube.ProductLineName = item;
                list.Add(LineAndCube);
            }
            json.list = list;
            json.listLine = listLine;
            json.listMonth = listMonth;
            return Json(json, JsonRequestBehavior.AllowGet);

        }
    }
}
