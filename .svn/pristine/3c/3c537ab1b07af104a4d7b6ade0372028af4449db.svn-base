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
    public class CustomerPlanController : BaseController<CustomerPlan, string>
    {
        public override ActionResult Index()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);

            var pumpList = this.service.Car.GetPumpList()
            .OrderBy(p => p.ID);
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " (UserType = '51' or  UserType = '15' )  AND IsUsed=1 ", "ID", true, "");
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
        public ActionResult IndexSH()
        {
            base.InitCommonViewBag();
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);

            var pumpList = this.service.Car.GetPumpList()
            .OrderBy(p => p.ID);
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " (UserType = '51' or  UserType = '15' )  AND IsUsed=1 ", "ID", true, "");
            ViewBag.PumpManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '52' AND IsUsed=1 ", "ID", true, "");

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
            return View();
        }
        public override ActionResult Find(JqGridRequest request, string condition)
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
        public ActionResult ManageFind(JqGridRequest request, string condition)
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
        public JsonResult Auditing(CustomerPlan plan)
        {
            bool ret = this.service.CustomerPlan.Auditing(plan);
            
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
        }

        public ActionResult MultAudit(string[] ids)
        {
            bool ret = this.service.CustomerPlan.MultAudit(ids);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed, false);
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult CancelAudit(string[] ids)
        {
            string errmsg = "";
            bool ret = this.service.CustomerPlan.CancelAudit(ids, ref errmsg);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed + ":" + errmsg, false);
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
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<Contract> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.Contract.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.Contract.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>[{5}]<br/>{3}：{1}<br/>{4}：{2}",
                        HelperExtensions.Eval(p, textField),
                        p.CustName,
                        p.ContractName,
                        Lang.Entity_Property_CustName,
                        Lang.Entity_Property_ContractName,
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        public override ActionResult Update(CustomerPlan entity)
        {
            var planInDB = this.service.CustomerPlan.Get(entity.ID);

            if (planInDB != null && planInDB.AuditStatus == (int)AuditStatus.Pass)
            {
                return OperateResult(false, "已审核的计划不能修改", null);
            }
            //根据合同和工程名称匹配出工程ID号 
            var project = this.service.GetGenericService<Project>().Query().Where(t => t.ContractID == entity.ContractID && t.ProjectName == entity.ProjectName).FirstOrDefault();
            if (project != null)
            {
                entity.ProjectIDApp = project.ID;
            } 
            var ResultInfo = IsHandInputConStrength(entity);
            if (!ResultInfo.Result)
            {
                return OperateResult(false, ResultInfo.Message, null);
            } 
            return base.Update(entity);
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
        //public virtual JsonResult ListDataStrengthInfo(string id, string textField, string valueField, string orderBy = "ID", bool ascending = false, string condition = "")
        //{
        //    IEnumerable<SelectListItem> list = HelperExtensions.SelectListData<ContractItem>("ConStrength", "ID", condition, "ConStrength", true, null);
        //    return Json(list);
        //}

        public virtual JsonResult ListDataStrengthInfo(string textField, string valueField,string condition = "")
        {
            IEnumerable<SelectListItem> list = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ID", condition, "ConStrengthCode", true, null);
            return Json(list);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(CustomerPlan entity)
        {


            if (!string.IsNullOrEmpty(entity.ConStrength))
            {
                if (entity.ConStrength.ToUpper().IndexOf("O") >= 0)
                {
                    return OperateResult(false, "砼强度中包含非法字符‘O’，请注意检查", null);
                }
            }

            //根据合同和工程名称匹配出工程ID号 
            var project = this.service.GetGenericService<Project>().Query().Where(t => t.ContractID == entity.ContractID && t.ProjectName == entity.ProjectName).FirstOrDefault();
            if (project != null)
            {
                entity.ProjectIDApp = project.ID;
            } 

            var ResultInfo = IsHandInputConStrength(entity);
            if (!ResultInfo.Result)
            {
                return OperateResult(false, ResultInfo.Message, null);
            }
            
            return base.Add(entity);
        }

        [HandleAjaxError]
        public JsonResult SaveCustPlanIdentities(string custPlanId, int[] identities)
        {
            CustomerPlan cp = this.service.CustomerPlan.Get(custPlanId);
            List<IdentitySetting> list = new List<IdentitySetting>();
            if (identities != null)
            {
                var setts = this.service.GetGenericService<IdentitySetting>().Query()
                    .Where(p => identities.Contains(p.ID ?? 0))
                    .ToList();
                list = setts;
                string temp = "";


                foreach (var a in list)
                {
                    temp = temp + a.IdentityName + ",";
                }
                if (temp != "")
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
                cp.IdentityValue = temp;
                this.Update(cp);
            }
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }

        /// <summary>
        /// 根据配置文件，在新建工地计划的时候，判断砼强度是否能手动随意输入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo IsHandInputConStrength(CustomerPlan entity)
        {
            //判断浇筑方式是否在字典中存在

            var castMode = entity.CastMode;
            var dic = this.service.GetGenericService<Dic>().Query().FirstOrDefault(t => t.ParentID == DicEnum.CastMode.ToString() && t.DicName == castMode);
            if (dic == null)
            {
                return new ResultInfo { Result = false, Message = "输入的浇筑方式有误，确保字典中存在此浇筑方式" };
            }

            var config = this.service.GetGenericService<SysConfig>().Query().Where(t => t.ConfigName == "IsHandInputConStrength").FirstOrDefault();
            bool result = true;
            bool.TryParse(config.ConfigValue, out result);
            //如果匹配，则需要在合同明细中匹配合同中的砼强度2019-03-20
            Contract con = this.service.Contract.Get(entity.ContractID);
            if (result)
            {
                if (con.ContractItems != null)
                {
                    var r = con.ContractItems.FirstOrDefault(t => t.ConStrength == entity.ConStrength);
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
                //entity.ConStrength = entity.ConStrength.Trim().ToUpper();
                var strength = this.service.GetGenericService<ConStrength>().Query().FirstOrDefault(t => t.ConStrengthCode == entity.ConStrength);
                //判断是否存在基准砼强度
                if (strength != null)
                {
                    if (con.ContractItems != null)//判断当前合同是否有砼强度明细
                    {
                        var r = con.ContractItems.FirstOrDefault(t => t.ConStrength == entity.ConStrength);
                        if (r == null)
                        {
                            ContractItem item = new ContractItem();
                            item.ContractID = entity.ContractID;
                            item.ConStrength = entity.ConStrength;
                            item.ConStrengthID = Convert.ToInt32(strength.ID);
                            item.UnPumpPrice = strength.BrandPrice;
                            this.service.GetGenericService<ContractItem>().Add(item);
                        }
                    }
                    else
                    {
                        ContractItem item = new ContractItem();
                        item.ContractID = entity.ContractID;
                        item.ConStrength = entity.ConStrength;
                        item.ConStrengthID = Convert.ToInt32(strength.ID);
                        item.UnPumpPrice = strength.BrandPrice;
                        this.service.GetGenericService<ContractItem>().Add(item);
                    }
                }
                else {
                    //不存在当前输入的砼强度
                    ConStrength constr = new ConStrength();
                    constr.ConStrengthCode = entity.ConStrength;
                    constr.IsBase = true;
                    this.service.GetGenericService<ConStrength>().Add(constr);//新增一条砼强度基准信息

                    ContractItem item = new ContractItem();
                    item.ContractID = entity.ContractID;
                    item.ConStrength = entity.ConStrength;
                    item.ConStrengthID = Convert.ToInt32(constr.ID);
                    item.UnPumpPrice = 0.00m;
                    this.service.GetGenericService<ContractItem>().Add(item);//新增一条合同明细
                }
            }
            return new ResultInfo { Result = true };

        }
    }   
}
