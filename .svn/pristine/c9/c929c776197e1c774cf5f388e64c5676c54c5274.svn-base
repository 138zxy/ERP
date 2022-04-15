using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;
namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_MaintainController : BaseController<SC_Fixed_Maintain, string>
    {
        public override ActionResult Index()
        {
            List<SelectListItem> RepairTypes = new List<SelectListItem>();
            RepairTypes.Add(new SelectListItem { Text = "维修", Value = "维修" });
            RepairTypes.Add(new SelectListItem { Text = "保养", Value = "保养" });
            ViewBag.RepairTypes = RepairTypes;

            List<SelectListItem> RepairWays = new List<SelectListItem>();
            RepairWays.Add(new SelectListItem { Text = "厂内", Value = "厂内" });
            RepairWays.Add(new SelectListItem { Text = "厂外", Value = "厂外" });
            ViewBag.RepairWays = RepairWays;

            ViewBag.Users = new SupplyChainHelp().GetUser();
            return base.Index();
        }
        public ActionResult GenerateOrderNo()
        {
            string orderNo = GetGenerateOrderNo();
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public string GetGenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateOrderNo();
            return string.Format("{0}{1}", "C", orderNo);
        }
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_Fixed(q, textField, valueField, orderBy, ascending, condition);
        }

        public override ActionResult Add(SC_Fixed_Maintain entity)
        {
            var re = base.Add(entity);
            this.service.SupplyChainService.UpdateFixedCondition(entity.FixedID);
            return re;
        }

        public override ActionResult Update(SC_Fixed_Maintain entity)
        {

            var re = base.Update(entity);

            this.service.SupplyChainService.UpdateFixedCondition(entity.FixedID);

            return re;
        }
        public override ActionResult Delete(string[] id)
        {
            var fix = this.m_ServiceBase.Get(id[0]);
            var re = base.Delete(id);
            this.service.SupplyChainService.UpdateFixedCondition(fix.FixedID);
            return re;
        }

        public ActionResult Out(int id)
        {
            if (id <= 0)
            {
                return OperateResult(false, "维修单有误", null);
            }
            try
            {
                var Maintain = this.m_ServiceBase.Get(id.ToString());
                if (Maintain != null)
                {
                    //2.生成出库库单
                    SC_PiaoOut PiaoOut = new SC_PiaoOut();

                    var lib = this.service.GetGenericService<SC_Lib>().Query().FirstOrDefault(t => !t.IsOff);
                    PiaoOut.LibID = Convert.ToInt32(lib.ID);
                    PiaoOut.OutType = SC_Common.OutType.MaintainOut;
                    PiaoOut.OutDate = DateTime.Now;
                    string OutNo = new SC_PiaoOutController().GetGenerateOrderNo();
                    PiaoOut.OutNo = OutNo;
                    PiaoOut.MaintainID = id;

                    var department = this.service.GetGenericService<Department>().Query().FirstOrDefault(t => t.DepartmentName == Maintain.DepartMent);
                    if (department != null)
                    {
                        PiaoOut.DepartmentID = department.ID;
                    }
                    PiaoOut.UserID = Maintain.GiveMan;
                    PiaoOut.VarietyNum = 0;
                    PiaoOut.OutMoney = 0;
                    PiaoOut.PayType = SC_Common.OutType.MaintainOut;
                    PiaoOut.Condition = SC_Common.InStatus.Ini;
                    PiaoOut.Remark = SC_Common.OutType.MaintainOut;

                    this.service.GetGenericService<SC_PiaoOut>().Add(PiaoOut);
                    return OperateResult(false, "生成出库单成功", null);
                }
                return OperateResult(false, "参数错误", null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }

        public ActionResult Back(int id)
        {
            if (id <= 0)
            {
                return OperateResult(false, "维修单有误", null);
            }
            try
            {
                var Maintain = this.m_ServiceBase.Get(id.ToString());
                if (Maintain != null)
                {
                    //2.生成出库库单
                    SC_PiaoOut PiaoOut = new SC_PiaoOut();

                    var lib = this.service.GetGenericService<SC_Lib>().Query().FirstOrDefault(t => !t.IsOff);
                    PiaoOut.LibID = Convert.ToInt32(lib.ID);
                    PiaoOut.OutType = SC_Common.OutType.MaintainBack;
                    PiaoOut.OutDate = DateTime.Now;
                    string OutNo = new SC_PiaoOutController().GetGenerateOrderNo();
                    PiaoOut.OutNo = OutNo;
                    PiaoOut.MaintainID = id;

                    var department = this.service.GetGenericService<Department>().Query().FirstOrDefault(t => t.DepartmentName == Maintain.DepartMent);
                    if (department != null)
                    {
                        PiaoOut.DepartmentID = department.ID;
                    }
                    PiaoOut.UserID = Maintain.GiveMan;
                    PiaoOut.VarietyNum = 0;
                    PiaoOut.OutMoney = 0;
                    PiaoOut.PayType = SC_Common.OutType.MaintainBack;
                    PiaoOut.Condition = SC_Common.InStatus.Ini;
                    PiaoOut.Remark = SC_Common.OutType.MaintainBack;

                    this.service.GetGenericService<SC_PiaoOut>().Add(PiaoOut);
                    return OperateResult(false, "生成出库单成功", null);
                }
                return OperateResult(false, "参数错误", null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
    }
}
