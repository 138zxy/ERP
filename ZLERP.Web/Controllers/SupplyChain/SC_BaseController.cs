using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Web.Helpers;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_BaseController : BaseController<SC_Base, string>
    {
        //
        // GET: /SC_Base/
        public override ActionResult Index()
        {
            List<SelectListItem> ItemsTypes = new List<SelectListItem>();
            ItemsTypes.Add(new SelectListItem { Text = "单位", Value = "单位" });
            ItemsTypes.Add(new SelectListItem { Text = "供应商类别", Value = "供应商类别" });
            ItemsTypes.Add(new SelectListItem { Text = "付款方式", Value = "付款方式" });
            ItemsTypes.Add(new SelectListItem { Text = "品牌", Value = "品牌" });
            ItemsTypes.Add(new SelectListItem { Text = "资产增加方式", Value = "资产增加方式" });
            ItemsTypes.Add(new SelectListItem { Text = "资产清理方式", Value = "资产清理方式" }); 
            ViewBag.ItemsType = ItemsTypes;
            return base.Index();
        }

        public override ActionResult Add(SC_Base SC_Base)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ItemsType == SC_Base.ItemsType && t.ItemsName == SC_Base.ItemsName).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("类别：{0}名称：{1}已经存在，不能重复添加", SC_Base.ItemsType, SC_Base.ItemsName), null);
            }
            return base.Add(SC_Base);
        }

        public override ActionResult Update(SC_Base SC_Base)
        {
            var query = this.m_ServiceBase.Query().Where(t =>t.ID!=SC_Base.ID && t.ItemsType == SC_Base.ItemsType && t.ItemsName == SC_Base.ItemsName).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("类别：{0}名称：{1}已经存在，不能重复添加", SC_Base.ItemsType, SC_Base.ItemsName), null);
            }
            return base.Update(SC_Base);
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
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "Fcode", bool ascending = true, string condition = "")
        {
            return new SupplyChainHelp().GetSC_Fixed(q, textField, valueField, orderBy, ascending, condition); 
        }
    }
}
