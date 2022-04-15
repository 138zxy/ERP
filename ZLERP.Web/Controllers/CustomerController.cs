using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class CustomerController : BaseController<Customer,string>
    {
        public override ActionResult Index()
        {
            var buyerList = this.service.GetGenericService<User>().All("", "ID", true);
            ViewBag.BuyerList = new SelectList(buyerList, "ID", "TrueName");
            return base.Index();
        }
        public ActionResult Contract() {
            return View();
        }

        public ActionResult EnableAll(string[] ids)
        {
            bool ret = this.service.Customer.EnableAll(ids, true);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
        }
        public ActionResult DisableAll(string[] ids)
        {
            bool ret = this.service.Customer.EnableAll(ids, false);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
        }
        public override ActionResult Add(Customer entity)
        {
            entity.CustName = entity.CustName.Trim();
            var cust = this.m_ServiceBase.Query().Where(t => t.CustName == entity.CustName).FirstOrDefault();
            if (cust != null)
            {
                return OperateResult(false,string.Format("已存在客户名称为{0}的数据，请检查",entity.CustName),null);
            }
            return base.Add(entity);
        }

        public override ActionResult Update(Customer entity)
        {
            entity.CustName = entity.CustName.Trim();
            var cust = this.m_ServiceBase.Query().Where(t => t.CustName == entity.CustName && t.ID != entity.ID).FirstOrDefault();
            if (cust != null)
            {
                return OperateResult(false, string.Format("已存在客户名称为{0}的数据，请检查", entity.CustName), null);
            }
            return base.Update(entity);
        }

        public ActionResult ContractPrice()
        {
            base.InitCommonViewBag();
            return View();
        }
    }
}
