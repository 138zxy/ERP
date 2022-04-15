using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using System.IO;
using System.Configuration;
using ZLERP.Web.Helpers;
using System.Data;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_Base_PersonnelController : HRBaseController<HR_Base_Personnel, int?>
    {
        public override ActionResult Index()
        {
            ViewBag.Sex = GetBaseData(BaseDataType.性别);
            ViewBag.Nation = GetBaseData(BaseDataType.民族);
            ViewBag.Marry = GetBaseData(BaseDataType.结婚状态);
            ViewBag.RecordSchool = GetBaseData(BaseDataType.学历);
            ViewBag.Profession = GetBaseData(BaseDataType.所学专业);
            ViewBag.SocialState = GetBaseData(BaseDataType.社保状态);
            ViewBag.PositionType = GetBaseData(BaseDataType.职务);
            ViewBag.Post = GetBaseData(BaseDataType.岗位);
            ViewBag.EmploymentForm = GetBaseData(BaseDataType.用工形式);
            ViewBag.State = GetBaseData(BaseDataType.人员状态);
            return base.Index();
        }
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "Name", bool ascending = false, string condition = "")
        {
            return base.GetPersonnel(q, textField, valueField, orderBy, ascending, condition);
        }

        public ActionResult LoadExcel()
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

                List<HR_Base_Personnel> Records = new List<HR_Base_Personnel>();

                foreach (DataRow row in table.Rows)
                {
                    if (string.IsNullOrEmpty(row["姓名"].ToString()))
                    {
                        break;
                    }
                    DateTime dtNow;
                    HR_Base_Personnel Record = new HR_Base_Personnel();
                    Record.Code = row["员工编号"].ToString();
                    Record.Name = row["姓名"].ToString();
                    Record.JobNo = row["工号"].ToString();
                    Record.PyCode = row["拼音码"].ToString();
                    Record.Nation = row["性别"].ToString();
                    Record.Marry = row["结婚状态"].ToString();
                    if (DateTime.TryParse(row["生日"].ToString(), out dtNow))
                    {
                        Record.Birthday = dtNow;
                    }
                    if (DateTime.TryParse(row["入职时间"].ToString(), out dtNow))
                    {
                        Record.PostDate = dtNow;
                    }
                    Record.RecordSchool = row["学历"].ToString();
                    Record.School = row["毕业学校"].ToString();
                    Record.Profession = row["所学专业"].ToString();
                    if (DateTime.TryParse(row["毕业时间"].ToString(), out dtNow))
                    {
                        Record.GraduateDate = dtNow;
                    }
                    Record.SocialState = row["社保状态"].ToString();
                    Record.Address = row["现住地址"].ToString();
                    Record.IDno = row["身份证号"].ToString();
                    Record.IDAddress = row["身份证地址"].ToString();
                    Record.Telphone = row["家庭电话"].ToString();
                    Record.CellPhone = row["手机"].ToString();
                    Record.UrgentPerson = row["紧急联系人"].ToString();
                    Record.PositionType = row["职务"].ToString();
                    Record.Post = row["岗位"].ToString();
                    Record.MountGuardProperty = row["上岗性质"].ToString();
                    var dep = row["部门"].ToString();
                    var query = this.service.GetGenericService<ZLERP.Model.Department>().Query().Where(t => t.DepartmentName == dep).FirstOrDefault();
                    if (query != null)
                    {
                        Record.DepartmentID = Convert.ToInt32(query.ID);
                    }
                    Record.SchoolRecordNo = row["学历证号"].ToString();
                    Record.State = row["人员状态"].ToString();
                    Record.EmploymentForm = row["用工形式"].ToString();
                    Record.Email = row["电子邮件"].ToString();
                    Record.Vita = row["个人简历"].ToString();
                    if (DateTime.TryParse(row["转正日期"].ToString(), out dtNow))
                    {
                        Record.CorrectionDate = dtNow;
                    }

                    Record.ArchivesNo = row["档案号"].ToString();
                    Record.BankNo = row["银行卡号"].ToString();
                    Record.Meno = row["备注"].ToString();
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
