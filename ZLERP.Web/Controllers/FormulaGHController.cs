using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.Model.ViewModels;
using ZLERP.Web.Helpers;


namespace ZLERP.Web.Controllers
{
    public class FormulaGHController : BaseController<FormulaGH, string>
    {
        public override ActionResult Index()
        {
            ViewBag.ConStrength = Helpers.HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode not like 'C%'", "ConStrengthCode", true, null);
            ViewBag.StuffType = HelperExtensions.SelectListData<StuffType>("StuffTypeName", "ID", " IsUsed=1 AND StuffTypeName like '%干混%' AND TypeID='StuffType'", "OrderNum", true, "");
            return base.Index();
        }
        public ActionResult DesignFormulaGH()
        {
            ViewBag.ConStrength = Helpers.HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);

            base.InitCommonViewBag();
            return View();
        }


        public override ActionResult Update(FormulaGH FormulaGH)
        {
            if (string.IsNullOrEmpty(FormulaGH.ConStrength))
            {
                return OperateResult(false, "强度不可为空。", null);
            }
            return base.Update(FormulaGH);
        }

        public override ActionResult Add(FormulaGH FormulaGH)
        {
            if (string.IsNullOrEmpty(FormulaGH.ConStrength))
            {
                return OperateResult(false, "强度不可为空。", null);
            }
            if (FormulaGH.ID != null)
            {
                return this.Update(FormulaGH);
            }
            else
            {
                return base.Add(FormulaGH);
            }
        }

        public ActionResult CopyInfo()
        {
            InitCommonViewBag();
            return View();
        }

        public ActionResult GetSourceList(string op)
        {
            dynamic list = this.service.GetGenericService<FormulaGH>().All();
            switch (op)
            {
                case "FO":
                    list = this.service.GetGenericService<FormulaGH>().All();
                    break;
                case "CO":
                    list = this.service.GetGenericService<ConsMixprop>().All();
                    break;
                case "CU":
                    list = this.service.GetGenericService<CustMixprop>().All();
                    break;
            }

            return this.Json(list);
        }

