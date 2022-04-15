﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class ContractItemController : BaseController<ContractItem, int>
    {

        public override ActionResult Add(ContractItem contractItem)
        {

            var contractid = contractItem.ContractID;
            //判断合同里面是否已经存在此砼强度
            var item = this.service.GetGenericService<ContractItem>().Query().FirstOrDefault(t => t.ContractID == contractid && t.ConStrength == contractItem.ConStrength);
            if (item != null)
            {
                return OperateResult(false, "当前合同中已经存在该砼强度，无法重复添加", null);
            }
            //根据砼强度获取砼强度主键ID
            var con = this.service.GetGenericService<ConStrength>().Query().FirstOrDefault(t => t.ConStrengthCode == contractItem.ConStrength);
            if (con != null)
            {
                contractItem.ConStrengthID = Convert.ToInt32(con.ID);
            }
            contractItem.UnPumpPrice = contractItem.TranPrice;
            return base.Add(contractItem);
        }

        public override ActionResult Update(ContractItem contractItem)
        {
            var item = this.service.GetGenericService<ContractItem>().Get(contractItem.ID);
             
            var query = this.service.GetGenericService<Contract>().Query().FirstOrDefault(t => t.ID == item.ContractID);
            if (query != null && query.AuditStatus == 1)
            {
                return OperateResult(false, "已审核的合同无法直接修改基准信息", null);
            }
            //if (Convert.ToDecimal(contractItem.UnPumpPrice) <= 0)
            //{
            //    contractItem.UnPumpPrice = contractItem.TranPrice;
            //}
            return base.Update(contractItem);
        }
        /// <summary>
        /// 判断砼强度是否属于合同里面
        /// </summary>
        /// <param name="contractid"></param>
        /// <param name="constrength"></param>
        /// <returns></returns>
        public ActionResult getContractItem(string contractid, string constrength)
        {
            var cons = this.service.GetGenericService<ContractItem>().Find("contractid='" + contractid + "' and constrength='"+constrength+"'", 1, 100, "", "");
            if (cons.ToList().Count > 0)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }

        public override ActionResult Delete(int[] id)
        {
            var contractItem = this.service.GetGenericService<ContractItem>().Query().FirstOrDefault(t => t.ID == id[0]);
            var contractid = contractItem.ContractID;
            var query = this.service.GetGenericService<Contract>().Query().FirstOrDefault(t => t.ID == contractid);
            if (query != null && query.AuditStatus == 1)
            {
                return OperateResult(false, "已审核的合同无法删除砼列表", null);
            }
            return base.Delete(id);
        }


        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ContractItem sd = this.service.GetGenericService<ContractItem>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus == 0 && sd.AuditStatus2 == 0)
                    {
                        sd.AuditStatus = 1;
                        sd.Auditor = AuthorizationService.CurrentUserID;
                        sd.AuditTime = DateTime.Now;
                        this.service.GetGenericService<ContractItem>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 批量取消审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit_N(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ContractItem sd = this.service.GetGenericService<ContractItem>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus == 1 && sd.AuditStatus2 == 0)
                    {
                        sd.AuditStatus = 0;
                        sd.Auditor = AuthorizationService.CurrentUserID;
                        sd.AuditTime = DateTime.Now;
                        this.service.GetGenericService<ContractItem>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        /// <summary>
        /// 批量二次审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit2(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ContractItem sd = this.service.GetGenericService<ContractItem>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus2 == 0 && sd.AuditStatus == 1)
                    {
                        sd.AuditStatus2 = 1;
                        sd.Auditor2 = AuthorizationService.CurrentUserID;
                        sd.AuditTime2 = DateTime.Now;
                        this.service.GetGenericService<ContractItem>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 批量取消二次审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit2_N(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    ContractItem sd = this.service.GetGenericService<ContractItem>().Get(Convert.ToInt32(id));
                    if (sd.AuditStatus2 == 1 && sd.AuditStatus == 1)
                    {
                        sd.AuditStatus2 = 0;
                        sd.Auditor2 = AuthorizationService.CurrentUserID;
                        sd.AuditTime2 = DateTime.Now;
                        this.service.GetGenericService<ContractItem>().Update(sd);
                    }
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
    }
}
