using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport.Web;
using System.Data;
using FastReport;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Reports.Produce
{
    public partial class ReportPrint : System.Web.UI.Page
    {
        private string PrimaryID;
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            PrimaryID = Request.QueryString["id"];//获取主键ID
            type = Request.QueryString["type"];//获取模板名
        }
        protected void WebReport_StartReport(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            Report FReport = (sender as WebReport).Report;
           
            FReportLoad(FReport);
        }

        /// 获取fastreport模板的路径
        /// <summary>
        /// 获取fastreport模板的路径
        /// </summary>
        /// <param name="sReportName">模板名称</param>
        /// <returns>返回模板路径</returns>
        public string GetReportsPath(string sReportName)
        {
            return FastReport.Utils.Config.ApplicationFolder + "Reports\\FastReports\\" + sReportName;
        }


        /// Load报表模板
        /// <summary>
        /// Load报表模板
        /// </summary>
        private void FReportLoad(Report FReport)
        {
            DataSet ds = new DataSet();
            string FileName = type + ".frx";
            string sPath = GetReportsPath(FileName);//"ShippingDocumentGH.frx"
            FReport.Load(sPath);
            FReport.RegisterData(ds, GetTableName("TableName"));//ds, "ShippingDocumentGH"
            PrimaryID = "'" + PrimaryID + "'";
            FReport.Report.SetParameterValue(GetTableName("Primary"), PrimaryID);//"ShipDocID", PrimaryID
            
            //将DataSet对象注册到FastReport控件中
            SetParameterValue(FReport);
            FReport.RegisterData(ds);
            //FReport.Prepare();
            //FReport.PrintSettings.ShowDialog = false;
            //FReport.Print();
        }

        /// 获取模板对应表名或主键名
        /// <summary>
        /// 获取模板对应表名或主键名
        /// </summary>
        /// <returns>返回表名或主键名</returns>
        private string GetTableName(string flag)
        {
            string Name = "";
            if (type.ToLower().Contains("shippingdocument"))
            {
                if (flag == "TableName")
                    Name = type;
                else if (flag == "Primary")
                    Name = "ShipDocID";
            }
            else if (type.ToLower().Contains("deliverytype"))
            {
                if (flag == "TableName")
                    Name = "DeliveryBill";
                else if (flag == "Primary")
                    Name = "DeliveryBillID";
            }
            else if (type.ToLower().Contains("stockpactdispatch"))
            {
                if (flag == "TableName")
                    Name = "StockPactDispatch";
                else if (flag == "Primary")
                    Name = "StockPactDispatchID";
            }
            else if (type.ToLower().Contains("stocksaledelivery"))
            {
                if (flag == "TableName")
                    Name = "StockSaleDelivery";
                else if (flag == "Primary")
                    Name = "StockSaleDeliveryID";
            }
            return Name;
        }
        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="FReport"></param>
        private void SetParameterValue(Report FReport) 
        {
            string companyName = "";
            //运输单
            if (type.ToLower().Contains("shippingdocument"))
            {
                ZLERP.Web.Helpers.SqlServerHelper helper = new ZLERP.Web.Helpers.SqlServerHelper();
                string sql = string.Format("SELECT CompName FROM Company WHERE CompanyID IN (SELECT CompanyID FROM DispatchList WHERE shipdocid={0})", PrimaryID);
                System.Data.DataSet ds = helper.ExecuteDataset(sql, System.Data.CommandType.Text, null);
                if (ds != null && ds.Tables.Count > 0 )
                {
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        companyName = ds.Tables[0].Rows[0]["CompName"].ToString();
                    }
                }
                companyName = companyName == "" ? "暂无公司名称" : companyName;
                FReport.Report.SetParameterValue("CompName", companyName);//"ShipDocID", PrimaryID
            }
            //销售发车
            if (type.ToLower().Contains("stocksaledelivery"))
            {
                ZLERP.Web.Helpers.SqlServerHelper helper = new ZLERP.Web.Helpers.SqlServerHelper();
                string sql = string.Format("SELECT StockSaleType FROM StockSaleDelivery WHERE StockSaleDeliveryID ={0}", PrimaryID);
                System.Data.DataSet ds = helper.ExecuteDataset(sql, System.Data.CommandType.Text, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        companyName = ds.Tables[0].Rows[0]["StockSaleType"].ToString();
                    }
                }
                companyName = companyName == "OutsideSale" ? "(外销)" : "(内销)";
                FReport.Report.SetParameterValue("CompName", companyName);//"ShipDocID", PrimaryID
            }
        }
    }
}