using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_PonTranBalanceController : BaseController<B_TranBalance, string>
    {


        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(B_TranBalance B_TranBalance)
        {
            return base.Add(B_TranBalance);
        }
        public override ActionResult Update(B_TranBalance B_TranBalance)
        {
            if (B_TranBalance.IsOnePrice && B_TranBalance.IsStockPrice)
            {
                return OperateResult(false, "只能选择按系统结算或者按手动结算其中一种方式", null);
            }
            DateTime dt;
            bool reslut = false;
            reslut = DateTime.TryParse(B_TranBalance.InMonth + "-01", out dt);
            if (!reslut)
            {
                return OperateResult(false, "纳入月份请输入正确的格式", null);
            }
            return base.Update(B_TranBalance);
        }

        public override ActionResult Delete(string[] id)
        {
            //这个删除的操作非常重要，一定要保持同步进行，主体删除，明细没有删除，入库单永远找不到为啥已标记为结算
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.DeleteTranBalance(id);
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
            }
            else
            {
                //已经产生付款单的结算单，不能反审，这里需要添加逻辑

                //如果已经产生了付款单则不允许反审
                int OrderNo = Convert.ToInt32(id);
                var detiel = this.service.GetGenericService<B_TranYingSFrecdetail>().Query().Where(t => t.OrderNo == OrderNo).FirstOrDefault();
                if (detiel != null)
                {
                    return OperateResult(false, "当前结算单已经产生付款单，无法操作！", null);
                }
                query.AuditStatus = 0;
                query.AuditTime = null;
                query.Auditor = null;
                query.PayOwing = 0;
            }
            query.Modifier = AuthorizationService.CurrentUserName;
            query.ModifyTime = DateTime.Now;
            this.m_ServiceBase.Update(query, null);
            if (query.AuditStatus == 1)
            {
                UpdateSupplyInfoPeyMoney(query.TranID, query.AllOkMoney);
            }
            else
            {
                UpdateSupplyInfoPeyMoney(query.TranID, -query.AllOkMoney);
            }
            return OperateResult(true, "操作成功", null);
        }

        /// <summary>
        /// 更新当前结算单的供货厂商的总付款金额
        /// </summary>
        /// <param name="id"></param>
        private void UpdateSupplyInfoPeyMoney(int TranID, decimal? money)
        {
            if (TranID > 0)
            {
                var su = this.service.GetGenericService<B_CarFleet>().Get(TranID.ToString());
                su.PayMoney = su.PayMoney + Convert.ToDecimal(money);
                this.service.GetGenericService<B_CarFleet>().Update(su, null);
            }
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
            resultInfo = this.service.BetonService.ComputePeCar(id);
            return Json(resultInfo);
        }

        /// <summary>
        /// 新增入库单到本结算单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult AddShipDoc(string id, List<string> keys)
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
            var query = this.service.GetGenericService<ShippingDocument>().Query().Where(t => keys.Contains(t.ID));
            foreach (var q in query)
            {
                if (q.ProjectID != bale.ProjectID)
                {
                    return OperateResult(false, "选择待结算的工地与主体结算单的工地不一致", null);
                }
                //保持车牌号与运输车队一致
                var car = this.service.GetGenericService<Car>().Get(q.PumpName);
                if (car.RentCarName != bale.TranID)
                {
                    return OperateResult(false, "选择待结算的运输单与主体结算单的车队信息不一致", null);
                }
            }
            //排除已经存在的
            var del = this.service.GetGenericService<B_TranBalanceDel>().Query().Where(t => keys.Contains(t.ShipDocID) && t.ModelType == ZLERP.Model.Beton.B_Common.ModelType.PeCar);
            if (del != null && del.Count() > 0)
            {
                foreach (var d in del)
                {
                    keys.Remove(d.ShipDocID);
                }
            }
            if (keys.Count <= 0)
            {
                return OperateResult(false, "无有效的进来单数据", null);
            }
            foreach (var k in keys)
            {
                B_TranBalanceDel BaleBalanceDel = new B_TranBalanceDel();
                BaleBalanceDel.BaleBalanceID = Convert.ToInt32(id);
                BaleBalanceDel.ShipDocID = k;
                BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.PeCar;
                this.service.GetGenericService<B_TranBalanceDel>().Add(BaleBalanceDel);
            }
            //添加之后重新计算当前结算单
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.ComputePeCar(id);
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
            var query = this.service.GetGenericService<ShippingDocument>().Query().Where(t => t.ProjectID == bale.ProjectID && t.ProduceDate > StartDay && t.ProduceDate <= EndDay);
            if (query == null)
            {
                return OperateResult(false, "选择的结算单没有需要添加的单据！", null);
            }
            foreach (var model in query)
            {
                if (!model.IsPonAccount && model.IsAudit == true && model.IsEffective == true)
                {
                    B_TranBalanceDel BaleBalanceDel = new B_TranBalanceDel();
                    BaleBalanceDel.BaleBalanceID = Convert.ToInt32(id);
                    BaleBalanceDel.ShipDocID = model.ID;
                    BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.PeCar;
                    this.service.GetGenericService<B_TranBalanceDel>().Add(BaleBalanceDel);
                }
            }
            //添加之后重新计算当前结算单
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.ComputePeCar(id);
            return Json(resultInfo);
        }

    }
}
