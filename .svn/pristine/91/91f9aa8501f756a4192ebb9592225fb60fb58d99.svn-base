using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Web.Mvc; 
using System.Configuration;
using ZLERP.Web.Controllers;
using System.IO; 
namespace ZLERP.Web.Helpers
{
    /// <summary>
    /// 此帮助类，皆在为文件导入系统后进行二次处理的需求服务
    /// </summary>
    public class FileHelper : ServiceBasedController
    {
        private static string _attachmentBaseDir = ConfigurationManager.AppSettings["AttachmentBaseDir"];

        /// <summary>
        /// 上传导入的附件信息
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //public string UploadFile(HttpPostedFileBase file, string pathFolder = "UploadFile")
        //{
        //    string uploadFolder = Server.MapPath(_attachmentBaseDir);
        //    uploadFolder = Path.Combine(uploadFolder, pathFolder);
        //    string fileName = Path.GetFileName(file.FileName);
        //    DirectoryInfo dirinfo = new DirectoryInfo(uploadFolder);
        //    if (!dirinfo.Exists)
        //    {
        //        dirinfo.Create();
        //    }
        //    DateTime dt = DateTime.Now;
        //    DateTime sdt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    int difdt = (int)(dt - sdt).TotalSeconds;
        //    string reName = difdt + "_" + fileName;
        //    string fullFileName = Path.Combine(uploadFolder, reName);
        //    file.SaveAs(fullFileName);
        //    return fullFileName;

        //}

        ///// <summary>
        ///// 上传之后一般需要删除
        ///// </summary>
        ///// <param name="filePath"></param>
        //public void DeleteFile(string filePath)
        //{
        //    if (System.IO.File.Exists(filePath))
        //    {
        //        System.IO.File.Delete(filePath);
        //    }
        //}
    }
}