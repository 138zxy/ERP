using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
namespace ZLERP.Web.Controllers.HR
{
    public class HR_Base_DataController : HRBaseController<HR_Base_Data, long?>
    {
        public override ActionResult Index()
        {
            List<SelectListItem> ItemsTypes = new List<SelectListItem>();
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.性别, Value = BaseDataType.性别 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.结婚状态, Value = BaseDataType.结婚状态 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.社保状态, Value = BaseDataType.社保状态 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.所学专业, Value = BaseDataType.所学专业 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.用工形式, Value = BaseDataType.用工形式 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.人员状态, Value = BaseDataType.人员状态 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.学历, Value = BaseDataType.学历 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.岗位, Value = BaseDataType.岗位 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.职务, Value = BaseDataType.职务 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.政治面貌, Value = BaseDataType.政治面貌 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.职称, Value = BaseDataType.职称 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.民族, Value = BaseDataType.民族 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.人才等级, Value = BaseDataType.人才等级 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.籍贯, Value = BaseDataType.籍贯 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.职称取得方式, Value = BaseDataType.职称取得方式 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.证照类型, Value = BaseDataType.证照类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.发证机构, Value = BaseDataType.发证机构 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.合同年份, Value = BaseDataType.合同年份 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.合同类型, Value = BaseDataType.合同类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.合同解除类别, Value = BaseDataType.合同解除类别 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.调动类型, Value = BaseDataType.调动类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.离职类型, Value = BaseDataType.离职类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.复职类型, Value = BaseDataType.复职类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.开户行, Value = BaseDataType.开户行 });

            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.奖惩处理类别, Value = BaseDataType.奖惩处理类别 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.奖惩方式, Value = BaseDataType.奖惩方式 });

            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.请假类型, Value = BaseDataType.请假类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.假期类型, Value = BaseDataType.假期类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.加班类型, Value = BaseDataType.加班类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.出差类型, Value = BaseDataType.出差类型 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.扣除项目, Value = BaseDataType.扣除项目 });
            ItemsTypes.Add(new SelectListItem { Text = BaseDataType.补贴项目, Value = BaseDataType.补贴项目 }); 
            ViewBag.ItemsType = ItemsTypes;
            return base.Index();
        }

        public override ActionResult Add(HR_Base_Data HR_Base_Data)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ItemsType == HR_Base_Data.ItemsType && t.ItemsName == HR_Base_Data.ItemsName).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("类别：{0}名称：{1}已经存在，不能重复添加", HR_Base_Data.ItemsType, HR_Base_Data.ItemsName), null);
            }
            return base.Add(HR_Base_Data);
        }

        public override ActionResult Update(HR_Base_Data HR_Base_Data)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != HR_Base_Data.ID && t.ItemsType == HR_Base_Data.ItemsType && t.ItemsName == HR_Base_Data.ItemsName).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("类别：{0}名称：{1}已经存在，不能重复添加", HR_Base_Data.ItemsType, HR_Base_Data.ItemsName), null);
            }
            return base.Update(HR_Base_Data);
        }
        /// <summary>
        /// 组合任务单数据显示在autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        //public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "Fcode", bool ascending = true, string condition = "")
        //{
        //    return new HRBase().GetSC_Fixed(q, textField, valueField, orderBy, ascending, condition);
        //}
    }
}
