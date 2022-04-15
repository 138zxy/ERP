using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using ZLERP.Model.SupplyChain;
using ZLERP.Business;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Resources; 

namespace ZLERP.Web.Controllers.SupplyChain
{
    //作为一个公共类（控制器处理）
    public class SupplyChainHelp : BaseController<SC_Base, string>
    {
        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetUnit()
        {
            var Item = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "单位").ToList();
            List<SelectListItem> Items = (from q in Item
                                          select new SelectListItem
                                          {
                                              Text = q.ItemsName,
                                              Value = q.ItemsName
                                          }).ToList();

            return Items;
        }

        /// <summary>
        /// 获取品牌
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBrand()
        {
            var Brand = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "品牌").ToList();
            List<SelectListItem> Brands = (from q in Brand
                                           select new SelectListItem
                                           {
                                               Text = q.ItemsName,
                                               Value = q.ItemsName
                                           }).ToList();
            return Brands;
        }

        /// <summary>
        /// 获取供应商类别
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSupplyType()
        {
            var sup = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "供应商类别").ToList();
            List<SelectListItem> SupplierTypes = (from q in sup
                                                  select new SelectListItem
                                                  {
                                                      Text = q.ItemsName,
                                                      Value = q.ItemsName
                                                  }).ToList();

            return SupplierTypes;
        }
        /// <summary>
        /// 获取资产清理方式
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCleanType()
        {
            var sup = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "资产清理方式").ToList();
            List<SelectListItem> SupplierTypes = (from q in sup
                                                  select new SelectListItem
                                                  {
                                                      Text = q.ItemsName,
                                                      Value = q.ItemsName
                                                  }).ToList();

            return SupplierTypes;
        }
        /// <summary>
        /// 获取付款方式
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetFinanceTypes()
        {
            var fin = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "付款方式").ToList();
            List<SelectListItem> FinanceTypes = (from q in fin
                                                 select new SelectListItem
                                                 {
                                                     Text = q.ItemsName,
                                                     Value = q.ItemsName
                                                 }).ToList();

            FinanceTypes.Add(new SelectListItem { Text = SC_Common.PayType.SupplyIn, Value = SC_Common.PayType.SupplyIn });
            FinanceTypes.Add(new SelectListItem { Text = SC_Common.PayType.AllOut, Value = SC_Common.PayType.AllOut });
            return FinanceTypes;
        }
        /// <summary>
        /// 获取付款方式,排除供应商预付款和全额欠款
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPerPayTypes()
        {
            var fin = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "付款方式").ToList();
            List<SelectListItem> FinanceTypes = (from q in fin
                                                 select new SelectListItem
                                                 {
                                                     Text = q.ItemsName,
                                                     Value = q.ItemsName
                                                 }).ToList(); 
            return FinanceTypes;
        }
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSupply()
        {
            var Supply = this.service.GetGenericService<SC_Supply>().Query().Where(T => !T.IsOff).ToList();
            List<SelectListItem> Supplys = (from q in Supply
                                            select new SelectListItem
                                            {
                                                Text = q.SupplierName,
                                                Value = q.ID
                                            }).ToList();
            return Supplys;
        }

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetLib()
        {
            var Lib = this.service.GetGenericService<SC_Lib>().Query().Where(T => !T.IsOff).ToList();
            List<SelectListItem> Libs = (from q in Lib
                                         select new SelectListItem
                                         {
                                             Text = q.LibName,
                                             Value = q.ID
                                         }).ToList();
            return Libs;
        }

        /// <summary>
        /// 生产时间订单号
        /// </summary>
        /// <returns></returns>
        public string GenerateOrderNo()
        {
            Random ran = new Random();
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
        }
        /// <summary>
        /// 生产订单号
        /// </summary>
        /// <returns></returns>
        public string GenerateNo(string qz)
        {
            Random ran = new Random();
            return string.Format("{0}{1}{2}", qz, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999).ToString("000"));
        }

        /// <summary>
        /// 加载商品
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetSC_Goods(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<SC_Goods> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<SC_Goods>().Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or Spec like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.GetGenericService<SC_Goods>().Find(
                    where,
                    1,
                    100,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>[{5}]<br/>{3}：{1}<br/>{4}：{2}<br/>{6}：{7}",
                        HelperExtensions.Eval(p, textField),
                        p.Spec,
                        p.Price,
                        "规格",
                        "最近购买单价",
                        p.GoodsCode,
                        "库存",
                        p.AllNum),

                Value = HelperExtensions.Eval(p, valueField)
            });

            return Json(new SelectList(dataList, "Value", "Text"));
        }

        /// <summary>
        /// 加载库存商品，进行调拨,这里是根据仓库动态选择库存商品，不好做，所以把当前仓库显示出来
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetSC_LibGoods(string q, string textField, string valueField = "ID", string orderBy = "GoodsName", bool ascending = false, string condition = "")
        {
            IList<SC_NowLib> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<SC_NowLib>().Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.GetGenericService<SC_NowLib>().Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>[{9}]<br/>{1}：{5}    {2}：{6}<br/>{3}：{7}    {4}：{8}",
                        HelperExtensions.Eval(p, textField), 
                        "规格",
                        "库存数量",
                        "库存单价",
                        "仓库", 
                        p.SC_Goods.Spec,
                        p.Quantity,
                        p.PirceUnit,
                        p.SC_Lib.LibName,
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });

            return Json(new SelectList(dataList, "Value", "Text"));
        }

        /// <summary>
        /// 简单的商品加载，去掉敏感价格，适合物料的申请
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetSC_GoodsSample(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<SC_Goods> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<SC_Goods>().Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.GetGenericService<SC_Goods>().Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>[{5}]<br/>{3}：{1}<br/>{4}：{2}<br/>{6}：{7}",
                        HelperExtensions.Eval(p, textField),
                        p.Spec,
                        p.Brand,
                        "规格",
                        "品牌",
                        p.GoodsCode,
                        "库存",
                        p.AllNum),

                Value = HelperExtensions.Eval(p, valueField)
            });

            return Json(new SelectList(dataList, "Value", "Text"));
        }
        /// <summary>
        /// 获取用户列表列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetUser()
        {
            var user = this.service.GetGenericService<ZLERP.Model.User>().Query().Where(t=>t.IsUsed).OrderBy(t=>t.TrueName).ToList();
            List<SelectListItem> users = (from q in user
                                         select new SelectListItem
                                         {
                                             Text = q.TrueName,
                                             Value = q.ID
                                         }).ToList();
            return users;
        }

        /// <summary>
        /// 存在判断操作单，未修正，不能出入库
        /// </summary>
        /// <param name="lib"></param>
        /// <returns></returns>
        public bool IsPanDian(int lib)
        {
           var pan= this.service.GetGenericService<SC_PanDian>().Query().Where(t => t.LibID == lib && t.IsOff == false).FirstOrDefault();
           if (pan != null)
           {
               return true;
           } 
           return false;
        }

        /// <summary>
        /// 绑定单位
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult ListDataUnit(string GoodsID)
        {
            var good = this.service.GetGenericService<SC_Goods>().Get(GoodsID);
            var goodsID = Convert.ToInt32(GoodsID);
            var data0 = this.service.GetGenericService<SC_GoodsUnit>().Query().Where(t => t.GoodsID == goodsID);
            List<SC_GoodsUnit> umodel = new List<SC_GoodsUnit>();

            SC_GoodsUnit Model = new SC_GoodsUnit();
            Model.ID = 0;
            Model.Unit = good.Unit;
            Model.GoodsID = Convert.ToInt32(good.ID);
            Model.Rate = 1;
            umodel.Add(Model);
            umodel.AddRange(data0);
            umodel = umodel.OrderBy(t => t.Rate).ToList();
            return Json(new SelectList(umodel, "ID", "Unit"));
        }
        /// <summary>
        /// 获取单位转换比率
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public JsonResult GetUnitRate(string goodsId, string unit)
        {
            if (goodsId == "")
            {
                return OperateResult(false, "物资编码不存在，请重新选择", null);
            }
            if (unit == "")
            {
                return OperateResult(false, "单位不存在，请重新选择", null);
            }
            var unitGood = this.service.GetGenericService<SC_GoodsUnit>().Query().Where(p => p.GoodsID.ToString() == goodsId && p.Unit == unit).FirstOrDefault();
            var rate = Convert.ToDecimal(unitGood.Rate);
            var id = unitGood.GoodsID.ToString();
            var sc_good = this.service.GetGenericService<SC_Goods>().Get(id);
            var price = sc_good.Price;
            var LibPrice = sc_good.LibPrice;
            return Json(new { rate = rate, price = price * rate, LibPrice = LibPrice * rate });
        }

        /// <summary>
        /// 获取拼音简码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public JsonResult LP_TOPY_SHORT(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                SqlServerHelper help = new SqlServerHelper();
                var ds = help.ExecuteDataset("select dbo.LP_TOPY_SHORT(@str)", System.Data.CommandType.Text, new System.Data.SqlClient.SqlParameter("@str", str));

                return OperateResult(true, "拼音简码", ds.Tables[0].Rows[0][0]);
            }
            return OperateResult(false, "拼音简码", null);
        }

        /// <summary>
        /// 根据以往记录，动态获取填过的数据
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetSC_FixedInfo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            List<SelectListItem> data = new List<SelectListItem>(); ;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                SqlServerHelper help = new SqlServerHelper();
                var ds = help.ExecuteDataset(string.Format("select DISTINCT top 30  {0}  from SC_Fixed where  {0} like '%{1}%' ", textField,q), System.Data.CommandType.Text);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    data.Add(new SelectListItem { Text = row[0].ToString(), Value = row[0].ToString() });
                }
            }
            else
            {
                SqlServerHelper help = new SqlServerHelper();
                var ds = help.ExecuteDataset(string.Format("select DISTINCT top 30  {0} from SC_Fixed", textField), System.Data.CommandType.Text);
                foreach ( System.Data.DataRow row in ds.Tables[0].Rows)
                {
                   data.Add(new SelectListItem { Text = row[0].ToString(),Value=row[0].ToString()});
                }
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>",
                        p.Text), 
                Value = p.Value
            });

            return Json(new SelectList(dataList, "Value", "Text"));
        }

        /// <summary>
        /// 获取品牌
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetAddType()
        {
            var Brand = this.service.GetGenericService<SC_Base>().Query().Where(t => t.ItemsType == "资产增加方式").ToList();
            List<SelectListItem> Brands = (from q in Brand
                                           select new SelectListItem
                                           {
                                               Text = q.ItemsName,
                                               Value = q.ItemsName
                                           }).ToList();
            return Brands;
        }

       /// <summary>
       /// 获取部门
       /// </summary>
       /// <returns></returns>
        public List<SelectListItem> GetDepartment()
        {
            var user = this.service.GetGenericService<ZLERP.Model.Department>().Query().OrderBy(t=>t.DepartmentName).ToList();
            List<SelectListItem> users = (from q in user
                                          select new SelectListItem
                                          {
                                              Text = q.DepartmentName,
                                              Value = q.DepartmentName
                                          }).ToList();
            return users;
        }

       /// <summary>
       /// 获取资产信息
       /// </summary>
       /// <param name="q"></param>
       /// <param name="textField"></param>
       /// <param name="valueField"></param>
       /// <param name="orderBy"></param>
       /// <param name="ascending"></param>
       /// <param name="condition"></param>
       /// <returns></returns>
        public JsonResult GetSC_Fixed(string q, string textField, string valueField = "ID", string orderBy = "Fname", bool ascending = false, string condition = "")
        {
            IList<SC_Fixed> data;
            //q = FilterSpecial(q);//过滤调特殊字符
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<SC_Fixed>().Find(condition, 1, 100, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or Fcode like '%{1}%' or Fname like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.GetGenericService<SC_Fixed>().Find(
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
                        "资产编码",
                        p.Fcode,
                        "使用部门",
                        p.DepartMent),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }
    }
}