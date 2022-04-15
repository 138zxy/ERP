using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;
using ZLERP.Model.ViewModels;
using System.Collections;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class ProjectUsersController : ServiceBasedController
    {
        /// <summary>
        /// 定义JqGrid的表记录类
        /// </summary>
        public class JqGridRow
        {
            public string id { get; set; }
            public List<string> cell { get; set; }
        }
        /// <summary>
        /// 定义JqGrid的JSON数据类
        /// </summary>
        public class JsonJqGridData
        {
            public int page { get; set; }
            public int total { get; set; }
            public int records { get; set; }
            public string userdata { get; set; }
            public List<JqGridRow> rows { get; set; }
        }

        public override ActionResult Index()
        {          
            return base.Index();
        }
     
        [Description("获取tree列表")]
        public ActionResult GetZtreeData_Project()
        {
            List<TreeNode> znodes = new List<TreeNode>();
            #region 工地列表
            TreeNode pjRoot = new TreeNode();
            pjRoot.isParent = true;
            pjRoot.name = "工地列表";
            pjRoot.open = true;
            pjRoot.icon = "";
            pjRoot.dbId = "p1_0";
            pjRoot.nocheck = true;
            List<TreeNode> sub = new List<TreeNode>();
            IList<Project> projectList = this.service.GetGenericService<Project>().Query().Where(p => p.Contract.Customer.CustType == "CustTG" || p.Contract.Customer.CustType == "CustTS").OrderBy(p => p.Contract.ContractName).ToList();
            ArrayList list = new ArrayList();
            List<Contract> ContractList = new List<Contract>();
            foreach (Project item in projectList)
            {
                if (!list.Contains(item.Contract.ID))
                {
                    list.Add(item.Contract.ID);
                }
                if (ContractList.FirstOrDefault(t => t.ID == item.ContractID) == null)
                {
                    ContractList.Add(item.Contract);
                }
            }
            TreeNode nodeP;
            for (int i = 0; i < list.Count; i++)
            {
                //构造子节点
                var projectListQ = projectList.Where(p => p.ContractID == list[i].ToString()).ToList();
                List<TreeNode> _list = new List<TreeNode>();
                foreach (Project p in projectListQ)
                {
                    nodeP = new TreeNode(                   
                    p.ProjectName
                    , p.ID + "," + p.ProjectAddr
                    , null
                    , true
                    , false
                    , "../../Content/erpimage/mapimage/icon/home.png"
                    , p.Longitude.HasValue ? "" : "red"
                    , p.ID
                    , string.Empty
                    , (p.Longitude + "," + p.Latitude)
                    , (p.PlaceRange.HasValue ? p.PlaceRange : 0) + "," + p.LinkMan + "," + p.Tel + "," + p.ProjectAddr
                    , false);
                    _list.Add(nodeP);
                }

                //Contract op = this.service.GetGenericService<Contract>().Query().Where(p => p.ID == list[i].ToString()).FirstOrDefault();
                Contract op = ContractList.FirstOrDefault(t => t.ID == list[i].ToString());
                nodeP = new TreeNode(op.ContractName
                    , op.ContractName
                    , _list
                    , false
                    , true
                    , ""
                    , ""
                    , "ccc2_" + list[i]
                    , string.Empty
                    , ""
                    , ""
                    , false);
                sub.Add(nodeP);
            }

            if (sub.Count() == 0)
            {
                pjRoot.isParent = false;
            }
            else
            {
                pjRoot.children = sub;
            }
            znodes.Add(pjRoot);
            #endregion
            return Json(znodes);
        }

        /// <summary>
        /// 获取用户工程权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult GetUserProject(string userId)
        {
            IList<Project> pList = this.service.User.GetUserProject(userId).OrderBy(p => p.ID).ToList();
            return Json(pList);
        }

        /// <summary>
        /// 保存用户工程权限
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        /// <returns></returns>
        public ActionResult SaveUserProjects(string[] userIds, string[] projects)
        {
            //UNDONE:同时修改多用户工程权限的问题
            this.service.User.SaveUserProjects(userIds, projects);
            return OperateResult(true, Lang.Msg_Operate_Success, "");
        }
    }
}
