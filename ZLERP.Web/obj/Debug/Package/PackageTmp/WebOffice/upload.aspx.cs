using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using ZLERP.Web.Helpers;
using log4net;
using System.Text;
namespace ZLERP.Web
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlServerHelper helper = new SqlServerHelper();
            ILog log = LogManager.GetLogger(typeof(upload));
            string dataType, docTitle, docType, fileurl, saveType, rid;

            Response.Clear();
            dataType = Request.Params["DataType"] == null ? "" : Request.Params["DataType"];
            string id = Request.Params["DocID"] == null ? "" : Request.Params["DocID"];
            docTitle = System.Web.HttpUtility.UrlDecode(Request.Params["DocTitle"], Encoding.UTF8);//解码中文，防止乱码
            docType = Request.Params["DocType"] == null ? "" : Request.Params["DocType"];
            fileurl = Request.Params["fileurl"] == null ? "" : Request.Params["fileurl"];
            saveType = Request.Params["saveType"] == null ? "" : Request.Params["saveType"];
            rid = Request.Params["rid"] == null ? "" : Request.Params["rid"];

            if (docType == "")
                docType = "doc";
            if (Request.Files.Count > 0)
            {
                HttpFileCollection files = Request.Files;
                //没有文件上传，直接返回
                if (files.Count == 0 || files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
                {
                    Response.Write("没有文件");
                }
                else
                {
                    HttpPostedFile file = files[0];
                    if (file.ContentLength > 0)
                    {
                        string filePath = "", filepath0 = "";
                        if (saveType == "tempdate")
                        {
                            filepath0 = "/Content/Files/LabTemplate";
                        }else if (saveType == "labreport")
                        {
                            filepath0 = "/Content/Files/LabReport";
                        }
                        else if (saveType == "labreportCon")
                        {
                            filepath0 = "/Content/Files/labreportCon";
                        }
                        else
                        {
                            filepath0 = "/Content/Files/labTemp";
                        }
                        filePath = Server.MapPath("~" + filepath0);

                        if (!Directory.Exists(filePath))//如果不存
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = file.FileName;
                        string FileEextension = Path.GetExtension(file.FileName);
                        string aLastName = fileName.Substring(fileName.LastIndexOf(".") + 1, (fileName.Length - fileName.LastIndexOf(".") - 1));

                        ////载入xls文档
                        //Workbook workbook = new Workbook();
                        //workbook.LoadFromFile("Input.xls");
                        ////保存为xlsx格式
                        //workbook.SaveToFile("XlsToXlsx.xlsx",ExcelVersion.Version2013);

                        file.SaveAs(Path.Combine(filePath, docTitle + FileEextension.Replace("xlsx", "xls")));
                        string sqlStr;
                        string url = @"" + filepath0 + "/" + docTitle + FileEextension.Replace("xlsx", "xls");

                        if (saveType == "tempdate")//模板文档
                        {
                            sqlStr = string.Format(@"update Lab_Template set LabTemplatePath='{0}',DocType='{1}' where LabTemplateID='{2}'", url, aLastName.Replace("xlsx", "xls"), id);
                            int rows = helper.ExecuteNonQuery(CommandType.Text, sqlStr, null);
                        }
                        if (saveType == "labreport")//试验记录文档
                        {
                            sqlStr = string.Format(@"update Lab_Record set ReportUrl='{0}',DocType='{1}' where ID='{2}'", url, aLastName.Replace("xlsx", "xls"), rid);
                            int rows = helper.ExecuteNonQuery(CommandType.Text, sqlStr, null);
                        }
                        if (saveType == "labreportCon")//混凝土试验记录文档
                        {
                            sqlStr = string.Format(@"update Lab_ConWPRecordItems set ReportUrl='{0}',DocType='{1}' where Lab_ConWPRecordItemsID='{2}'", url, aLastName.Replace("xlsx", "xls"), rid);
                            int rows = helper.ExecuteNonQuery(CommandType.Text, sqlStr, null);
                        }
                    }

                }
                Response.Write("succeed");
                Response.End();
            }
            else
                Response.Write("fail");
        }

    }
}