using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_CirculateController : BaseController<SC_Fixed_Circulate, string>
    {
        public override ActionResult Index()
        {
            ViewBag.Users = new SupplyChainHelp().GetUser();
            ViewBag.Departments = new SupplyChainHelp().GetDepartment();
            return base.Index();
        }
        public ActionResult GenerateOrderNo()
        {
            string orderNo = GetGenerateOrderNo();
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public string GetGenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateOrderNo();
            return string.Format("{0}{1}", "C", orderNo);
        }
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_Fixed(q, textField, valueField, orderBy, ascending, condition);
        }

        public override ActionResult Add(SC_Fixed_Circulate entity)
        {
            var re = base.Add(entity);
            this.service.SupplyChainService.UpdateFixedCondition(entity.FixedID);
            return re;
        }

        public override ActionResult Update(SC_Fixed_Circulate entity)
        {
            if (entity.BorrowDate >= entity.BackDate)
            {
                return OperateResult(false, "借出时间不能大于归还时间", null);
            }
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
