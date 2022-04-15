using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PiaoOutController : BaseController<SC_PiaoOut, string>
    {
        //
        // GET: /SC_Base/

        private object obj = new object();
        public override ActionResult Index()
        {
      
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            ViewBag.Users = new SupplyChainHelp().GetUser();
            List<SelectListItem> OutTypes = new List<SelectListItem>(); 
            OutTypes.Add(new SelectListItem { Text = SC_Common.OutType.OtherIn, Value = SC_Common.OutType.OtherIn });
            OutTypes.Add(new SelectListItem { Text = SC_Common.OutType.QurOut, Value = SC_Common.OutType.QurOut });
            OutTypes.Add(new SelectListItem { Text = SC_Common.OutType.MaintainOut, Value = SC_Common.OutType.MaintainOut });
            OutTypes.Add(new SelectListItem { Text = SC_Common.OutType.MaintainBack, Value = SC_Common.OutType.MaintainBack });
            ViewBag.OutType = OutTypes;
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
            return string.Format("{0}{1}", "Z", orderNo);
        }

        public override ActionResult Add(SC_PiaoOut SC_PiaoOut)
        {
       
            if (SC_PiaoOut.LibID <= 0)
            {
                return OperateResult(false, "请选择仓库", null);
            } 
            var query = this.service.GetGenericService<SC_PiaoOut>().Query().Where(t => t.OutNo == SC_PiaoOut.OutNo).FirstOrDefault();
            if (query != null)
            {
                SC_PiaoOut.OutNo = GetGenerateOrderNo();
            }
            if (SC_PiaoOut.OutType == SC_Common.OutType.OtherIn)
            {
                SC_PiaoOut.PayType = SC_Common.OutType.OtherIn;
            }
            SC_PiaoOut.PayType = SC_PiaoOut.OutType;
            SC_PiaoOut.Department = null;
            SC_PiaoOut.SC_Lib = null;
            SC_PiaoOut.Condition = SC_Common.InStatus.Ini;
            return base.Add(SC_PiaoOut);
        }

        public override ActionResult Update(SC_PiaoOut SC_PiaoOut)
        {
            
            if (SC_PiaoOut.LibID <= 0)
            {
                return OperateResult(false, "请选择仓库", null);
            }
            if (SC_PiaoOut.OutType != SC_Common.OutType.QurOut && SC_PiaoOut.OutOrderID > 0)
            {
                return OperateResult(false, "当前单据为申领出库单，不能修改出库方式", null);
            }
            var sc = this.m_ServiceBase.Get(SC_PiaoOut.ID);
            if (sc.Condition != SC_Common.InStatus.Ini)
            {
                return OperateResult(false, "不在草稿状态，不能做此操作", null);
            }
            SC_PiaoOut.PayType = SC_PiaoOut.OutType;
            return base.Update(SC_PiaoOut);
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

        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
           // return new SupplyChainHelp().GetSC_LibGoods(q, textField, valueField, orderBy, ascending, condition);
            return new SupplyChainHelp().GetSC_Goods(q, textField, valueField, orderBy, ascending, condition); 
        }


        /// <summary>
        /// 采购审核出库的复杂操作
        /// </summary>
        /// <param name="SC_PiaoOut"></param>
        /// <returns></returns>
        public ActionResult PurOut(string id)
        {

            lock (obj)
            {
                int no = 0;
                if (!int.TryParse(id, out no))
                {
                    return OperateResult(false, "请选择一个出库单进行操作", null);
                }
                if (no <= 0)
                {
                    return OperateResult(false, "请选择一个出库单进行操作", null);
                }
                var purOut = this.m_ServiceBase.Get(id);

                if (purOut.LibID <= 0)
                {
                    return OperateResult(false, "请选择仓库", null);
                }
                else
                {
                    string libId = purOut.LibID.ToString();
                    var lib = this.service.GetGenericService<SC_Lib>().Get(libId);
                    if (lib == null)
                    {
                        return OperateResult(false, "仓库不存在，请重新选择", null);
                    }
                    if (new SupplyChainHelp().IsPanDian(purOut.LibID))
                    {
                        return OperateResult(false, string.Format("仓库{0}正在盘点，无法进行操作", lib.LibName), null);
                    }
                }
                if (purOut.SC_ZhangOuts == null || purOut.SC_ZhangOuts.Count <= 0)
                {
                    return OperateResult(false, "没有出库明细", null);
                }
                foreach (var o in purOut.SC_ZhangOuts)
                {
                    if (purOut.OutType!=SC_Common.OutType.MaintainBack  && o.Quantity <= 0)
                    {
                        return OperateResult(false, "申领数量必须大于0", null);
                    }
                }
                //判断库存不足的问题
                List<int> goodsid = purOut.SC_ZhangOuts.Select(t => t.GoodsID).ToList();
                var nowlib = this.service.GetGenericService<SC_NowLib>().Query().Where(t => t.LibID == purOut.LibID && goodsid.Contains(t.GoodsID)).ToList();
                var listZout = purOut.SC_ZhangOuts.GroupBy(t => t.GoodsID).Select(t => new
                {
                    GoodsID = t.Key,
                    Quantity = t.Sum(z => z.Quantity * z.UnitRate)
                }).ToList();
                foreach (var o in listZout)
                {
                    var lib = nowlib.Where(t => t.GoodsID == o.GoodsID && t.LibID == purOut.LibID).FirstOrDefault();
                    if (lib == null || (lib.Quantity < o.Quantity))
                    {
                        string goodsname = purOut.SC_ZhangOuts.Where(t => t.GoodsID == o.GoodsID).FirstOrDefault().SC_Goods.GoodsName;
                        return OperateResult(false, string.Format("申请的仓库{0} 商品{1}不存在库存或者库存不足，无法出库", purOut.SC_Lib.LibName, goodsname), null);
                    }
                }
                ResultInfo resultInfo = new ResultInfo();
                resultInfo = this.service.SupplyChainService.PurOutLib(id);
                return Json(resultInfo);
            }
        }
    }
}
