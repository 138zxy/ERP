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
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_JxcReportController : BaseController<SC_Base, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            return base.Index();
        }
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_Goods(q, textField, valueField, orderBy, ascending, condition);
        }
        /// <summary>
        /// 月度汇总查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Find(JqGridRequest request, string condition)
        {
            string[] con = condition.Split('&');
            DateTime date1;
            if (string.IsNullOrWhiteSpace(con[1]) || !DateTime.TryParse(con[1] + "-01", out date1))
            {
                return OperateResult(false, "开始月份请输入正确", null);
            }
            if (string.IsNullOrWhiteSpace(con[2]) || !DateTime.TryParse(con[2] + "-01", out date1))
            {
                return OperateResult(false, "结束月份请输入正确", null);
            } 
            SqlServerHelper help = new SqlServerHelper();

            var ds = help.ExecuteDataset("SP_SC_jxcMonthReport", System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("@typeNo", con[0]), new System.Data.SqlClient.SqlParameter("@startMonth", con[1]), new System.Data.SqlClient.SqlParameter("@endMonth", con[2]));

            var Monthdata = ModelConvertHelper<V_SC_jxcMonthReport>.ConvertToModel(ds.Tables[0]);

            JqGridData<V_SC_jxcMonthReport> data = new JqGridData<V_SC_jxcMonthReport>()
            {
                page = 1,
                records = 100000,
                pageSize = request.RecordsCount,
                rows = Monthdata
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };

        }
        /// <summary>
        /// 商品明细查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Find2(JqGridRequest request, string condition)
        {
            string[] con = condition.Split('&');
            DateTime date1;
            if (string.IsNullOrWhiteSpace(con[1]) || !DateTime.TryParse(con[1], out date1))
            {
                return OperateResult(false, "开始时间请输入正确", null);
            }
            if (string.IsNullOrWhiteSpace(con[2]) || !DateTime.TryParse(con[2], out date1))
            {
                return OperateResult(false, "结束时间请输入正确", null);
            }
            SqlServerHelper help = new SqlServerHelper();

            var ds = help.ExecuteDataset("SP_SC_jxcDetailReport", System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("@typeNo", con[0]), new System.Data.SqlClient.SqlParameter("@startDate", con[1]), new System.Data.SqlClient.SqlParameter("@enddate", con[2]), new System.Data.SqlClient.SqlParameter("@goodsName", con[3]), new System.Data.SqlClient.SqlParameter("@lib", con[4]));

            var jxcDetailReportdata = ModelConvertHelper<V_SC_jxcDetailReport>.ConvertToModel(ds.Tables[0]);

            JqGridData<V_SC_jxcDetailReport> data = new JqGridData<V_SC_jxcDetailReport>()
            {
                page = 1,
                records = 100000,
                pageSize = request.RecordsCount,
                rows = jxcDetailReportdata
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };

        }
        /// <summary>
        /// 单品查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Find3(JqGridRequest request, string condition)
        {
            string[] con = condition.Split('&'); 
            if (string.IsNullOrWhiteSpace(con[0]))
            {
                return OperateResult(false, "请选择一个商品进行查询", null);
            } 
            SqlServerHelper help = new SqlServerHelper();

            var ds = help.ExecuteDataset("SP_SC_jxcGoodsReport", System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("@GoodsID", con[0]), new System.Data.SqlClient.SqlParameter("@LibID", con[1]));

            var jxcGoodsReportdata = ModelConvertHelper<V_SC_jxcGoodsReport>.ConvertToModel(ds.Tables[0]);
           //对结余库存进行处理 
            decimal LibQuantity = 0.00m;
            foreach (var p in jxcGoodsReportdata)
            {
                LibQuantity += p.OrderNum;
                p.LibQuantity = LibQuantity;
            } 
            JqGridData<V_SC_jxcGoodsReport> data = new JqGridData<V_SC_jxcGoodsReport>()
            {
                page = 1,
                records = 100000,
                pageSize = request.RecordsCount,
                rows = jxcGoodsReportdata
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };

        }
    }
}
