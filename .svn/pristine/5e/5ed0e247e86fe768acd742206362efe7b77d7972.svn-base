using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_ZhangInOrderController : BaseController<SC_ZhangInOrder, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
          
            return base.Index();
        }


        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_Goods(q, textField, valueField, orderBy, ascending, condition); 
        }

        public override ActionResult Add(SC_ZhangInOrder SC_ZhangInOrder)
        {
            if (SC_ZhangInOrder.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangInOrder.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            if (SC_ZhangInOrder.UnitPrice <= 0)
            {
                return OperateResult(false, "请输入购买单价", null);
            }
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.OrderNo == SC_ZhangInOrder.OrderNo && t.GoodsID == SC_ZhangInOrder.GoodsID && t.Unit==SC_ZhangInOrder.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            } 
            SC_ZhangInOrder.SC_Goods = null;
            var res = base.Add(SC_ZhangInOrder);
            UpdatePiaoInOrder(SC_ZhangInOrder.OrderNo.ToString());
            return res;
        }

        public override ActionResult Update(SC_ZhangInOrder SC_ZhangInOrder)
        {
            if (SC_ZhangInOrder.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangInOrder.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            if (SC_ZhangInOrder.UnitPrice <= 0)
            {
                return OperateResult(false, "请输入购买单价", null);
            }
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.OrderNo == SC_ZhangInOrder.OrderNo && t.ID != SC_ZhangInOrder.ID && t.GoodsID == SC_ZhangInOrder.GoodsID && t.Unit == SC_ZhangInOrder.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            var res = base.Update(SC_ZhangInOrder);
            UpdatePiaoInOrder(SC_ZhangInOrder.OrderNo.ToString());
            return res;
        }


        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个入库单进行删除", null);
            } 
            var res = base.Delete(id);
            UpdatePiaoInOrder(id[0].ToString());
            return res;
        }
        /// <summary>
        /// 更新主表的品种数量和订单金额
        /// </summary>
        /// <param name="id"></param>
        public void UpdatePiaoInOrder(string id)
        {

            var inOrder = this.service.GetGenericService<SC_PiaoInOrder>().Query().Where(t => t.ID == id).FirstOrDefault();
            if (inOrder != null)
            {
                int orderNo = Convert.ToInt32(id);
                var ZhangOrders = this.m_ServiceBase.Query().Where(t => t.OrderNo == orderNo).ToList();
                var varietyNum = ZhangOrders.GroupBy(t => t.GoodsID).Count();
                var orderMoney = ZhangOrders.Sum(t => t.ZhangMoney);
                inOrder.VarietyNum = varietyNum;
                inOrder.OrderMoney = orderMoney ?? 0;
                this.service.GetGenericService<SC_PiaoInOrder>().Update(inOrder,null);
            }

        }


 
    }
}
