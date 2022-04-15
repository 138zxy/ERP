using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model;
using ZLERP.Model.SystemManage;
 
namespace ZLERP.Web.Controllers.SystemManage
{
    public class PrintReportController : BaseController<PrintReport, string>
    {


        public override ActionResult Index()
        {
            List<SelectListItem> SoucreType = new List<SelectListItem>();
            SoucreType.Add(new SelectListItem { Text = "SQL语句", Value = "SQL语句" });
            SoucreType.Add(new SelectListItem { Text = "存储过程", Value = "存储过程" });
            ViewBag.SoucreType = SoucreType;

            List<SelectListItem> ReportType = new List<SelectListItem>();
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report1, Value = ST_Common.ReportType.Report1 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report2, Value = ST_Common.ReportType.Report2 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report3, Value = ST_Common.ReportType.Report3 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report4, Value = ST_Common.ReportType.Report4 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report5, Value = ST_Common.ReportType.Report5 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report6, Value = ST_Common.ReportType.Report6 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report7, Value = ST_Common.ReportType.Report7 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report8, Value = ST_Common.ReportType.Report8 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report9, Value = ST_Common.ReportType.Report9 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report10, Value = ST_Common.ReportType.Report10 });
            ReportType.Add(new SelectListItem { Text = ST_Common.ReportType.Report11, Value = ST_Common.ReportType.Report11 });
            ViewBag.ReportType = ReportType;


            return base.Index();
        }

        public override ActionResult Add(PrintReport entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != entity.ID && t.ReportNo == entity.ReportNo).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "报表编号不能重复", null);
            }
            if (entity.SqlData.ToUpper().Contains("UPDATE") || entity.SqlData.ToUpper().Contains("DELETE") || entity.SqlData.ToUpper().Contains("INSERT") || entity.SqlData.ToUpper().Contains("CREATE") || entity.SqlData.ToUpper().Contains("DROP"))
            {
                return OperateResult(false, "语句中包含敏感关键字", null);
            }
            entity.FilePath = "/GridReport/grf/" + entity.ReportNo + ".grf";
            return base.Add(entity);
        }

        public override ActionResult Update(PrintReport entity)
        {
            if (entity.SqlData.ToUpper().Contains("UPDATE") || entity.SqlData.ToUpper().Contains("DELETE") || entity.SqlData.ToUpper().Contains("INSERT") || entity.SqlData.ToUpper().Contains("CREATE") || entity.SqlData.ToUpper().Contains("DROP"))
            {
                return OperateResult(false, "语句中包含敏感关键字", null);
            }
            return base.Update(entity);
        }
        /// <summary>
        /// 复制一个样式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Copy(string id)
        {
            var query = this.m_ServiceBase.Get(id);

            var maxID = this.m_ServiceBase.Query().Max(t => t.ID);
            int NowID = Convert.ToInt32(maxID) + 1;
            PrintReport report = new PrintReport();
            report.ReportNo = query.ReportNo + "_" + NowID.ToString();
            report.ReportName = query.ReportName;
            report.ReportType = query.ReportType;
            report.SoucreType = query.SoucreType;
            report.SqlData = query.SqlData;
            report.FilePath = "/GridReport/grf/" + report.ReportNo + ".grf";
            report.Remark = query.Remark;
            this.m_ServiceBase.Add(report);

            string strPathFile = Server.MapPath("") + @"\GridReport\grf\" + query.ReportNo + ".grf";
            string strPathFile2 = Server.MapPath("") + @"\GridReport\grf\" + report.ReportNo + ".grf";
            System.IO.File.Copy(strPathFile, strPathFile2);

            return OperateResult(true, "复制成功", null);
        }

        /// <summary>
        /// 直接上传样式报表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Upload(string id)
        {
            var query = this.m_ServiceBase.Get(id);
            string strPathFile = Server.MapPath("") + @"\GridReport\grf\" + query.ReportNo + ".grf";
            string fileName = System.IO.Path.GetExtension(Request.Files[0].FileName);
            if (fileName != ".grf")
            {
                return OperateResult(false, "只能上传grf格式的报表样式", null);
            }
            query.FilePath = "/GridReport/grf/" + query.ReportNo + ".grf";
            this.m_ServiceBase.Update(query, null);
            Request.Files[0].SaveAs(strPathFile); 
            return Content(
                  ZLERP.Web.Helpers.HelperExtensions.ToJson(
                      OperateResult(true, "上传成功", null).Data
                  )
              );
        }
 
    }
}
