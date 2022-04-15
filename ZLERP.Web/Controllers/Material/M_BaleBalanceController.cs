using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model.Material;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers.Material
{
    public class M_BaleBalanceController : BaseController<M_BaleBalance, string>
    {


        public override ActionResult Index()
        {
            List<SelectListItem> ItemsTypes = new List<SelectListItem>();
            ItemsTypes.Add(new SelectListItem { Text = "按结算重量", Value = "按结算重量" });
            ItemsTypes.Add(new SelectListItem { Text = "按厂商数量", Value = "按厂商数量" });
            ItemsTypes.Add(new SelectListItem { Text = "按方量", Value = "按方量" }); 
            ViewBag.ItemsType = ItemsTypes;
            return base.Index();
        }
        public override ActionResult Add(M_BaleBalance M_BaleBalance)
        {
            return base.Add(M_BaleBalance);
        }
        public override ActionResult Update(M_BaleBalance M_BaleBalance)
        {
            if (M_BaleBalance.IsOnePrice && M_BaleBalance.IsStockPrice)
            {
                return OperateResult(false, "只能选择按合同价结算或者按单一价结算其中一种方式", null);
            }
            return base.Update(M_BaleBalance);
        }

        public override ActionResult Delete(string[] id)
        {
            //这个删除的操作非常重要，一定要保持同步进行，主体删除，明细没有删除，入库单永远找不到为啥已标记为结算
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.DeleteBaleBalance(id);
            return Json(resultInfo);
        }

        public ActionResult ChangeCondition(string id, int type)
        {
            var query = this.m_ServiceBase.Get(id);
            if (type == 0)
            {
                query.AuditStatus = 1;
                query.AuditTime = DateTime.Now;
                query.Auditor = AuthorizationService.CurrentUserName;
                query.PayOwing = query.AllOkMoney;
                query.PiaoPayOwing = query.AllOkMoney;
            }
            else
            {
                //已经产生付款单的结算单，不能反审，这里需要添加逻辑

                //如果已经产生了付款单则不允许反审
                int OrderNo = Convert.ToInt32(id);
                var detiel = this.service.GetGenericService<M_YingSFrecdetail>().Query().Where(t => t.OrderNo == OrderNo).FirstOrDefault();
                if (detiel != null)
                {
                    return OperateResult(false, "当前结算单已经产生付款单，无法操作！", null);
                }
                query.AuditStatus = 0;
                query.AuditTime = null;
                query.Auditor = null;
                query.PayOwing = 0;
                query.PiaoPayOwing = 0;
            }
            query.Modifier = AuthorizationService.CurrentUserName;
            query.ModifyTime = DateTime.Now;
            this.m_ServiceBase.Update(query, null);
            if (query.AuditStatus == 1)
            {
                UpdateSupplyInfoPeyMoney(query.StockPactID, query.AllOkMoney);
            }
            else
            {
                UpdateSupplyInfoPeyMoney(query.StockPactID, -query.AllOkMoney);
            }
            return OperateResult(true, "操作成功", null);
        }

        /// <summary>
        /// 更新当前结算单的供货厂商的总付款金额
        /// </summary>
        /// <param name="id"></param>
        private void UpdateSupplyInfoPeyMoney(string SupplyID, decimal? money)
        {
            var su = this.service.GetGenericService<SupplyInfo>().Get(SupplyID);
            su.PayMoney = su.PayMoney + Convert.ToDecimal(money);  
            this.service.GetGenericService<SupplyInfo>().Update(su, null);
        }
        /// <summary>
        /// 重新计算，
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Compute(string id)
        {
            var bale = this.m_ServiceBase.Get(id);
            if (bale != null && bale.AuditStatus == 1)
            {
                return OperateResult(false, "已经审核的单据无法操作", null);
            }

            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.Compute(id);
            return Json(resultInfo);
        }

        /// <summary>
        /// 新增入库单到本结算单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult AddStuffIn(string id, List<string> keys)
        {
            if (string.IsNullOrWhiteSpace(id) || keys == null || keys.Count <= 0)
            {
                return OperateResult(false, "请选择相应的数据", null);
            }
            var bale = this.m_ServiceBase.Get(id);
            if (bale.AuditStatus == 1)
            {
                return OperateResult(false, "该结算单已经审核", null);
            }
            var query = this.service.GetGenericService<StuffIn>().Query().Where(t => keys.Contains(t.ID));
            foreach (var q in query)
            {
                if (q.StuffID != bale.StuffID || q.SupplyID != bale.StockPactID)
                {
                    return OperateResult(false, "选择待结算的进料单与主体结算单的合同号或材料不一致", null);
                }
            }
            //排除已经存在的
            var del = this.service.GetGenericService<M_BaleBalanceDel>().Query().Where(t => keys.Contains(t.StuffInID));
            if (del != null && del.Count() > 0)
            {
                foreach (var d in del)
                {
                    keys.Remove(d.StuffInID);
                }
            }
            if (keys.Count <= 0)
            {
                return OperateResult(false, "无有效的进来单数据", null);
            }
            foreach (var k in keys)
            {
                M_BaleBalanceDel BaleBalanceDel = new M_BaleBalanceDel();
                BaleBalanceDel.BaleBalanceID = Convert.ToInt32(id);
                BaleBalanceDel.StuffInID = k;
                StuffIn STU = this.service.GetGenericService<StuffIn>().Get(k);
                BaleBalanceDel.SpecID = STU.SpecID;
                this.service.GetGenericService<M_BaleBalanceDel>().Add(BaleBalanceDel);
            }
            //添加之后重新计算当前结算单
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.Compute(id);
            return Json(resultInfo);
        }

        /// <summary>
        /// 新增入库单到本结算单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult AddAllBalance(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperateResult(false, "请选择相应的数据", null);
            }
            var bale = this.m_ServiceBase.Get(id);
            if (bale.AuditStatus == 1)
            {
                return OperateResult(false, "该结算单已经审核", null);
            }
            string ChangeDay = base.service.SysConfig.GetSysConfig(SysConfigEnum.ChangeDay).ConfigValue;
            DateTime EndDay = Convert.ToDateTime(bale.InMonth + "-" + ChangeDay);
            DateTime StartDay = EndDay.AddMonths(-1);
            var query = this.service.GetGenericService<StuffIn>().Query().Where(t => t.SupplyID == bale.StockPactID && t.StuffID == bale.StuffID && t.InDate > StartDay && t.InDate <= EndDay);
            if (query == null)
            {
                return OperateResult(false, "选择的结算单没有需要添加的单据！", null);
            }
            foreach (var model in query)
            {
                if (!model.IsAccount && model.Lifecycle == 3 )
                {
                    M_BaleBalanceDel BaleBalanceDel = new M_BaleBalanceDel();
                    BaleBalanceDel.BaleBalanceID = Convert.ToInt32(id);
                    BaleBalanceDel.StuffInID = model.ID;
                    BaleBalanceDel.SpecID = model.SpecID;
                    this.service.GetGenericService<M_BaleBalanceDel>().Add(BaleBalanceDel);
                }
            }
            //添加之后重新计算当前结算单
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.Compute(id);
            return Json(resultInfo);
        }    
    }
}
