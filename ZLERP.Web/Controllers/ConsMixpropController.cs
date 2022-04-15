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
    public class ConsMixpropController : BaseController<ConsMixprop, string>
    {
        public override ActionResult Index()
        {
            ViewBag.ProductLineList = HelperExtensions.SelectListData<ProductLine>("ProductLineName", "ID", "IsUsed=1 ", "ID", true, "");
            ViewBag.ProductLineList2 = this.service.GetGenericService<ProductLine>()
                .Query()
                .Where(m => m.IsUsed)
                .ToList();
            return base.Index();
        }
        public ActionResult IndexSH()
        {
            base.InitCommonViewBag();
            ViewBag.ProductLineList = HelperExtensions.SelectListData<ProductLine>("ProductLineName", "ID", "IsUsed=1 ", "ID", true, "");
            ViewBag.ProductLineList2 = this.service.GetGenericService<ProductLine>()
                .Query()
                .Where(m => m.IsUsed)
                .ToList();
            return View();
        }
        public ActionResult SendModifiedPBToKZ(ConsMixprop ConsMixprop, string[] DirtyDataArr)
        {
            try {
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == ConsMixprop.TaskID && (p.BetonFormula == ConsMixprop.ID || p.SlurryFormula == ConsMixprop.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("正在生产的任务单不允许修改配比");

                    }
                }
                //先保存子表数据,只用到编号与数量
                this.service.ConsMixprop.SendModifiedPBToKZ(ConsMixprop, DirtyDataArr);
                //更新容重
                SqlServerHelper helper = new SqlServerHelper();
                int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rz", new System.Data.SqlClient.SqlParameter("@FormulaID", ConsMixprop.ID));
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }catch(Exception ex){
            
                return OperateResult(false, ex.Message, false);
            }
        }
        public override ActionResult Update(ConsMixprop ConsMixprop)
        {
            try
            {
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == ConsMixprop.TaskID && (p.BetonFormula == ConsMixprop.ID || p.SlurryFormula == ConsMixprop.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("正在生产的任务单不允许修改配比");

                    }
                }
                return base.Update(ConsMixprop);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult Audit(ConsMixprop ConsMixprop)
        {
            try
            {
                var cons = this.service.ConsMixprop.Get(ConsMixprop.ID);
                //SysConfig config = this.service.SysConfig.GetSysConfig("IsPHBAuditInfo");
                //if (config.ConfigValue == "true")
                //{
                //    if (string.IsNullOrEmpty(cons.ProduceTask.OtherDemand))
                //    {
                //        return OperateResult(false, "审核失败，其他技术要求必填", null);
                //    }
                //}
                var message = "";
                var result = this.service.ProduceTask.IsWarpConsMixprop(cons, out message);
                if (result)
                {
                    return OperateResult(false, message, null);
                }
                this.service.ConsMixprop.Audit(ConsMixprop);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ConsMixprop.ID, null, "施工配比审核");
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
                this.service.ConsMixprop.UnAudit(consMixID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, consMixID, null, "施工配比取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }
        /// <summary>
        /// 批量修改施工配比明细
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult BatchEditConsMixpropItems(string consid, string taskid, string pid)
        {
            try
            {
                this.service.ConsMixprop.BatchEditConsMixpropItems(consid, taskid, pid);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public override ActionResult Delete(string[] id)
        {
            foreach (object item in id)
            {
                ConsMixprop cm = this.m_ServiceBase.Get(item);
                DateTime dt1 = cm.BuildTime;
                
                string taskid = cm.TaskID;
                string pid = cm.ProductLineID;
                ProduceTask pt = this.service.ProduceTask.Get(taskid);
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
        /*XJM 2014/5/4注释：与Base.Find()相同，无需重写
        public override ActionResult Find(JqGridRequest request, string condition)
        {
             int total;
             IList<ConsMixprop> list =  this.service.ConsMixprop.Find(request,condition,out total);
             JqGridData<ConsMixprop> data = new JqGridData<ConsMixprop>()
             {
                 page = request.PageIndex,
                 records = total,
                 pageSize = request.RecordsCount,
                 rows = list
             };
             return Json(data);
        }*/

        /// <summary>
        /// 根据生产线号获取动态列头
        /// </summary>
        /// <param name="ProductLineID"></param>
        /// <returns></returns>
        public ActionResult GetDynamicCol(string ProductLineID) {
            dynamic dynamicCol = this.service.ConsMixprop.GetDynamicColByPid(ProductLineID);
            return this.Json(dynamicCol);
        }

        public ActionResult SaveAll(string DirtyDatas) {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                object json = serializer.DeserializeObject(DirtyDatas);
                List<ConsMixprop> listCms = serializer.ConvertToType<List<ConsMixprop>>(json);
                List<Dictionary<string, string>> cmDicList = serializer.ConvertToType<List<Dictionary<string, string>>>(json);
                IList<string> allErrorList = new List<string>();
                Dictionary<string, string> failedConsMixID = new Dictionary<string, string>();
                foreach (Dictionary<string, string> cmDic in cmDicList)
                {
                    string id = cmDic["id"];
                    foreach (ConsMixprop cm in listCms)
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
                            IList<string> returnError = this.service.ConsMixprop.UpdatePrimaryAndItems(cm, updateKeys);
                            if (returnError.Count != 0) { //说明该条配比修改失败，记录编号以便标记
                                failedConsMixID.Add(cm.ID, cm.ID);
                            }
                            //更新容重
                            SqlServerHelper helper = new SqlServerHelper();
                            int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rz", new System.Data.SqlClient.SqlParameter("@FormulaID", cm.ID));
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
        /// <summary>
        /// 修改状态为修改中
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult ChangeState(string id, int Status)
        {
            try
            {
                ConsMixprop entity = this.service.ConsMixprop.Get(id);
                entity.SynStatus = Status;
                SysConfig config = this.service.SysConfig.GetSysConfig("IsAutoAuditForConsMixprop");
                if (config != null && config.ConfigValue == "false")//不自动
                {
                    entity.AuditStatus = 0;
                }
                this.service.ConsMixprop.Update(entity);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult CopyModify(ConsMixprop consMixprop, string[] dirtyDataArr, DateTime copyBeginDateTime, DateTime copyEndDateTime)
        {
            try
            {
                //1. 先保存当前修改的施工配比
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == consMixprop.TaskID && (p.BetonFormula == consMixprop.ID || p.SlurryFormula == consMixprop.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("当前正在生产的任务单不允许修改配比");
                    }
                }

                //先保存子表数据,只用到编号与数量
                this.service.ConsMixprop.SendModifiedPBToKZ(consMixprop, dirtyDataArr);
                //更新容重
                SqlServerHelper helper = new SqlServerHelper();
                int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rz", new System.Data.SqlClient.SqlParameter("@FormulaID", consMixprop.ID));

                //2. 查找时间段内，同生产线，同理论配比的施工配比进行复制修改
                ConsMixprop dbConsMixprop = this.service.ConsMixprop.Get(consMixprop.ID); //因为页面回传经模型绑定的conxMixprop 只包含ID数据，其他数据需要重新查询获取
                List<ConsMixprop> consMixpropList = this.service.ConsMixprop.Query().Where(cm => cm.ID != consMixprop.ID
                                                                                                 && cm.ProductLineID == dbConsMixprop.ProductLineID
                                                                                                 && cm.FormulaID == dbConsMixprop.FormulaID
                                                                                                 && cm.BuildTime >= copyBeginDateTime
                                                                                                 && cm.BuildTime <= copyEndDateTime).ToList();

                //3. 解析DirtyDataArr，查找修改的筒仓和用量
                Dictionary<string, string> changedSiloAmoutArray = new Dictionary<string, string>();
                foreach (var dirtyDataString in dirtyDataArr)
                {
                    string[] dirty = dirtyDataString.Split(',');
                    int consMixporpItemID = Convert.ToInt32(dirty[0]);
                    string amount = dirty[1];

                    string targetSiloID = dbConsMixprop.ConsMixpropItems.Single(item => item.ID == consMixporpItemID).SiloID;

                    changedSiloAmoutArray.Add(targetSiloID, amount);
                }


                //3. 将修改复制到其他施工配比
                foreach (var con in consMixpropList)
                {
                    //先保存一次原始的同步状态， 在复制完配比用量后，
                    //再次更新施工配比的同步状态， 这样就可以在synmonitor表中插入数据
                    int? conOriginalSynStatus = con.SynStatus;

                    //4. 构造当前施工配比的 DirtyDataArray
                    var conDirtyDataArray = changedSiloAmoutArray.Select(siloAmount => con.ConsMixpropItems.Single(item => item.SiloID == siloAmount.Key).ID.ToString()
                                                                                       + "," + siloAmount.Value)
                                                                 .ToArray<string>();

                    //先保存子表数据,只用到编号与数量
                    this.service.ConsMixprop.SendModifiedPBToKZ(con, conDirtyDataArray);
                    //更新容重
                    int effectedRows = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rz", new System.Data.SqlClient.SqlParameter("@FormulaID", con.ID));
                    //再次更新施工配比的同步状态， 这样就可以在synmonitor表中插入数据
                    con.SynStatus = conOriginalSynStatus;
                    this.service.ConsMixprop.Update(con);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }
    }
}
