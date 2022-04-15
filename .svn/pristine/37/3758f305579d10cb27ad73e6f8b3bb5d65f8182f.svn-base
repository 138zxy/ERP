using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model.ViewModel;
using ZLERP.Web.Controllers.Attributes;

namespace ZLERP.Web.Controllers
{
    public class ProjectGHController : BaseController<ProjectGH, string>
    {
        //public override ActionResult Add(ProjectGH entity)
        //{
        //    return base.Add(entity);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public ActionResult GetProjectByName(string ProjectName) {
            IList<ProjectGH> projects = this.service.ProjectGH.Query().Where(m => m.ProjectName.Contains(ProjectName)).OrderByDescending(m => m.BuildTime).Take(30).ToList();
            var projectsList = projects.Select(m => new { ProjectName = m.ProjectName });
            dynamic data = new { Name = projectsList };
            return this.Json(data);
        }
        /// <summary>
        /// 获取工程名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetProjectName(string id)
        {
            IList<ProjectGH> ps = this.service.ProjectGH.Query().Where(m => m.ContractID == id).OrderByDescending(m => m.ID).ToList();
            if (ps == null || ps.Count == 0)
                return this.Json(new { Name = "",ProjectId="" });
            else
                return this.Json(new { Name = ps[0].ProjectName, ProjectId = ps[0].ID });
        }

        public ActionResult MapEnable(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    //this.service.ShippingDocument.Audit(id, false);
                    ProjectGH p = m_ServiceBase.Get(id);
                    p.IsShow = true;
                    m_ServiceBase.Update(p,null);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult MapDisable(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ProjectGH p = m_ServiceBase.Get(id);
                    p.IsShow = false;
                    m_ServiceBase.Update(p, null);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult GetProject(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            IList<ProjectGH> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.ProjectGH.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.ProjectGH.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>"+
                "[<span class='PId'>{8}</span>]<br/>{5}：<span class='PAddr'>{1}</span><br/>{6}：<span class='Plink'>{2}</span><br/>{7}：<span class='PTel'>{3}</span>" +
                "<span  style='display:none' class='PRmark'>{4}</span>",
                        HelperExtensions.Eval(p, textField),
                        p.ProjectAddr,
                        p.LinkMan,
                        p.Tel,
                        p.Remark,
                       "工程地址",
                       "联系人",
                       "联系电话",
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        public ActionResult GetSingleProjInfo(string id)
        {
            var pj = this.service.ProjectGH.Get(id);
            ProjInfoVm pvm = new ProjInfoVm();
            if (pj != null)
            {
                pvm.Address = pj.ProjectAddr;
                if (String.IsNullOrWhiteSpace(pj.ProjectAddr))
                {

                    double bd_lng = 0; double bd_lat = 0;
                    pvm.Lat = bd_lat;
                    pvm.Lng = bd_lng;
                }

                pvm.LinkMan = pj.LinkMan;
                pvm.LinkTel = pj.Tel;
                pvm.Name = pj.ProjectName;
                pvm.ProjId = pj.ID;
                pvm.TskVms = new List<TasksVm>();
                string cmpStatus = "007003" ;//已完成
                string undoStatus = "007004";//已撤销
                //var avlibCts = unitOfWork.CarTaskRepository.Get(s => s.StatusID != cmpStatus.Id && s.StatusID != undoStatus.Id && (!s.Task.Completed)).ToList();
                var avlibCts = this.service.ShippingDocument.Query().Where(s => s.Status != cmpStatus && s.Status != undoStatus).ToList();
                string carnos = "";
                //未完成任务单
                //var tsks = unitOfWork.TaskRepository.Get(s => !s.Completed && s.ProjectID == pj.ID).ToList();
                var tsks = this.service.ProduceTask.Query().Where(t => !t.IsCompleted && t.ProjectID == pj.ID).ToList();
                foreach (var t in tsks)
                {
                    var cts = avlibCts.Where(s => s.TaskID == t.ID).ToList();
                    foreach (var c in cts)
                    {
                        carnos += (c.CarID + " ");
                    }
                    TasksVm tv = new TasksVm();
                    tv.TaskNo = t.ID;
                    tv.ConsPos = t.ConsPos;
                    tv.PlanCube = t.PlanCube;
                    tv.ProvidedCube = t.ProvidedCube;
                    tv.Grade = t.ConStrength;
                    pvm.TskVms.Add(tv);
                }
                //本月已完成方量
                //var cmpCts = unitOfWork.CarTaskRepository.Get(s => s.StatusID == cmpStatus.Id && (s.Task.ProjectID == pj.ID)).ToList();
                var cmpCts = this.service.ShippingDocument.Query().Where(s => !s.IsEffective && (s.ProjectID == pj.ID)).ToList();
                var now = DateTime.Now;
                var theMon = new DateTime(now.Year, now.Month, 1);
                var theDay = new DateTime(now.Year, now.Month, now.Day);
                var monCube = cmpCts.Where(s => s.ProduceDate >= theMon).Sum(s => s.Cube);
                var dayCube = cmpCts.Where(s => s.ProduceDate >= theDay).Sum(s => s.Cube);
                pvm.carNos = carnos;
                //该工地线路
                //pvm.ProjectPaths = unitOfWork.ProjectPathRepository.Get(s => s.ProjectID == id && s.IsUsed && s.IsComplete).ToList();

            }
            return Json(pvm, JsonRequestBehavior.AllowGet);
        }
         
        [HttpPost]
        public ActionResult UpdateProjRange(string ID , decimal PlaceRange)
        {
            string Pname = string.Empty;
            try
            {
                ProjectGH pro = m_ServiceBase.Get(ID);
                if (pro != null)
                {
                    Pname = pro.ProjectName;
                }
                pro.PlaceRange = PlaceRange;
                base.Update(pro);
            }
            catch (Exception e)
            {
               return OperateResult(false, "error:" + e.Message,null);
            }
            return OperateResult(true,"修改工地范围*(" + Pname + "," + PlaceRange +","+ID + ")*成功",null);
        }

        /// <summary>
        /// 获取任务单是否已完成状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        private bool getTaskIsCompleted(string taskID)
        {
            ProduceTask pt = this.service.ProduceTask.Get(taskID);
            if (pt == null)
                return true;
            else
                return pt.IsCompleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractid"></param>
        /// <param name="linkman"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public JsonResult GetProjectInfoByContractId(string projectid)
        {
            ProjectGH cp = this.service.ProjectGH.Get(projectid);
            return OperateResult(true, "", cp);
        }

        public override  JsonResult ListData1(string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            var data = this.m_ServiceBase.All(new List<string> { valueField, textField }, condition, orderBy, ascending);

            List<SelectListItem> items = new List<SelectListItem>();
            IList<ProjectGH> list = data as IList<ProjectGH>;
            int i = 0;
            foreach (ProjectGH j in list)
            {
                bool f = false;
                if (i == 0) { f = true; i++; }
                else f = false;
                SelectListItem item = new SelectListItem() { Text = j.ProjectName, Value = j.ID, Selected = f };
                items.Add(item);
            }
            return Json(items);
        }


        public override ActionResult Delete(string[] id)
        {
            foreach (var no in id)
            { 
               var project= this.m_ServiceBase.Get(no);
               if (project.ContractGH.AuditStatus == 1)
               {
                   return OperateResult(false, "已经审核过的合同不能删除工程", null);
               }
            }
            return base.Delete(id);
        }
    }
}
