
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model; 
using System.Linq;
using ZLERP.Model.Enums;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class CarStatusController : BaseController<Car, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            //车辆状态
            var Cardic = this.service.GetGenericService<Dic>().Query().Where(t => t.ParentID == DicEnum.CarStatus.ToString());
            ViewBag.CarStatus = (from q in Cardic
                                 select new SelectListItem
                                 {
                                     Text = q.DicName,
                                     Value = q.TreeCode
                                 }).ToList();
            //每个状态对应的车辆数量
            var cars = this.m_ServiceBase.Query().Where(t => t.IsUsed).OrderBy(t=>t.ID).ToList();
            ViewBag.cars = cars;
            var carCounts = cars.GroupBy(t => t.CarStatus).Select(t => new { Key = t.Key, Count = t.Count().ToString() }).ToList();
            ViewBag.carCounts = (from g in carCounts
                                 join t in Cardic on g.Key equals t.TreeCode
                                 select new SelectListItem
                                 {
                                     Text = t.DicName,
                                     Value = g.Count
                                 }).ToList();

            //车辆类型
            ViewBag.CarTypes = this.service.GetGenericService<Dic>().Query()
             .Where(f => f.ParentID == DicEnum.CarType.ToString()).ToList();

            //获取所有的车辆


            return base.Index();
        }

        public ActionResult GetShopdocByCarID(string carid)
        {
            //var Nowlib = this.m_ServiceBase.Query().Where(t => t.LibID == lib && t.GoodsID == goodsID).FirstOrDefault();
            return OperateResult(true, "", null);
        }

        /// <summary>
        /// 查找菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<Dic> funcss = null;
        public JsonResult FindTree(string id)
        {
            IList<Dic> root = this.service.GetGenericService<Dic>().Query()
                .Where(f => f.ID == DicEnum.CarType.ToString()).OrderBy(p => p.OrderNum).ToList();
            funcss = root;

            subFindTree(root);

            var treeDics = from f in funcss
                           select new
                           {
                               id = f.ID,
                               name = f.DicName,
                               title = f.DicName,
                               pId = f.ParentID,
                               typeNo = f.TreeCode 
                           };

            return Json(treeDics.ToList());
        }
        /// <summary>
        /// 子菜单递归查找
        /// </summary>
        /// <param name="root"></param>
        private void subFindTree(IList<Dic> root)
        {
            IList<Dic> sub = this.service.GetGenericService<Dic>().All()
                .Where(p => root.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p => p.OrderNum)
                .ToList();
            funcss = funcss.Union(sub).ToList();

            if (sub.Count != 0)
            {
                subFindTree(sub);
            }
        }
    }    
}
