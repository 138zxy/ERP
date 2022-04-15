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
    public class ContractInvoiceGHController : BaseController<ContractInvoiceGH, int>
    {
        public override ActionResult Add(ContractInvoiceGH contractInvoice)
        {
            try
            {
                var addEntity = this.service.ContractInvoiceGH.Add(contractInvoice);
                return OperateResult(true, Lang.Msg_Operate_Success, addEntity);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    e = e.InnerException;
                log.Error(e.Message, e);
                return OperateResult(false, e.Message, null);
            }
        }

        public ActionResult Update(ContractInvoiceGH contractInvoice, System.Collections.Specialized.NameValueCollection form)
        {
            try
            {
                this.service.ContractInvoiceGH.Update(contractInvoice, form);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    e = e.InnerException;
                log.Error(e.Message, e);
                return OperateResult(false, e.Message, null);
            }
        }

        public ActionResult Delete(object[] id)
        {
            try
            {
                this.service.ContractInvoiceGH.Delete(ids: id);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    e = e.InnerException;
                log.Error(e.Message, e);
                return OperateResult(false, e.Message, null);
            }
        }

    }
}
