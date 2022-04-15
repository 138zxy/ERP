using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class SupplyInfoController : BaseController<SupplyInfo,string>
    {
        /// <summary>
        /// 更新厂商使用状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="usedStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateSupplyInfo(SupplyInfo supplyInfo)
        {
            try
            {
                base.Update(supplyInfo);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }

        /// <summary>
        /// 更新厂商使用状态(批量)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="usedStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUsedStatus(string[] ids, bool usedStatus)
        {
            try
            {
                foreach (string id in ids)
                {
                    SupplyInfo supplyInfo = m_ServiceBase.Get(id);
                    supplyInfo.IsUsed = usedStatus;
                    base.Update(supplyInfo);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }

        /// <summary>
        /// 获取供应商电话
        /// </summary>
        /// <param name="supplyid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSupplyTel(string supplyid)
        {
            try
            {
                SupplyInfo supplyInfo = m_ServiceBase.Get(supplyid);

                return OperateResult(true, Lang.Msg_Operate_Success, supplyInfo.LinkTel);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }

        public override ActionResult Add(SupplyInfo SupplyInfo)
        {
            try
            {
                var list = this.service.GetGenericService<SupplyInfo>().Find(" SupplyID like '%BF%' ", 1, 1000, "", "");
                int lastvalue = 1;
                foreach (var item in list)
                {
                    if (item.ID.Contains("BF"))
                    {
                        var idNum = Convert.ToInt32(item.ID.Substring(2).ToString());
                        if (idNum >= lastvalue)
	                    {
                            lastvalue = idNum+1;
	                    }
                    }
                }
                SupplyInfo.ID = "BF" + lastvalue.ToString();
                SupplyInfo.SupplyID = "BF" + lastvalue.ToString();
                SupplyInfo.PayMoney = SupplyInfo.PaidIn;
                SupplyInfo.PaidOwing = SupplyInfo.PaidIn;
                SupplyInfo.PrePay = SupplyInfo.PrepayInit;

                SupplyInfo.PiaoPayMoney = SupplyInfo.PiaoPaidIn;
                SupplyInfo.PiaoPaidOwing = SupplyInfo.PiaoPaidIn;

                return base.Add(SupplyInfo);
            }
            catch (Exception ex)
            {

                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }

        public override ActionResult Update(SupplyInfo SupplyInfo)
        {
            var su = this.m_ServiceBase.Get(SupplyInfo.ID);

            //如果期初应付金额发生变化
            if (SupplyInfo.PaidIn != su.PaidIn)
            {
                decimal exch = su.PaidIn - SupplyInfo.PaidIn;
                //期初欠款=期初应付-已付
                su.PaidOwing = SupplyInfo.PaidIn - su.PaidOut;
                su.PayMoney = su.PayMoney - exch;
            }
            //如果对于期初收票发生变化
            if (SupplyInfo.PiaoPaidIn != su.PiaoPaidIn)
            {
                decimal exch = su.PiaoPaidIn - SupplyInfo.PiaoPaidIn;
                //期初欠款=期初应付-已付
                su.PiaoPaidOwing = SupplyInfo.PiaoPaidIn - su.PiaoPaidOut;
                su.PiaoPayMoney = su.PiaoPayMoney - exch;
            }
            if (SupplyInfo.PrepayInit != su.PrepayInit)
            {
                var exchIni = su.PrepayInit - SupplyInfo.PrepayInit;
                su.PrePay = su.PrePay - exchIni;
            }
            su.ShortName = SupplyInfo.ShortName;
            su.SupplyKind = SupplyInfo.SupplyKind;
            su.SupplyName = SupplyInfo.SupplyName;
            su.Principal = SupplyInfo.Principal;
            su.SupplyAddr = SupplyInfo.SupplyAddr;
            su.InvoiceAddr = SupplyInfo.InvoiceAddr;
            su.BankName = SupplyInfo.BankName;
            su.BankAccount = SupplyInfo.BankAccount;
            su.PaidIn = SupplyInfo.PaidIn;
            su.PiaoPaidIn = SupplyInfo.PiaoPaidIn;
            su.PrepayInit = SupplyInfo.PrepayInit;
            su.BusinessTel = SupplyInfo.BusinessTel;
            su.BusinessFax = SupplyInfo.BusinessFax;
            su.PostCode = SupplyInfo.PostCode;

            su.PrincipalTel = SupplyInfo.PrincipalTel;
            su.LinkMan = SupplyInfo.LinkMan;
            su.LinkTel = SupplyInfo.LinkTel;
            su.SupplyType = SupplyInfo.SupplyType;
            su.CreditWorthiness = SupplyInfo.CreditWorthiness;
            su.Email = SupplyInfo.Email;
            su.IsUsed = SupplyInfo.IsUsed;
            su.IsTax = SupplyInfo.IsTax;
            su.SupplyID = SupplyInfo.SupplyID;
            su.IsNz = SupplyInfo.IsNz; 

            su.Remark = SupplyInfo.Remark;
            return base.Update(su);
        }

    }
}
