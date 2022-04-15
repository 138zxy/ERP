using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.Beton;
using ZLERP.Model;
using System.Web.Script.Serialization;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers.Beton
{
    public class B_VTranYingSFrecController : BaseController<B_TranYingSFrec, string>
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
                condition += " and (PayType='预付款' or Source='预付')";
            }
            int total;
            IList<B_TranYingSFrec> list = m_ServiceBase.Find(request, condition, out total).ToList();
            List<ZLERP.Model.Material.M_Common.M_V_YingSFrec> V_YingSFrecs = new List<ZLERP.Model.Material.M_Common.M_V_YingSFrec>();

            var query = (from q in list
                         select new ZLERP.Model.Material.M_Common.M_V_YingSFrec
                        {
                            FinanceDate = q.FinanceDate,
                            FinanceNo = q.FinanceNo,
                            Source = q.Source,
                            PayType = q.PayType,
                            FinanceMoney = q.Source == "预付" ? q.FinanceMoney : 0,
                            YFinanceMoney = q.PayType == "预付款" ? q.FinanceMoney : 0,
                            PayFavourable = q.PayFavourable
                        }).ToList();

            JqGridData<ZLERP.Model.Material.M_Common.M_V_YingSFrec> data = new JqGridData<ZLERP.Model.Material.M_Common.M_V_YingSFrec>()
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
