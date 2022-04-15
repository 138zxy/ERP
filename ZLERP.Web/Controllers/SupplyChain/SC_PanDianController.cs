using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PanDianController : BaseController<SC_PanDian, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            return base.Index();
        }

        public override ActionResult Add(SC_PanDian SC_PanDian)
        {
            if (SC_PanDian.LibID <= 0)
            {
                return OperateResult(false, "请选择仓库进行盘点，请先处理", null);
            }
            var query = this.m_ServiceBase.Query().Where(t => t.LibID == SC_PanDian.LibID && t.IsOff == false).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前仓库还有未修正的盘点单，请先处理", null);
            }
            ResultInfo resultInfo = new ResultInfo();

            resultInfo = this.service.SupplyChainService.InsertPanDian(SC_PanDian);

            return Json(resultInfo);
        }


        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个入库单进行删除", null);
            }
            var sc = this.m_ServiceBase.Get(id[0]);
            if (sc.IsOff)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            var res = base.Delete(id);
            //删除明细
            var orderNo = Convert.ToInt32(id[0]);
            var zhangIns = this.service.GetGenericService<SC_PanDianDetail>().Query().Where(t => t.PanID == orderNo);
            foreach (var q in zhangIns)
            {
                this.service.GetGenericService<SC_PanDianDetail>().Delete(q);
            }
            return res;
        }
        public ActionResult PanDianIn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return OperateResult(false, "请选择仓库进行盘点，请先处理", null);
            }
            var query = this.m_ServiceBase.Query().Where(t => t.ID == id).FirstOrDefault();
            if (query == null)
            {
                return OperateResult(false, "当前盘点单不存在，请先处理", null);
            }
            else
            {
                if (query.IsOff)
                {
                    return OperateResult(false, "当前盘点单已经修正入库,不能再次处理", null);
                }
            }
            ResultInfo resultInfo = new ResultInfo();

            resultInfo = this.service.SupplyChainService.PanDianIn(id);

            return Json(resultInfo);
        }
    }
}