        public ActionResult StartCopy(string op, string sid, string did)
        {
            ///Todo
            ///通过源表、目标表、源ID进行自动导入
            try
            {
                FormulaGHService service = this.service.FormulaGH;
                bool result = service.StartCopy(op, sid, did);
                if (result)
                {
                    return OperateResult(result, Lang.Msg_Operate_Success, result);
                }
                else
                {
                    return OperateResult(result, Lang.Msg_Operate_Failed, result);
                }
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
        /// 导入材料类型，自动生成参考配比
        /// </summary>
        /// <param name="FormulaGHid"></param>
        /// <returns></returns>
        public ActionResult ExportStuff(string formulaid)
        {
            FormulaGHService service = this.service.FormulaGH;
            bool result = service.ExportStuff(formulaid);
            if (result)
            {
                return OperateResult(result, Lang.Msg_Operate_Success, result);
            }
            else
            {
                return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
        }

        //public ActionResult SaveDeFormulaGH(FormCollection forms) {
        //    var ids = forms["FormulaGHName"];
        //    var ere = forms["FormulaGHType"];
        //    return OperateResult(true, Lang.Msg_Operate_Success, true);
        //}

        /// <summary>
        /// 配合比设计后保存为理论配比
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveDeFormulaGH(FormulaDesignModel data)
        {
            try
            {
                FormulaGH temp = this.m_ServiceBase.Add(data.FormulaGH);

                //保存水泥用量
                FormulaGHItem ceitem = new FormulaGHItem();
                ceitem.FormulaGHID = temp.ID;
                ceitem.StandardAmount = data.CEAmount_S == null ? 0 : (decimal)data.CEAmount_S;
                ceitem.StuffAmount = data.CEAmount_R == null ? 0 : (decimal)data.CEAmount_R;
                ceitem.StuffTypeID = "CE";
                this.service.FormulaGHItem.Add(ceitem);
                //this.service.GetGenericService<FormulaGHItem>().Add(ceitem);

                //保存饮用水用量
                FormulaGHItem waitem = new FormulaGHItem();
                waitem.FormulaGHID = temp.ID;
                waitem.StandardAmount = data.WaAmount_S == null ? 0 : (decimal)data.WaAmount_S;
                waitem.StuffAmount = data.WaAmount_R == null ? 0 : (decimal)data.WaAmount_R;
                waitem.StuffTypeID = "WA";
                this.service.FormulaGHItem.Add(waitem);

                //保存粗骨料用量
                FormulaGHItem caitem = new FormulaGHItem();
                caitem.FormulaGHID = temp.ID;
                caitem.StandardAmount = data.CaAmount_S == null ? 0 : (decimal)data.CaAmount_S;
                caitem.StuffAmount = data.CaAmount_R == null ? 0 : (decimal)data.CaAmount_R;
                caitem.StuffTypeID = "CA";
                this.service.FormulaGHItem.Add(caitem);

                //保存细骨料用量
                FormulaGHItem faitem = new FormulaGHItem();
                faitem.FormulaGHID = temp.ID;
                faitem.StandardAmount = data.FaAmount_S == null ? 0 : (decimal)data.FaAmount_S;
                faitem.StuffAmount = data.FaAmount_R == null ? 0 : (decimal)data.FaAmount_R;
                faitem.StuffTypeID = "FA";
                this.service.FormulaGHItem.Add(faitem);


                //保存掺合料一用量
                FormulaGHItem air1item = new FormulaGHItem();
                air1item.FormulaGHID = temp.ID;
                air1item.StandardAmount = data.AIR1Amount_S == null ? 0 : (decimal)data.AIR1Amount_S;
                air1item.StuffAmount = data.AIR1Amount_R == null ? 0 : (decimal)data.AIR1Amount_R;
                air1item.StuffTypeID = "AIR1";
                this.service.FormulaGHItem.Add(air1item);

                //保存掺合料二用量
                FormulaGHItem air2item = new FormulaGHItem();
                air2item.FormulaGHID = temp.ID;
                air2item.StandardAmount = data.AIR2Amount_S == null ? 0 : (decimal)data.AIR2Amount_S;
                air2item.StuffAmount = data.AIR2Amount_R == null ? 0 : (decimal)data.AIR2Amount_R;
                air2item.StuffTypeID = "AIR2";
                this.service.FormulaGHItem.Add(air2item);

                //保存外加剂一用量
                FormulaGHItem adm1item = new FormulaGHItem();
                adm1item.FormulaGHID = temp.ID;
                adm1item.StandardAmount = data.ADM1Amount_S == null ? 0 : (decimal)data.ADM1Amount_S;
                adm1item.StuffAmount = data.ADM1Amount_R == null ? 0 : (decimal)data.ADM1Amount_R;
                adm1item.StuffTypeID = "ADM1";
                this.service.FormulaGHItem.Add(adm1item);

                //保存外加剂二用量
                FormulaGHItem adm2item = new FormulaGHItem();
                adm2item.FormulaGHID = temp.ID;
                adm2item.StandardAmount = data.ADM2Amount_S == null ? 0 : (decimal)data.ADM2Amount_S;
                adm2item.StuffAmount = data.ADM2Amount_R == null ? 0 : (decimal)data.ADM2Amount_R;
                adm2item.StuffTypeID = "ADM2";
                this.service.FormulaGHItem.Add(adm2item);


                if (temp != null)
                {
                    return OperateResult(true, Lang.Msg_Operate_Success, true);
                }
                else
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, false);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, false);
            }
        }

        public ActionResult FindFormulaGHInfo(string FormulaGHID)
        {
            IList<FormulaGHItem> FormulaGHlist = this.service.FormulaGHItem.Query().Where(m => m.FormulaGHID == FormulaGHID).ToList();
            //出厂资料是否与实际数据一致
            bool result = this.service.SysConfig.GetSysConfig("LabDataModel") == null ? true : Convert.ToBoolean(this.service.SysConfig.GetSysConfig("LabDataModel").ConfigValue);
            return OperateResult(result, Lang.Msg_Operate_Success, FormulaGHlist);
        }

    }
}
