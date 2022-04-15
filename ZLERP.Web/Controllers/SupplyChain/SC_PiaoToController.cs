using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PiaoToController : BaseController<SC_PiaoTo, string>
    {

        private object obj = new object();
        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
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

        public override ActionResult Add(SC_PiaoTo SC_PiaoTo)
        {
            if (SC_PiaoTo.OutLibID <= 0)
            {
                return OperateResult(false, "请选择调出仓库", null);
            }
            if (SC_PiaoTo.InLibID <= 0)
            {
                return OperateResult(false, "请选择调入仓库", null);
            }
            if (SC_PiaoTo.OutLibID == SC_PiaoTo.InLibID)
            {
                return OperateResult(false, "调入仓库和调出仓库不能是同一个", null);
            }
            var query = this.service.GetGenericService<SC_PiaoTo>().Query().Where(t => t.ChangeNo == SC_PiaoTo.ChangeNo).FirstOrDefault();
            if (query != null)
            {
                SC_PiaoTo.ChangeNo = GetGenerateOrderNo();
            }
            SC_PiaoTo.Condition = SC_Common.InStatus.Ini;
            SC_PiaoTo.SC_LibIn = null;
            SC_PiaoTo.SC_LibOut = null;
            return base.Add(SC_PiaoTo);
        }

        public override ActionResult Update(SC_PiaoTo SC_PiaoTo)
        {
            if (SC_PiaoTo.OutLibID <= 0)
            {
                return OperateResult(false, "请选择调出仓库", null);
            }
            if (SC_PiaoTo.InLibID <= 0)
            {
                return OperateResult(false, "请选择调入仓库", null);
            }
            var sc = this.m_ServiceBase.Get(SC_PiaoTo.ID);
            if (sc.Condition != SC_Common.InStatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            return base.Update(SC_PiaoTo);
        }

        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个入库单进行删除", null);
            }
            var sc = this.m_ServiceBase.Get(id[0]);
            if (sc.Condition != SC_Common.InStatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            var res = base.Delete(id);
            //删除明细
            var orderNo = Convert.ToInt32(id[0]);
            var zhangIns = this.service.GetGenericService<SC_ZhangTo>().Query().Where(t => t.ChangeNo == orderNo);
            foreach (var q in zhangIns)
            {
                this.service.GetGenericService<SC_ZhangTo>().Delete(q);
            }
            return res;
        }
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_GoodsSample(q, textField, valueField, orderBy, ascending, condition); 
        }
        public ActionResult PurIn(string id)
        {
            lock (obj)
            {
                int no = 0;
                if (!int.TryParse(id, out no))
                {
                    return OperateResult(false, "请选择一个调拨单进行操作", null);
                }
                if (no <= 0)
                {
                    return OperateResult(false, "请选择一个调拨单进行操作", null);
                }
                var purTo = this.m_ServiceBase.Get(id);

                if (purTo.OutLibID <= 0 || purTo.InLibID <= 0 || purTo.OutLibID == purTo.InLibID)
                {
                    return OperateResult(false, "请确认仓库", null);
                }
                ResultInfo resultInfo = new ResultInfo();

                //判断是否满足库存
                var SC_ZhangTos = purTo.SC_ZhangTos.GroupBy(t => t.GoodsID).Select(t => new
                {
                    GoodsID = t.Key,
                    Quantity = t.Sum(z => z.Quantity * z.UnitRate)
                }).ToList();
                foreach (var q in SC_ZhangTos)
                {
                    var newLib = this.service.GetGenericService<SC_NowLib>().Query().Where(t => t.LibID == purTo.OutLibID && t.GoodsID == q.GoodsID).FirstOrDefault();
                    if (newLib.Quantity < q.Quantity)
                    {
                        return OperateResult(false, string.Format("仓库{0}当前供应商的商品{1}的库存{2}少于需要调拨的数量{3},不能调拨", purTo.SC_LibOut.LibName, newLib.SC_Goods.GoodsName, newLib.Quantity, q.Quantity), null);
                    }
                }
                if (purTo.Condition == SC_Common.InStatus.Ini)
                {
                    resultInfo = this.service.SupplyChainService.ChangeLibIn(id);
                }
                return Json(resultInfo);
            }
        }
    }
}
