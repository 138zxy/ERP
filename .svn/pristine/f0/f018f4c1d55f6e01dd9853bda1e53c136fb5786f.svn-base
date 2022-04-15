using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class Lab_ReportTypeConfigController : BaseController<Lab_ReportTypeConfig, int?>
    {
        /// <summary>
        /// 试验报告管理
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            var parentType = this.service.GetGenericService<StuffType>().All("", "ID", true);
            ViewBag.StuffType = HelperExtensions.SelectListData<StuffType>("StuffTypeName", "ID", " IsUsed=1 AND (TypeID='StuffType' OR TypeID='Oil' OR TypeID='Other' OR TypeID='Oil1')", "OrderNum", true, "");
            ViewBag.StuffTypeDics = new SelectList(parentType, "ID", "StuffTypeName");

            ViewBag.LabReportType = HelperExtensions.SelectListData<Dic>("DicName", "ID", " ParentID='LabTemplateType' AND Field2=1", "OrderNum", true, "");

            return base.Index();
        }
    }
}
