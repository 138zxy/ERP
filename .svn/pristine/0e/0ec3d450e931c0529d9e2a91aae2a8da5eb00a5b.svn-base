using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;
using System.Web.Script.Serialization;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_PiaoChangeController : BaseController<SC_PiaoChange, string>
    {

        private object obj = new object();
        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            return base.Index();
        }
        public ActionResult GenerateOrderNo()
        {
            string orderNo = GetGenerateOrderNo();
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        public string GetGenerateOrderNo()
        {
            string orderNo = new SupplyChainHelp().GenerateOrderNo();
            return string.Format("{0}{1}", "G", orderNo);
        }

        public override ActionResult Find(Lib.Web.Mvc.JQuery.JqGrid.JqGridRequest request, string condition)
        {
            int total;
            IList<SC_PiaoChange> list = m_ServiceBase.Find(request, condition, out total);
            foreach (var l in list)
            {
                foreach (var q in l.SC_ZhangChanges.Where(t => t.Quantity < 0).ToList())
                {
                    //品名
                    if (string.IsNullOrEmpty(l.GoodsName))
                    {
                        l.GoodsName = q.SC_Goods.GoodsName;
                    }
                    else
                    {
                        l.GoodsName += "</br>" + q.SC_Goods.GoodsName;
                    }
                    //单位
                    if (string.IsNullOrEmpty(l.Unit))
                    {
                        l.Unit = q.SC_Goods.Unit;
                    }
                    else
                    {
                        l.Unit += "</br>" + q.SC_Goods.Unit;
                    }
                    //数量
                    if (string.IsNullOrEmpty(l.V_Quantity))
                    {
                        l.V_Quantity = q.Quantity.ToString();
                    }
                    else
                    {
                        l.V_Quantity += "</br>" + q.Quantity.ToString();
                    }

                    //规格
                    if (string.IsNullOrEmpty(l.Spec))
                    {
                        l.Spec = q.SC_Goods.Spec;
                    }
                    else
                    {
                        l.Spec += "</br>" + q.SC_Goods.Spec;
                    }

                    //单价

                    if (string.IsNullOrEmpty(l.Price))
                    {
                        l.Price = q.PriceUnit.ToString();
                    }
                    else
                    {
                        l.Price += "</br>" + q.PriceUnit.ToString();
                    }
                    //仓库

                    if (string.IsNullOrEmpty(l.LibName))
                    {
                        l.LibName = q.SC_Lib.LibName;
                    }
                    else
                    {
                        l.LibName += "</br>" + q.SC_Lib.LibName;
                    }
                }

                foreach (var q in l.SC_ZhangChanges.Where(t => t.Quantity > 0).ToList())
                {
                    //品名
                    if (string.IsNullOrEmpty(l.GoodsName2))
                    {
                        l.GoodsName2 = q.SC_Goods.GoodsName;
                    }
                    else
                    {
                        l.GoodsName2 += "</br>" + q.SC_Goods.GoodsName;
                    }
                    //单位
                    if (string.IsNullOrEmpty(l.Unit2))
                    {
                        l.Unit2 = q.SC_Goods.Unit;
                    }
                    else
                    {
                        l.Unit2 += "</br>" + q.SC_Goods.Unit;
                    }
                    //数量
                    if (string.IsNullOrEmpty(l.V_Quantity2))
                    {
                        l.V_Quantity2 = q.Quantity.ToString();
                    }
                    else
                    {
                        l.V_Quantity2 += "</br>" + q.Quantity.ToString();
                    }

                    //规格
                    if (string.IsNullOrEmpty(l.Spec2))
                    {
                        l.Spec2 = q.SC_Goods.Spec;
                    }
                    else
                    {
                        l.Spec2 += "</br>" + q.SC_Goods.Spec;
                    }

                    //单价

                    if (string.IsNullOrEmpty(l.Price2))
                    {
                        l.Price2 = q.PriceUnit.ToString();
                    }
                    else
                    {
                        l.Price2 += "</br>" + q.PriceUnit.ToString();
                    }
                    //仓库

                    if (string.IsNullOrEmpty(l.LibName2))
                    {
                        l.LibName2 = q.SC_Lib.LibName;
                    }
                    else
                    {
                        l.LibName2 += "</br>" + q.SC_Lib.LibName;
                    }
                }
            }

            JqGridData<SC_PiaoChange> data = new JqGridData<SC_PiaoChange>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = list
            };
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(data),
                ContentType = "application/json"
            };
        } 
 
    }
}
