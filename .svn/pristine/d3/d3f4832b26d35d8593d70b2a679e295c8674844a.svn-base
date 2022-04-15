using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Web.Controllers.SupplyChain;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.HR
{
    public class HR_GZ_PieceController : HRBaseController<HR_GZ_Piece, string>
    {
        public override ActionResult Index()
        {
            var query = this.service.GetGenericService<HR_GZ_PieceSet>().All();
            List<SelectListItem> Items = (from q in query
                                          select new SelectListItem
                                          {
                                              Text = q.Name,
                                              Value = q.ID.ToString()
                                          }).ToList();
            ViewBag.SetID = Items;
            return base.Index();
        }
         
    }
}
