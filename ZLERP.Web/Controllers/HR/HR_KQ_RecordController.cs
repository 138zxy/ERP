using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using System.IO;
using ZLERP.Web.Helpers;
using System.Data;
using System.Configuration;
using ZLERP.Business.HR;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_KQ_RecordController : HRBaseController<HR_KQ_Record, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(HR_KQ_Record entity)
        {
            entity.DataSource = "手动新增";
            return base.Add(entity);
        }

        public ActionResult ToExcel()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files.Count <= 0)
                {
                    return OperateResult(false, "请选择合适的文件上传", null);
                }
                var file = files[0];
                string ext = Path.GetExtension(file.FileName).ToLower();
                if (!(ext == ".xls" || ext == ".xlsx"))
                {
                    return OperateResult(false, "请选择excel文件上传", null);
                }

                string _attachmentBaseDir = ConfigurationManager.AppSettings["AttachmentBaseDir"];

                string uploadFolder = Server.MapPath(_attachmentBaseDir);
                uploadFolder = Path.Combine(uploadFolder, "UploadFile");
                string fileName = Path.GetFileName(file.FileName);
                DirectoryInfo dirinfo = new DirectoryInfo(uploadFolder);
                if (!dirinfo.Exists)
                {
                    dirinfo.Create();
                }
                DateTime dt = DateTime.Now;
                DateTime sdt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                int difdt = (int)(dt - sdt).TotalSeconds;
                string reName = difdt + "_" + fileName;
                string fullFileName = Path.Combine(uploadFolder, reName);
                file.SaveAs(fullFileName);

                // 读取execl处理
                DataTable table = ExcelExportHelper.ExcelToDataTable(fullFileName, true);

                List<HR_KQ_Record> Records = new List<HR_KQ_Record>();
                int i = 0;
                List<string> listNo = new List<string>();
                foreach (DataRow row in table.Rows)
                {
                    listNo.Add(row["工号"].ToString());
                }
                if (listNo == null || listNo.Count <= 0)
                {
                    return OperateResult(false, "请在模板中填入有效数据", null);
                }
                var personList = this.service.GetGenericService<HR_Base_Personnel>().Query().Where(t => listNo.Contains(t.JobNo)).Select(t => new { ID = t.ID, JobNo = t.JobNo }).ToList();
                foreach (DataRow row in table.Rows)
                {
                    i++;
                    DateTime d1;
                    string nowdate = row["签卡日期"].ToString() + " " + row["签卡时间"].ToString();
                    if (string.IsNullOrEmpty(row["签卡日期"].ToString()))
                    {
                        break;
                    }
                    if (!DateTime.TryParse(nowdate, out d1))
                    {
                        return OperateResult(false, string.Format("第{0}行的时间格式有问题，请检查", i), null);
                    }
                    var person = personList.Where(t => t.JobNo == row["工号"].ToString()).FirstOrDefault();
                    if (person == null)
                    {
                        continue;
                    }
                    HR_KQ_Record Record = new HR_KQ_Record();
                    Record.PersonID = Convert.ToInt32(person.ID);
                    Record.CheckTime = d1;
                    Record.DataSource = "Excel导入";
                    Record.Builder = AuthorizationService.CurrentUserID;
                    Record.BuildTime = DateTime.Now;
                    Records.Add(Record);
                }
                this.service.HRService.InsertRecord(Records);
                //处理完毕之后，删除
                if (System.IO.File.Exists(fullFileName))
                {
                    System.IO.File.Delete(fullFileName);
                }
                return ContentResult(true, string.Join("<br/>", "导入成功"), null);

            }
            catch (Exception ex)
            {
                return ContentResult(false, "No files to upload.", null);
            }
        }
        ActionResult ContentResult(bool result, string message, object data)
        {
            return Content(
                    HelperExtensions.ToJson(
                        OperateResult(result, message, data).Data
                    )
                );
        }
    }
}
