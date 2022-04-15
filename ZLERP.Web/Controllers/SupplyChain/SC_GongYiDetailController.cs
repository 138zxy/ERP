using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_GongYiDetailController : BaseController<SC_GongYiDetail, string>
    {

        private object obj = new object();
        public override ActionResult Index()
        {
 
            return base.Index();
        }

        public override ActionResult Add(SC_GongYiDetail SC_GongYiDetail)
        {
            SC_GongYiDetail.SC_Goods = null; 
            var del = this.m_ServiceBase.Query().Where(t => t.GongYiID == SC_GongYiDetail.GongYiID && t.GoodsID == SC_GongYiDetail.GoodsID).FirstOrDefault();
            if (del != null)
            {
                return OperateResult(false, "加工前后的商品中已经存在相同的商品了，不能重复添加", null);
            }
            return base.Add(SC_GongYiDetail);
        }

        public override ActionResult Update(SC_GongYiDetail SC_GongYiDetail)
        {
            SC_GongYiDetail.SC_Goods = null;
            var del = this.m_ServiceBase.Query().Where(t => t.ID != SC_GongYiDetail.ID && t.GongYiID == SC_GongYiDetail.GongYiID && t.GoodsID == SC_GongYiDetail.GoodsID).FirstOrDefault();
            if (del != null)
            {
                return OperateResult(false, "加工前后的商品中已经存在相同的商品了，不能重复添加", null);
            }
            return base.Update(SC_GongYiDetail);
        }
    }
}
