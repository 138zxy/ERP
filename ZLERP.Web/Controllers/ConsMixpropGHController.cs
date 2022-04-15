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
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
namespace ZLERP.Web.Controllers
{
    public class ConsMixpropGHController : BaseController<ConsMixpropGH, string>
    {

        public ActionResult SendModifiedPBToKZ(ConsMixpropGH ConsMixpropGH, string[] DirtyDataArr)
        {
            try {
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == ConsMixpropGH.TaskID && (p.BetonFormula == ConsMixpropGH.ID || p.SlurryFormula == ConsMixpropGH.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();
                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("正在生产的任务单不允许修改配比");
                    }
                }
                //先保存子表数据,只用到编号与数量
                this.service.ConsMixpropGH.SendModifiedPBToKZ(ConsMixpropGH, DirtyDataArr);
                //更新容重
                SqlServerHelper helper = new SqlServerHelper();
                int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rzGH", new System.Data.SqlClient.SqlParameter("@FormulaID", ConsMixpropGH.ID));
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }catch(Exception ex){
            
                return OperateResult(false, ex.Message, false);
            }
        }
        public override ActionResult Update(ConsMixpropGH ConsMixpropGH)
        {
            try
            {
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == ConsMixpropGH.TaskID && (p.BetonFormula == ConsMixpropGH.ID || p.SlurryFormula == ConsMixpropGH.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("正在生产的任务单不允许修改配比");

                    }
                }
                return base.Update(ConsMixpropGH);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixpropGH"></param>
        /// <returns></returns>
        public ActionResult Audit(ConsMixpropGH ConsMixpropGH)
        {
            try
            {
                var cons = this.service.ConsMixpropGH.Get(ConsMixpropGH.ID);
                var message = "";
                var result = this.service.ProduceTaskGH.IsWarpConsMixprop(cons, out message);
                if (result)
                {
                    return OperateResult(false, message, null);
                }
                this.service.ConsMixpropGH.Audit(ConsMixpropGH);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ConsMixpropGH.ID, null, "施工配比审核");
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
        /// <param name="consMixID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string consMixID, int auditStatus)
        {
            try
            {
                this.service.ConsMixpropGH.UnAudit(consMixID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, consMixID, null, "施工配比取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ConsMixpropGH"></param>
        /// <returns></returns>
        public ActionResult BacthAudit(string[] ids,int auditStatus)
        {
            try
            {
                this.service.ConsMixpropGH.BatchAudit(ids, auditStatus);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public override ActionResult Index()
        {
            ViewBag.ProductLineList = HelperExtensions.SelectListData<ProductLine>("ProductLineName", "ID", "IsUsed=1 and IsGH=1 ", "ID", true, "");
            return base.Index();
        }

        public override ActionResult Delete(string[] id)
        {
            foreach (object item in id)
            {
                ConsMixpropGH cm = this.m_ServiceBase.Get(item);
                DateTime dt1 = cm.BuildTime;
                
                string taskid = cm.TaskID;
                string pid = cm.ProductLineID;
                ProduceTaskGH pt = this.service.ProduceTaskGH.Get(taskid);
                ShippingDocument doc = this.service.ShippingDocument.Find("TaskID = '" + taskid + "' AND IsEffective = 1 AND ShipDocType = '0'", 1, 1, "ID", "DESC").FirstOrDefault();
                if (doc != null)
                {
                    DateTime dt2 = doc.BuildTime;

                    if (doc.ProvidedCube > 0 && doc.ProductLineID == pid && dt1 < dt2)
                    {
                        return OperateResult(false, Lang.Msg_Operate_Failed + "已生产的施工配比不允许删除", "");
                    }
                }

            }
            return base.Delete(id);
        }

        /// <summary>
        /// 根据生产线号获取动态列头
        /// </summary>
        /// <param name="ProductLineID"></param>
        /// <returns></returns>
        public ActionResult GetDynamicCol(string ProductLineID) {
            dynamic dynamicCol = this.service.ConsMixpropGH.GetDynamicColByPid(ProductLineID);
            return this.Json(dynamicCol);
        }

        public ActionResult SaveAll(string DirtyDatas) {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                object json = serializer.DeserializeObject(DirtyDatas);
                List<ConsMixpropGH> listCms = serializer.ConvertToType<List<ConsMixpropGH>>(json);
                List<Dictionary<string, string>> cmDicList = serializer.ConvertToType<List<Dictionary<string, string>>>(json);
                IList<string> allErrorList = new List<string>();
                Dictionary<string, string> failedConsMixID = new Dictionary<string, string>();
                foreach (Dictionary<string, string> cmDic in cmDicList)
                {
                    string id = cmDic["id"];
                    foreach (ConsMixpropGH cm in listCms)
                    {
                        if (cm.ID == id)
                        {
                            Dictionary<string, string>.KeyCollection allKeys = cmDic.Keys;
                            NameValueCollection updateKeys = new NameValueCollection();
                            foreach (string key in allKeys)
                            {
                                updateKeys.Add(key, "");
                            }
                            //更新主表和子表
                            IList<string> returnError = this.service.ConsMixpropGH.UpdatePrimaryAndItems(cm, updateKeys);
                            if (returnError.Count != 0) { //说明该条配比修改失败，记录编号以便标记
                                failedConsMixID.Add(cm.ID, cm.ID);
                            }
                            //更新容重
                            SqlServerHelper helper = new SqlServerHelper();
                            int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rzGH", new System.Data.SqlClient.SqlParameter("@FormulaID", cm.ID));
                            allErrorList = allErrorList.Concat(returnError).ToList<string>();
                            break;
                        }
                    }

                }
                if (allErrorList.Count > 0)
                {//部分修改成功但有错误
                    return OperateResult(false, string.Join("<br/>", allErrorList), failedConsMixID);
                }
                else {//完全成功
                    return OperateResult(true, Lang.Msg_Operate_Success, ""); 
                }
            }
            catch (Exception e) {
                return OperateResult(false, e.Message, "");
            }
            
        }

        public ActionResult QuickModifyRZ(string rzRange) {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                object json = serializer.DeserializeObject(rzRange);
                List<Dictionary<string, decimal>> cmDicList = serializer.ConvertToType<List<Dictionary<string, decimal>>>(json);
                foreach (Dictionary<string, decimal> d in cmDicList)
                {
                    SysConfig scf = this.service.SysConfig.GetSysConfig(d.First().Key);
                    scf.ConfigValue = d.First().Value.ToString();
                    this.service.SysConfig.Update(scf, null);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, ""); 
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, "");
            }
            
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            ActionResult s = base.Find(request, condition);
            //IList<ConsMixpropGH> list = ((JqGridData<ConsMixpropGH>)((JsonResult)s).Data).rows;
            //foreach (ConsMixpropGH b in list)
            //{
            //    b.CompanyName = this.service.ProductLine.Get(b.ProductLineID).CompName;
            //}
            return s;
        }
    }
}
