using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_Fixed_DepreciaController : BaseController<SC_Fixed_Deprecia, string>
    {
        public override ActionResult Index()
        {
            string month = DateTime.Now.ToString("yyyy-MM");
            SqlServerHelper help = new SqlServerHelper();
            help.ExecuteDataset("SP_SC_DepreciaReport", System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("@month", month));
            return base.Index();
        }

        public ActionResult Search(string month)
        {
            try
            {
                DateTime dt = DateTime.Now;
                if (!DateTime.TryParse(month + "-01", out dt))
                {
                    if (DateTime.TryParse(month, out dt))
                    {
                        month = dt.ToString("yyyy-MM");
                    }
                    else
                    {
                        return OperateResult(false, "输入的时间格式有误", null);
                    }
                }
                SqlServerHelper help = new SqlServerHelper();
                help.ExecuteDataset("SP_SC_DepreciaReport", System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("@month", month));
                return OperateResult(true, "分析完成", null);

            }
            catch (Exception ex)
            {

                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
    }
}
