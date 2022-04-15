using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PiaoInController : BaseController<SC_PiaoIn, string>
    {
        private object obj = new object();
        public override ActionResult Index()
        {
            ViewBag.Supplys = new SupplyChainHelp().GetSupply();
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            ViewBag.FinanceType = new SupplyChainHelp().GetFinanceTypes();
            List<SelectListItem> InTypes = new List<SelectListItem>();
            InTypes.Add(new SelectListItem { Text = SC_Common.InType.PurIn, Value = SC_Common.InType.PurIn });
            InTypes.Add(new SelectListItem { Text = SC_Common.InType.PurOut, Value = SC_Common.InType.PurOut });
            InTypes.Add(new SelectListItem { Text = SC_Common.InType.OtherIn, Value = SC_Common.InType.OtherIn });
            
            //InTypes.Add(new SelectListItem { Text = SC_Common.InType.LibIni, Value = SC_Common.InType.LibIni });
            ViewBag.InType = InTypes;
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
            return string.Format("{0}{1}", "L", orderNo);
        }

        public override ActionResult Add(SC_PiaoIn SC_PiaoIn)
        {
            if (SC_PiaoIn.SupplierID <= 0 && (SC_PiaoIn.InType==SC_Common.InType.PurOut || SC_PiaoIn.InType==SC_Common.InType.PurIn))
            {
                return OperateResult(false, "请选择供应商", null);
            }
            if (SC_PiaoIn.LibID <= 0)
            {
                return OperateResult(false, "请选择仓库", null);
            }
            if (SC_PiaoIn.InType == SC_Common.InType.LibIni)
            {
                return OperateResult(false, "库存初始化不能直接新增", null);
            }
            var query = this.service.GetGenericService<SC_PiaoIn>().Query().Where(t => t.InNo == SC_PiaoIn.InNo).FirstOrDefault();
            if (query != null)
            {
                SC_PiaoIn.InNo = GetGenerateOrderNo();
            }
            SC_PiaoIn.Condition = SC_Common.InStatus.Ini;
            return base.Add(SC_PiaoIn);
        }

        public override ActionResult Update(SC_PiaoIn SC_PiaoIn)
        {
            if (SC_PiaoIn.SupplierID <= 0 && (SC_PiaoIn.InType == SC_Common.InType.PurOut || SC_PiaoIn.InType == SC_Common.InType.PurIn))
            {
                return OperateResult(false, "请选择供应商", null);
            }
            if (SC_PiaoIn.LibID <= 0)
            {
                return OperateResult(false, "请选择仓库", null);
            }
            if (SC_PiaoIn.PurchaseID > 0)
            {
                return OperateResult(false, "当前单据为采购订单的入库单，不能修改入库方式", null);
            }
            var sc = this.m_ServiceBase.Get(SC_PiaoIn.ID);
            if (sc.Condition != SC_Common.InStatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            return base.Update(SC_PiaoIn);
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
            var zhangIns = this.service.GetGenericService<SC_ZhangIn>().Query().Where(t => t.InNo == orderNo);
            foreach (var q in zhangIns)
            {
                this.service.GetGenericService<SC_ZhangIn>().Delete(q);
            }
            return res;
        }

        /// <summary>
        /// 库存初始化操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PurInIni(string id)
        {
            lock (obj)
            {
                int no = 0;
                if (!int.TryParse(id, out no))
                {
                    return OperateResult(false, "请选择一个入库单进行操作", null);
                }
                if (no <= 0)
                {
                    return OperateResult(false, "请选择一个入库单进行操作", null);
                }
                var purIn = this.m_ServiceBase.Get(id);

                if (purIn.Condition == "已入库")
                {
                    return OperateResult(false, "不能重复入库，请刷新", null);
                }

                if (purIn.LibID <= 0)
                {
                    return OperateResult(false, "请选择仓库", null);
                }
                else
                {
                    string libId = purIn.LibID.ToString();
                    var lib = this.service.GetGenericService<SC_Lib>().Get(libId);
                    if (lib == null)
                    {
                        return OperateResult(false, "仓库不存在，请重新选择", null);
                    }
                    if (new SupplyChainHelp().IsPanDian(purIn.LibID))
                    {
                        return OperateResult(false, string.Format("仓库{0}正在盘点，无法进行操作",lib.LibName), null);
                    }
                }
                var piaoin = this.m_ServiceBase.Get(id);
                if (piaoin.SC_ZhangIns == null || piaoin.SC_ZhangIns.Count <= 0)
                {
                    return OperateResult(false, "请增加入库明细", null);
                }
                foreach (var p in piaoin.SC_ZhangIns)
                {
                    if (p.Quantity <= 0)
                    {
                        return OperateResult(false, string.Format("商品{0}的数量小于等于0，不能初始化", p.SC_Goods.GoodsName), null);
                    }
                }
            
                ResultInfo resultInfo = new ResultInfo();
                //库存初始化操作
                if (purIn.InType == SC_Common.InType.LibIni)
                {
                    resultInfo = this.service.SupplyChainService.InLibIni(id);
                }
                return Json(resultInfo);
            }
        }

        /// <summary>
        /// 采购审核入库的复杂操作
        /// </summary>
        /// <param name="SC_PiaoIn"></param>
        /// <returns></returns>
        public ActionResult PurIn(SC_PiaoIn SC_PiaoIn)
        {
            string id = SC_PiaoIn.PayInID.ToString();
            lock (obj)
            {
                int no = 0;
                if (!int.TryParse(id, out no))
                {
                    return OperateResult(false, "请选择一个入库单进行操作", null);
                }
                if (no <= 0)
                {
                    return OperateResult(false, "请选择一个入库单进行操作", null);
                }
                var purIn = this.m_ServiceBase.Get(id);
                
                if (purIn.Condition == "已入库")
                {
                    return OperateResult(false, "不能重复入库，请刷新", null);
                }

                if (purIn.LibID <= 0)
                {
                    return OperateResult(false, "入库前，请修改单据主体，选择相应的仓库", null);
                }
                else
                {
                    string libId = purIn.LibID.ToString();
                    var lib = this.service.GetGenericService<SC_Lib>().Get(libId);
                    if (lib == null)
                    {
                        return OperateResult(false, "仓库不存在，请重新选择", null);
                    }
                    if (new SupplyChainHelp().IsPanDian(purIn.LibID))
                    {
                        return OperateResult(false, string.Format("仓库{0}正在盘点，无法进行操作", lib.LibName), null);
                    }
                }
                if (purIn.InType == SC_Common.InType.PurOut)
                {
                    if (SC_PiaoIn.PayMoney > 0)
                    {
                        SC_PiaoIn.PayMoney = -SC_PiaoIn.PayMoney;
                    }
                    if (SC_PiaoIn.PayFavourable > 0)
                    {
                        SC_PiaoIn.PayFavourable = -SC_PiaoIn.PayFavourable;
                    }
                    if (SC_PiaoIn.PayOwing > 0)
                    {
                        SC_PiaoIn.PayOwing = -SC_PiaoIn.PayOwing;
                    }
                    //采购退货，需要考虑库存不足的问题

                    foreach (var q in purIn.SC_ZhangIns)
                    {
                        var nowlib = this.service.GetGenericService<SC_NowLib>().Query().Where(t => t.LibID == purIn.LibID && t.GoodsID == q.GoodsID  ).FirstOrDefault();
                        if (nowlib != null)
                        {
                            if (nowlib.Quantity < -q.Quantity)
                            {
                                return OperateResult(false, string.Format("供应商{0}的商品{1}库存不足,请通过其他方法处理！", purIn.SC_Supply.SupplierName, q.SC_Goods.GoodsName), null);
                            }
                        }
                    }
                }
                SC_PiaoIn.PayOwing = SC_PiaoIn.InMoney - SC_PiaoIn.PayMoney - SC_PiaoIn.PayFavourable;
                ResultInfo resultInfo = new ResultInfo();
                if (purIn.InType != SC_Common.InType.LibIni)
                {

                    if (purIn.PayType == SC_Common.PayType.AllOut)
                    {
                        SC_PiaoIn.PayMoney = 0;
                        SC_PiaoIn.PayOwing = SC_PiaoIn.InMoney - SC_PiaoIn.PayFavourable;
                    }
                    if (purIn.PayType == SC_Common.PayType.SupplyIn)
                    {
                        if (purIn.SupplierID <= 0)
                        {
                            return OperateResult(false, "请选择供应商", null);
                        }
                        var su = this.service.GetGenericService<SC_Supply>().Get(purIn.SupplierID.ToString());
                        if (su.PrePay < purIn.PayMoney)
                        {
                            return OperateResult(false, "当前供应商的预付款余额不足，无法支付", null);
                        }
                    }

                    resultInfo = this.service.SupplyChainService.PurInLib(SC_PiaoIn);
                }
                return Json(resultInfo);
            }
        }

        /// <summary>
        /// 取得下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        //public virtual JsonResult ListDataUnit(string GoodsID)
        //{
        //    return new SupplyChainHelp().ListDataUnit(GoodsID);
        //}

        /// <summary>
        /// 获取单位换算率
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        public virtual JsonResult GetUnitRate(string goodsId, string unit)
        {
            if (goodsId == "")
            {
                return OperateResult(false, "物资编码不存在，请重新选择", null);
            }
            if (unit == "")
            {
                return OperateResult(false, "单位不存在，请重新选择", null);
            }
            return new SupplyChainHelp().GetUnitRate(goodsId, unit);
        }
    }
}
