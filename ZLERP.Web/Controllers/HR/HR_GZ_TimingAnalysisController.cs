using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Linq.Expressions;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_TimingAnalysisController : BaseController<HR_GZ_Timing, string>
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
            var GZ_Piece = this.service.GetGenericService<HR_GZ_Timing>().Query().Where(t => t.WorkDate >= begindate && t.WorkDate <= enddate)
                  .GroupBy(t => t.SetID).Select(t => new
                {
                    SetID = t.Key,
                    Quantity = t.Sum(q => q.Quantity),
                    AllMoney = t.Sum(s => s.AllMoney)
                }).ToList();
            var GZ_PieceSet = this.service.GetGenericService<HR_GZ_TimingSet>().All();

            var allMoney = GZ_Piece.Sum(t => t.AllMoney);

            var query = (from q in GZ_Piece
                         join z in GZ_PieceSet on q.SetID.ToString() equals z.ID into g
                         from h in g.DefaultIfEmpty()
                         select new HR_GZ_PieceAnalysis
                         {
                             Code = h.Code,
                             Name = h.Name,
                             Quantity = q.Quantity,
                             AllMoney = q.AllMoney ,
                             Proportion = decimal.Round(q.AllMoney / allMoney * 100,2)
                         }).ToList();

            total = GZ_Piece.Count;
            JqGridData<HR_GZ_PieceAnalysis> data = new JqGridData<HR_GZ_PieceAnalysis>()
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
