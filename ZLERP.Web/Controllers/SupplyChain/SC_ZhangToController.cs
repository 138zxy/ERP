using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_ZhangToController : BaseController<SC_ZhangTo, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(SC_ZhangTo SC_ZhangTo)
        {
            if (SC_ZhangTo.GoodsID <= 0)
            {
                return OperateResult(false, "请选择商品", null);
            }
            if (SC_ZhangTo.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            if (SC_ZhangTo.PriceUnit <= 0)
            {
                return OperateResult(false, "请输入购买单价", null);
            }
            //判断一个调拨单 是否多选了同一个库存ID进行调拨
            var query = this.m_ServiceBase.Query().Where(t => t.ChangeNo == SC_ZhangTo.ChangeNo && t.GoodsID == SC_ZhangTo.GoodsID && t.Unit == SC_ZhangTo.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该库存商品，请不要重复添加", null);
            }
            SC_ZhangTo.ChangeMoney = SC_ZhangTo.Quantity * SC_ZhangTo.PriceUnit;
            SC_ZhangTo.SC_Goods = null; 
            var res = base.Add(SC_ZhangTo);
            UpdateZhangIn(SC_ZhangTo.ChangeNo.ToString());
            return res;
        }

        public override ActionResult Update(SC_ZhangTo SC_ZhangTo)
        {
            if (SC_ZhangTo.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangTo.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            if (SC_ZhangTo.PriceUnit <= 0)
            {
                return OperateResult(false, "请输入购买单价", null);
            }
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.ChangeNo == SC_ZhangTo.ChangeMoney && t.ID != SC_ZhangTo.ID && t.GoodsID == SC_ZhangTo.GoodsID && t.Unit == SC_ZhangTo.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            SC_ZhangTo.ChangeMoney = SC_ZhangTo.Quantity * SC_ZhangTo.PriceUnit;
            var res = base.Update(SC_ZhangTo);
            UpdateZhangIn(SC_ZhangTo.ChangeNo.ToString());
            return res;
        }

        public override ActionResult Delete(string[] id)
        {
            if (id.Length > 1)
            {
                return OperateResult(false, "请选择一个入库单进行删除", null);
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

            var inOrder = this.service.GetGenericService<SC_PiaoTo>().Query().Where(t => t.ID == id).FirstOrDefault();
            if (inOrder != null)
            {
                int orderNo = Convert.ToInt32(id);
                var ZhangOrders = this.m_ServiceBase.Query().Where(t => t.ChangeNo == orderNo).ToList();
                var varietyNum = ZhangOrders.GroupBy(t=>t.GoodsID).Count();
                var orderMoney = ZhangOrders.Sum(t => t.ChangeMoney);
                inOrder.VarietyNum = varietyNum;
                inOrder.ChangeMoney = orderMoney;
                this.service.GetGenericService<SC_PiaoTo>().Update(inOrder, null);
            }
        }
    }
}
