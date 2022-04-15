﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Resources;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers
{
    public class StockPactController : BaseController<StockPact, string>
    {

        public override System.Web.Mvc.ActionResult Index()
        {
            List<SelectListItem> BitUpdatePrice = new List<SelectListItem>();
            BitUpdatePrice.Add(new SelectListItem { Text = "按百分比", Value = "0" });
            BitUpdatePrice.Add(new SelectListItem { Text = "按数额", Value = "1" });
            ViewBag.BitUpdatePrice = BitUpdatePrice;

            List<SelectListItem> FootFrom = new List<SelectListItem>();
            FootFrom.Add(new SelectListItem { Text = "厂商数量", Value = "0" });
            FootFrom.Add(new SelectListItem { Text = "净重", Value = "1" });
            ViewBag.FootFrom = FootFrom;

            List<SelectListItem> TaxRate = new List<SelectListItem>();
            var list = this.service.GetGenericService<Dic>().Find(" ParentID = 'Bd_Taxrate' ", 1, 100, "  CAST(Field3 as int) ", "ASC").ToList();
            foreach (var item in list)
            {
                TaxRate.Add(new SelectListItem { Text = item.Field3, Value = item.Field1 });
            }
            ViewBag.TaxRates = TaxRate;

            ViewBag.BitStuffID = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "IsUsed=1", "StuffName", true, null);

            return base.Index();
        }

        public ActionResult PriceSet()
        {
            return View();
        }
        /// <summary>
        /// 采购合同-增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(StockPact StockPact)
        {
            if (string.IsNullOrEmpty(StockPact.SupplyID))
            {
                return OperateResult(false, "请选择供应商", null);
            }

            //判断供应商不能重复 
            var query = this.m_ServiceBase.Query().Where(t => t.SupplyID == StockPact.SupplyID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "已经存在当前供应商", null);
            }
            string orderNo = new SupplyChainHelp().GenerateOrderNo();
            var pactno = string.Format("{0}{1}", "S", orderNo);

            StockPact.StockPactNo = pactno;
            StockPact.PactName = pactno;


            return base.Add(StockPact);
        }
        /// <summary>
        /// 采购合同-修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Update(StockPact StockPact)
        {
            if (string.IsNullOrEmpty(StockPact.SupplyID))
            {
                return OperateResult(false, "请选择供应商", null);
            }
            //判断供应商不能重复 
            var query = this.m_ServiceBase.Query().Where(t => t.ID != StockPact.ID && t.SupplyID == StockPact.SupplyID).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "已经存在当前供应商", null);
            }
            return base.Update(StockPact);
        }
       

        /// <summary>
        /// 定义下拉列表样式
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<StockPact> data;
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<StockPact>().Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("{0} like '%{1}%' or SupplyInfo.ShortName like '%{1}%'", textField, q.Replace("'", ""), "PactName");
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = m_ServiceBase.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong><br/>{5}：{1}<br/>{6}：{2}<br/>{7}：{3}<br/>{8}：{4}",
                        HelperExtensions.Eval(p, textField),
                        p.ID,
                        p.SupplyName,
                        p.PactName,
                        p.StuffInfo.StuffName + "," + (p.StuffInfo1 == null ? "" : p.StuffInfo1.StuffName) + "," + (p.StuffInfo2 == null ? "" : p.StuffInfo2.StuffName) + "," + (p.StuffInfo3 == null ? "" : p.StuffInfo3.StuffName) + "," + (p.StuffInfo4 == null ? "" : p.StuffInfo4.StuffName),
                        Lang.Entity_Property_ID,
                        "供货厂商",
                        "价格编号",
                        Lang.Entity_Property_StuffName),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }


        /// <summary>
        /// 新增批量调价的功能
        /// </summary>
        /// <returns></returns>
        public ActionResult BitUpdatePrice(List<string> keys, int BitUpdateType,string BitStuffID, decimal BitUpdateCnt, DateTime BitUpdateDate)
        {

            if (keys == null || keys.Count <= 0 || BitUpdateCnt == 0)
            {
                return OperateResult(false, "参数错误", "");
            }
            List<BitStuffUpdatePrice> BitUpdatePrices = new List<BitStuffUpdatePrice>();
            foreach (var key in keys)
            {
                BitStuffUpdatePrice UpdatePrice = new BitStuffUpdatePrice();
                UpdatePrice.BitStockPactID = key;
                UpdatePrice.BitUpdateType = BitUpdateType;
                UpdatePrice.BitStuffID = BitStuffID;
                UpdatePrice.BitUpdateCnt = BitUpdateCnt;
                UpdatePrice.BitUpdateDate = BitUpdateDate;
                BitUpdatePrices.Add(UpdatePrice);
            }
            ResultInfo ResultInfo = this.service.MaterialService.BitUpdatePrice(BitUpdatePrices);
            return OperateResult(ResultInfo.Result, ResultInfo.Message, ResultInfo.Data); 
        }


        public ActionResult UpdateBalanceRecordAndItems(string StockPactID)
        {
            try
            {
                this.service.StockPact.UpdateBalanceRecordAndItems(StockPactID);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);
            }
        }

        public ActionResult AuditPrice(string id, string status)
        {
            try
            {
                StockPact sd = this.service.StockPact.Get(id);
                sd.AuditStatus = sd.AuditStatus == 0 ? 1 : 0;
                this.service.StockPact.Update(sd);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }



    }
}
