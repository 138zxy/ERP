using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Model;
using System.Collections.Specialized;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_GoodsTypeController : BaseController<SC_GoodsType, string>
    {
        #region 获取分类树
        /// <summary>
        /// 获取分类树
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public ActionResult FindGoodsTypes(string nodeid)
        {
            IList<SC_GoodsType> sysfuncs = null;
            if (string.IsNullOrEmpty(nodeid))
            {
                sysfuncs = this.service.GetGenericService<SC_GoodsType>().All()
                    .Where(d => (String.IsNullOrEmpty(d.ParentID.ToString()) || d.ParentID.ToString() == "0"))
                    .OrderBy(p => p.OrderNo)
                    .ToList();
            }
            else
            {
                sysfuncs = this.service.GetGenericService<SC_GoodsType>().All()
                    .Where(d => d.ParentID.ToString() == nodeid)
                    .OrderBy(p => p.OrderNo)
                    .ToList();
            }

            if (sysfuncs != null && sysfuncs.Count > 0)
            {
                var data = new JqGridData<SC_GoodsType>
                {
                    rows = sysfuncs
                };
                return Json(data);
            }
            else
            {
                return new EmptyResult();
            }
        }
        #endregion

        #region 获取选择分类树
        /// <summary>
        /// 查找菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<SC_GoodsType> funcss = null;
        public JsonResult FindTree(string id)
        {
            IList<SC_GoodsType> root = this.service.GetGenericService<SC_GoodsType>().All()
                .Where(f => f.ParentID >= 0).OrderBy(p => p.OrderNo).ToList();
            funcss = root;
            subFindTree(root);
            var treeDics = from f in funcss
                           select new
                           {
                               id = f.ID,
                               name = f.TypeName,
                               title = f.TypeName,
                               pId = f.ParentID,
                               typeNo = f.TypeNo,
                               flag = f.Flag
                           };

            return Json(treeDics.ToList());
        }
        /// <summary>
        /// 子菜单递归查找
        /// </summary>
        /// <param name="root"></param>
        private void subFindTree(IList<SC_GoodsType> root)
        {
            IList<SC_GoodsType> sub = this.service.GetGenericService<SC_GoodsType>().All()
                .Where(p => root.Select(f => Convert.ToInt32(f.ID)).Contains(p.ParentID) && p.Flag == 1).OrderBy(p => p.OrderNo)
                .ToList();
            funcss = funcss.Union(sub).ToList();

            if (sub.Count != 0)
            {
                subFindTree(sub);
            }
        }
        #endregion

        #region 重写 新增、修改
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(SC_GoodsType entity)
        {          
            try
            {
                CalcEntityDeep(entity);
                var ret = base.Add(entity);
                return OperateResult(true, "新增成功", null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public override ActionResult Update(SC_GoodsType entity)
        {
            try
            {
                var entityT = this.m_ServiceBase.Get(entity.ID);
                CalcEntityDeep(entityT);
                this.service.GetGenericService<SC_GoodsType>().Update(entityT, null);

                base.Update(entity);
                return OperateResult(true, "修改成功", null);
            }
            catch (Exception ex)
            {

                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
        /// <summary>
        /// 计算实体的Deep属性
        /// </summary>
        /// <param name="entity"></param>
        private SC_GoodsType CalcEntityDeep(SC_GoodsType entity)
        {
            entity.DptLevel = 0;
            if (!string.IsNullOrEmpty(entity.ParentID.ToString()))
            {
                SC_GoodsType parent = this.service.GetGenericService<SC_GoodsType>().Get(entity.ParentID.ToString());
                if (parent != null)
                    entity.DptLevel = parent.DptLevel + 1;
            }
            return entity;
        }


        public override ActionResult Delete(string[] id)
        {

            foreach (var strid in id)
            {
                //判断当前分类下是否有商品
                int no = Convert.ToInt32(strid);
                var good = this.service.GetGenericService<SC_Goods>().Query().FirstOrDefault(t => t.TypeNo == no);
                if (good != null)
                {
                    return OperateResult(false, "当前分类下还有商品，不能删除", null);
                }
                //盘点是否有固定资产 
                var fiexd = this.service.GetGenericService<SC_Fixed>().Query().FirstOrDefault(t => t.Ftype == strid);
                if (fiexd != null)
                {
                    return OperateResult(false, "当前分类下还有固定资产，不能删除", null);
                }
            }
            return base.Delete(id);
        }
        #endregion
    }
}
