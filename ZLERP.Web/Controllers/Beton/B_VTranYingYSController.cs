using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Model;
using System.Web.Script.Serialization;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_VTranYingYSController : BaseController<B_TranBalance, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Update(B_TranBalance B_TranBalance)
        {
            return OperateResult(true, "修改成功", null);
        }
    }
}
