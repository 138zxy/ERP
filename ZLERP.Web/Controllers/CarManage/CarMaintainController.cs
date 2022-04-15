using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.CarManage;
using ZLERP.Model;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Model.SupplyChain;
namespace ZLERP.Web.Controllers.CarManage
{
    public class CarMaintainController : BaseController<CarMaintain, string>
    {
        public override ActionResult Index()
        {
            List<SelectListItem> RepairWays = new List<SelectListItem>();
            RepairWays.Add(new SelectListItem { Text = "厂内", Value = "厂内" });
            RepairWays.Add(new SelectListItem { Text = "厂外", Value = "厂外" });
            ViewBag.RepairWays = RepairWays;

            ViewBag.Users = new SupplyChainHelp().GetUser();


            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");

            var CarDealingsList = this.service.GetGenericService<CarDealings>().Query().Where(t => !t.IsOff).ToList();
            ViewBag.CarDealingsList = new SelectList(CarDealingsList, "ID", "Name");


            var ItemList = this.service.GetGenericService<CarMaintainItem>().All().ToList();
            ViewBag.ItemList = new SelectList(ItemList, "ID", "ItemName");

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
            return string.Format("{0}{1}", "A", orderNo);
        }
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_Fixed(q, textField, valueField, orderBy, ascending, condition);
        }

        public override ActionResult Add(CarMaintain CarMaintain)
        {
            var re = base.Add(CarMaintain); 
            return re;
        }

        public override ActionResult Update(CarMaintain CarMaintain)
        {

            var re = base.Update(CarMaintain); 
            return re;
        }
        public override ActionResult Delete(string[] id)
        {
            var re = base.Delete(id);
            foreach (var mainid in id)
            {
                var Dels = this.service.GetGenericService<CarMaintainDel>().Query().Where(t => t.MaintainID == mainid);
                foreach (var del in Dels)
                {
                    this.service.GetGenericService<CarMaintainDel>().Delete(del);
                }
            }
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
                    PiaoOut.CarMaintainID = id;
                    var user = this.service.GetGenericService<User>().Query().FirstOrDefault(t => t.TrueName == Maintain.GiveMan);
                    if (user != null)
                    {
                        PiaoOut.DepartmentID = user.DepartmentID;
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
                    PiaoOut.CarMaintainID = id; 
                    var user = this.service.GetGenericService<User>().Query().FirstOrDefault(t => t.TrueName == Maintain.GiveMan);
                    if (user != null)
                    {
                        PiaoOut.DepartmentID = user.DepartmentID;
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
