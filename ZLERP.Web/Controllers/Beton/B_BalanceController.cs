using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Model.Enums;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_BalanceController : BaseController<B_Balance, string>
    {


        public override ActionResult Index()
        {
            var CarTemplet = this.service.GetGenericService<B_FareTemplet>().All("", "ID", true);
            ViewBag.ItemsType = new SelectList(CarTemplet, "ID", "FareTempletName");

            List<SelectListItem> BalaneType = new List<SelectListItem>();
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type1, Value = B_Common.BalaneType.type1 });
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type2, Value = B_Common.BalaneType.type2 });
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type3, Value = B_Common.BalaneType.type3 });
            BalaneType.Add(new SelectListItem { Text = B_Common.BalaneType.type4, Value = B_Common.BalaneType.type4 });

            ViewBag.BalaneType = BalaneType;

            return base.Index();
        }
        public override ActionResult Add(B_Balance B_Balance)
        {
            return base.Add(B_Balance);
        }
        public override ActionResult Update(B_Balance B_Balance)
        {
            if (B_Balance.IsOnePrice && B_Balance.IsStockPrice)
            {
                return OperateResult(false, "只能选择按模板结算或者按手动结算其中一种方式", null);
            }
            DateTime dt;
            bool reslut = false;
            reslut = DateTime.TryParse(B_Balance.InMonth + "-01", out dt);
            if (!reslut)
            {
                return OperateResult(false, "纳入月份请输入正确的格式", null);
            }
            B_Balance.ModelType = ZLERP.Model.Beton.B_Common.ModelType.Beton;
            return base.Update(B_Balance);
        }

        public override ActionResult Delete(string[] id)
        {
            //这个删除的操作非常重要，一定要保持同步进行，主体删除，明细没有删除，入库单永远找不到为啥已标记为结算
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.DeleteBalance(id);
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
                var detiel = this.service.GetGenericService<B_YingSFrecdetail>().Query().Where(t => t.OrderNo == OrderNo).FirstOrDefault();
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
                UpdateSupplyInfoPeyMoney(query.ContractID, query.AllOkMoney);
            }
            else
            {
                UpdateSupplyInfoPeyMoney(query.ContractID, -query.AllOkMoney);
            }
            return OperateResult(true, "操作成功", null);
        }

        /// <summary>
        /// 更新当前结算单的供货厂商的总付款金额
        /// </summary>
        /// <param name="id"></param>
        private void UpdateSupplyInfoPeyMoney(string TranID, decimal? money)
        {
            if (!string.IsNullOrEmpty(TranID))
            {
                var su = this.service.GetGenericService<Contract>().Get(TranID);
                su.PayMoney = su.PayMoney + money ?? 0;
                this.service.GetGenericService<Contract>().Update(su, null);
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
            resultInfo = this.service.BetonService.ComputeBeton(id);
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
                if (q.ProjectID != bale.ProjectID || q.ContractID != bale.ContractID)
                {
                    return OperateResult(false, "选择待结算的合同,工程与主体结算单的工地不一致", null);
                }

            }
            //排除已经存在的
            var del = this.service.GetGenericService<B_BalanceDel>().Query().Where(t => keys.Contains(t.ShipDocID) && t.ModelType == ZLERP.Model.Beton.B_Common.ModelType.Beton);
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
                B_BalanceDel BaleBalanceDel = new B_BalanceDel();
                BaleBalanceDel.BaleBalanceID = Convert.ToInt32(id);
                BaleBalanceDel.ShipDocID = k;
                BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.Beton;
                this.service.GetGenericService<B_BalanceDel>().Add(BaleBalanceDel);
            }
            //添加之后重新计算当前结算单
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.ComputeBeton(id);
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
            var query = this.service.GetGenericService<ShippingDocument>().Query().Where(t => t.ProjectID == bale.ProjectID && t.ContractID == bale.ContractID && t.ProduceDate > StartDay && t.ProduceDate <= EndDay);
            if (query==null)
            {
                return OperateResult(false, "选择的结算单没有需要添加的单据！", null);
            }
            foreach (var model in query)
            {
                if (!model.IsAccount && model.IsAudit == true && model.IsEffective == true)
                {
                    B_BalanceDel BaleBalanceDel = new B_BalanceDel();
                    BaleBalanceDel.BaleBalanceID = Convert.ToInt32(id);
                    BaleBalanceDel.ShipDocID = model.ID;
                    BaleBalanceDel.ModelType = ZLERP.Model.Beton.B_Common.ModelType.Beton;
                    this.service.GetGenericService<B_BalanceDel>().Add(BaleBalanceDel);
                }
            }
            //添加之后重新计算当前结算单
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.BetonService.ComputeBeton(id);
            return Json(resultInfo);
        }
        public ActionResult AddBalance(string InMonth, string ProjectID)
        {
            try
            {
                if (string.IsNullOrEmpty(InMonth))
                {
                    return this.OperateResult(false, "请输入纳入月份！", null);
                }
                if (string.IsNullOrEmpty(ProjectID))
                {
                    return this.OperateResult(false, "请选择工程明细！", null);
                }
                Project project = base.service.Project.Get(ProjectID);
                DateTime time = Convert.ToDateTime(InMonth + "-" + base.service.SysConfig.GetSysConfig(SysConfigEnum.ChangeDay).ConfigValue);
                DateTime time2 = time.AddMonths(-1);
                B_Balance entity = new B_Balance();
                string orderNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999).ToString("000"));
                entity.BaleNo = string.Format("{0}{1}", "T", orderNo);
                entity.InMonth = InMonth;
                entity.StartDate = new DateTime?(time2);
                entity.EndDate = new DateTime?(time);
                entity.ProjectID = ProjectID;
                entity.OrderDate = DateTime.Now;
                entity.ModelType = B_Common.ModelType.Beton;
                entity.ContractID = project.ContractID;
                entity.ThatBaleMan = "";
                if (project.B_FareTemplet != null)
                {
                    entity.BaleType = new int?(Convert.ToInt32(project.B_FareTemplet.ID));
                }
                else
                {
                    B_FareTemplet templet = (from t in base.service.GetGenericService<B_FareTemplet>().All()
                        orderby t.BuildTime descending
                        select t).First<B_FareTemplet>();
                    if (templet != null)
                    {
                        entity.BaleType = new int?(Convert.ToInt32(templet.ID));
                    }
                    else
                    {
                        entity.BaleType = 0;
                    }
                }
                entity.IsOnePrice = false;
                entity.OnePriceType = B_Common.BalaneType.type3;
                entity.IsStockPrice = true;
                entity.AuditStatus = 0;
                entity.BaleDate = DateTime.Now;
                entity.BaleMan = AuthorizationService.CurrentUserName;
                entity.Remark = "手动添加的结算单";
                ActionResult result = base.Add(entity);
                return this.OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception exception)
            {
                return this.OperateResult(false, Lang.Msg_Operate_Failed + exception.Message, null);
            }
        }

    }
}
