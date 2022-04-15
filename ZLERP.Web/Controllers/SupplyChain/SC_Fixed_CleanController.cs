using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain; 
namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_CleanController : BaseController<SC_Fixed_Clean, string>
    {
        public override ActionResult Index()
        {
            ViewBag.CleanType = new SupplyChainHelp().GetCleanType();
            return base.Index();
        }

        /// <summary>
        /// 生产订单号
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateNo("FC");
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public override ActionResult Add(SC_Fixed_Clean entity)
        {
            var re = base.Add(entity);
            this.service.SupplyChainService.UpdateFixedCondition(entity.FixedID);
            return re;
        }

        public override ActionResult Update(SC_Fixed_Clean entity)
        {
         
            var re = base.Update(entity);

            this.service.SupplyChainService.UpdateFixedCondition(entity.FixedID);

            return re;
        }
        public override ActionResult Delete(string[] id)
        {
            var fix = this.m_ServiceBase.Get(id[0]);
            var re = base.Delete(id);
            this.service.SupplyChainService.UpdateFixedCondition(fix.FixedID);
            return re;
        }
    }
}
