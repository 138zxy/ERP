using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class FormulaGHItemController : BaseController<FormulaGHItem, int>
    {


        public override ActionResult Add(FormulaGHItem ForGHItem)
        {
            return base.Add(ForGHItem);
        }


        public ActionResult IsEnableEdit(int id, decimal StuffAmount)
        {
            try
            {
                bool result = this.service.FormulaGHItem.IsEnableEdit(id, StuffAmount);
                return OperateResult(result, Lang.Msg_Operate_Success, result);

            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }
    }
}
