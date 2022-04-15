using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public class ContractGHController : BaseController<ContractGH, string>
    {

        [ValidateInput(false)]
        public override ActionResult Add(ContractGH ContractGH)
        {
            string day = "GH" + DateTime.Now.ToString("yyyyMMdd");
            IList<AutoGenerateId> snolist = this.service.GetGenericService<AutoGenerateId>().Query().Where(m => m.IDPrefix == day).Where(m => m.Table == "ContractGH").ToList();
            if (snolist.Count > 0)
            {
                //不足位数补0
                string nvalue = snolist[0].NextValue.ToString();
                ContractGH.ContractNo = day + "-" + nvalue.PadLeft(4, '0');

                //修改下一个值
                //sno.ID = snolist[0].ID;
                AutoGenerateId sno = snolist[0];
                sno.NextValue = snolist[0].NextValue + 1;
                this.service.GetGenericService<AutoGenerateId>().Update(sno, null);
            }
            else
            {
                AutoGenerateId sno = new AutoGenerateId();
                ContractGH.ContractNo = day + "-" + "0001";
                //新增记录
                sno.Table = "ContractGH";
                sno.IDPrefix = day;
                sno.NextValue = 2;
                this.service.GetGenericService<AutoGenerateId>().Add(sno);
            }
            //ContractGH.ContractNo = ContractGH.ContractName;
            return base.Add(ContractGH);
        }

        public ActionResult CancelAudit(string[] ids)
        {
            string errmsg = "";
            bool ret = this.service.ContractGH.CancelAudit(ids, ref errmsg);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed + ":" + errmsg, false);
        }

        public ActionResult AuditNew(string[] ids)
        {
            string errmsg = "";
            bool ret = this.service.ContractGH.Audit(ids, ref errmsg);
            return OperateResult(ret, ret ? Lang.Msg_Operate_Success : Lang.Msg_Operate_Failed + ":" + errmsg, false);
        }

        public ActionResult ControlIndex()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode not like 'C%' ", "ConStrengthCode", true,"");
 
            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName",
                "ID",
                string.Format("UserType='{0}'", Params["salestype"]),
                "TrueName",
                true,
                "");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");

            List<SelectListItem> BitUpdatePrice = new List<SelectListItem>();
            BitUpdatePrice.Add(new SelectListItem { Text = "按百分比", Value = "0" });
            BitUpdatePrice.Add(new SelectListItem { Text = "按数额", Value = "1" });
            BitUpdatePrice.Add(new SelectListItem { Text = "按基准强度", Value = "2" });
            ViewBag.BitUpdatePrice = BitUpdatePrice;

            List<SelectListItem> Relation = new List<SelectListItem>();
            Relation.Add(new SelectListItem { Text = "并且", Value = "并且" });
            Relation.Add(new SelectListItem { Text = "或者", Value = "或者" });

            ViewBag.Relation = Relation;


            base.InitCommonViewBag();
            string funcId = Request.QueryString["f"];
            ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));
            ViewBag.ExtraPumpList = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "设置润管砂浆", Text = "设置润管砂浆" },
                    new SelectListItem(){ Value = "设置臂架泵", Text = "设置臂架泵" },
            };
            return View();
        }

        /// <summary>
        /// 新增批量调价的功能
        /// </summary>
        /// <returns></returns>
        public ActionResult BitUpdatePrice(List<string> keys, int BitUpdateType, decimal BitUpdateCnt, DateTime BitUpdateDate)
        {

            if (keys == null || keys.Count <= 0 || BitUpdateCnt == 0)
            {
                return OperateResult(false, "参数错误", "");
            }
            List<BitUpdatePrice> BitUpdatePrices = new List<BitUpdatePrice>();
            foreach (var key in keys)
            {
                BitUpdatePrice UpdatePrice = new BitUpdatePrice();
                UpdatePrice.BitContractID = key;
                UpdatePrice.BitUpdateType = BitUpdateType;
                UpdatePrice.BitUpdateCnt = BitUpdateCnt;
                UpdatePrice.BitUpdateDate = BitUpdateDate;
                BitUpdatePrices.Add(UpdatePrice);
            }
            ResultInfo ResultInfo = this.service.ContractGH.BitUpdatePrice(BitUpdatePrices);
            return OperateResult(ResultInfo.Result, ResultInfo.Message, ResultInfo.Data);

        }


        public ActionResult SalesmanIndex()
        {
            base.InitCommonViewBag();
            return View();
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <returns></returns>
        public ActionResult PayIndex()
        {
            base.InitCommonViewBag();
            ViewBag.DZType = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "累计垫资", Text = "累计垫资" },
                    new SelectListItem(){ Value = "当月应收垫资", Text = "当月应收垫资" },
            };
            return View();
        }
        
        public ActionResult balanceIndex()
        {
            base.InitCommonViewBag();
            return View();
        }

        //增加余额或者减少余额
        public ActionResult AddAccountBalance(string[] values)
        {
            lock (customerObj)
            {
                try
                {
                    //检查锁
                    string custID = values[0];
                    string money = values[1];
                    string remark = values[2];
                    Customer customer = this.service.Customer.Get(custID);
                    if (customer == null)
                        return OperateResult(false, "找不到客户，请刷新。", null);
                    decimal m;
                    if (!decimal.TryParse(money, out m))
                    {
                        return OperateResult(false, "余额请填写数字", null);
                    }

                    decimal _money = m;
                    customer.AccountBalance += _money;
                    if (customer.AccountBalance < 0)
                        return OperateResult(false, "余额不能小于0。", null);
                    this.service.Customer.Update(customer,null);

                    Balance op = new Balance();
                    op.CustomerID = custID;
                    op.Balance = m;
                    op.Remark = remark;
                    op.userID = AuthorizationService.CurrentUserID;
                    this.service.Balance.Add(op);

                    this.service.Balance.SendMsg("客户名称：" + customer.CustName + " 充值：" + _money + "元");
                    
                    return OperateResult(true, Lang.Msg_Operate_Success, null);
                }
                catch (Exception e)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
                }
            }
        }

        public override ActionResult Update(ContractGH ContractGH)
        {
            return base.Update(ContractGH);
        }

        /// <summary>
        /// 结算确认
        /// </summary>
        /// <param name="ContractGH"></param>
        /// <returns></returns>
        public ActionResult Confirm(ContractGH ContractGH)
        {
            ContractGH entity = this.service.ContractGH.Get(ContractGH.ID);
            entity.FootDate = ContractGH.FootDate;
            List<ShippingDocumentGH> sdlists = this.service.ShippingDocumentGH.All().Where(p => p.ContractID == ContractGH.ID && p.BuildTime < ContractGH.FootDate && p.IsJS == false).ToList();
            foreach(var a in sdlists)
            {
                this.service.ContractGH.JS(a);
            }
            return base.Update(entity);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult Audit(ContractGH ContractGH)
        {
            try
            {
                this.service.ContractGH.Audit(ContractGH);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ContractGH.ID, null, "合同审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
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
                this.service.ContractGH.UnAudit(contractID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, contractID, null, "合同取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
            
        }
        /// <summary>
        /// 快速解除限制
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="contractStatus"></param>
        /// <param name="contractLimitType"></param>
        /// <returns></returns>
        public ActionResult QuickUnfreeze(string contractID, string contractStatus, string contractLimitType)
        {
            try
            {
                this.service.ContractGH.QuickUnfreeze(contractID, contractStatus, contractLimitType);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
        }
        
        /// <summary>
        /// 快速锁定合同
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult QuickLock(string contractID,string remark)
        {
            try
            {
                this.service.ContractGH.QuickLock(contractID, remark);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
        }

        public override ActionResult Index()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
 
            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName",
                "ID",
                string.Format("UserType='{0}'", Params["salestype"]),
                "TrueName",
                true,
                "");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            return base.Index();
        }

        /// <summary>
        /// 获取砼列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getConList()
        {
            var conlist = this.service.ConStrength.Query().Where(p => p.ConStrengthCode.Length == 3 || (p.ConStrengthCode.Length == 5 && (p.ConStrengthCode.Contains("低碱") || p.ConStrengthCode.Contains("耐久")))).OrderBy(p=>p.ConStrengthCode);
            return Json(conlist);
        }

        /// <summary>
        /// 将合同置为完工
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult SetComplete(string contractID)
        {
            bool result = this.service.ContractGH.SetComplete(contractID);
            return OperateResult(result, Lang.Msg_Operate_Success, result);
        }

        /// <summary>
        /// 将合同置为未完工
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult SetUnComplete(string contractID)
        {
            bool result = this.service.ContractGH.SetUnComplete(contractID);
            return OperateResult(result, Lang.Msg_Operate_Success, result);
        }

        /// <summary>
        /// 获取特性价格
        /// </summary>
        /// <param name="identityName"></param>
        /// <param name="identityType"></param>
        /// <returns></returns>
        public ActionResult getIdentityPrice(string identityName, string identityType)
        {
            dynamic identity = this.service.ContractGH.getIdentityPrice(identityName, identityType);

            return this.Json(identity);
        }
        /// <summary>
        /// 导入砼强度
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="conStrength"></param>
        /// <returns></returns>
        public ActionResult Import(string contractID, string[] conStrength)
        {
            try
            {
                bool result = this.service.ContractGH.Import(contractID, conStrength);
                return OperateResult(true, Lang.Msg_Operate_Success, result);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);
            }
        }
    }
}
