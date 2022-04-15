using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PiaoOutOrderController : BaseController<SC_PiaoOutOrder, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.Users = new SupplyChainHelp().GetUser();
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            return base.Index();
        }

        public ActionResult GetDepart(string userid)
        {
            var user = this.service.GetGenericService<ZLERP.Model.User>().Get(userid);
            if (user.Department != null)
            {
                return OperateResult(true, "操作成功", user.Department);
            }
            else
            {
                return OperateResult(true, "操作成功", "");
            }
        }
        public ActionResult GenerateOrderNo()
        {
            string orderNo = GetGenerateOrderNo();
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public string GetGenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateOrderNo();
            return string.Format("{0}{1}", "U", orderNo);
        }

        public override ActionResult Add(SC_PiaoOutOrder SC_PiaoOutOrder)
        {
            if (string.IsNullOrWhiteSpace(SC_PiaoOutOrder.UserID))
            {
                return OperateResult(false, "请选择使用人", "");
            }
            var query = this.service.GetGenericService<SC_PiaoOutOrder>().Query().Where(t => t.OrderNo == SC_PiaoOutOrder.OrderNo).FirstOrDefault();
            if (query != null)
            {
                SC_PiaoOutOrder.OrderNo = GetGenerateOrderNo();
            }
            SC_PiaoOutOrder.Department = null;
            SC_PiaoOutOrder.Condition = SC_Common.Pstatus.Ini;
            return base.Add(SC_PiaoOutOrder);
        }

        public override ActionResult Update(SC_PiaoOutOrder SC_PiaoOutOrder)
        {
           
            var sc = this.m_ServiceBase.Get(SC_PiaoOutOrder.ID);
            if (sc.Condition != SC_Common.Pstatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            return base.Update(SC_PiaoOutOrder);
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
                        var inOrder = this.service.GetGenericService<SC_PiaoOut>().Query().Where(t => t.OutOrderID == pid).FirstOrDefault();
                        if (inOrder != null)
                        {
                            return OperateResult(false, "该申请单已经产生出库单，不能反审", null);
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
        /// 申请出库(这里需要使用事物来控制)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PurOut(int id, int libid)
        {
            var purOrder = this.service.GetGenericService<SC_ZhangOutOrder>().Query().Where(t => t.OrderNo == id).ToList();
            //1.查出为出库的申请的数量
            var purOrder1 = purOrder.Where(t => t.Quantity - t.InQuantity - t.WaitQuantity > 0).ToList();
            if (purOrder1 == null || purOrder1.Count <= 0)
            {
                return OperateResult(false, "当前申请单已经全部生成出库单，请核准", null);
            }
            var goodsid = purOrder1.Select(t => t.GoodsID).ToList();
            var nowlib = (this.service.GetGenericService<SC_NowLib>().Query().Where(t => t.LibID == libid && t.Quantity > 0 && goodsid.Contains(t.GoodsID))).ToList();

            var puroutOrder = this.m_ServiceBase.Get(id.ToString());

            //2.生成出库库单
            SC_PiaoOut PiaoOut = new SC_PiaoOut();
            PiaoOut.LibID = libid;
            PiaoOut.OutType = SC_Common.OutType.QurOut;
            PiaoOut.OutDate = DateTime.Now;
            string OutNo = new SC_PiaoOutController().GetGenerateOrderNo();
            PiaoOut.OutNo = OutNo;
            PiaoOut.OutOrderID = id;
            PiaoOut.DepartmentID = puroutOrder.DepartmentID;
            PiaoOut.UserID = puroutOrder.UserID;
            PiaoOut.VarietyNum = 0; //要算
            PiaoOut.OutMoney = 0; //要算
            PiaoOut.PayType = SC_Common.OutType.QurOut;
            PiaoOut.Condition = SC_Common.InStatus.Ini;
            PiaoOut.Remark = "申领出库";

            //生成出库明细
            List<SC_ZhangOut> ZhangOuts = new List<SC_ZhangOut>();

            var lQuantity = 0.00m;
            var lgoodID = 0;
            var libg = new SC_NowLib();
            foreach (var p in purOrder1.OrderBy(t=>t.GoodsID).ToList())
            {
                if (lgoodID != p.GoodsID)
                {
                    lgoodID = p.GoodsID;
                    lQuantity = 0.00m;
                    libg = nowlib.Where(t => t.GoodsID == p.GoodsID).OrderByDescending(t => t.Quantity).FirstOrDefault();
                    lQuantity = libg.Quantity;
                }
                int i = 0;
                var UnQuantity = (p.Quantity - p.InQuantity - p.WaitQuantity); 
                if (libg != null)
                {
                    if (UnQuantity <= 0)
                    {
                        continue;
                    }
                    if (lQuantity >= UnQuantity * p.UnitRate)
                    {
                        SC_ZhangOut ZhangOut = new SC_ZhangOut();
                        ZhangOut.GoodsID = p.GoodsID;
                        ZhangOut.Sequence = i++;
                        ZhangOut.OutDate = DateTime.Now;
                        ZhangOut.Quantity = UnQuantity;
                        ZhangOut.PriceUnit = libg.PirceUnit * p.UnitRate;
                        ZhangOut.OutMoney = ZhangOut.Quantity * ZhangOut.PriceUnit;
                        ZhangOut.UnitRate = p.UnitRate;
                        ZhangOut.Unit = p.Unit;
                        ZhangOuts.Add(ZhangOut);
                        lQuantity = lQuantity - ZhangOut.Quantity * p.UnitRate;
                    }
                    else
                    {
                        decimal b = lQuantity / p.UnitRate;
                        if (b <= 0)
                        {
                            continue;
                        }
                        SC_ZhangOut ZhangOut = new SC_ZhangOut();
                        ZhangOut.GoodsID = p.GoodsID;
                        ZhangOut.Sequence = i++;
                        ZhangOut.OutDate = DateTime.Now;
                        ZhangOut.Quantity = b;
                        ZhangOut.PriceUnit = libg.PirceUnit * p.UnitRate;
                        ZhangOut.OutMoney = ZhangOut.Quantity * ZhangOut.PriceUnit;
                        ZhangOut.UnitRate = p.UnitRate;
                        ZhangOut.Unit = p.Unit;
                        ZhangOuts.Add(ZhangOut);
                        lQuantity = lQuantity - ZhangOut.Quantity * p.UnitRate;
                    }
                   
                }
            }
            PiaoOut.VarietyNum = ZhangOuts.GroupBy(t=>t.GoodsID).Count();
            PiaoOut.OutMoney = ZhangOuts.Sum(t => t.OutMoney);
            var result = this.service.SupplyChainService.PurOut(PiaoOut, ZhangOuts);
            return Json(result);
        }


        /// <summary>
        /// 选择仓库的时候需要判断是否能生成这么多。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libid"></param>
        /// <returns></returns>
        public ActionResult IsCanOut(int id, int libid)
        {
            if (id <= 0)
            {
                return OperateResult(false, "请选择相应的单据", null);
            }
            if (libid <= 0)
            {
                return OperateResult(false, "请选择相应的仓库进行出库", null);
            }
            var pur = this.m_ServiceBase.Get(id.ToString());
            if (pur.Condition == SC_Common.Pstatus.Completed)
            {
                return OperateResult(false, "当前申请单已经完成", null);
            }
            var purOrder = this.service.GetGenericService<SC_ZhangOutOrder>().Query().Where(t => t.OrderNo == id).ToList();
            //1.查出为出库的申请的数量
            var purOrder1 = purOrder.Where(t => t.Quantity - t.InQuantity - t.WaitQuantity > 0).ToList();
            if (purOrder1 == null || purOrder1.Count <= 0)
            {
                return OperateResult(false, "当前申请单已经全部生产入库单，请核准", null);
            }
            var goodsid = purOrder1.Select(t => t.GoodsID).ToList();
            var nowlib = (this.service.GetGenericService<SC_NowLib>().Query().Where(t => t.LibID == libid && t.Quantity>0 && goodsid.Contains(t.GoodsID)).GroupBy(t => t.GoodsID).Select(t => new
            {
                GoodsID = t.Key,
                Quantity = t.Sum(s => s.Quantity)
            })).ToList();
            if (nowlib == null || nowlib.Count <= 0)
            {
                return OperateResult(false, "当前申请单申请物品没有库存，请核准", null);
            }
            var listZout = purOrder1.GroupBy(t => t.GoodsID).Select(t => new
            {
                GoodsID = t.Key,
                Quantity = t.Sum(z => z.Quantity * z.UnitRate)
            }).ToList();
            foreach (var p in listZout)
            {
                var nowl = nowlib.Where(t => t.GoodsID == p.GoodsID).FirstOrDefault();
                if (nowl == null || (nowl != null && nowl.Quantity < p.Quantity))
                {
                    return OperateResult(true, "当前生成的商品只有部分有库存，只能生成部分出库明细，请确认", 1);
                }
               
            }
            return OperateResult(true, "", 0);
        }
    }
}
