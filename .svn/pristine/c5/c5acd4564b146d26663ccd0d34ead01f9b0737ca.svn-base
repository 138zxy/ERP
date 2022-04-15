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

namespace ZLERP.Web.Controllers
{
    public class ConStrengthController : BaseController<ConStrength,int>
    {
        public override ActionResult Index()
        {
            List<SelectListItem> ItemsTypes = new List<SelectListItem>() ;
            ItemsTypes.Add(new SelectListItem { Text = "混凝土", Value = "0" });
            ItemsTypes.Add(new SelectListItem { Text = "湿拌", Value = "1" });
            ItemsTypes.Add(new SelectListItem { Text = "干混", Value = "2" });
            ViewBag.IsSH = ItemsTypes;
            return base.Index();
        }
        public override ActionResult Add(ConStrength ConStrength)
        {
            string constrCode = ConStrength.ConStrengthCode.ToUpper();
            List<ConStrength> list = this.service.GetGenericService<ConStrength>().Query().Where(a=>a.ConStrengthCode.ToUpper()==constrCode).ToList();
            if (list.Count>0)
            {
                return OperateResult(false, "该砼强度已存在，请勿重复添加！", null);
            }
            SysConfig SysConfig = base.service.SysConfig.GetSysConfig("IsOpenNC");
            if (SysConfig !=null)
            {
                if (bool.Parse(SysConfig.ConfigValue)&&(ConStrength.NCCode==""||ConStrength.NCCode==null))
                {
                    return OperateResult(false, "您设置了NC必填，请填写NC编码！", null);
                }
            }
            ConStrength.Exchange = ConStrength.Exchange * 1000;
            return base.Add(ConStrength);
        }


        public override ActionResult Update(ConStrength ConStrength)
        {
            ConStrength.Exchange = ConStrength.Exchange * 1000;
            return base.Update(ConStrength);
        }

        /// <summary>
        /// 获取砼强度列表
        /// </summary>
        /// <param name="ConStrengthCode"></param>
        /// <returns></returns>
        public virtual JsonResult getConStrengthList(string id, string textField, string valueField, string orderBy = "ConStrengthCode", bool ascending = false, string condition = "")
        {
            IEnumerable<SelectListItem> list = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", condition, "ConStrengthCode", true, null);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据DataType获取砼强度列表
        /// </summary>
        /// <param name="ProductLineID"></param>
        /// <returns></returns>
        public ActionResult GetDynamicColByDataType(string Datatype)
        {
            dynamic dynamicCol = this.service.ConStrength.GetDynamicColByDataType(Datatype);
            return this.Json(dynamicCol);
        }
    }
}
