using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PanDianDetailController : BaseController<SC_PanDianDetail, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Update(SC_PanDianDetail SC_PanDianDetail)
        {
            var detail = this.m_ServiceBase.Get(SC_PanDianDetail.ID);
            var PanID = detail.PanID;
            var SC_PanDian = this.service.GetGenericService<SC_PanDian>().Get(PanID.ToString());
            if (SC_PanDian.IsOff)
            {
                return OperateResult(false, "当前盘点单已经盘点完成", "");
            }
            if (SC_PanDianDetail.PanNum >= 0)
            {
                detail.PanNum = SC_PanDianDetail.PanNum;

                detail.DifferenceNum = detail.PanNum - detail.LibNum;
            }
            if (!string.IsNullOrEmpty(SC_PanDianDetail.Remark))
            {
                detail.Remark = SC_PanDianDetail.Remark;
            }
            return base.Update(detail);
        }
    }
}
