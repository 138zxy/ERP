using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using System.Globalization;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using System.Web.Script.Serialization;
using ZLERP.Model.ViewModels;
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers
{
    public class StuffInController : BaseController<StuffIn, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            IEnumerable<SelectListItem> StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "IsUsed=1", "OrderNum", true, null);
            ViewBag.StuffList = StuffList;

            //供货商和供货运输商
            IEnumerable<SelectListItem> Supplies= HelperExtensions.SelectListData<SupplyInfo>("SupplyName",
                "ID",
                string.Format("SupplyKind in ('{0}') AND IsUsed=1",
                Params["supplytype"].Replace(",", "','")),
                "SupplyName",
                true,
                "");
            ViewBag.Supplies = Supplies;

            //运输商
            IEnumerable<SelectListItem> Transports = HelperExtensions.SelectListData<SupplyInfo>("SupplyName",
                "ID",
                string.Format("SupplyKind in ('{0}') AND IsUsed=1",
                Params["transtype"].Replace(",", "','")),
                "SupplyName",
                true,
                "");
            ViewBag.Transports = Transports;

            List<SelectListItem> FootFrom = new List<SelectListItem>();
            FootFrom.Add(new SelectListItem { Text = "净重", Value = "0" });
            FootFrom.Add(new SelectListItem { Text = "厂商数量", Value = "1" });
            ViewBag.FootFrom = FootFrom;
            return base.Index();
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string contractID, int auditStatus)
        {
            try
            {
                this.service.StuffIn.UnAudit(contractID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, contractID, null, "原材料入库单取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
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
        public virtual JsonResult ListDataStuffInfo(string id , string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            StockPact data=null;
            IList<StuffInfo> _data = new List<StuffInfo>();
            if (id != "")
            {
                data = this.service.StockPact.Query().Where(m => m.ID == id).FirstOrDefault();
                string stuffid = data.StuffID;
                string stuffid1 = data.StuffID1;
                string stuffid2 = data.StuffID2;
                string stuffid3 = data.StuffID3;
                string stuffid4 = data.StuffID4;

                string stuffname = data.StuffName;
                string stuffname1 = data.StuffName1;
                string stuffname2 = data.StuffName2;
                string stuffname3 = data.StuffName3;
                string stuffname4 = data.StuffName4;

                
                StuffInfo info = new StuffInfo();
                if (!string.IsNullOrEmpty(stuffid))
                {
                    info.ID = stuffid;
                    info.StuffName = stuffname;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid1))
                {
                    info = new StuffInfo();
                    info.ID = stuffid1;
                    info.StuffName = stuffname1;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid2))
                {
                    info = new StuffInfo();
                    info.ID = stuffid2;
                    info.StuffName = stuffname2;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid3))
                {
                    info = new StuffInfo();
                    info.ID = stuffid3;
                    info.StuffName = stuffname3;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid4))
                {
                    info = new StuffInfo();
                    info.ID = stuffid4;
                    info.StuffName = stuffname4;
                    _data.Add(info);
                }
                return Json(new SelectList(_data, valueField, textField));
            }
            else//ID为空返回所有材料列表
            {
                IEnumerable<SelectListItem> StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "IsUsed=1", "OrderNum", true, null);

                return Json(StuffList);
            }       
        }
        /// <summary>
        /// 采购入库-增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(StuffIn entity)
        {
            if (entity.Operator == string.Empty)
            {
                entity.Operator = AuthorizationService.CurrentUserName;//当前用户名
            }
            return base.Add(entity);
        }

        /// <summary>
        /// 进货价格确认
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceConfirm()
        {
            InitCommonViewBag();
            return View();
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ExecutePrice(string[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                this.service.StuffIn.ExecutePrice(string.Join(",", ids));
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, Lang.Error_ParameterRequired);
        }
        /// <summary>
        /// 入库审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SiloAudit(string id)
        {
            if (id != null)
            {
                this.service.StuffIn.Audit(id);
                return OperateResult(true, Lang.Msg_Operate_Success, id);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, Lang.Error_ParameterRequired);
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="datas"></param>
        /// <param name="stuffinfoid"></param>
        /// <returns></returns>
        public ActionResult SiloMultiAuditZ(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    decimal bnum = 0;
                    StuffIn entity = this.service.StuffIn.Get(id);
                    Silo sentity = this.service.Silo.Get(entity.SiloID);
                    bnum = sentity.Content;
                    this.service.StuffIn.Audit(id);

                    InsertSiloLedger(entity.SiloID, entity.StuffID, "审核入库", bnum, entity.InNum, bnum + entity.InNum, entity.ID);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch(Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed+":"+ex.Message, Lang.Error_ParameterRequired);
            }
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="datas"></param>
        /// <param name="stuffinfoid"></param>
        /// <returns></returns>
        public ActionResult SiloMultiAudit(string[] ids, string datas, string stuffinfoid)
        {
            string StockPactID = "";
            string UnitPrice = "";
            string Guige = "";
            string SupplyNum = "";
            decimal price = 0;
            decimal supplyNum = 0;
            foreach (string a in datas.Split('&'))
            {
                string[] b = a.Split('=');
                if (b.Count() == 2)
                {
                    if (b[0] == "StockPactID_S")
                    {
                        StockPactID = b[1];
                    }
                    else if (b[0] == "UnitPrice")
                    {
                        UnitPrice = b[1];
                    }
                    else if (b[0] == "Guige")
                    {
                        Guige = b[1];
                    }
                    else if (b[0] == "SupplyNum")
                    {
                        SupplyNum = b[1];
                    }
                }
            }
            try
            {
                price = Convert.ToDecimal(UnitPrice);
                if (SupplyNum != "")
                {
                    supplyNum = Convert.ToDecimal(SupplyNum);
                }
            }
            catch
            {
                return OperateResult(false, "单价数量不合法", Lang.Error_ParameterRequired);
            }
            if (ids != null && StockPactID != "" && price != 0)
            {
                StockPact sp = this.service.GetGenericService<StockPact>().Get(StockPactID);
                foreach (string id in ids)
                {
                    this.service.StuffIn.Audit(id, sp, price, Guige, supplyNum, stuffinfoid);
                }
                return OperateResult(true, "已跳过不符入库单，对和合同对应的入库单进行审核。（注意同名的不同原材料，最好将所有原材料名称改为不同）", ids);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, Lang.Error_ParameterRequired);

        }
        /// <summary>
        /// 取得合同材料对应的价格
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string getStuffInfoPrice(string StockPactId, string StuffInfoId)
        {
            StockPact data = null;
            IList<StuffInfo> _data = new List<StuffInfo>();
            string StuffInfoPrice = "0";
            if (StockPactId != "")
            {
                data = this.service.StockPact.Query().Where(m => m.ID == StockPactId).FirstOrDefault();
                string stuffid = data.StuffID;
                string stuffid1 = data.StuffID1;
                string stuffid2 = data.StuffID2;
                string stuffid3 = data.StuffID3;
                string stuffid4 = data.StuffID4;
                if (data != null)
                {
                    if (StuffInfoId == stuffid)
                    {
                        StuffInfoPrice = data.StockPrice.ToString();
                    }
                    if (StuffInfoId == stuffid1)
                    {
                        StuffInfoPrice = data.StockPrice1.ToString();
                    }
                    if (StuffInfoId == stuffid2)
                    {
                        StuffInfoPrice = data.StockPrice2.ToString();
                    }
                    if (StuffInfoId == stuffid3)
                    {
                        StuffInfoPrice = data.StockPrice3.ToString();
                    }
                    if (StuffInfoId == stuffid4)
                    {
                        StuffInfoPrice = data.StockPrice4.ToString();
                    }
                }
            }
            return StuffInfoPrice;
        }

        /// <summary>
        /// 抽样
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult ToLabRecord(string id)
        {
            try
            {
                Lab_Record r = new Lab_Record();
                StuffIn s = this.service.GetGenericService<StuffIn>().Get(id);
                int c = this.service.GetGenericService<Lab_Record>().All().Count(p => p.stuffinid == id);
                if (c > 0)
                {
                    return OperateResult(false, "该单已抽样", null);
                }
                //获取试验报告类型
                List<Lab_ReportTypeConfig> rtclist = this.service.GetGenericService<Lab_ReportTypeConfig>().All().Where(p => p.StuffTypeID == s.StuffInfo.StuffTypeID).ToList();
                if (rtclist.Count == 0)
                {
                    r.ReportType = "";
                    return OperateResult(false, s.StuffInfo.StuffType.StuffTypeName +"(" +s.StuffInfo.StuffType.FinalStuffType + ")-报告原材料配置处未找到对应配置", null);
                }
                else
                {
                    r.ReportType = rtclist[0].LabReportTypeID;
                }
                r.FinalStuffTypeID = s.StuffInfo.StuffType.FinalStuffType;
                r.StuffTypeID = s.StuffInfo.StuffTypeID;
                r.Carno = s.CarNo;
                r.EndDate = s.InDate;
                r.InMan = s.Builder;
                r.Siloid = s.SiloID;
                r.Date = DateTime.Now;
                r.Stuffid = s.StuffID;
                r.Supplyid = s.SupplyID;
                r.Weight = s.InNum;
                //r.Spec = s.Guige;

                //获取样品号
                Dic dic = this.service.GetGenericService<Dic>().Get(r.ReportType);

                string month = DateTime.Now.ToString("yyyyMM");
                IList<AutoGenerateId> snolist = this.service.GetGenericService<AutoGenerateId>().Query().Where(m => m.IDPrefix == month).Where(m => m.Table == "Lab_Record" + dic.Field1).ToList();
                if (snolist.Count > 0)
                {
                    //不足位数补0
                    string nvalue = snolist[0].NextValue.ToString();
                    r.ShowpeieNo = "Y" + month + dic.Field1 + nvalue.PadLeft(4, '0');

                    //修改下一个值
                    //sno.ID = snolist[0].ID;
                    AutoGenerateId sno = snolist[0];
                    sno.NextValue = snolist[0].NextValue + 1;
                    this.service.GetGenericService<AutoGenerateId>().Update(sno, null);
                }
                else
                {
                    AutoGenerateId sno = new AutoGenerateId();
                    r.ShowpeieNo = "Y" + month + dic.Field1 + "0001";
                    //新增记录
                    sno.Table = "Lab_Record" + dic.Field1;
                    sno.IDPrefix = month;
                    sno.NextValue = 2;
                    this.service.GetGenericService<AutoGenerateId>().Add(sno);
                }

                int? Lid = this.service.GetGenericService<Lab_Record>().Add(r).ID;
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

        /// <summary>
        /// 入库单价调整（批量）存错过程
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult StuffInPriceAdjust(string beginDate, string endDate)
        {
            try
            {
                //IList<MonthAccount> mlist = this.service.GetGenericService<MonthAccount>().All("Month='" + Month + "'", "Month", true);
                //if (mlist.Count > 0)
                //{
                //    return OperateResult(false, Month + "月份已经月结，不能再进行月结！", null);

                //}

                //IList<MonthAccount> mlist2 = this.service.GetGenericService<MonthAccount>().All("1=1", "Month", true);
                //var m = mlist2.FirstOrDefault();
                //if (m != null && Convert.ToInt64(m.Month) >= Convert.ToInt64(Month))
                //{
                //    return OperateResult(false, "不能比已经月结过的月份小，请选择大于最后月结的月份！", null);
                //}

                //if (mlist2.Count != 0)
                //{
                //    TimeSpan ts = DateTime.Now - Convert.ToDateTime(m.Buildtime);
                //    if (ts.Days < 30)
                //    {
                //        return OperateResult(false, "与上一次的月结间隔不能少于30天！", null);
                //    }
                //}

                this.service.StuffInPriceAdjust.StuffInPriceAdjustOper(beginDate, endDate);
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

        /// <summary>
        /// 批量调整结算数量 根据出厂时间
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="FootFrom"></param>
        public ActionResult StuffInChangeNum(string beginDate_ChangeNum, string endDate_ChangeNum, int FootFrom)
        {
            try
            {
                this.service.StuffIn.StuffInChangeNum(beginDate_ChangeNum, endDate_ChangeNum, FootFrom);
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
        /// <summary>
        /// 自动结算单，按照合同和原材料自动汇总，可能生成多个结算单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult BaleBalance(List<string> ids)
        {
            if (ids.Count <= 0)
            {
                return OperateResult(false, "请选择单据", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.BaleBalance(ids);
            return Json(resultInfo);
        }
        /// <summary>
        /// 运费结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult TranBalance(List<string> ids)
        {
            if (ids.Count <= 0)
            {
                return OperateResult(false, "请选择单据", null);
            }
            ResultInfo resultInfo = new ResultInfo();
            resultInfo = this.service.MaterialService.TranBalance(ids);
            return Json(resultInfo);
        }


        /// <summary>
        /// 入库单错误修正
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  ActionResult UpdateSilo(StuffIn entity)
        {
            try
            {
                //错误修正
                string sourceID = entity.ID;
                StuffIn sourceentity = this.service.StuffIn.Get(sourceID);
                if (sourceentity.Lifecycle == 0)//修改
                {
                    return base.Update(entity);//修改源单据作废
                }

                SysConfig config = this.service.SysConfig.GetSysConfig("IsAuditStuffin");//入库单修正是否自动审核
                bool IsAudit = Convert.ToBoolean(config.ConfigValue);
                entity.ID = null;
                entity.Lifecycle = IsAudit == true ? 1 : 0;
                entity.Remark = "原单据:"+sourceentity.ID;
                Silo newsilo = this.service.Silo.Get(entity.SiloID);
                entity.SiloName = newsilo.SiloName;
                string newStuffInId=this.service.GetGenericService<StuffIn>().Add(entity).ID;//新增一条单据，状态设置未审核

                decimal InNum;
                Silo silo;
                StuffInfo stuff;
                decimal oldcontent;

                //---------------------作废------------------------
                InNum = sourceentity.InNum;//数据库获值
                //减筒仓库存
                silo = this.service.Silo.Get(sourceentity.SiloID);
                oldcontent = silo.Content;
                silo.Content = silo.Content - InNum;
                this.service.Silo.Update(silo, null);
                //减材料库存
                stuff = this.service.StuffInfo.Get(sourceentity.StuffID);
                stuff.Inventory = stuff.Inventory - InNum;
                this.service.StuffInfo.Update(stuff, null);
                //插入流水账
                InsertSiloLedger(sourceentity.SiloID, sourceentity.StuffID, "修正作废", oldcontent, InNum, silo.Content, sourceentity.ID);

                //-------------------新增单据----------------------
                if (IsAudit)
                {
                    InNum = entity.InNum;//界面传值
                    //加筒仓库存
                    silo = this.service.Silo.Get(entity.SiloID);
                    oldcontent = silo.Content;
                    silo.Content = silo.Content + InNum;
                    this.service.Silo.Update(silo, null);
                    //加材料库存
                    stuff = this.service.StuffInfo.Get(entity.StuffID);
                    stuff.Inventory = stuff.Inventory + InNum;
                    this.service.StuffInfo.Update(stuff, null);

                    InsertSiloLedger(entity.SiloID, entity.StuffID, "修正入库", oldcontent, InNum, silo.Content, entity.ID);
                }

                //修改换罐的子记录的ParentID为新的单据ID
                IList<StuffIn> silist = this.service.GetGenericService<StuffIn>().All("ParentID='" + sourceentity.ID + "'", "ParentID", true);
                foreach (var si in silist)
                {
                    si.ParentID = newStuffInId;
                    this.service.GetGenericService<StuffIn>().Update(si);
                }
                sourceentity.Lifecycle = 2;
                return base.Update(sourceentity);//修改源单据作废
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, "");
            }
            
        }

        /// <summary>
        /// 入库单复制
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Copy(string  id)
        {
            try
            {
                this.service.StuffIn.Copy(id);
                return OperateResult(true, Lang.Msg_Operate_Success, id);
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, "");
            }
        }

        /// <summary>
        /// 入库单作废
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult GiveUp(string id)
        {
            try
            {
                decimal oldcontent = 0;
                StuffIn entity = this.service.StuffIn.Get(id);
                entity.Lifecycle = 2;
                decimal InNum;
                string siloid, stuffid;
                Silo silo;
                StuffInfo stuff;
                InNum = entity.InNum;
                siloid = entity.SiloID;               
                silo = this.service.Silo.Get(siloid);
                oldcontent = silo.Content;
                silo.Content = silo.Content - InNum;
                this.service.Silo.Update(silo,null);//加筒仓库存

                stuffid = entity.StuffID;
                stuff = this.service.StuffInfo.Get(stuffid);
                stuff.Inventory = stuff.Inventory - InNum;
                this.service.StuffInfo.Update(stuff, null);//加材料库存

                InsertSiloLedger(entity.SiloID,entity.StuffID,"作废",oldcontent,InNum,silo.Content,entity.ID);
                return base.Update(entity);//修改源单据作废
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, "");
            }
        }
        /// <summary>
        /// 筒仓总账插入记录
        /// </summary>
        /// <param name="siloId"></param>
        /// <param name="stuffId"></param>
        /// <param name="action"></param>
        /// <param name="bNum"></param>
        /// <param name="num"></param>
        /// <param name="rNum"></param>
        private void InsertSiloLedger(string siloId,string stuffId,string action,decimal bNum,decimal num,decimal rNum,string docNo)
        {
            SiloLedgerContent_InAndCheck sic = new SiloLedgerContent_InAndCheck();
            sic.SiloID = siloId;
            sic.StuffID = stuffId;
            sic.Action = action;
            sic.BaseStore = bNum;
            sic.Num = num;
            sic.Remaining = rNum;
            sic.ActionTime = DateTime.Now;
            sic.UserName = AuthorizationService.CurrentUserName;
            sic.Builder = AuthorizationService.CurrentUserID;
            sic.BuildTime = DateTime.Now;
            sic.DocNo = docNo;
            this.service.GetGenericService<SiloLedgerContent_InAndCheck>().Add(sic);

        }

        public ActionResult Comprice(string id, int Lifecycle)
        {
            var bale = this.service.GetGenericService<M_BaleBalanceDel>().Query().FirstOrDefault(t => t.StuffInID == id);
            if (bale == null)
            {
                return OperateResult(false, "已经加入结算，无法操作", "");
            }
            var stuffin = this.m_ServiceBase.Get(id);
            stuffin.Lifecycle = Lifecycle;
            this.m_ServiceBase.Update(stuffin, null);
            return OperateResult(true, "核对成功", "");
        }
        /// <summary>
        /// 批量核对
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult CompriceALL(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    var bale = this.service.GetGenericService<M_BaleBalanceDel>().Query().FirstOrDefault(t => t.StuffInID == id);
                    if (bale != null)
                    {
                        return OperateResult(false, "已经加入结算，无法操作", "");
                    }
                    this.service.StuffIn.Comprice(id);
                }
                return OperateResult(true, "核对成功", ids);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + ex.Message, Lang.Error_ParameterRequired);
            }
        }
        /// <summary>
        /// 暗扣
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public ActionResult DarkWeight(StuffIn entity)
        {
            var stuffin = this.service.StuffIn.Get(entity.ID);
            if (stuffin == null)
            {
                return OperateResult(false, "无法操作", "");
            }
            decimal darkweight = entity.DarkWeight - stuffin.DarkWeight;//暗扣差值
            stuffin.TotalNum = stuffin.TotalNum - darkweight;
            stuffin.DarkWeight = entity.DarkWeight;

            decimal inNum = Convert.ToDecimal(stuffin.TotalNum) - Convert.ToDecimal(stuffin.CarWeight) - Convert.ToDecimal(stuffin.MingWeight);
            stuffin.InNum = stuffin.StockNum = inNum;
            if (stuffin.Proportion != 0)
            {
                decimal FootNum = inNum / stuffin.Proportion;
                decimal FinalFootNum = FootNum;
                stuffin.FootNum = FootNum;
                stuffin.FinalFootNum = FinalFootNum;
            }

            stuffin.FinalFootNum = inNum;
            this.m_ServiceBase.Update(stuffin, null);
            return OperateResult(true, "暗扣成功", "");
        }


        [HttpPost]
        public JsonResult QualityInspect(string stuffInId)
        {
            try
            {
                StuffIn stuffIn = this.service.StuffIn.Get(stuffInId);
                stuffIn.IsQualityInspected = !stuffIn.IsQualityInspected;
                this.service.StuffIn.Update(stuffIn, null);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(true, Lang.Msg_Operate_Failed, ex.Message);
            }
        }
    }
}


