using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_LibController : BaseController<SC_Lib, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(SC_Lib SC_Lib)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.LibName == SC_Lib.LibName ).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("仓库：{0}已经存在，不能重复添加", SC_Lib.LibName), null);
            }
            return base.Add(SC_Lib);
        }

        public override ActionResult Update(SC_Lib SC_Lib)
        {
            var query = this.m_ServiceBase.Query().Where(t => t.ID != SC_Lib.ID && t.LibName == SC_Lib.LibName ).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, string.Format("仓库：{0}已经存在，不能重复添加", SC_Lib.LibName), null);
            }
            return base.Update(SC_Lib);
        }

        public override ActionResult Delete(string[] id)
        {
            foreach (var libid in id)
            {
                int lib = Convert.ToInt32(libid);
                var nowlib = this.service.GetGenericService<SC_NowLib>().Query().FirstOrDefault(t => t.LibID == lib);
                if (nowlib != null)
                {
                    return OperateResult(false, string.Format("当前仓库不能删除，已经存在库存数据"), null);
                }
            }
            return base.Delete(id);
        }
    }
}
