using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Linq.Expressions;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_GoodsReportController : BaseController<V_SC_GoodsReport, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            int total;
            IList<V_SC_GoodsReport> list = m_ServiceBase.Find(request, condition, out total);
            int goodsID = 0;
            int goodNum = 0;
            decimal goodMoney = 0;
            foreach (var p in list)
            {
                if (goodsID == p.GoodsID)
                {
                    goodNum = goodNum + Convert.ToInt32(p.InQuantity) - Convert.ToInt32(p.OutQuantity);
                    goodMoney = goodMoney + Convert.ToDecimal(p.InMoney) - Convert.ToDecimal(p.OutMoney);
                    p.RemainderQuantity = goodNum;
                    p.RemainderMoney = goodMoney;
                }
                else
                {
                    goodsID = p.GoodsID;
                    goodNum = Convert.ToInt32(p.InQuantity) - Convert.ToInt32(p.OutQuantity);  
                    goodMoney = Convert.ToDecimal(p.InMoney) - Convert.ToDecimal(p.OutMoney);
                    p.RemainderQuantity = goodNum;
                    p.RemainderMoney = goodMoney;
                }
            }
                 
            JqGridData<V_SC_GoodsReport> data = new JqGridData<V_SC_GoodsReport>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = list
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };
        }
    }
}
