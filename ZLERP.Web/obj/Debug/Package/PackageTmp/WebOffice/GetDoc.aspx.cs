using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using ZLERP.Web.Helpers;
using System.Net;
using System.Text;
using NPOI.HSSF.UserModel;
namespace ZLERP.Web
{
    public partial class GetDoc : System.Web.UI.Page
    {
        string filepathtemp;
        SqlServerHelper helper;
        protected void Page_Load(object sender, EventArgs e)
        {
            filepathtemp = "/Content/Files/LabReport/Temp/";
            string id = "", rid = "";
            string fileUrl = "";
            string sqlStr;
            string dataType, saveType;
            string rurl = "";
            helper = new SqlServerHelper();
            //---------------------------------------------------
            Response.Clear();
            id = Request["id"].ToString();
            rid = Request["rid"].ToString();
            dataType = Request["dataType"].ToString();
            saveType = Request["saveType"].ToString();
            //模板
            if (dataType == "tempdate")
            {
                sqlStr = string.Format(@"select top 1 * from Lab_Template where LabTemplateID='{0}'", id);
                DataTable dt = helper.ExecuteDataset(sqlStr, CommandType.Text, null).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rurl = dt.Rows[0]["LabTemplatePath"].ToString();
                    if (string.IsNullOrEmpty(rurl))
                    {
                        Response.End();
                        return;
                    }
                    string filePath = Server.MapPath("~" + rurl);
                    if (File.Exists(filePath))//判断文件是否存在
                    {
                        if (saveType == "labreport" || saveType == "labreportCon")//调用模板生成记录报告
                        {
                            string burl = CreateTempExcel(rid, dt.Rows[0]["LabTemplateID"].ToString(), rurl, saveType);
                            if (burl == "")//没有配置插入数据
                            {
                                fileUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + rurl;
                            }
                            else
                            {
                                fileUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + burl;
                            }

                        }
                        else//打开模板
                        {
                            fileUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + rurl;
                        }
                    }
                    else
                    {
                        //中文出现乱码是因为使用了Encoding.UTF8等字符编码，使用Encoding.GetEncoding("GB2312")即可解决乱码问题
                        Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                        Response.Write("没有找到对应文件！");
                        Response.End();
                    }
                }
            }
            //试验报告
            if (dataType == "labreport")
            {
                sqlStr = string.Format(@"select top 1 * from Lab_Record where ID='{0}'", rid);
                DataTable dt = helper.ExecuteDataset(sqlStr, CommandType.Text, null).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rurl = dt.Rows[0]["reporturl"].ToString();
                    if (string.IsNullOrEmpty(rurl))
                    {
                        Response.End();
                        return;
                    }
                    string filePath = Server.MapPath("~" + rurl);
                    if (File.Exists(filePath))//判断文件是否存在
                    {
                        fileUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + rurl;
                    }
                    else
                    {
                        Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                        Response.Write("没有找到对应文件！");
                        Response.End();
                    }
                }
            }
            //混凝土试验报告
            if (dataType == "labreportCon")
            {
                sqlStr = string.Format(@"select top 1 * from Lab_ConWPRecordItems where Lab_ConWPRecordItemsID='{0}'", rid);
                DataTable dt = helper.ExecuteDataset(sqlStr, CommandType.Text, null).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rurl = dt.Rows[0]["ReportUrl"].ToString();
                    if (string.IsNullOrEmpty(rurl))
                    {
                        Response.End();
                        return;
                    }
                    string filePath = Server.MapPath("~" + rurl);
                    if (File.Exists(filePath))//判断文件是否存在
                    {
                        fileUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + rurl;
                    }
                    else
                    {
                        Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                        Response.Write("没有找到对应文件！");
                        Response.End();
                    }
                }
            }
            WebClient client = new WebClient();
            if (fileUrl != "")
            {
                byte[] pageData = client.DownloadData(fileUrl);
                Response.BinaryWrite(pageData);
            }
            DelectDir(Server.MapPath("~" + filepathtemp));
            Response.End();

        }
        /// <summary>
        /// Excel
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <param name="modelExlPath">Excel模版</param>
        private string CreateTempExcel(string reportID, string templateID, string modelExlPath, string saveType)
        {
            string filePath = "";
            try
            {
                string filepath0 = "~" + filepathtemp;
                if (!Directory.Exists(Server.MapPath(filepath0)))
                {
                    Directory.CreateDirectory(Server.MapPath(filepath0));
                }
                string filename = filepath0 + templateID + ".xls";
                //临时存放路径
                filePath = Server.MapPath(filename);
                HSSFWorkbook hssfworkbookDown = null;   //创建一个excel对象
                //读入刚复制的要导出的excel文件
                string templatepath = Server.MapPath("~" + modelExlPath);
                using (FileStream file = new FileStream(templatepath, FileMode.Open, FileAccess.Read))  //路径，打开权限，读取权限
                {
                    if (modelExlPath.IndexOf(".xlsx") > 0) // 2007版本
                    {
                        //hssfworkbookDown = new XSSFWorkbook(file);
                    }
                    else if (modelExlPath.IndexOf(".xls") > 0) // 2003版本
                    {
                        hssfworkbookDown = new HSSFWorkbook(file);
                    }
                    file.Close();
                }

                //模版的一个页面在GetSheetAt方法中，这里取第一个页面是0
                HSSFSheet sheet1 = (HSSFSheet)hssfworkbookDown.GetSheetAt(0);
                //开始向excel表格中写入数据
                //表页、行和列都是从0开始编号
                //修改单元格，这里是第2行第5列
                string strSql = @"SELECT * FROM Lab_TemplateDataConfig WHERE LabTemplateID='" + templateID + "'";
                DataTable dt = helper.ExecuteDataset(strSql, CommandType.Text, null).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    return "";
                }
                NPOI.SS.UserModel.ICell cell = null;
                NPOI.SS.UserModel.IRow rowHSSF = null;
                int row, column;
                string fieldvalue;
                foreach (DataRow drow in dt.Rows)
                {
                    row = Convert.ToInt32(drow["ExcelRow"]);//行
                    column = Convert.ToInt32(drow["ExcelColumun"]);//列
                    cell = (HSSFCell)sheet1.GetRow(row).GetCell(column);
                    //if (cell != null)
                    //{
                    if (saveType == "labreport")
                    {
                        strSql = @"SELECT top 1 (" + drow["Field"] + ") as field FROM (SELECT a.*,b.StuffName,c.SupplyName FROM Lab_Record a LEFT JOIN StuffInfo b ON a.stuffid=b.StuffID LEFT JOIN SupplyInfo c ON a.Supplyid=c.SupplyID) a   WHERE ID=" + reportID + "";
                    }
                    if (saveType == "labreportCon")
                    {

                        strSql = @"SELECT top 1 (" + drow["Field"] + ") as field FROM Lab_ConWPRecord a   WHERE Lab_ConWPRecordId in(SELECT Lab_ConWPRecordId FROM Lab_ConWPRecordItems WHERE Lab_ConWPRecordItemsID=" + reportID + ")";
                    }
                    fieldvalue = helper.ExecuteDataset(strSql, CommandType.Text, null).Tables[0].Rows[0]["field"].ToString();

                    if (sheet1.GetRow(row) == null)
                    {
                        rowHSSF = sheet1.CreateRow(row);
                    }
                    else
                        rowHSSF = sheet1.GetRow(row);

                    if (rowHSSF.GetCell(column) == null)
                        cell = rowHSSF.CreateCell(column);
                    else
                        cell = rowHSSF.GetCell(column);

                    cell.SetCellValue(fieldvalue);
                    //}
                }

                //创建文件
                FileStream files = new FileStream(filePath, FileMode.Create);
                hssfworkbookDown.Write(files);
                files.Close();
                return filepathtemp + templateID + ".xls";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        /// <summary>
        /// 把一个文件夹下所有文件删除
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                if (!Directory.Exists(srcPath))//如果不存在
                {
                    Directory.CreateDirectory(srcPath);
                }
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();//返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)//判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);//删除子目录和文件
                    }
                    else
                    {
                        System.IO.File.Delete(i.FullName);//删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}