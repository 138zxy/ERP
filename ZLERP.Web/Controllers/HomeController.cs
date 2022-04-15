﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;　
using ZLERP.Business;
using ZLERP.Model.ViewModels;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.IO;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Data;
using System.Text;
using System.Collections;

namespace ZLERP.Web.Controllers
{
    public class HomeController : ServiceBasedController
    {
        [HttpPost]
        public JsonResult CheckIsForcedLogout()
        {
            lock (ServiceBasedController.licLock)
            {
                try
                {
                    int m = 0;
                    HttpContext httpContext = System.Web.HttpContext.Current;
                    Hashtable userOnline = (Hashtable)httpContext.Application["Online"];
                    if (userOnline != null)
                    {
                        m = 1;
                        if (userOnline.ContainsKey(httpContext.Session.SessionID))
                        {
                            m = 2;
                            var value = userOnline[httpContext.Session.SessionID];
                            //判断当前session保存的值是否为被注销值
                            if (value != null && "_offline_".Equals(value))
                            {
                                m = 3;
                                //验证被注销则清空session
                                userOnline.Remove(httpContext.Session.SessionID);
                                httpContext.Application.Lock();
                                httpContext.Application["online"] = userOnline;
                                httpContext.Application.UnLock();
                                m = 4;
                                string msg = "下线通知：当前账号另一地点登录， 您被迫下线。若非本人操作，您的登录密码很可能已经泄露，请及时改密。";

                                //登出，清除cookie
                                FormsAuthentication.SignOut();
                                m = 5;
                                return Json(new { OperateResult = "Success", OperateData = msg }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            if (userOnline == null)
                                userOnline = new Hashtable();
                            httpContext.Application.Lock();
                            userOnline.Add(httpContext.Session.SessionID, AuthorizationService.CurrentUserID);
                            httpContext.Application["Online"] = userOnline;
                            httpContext.Application.UnLock();
                        }
                    }
                    else
                    {
                        userOnline = new Hashtable();
                        httpContext.Application.Lock();
                        userOnline.Add(httpContext.Session.SessionID, AuthorizationService.CurrentUserID);
                        httpContext.Application["Online"] = userOnline;
                        httpContext.Application.UnLock();
                    }
                    return Json(new { OperateResult = m + "-" + httpContext.Session.SessionID + "-" + (userOnline[httpContext.Session.SessionID] == null ? "!" : userOnline[httpContext.Session.SessionID]) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { OperateResult = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        /// <summary>
        /// 导出Excel
        /// </summary>
        public void excel()
        {
            DataSet dataSet = new DataSet();
            string strXML = Request.Form["xml"];
            UTF8Encoding encoding = new UTF8Encoding();
            if (!string.IsNullOrEmpty(strXML))
            {
                strXML = encoding.GetString(Convert.FromBase64String(strXML));
            }
            System.IO.StringReader textReader = new System.IO.StringReader(strXML);
            dataSet.ReadXml(textReader);
            ExcelExportHelper.ExportExcel(dataSet);
        }

        public override ActionResult Index()
        {            
            HomeViewModel hvm = new HomeViewModel();
            log.Debug("-");
            //所有系统项配置
            hvm.SysConfigs = this.service.SysConfig.GetAllSysConfigs();

            IList<SysFunc> funcs;

            //获取用户权限方案
            PublicService ps = new PublicService();
            SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.AuthScheme);
            if (config == null)
            {
                config.ConfigValue = "1"; 
            }
            log.Debug("0");
            //用户权限方案一
            if (config.ConfigValue == "1")
            {
                funcs = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);
                ViewBag.AuthScheme = "启用权限方案一";
            }
            //用户权限方案二
            else
            {
                funcs = this.service.User.GetUserFuncs2(AuthorizationService.CurrentUserID);
                ViewBag.AuthScheme = "启用权限方案二";
            }

            //当前用户根目录列表
            hvm.RootFuncs = funcs.Where(f => string.IsNullOrEmpty(f.ParentID) && !f.IsDisabled) 
            .Select(p => new ZLERP.Model.ViewModels.HomeViewModel.MenuInfo()
            {
                ID = p.ID,
                PID = p.ParentID,
                Name = p.FuncName,
                Desc = p.FuncDesc,
                Url = p.URL,
                IsL = p.IsLeaf
            }).ToList();
            log.Debug("1");
            ////子菜单列表（无限级） 
            //funcss = funcs;
            //subFindFunc(funcs);
            //var subFuncs = funcss.Select(p => new ZLERP.Model.ViewModels.HomeViewModel.MenuInfo()
            //    {
            //        ID = p.ID,
            //        PID = p.ParentID,
            //        Name = p.FuncName,
            //        Desc = p.FuncDesc,
            //        Url = p.Urls.FirstOrDefault(),
            //        IsL = p.IsLeaf
            //    }).ToList();

            //子菜单列表(有限级)
            var subFuncs = funcs.Where(f => !string.IsNullOrEmpty(f.ParentID) && !f.IsButton && !f.IsDisabled && (f.ID == "9501" || f.ParentID != "95"))
                .Select(p => new ZLERP.Model.ViewModels.HomeViewModel.MenuInfo()
                {
                    ID = p.ID,
                    PID = p.ParentID,
                    Name = p.FuncName,
                    Desc = p.FuncDesc,
                    Url = p.Urls.FirstOrDefault(),
                    IsL = p.IsLeaf
                }).ToList();

            //当前用户子功能列表
            hvm.SubFuncs = Helpers.HelperExtensions.ToJson(subFuncs);

            /*
            IList<Dic> allDics = this.service.Dic.All("OrderNum", true);
            //用于render的dics对象，dic["dicid"] 保存所有子元素
            Dictionary<string, IList<Dic>> dics = new Dictionary<string, IList<Dic>>();
            foreach (var dic in allDics.Where(p => string.IsNullOrEmpty(p.ParentID)).ToList())
            {
                dics[dic.ID] = allDics.Where(p => p.ParentID == dic.ID).ToList(); 
            }
            ViewBag.Dics = MvcHtmlString.Create(HelperExtensions.ToJson(dics));
            */

            //部门列表数据
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "DepartmentName", true, "");           
            ViewData.Model = hvm;

            //对在线人数全局变量进行加1处理
            HttpContext rq = System.Web.HttpContext.Current;
            ViewBag.OnLineCount = rq.Application["OnLineCount"];
            //APP相关
            var amodel = this.service.GetGenericService<ZLERP.Model.App.App_VersionManage>().Get(1);
            var imodel = this.service.GetGenericService<ZLERP.Model.App.App_VersionManage>().Get(2);
            ViewBag.aAppUrl =amodel==null?"":amodel.AppUrl;
            ViewBag.iAppUrl = imodel == null ? "" : imodel.AppUrl;
            ViewBag.aAppName = amodel == null ? "" : amodel.AppName;
            ViewBag.iAppName = imodel == null ? "" : imodel.AppName;
            return base.Index();
             
        }
        ///// <summary>
        ///// 子菜单递归查找
        ///// </summary>
        ///// <param name="root"></param>
        //private void subFindFunc(IList<SysFunc> funcs)
        //{
        //    IList<SysFunc> subFunc = this.service.SysFunc.All()
        //        .Where(p => !string.IsNullOrEmpty(p.ParentID) && !p.IsButton && !p.IsDisabled && (p.ID == "9501" || p.ParentID != "95") 
        //              && funcs.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p => p.OrderNum)
        //        .ToList();

        //    funcss = funcss.Union(subFunc).ToList();
        //    if (subFunc.Count != 0)
        //    {
        //        subFindFunc(subFunc);
        //    }
        //}

        public ActionResult SiteMaps()
        {
            return View();
        }
        /// <summary>
        /// 桌面管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Desktop()
        {           
            //var data = this.service.ProduceTask.GetRangeTimeTasks(new JqGridRequest() { 
            //    PageIndex = 0, RecordsCount = 8, SortingName = "ID", SortingOrder =  JqGridSortingOrders.Desc
            //} );

            var myUndoMsg = this.service.MyMsg.Find(new JqGridRequest()
            {
                PageIndex = 0,
                RecordsCount = 0,
                SortingName = "ID",
                SortingOrder = JqGridSortingOrders.Desc
            }, "UserID='"+AuthorizationService.CurrentUserID+"' AND IsRead = 0 AND DealStatus = 0");

            ViewBag.TotalMsg = myUndoMsg;//未读消息数量
            //ViewBag.TotalTasks = data.Count;

            //公共列表
            var notice = this.service.GetGenericService<Notice>().Query()
                .OrderByDescending(p => p.IsTop)
                .OrderByDescending(p => p.BuildTime)
                .Take(1)
                .FirstOrDefault();
            ViewBag.Notice = notice;

            return View(myUndoMsg);
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// 默认上传文件路径
        /// </summary>
        private string uploadDir = "~/Content/Files/";
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="uploadPath"></param>
        /// <returns></returns>
       [HttpPost]
        public ActionResult UploadReport(string uploadPath)
        {
            //string token = Request.Form["__RequestVerificationToken"];
            //if (string.IsNullOrEmpty(token) || token != HelperExtensions.RequestVerificationToken(null))
            //{
            //    return HttpNotFound();
            //}
            if (Request.Files.Count > 0)
            {
                try
                { 
                    string uploadUrl = uploadDir;
                    string uploadFolder = Server.MapPath(uploadDir);
                    if (!string.IsNullOrEmpty(uploadPath))
                    {
                        uploadFolder = Path.Combine(uploadFolder, uploadPath);
                        uploadUrl += uploadPath;
                    }
                    DirectoryInfo dirinfo = new DirectoryInfo(uploadFolder);
                    if (!dirinfo.Exists)
                    {
                        dirinfo.Create();
                    }
                    IList<string> message = new List<string>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if (Request.Files[i].ContentLength > 0)
                        {
                            if (!IsFileTypeAllowed(Path.GetExtension(Request.Files[i].FileName)))
                            {
                                message.Add(Lang.Error_FileTypeNotAllowed);
                                continue;
                            }
                            else
                            {
                                string fileName = Path.GetFileName(Request.Files[i].FileName);
                                string fullFileName = Path.Combine(uploadFolder, fileName);
                                Request.Files[i].SaveAs(fullFileName);
                                uploadUrl += fileName;
                                message.Add(VirtualPathUtility.ToAbsolute(uploadUrl));
                            }
                        }
                    }
                    return OperateResult(true, Lang.Msg_Operate_Success, message);
                }
                catch (Exception e)
                {
                    log.Error(e.Message, e);
                    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
                }
            }
            else
            {
                return OperateResult(false, Lang.Error_ParameterRequired, "Request.Files");
            }
        }

        /// <summary>
        /// 是否允许上传的文件类型
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        bool IsFileTypeAllowed(string fileExt)
        {
           SysConfig config =  this.service.SysConfig.GetSysConfig( Model.Enums.SysConfigEnum.UploadFileTypes);
           if (config != null) {
               return config.ConfigValue.ToLower()
                   .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                   .Contains(fileExt.ToLower());
           }
           return false;

        }
        /// <summary>
        /// 获取工具条按钮
        /// </summary>
        /// <param name="funcId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public JsonResult GetButtons(string funcId, int flag)
        {
            
            var buttons = this.service.User.GetUserButtons(funcId, flag);
            return Json(buttons, JsonRequestBehavior.AllowGet);
        }
    }
}
