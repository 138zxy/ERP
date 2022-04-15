using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class LimitRegionController : BaseController<LimitRegion, int>
    {

        //只显示公司的
        [HttpPost]
        public ActionResult GetBoundInfo(bool? isAll)
        {
            // var items = unitOfWork.LimitRegionRepository.Get();
            IList<LimitRegion> items = m_ServiceBase.Query().ToList();
            if (isAll != null && isAll.HasValue)
            {
                // items = items.Where(e => e.IsShow);
                items = items.Where(p => p.IsShow).ToList();
            }
            items = items.Where(p => p.type == 2).ToList();
            JqGridData<LimitRegion> data = new JqGridData<LimitRegion>()
            {
                page = 1,
                records = items.Count,
                pageSize = items.Count,
                rows = items
            };
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetBoundInfoByPid(string id)
        {
            IList<LimitRegion> items = m_ServiceBase.Query().ToList();
            items = items.Where(p => p.IsShow).Where(p => p.ProjectID == id).ToList();
            JqGridData<LimitRegion> data = new JqGridData<LimitRegion>()
            {
                page = 1,
                records = items.Count,
                pageSize = items.Count,
                rows = items
            };
            return Json(data);
        }


        /// <summary>
        /// 设置限制范围
        /// </summary>
        /// <param name="isOutAlarm"></param>
        /// <param name="bounds"></param>
        /// <param name="boundName"></param>
        /// <returns></returns>
        public ActionResult SetBound(int? isOutAlarm, string bounds, string boundName, string ProjectID, string CompanyID)
        {
            int id = 0; int isOutAlarmtemp = 0;
            try
            {
                if (bounds == "" || bounds == "null" || isOutAlarm == null || isOutAlarm.Value < 0)
                {
                    return OperateResult(false, "出现异常", null);
                }
                if (String.IsNullOrEmpty(ProjectID) && String.IsNullOrEmpty(CompanyID))
                {
                    return OperateResult(false, "工地和公司选择一个填写", null);
                }
                //if (  this.m_ServiceBase.Query().ToList().Count() > 4)
                //{
                //    return OperateResult(false, "区域设置失败，最多能设置5个限制区域", null);
                //}
                isOutAlarmtemp = isOutAlarm.HasValue ? isOutAlarm.Value : 0;
                LimitRegion lr = new LimitRegion();
                lr.DotsStr = bounds;
                lr.IsOutAlarm = (isOutAlarmtemp == 1);
                lr.Name = boundName;
                lr.IsShow = true;
                if (ProjectID != "")
                {
                    lr.type = 1;
                    lr.ProjectID = ProjectID;
                }
                else
                {
                    lr.type = 2;
                    lr.CompanyID = CompanyID;
                }

                base.Add(lr);

                id = Convert.ToInt32(lr.ID);
                return OperateResult(true, "新增限制区域 " + boundName + "[" + id + "] 成功", null);
            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }


        }
       
    }
}
