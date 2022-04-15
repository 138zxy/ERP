using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_ShiftController : BaseController<SC_Fixed_Shift, string>
    {
        public override ActionResult Index()
        {
            ViewBag.Departments = new SupplyChainHelp().GetDepartment();
            return base.Index();
        }

        /// <summary>
        /// 生产订单号
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateNo("FS");
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public override ActionResult Add(SC_Fixed_Shift SC_Fixed_Shift)
        {
            try
            {
                var scFixed = this.service.GetGenericService<SC_Fixed>().Get(SC_Fixed_Shift.FixedID.ToString());
                scFixed.Position = SC_Fixed_Shift.PositionNew;
                scFixed.DepartMent = SC_Fixed_Shift.DepartMentNew;
                scFixed.Ftype = SC_Fixed_Shift.FtypeNew;
                scFixed.Storeman = SC_Fixed_Shift.StoremanNew;
                this.service.GetGenericService<SC_Fixed>().Update(scFixed,null);

                base.Add(SC_Fixed_Shift);
                return OperateResult(true, "新增成功", null);
            }
            catch (Exception ex)
            {

                return OperateResult(false, ex.Message.ToString(), null);
            }
        }

        public override ActionResult Update(SC_Fixed_Shift SC_Fixed_Shift)
        {
            try
            {
                var scFixed = this.service.GetGenericService<SC_Fixed>().Get(SC_Fixed_Shift.FixedID.ToString());
                scFixed.Position = SC_Fixed_Shift.PositionNew;
                scFixed.DepartMent = SC_Fixed_Shift.DepartMentNew;
                scFixed.Ftype = SC_Fixed_Shift.FtypeNew;
                scFixed.Storeman = SC_Fixed_Shift.StoremanNew;
                this.service.GetGenericService<SC_Fixed>().Update(scFixed, null);

                base.Update(SC_Fixed_Shift);
                return OperateResult(true, "修改成功", null);
            }
            catch (Exception ex)
            {

                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
    }
}
