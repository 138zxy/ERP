using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_NowLibIniController : BaseController<SC_Goods, string>
    {
        //
        // GET: /SC_Base/

        public override ActionResult Index()
        {
            ViewBag.Libs = new SupplyChainHelp().GetLib();
            ViewBag.SC_PiaoIns = this.service.GetGenericService<SC_PiaoIn>().Query().Where(t => t.InType == SC_Common.InType.LibIni).ToList();

            return base.Index();
        }

        /// <summary>
        /// 库存初始化选择的商品
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ActionResult LibIn(string[] keys, int libId)
        {
            if (keys.Length <= 0)
            {
                return OperateResult(false, "请选择需要初始化的商品", null);
            }
            if (libId <= 0)
            {
                return OperateResult(false, "请选择初始化的仓库", null);
            }
            var goods = this.m_ServiceBase.Query().Where(t => keys.Contains(t.ID)).ToList();
            //2.生成入库单
            SC_PiaoIn PiaoIn = new SC_PiaoIn();
            PiaoIn.PurchaseID = 0;
            PiaoIn.SupplierID = 0;
            PiaoIn.LibID = libId;
            PiaoIn.InType = SC_Common.InType.LibIni;
            PiaoIn.InDate = DateTime.Now;
            string InNo = new SC_PiaoInController().GetGenerateOrderNo();
            PiaoIn.InNo = InNo;
            PiaoIn.Purchase = "";
            PiaoIn.VarietyNum = 0; //要算
            PiaoIn.InMoney = 0; //要算
            PiaoIn.PayType = "";
            PiaoIn.Condition = SC_Common.InStatus.Ini;
            PiaoIn.Remark = SC_Common.InType.LibIni;

            int i = 0;
            //3.生产入库单明细
            var ZhangIns = (from q in goods
                            select new SC_ZhangIn
                            {
                                GoodsID = Convert.ToInt32(q.ID),
                                Sequence = i++,
                                Indate = DateTime.Now,
                                Quantity = 0,
                                PriceUnit = q.Price,
                                InMoney = 0,
                                PiNo = "" ,
                                UnitRate=1,
                                Unit=q.Unit
                            }).ToList();

            PiaoIn.VarietyNum = ZhangIns.Count();
            PiaoIn.InMoney = ZhangIns.Sum(t => t.InMoney);
            var result = this.service.SupplyChainService.PurIn(PiaoIn, ZhangIns);
            return Json(result);
        }
    }
}
