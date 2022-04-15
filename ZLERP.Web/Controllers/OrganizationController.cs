using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class OrganizationController : BaseController<Organization, int>
    {
        public override ActionResult Index()
        {

            var parentOrganizations = this.service.GetGenericService<Organization>().All("", "ID", true);
            parentOrganizations.Insert(0, new Organization());
            ViewBag.parentOrganizations = new SelectList(parentOrganizations, "ID", "OrganizationName");


            return base.Index();
        }

        public ActionResult Departments()
        {
            Dictionary<int?, string> departments = new Dictionary<int?, string>();
            var _department = this.service.GetGenericService<Department>().All("", "ID", true);
            foreach (Department department in _department)
            {
                departments.Add(department.ID, department.DepartmentName);
            }
            return PartialView("Select", departments);
        }

        public ActionResult FindOrganizations(string nodeid)
        {
            IList<Organization> results = null;
            if (string.IsNullOrEmpty(nodeid))
            {
                results = this.service.GetGenericService<Organization>().All()
                    .Where(d => (String.IsNullOrEmpty(d.ParentId) || d.ParentId == "0"))
                    .ToList();
            }
            else
            {
                results = this.service.GetGenericService<Organization>().All()
                    .Where(d => d.ParentId == nodeid)
                    .ToList();
            }

            if (results != null && results.Count > 0)
            {

                var data = new JqGridData<Organization>
                {
                    rows = results
                };
                return Json(data);

            }
            else
            {
                var data = new JqGridData<Organization>
                {

                };
                return Json(data);
            }

        }

        public JsonResult FindTree(string id)
        {

            var root = this.service.GetGenericService<Organization>().All()
                .Where(f => string.IsNullOrEmpty(f.ParentId));

            var sub1 = this.service.GetGenericService<Organization>().All()
                .Where(p => root.Select(f => f.ID.ToString()).Contains(p.ParentId))
                .ToList();

            var sub2 = this.service.GetGenericService<Organization>().All()
               .Where(p => sub1.Select(f => f.ID.ToString()).Contains(p.ParentId))
               .ToList();

            var funcs = root.Union(sub1)
                 .Union(sub2);

            var treeDics = from f in funcs
                           select new
                           {
                               id = f.ID,
                               name = f.OrganizationName,
                               title = f.OrganizationName,
                               pId = f.ParentId

                           };

            return Json(treeDics.ToList());
        }
    }
}
