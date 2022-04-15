using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarDealingsController : BaseController<CarDealings, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            List<SelectListItem> ItemsTypes = new List<SelectListItem>();
            ItemsTypes.Add(new SelectListItem { Text = "保险单位", Value = "保险单位" });
            ItemsTypes.Add(new SelectListItem { Text = "维修单位", Value = "维修单位" });
            ItemsTypes.Add(new SelectListItem { Text = "保养单位", Value = "保养单位" });
            ItemsTypes.Add(new SelectListItem { Text = "加油单位", Value = "加油单位" });
            ItemsTypes.Add(new SelectListItem { Text = "交警单位", Value = "交警单位" }); 
            ViewBag.ItemsType = ItemsTypes; 
            return base.Index();
        }
        public override ActionResult Add(CarDealings entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.Name == entity.Name).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("单位：{0}已经存在，不能重复添加", entity.Name), null);
            }

            var fristGood = this.m_ServiceBase.Query().Where(t => t.Code.Length > 0).OrderByDescending(T => T.Code).FirstOrDefault();
            if (fristGood == null || string.IsNullOrWhiteSpace(fristGood.Code))
            {
                entity.Code = "D000001";
            }
            else
            {
                var codeString = fristGood.Code.Substring(1, 6);
                var code = Convert.ToInt32(codeString) + 1;
                codeString = code.ToString().PadLeft(6, '0');
                entity.Code = "D" + codeString;
            }


            return base.Add(entity);
        }

        public override ActionResult Update(CarDealings CarDealings)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != CarDealings.ID && t.Name == CarDealings.Name).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("单位：{0}已经存在，不能重复添加", CarDealings.Name), null);
            }
            return base.Update(CarDealings);
        }
    }
}
