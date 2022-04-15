using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class Lab_ConWPRecordGHController : BaseController<Lab_ConWPRecordGH, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            //base.InitCommonViewBag();
            //依据列表
            //var dlist = this.service.GetGenericService<Lab_DependInfo>().All("", "Name", true).Where(s => s.Lifecycle == 0).ToList();
            //ViewBag.DependInfoList = new SelectList(dlist, "ID", "Name");

            return base.Index();
        }

        [HttpPost]
        public ActionResult AddItems(Lab_ConWPRecordGH entity)
        {
            Lab_ConWPRecordItemsGH _Lab_ConWPRecordItemsGH = entity.Lab_ConWPRecordItemsGH;
            ActionResult result = null;
            if (_Lab_ConWPRecordItemsGH != null)
            {
                List<Dic> list = this.service.GetGenericService<Dic>().All("", "ID", true).Where(s => s.ParentID == "LabTemplateType" && s.Field2 == "3" && s.DicName == _Lab_ConWPRecordItemsGH.ReportType).ToList();
                if (list == null || list.Count == 0)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, null);
                }
                string ReportType = list[0].TreeCode;
                List<Lab_ConWPRecordItemsGH> items = this.service.GetGenericService<Lab_ConWPRecordItemsGH>().All("", "Lab_ConWPRecordGHId", true).Where(s => s.Lab_ConWPRecordGHId == _Lab_ConWPRecordItemsGH.Lab_ConWPRecordGHId && s.ReportType == ReportType).ToList();
                if (items != null && items.Count > 0)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, null);
                }
                _Lab_ConWPRecordItemsGH.ReportType = ReportType;
                var re = this.service.GetGenericService<Lab_ConWPRecordItemsGH>().Add(_Lab_ConWPRecordItemsGH);
                return result = OperateResult(true, Lang.Msg_Operate_Success, re.GetId());
            }
            return OperateResult(false, Lang.Msg_Operate_Failed, null);
        }
    }
}
