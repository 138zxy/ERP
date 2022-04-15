using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PiaoInOrderController : BaseController<SC_PiaoInOrder, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.Supplys = new SupplyChainHelp().GetSupply();
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
            return string.Format("{0}{1}", "P", orderNo);
        }

        public override ActionResult Add(SC_PiaoInOrder SC_PiaoInOrder)
        {
            if (SC_PiaoInOrder.SupplierID <= 0)
            {
                return OperateResult(false, "请选择供应商", null);
            }
            var query = this.service.GetGenericService<SC_PiaoInOrder>().Query().Where(t => t.OrderNo == SC_PiaoInOrder.OrderNo).FirstOrDefault();
            if (query != null)
            {
                SC_PiaoInOrder.OrderNo = GetGenerateOrderNo();
            }
            SC_PiaoInOrder.Condition = SC_Common.Pstatus.Ini;
            return base.Add(SC_PiaoInOrder);
        }

        public override ActionResult Update(SC_PiaoInOrder SC_PiaoInOrder)
        {
            if (SC_PiaoInOrder.SupplierID <= 0)
            {
                return OperateResult(false, "请选择供应商", null);
            }
            var sc = this.m_ServiceBase.Get(SC_PiaoInOrder.ID);
            if (sc.Condition != SC_Common.Pstatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            return base.Update(SC_PiaoInOrder);
        }

        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个采购单进行删除", null);
            }
            var sc = this.m_ServiceBase.Get(id[0]);
            if (sc.Condition != SC_Common.Pstatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            var res = base.Delete(id);
            //删除明细
            var orderNo = Convert.ToInt32(id[0]);
            var zhangIns = this.service.GetGenericService<SC_ZhangInOrder>().Query().Where(t => t.OrderNo == orderNo);
            foreach (var q in zhangIns)
            {
                this.service.GetGenericService<SC_ZhangInOrder>().Delete(q);
            }
            return res;
        }

        /// <summary>
        /// 状态变化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeCondition(int type, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return OperateResult(false, "请选择相应的单据", null);
            }
            int status = 0;
            switch (type)
            {
                case 1: //审核
                    if (true)
                    {
                        var sc = this.m_ServiceBase.Get(id);
                        if (sc.Condition != SC_Common.Pstatus.Ini)
                        {
                            return OperateResult(false, "该单据不是草稿状态不能审核", null);
                        }
                        sc.Condition = SC_Common.Pstatus.Audit;
                        sc.Auditor = AuthorizationService.CurrentUserID;
                        sc.AuditTime = DateTime.Now;
                        this.m_ServiceBase.Update(sc, null);
                        status = 0;
                    }
                    break;
                case 2: //反审
                    if (true)
                    {
                        var sc = this.m_ServiceBase.Get(id);
                        if (sc.Condition != SC_Common.Pstatus.Audit)
                        {
                            return OperateResult(false, "该单据不是已审核状态不能反审", null);
                        }
                        var pid = Convert.ToInt32(id);
                        var inOrder = this.service.GetGenericService<SC_PiaoIn>().Query().Where(t => t.PurchaseID == pid).FirstOrDefault();
                        if (inOrder != null)
                        {
                            return OperateResult(false, "该采购单已经产生入库单，不能反审", null);
                        }
                        sc.Condition = SC_Common.Pstatus.Ini;
                        sc.Auditor = "";
                        sc.AuditTime = null;
                        this.m_ServiceBase.Update(sc, null);
                        status = 0;
                    }
                    break;
                case 3://完成
                    if (true)
                    {
                        var sc = this.m_ServiceBase.Get(id);
                        if (sc.Condition != SC_Common.Pstatus.Audit)
                        {
                            return OperateResult(false, "该单据不是已审核状态不能点击完成", null);
                        }
                        sc.Condition = SC_Common.Pstatus.Completed;
                        this.m_ServiceBase.Update(sc, null);
                        status = 0;
                    }
                    break;
                case 4:
                    if (true)
                    {
                        var sc = this.m_ServiceBase.Get(id);
                        if (sc.Condition != SC_Common.Pstatus.Completed)
                        {
                            return OperateResult(false, "该单据不是完成状态不能点击反完成", null);
                        }
                        sc.Condition = SC_Common.Pstatus.Audit;
                        this.m_ServiceBase.Update(sc, null);
                        status = 0;
                    }
                    break;
                default:
                    status = 1;
                    break;

            }
            if (status == 0)
            {
                return OperateResult(true, "操作成功", null);
            }
            else
            {
                return OperateResult(false, "参数错误", null);
            }
        }


        /// <summary>
        /// 采购入库(这里需要使用食物来控制)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PurIn(int id)
        {
            if (id <= 0)
            {
                return OperateResult(false, "请选择相应的单据", null);
            }
            var pur = this.m_ServiceBase.Get(id.ToString());
            var purOrder = this.service.GetGenericService<SC_ZhangInOrder>().Query().Where(t => t.OrderNo == id).ToList();
            //1.查出为入库的采购的数量
            var purOrder1 = purOrder.Where(t => t.Quantity - t.InQuantity - t.WaitQuantity > 0).ToList();
            if (purOrder1 == null || purOrder1.Count <= 0)
            {
                return OperateResult(false, "当前采购单已经全部生产入库单，请核准", null);
            }
            //2.生成入库单
            SC_PiaoIn PiaoIn = new SC_PiaoIn();
            PiaoIn.PurchaseID = id;
            PiaoIn.SupplierID = pur.SupplierID;
            PiaoIn.LibID = pur.LibID ?? 0;
            PiaoIn.InType = SC_Common.InType.PurIn;
            PiaoIn.InDate = DateTime.Now;
            string InNo = new SC_PiaoInController().GetGenerateOrderNo();
            PiaoIn.InNo = InNo;
            PiaoIn.Purchase = pur.Operator;
            PiaoIn.VarietyNum = 0; //要算
            PiaoIn.InMoney = 0; //要算
            PiaoIn.PayType = pur.SC_Supply.FinanceType;
            PiaoIn.Condition = SC_Common.InStatus.Ini;
            PiaoIn.Remark = "采购入库";

            int i = 0;
            //3.生产入库单明细
            var ZhangIns = (from q in purOrder1
                            select new SC_ZhangIn
                            {
                                GoodsID = q.GoodsID,
                                Sequence = i++,
                                Indate = DateTime.Now,
                                Quantity = q.Quantity - q.InQuantity - q.WaitQuantity,
                                PriceUnit = q.UnitPrice,
                                InMoney = (q.Quantity - q.InQuantity - q.WaitQuantity) * q.UnitPrice,
                                PiNo = "",
                                Unit = q.Unit,
                                UnitRate = q.UnitRate
                            }).ToList();

            PiaoIn.VarietyNum = ZhangIns.GroupBy(t => t.GoodsID).Count();
            PiaoIn.InMoney = ZhangIns.Sum(t => t.InMoney);
            var result = this.service.SupplyChainService.PurIn(PiaoIn, ZhangIns);
            return Json(result);
        }
    }
}
