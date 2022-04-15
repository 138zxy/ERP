using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class OweMoneyController : BaseController<OweMoney,int?>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
        
        [HttpPost]
        public override ActionResult Add(OweMoney _OweMoney)
        {
            if (_OweMoney.SaleDate == null)
            {
                return OperateResult(false, "当前录入时间格式错误，请重新输入！", null);
            }
            DateTime saledate = Convert.ToDateTime(_OweMoney.SaleDate);
            DateTime startdate = new DateTime(saledate.Year, saledate.Month, saledate.Day);
            var lists = this.service.OweMoney.Query().Where(m => m.SaleDate >= startdate && m.SaleDate < startdate.AddDays(1)).OrderByDescending(m => m.BuildTime).ToList();
            if (lists.Count>0)
            {
                return OperateResult(false, "当前录入时间已经存在记录，请不要重复添加！", null);
            }
            ActionResult result = base.Add(_OweMoney);
            return result;
        }
        
        [HttpPost]
        public override ActionResult Update(OweMoney _OweMoney)
        {
            if (_OweMoney.SaleDate == null)
            {
                return OperateResult(false, "当前录入时间格式错误，请重新输入！", null);
            }
            DateTime saledate = Convert.ToDateTime(_OweMoney.SaleDate);
            DateTime startdate = new DateTime(saledate.Year, saledate.Month, saledate.Day);
            var lists = this.service.OweMoney.Query().Where(m => m.ID != _OweMoney.ID && m.SaleDate >= startdate && m.SaleDate < startdate.AddDays(1)).OrderByDescending(m => m.BuildTime).ToList();
            if (lists.Count > 0)
            {
                return OperateResult(false, "当前录入时间已经存在记录，请不要重复添加！", null);
            }
            ActionResult result = base.Update(_OweMoney);
            return result;
        }

        [HttpPost]
        public override ActionResult Delete(int?[] id)
        {
            ActionResult result = base.Delete(id);
            return result;
        }
   }
}
