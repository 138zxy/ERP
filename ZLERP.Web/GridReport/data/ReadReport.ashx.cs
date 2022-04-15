using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZLERP.Web.GridReport.data
{
    /// <summary>
    /// ReadReport 的摘要说明
    /// </summary>
    public class ReadReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strPathFile = context.Server.MapPath("") + @"\GridReport\grf\" + context.Request.QueryString["report"] + ".grf";
            context.Response.WriteFile(strPathFile);
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