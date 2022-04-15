using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class Lab_TemplateController : BaseController<Lab_Template, string>
    {
        #region 下载文件
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
                    bool issucees = CreateDownFile(id);
                }

                string path0 = "~/Content/Files/LabTemplate/";
                string path1 = path0 + "DownFile/";
                string path = Server.MapPath(path1);

                //复制批量命令文件
                //System.IO.File.Copy(Server.MapPath(path0 + "批量打印试验记录页.xls"), Server.MapPath(path1 + "批量打印试验记录页.xls"), true);

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
            catch
            {
                return OperateResult(false, "操作失败！", null);
            }
        }
        /// <summary>
        /// 创建下载文件包
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CreateDownFile(string id)
        {
            try
            {
                Lab_Template lr = this.service.GetGenericService<Lab_Template>().Get(id);
                if (lr.LabTemplatePath == "")
                {
                    return false;
                }
                string path = Path.GetDirectoryName(lr.LabTemplatePath);
                string kzm = Path.GetExtension(lr.LabTemplatePath);
                string pathn = path + @"\DownFile\";
                if (!Directory.Exists(Server.MapPath(pathn)))
                {
                    Directory.CreateDirectory(Server.MapPath(pathn));//创建该文件夹
                }
                path = pathn + Path.GetFileNameWithoutExtension(lr.LabTemplatePath) + kzm;

                string sfile = Server.MapPath(lr.LabTemplatePath);
                string tfile = Server.MapPath(path);
                System.IO.File.Copy(sfile, tfile, true);
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
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();//返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)//判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);//删除子目录和文件
                    }
                    else
                    {
                        System.IO.File.Delete(i.FullName);//删除指定文件
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
        /// 判断模板文件是否存在
        /// </summary>
        /// <param name="templateid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsExistTemplateFile(string templateid)
        {
            try
            {
                bool isExist = false;
                Lab_Template ltmodel = this.service.GetGenericService<Lab_Template>().Get(templateid);
                if (ltmodel != null)
                {
                    if (!string.IsNullOrEmpty(ltmodel.LabTemplatePath) && System.IO.File.Exists(Server.MapPath("~" + ltmodel.LabTemplatePath)))
                    {
                        isExist = true;
                    }
                    else
                    {
                        isExist = false;
                    }
                }
                else
                {
                    isExist = false;
                }
                if (isExist)
                {
                    return OperateResult(true, "", isExist);
                }
                else
                {
                    return OperateResult(true, "你选择的模板文件不存在！", isExist);
                }
            }
            catch
            {
                return OperateResult(false, "操作失败！", false);
            }
        }

    }   
}
