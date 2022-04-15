
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Resources;
using System.Collections.Specialized;

namespace ZLERP.Web.Controllers
{
    public class StuffSellController : BaseController<StuffSell, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);

            return base.Index();
        }

        public override System.Web.Mvc.ActionResult Add(StuffSell entity)
        {
            try
            {
                var addedEntity = this.service.StuffSell.Add(entity);
                return OperateResult(true, Lang.Msg_Operate_Success, addedEntity.ID);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, ex.Message);
            }
        }


        public  System.Web.Mvc.ActionResult Update(NameValueCollection form)
        {
            try
            {
                this.service.StuffSell.Update(form);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, ex.Message);
            }
        }


        public override System.Web.Mvc.ActionResult Delete(string[] id)
        {
            try
            {
                this.service.StuffSell.Delete(id);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, ex.Message);
            }
        }







    }    
}
