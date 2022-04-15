using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;  
using NPOI.HPSF; 
using NPOI.HSSF.Util;
using System.IO;

namespace ZLERP.Web.Helpers
{
    public class ExcelExportHelper
    {
        /// <summary>
        /// 将单个数据表保存到Excel
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="fileName">文件名</param>
        public static void ExportExcel(DataTable dataTable, string fileName = null)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            ISheet sheet = workBook.CreateSheet("Sheet1");

            IFont font = workBook.CreateFont();
            font.Boldweight = 700;
            ICellStyle style = workBook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.SetFont(font);

            int rownum = 0;
            IRow row = sheet.CreateRow(rownum++);
            ICell cell;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(dataTable.Columns[i].ColumnName);
                cell.CellStyle = style;
            }

            ICellStyle dateStyle = workBook.CreateCellStyle();
            IDataFormat format = workBook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                row = sheet.CreateRow(rownum++);
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    cell = row.CreateCell(i);
                    string strValue = dataRow[i].ToString();
                    if (string.IsNullOrWhiteSpace(strValue))
                        cell.SetCellValue("");
                    else
                    {
                        switch (dataTable.Columns[i].DataType.ToString())
                        {
                            case "System.DateTime":
                                DateTime dateTime;
                                DateTime.TryParse(strValue, out dateTime);
                                cell.SetCellValue(dateTime);
                                cell.CellStyle = dateStyle;
                                break;
                            case "System.Boolean":
                                bool bValue;
                                bool.TryParse(strValue, out bValue);
                                cell.SetCellValue(bValue);
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int iValue = 0;
                                int.TryParse(strValue, out iValue);
                                cell.SetCellValue(iValue);
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                double dValue = 0;
                                double.TryParse(strValue, out dValue);
                                cell.SetCellValue(dValue);
                                break;
                            default:
                                cell.SetCellValue(strValue);
                                break;
                        }
                    }
                }
            }
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            workBook.Write(HttpContext.Current.Response.OutputStream);
           
           
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + GetToExcelName(fileName) + ".xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 将数据集多个表保存到Excel
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="fileName">文件名</param>
        public static void ExportExcel(DataSet dataSet, string fileName = null)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            int tableCount = 1;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                ISheet sheet = workBook.CreateSheet("Sheet" + tableCount++);

                IFont font = workBook.CreateFont();
                font.Boldweight = 700;
                ICellStyle style = workBook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.CENTER;
                style.SetFont(font);

                int rownum = 0;
                IRow row = sheet.CreateRow(rownum++);
                ICell cell;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    cell = row.CreateCell(i);
                    cell.SetCellValue(dataTable.Columns[i].ColumnName);
                    cell.CellStyle = style;
                }

                ICellStyle dateStyle = workBook.CreateCellStyle();
                IDataFormat format = workBook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    row = sheet.CreateRow(rownum++);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        cell = row.CreateCell(i);
                        string strValue = dataRow[i].ToString();
                        if (string.IsNullOrWhiteSpace(strValue))
                            cell.SetCellValue("");
                        else
                        {
                            switch (dataTable.Columns[i].DataType.ToString())
                            {
                                case "System.DateTime":
                                    DateTime dateTime;
                                    DateTime.TryParse(strValue, out dateTime);
                                    cell.SetCellValue(dateTime);
                                    cell.CellStyle = dateStyle;
                                    break;
                                case "System.Boolean":
                                    bool bValue;
                                    bool.TryParse(strValue, out bValue);
                                    cell.SetCellValue(bValue);
                                    break;
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int iValue = 0;
                                    int.TryParse(strValue, out iValue);
                                    cell.SetCellValue(iValue);
                                    break;
                                case "System.Decimal":
                                case "System.Double":
                                    double dValue = 0;
                                    double.TryParse(strValue, out dValue);
                                    cell.SetCellValue(dValue);
                                    break;
                                default:
                                    cell.SetCellValue(strValue);
                                    break;
                            }
                        }
                    }
                }
                //for (int i = 0; i < dataTable.Columns.Count; i++)
                //{
                //    sheet.AutoSizeColumn(i);
                //}
            }

            workBook.Write(HttpContext.Current.Response.OutputStream);
           // workBook.Dispose();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + GetToExcelName(fileName) + ".xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 转换中文excel名称，防止乱码
        /// </summary>
        /// <param name="fileName">中文名称</param>
        /// <returns></returns>
        public static string GetToExcelName(string fileName)
        {
            string browser = HttpContext.Current.Request.Browser.Browser.ToLower();

            if (browser.IndexOf("firefox") == -1)
            {
                fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            }

            return fileName;
        }
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                            switch (cell.CellType)
                                            {
                                                case CellType.BLANK:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.NUMERIC:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.STRING:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }


        public static bool DataTableToExcel(DataTable dt, string filePath)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(filePath))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }
        }  
    }
}