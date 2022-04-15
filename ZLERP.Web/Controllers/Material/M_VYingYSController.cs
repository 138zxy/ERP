using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Material;
using ZLERP.Model;
using System.Web.Script.Serialization;

namespace ZLERP.Web.Controllers.Material
{
    public class M_VYingYSController : BaseController<M_BaleBalance, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Update(M_BaleBalance M_BaleBalance)
        {
            return OperateResult(true, "修改成", null);
        }
    }
}
