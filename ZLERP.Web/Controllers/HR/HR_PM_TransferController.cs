﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_PM_TransferController : HRBaseController<HR_PM_Transfer, int?>
    {
        public override ActionResult Index()
        {
            ViewBag.PositionTypeList = GetBaseData(BaseDataType.职务);
            ViewBag.PostList = GetBaseData(BaseDataType.岗位);
            return base.Index();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(HR_PM_Transfer entity)
        {
            entity.State = RecordState.Draft;
            return base.Add(entity);
        }

        /// <summary>
        /// 状态变化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeState(int type, int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return OperateResult(false, "请选择相应的单据", null);
            }
            int status = 0;
            switch (type)
            {
                case 1: //审核
                    if (true)
                    {
                        var qj = this.m_ServiceBase.Get(id);
                        if (qj.State != RecordState.Draft)
                        {
                            return OperateResult(false, "该单据不是草稿状态不能审核", null);
                        }
                        qj.State = PCondition.Audit;
                        qj.Auditor = AuthorizationService.CurrentUserID;
                        qj.AuditTime = DateTime.Now;
                        this.m_ServiceBase.Update(qj, null);
                        status = 0;

                        //修改人员调动信息
                        HR_Base_Personnel personmodel = this.service.GetGenericService<HR_Base_Personnel>().Get(Convert.ToInt32(qj.PersonID));
                        if (personmodel != null)
                        {
                            if(qj.IsDept)//部门
                            {
                                if (qj.NewDepartmentID.ToString() != "" && qj.NewDepartmentID.ToString() != "0")
                                {
                                    personmodel.DepartmentID = qj.NewDepartmentID;
                                }
                            }
                            if (qj.IsPosition)//职务
                            {
                                if (qj.NewPositionID!=null&&qj.NewPositionID.ToString() != "" && qj.NewPositionID.ToString() != "0")
                                {
                                    personmodel.PositionType = qj.NewPositionID;
                                }
                            }
                            if (qj.IsPost)//岗位
                            {
                                if (qj.NewPostID != null && qj.NewPostID.ToString() != "" && qj.NewPostID.ToString() != "0")
                                {
                                    personmodel.Post = qj.NewPostID;
                                }
                            }
                            this.service.GetGenericService<HR_Base_Personnel>().Update(personmodel, null);
                        }
                    }
                    break;
                case 2: //反审
                    if (true)
                    {
                        var qj = this.m_ServiceBase.Get(id);
                        if (qj.State != RecordState.Audit)
                        {
                            return OperateResult(false, "该单据不是已审核状态不能反审", null);
                        }
                        qj.State = RecordState.Draft;
                        qj.Auditor = "";
                        qj.AuditTime = null;
                        this.m_ServiceBase.Update(qj, null);
                        status = 0;

                        //人员信息-恢复调动前信息
                        HR_Base_Personnel personmodel = this.service.GetGenericService<HR_Base_Personnel>().Get(Convert.ToInt32(qj.PersonID));
                        if (personmodel != null)
                        {
                            if (qj.IsDept)//部门
                            {
                                personmodel.DepartmentID = qj.OldDepartmentID;
                            }
                            if (qj.IsPosition)//职务
                            {
                                personmodel.PositionType = qj.OldPositionID;
                            }
                            if (qj.IsPost)//岗位
                            {
                                personmodel.Post = qj.OldPostID;
                            }
                            this.service.GetGenericService<HR_Base_Personnel>().Update(personmodel, null);
                        }
                    }
                    break;
                default:
                    status = 1;
                    break;

            }
            if (status == 0)
            {
                return OperateResult(true, "操作成功", null);
            }
            else
            {
                return OperateResult(false, "参数错误", null);
            }
        }
    }
}