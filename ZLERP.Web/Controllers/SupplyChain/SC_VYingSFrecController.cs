using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;
using System.Web.Script.Serialization;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_VYingSFrecController : BaseController<SC_YingSFrec, string>
    {
        /// <summary>
        /// 重写Find
        /// </summary>
        /// <param name="request">分页</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public override ActionResult Find(JqGridRequest request, string condition)
        {
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition += " and (PayType='供应商预付款' or Source='预付')";
            }
            int total;
            IList<SC_YingSFrec> list = m_ServiceBase.Find(request, condition, out total).ToList();
            List<V_YingSFrec> V_YingSFrecs = new List<V_YingSFrec>();

            var query = (from q in list
                        select new V_YingSFrec
                        {
                            FinanceDate = q.FinanceDate,
                            FinanceNo = q.FinanceNo,
                            Source = q.Source,
                            PayType = q.PayType,
                            FinanceMoney = q.Source == "预付" ? q.FinanceMoney : 0,
                            YFinanceMoney = q.PayType == "供应商预付款" ? q.FinanceMoney : 0,
                            PayFavourable = q.PayFavourable
                        }).ToList();

            JqGridData<V_YingSFrec> data = new JqGridData<V_YingSFrec>()
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
