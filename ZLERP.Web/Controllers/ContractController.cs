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
using ZLERP.Model.Beton;

namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public class ContractController : BaseController<Contract, string>
    {

        //[ValidateInput(false)]
        public override ActionResult Add(Contract Contract)
        {
            if (Contract.DataType == 1)
            {
                string day = "SB" + DateTime.Now.ToString("yyyyMMdd");
                IList<AutoGenerateId> snolist = this.service.GetGenericService<AutoGenerateId>().Query().Where(m => m.IDPrefix == day).Where(m => m.Table == "ContractSH").ToList();
                if (snolist.Count > 0)
                {
                    //不足位数补0
                    string nvalue = snolist[0].NextValue.ToString();
                    Contract.ContractNo = day + "-" + nvalue.PadLeft(4, '0');

                    //修改下一个值
                    //sno.ID = snolist[0].ID;
                    AutoGenerateId sno = snolist[0];
                    sno.NextValue = snolist[0].NextValue + 1;
                    this.service.GetGenericService<AutoGenerateId>().Update(sno, null);
                }
                else
                {
                    AutoGenerateId sno = new AutoGenerateId();
                    Contract.ContractNo = day + "-" + "0001";
                    //新增记录
                    sno.Table = "ContractSH";
                    sno.IDPrefix = day;
                    sno.NextValue = 2;
                    this.service.GetGenericService<AutoGenerateId>().Add(sno);
                }
            }
            else
            {

                string day = "SH" + DateTime.Now.ToString("yyyyMMdd");
                IList<AutoGenerateId> snolist = this.service.GetGenericService<AutoGenerateId>().Query().Where(m => m.IDPrefix == day).Where(m => m.Table == "Contract").ToList();
                if (snolist.Count > 0)
                {
                    //不足位数补0
                    string nvalue = snolist[0].NextValue.ToString();
                    Contract.ContractNo = day + "-" + nvalue.PadLeft(4, '0');

                    //修改下一个值
                    //sno.ID = snolist[0].ID;
                    AutoGenerateId sno = snolist[0];
                    sno.NextValue = snolist[0].NextValue + 1;
                    this.service.GetGenericService<AutoGenerateId>().Update(sno, null);
                }
                else
                {
                    AutoGenerateId sno = new AutoGenerateId();
                    Contract.ContractNo = day + "-" + "0001";
                    //新增记录
                    sno.Table = "Contract";
                    sno.IDPrefix = day;
                    sno.NextValue = 2;
                    this.service.GetGenericService<AutoGenerateId>().Add(sno);
                }
            }
            Contract.PayMoney = Contract.PaidIn;
            Contract.PaidOwing = Contract.PaidIn;
            Contract.PrePay = Contract.PrepayInit;

            Contract.PiaoPayMoney = Contract.PiaoPaidIn;
            Contract.PiaoPaidOwing = Contract.PiaoPaidIn;
            return base.Add(Contract);
        }

        public ActionResult ControlIndex()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "IsBase=1", "ConStrengthCode", true, "");

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
            //string funcId = Request.QueryString["f"];
            //ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            //ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));

            return View();
        }


        public ActionResult ControlIndexSH()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "IsBase=1", "ConStrengthCode", true, "");

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
            //string funcId = Request.QueryString["f"];
            //ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            //ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));

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

        public override ActionResult Update(Contract Contract)
        {
            var su = this.m_ServiceBase.Get(Contract.ID);
            bool ischang = false;
            //如果期初应付金额发生变化
            if (Contract.PaidIn != su.PaidIn)
            {
                decimal exch = su.PaidIn - Contract.PaidIn;
                //期初欠款=期初应付-已付
                su.PaidOwing = Contract.PaidIn - su.PaidOut;
                su.PayMoney = su.PayMoney - exch;
                ischang = true;
            }
            //如果期初应开票金额发生变化
            if (Contract.PiaoPaidIn != su.PiaoPaidIn)
            {
                decimal exch = su.PiaoPaidIn - Contract.PiaoPaidIn;
                //期初欠款=期初应付-已付
                su.PiaoPaidOwing = Contract.PiaoPaidIn - su.PiaoPaidOut;
                su.PiaoPayMoney = su.PiaoPayMoney - exch;
                ischang = true;
            }
            if (Contract.PrepayInit != su.PrepayInit)
            {
                var exchIni = su.PrepayInit - Contract.PrepayInit;
                su.PrePay = su.PrePay - exchIni;
                ischang = true;
            }
            if (ischang)
            {
                this.m_ServiceBase.Update(su, null);
            }
            return base.Update(Contract);

        }

        /// <summary>
        /// 结算确认
        /// </summary>
        /// <param name="Contract"></param>
        /// <returns></returns>
        //public ActionResult Confirm(Contract Contract)
        //{
        //    Contract entity = this.service.Contract.Get(Contract.ID);
        //    entity.FootDate = Contract.FootDate;
        //    List<ShippingDocument> sdlists = this.service.ShippingDocument.All().Where(p => p.ContractID == Contract.ID && p.BuildTime < Contract.FootDate && p.IsJS == false).ToList();
        //    foreach (var a in sdlists)
        //    {
        //        this.service.Contract.JS(a);
        //    }
        //    return base.Update(entity);
        //}
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult Audit(Contract Contract)
        {
            try
            {
                this.service.Contract.Audit(Contract);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, Contract.ID, null, "合同审核");
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
                this.service.Contract.UnAudit(contractID, auditStatus);
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
                this.service.Contract.QuickUnfreeze(contractID, contractStatus, contractLimitType);
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
        public ActionResult QuickLock(string contractID, string remark)
        {
            try
            {
                this.service.Contract.QuickLock(contractID, remark);
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }
        }

        public override System.Web.Mvc.ActionResult Index()
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
            var conlist = this.service.ConStrength.Query().Where(p => p.ConStrengthCode.Length == 3 || (p.ConStrengthCode.Length == 5 && (p.ConStrengthCode.Contains("低碱") || p.ConStrengthCode.Contains("耐久")))).OrderBy(p => p.ConStrengthCode);
            return Json(conlist);
        }

        /// <summary>
        /// 将合同置为完工
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult SetComplete(string contractID)
        {
            bool result = this.service.Contract.SetComplete(contractID);
            return OperateResult(result, Lang.Msg_Operate_Success, result);
        }

        /// <summary>
        /// 将合同置为未完工
        /// </summary>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult SetUnComplete(string contractID)
        {
            bool result = this.service.Contract.SetUnComplete(contractID);
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
            dynamic identity = this.service.Contract.getIdentityPrice(identityName, identityType);

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
                bool result = this.service.Contract.Import(contractID, conStrength);
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

        /// <summary>
        /// 运费模板
        /// </summary>
        /// <returns></returns>
        public ActionResult getB_FareTempletList()
        {
            var Templetlist = this.service.GetGenericService<B_FareTemplet>().All();
            return Json(Templetlist);
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
            ResultInfo ResultInfo = this.service.Contract.BitUpdatePrice(BitUpdatePrices);
            return OperateResult(ResultInfo.Result, ResultInfo.Message, ResultInfo.Data);

        }


        public ActionResult getConStrengthPrice(string DataType)
        {
            ContractItem contractitem = new ContractItem();
            var cons = this.service.ConStrength.Query().Where(a => a.IsBase == true ).ToList();
            if (DataType == "1")
            {
                cons = cons.Where(a => a.IsSH == 1).ToList();

                ConStrength jz = cons.Where(p => p.IsBaseCon).FirstOrDefault();
                if (jz==null)
                {
                    return OperateResult(true, "未录入定价砼强度单价", "");
                }
                return OperateResult(true, Lang.Msg_Operate_Success, jz.BrandPrice);
            }
            else {
                cons = cons.Where(a => a.IsSH == 0).ToList();

                ConStrength jz = cons.Where(p => p.IsBaseCon).FirstOrDefault();
                if (jz == null)
                {
                    return OperateResult(true, "未录入定价砼强度单价", "");
                }
                return OperateResult(true, "定价砼强度查询成功", jz.BrandPrice);
            }
        }
        #region 合同管理


        public ActionResult ContractManage()
        {
            ViewBag.ConStrength = HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "IsBase=1", "ConStrengthCode", true, "");

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
            //string funcId = Request.QueryString["f"];
            //ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            //ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));

            return View();
        }

        #endregion

        public ActionResult UpdateBalanceRecordAndItems(string contractId)
        {
            try
            {
                this.service.Contract.UpdateBalanceRecordAndItems(contractId);
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
        /// 修改NC项目绑定
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="Bd_Project"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeProjectNC(string ProjectID, string Bd_Project)
        {
            try
            {
                Project entity = this.service.Project.Get(ProjectID);
                entity.Bd_Project = Bd_Project;
                this.service.Project.Update(entity);
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
    }
}
