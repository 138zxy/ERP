using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Model.Material;

namespace ZLERP.Web.Controllers.Material
{
    public class M_CarController : BaseController<M_Car, string>
    {


        public override ActionResult Index()
        {
            return base.Index();
        }
        public override ActionResult Add(M_Car M_Car)
        {
            return base.Add(M_Car);
        }
        public override ActionResult Update(M_Car M_Car)
        {
            return base.Update(M_Car);
        }
    }
}
