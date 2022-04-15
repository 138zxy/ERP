using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class Lab_ConWPRecordController : BaseController<Lab_ConWPRecord, string>
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
        public ActionResult AddItems(Lab_ConWPRecord entity)
        {
            Lab_ConWPRecordItems _Lab_ConWPRecordItems = entity.Lab_ConWPRecordItems;
            ActionResult result = null;
            if (_Lab_ConWPRecordItems!=null)
            {
                List<Dic> list = this.service.GetGenericService<Dic>().All("", "ID", true).Where(s => s.ParentID == "LabTemplateType" && s.Field2 == "2" && s.DicName == _Lab_ConWPRecordItems.ReportType).ToList();
                if (list==null||list.Count==0)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, null);
                }
                string ReportType = list[0].TreeCode;
                List<Lab_ConWPRecordItems> items = this.service.GetGenericService<Lab_ConWPRecordItems>().All("", "Lab_ConWPRecordId", true).Where(s => s.Lab_ConWPRecordId == _Lab_ConWPRecordItems.Lab_ConWPRecordId && s.ReportType == ReportType).ToList();
                if (items != null && items.Count > 0)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, null); 
                }
                _Lab_ConWPRecordItems.ReportType = ReportType;
                var re = this.service.GetGenericService<Lab_ConWPRecordItems>().Add(_Lab_ConWPRecordItems);
                return result = OperateResult(true, Lang.Msg_Operate_Success, re.GetId());
            }
            return OperateResult(false, Lang.Msg_Operate_Failed, null); 
        }
    }
}
