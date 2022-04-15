using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;
using ZLERP.Resources;
using System.IO;
using System.Configuration;
using ZLERP.Web.Helpers;
using System.Data;
using ZLERP.Business;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_GoodsController : BaseController<SC_Goods, string>
    {
        private object objlock = new object();
        public override ActionResult Index()
        {
            ViewBag.ItemsType =new  SupplyChainHelp().GetUnit();
            ViewBag.Brand =new SupplyChainHelp().GetBrand(); 
            return base.Index();
        }

        #region 构造分类数
        /// <summary>
        /// 查找菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<SC_GoodsType> funcss = null;
        public JsonResult FindTree(string id)
        {
            IList<SC_GoodsType> root = this.service.GetGenericService<SC_GoodsType>().All()
                .Where(f => f.ParentID >= 0 && f.Flag == 1).OrderBy(p => p.OrderNo).ToList();
            funcss = root;

            subFindTree(root);

            var treeDics = from f in funcss
                           select new
                           {
                               id = f.ID,
                               name = f.TypeName,
                               title = f.TypeName,
                               pId = f.ParentID,
                               typeNo = (f.TypeNo == null ? "" : f.TypeNo),
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

        /// <summary>
        /// 查找菜单树-固定资产
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FindTreeFixed(string id)
        {
            IList<SC_GoodsType> root = this.service.GetGenericService<SC_GoodsType>().All()
                .Where(f => f.ParentID >= 0&&f.Flag==2).OrderBy(p => p.OrderNo).ToList();
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
        private void subFindTreeFixed(IList<SC_GoodsType> root)
        {
            IList<SC_GoodsType> sub = this.service.GetGenericService<SC_GoodsType>().All()
                .Where(p => root.Select(f => Convert.ToInt32(f.ID)).Contains(p.ParentID) && p.Flag == 2).OrderBy(p => p.OrderNo)
                .ToList();
            funcss = funcss.Union(sub).ToList();

            if (sub.Count != 0)
            {
                subFindTree(sub);
            }
        }
        #endregion

        #region 分类管理
        /// <summary>
        /// 新增，修改，都在此方法进行
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pid">新增时是父类ID,修改时是当前ID</param>
        /// <param name="typeName"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult AddGoodsType(int id, string typeName, string orderNo, int flag)
        {


            if (id <= 0 || string.IsNullOrWhiteSpace(typeName))
            {
                return OperateResult(false, "父类或者子类名称不能为空", null);
            }
            var server = this.service.GetGenericService<SC_GoodsType>();
            int num = 0;
            int.TryParse(orderNo, out num);

            var isExist = server.Query().Where(t => t.TypeName == typeName).FirstOrDefault();
            if (isExist != null)
            {
                return OperateResult(false, string.Format("已经存在名称为{0}的分类了", typeName), null);
            }
            string TypeNo = string.Empty;
            var list = server.Query().Where(t => t.ParentID == id).ToList();
            //默认一个等级是3位数
            if (list == null || list.Count <= 0)
            {
                var parent = server.Get(id);
                TypeNo = parent.TypeNo + "000";
            }
            else
            {
                var Max = list.Select(t => Convert.ToInt32(t.TypeNo)).Max();
                int leng = list.First().TypeNo.Length;
                Max++;
                TypeNo = Max.ToString().PadLeft(leng, '0');
            }
            SC_GoodsType sc = new SC_GoodsType();
            sc.ParentID = id;
            sc.TypeName = typeName;
            sc.OrderNo = num;
            sc.TypeNo = TypeNo;
            sc.Flag = flag;
            server.Add(sc);

            return OperateResult(true, Lang.Msg_Operate_Success, null);

        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="id">编码</param>
        /// <param name="typeName">类型名称</param>
        /// <param name="orderNo">序号</param>
        /// <param name="flag">标识</param>
        /// <returns></returns>
        public ActionResult EditGoodsType(int id, string typeName, string orderNo, int flag)
        {
            try
            {
                var sc = this.service.GetGenericService<SC_GoodsType>().Get(id.ToString());
                sc.TypeName = typeName;
                sc.Flag = flag;
                int num = 0;
                int.TryParse(orderNo, out num);
                if (num > 0)
                {
                    sc.OrderNo = num;
                }
                this.service.GetGenericService<SC_GoodsType>().Update(sc,null);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id">主键编码</param>
        /// <returns></returns>
        public ActionResult DeleteGoodsType(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return OperateResult(false, "子类名称不能为空", null);
                }
                var parent = this.service.GetGenericService<SC_GoodsType>().Query().Where(t => t.ParentID == id).FirstOrDefault();
                if (parent!=null)
                {
                    return OperateResult(false, "当前类存在子类，请先删除子类", null);
                }
                var sc = this.service.GetGenericService<SC_GoodsType>().Get(id);
                this.service.GetGenericService<SC_GoodsType>().Delete(sc);
                return OperateResult(true, Lang.Msg_Operate_Success, null); 
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message.ToString(), null);
            }
        }
        #endregion

        #region 商品管理
        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(SC_Goods entity)
        {
            if (string.IsNullOrWhiteSpace(entity.GoodsName))
            {
                return OperateResult(false, "商品品名不能为空", null);
            }
            if (string.IsNullOrWhiteSpace(entity.Unit))
            {
                return OperateResult(false, "商品最小计量单位不能为空", null);
            }
            if (entity.TypeNo <= 0)
            {
                return OperateResult(false, "商品分类不能为空", null);
            }
            var type = this.service.GetGenericService<SC_GoodsType>().Get(entity.TypeNo.ToString());
            if (type == null)
            {
                return OperateResult(false, "选择的分类不正确", null);
            }
            entity.TypeString = type.TypeNo;

            var fristGood = this.m_ServiceBase.Query().Where(t => t.GoodsCode.Length > 0).OrderByDescending(T => T.GoodsCode).FirstOrDefault();
            if (fristGood == null || string.IsNullOrWhiteSpace(fristGood.GoodsCode))
            {
                entity.GoodsCode = "G000001";
            }
            else
            {
                var codeString = fristGood.GoodsCode.Substring(1, 6);
                var code = Convert.ToInt32(codeString) + 1;
                codeString = code.ToString().PadLeft(6, '0');
                entity.GoodsCode = "G" + codeString;
            }

            var res = base.Add(entity);

            var goodid = Convert.ToInt32(entity.ID);
            //增加辅助单位
            var unitGoods = this.service.GetGenericService<SC_GoodsUnit>().Query().Where(t => t.GoodsID == goodid && t.Unit == entity.Unit).FirstOrDefault();
            if (unitGoods == null)
            {
                SC_GoodsUnit umodel = new SC_GoodsUnit();
                umodel.Unit = entity.Unit;
                umodel.Rate = 1;
                umodel.GoodsID = goodid;
                umodel.UnitDesc = "最小计量单位";
                umodel.Meno = "系统生成";
                this.service.GetGenericService<SC_GoodsUnit>().Add(umodel);
            }
            return res;
        }

        //public override ActionResult Update(SC_Goods entity)
        //{

        //    //if (string.IsNullOrWhiteSpace(entity.GoodsName))
        //    //{
        //    //    return OperateResult(false, "商品品名不能为空", null);
        //    //}
        //    //if (entity.TypeNo <= 0)
        //    //{
        //    //    return OperateResult(false, "商品分类不能为空", null);
        //    //}
        //    var type = this.service.GetGenericService<SC_GoodsType>().Get(entity.TypeNo.ToString());
        //    if (type == null)
        //    {
        //        return OperateResult(false, "选择的分类不正确", null);
        //    }
        //    else
        //    {
        //       var sc= this.m_ServiceBase.Get(entity.ID);
        //       if (sc.TypeNo != entity.TypeNo)
        //       {
        //           sc.TypeString = type.TypeNo;
        //           this.m_ServiceBase.Update(sc,null);
        //       }
        //    }
        //    return base.Update(entity);
        //}

        public ActionResult LoadExcel()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files.Count <= 0)
                {
                    return OperateResult(false, "请选择合适的文件上传", null);
                }
                var file = files[0];
                string ext = Path.GetExtension(file.FileName).ToLower();
                if (!(ext == ".xls" || ext == ".xlsx"))
                {
                    return OperateResult(false, "请选择excel文件上传", null);
                }

                string _attachmentBaseDir = ConfigurationManager.AppSettings["AttachmentBaseDir"];

                string uploadFolder = Server.MapPath(_attachmentBaseDir);
                uploadFolder = Path.Combine(uploadFolder, "UploadFile");
                string fileName = Path.GetFileName(file.FileName);
                DirectoryInfo dirinfo = new DirectoryInfo(uploadFolder);
                if (!dirinfo.Exists)
                {
                    dirinfo.Create();
                }
                DateTime dt = DateTime.Now;
                DateTime sdt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                int difdt = (int)(dt - sdt).TotalSeconds;
                string reName = difdt + "_" + fileName;
                string fullFileName = Path.Combine(uploadFolder, reName);
                file.SaveAs(fullFileName);

                // 读取execl处理
                DataTable table = ExcelExportHelper.ExcelToDataTable(fullFileName, true);
                //新增标识列
                table.Columns.Add("GoodsCode", typeof(string));
                var GoodsCode = string.Empty;
                var fristGood = this.m_ServiceBase.Query().Where(t => t.GoodsCode.Length > 0).OrderByDescending(T => T.GoodsCode).FirstOrDefault();
                if (fristGood == null || string.IsNullOrWhiteSpace(fristGood.GoodsCode))
                {
                    GoodsCode = "G000001";
                }
                else
                {
                    var codeString = fristGood.GoodsCode.Substring(1, 6);
                    var code = Convert.ToInt32(codeString) + 1;
                    codeString = code.ToString().PadLeft(6, '0');
                    GoodsCode = "G" + codeString;
                }
                //查询所有的分类 
                var goodstype = this.service.GetGenericService<SC_GoodsType>().All();

                //查询所有的商品，用来判断品名和规格是否重复

                var goods = this.service.GetGenericService<SC_Goods>().Query().Select(t => new { GoodsName = t.GoodsName, Spec = t.Spec }).ToList();

                List<SC_Goods> Records = new List<SC_Goods>();
                int i = 0;
                foreach (DataRow row in table.Rows)
                {
                    var goosname = row["品名"].ToString();
                    var spec = row["规格"].ToString();
                    if (string.IsNullOrEmpty(goosname))
                    {
                        break;
                    }
                    var goodOne = goods.FirstOrDefault(t => t.GoodsName == goosname && t.Spec==spec);
                    if (goodOne != null)
                    {
                        continue;
                    }
                    SC_Goods Record = new SC_Goods();
                    Record.GoodsCode = GetGoodsCode(GoodsCode, i);
                    i++;
                    //标识列赋值
                    row["GoodsCode"] = Record.GoodsCode;
                    Record.GoodsName = row["品名"].ToString();
                    Record.GoodsNo = row["条码"].ToString();
                    Record.Spec = row["规格"].ToString();
                    Record.Unit = row["最小计量单位"].ToString();
                    Record.MinWarning = row["预警下线"].ToString() == "" ? 0 : Convert.ToInt32(row["预警下线"].ToString());
                    Record.MaxWarning = row["预警上线"].ToString() == "" ? 0 : Convert.ToInt32(row["预警上线"].ToString());

                    Record.Brand = row["品牌"].ToString();
                    Record.BrevityCode = row["拼音简码"].ToString();

                    var type = row["分类"].ToString();
                    var querytype = goodstype.FirstOrDefault(t => t.TypeName == type);
                    if (querytype != null)
                    {
                        Record.TypeNo = Convert.ToInt32(querytype.ID);
                    }
                    else
                    {
                        goodstype = this.service.GetGenericService<SC_GoodsType>().All();
                        querytype = goodstype.FirstOrDefault(t => t.TypeName == type);
                        if (querytype == null)
                        {
                            SC_GoodsType gdtype = new SC_GoodsType();
                            gdtype.TypeName = row["分类"].ToString();
                            gdtype.ParentID = 1;
                            gdtype.Flag = 1;
                            this.service.GetGenericService<SC_GoodsType>().Add(gdtype);
                            Record.TypeNo = Convert.ToInt32(gdtype.ID.ToString());
                        }
                        else
                        {
                            Record.TypeNo = Convert.ToInt32(querytype.ID.ToString()); ;
                        }
                        
                    }

                    Record.Price = row["最近购买价"].ToString() == "" ? 0 : Convert.ToDecimal(row["最近购买价"].ToString());

                    Record.SellerNo = row["商家编码"].ToString();
                    Record.Remark = row["备注"].ToString();
                    Record.Builder = AuthorizationService.CurrentUserID;
                    Record.BuildTime = DateTime.Now;
                    Records.Add(Record);
                }
                if (Records != null && Records.Count > 0)
                {
                    this.service.SupplyChainService.InsertRecord(Records);
                }
                //遍历Table 将库存插入入库单
                //处理完毕之后，删除
                if (System.IO.File.Exists(fullFileName))
                {
                    System.IO.File.Delete(fullFileName);
                }
                return ContentResult(true, string.Join("<br/>", "导入成功"), null);

            }
            catch (Exception ex)
            {
                return ContentResult(false, "No files to upload.", null);
            }
        }

        ActionResult ContentResult(bool result, string message, object data)
        {
            return Content(
                    HelperExtensions.ToJson(
                        OperateResult(result, message, data).Data
                    )
                );
        }

        /// <summary>
        /// 对商品编码循环++
        /// </summary>
        /// <param name="GoodsCode"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetGoodsCode(string GoodsCode, int i)
        {
            var codeString = GoodsCode.Substring(1, 6);
            var code = Convert.ToInt32(codeString) + i;
            codeString = code.ToString().PadLeft(6, '0');
            GoodsCode = "G" + codeString;
            return GoodsCode;
        }


        public override ActionResult Delete(string[] id)
        {
            foreach (var item in id)
            {
                //判断不能再采购单，入库单中存在
                var goodid = Convert.ToInt32(item);
                var reslut = this.service.GetGenericService<SC_ZhangInOrder>().Query().FirstOrDefault(t => t.GoodsID == goodid);
                if (reslut != null)
                {
                    return OperateResult(false, "该商品在采购单中存在，无法删除", null);
                }
                else
                {
                    var reslut1 = this.service.GetGenericService<SC_ZhangIn>().Query().FirstOrDefault(t => t.GoodsID == goodid);
                    if (reslut1 != null)
                    {
                        return OperateResult(false, "该商品在入库单中存在，无法删除", null);
                    }
                }
            }
            return base.Delete(id);
        }
        #endregion
    }
}
