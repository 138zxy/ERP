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
    public class SC_UseReportController : BaseController<SC_Goods, string>
    {
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
            //查询出所有出库单
            var ZhangOutTemp = this.service.GetGenericService<SC_ZhangOut>().Query().Where(t =>  t.OutDate >= begindate && t.OutDate < enddate).Select(t => new
            {
                GoodsID = t.GoodsID,
                OutNo = t.OutNo,
                Quantity = t.Quantity*t.UnitRate,
                OutMoney = t.OutMoney
            }).ToList();

            //查询出申请单
            List<string> sup = new List<string>();
            var OutNos = ZhangOutTemp.Select(t => t.OutNo).Distinct().ToList();
            foreach (var s in OutNos)
            {
                sup.Add(s.ToString());
            }
            var PiaoOut = this.service.GetGenericService<SC_PiaoOut>().Query().Where(t => sup.Contains(t.ID)).ToList();
            //关联统计出每个申请单是哪个部门
            var use = (from q in PiaoOut
                       select new
                       {
                           OutNo = Convert.ToInt32(q.ID),
                           UserID = q.UserID
                       }).ToList();

            var ZhangOut = (from z in ZhangOutTemp
                            join u in use on z.OutNo equals u.OutNo
                            group z by new { z.GoodsID, u.UserID } into t
                            select new
                            {
                                GoodsID = t.Key.GoodsID,
                                UserID = t.Key.UserID,
                                Quantity = t.Sum(s => s.Quantity),
                                OutMoney = t.Sum(s => s.OutMoney)
                            }).ToList();

            var query = (from q in list
                         join z in ZhangOut on q.ID equals z.GoodsID.ToString() into g
                         from h in g.DefaultIfEmpty()
                         select new UseReport
                         {
                             GoodsName = q.GoodsName,
                             Spec = q.Spec,
                             Unit = q.Unit,
                             UserID = h == null ? "" : h.UserID,
                             OutNum = h == null ? 0.00m : h.Quantity,
                             OutMoney = h == null ? 0.00m : h.OutMoney
                         }).ToList(); 
            JqGridData<UseReport> data = new JqGridData<UseReport>()
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
