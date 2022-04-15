using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_ZhangOutController : BaseController<SC_ZhangOut, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(SC_ZhangOut SC_ZhangOut)
        {
            if (SC_ZhangOut.GoodsID <= 0)
            {
                return OperateResult(false, "请选择商品", null);
            }
            if (SC_ZhangOut.Quantity <= 0)
            {
                return OperateResult(false, "请输入申领数量", null);
            } 
          
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.OutNo == SC_ZhangOut.OutNo && t.GoodsID == SC_ZhangOut.GoodsID && t.Unit == SC_ZhangOut.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该供应商商品，请不要重复添加", null);
            }

            //如果是剩料退还，则数量需要是负数
            var piaoOut=this.service.GetGenericService<SC_PiaoOut>().Get(SC_ZhangOut.OutNo.ToString());
            if (piaoOut.OutType == SC_Common.OutType.MaintainBack)
            {
                if (SC_ZhangOut.Quantity > 0)
                {
                    SC_ZhangOut.Quantity = -SC_ZhangOut.Quantity;
                }
            } 
            SC_ZhangOut.OutMoney = SC_ZhangOut.Quantity * SC_ZhangOut.PriceUnit;
            SC_ZhangOut.SC_Goods = null;  
            var res = base.Add(SC_ZhangOut);
            UpdateZhangIn(SC_ZhangOut.OutNo.ToString());
            return res;
        }

        public override ActionResult Update(SC_ZhangOut SC_ZhangOut)
        {
            if (SC_ZhangOut.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangOut.Quantity <= 0)
            {
                return OperateResult(false, "请输入申领数量", null);
            } 
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.OutNo == SC_ZhangOut.OutNo && t.ID != SC_ZhangOut.ID && t.GoodsID == SC_ZhangOut.GoodsID && t.Unit == SC_ZhangOut.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            //如果是剩料退还，则数量需要是负数
            var piaoOut = this.service.GetGenericService<SC_PiaoOut>().Get(SC_ZhangOut.OutNo.ToString());
            if (piaoOut.OutType == SC_Common.OutType.MaintainBack)
            {
                if (SC_ZhangOut.Quantity > 0)
                {
                    SC_ZhangOut.Quantity = -SC_ZhangOut.Quantity;
                }
            } 
            SC_ZhangOut.OutMoney = SC_ZhangOut.Quantity * SC_ZhangOut.PriceUnit;
            var res = base.Update(SC_ZhangOut);
            UpdateZhangIn(SC_ZhangOut.OutNo.ToString());
            return res;
        }

        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个申领单进行删除", null);
            }
            var res = base.Delete(id);
            UpdateZhangIn(id[0].ToString());
            return res;
        }
        /// <summary>
        /// 更新主表的品种数量和订单金额
        /// </summary>
        /// <param name="id"></param>
        public void UpdateZhangIn(string id)
        {

            var inOrder = this.service.GetGenericService<SC_PiaoOut>().Query().Where(t => t.ID == id).FirstOrDefault();
            if (inOrder != null)
            {
                int orderNo = Convert.ToInt32(id);
                var ZhangOrders = this.m_ServiceBase.Query().Where(t => t.OutNo == orderNo).ToList();
                var varietyNum = ZhangOrders.GroupBy(t => t.GoodsID).Count();
                var orderMoney = ZhangOrders.Sum(t => t.OutMoney);
                inOrder.VarietyNum = varietyNum;
                inOrder.OutMoney = orderMoney;
                this.service.GetGenericService<SC_PiaoOut>().Update(inOrder, null);
            }
        }
    }
}
