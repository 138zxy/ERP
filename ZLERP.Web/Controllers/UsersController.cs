using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.ComponentModel;
using ZLERP.Model.ViewModels;

namespace ZLERP.Web.Controllers
{
    public class UsersController : BaseController<User, string>
    {
        public override ActionResult Index()
        {
            ViewBag.UserList = new SelectList(this.service.User.All(new List<string> { "ID", "TrueName" }, "IsUsed=1 and UserType<>'01'", "ID", true), "ID", "TrueName");
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "DepartmentName", true, "");
            return base.Index();
        }

        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult GetUserFuncs(string userId)
        {
            IList<SysFunc> sysfuncList = this.service.User.GetUserFuncs2(userId).OrderBy(p=>p.OrderNum).ToList();
            return Json(sysfuncList);

        }   
    }
}
