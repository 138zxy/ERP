using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class PriceSettingController : BaseController<PriceSetting, int>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(PriceSetting PriceSetting)
        {
            var res= base.Add(PriceSetting);
            UpdateContractItem(Convert.ToInt32(PriceSetting.ContractItemsID));
            return res;
        }
  

        public override ActionResult Delete(int[] id)
        {
            var price = this.m_ServiceBase.Get(id[0]);
            var re = base.Delete(id);
            if (price != null)
            {
                UpdateContractItem(Convert.ToInt32(price.ContractItemsID));
            }
            return re;
        }

        /// <summary>
        /// 新增删除之后重新更新最新的砼单价
        /// </summary>
        /// <param name="id"></param>
        public void UpdateContractItem(int id)
        {
            var price = this.m_ServiceBase.Query().Where(t => t.ContractItemsID == id).OrderByDescending(t => t.ChangeTime).FirstOrDefault();
            if (price != null)
            {
                var server = this.service.GetGenericService<ContractItem>();
                ContractItem ci = server.Get(id);
                ci.UnPumpPrice = price.UnPumpPrice;
                ci.NewPriceDate = price.ChangeTime;
                server.Update(ci, null);
            }
        }
    }
}
