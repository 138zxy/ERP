using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Web.Script.Serialization;
using ZLERP.Web.Helpers;
using ZLERP.JBZKZ12;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_GongYiController : BaseController<SC_GongYi, string>
    {

        private object obj = new object();
        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            return base.Index();
        }

        public ActionResult GetLib()
        {
            var libs = new SupplyChainHelp().GetLib();
            return OperateResult(true, "", libs);
        }

        public override ActionResult Add(SC_GongYi SC_GongYi)
        {
            return base.Add(SC_GongYi);
        }

        public override ActionResult Update(SC_GongYi SC_GongYi)
        {
            return base.Update(SC_GongYi);
        }

        public ActionResult UpdateUlr()
        {
            return OperateResult(true, "", null);
        }

        public override ActionResult Delete(string[] id)
        {
            var tid = Convert.ToInt32(id[0]);
            var res= base.Delete(id);
            var query = this.service.GetGenericService<SC_GongYiDetail>().Query().Where(t => t.GongYiID == tid);
            foreach (var q in query)
            {
                this.service.GetGenericService<SC_GongYiDetail>().Delete(q);
            }
            return res;
        }

        public ActionResult SearchChange(JqGridRequest request, string condition)
        {
            var con = condition.Split(',');
            string id = con[0];
            var FromSource = con[1];
            int num = Convert.ToInt32(con[2]);

            if (string.IsNullOrWhiteSpace(id))
            {
                return OperateResult(false, "请选择相应的加工模板", null);
            }
            if (num <= 0)
            {
                return OperateResult(false, "请输入加工数量", null);
            }
            int GongYiID = Convert.ToInt32(id);
            //查询模板数据
            var query = this.service.GetGenericService<SC_GongYiDetail>().Query().Where(t => t.GongYiID == GongYiID && t.FormSource == FromSource).ToList();

            foreach (var m in query)
            {
                m.Quantity = m.Quantity * num;
                m.AllMoney = m.Quantity * m.SC_Goods.LibPrice;
                m.Pirce = m.SC_Goods.LibPrice;
            }

            JqGridData<SC_GongYiDetail> data = new JqGridData<SC_GongYiDetail>()
            {
                page = 1,
                records = 100,
                pageSize = request.RecordsCount,
                rows = query
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };


        }

        public ActionResult ChangeInlib(string model)
        {
            PiaoChangeModel PiaoChangeModel = JsonHelper.Instance.Deserialize<PiaoChangeModel>(model);
            if (PiaoChangeModel == null)
            {
                return OperateResult(false, "参数错误", null);
            }
            if (PiaoChangeModel.id <= 0 || PiaoChangeModel.lib <= 0)
            {
                return OperateResult(false, "请选择入库仓库", null);
            }
            if (PiaoChangeModel.records3.Count <= 0 || PiaoChangeModel.records4.Count <= 0)
            {
                return OperateResult(false, "加工前后的商品不能为空", null);
            }
            foreach (var q in PiaoChangeModel.records3)
            {
                var lib = this.service.GetGenericService<SC_Lib>().Query().Where(t => t.LibName == q.LibID).FirstOrDefault();
                if (lib == null)
                {
                    return OperateResult(false, "加工前的商品请选择仓库", null);
                }
                int libID = Convert.ToInt32(lib.ID);
                q.lib = libID;
                var nowlib = this.service.GetGenericService<SC_NowLib>().Query().Where(t => t.LibID == libID && t.GoodsID == q.GoodsID).FirstOrDefault();
                if (nowlib != null)
                {
                    if (q.Quantity > nowlib.Quantity)
                    {
                        return OperateResult(false, string.Format("仓库{0}中的商品{1}库存不足，无法加工", lib.LibName, nowlib.SC_Goods.GoodsName), null);
                    }
                }
                else
                {
                    return OperateResult(false, string.Format("仓库不存在此商品"), null);
                }
            }
            ResultInfo resultInfo = new ResultInfo(); 
            resultInfo = this.service.SupplyChainService.ChangeInlib(PiaoChangeModel); 
            return Json(resultInfo);
        }
    }
}
