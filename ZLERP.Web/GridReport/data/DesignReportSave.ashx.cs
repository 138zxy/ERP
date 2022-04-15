using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ZLERP.Web.GridReport.data
{
    /// <summary>
    /// DesignReportSave 的摘要说明
    /// </summary>
    public class DesignReportSave : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] FormData = context.Request.BinaryRead(context.Request.TotalBytes);

            //写入上传数据，最后保存到文件
            //string strPathFile = context.Server.MapPath("") + @"\..\grf\" + context.Request.QueryString["report"] + ".grf";
            string strPathFile = context.Server.MapPath("/") + @"GridReport/grf/" + context.Request.QueryString["report"] + ".grf";
            BinaryWriter bw = new BinaryWriter(File.Create(strPathFile));
            bw.Write(FormData);
            bw.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}