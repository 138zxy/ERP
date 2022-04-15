using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Linq;
using ZLERP.Model;
using ZLERP.Business;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Web.Controllers.Attributes;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class CustomerPlanGHController : BaseController<CustomerPlanGH, string>
    {
        public override ActionResult Index()
        {

         
                var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p => p.ID);
                ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

                ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
                ViewBag.PumpManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '52' AND IsUsed=1 ", "ID", true, "");

                //ViewBag.SupplyUnit = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "SupplyName", "supplyID in('1010401','1010405')", "SupplyName", true, "");
                //ViewBag.SupplyUnit = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "ID", "supplyID in('1010401','1010405')", "ID", true, null);
                //ViewBag.SupplyUnit = new List<SelectListItem>(){
                //    new SelectListItem(){ Value = "重庆天助商品混凝土搅拌有限公司", Text = "重庆天助商品混凝土搅拌有限公司" },
                //    new SelectListItem(){ Value = "重庆市城投混凝土有限公司", Text = "重庆市城投混凝土有限公司" }};
                ViewBag.NeedDateList = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "", Text = "" },
                    new SelectListItem(){ Value = "待定", Text = "待定" },
                    new SelectListItem(){ Value = "00:00", Text = "00:00" },
                    new SelectListItem(){ Value = "00:30", Text = "00:30" },
                    new SelectListItem(){ Value = "01:00", Text = "01:00" },
                    new SelectListItem(){ Value = "01:30", Text = "01:30" },
                    new SelectListItem(){ Value = "02:00", Text = "02:00" },
                    new SelectListItem(){ Value = "02:30", Text = "02:30" },
                    new SelectListItem(){ Value = "03:00", Text = "03:00" },
                    new SelectListItem(){ Value = "03:30", Text = "03:30" },
                    new SelectListItem(){ Value = "04:00", Text = "04:00" },
                    new SelectListItem(){ Value = "04:30", Text = "04:30" },
                    new SelectListItem(){ Value = "05:00", Text = "05:00" },
                    new SelectListItem(){ Value = "05:30", Text = "05:30" },
                    new SelectListItem(){ Value = "06:00", Text = "06:00" },
                    new SelectListItem(){ Value = "06:30", Text = "06:30" },
                    new SelectListItem(){ Value = "07:00", Text = "07:00" },
                    new SelectListItem(){ Value = "07:30", Text = "07:30" },
                    new SelectListItem(){ Value = "08:00", Text = "08:00" },
                    new SelectListItem(){ Value = "08:30", Text = "08:30" },
                    new SelectListItem(){ Value = "09:00", Text = "09:00" },
                    new SelectListItem(){ Value = "09:30", Text = "09:30" },
                    new SelectListItem(){ Value = "10:00", Text = "10:00" },
                    new SelectListItem(){ Value = "10:30", Text = "10:30" },
                    new SelectListItem(){ Value = "11:00", Text = "11:00" },
                    new SelectListItem(){ Value = "11:30", Text = "11:30" },
                    new SelectListItem(){ Value = "12:00", Text = "12:00" },
                    new SelectListItem(){ Value = "12:30", Text = "12:30" },
                    new SelectListItem(){ Value = "13:00", Text = "13:00" },
                    new SelectListItem(){ Value = "13:30", Text = "13:30" },
                    new SelectListItem(){ Value = "14:00", Text = "14:00" },
                    new SelectListItem(){ Value = "14:30", Text = "14:30" },
                    new SelectListItem(){ Value = "15:00", Text = "15:00" },
                    new SelectListItem(){ Value = "15:30", Text = "15:30" },
                    new SelectListItem(){ Value = "16:00", Text = "16:00" },
                    new SelectListItem(){ Value = "16:30", Text = "16:30" },
                    new SelectListItem(){ Value = "17:00", Text = "17:00" },
                    new SelectListItem(){ Value = "17:30", Text = "17:30" },
                    new SelectListItem(){ Value = "18:00", Text = "18:00" },
                    new SelectListItem(){ Value = "18:30", Text = "18:30" },
                    new SelectListItem(){ Value = "19:00", Text = "19:00" },
                    new SelectListItem(){ Value = "19:30", Text = "19:30" },
                    new SelectListItem(){ Value = "20:00", Text = "20:00" },
                    new SelectListItem(){ Value = "20:30", Text = "20:30" },
                    new SelectListItem(){ Value = "21:00", Text = "21:00" },
                    new SelectListItem(){ Value = "21:30", Text = "21:30" },
                    new SelectListItem(){ Value = "22:00", Text = "22:00" },
                    new SelectListItem(){ Value = "22:30", Text = "22:30" },
                    new SelectListItem(){ Value = "23:00", Text = "23:00" },
                    new SelectListItem(){ Value = "23:30", Text = "23:30" }
                };
           // }
            return base.Index();
        }
        
        public  override ActionResult Find(Lib.Web.Mvc.JQuery.JqGrid.JqGridRequest request, string condition)
        {
            //if (string.IsNullOrEmpty(condition))
            //{
            //    condition = string.Format("Builder='{0}'", AuthorizationService.CurrentUserID);
            //}
            //else
            //    condition += string.Format(" AND Builder='{0}'", AuthorizationService.CurrentUserID);

            return base.Find(request, condition);
        }

        [HttpPost]
        public ActionResult ManageFind(Lib.Web.Mvc.JQuery.JqGrid.JqGridRequest request, string condition)
        {
            return base.Find(request, condition);
        }

        public ActionResult ManageIndex()
        {
            base.InitCommonViewBag();
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
 
            var pumpList = this.service.Car.GetPumpList()
                .OrderBy(p => p.ID);
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            ViewBag.SupplyUnit1 = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "SupplyName", "supplyID in('1010401','1010405')", "SupplyName", true, null);
            return View();
        }

        [HttpPost, HandleAjaxError]
        public JsonResult Auditing(CustomerPlanGH plan)
        {
            lock (customerObj)
            {
                //获取用户权限方案
                PublicService ps = new PublicService();
                SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowCustomerBalanceNoEnough);
                SysConfig config0 = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.BalanceLimitType);

                string custID="";
                decimal price = 0;

                decimal money = 0;

                bool iskye = false;//是否扣余额
                if (config.ConfigValue == "false")
                {
                    iskye = false;
                    //查询余额
                    //string ConStrength = plan.ConStrength;
                    //ContractGH contractGH = this.service.ContractGH.Get(plan.ContractID);
                    //decimal money = Convert.ToDecimal(contractGH.Balance);

                    if (config0.ConfigValue == "1")
                    {
                        money = getB1(plan);
                    }
                    else
                    {
                        money = getB2(plan);
                    }

                    //获得单价
                    IList<ContractItemGH> list = this.service.ContractItemGH.Query().Where(m => m.ContractID == plan.ContractID && m.ConStrength == plan.ConStrength).ToList<ContractItemGH>();
                    if (list == null || list.Count == 0)
                        return OperateResult(false, "没有查询到合同的砼价格，请先添加.", null);
                   // price = list[0].UnPumpPrice.Value;
                    price = getConGHPrice(plan.ContractID, plan.ConStrength, plan.PlanDate);
                    //是否可以创建或者修改
                    price = price * plan.PlanCube;
                    if (price > -money)
                    {
                        return OperateResult(false, string.Format("计划需要金额{0}，用户余额{1},请减方或者通知客户经理垫资。", price, -money), null);
                    }
                }

                bool ret = this.service.CustomerPlanGH.Auditing(plan, price, custID, iskye);
                return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
            }
        }

        /// <summary>
        /// 获取合同余额
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        private decimal getB1(CustomerPlanGH plan)
        {
            ContractGH contract = this.service.ContractGH.Get(plan.ContractID);
            decimal money = Convert.ToDecimal(contract.Balance);
            return money;
        }
        /// <summary>
        /// 获取客户余额
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        private decimal getB2(CustomerPlanGH plan)
        {
            ContractGH contract = this.service.ContractGH.Get(plan.ContractID);
            Customer customer = this.service.Customer.Get(contract.CustomerID);
            decimal money = Convert.ToDecimal(customer.AccountBalance);
            return money;
        }

        /// <summary>
        /// 获取砼单价
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="conStrength"></param>
        /// <param name="produceDate"></param>
        /// <returns></returns>
        private decimal getConGHPrice(string contractID, string conStrength, DateTime produceDate)
        {
            decimal d = this.service.CustomerPlanGH.GetConGHPrice(contractID, conStrength, produceDate);
            return d;
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultAudit(string[] ids) 
        {
            lock (customerObj)
            {
                string msg = "";

                //获取用户权限方案
                PublicService ps = new PublicService();
                SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowCustomerBalanceNoEnough);
                SysConfig config0 = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.BalanceLimitType);
                foreach (string id in ids)
                {

                    CustomerPlanGH plan;
                    if (!string.IsNullOrEmpty(id))
                    {//防止提交空id出错
                        plan = this.service.CustomerPlanGH.Get(id);
                        if (plan != null && plan.AuditStatus != (int)AuditStatus.Pass)
                        {
                            plan.AuditStatus = 1;
                        }
                    }
                    else
                    {
                        continue;
                    }


                    string custID = "";
                    decimal price = 0;
                    decimal money = 0;
                    bool iskye = false;//是否扣余额
                    if (config.ConfigValue == "false")
                    {
                        iskye = false;
                        //查询余额
                        string ConStrength = plan.ConStrength;
                        if (config0.ConfigValue == "1")
                        {
                            money = getB1(plan);
                        }
                        else
                        {
                            money = getB2(plan);
                        }

                        //获得单价
                        IList<ContractItemGH> list = this.service.ContractItemGH.Query().Where(m => m.ContractID == plan.ContractID && m.ConStrength == ConStrength).ToList<ContractItemGH>();
                        if (list == null || list.Count == 0)
                            return OperateResult(false, "没有查询到合同的砼价格，请先添加.", null);
                      //  price = list[0].UnPumpPrice.Value;
                        price = getConGHPrice(plan.ContractID, plan.ConStrength, plan.PlanDate);
                        //是否可以创建或者修改
                        price = price * plan.PlanCube;
                        if (price > -money)
                            return OperateResult(false, string.Format("计划需要金额{0}，用户余额{1},请减方或者通知客户经理垫资。", price, -money), null);
                    }
                    bool ret = this.service.CustomerPlanGH.Auditing(plan, price, custID, iskye);
                    if (ret)
                    {
                        msg += string.Format("计划编号：{0}，审核通过</br>", plan.ID);
                    }
                    else
                    {
                        msg += string.Format("计划编号：{0}，审核不通过</br>", plan.ID);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, false);
            }
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult CancelAudit(string[] ids)
        {
            string errmsg="";
            bool ret=false;
            foreach (string id in ids)
            {
                CustomerPlanGH plan;
                if (!string.IsNullOrEmpty(id))
                {//防止提交空id出错
                    plan = this.service.CustomerPlanGH.Get(id);
                    if (plan != null && plan.AuditStatus != (int)AuditStatus.Pass)
                    {
                        plan.AuditStatus = 1;
                    }
                }
                else
                {
                    continue;
                }

                //获取用户权限方案
                PublicService ps = new PublicService();
                SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowCustomerBalanceNoEnough);

                string custID = "";
                decimal price = 0;
                decimal money = 0;
                bool iskye = false;//是否扣余额
                if (config.ConfigValue == "false")
                {
                    iskye = false;
                    //查询余额
                    string ConStrength = plan.ConStrength;
                    custID = this.service.ContractGH.Get(plan.ContractID).CustomerID;
                    Customer customer = this.service.Customer.Get(custID);
                    money = customer.AccountBalance;//余额

                    //获得单价
                    IList<ContractItemGH> list = this.service.ContractItemGH.Query().Where(m => m.ContractID == plan.ContractID && m.ConStrength == ConStrength).ToList<ContractItemGH>();
                    if (list == null || list.Count == 0)
                        return OperateResult(false, "没有查询到合同的砼价格，请先添加.", null);
                    price = list[0].UnPumpPrice.Value*plan.PlanCube;//金额
                }
                ret = this.service.CustomerPlanGH.CancelAudit(ids, money, price, custID, iskye, ref errmsg);
            }          
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed +":"+ errmsg, false);
        }

        /// <summary>
        /// 组合任务单数据显示在autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ContractNo", bool ascending = true, string condition = "")
        {
            IList<ContractGH> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.ContractGH.Find(condition, 1, 100, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ContractID like '%{1}%' or ContractName like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.ContractGH.Find(
                    where,
                    1,
                    100,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                //Text = string.Format("<strong>{0}</strong>[<strong>{5}</strong>]<br/>{3}：{1}<br/>{4}：{2}",
                //        HelperExtensions.Eval(p, textField),
                //        p.CustName,
                //        p.ContractName,
                //        Lang.Entity_Property_CustName,
                //        Lang.Entity_Property_ContractName,
                //        p.ContractName),
                Text = string.Format("<strong>{0}</strong>[<strong>{3}</strong>]<br/>{2}：{1}",
                        HelperExtensions.Eval(p, textField),
                        p.CustName,
                        Lang.Entity_Property_CustName, p.ContractName),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }

        public override ActionResult Update(CustomerPlanGH entity)
        {
            var planInDB = this.service.CustomerPlanGH.Get(entity.ID);

            if (planInDB != null && planInDB.AuditStatus == (int)AuditStatus.Pass)
            {
                return OperateResult(false, "已审核的计划不能修改", null);
            }
            var ResultInfo = IsHandInputConStrength(entity);
            if (!ResultInfo.Result)
            {
                return OperateResult(false, ResultInfo.Message, null);
            }
            return base.Update(entity);
            //}
        }

        /// <summary>
        /// 取得砼强度下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual JsonResult ListDataStrengthInfo(string id, string textField, string valueField, string orderBy = "ID",bool ascending = false,string condition = "")
        {
            IEnumerable<SelectListItem> list = HelperExtensions.SelectListData<ContractItemGH>("ConStrength", "ID", condition, "ConStrength", true, null);
            return Json(list);
        }

        public override ActionResult Add(CustomerPlanGH CustomerPlanGH)
        {
            ModelState.Remove("ContractGH.ContractName");
            ModelState.Remove("ContractGH.CustomerID");
            ModelState.Remove("ContractGH.Salesman");
            var ResultInfo = IsHandInputConStrength(CustomerPlanGH);
            if (!ResultInfo.Result)
            {
                return OperateResult(false, ResultInfo.Message, null);
            } 
            return base.Add(CustomerPlanGH);
        }

        /// <summary>
        /// 取得前场工长下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual JsonResult ListDataLinkMan(string id, string textField, string valueField, string orderBy = "ID", bool ascending = false, string condition = "")
        {

            var data = this.m_ServiceBase.All(new List<string> { valueField, textField }, condition, orderBy, ascending);
            List<SelectListItem> items = new List<SelectListItem>();
            IList<CustomerPlanGH> list2 = data as IList<CustomerPlanGH>;
            int i = 0;
            ArrayList arr = new ArrayList();
            foreach (CustomerPlanGH j in list2)
            {
                bool f = false;
                if (i == -1)
                { f = true; i++; }
                else f = false;
                if (!arr.Contains(j.LinkMan))
                {
                    arr.Add(j.LinkMan);
                    SelectListItem item = new SelectListItem() { Text = j.LinkMan, Value = j.LinkMan, Selected = f };
                    items.Add(item);
                }
            }
            return Json(items);           
        }
        
        /// <summary>
        /// 获取历史合同对应前场工长
        /// </summary>
        /// <param name="contractid"></param>
        /// <param name="linkman"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public JsonResult GetLinkManTel(string contractid,string linkman)
        {
            IList<CustomerPlanGH> cp = this.service.CustomerPlanGH.All().Where(c => c.ContractID == contractid && c.LinkMan == linkman).ToList();
            return  OperateResult(true, "", cp[0].Tel);
        }

        
        /// <summary>
        /// 复制工地计划单
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult CopyPlan(string planid, string[] ids, string conspos)
        {
            bool issuccess = this.service.CustomerPlanGH.CopyPlan(planid, ids, conspos);
            if (issuccess)
            {
                return OperateResult(true, "复制成功！", null);
            }
            else
            {
                return OperateResult(false, "复制失败！", null);
            }
        }
        /// <summary>
        /// 完全复制工地计划单
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult CopyPlanAll(string planid)
        {
            bool issuccess = this.service.CustomerPlanGH.CopyPlanAll(planid);
            if (issuccess)
            {
                return OperateResult(true, "复制成功！", null);
            }
            else
            {
                return OperateResult(false, "复制失败！", null);
            }
        }

        /// <summary>
        /// 移除历史计划单记录
        /// </summary>
        /// <param name="planid"></param>
        /// <returns></returns>
        public ActionResult RemovePlan(string planid)
        {
            try
            {
                var plan = this.service.GetGenericService<CustomerPlanGH>().Get(planid);
                plan.Lifecycle = -1;
                this.service.GetGenericService<CustomerPlanGH>().Update(plan, null);
                return OperateResult(true, "移除成功！", null);
            }
            catch(Exception ex)
            {
                return OperateResult(true, "移除失败！" + ex.Message, null);
            }
        }


        public ResultInfo IsHandInputConStrength(CustomerPlanGH entity)
        {
            var config = this.service.GetGenericService<SysConfig>().Query().Where(t => t.ConfigName == "IsHandInputConStrengthGH").FirstOrDefault();
            bool result = true;
            bool.TryParse(config.ConfigValue, out result);
            //如果匹配，则需要在合同明细中匹配合同中的砼强度2019-03-20
            ContractGH con = this.service.ContractGH.Get(entity.ContractID);
            if (result)
            {
                if (con.ContractItemsGH != null)
                {
                    var r = con.ContractItemsGH.FirstOrDefault(t => t.ConStrength == entity.ConStrength);
                    if (r == null)
                    {
                        return new ResultInfo { Result = false, Message = string.Format("当前砼强度{0}在合同明细中不存在，不能保存", entity.ConStrength) };
                    }
                }
                else
                {
                    return new ResultInfo { Result = false, Message = "请先录入合同强度明细" };
                }
            }
            else
            {
                entity.ConStrength = entity.ConStrength.ToUpper();
                //如果没有则直接给用户新增砼轻度到明细
                if (con.ContractItemsGH != null)
                {
                    var r = con.ContractItemsGH.FirstOrDefault(t => t.ConStrength == entity.ConStrength);
                    if (r == null)
                    {
                        ContractItemGH item = new ContractItemGH();
                        item.ContractID = entity.ContractID;
                        item.ConStrength = entity.ConStrength;
                        this.service.GetGenericService<ContractItemGH>().Add(item);
                    }
                }
            }
            return new ResultInfo { Result = true };

        }
    }    
}
