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
    public class SC_PurReportController : BaseController<SC_Goods, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        /// <summary>
        /// 采购分析统计报表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Find(JqGridRequest request, string condition)
        {
            int total;
            //对condition 进行解析
            string[] con = condition.Split('&');

            //查询出当前商品分类的所有商品
            IList<SC_Goods> list = m_ServiceBase.Find(request, con[0], out total).ToList();
            //查询出查询时间范围内的的商品的采购数量和采购金额

            List<int> goodsids = list.Select(t => Convert.ToInt32(t.ID)).ToList();
            if (goodsids.Count <= 0)
            {
                return new ContentResult
                {
                    Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(new List<PurReport>()),
                    ContentType = "application/json"
                };
            }
            //由于hql无法使用linq的动态拼接条件语法，故采用首尾默认制
            DateTime begindate = DateTime.Now.AddYears(-20);
            if (!string.IsNullOrWhiteSpace(con[1]))
            {
                DateTime.TryParse(con[1], out begindate);
            }
            DateTime enddate = DateTime.Now.AddDays(1);
            if (!string.IsNullOrWhiteSpace(con[2]))
            {
                if (DateTime.TryParse(con[2], out enddate))
                {
                    enddate = enddate.AddDays(1);
                }
            }
            var ZhangIn = this.service.GetGenericService<SC_ZhangIn>().Query().Where(t => goodsids.Contains(t.GoodsID) && t.Indate >= begindate && t.Indate < enddate)
                  .GroupBy(t => t.GoodsID).Select(t => new
                {
                    GoodsID = t.Key,
                    PurNum = t.Sum(q => q.Quantity * q.UnitRate),
                    PurMoney = t.Sum(s => s.InMoney)
                }).ToList();


            var query = (from q in list
                         join z in ZhangIn on q.ID equals z.GoodsID.ToString() into g
                         from h in g.DefaultIfEmpty()
                         select new PurReport
                         {
                             GoodsName = q.GoodsName,
                             Spec = q.Spec,
                             Unit = q.Unit,
                             PurNum = h == null ? 0.00m : h.PurNum,
                             PurMoney = h == null ? 0 : h.PurMoney
                         }).ToList();
            JqGridData<PurReport> data = new JqGridData<PurReport>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = query
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };
        }
    }
}
