using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Web.Controllers.Attributes;
using System.Data;
using ZLERP.Resources;
using System.Reflection;

namespace ZLERP.Web.Controllers
{
    public class Lab_TemplateDataConfigController : BaseController<Lab_TemplateDataConfig, int?>
    {

        /// <summary>
        /// 获取试验记录列
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetLabRecordListData(int flag,string textField, string valueField)
        {
            SqlServerHelper helper = new SqlServerHelper();
            string strsql="";
            if (flag==1)
            {
                strsql = @"SELECT 字段名 valueField,字段说明 textField FROM fn_TableFrame('Lab_Record')
                                                   UNION ALL
                                                   SELECT 'StuffName','材料名称'
                                                   union all
                                                   SELECT 'SupplyName','生产厂家'";
            }
            else if (flag==2)
            {
                strsql = @"SELECT 字段名 valueField,字段说明 textField FROM fn_TableFrame('Lab_ConWPRecord')";
            }

            else if (flag == 3)
            {
                strsql = @"SELECT 字段名 valueField,字段说明 textField FROM fn_TableFrame('Lab_ConWPRecordGH')";
            }


            DataTable dt = helper.ExecuteDataset(strsql, CommandType.Text, null).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            IList<ListDataModel> list = ModelConvertHelper<ListDataModel>.ConvertToModel(dt);
            List<SelectListItem> items = new List<SelectListItem>();
            int i = 0;
            foreach (dynamic j in list)
            {
                bool f = false;
                if (i == 0) { f = true; i++; }
                else f = false;
                SelectListItem item = new SelectListItem() { Text = j.textField, Value = j.valueField, Selected = f };
                items.Add(item);
            }

            return Json(items);
        }

    }
    public class ListDataModel
    {
        public virtual string textField
        {
            get;
            set;
        }
        public virtual string valueField
        {
            get;
            set;
        }
    }
}
