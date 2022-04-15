using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Model;
using System.Web.Script.Serialization;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_NowLibController : BaseController<SC_NowLib, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            return base.Index();
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            int total;
            IList<SC_NowLib> list = this.m_ServiceBase.Find(request, condition, out total).ToList();

            //查询每个商品的总库存
            var ids = list.Select(t => t.ID).ToList();

            var query = this.m_ServiceBase.Query().Where(t => ids.Contains(t.ID)).GroupBy(t => t.GoodsID).Select(t =>
               new
               {
                   GoodsID = t.Key,
                   Quantity = t.Sum(g => g.Quantity)
               }).ToList();
            foreach (var i in list)
            {
                var num = query.Where(t => t.GoodsID == i.GoodsID).First().Quantity;
                if (num < i.SC_Goods.MinWarning && i.SC_Goods.MinWarning > 0)
                {
                    i.Warning = "低于预警值";
                }
                if (num > i.SC_Goods.MaxWarning && i.SC_Goods.MaxWarning > 0)
                {
                    i.UpWarning = "高于预警值";
                }
            }

            JqGridData<SC_NowLib> data = new JqGridData<SC_NowLib>()
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
            // return base.Find(request, condition);
        }


        public ActionResult GetLibGoods(int lib, int goodsID)
        {
            var Nowlib = this.m_ServiceBase.Query().Where(t => t.LibID == lib && t.GoodsID == goodsID).FirstOrDefault();
            return OperateResult(true, "", Nowlib);
        }
    }
}
