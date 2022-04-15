using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_ZhangInController : BaseController<SC_ZhangIn, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(SC_ZhangIn SC_ZhangIn)
        {
            if (SC_ZhangIn.GoodsID <= 0)
            {
                return OperateResult(false, "请选择商品", null);
            }
            if (SC_ZhangIn.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            if (string.IsNullOrWhiteSpace(SC_ZhangIn.Unit))
            {
                return OperateResult(false, "请输入购买单位", null);
            }
            if (SC_ZhangIn.UnitRate<=0)
            {
                return OperateResult(false, "请输入单位转换比率有误", null);
            }
            if (SC_ZhangIn.PriceUnit <= 0)
            {
                return OperateResult(false, "请输入购买单价", null);
            }
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.InNo == SC_ZhangIn.InNo && t.GoodsID == SC_ZhangIn.GoodsID && t.Unit == SC_ZhangIn.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            //如果是采购退货，则数量需要是负数
            var piaoin = this.service.GetGenericService<SC_PiaoIn>().Get(SC_ZhangIn.InNo.ToString());
            if (piaoin.InType == SC_Common.InType.PurOut)
            {
                if (SC_ZhangIn.Quantity > 0)
                {
                    SC_ZhangIn.Quantity = -SC_ZhangIn.Quantity;
                }
            }
            SC_ZhangIn.InMoney = SC_ZhangIn.Quantity * SC_ZhangIn.PriceUnit;
            SC_ZhangIn.SC_Goods = null;
            var res = base.Add(SC_ZhangIn);
            UpdateZhangIn(SC_ZhangIn.InNo.ToString());
            return res;
        }

        public override ActionResult Update(SC_ZhangIn SC_ZhangIn)
        {
            if (SC_ZhangIn.GoodsID <= 0)
            {
                return OperateResult(false, "请选择供商品", null);
            }
            if (SC_ZhangIn.Quantity <= 0)
            {
                return OperateResult(false, "请输入购买数量", null);
            }
            if (SC_ZhangIn.PriceUnit <= 0)
            {
                return OperateResult(false, "请输入购买单价", null);
            }
            //判断同一个种商品是否录入多个
            var query = this.m_ServiceBase.Query().Where(t => t.InNo == SC_ZhangIn.InNo && t.ID != SC_ZhangIn.ID && t.GoodsID == SC_ZhangIn.GoodsID && t.Unit == SC_ZhangIn.Unit).FirstOrDefault();
            if (query != null)
            {
                return OperateResult(false, "当前订单已经存在该商品，请不要重复添加", null);
            }
            //如果是采购退货，则数量需要是负数
            var piaoin = this.service.GetGenericService<SC_PiaoIn>().Get(SC_ZhangIn.InNo.ToString());
            if (piaoin.InType == SC_Common.InType.PurOut)
            {
                if (SC_ZhangIn.Quantity > 0)
                {
                    SC_ZhangIn.Quantity = -SC_ZhangIn.Quantity;
                }
            }
            SC_ZhangIn.InMoney = SC_ZhangIn.Quantity * SC_ZhangIn.PriceUnit;
            var res = base.Update(SC_ZhangIn);
            UpdateZhangIn(SC_ZhangIn.InNo.ToString());
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

            var inOrder = this.service.GetGenericService<SC_PiaoIn>().Query().Where(t => t.ID == id).FirstOrDefault();
            if (inOrder != null)
            {
                int orderNo = Convert.ToInt32(id);
                var ZhangOrders = this.m_ServiceBase.Query().Where(t => t.InNo == orderNo).ToList();
                var varietyNum = ZhangOrders.GroupBy(t => t.GoodsID).Count();
                var orderMoney = ZhangOrders.Sum(t => t.InMoney);
                inOrder.VarietyNum = varietyNum;
                inOrder.InMoney = orderMoney;
                this.service.GetGenericService<SC_PiaoIn>().Update(inOrder, null);
            }
        }
    }
}
