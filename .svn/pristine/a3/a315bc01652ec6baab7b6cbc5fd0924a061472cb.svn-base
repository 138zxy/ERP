using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_Base_RewardsController : HRBaseController<HR_Base_Rewards, int?>
    {
        public override ActionResult Index()
        {
            ViewBag.RewardsTypeList = GetBaseData(BaseDataType.奖惩处理类别);
            ViewBag.RewardsModeList = GetBaseData(BaseDataType.奖惩方式);
            return base.Index();
        }
        int? _NoticeID;
        public override ActionResult Add(HR_Base_Rewards entity)
        {
            //如果需要发公告通知，则先发通知公告再保存奖惩信息
            
            if (entity.IsNotice && !string.IsNullOrEmpty((_NoticeID = AddNotice(entity)).ToString()))
            {
                entity.IsNotice = true;
                entity.NoticeID = _NoticeID;
            }
            entity.IsEffective = true;
            entity.ExcuteMan = AuthorizationService.CurrentUserName;
            return base.Add(entity);
        }

        public override ActionResult Update(HR_Base_Rewards entity)
        {
            //如果需要发公告通知，则先发通知公告再保存奖惩信息
            if (entity.IsNotice && !string.IsNullOrEmpty((_NoticeID = AddNotice(entity)).ToString()))
            {
                entity.IsNotice = true;
                entity.NoticeID = _NoticeID;
            }
            entity.ExcuteMan = AuthorizationService.CurrentUserName;
            return base.Update(entity);
        }

        private int? AddNotice(dynamic entity)
        {
            Notice notice = new Notice();
            notice.Title = entity.RewardsType;
            notice.IsTop = true;
            string Dep = this.service.User.Get(entity.UserID).Department.DepartmentName;
            notice.Content = Dep + " " + entity.UserID + "，" + entity.RewardsReason + "。鉴此，公司做如下处理：" + entity.ProcessingResult;
            notice.Builder = AuthorizationService.CurrentUserName;
            notice.BuildTime = DateTime.Now;
            return this.service.GetGenericService<Notice>().Add(notice).ID;
        }

        public ActionResult Revocation(HR_Base_Rewards entity)
        {
            return base.Update(entity);
        }
    }    
}
