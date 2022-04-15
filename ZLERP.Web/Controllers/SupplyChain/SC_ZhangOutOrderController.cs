using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_ZhangOutOrderController : BaseController<SC_ZhangOutOrder, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
          
            return base.Index();
        }


        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_GoodsSample(q, textField, valueField, orderBy, ascending, condition); 
        }

        public override ActionResult Add(SC_ZhangOutOrder SC_ZhangOutOrder)
        {
            if (SC_ZhangOutOrder.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangOutOrder.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.OrderNo == SC_ZhangOutOrder.OrderNo && t.GoodsID == SC_ZhangOutOrder.GoodsID && t.Unit == SC_ZhangOutOrder.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            SC_ZhangOutOrder.SC_Goods = null;
            var res = base.Add(SC_ZhangOutOrder);
            UpdatePiaoOutOrder(SC_ZhangOutOrder.OrderNo.ToString());
            return res;
        }

        public override ActionResult Update(SC_ZhangOutOrder SC_ZhangOutOrder)
        {
            if (SC_ZhangOutOrder.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangOutOrder.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
           
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.OrderNo == SC_ZhangOutOrder.OrderNo && t.ID != SC_ZhangOutOrder.ID && t.GoodsID == SC_ZhangOutOrder.GoodsID && t.Unit == SC_ZhangOutOrder.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            var res = base.Update(SC_ZhangOutOrder);
            UpdatePiaoOutOrder(SC_ZhangOutOrder.OrderNo.ToString());
            return res;
        }


        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个入库单进行删除", null);
            } 
            var res = base.Delete(id);
            UpdatePiaoOutOrder(id[0].ToString());
            return res;
        }
        /// <summary>
        /// 更新主表的品种数量和订单金额
        /// </summary>
        /// <param name="id"></param>
        public void UpdatePiaoOutOrder(string id)
        {
            var OutOrder = this.service.GetGenericService<SC_PiaoOutOrder>().Query().Where(t => t.ID == id).FirstOrDefault();
            if (OutOrder != null)
            {
                int orderNo = Convert.ToInt32(id);
                var ZhangOrders = this.m_ServiceBase.Query().Where(t => t.OrderNo == orderNo).ToList();
                var varietyNum = ZhangOrders.GroupBy(t => t.GoodsID).Count();
                OutOrder.VarietyNum = varietyNum;
                this.service.GetGenericService<SC_PiaoOutOrder>().Update(OutOrder, null);
            } 
        }


 
    }
}
