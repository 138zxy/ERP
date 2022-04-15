﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
//using Common.Logging.Configuration;
using System.IO;
using System.Drawing;
using ICSharpCode.SharpZipLib.Zip;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class Lab_RecordController : BaseController<Lab_Record, int?>
    {
        #region 下载压缩文件
        /// <summary>
        /// 下载文件（打包)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DownFile(string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                   bool issucees= CreateDownFile(id);
                }
                
                string path0 = "~/Content/Files/LabReport/";
                string path1 = path0 + "DownFile/";
                string path = Server.MapPath(path1);
                if (!Directory.Exists(Server.MapPath(path0)))
                {
                    System.IO.Directory.CreateDirectory(path0);
                }
                if (!Directory.Exists(Server.MapPath(path1)))
                {
                    System.IO.Directory.CreateDirectory(path1);
                }
                //复制批量命令文件
                System.IO.File.Copy(Server.MapPath(path0 + "批量打印试验记录页.xls"), Server.MapPath(path1+ "批量打印试验记录页.xls"), true);

                if (Directory.Exists(Server.MapPath(path1)))
                {
                    if (System.IO.File.Exists(Server.MapPath(path0) + "DownFile.zip"))
                    {
                        System.IO.File.Delete(Server.MapPath(path0) + "DownFile.zip");
                    }
                    CreateZip(Server.MapPath(path0 + "DownFile.zip"), path);

                }
                DelectDir(path);

                return OperateResult(true, "操作成功！", null);
            }
            catch(Exception ex)
            {
                return OperateResult(false, "操作失败！"+ex.Message, null);
            }
        }
        
        /// <summary>
        /// 生成压缩文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CreateDownFile(string id)
        {
            try
            {
                Lab_Record lr = this.service.GetGenericService<Lab_Record>().Get(Convert.ToInt32(id));
                if (lr.ReportUrl=="")
                {
                    return false;
                }
                string path = Path.GetDirectoryName(lr.ReportUrl);
                string kzm = Path.GetExtension(lr.ReportUrl);
                string pathn = path + @"\DownFile\";
                if (!Directory.Exists(Server.MapPath(pathn)))
                {
                    Directory.CreateDirectory(Server.MapPath(pathn));//创建该文件夹
                }
                path = pathn + Path.GetFileNameWithoutExtension(lr.ReportUrl) + kzm;
                          
                string sfile=Server.MapPath(lr.ReportUrl);
                string tfile= Server.MapPath(path);
                System.IO.File.Copy(sfile,tfile, true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        
        /// <summary>
        /// 把一个文件夹下所有文件删除
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)//判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        System.IO.File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="zipFileName">压缩后文件</param>
        /// <param name="sourceDirectory">源文件夹</param>
        /// <param name="recurse"></param>
        /// <param name="fileFilter"></param>
        public static void CreateZip(string zipFileName, string sourceDirectory, bool recurse = true, string fileFilter = "")
        {
            if (string.IsNullOrEmpty(sourceDirectory))
            {
                throw new ArgumentNullException("SourceZipDirectory");
            }
            if (string.IsNullOrEmpty(zipFileName))
            {
                throw new ArgumentNullException("TargetZipName");
            }
            if (!Directory.Exists(sourceDirectory))
            {
                throw new DirectoryNotFoundException("SourceDirecotry");
            }
            if (Path.GetExtension(zipFileName).ToUpper() != ".ZIP")
                throw new ArgumentException("TargetZipName  is not zip");
            FastZip fastZip = new FastZip();
            fastZip.CreateZip(zipFileName, sourceDirectory, recurse, fileFilter);
        }
        #endregion

        /// <summary>
        /// 判断报告记录文件是否存在
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsExistRecordFile(string reportid)
        {
            try
            {
                bool isexist = false;
                Lab_Record ltmodel = this.service.GetGenericService<Lab_Record>().Get(reportid);
                if (ltmodel != null)
                {
                    if (!string.IsNullOrEmpty(ltmodel.ReportUrl) && System.IO.File.Exists(ltmodel.ReportUrl))
                    {
                        isexist = true;
                    }
                    else
                    {
                        isexist = false;
                    }
                }
                else
                {
                    isexist = false;
                }
                return OperateResult(true, "操作成功！", isexist);
            }
            catch
            {
                return OperateResult(false, "操作失败！", false);
            }
        }

        #region 审核、取消审核
        //批量审核
        public ActionResult BatchAuditA(string[] ids, int auditStatus)
        {
            try
            {
                BatchAuditS(ids, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ids.ToString(), null, "试验报告审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        //取消审核
        public ActionResult CancelAudit(string lrID, int auditStatus)
        {
            try
            {
                CancelAuditS(lrID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, lrID, null, "试验报告取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="consMixprop"></param>
        public void BatchAuditS(string[] ids, int auditStatus)
        {
            try
            {
                foreach (var id in ids)
                {
                    Lab_Record lr = this.service.GetGenericService<Lab_Record>().Get(Convert.ToInt32(id));
                    string auditor = AuthorizationService.CurrentUserID;
                    lr.AuditStatus = auditStatus;
                    lr.Auditor = auditor;
                    lr.AuditTime = DateTime.Now;
                    this.service.GetGenericService<Lab_Record>().Update(lr, null);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// 取消审核
        /// </summary>
        /// <param name="consMixID"></param>
        /// <param name="auditStatus"></param>
        public void CancelAuditS(string lrid, int auditStatus)
        {
            try
            {
                Lab_Record lr = this.service.GetGenericService<Lab_Record>().Get(Convert.ToInt32(lrid));
                lr.AuditStatus = auditStatus;
                lr.Auditor = "";
                lr.AuditTime = null;
                this.service.GetGenericService<Lab_Record>().Update(lr, null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

            
    }   

}
