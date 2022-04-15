using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_ArrangeController : HRBaseController<HR_KQ_Arrange, string>
    {
        public override ActionResult Index()
        {

            return base.Index();
        }

        public override ActionResult Add(HR_KQ_Arrange entity)
        {
            string re = checkDt(entity);
            if (!string.IsNullOrWhiteSpace(re))
            {
                return OperateResult(false, re, null);
            }
            return base.Add(entity);
        }

        public override ActionResult Update(HR_KQ_Arrange entity)
        {
            string re = checkDt(entity);
            if (!string.IsNullOrWhiteSpace(re))
            {
                return OperateResult(false, re, null);
            }
            return base.Update(entity);
        }

        /// <summary>
        /// 检查数据的准确性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string checkDt(HR_KQ_Arrange entity)
        {

            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            string startdt = dt + " " + entity.WordStartTime;
            string enddt = dt + " " + entity.WordEndTime;
            DateTime starttime;
            DateTime endtime;
            if (DateTime.TryParse(startdt, out starttime) && DateTime.TryParse(enddt, out endtime))
            {
                if (!entity.IsInnerDay)
                {
                    if (starttime >= endtime)
                    {
                        return "上班时间不能大于下班时间";
                    }
                }
                else
                {
                    endtime = endtime.AddDays(1);
                    if (starttime >= endtime)
                    {
                        return "上班时间不能大于下班时间(跨天)";
                    }
                }

                starttime = starttime.AddHours(-entity.UpInterval);
                endtime = endtime.AddHours(entity.DownInterval);
                var h = (endtime - starttime).Days * 24 + (endtime - starttime).Hours;
                if (h >= 24)
                {
                    return "签到时间到签退时间总区间不能超过一天，否则无法统计考勤";
                }
            }
            else
            {
                return "时间格式错误";
            }

            return string.Empty;
        }
    }
}
