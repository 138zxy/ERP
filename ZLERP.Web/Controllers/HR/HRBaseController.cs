using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.HR;
using ZLERP.Model.HR.Enums;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers.HR
{
    public class HRBaseController<TEntity, Tid> : BaseController<TEntity, Tid> where TEntity : Entity
    {
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBaseData(string itemType)
        {
            var Item = this.service.GetGenericService<HR_Base_Data>().Query().Where(t => t.ItemsType == itemType).ToList();
            List<SelectListItem> Items = (from q in Item
                                          select new SelectListItem
                                          {
                                              Text = q.ItemsName,
                                              Value = q.ItemsName
                                          }).ToList();
            return Items;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPersonnelList()
        {
            var Item = this.service.GetGenericService<HR_Base_Personnel>().Query().Where(t => t.State == "在职").OrderBy(t=>t.Name).ToList();
            List<SelectListItem> Items = (from q in Item
                                          select new SelectListItem
                                          {
                                              Text = q.Name,
                                              Value = q.ID.ToString()
                                          }).ToList();
            return Items;
        }
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPersonnelListText()
        {
            var Item = this.service.GetGenericService<HR_Base_Personnel>().Query().Where(t => t.State == "在职").OrderBy(t => t.Name).ToList();
            List<SelectListItem> Items = (from q in Item
                                          select new SelectListItem
                                          {
                                              Text = q.Name,
                                              Value = q.Name
                                          }).ToList();
            return Items;
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetPersonnel(string q, string textField, string valueField = "ID", string orderBy = "name", bool ascending = false, string condition = "")
        {
            IList<HR_Base_Personnel> data; 
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<HR_Base_Personnel>().Find(condition, 1, 100, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or Department.DepartmentName like '%{1}%')", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.GetGenericService<HR_Base_Personnel>().Find(
                where,
                1,
                100,
                orderBy,
                ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>[<strong>{1}</strong>]<br/>{2}：{3}<br/>{4}：{5}",
                        HelperExtensions.Eval(p, textField),
                        p.ID,
                        "工号",
                        p.JobNo,
                        "部门",
                        p.Department==null?"":p.Department.DepartmentName),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }
        /// <summary>
        /// 生成单据号
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public ActionResult GenerateOrderNo(string prefix)
        {
            string orderNo = new ZLERP.Web.Controllers.SupplyChain.SupplyChainHelp().GenerateOrderNo();
            return OperateResult(true, "订单号生成成功", string.Format("{0}{1}", prefix, orderNo));
        }
         /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetArrangeList()
        {
            var Item = this.service.GetGenericService<HR_KQ_Arrange>().All().OrderBy(t => t.ArrangeName).ToList();
            List<SelectListItem> Items = (from q in Item
                                          select new SelectListItem
                                          {
                                              Text = q.ArrangeName,
                                              Value = q.ID.ToString()
                                          }).ToList();
            return Items;
        }
    }
    
}
