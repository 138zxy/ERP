using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;

namespace ZLERP.Web.Controllers.CarManage
{
    public class CarMaintainItemController : BaseController<CarMaintainItem, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(CarMaintainItem entity)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ItemName == entity.ItemName).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("单位：{0}已经存在，不能重复添加", entity.ItemName), null);
            }

            var fristGood = this.m_ServiceBase.Query().Where(t => t.Code.Length > 0).OrderByDescending(T => T.Code).FirstOrDefault();
            if (fristGood == null || string.IsNullOrWhiteSpace(fristGood.Code))
            {
                entity.Code = "M000001";
            }
            else
            {
                var codeString = fristGood.Code.Substring(1, 6);
                var code = Convert.ToInt32(codeString) + 1;
                codeString = code.ToString().PadLeft(6, '0');
                entity.Code = "M" + codeString;
            }


            return base.Add(entity);
        }

        public override ActionResult Update(CarMaintainItem CarMaintainItem)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != CarMaintainItem.ID && t.ItemName == CarMaintainItem.ItemName).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("单位：{0}已经存在，不能重复添加", CarMaintainItem.ItemName), null);
            }
            return base.Update(CarMaintainItem);
        }
    }
}
