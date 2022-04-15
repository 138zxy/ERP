using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Business.HR;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_PaiBanController : HRBaseController<HR_KQ_PaiBan, string>
    {
        public override ActionResult Index()
        {
            ViewBag.Arrange = GetArrangeList();
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "星期一", Value = "1" });
            Items.Add(new SelectListItem { Text = "星期二", Value = "2" });
            Items.Add(new SelectListItem { Text = "星期三", Value = "3" });
            Items.Add(new SelectListItem { Text = "星期四", Value = "4" });
            Items.Add(new SelectListItem { Text = "星期五", Value = "5" });
            Items.Add(new SelectListItem { Text = "星期六", Value = "6" });
            Items.Add(new SelectListItem { Text = "星期天", Value = "0" });
            ViewBag.StartWeek = Items;
            return base.Index();
        }

        public override ActionResult Add(HR_KQ_PaiBan entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.PersonID == entity.PersonID && t.WorkDate == entity.WorkDate).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "已存在当前员工当前工作日的排班，请确认", null);
            }
            return base.Add(entity);
        }

        public override ActionResult Update(HR_KQ_PaiBan entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != entity.ID && t.PersonID == entity.PersonID && t.WorkDate == entity.WorkDate).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "已存在当前员工当前工作日的排班，请确认", null);
            }
            return base.Update(entity);
        }

        public ActionResult GetAcitonArrage(string id, string workdate)
        {
            DateTime dt;
            if (DateTime.TryParse(workdate, out dt))
            {
                var Arrage = this.service.GetGenericService<HR_KQ_Arrange>().Get(id);
                var PaiBan = new HR_KQ_PaiBan();
                PaiBan.WordStartTime = Convert.ToDateTime(dt.ToString("yyyy-MM-dd") + " " + Arrage.WordStartTime);
                if (Arrage.IsInnerDay)
                {
                    PaiBan.WordEndTime = Convert.ToDateTime(dt.AddDays(1).ToString("yyyy-MM-dd") + " " + Arrage.WordEndTime);
                }
                else
                {
                    PaiBan.WordEndTime = Convert.ToDateTime(dt.ToString("yyyy-MM-dd") + " " + Arrage.WordEndTime);
                }
                PaiBan.UpStartTime = PaiBan.WordStartTime.AddHours(-Arrage.UpInterval);

                PaiBan.DwonEndTime = PaiBan.WordEndTime.AddHours(Arrage.DownInterval);

                PaiBan.IsInnerDay = Arrage.IsInnerDay;
                PaiBan.AutoUp = Arrage.AutoUp;
                PaiBan.AutoDown = Arrage.AutoDown;
                PaiBan.Remark = Arrage.Remark;
                return OperateResult(true, "操作成功", new
                {
                    WordStartTime = PaiBan.WordStartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    WordEndTime = PaiBan.WordEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpStartTime = PaiBan.UpStartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    DwonEndTime = PaiBan.DwonEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsInnerDay = PaiBan.IsInnerDay,
                    AutoUp = PaiBan.AutoUp,
                    AutoDown = PaiBan.AutoDown,
                    Remark = PaiBan.Remark
                });
            }
            else
            {
                return OperateResult(false, "参数有误", null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsSelectDepart"></param>
        /// <param name="departmentid"></param>
        /// <param name="Pesonid"></param>
        /// <param name="IsCancel"></param>
        /// <param name="StartPai"></param>
        /// <param name="EndPai"></param>
        /// <param name="Monday"></param>
        /// <param name="Tuesday"></param>
        /// <param name="Wednesday"></param>
        /// <param name="Thursday"></param>
        /// <param name="Friday"></param>
        /// <param name="Saturday"></param>
        /// <param name="Sunday"></param>
        /// <param name="IsWeekAlternate"></param>
        /// <param name="IsDayAlternate"></param>
        /// <param name="ArraggeShowID"></param>
        /// <param name="StartWeek"></param>
        /// <param name="AlternateDay"></param>
        /// <returns></returns>
        public ActionResult BathPaiBan(bool IsSelectDepart, List<int> departmentid, List<string> Pesonid,
            bool IsCancel, string StartPai, string EndPai, bool Monday, bool Tuesday, bool Wednesday,
           bool Thursday, bool Friday, bool Saturday, bool Sunday, bool IsWeekAlternate, bool IsDayAlternate,
            int ArraggeShowID = 0, int StartWeek = -1, int AlternateDay = 0)
        {
            DateTime dt1;
            DateTime dt2;
            if (!DateTime.TryParse(StartPai, out dt1) || !DateTime.TryParse(EndPai, out dt2))
            {
                return OperateResult(false, "请输入正确的起止时间", null);
            }
            if (!IsCancel && ArraggeShowID <= 0)
            {
                return OperateResult(false, "请选择班次", null);
            }
            if (IsSelectDepart)
            {
                if (departmentid == null || departmentid.Count <= 0)
                {
                    return OperateResult(false, "请选择部门", null);
                }
            }
            if (IsWeekAlternate)
            {
                if (StartWeek < 0)
                {
                    return OperateResult(false, "请选择开始星期", null);
                }
            }
            if (IsDayAlternate)
            {
                if (AlternateDay <= 0)
                {
                    return OperateResult(false, "请输入隔天天数", null);
                }
            }
            var result = this.service.HRService.BathPaiBan(IsSelectDepart, departmentid, Pesonid, IsCancel,
                dt1, dt2, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, IsWeekAlternate,
                IsDayAlternate, ArraggeShowID, StartWeek, AlternateDay);
            return Json(result);
        }
    }
}
