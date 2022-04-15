
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using System.IO;
using System.Data;
namespace ZLERP.Web.Controllers
{
    public class ProductRecordController : BaseController<ProductRecord, string>
    {
        /// <summary>
        /// 生产记录
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);
            //ViewBag.ShippingDocumentInfo =  HelperExtensions.SelectListData<ShippingDocument>("ID", "ID", "1<>1", "ID", true, "");
            return base.Index();
        }
        /// <summary>
        /// 新增手动生产记录
        /// </summary>
        /// <param name="ProductRecord"></param>
        /// <returns></returns>
        public ActionResult HandleAdd(ProductRecord ProductRecord)
        {
            ShippingDocument shippingDocument = this.service.GetGenericService<ShippingDocument>().Get(ProductRecord.ShipDocID);
            ProductRecord.ProductLineID = shippingDocument.ProductLineID;
            if (ProductRecord.ID == null)
            {
                ProductRecord.ElectValue = 0;
                ProductRecord.IsManual = true;
                return base.Add(ProductRecord);
            }
            else
            {
                return base.Update(ProductRecord);
            }
        }
        /// <summary>
        /// 修改生产记录
        /// </summary>
        /// <param name="ProductRecord"></param>
        /// <returns></returns>
        public override ActionResult Update(ProductRecord ProductRecord)
        {
            if (ProductRecord.DispatchID != null)
            {
                return OperateResult(false, "不允许修改此盘生产记录", false);
            }
            else
            {
                return base.Update(ProductRecord);
            }
            //throw new ApplicationException("该方法被禁止调用");
        }

        public FileResult ExportExcel(string ids)
        {
            //创建Excel文件的对象
            HSSFWorkbook workBook = new HSSFWorkbook();
            string[] idss = ids.Split(',');
            foreach (string id in idss)
            {
                ShippingDocument sd = this.service.ShippingDocument.Get(id);
                string title = sd.ProjectName + " 第" + sd.ProvidedTimes.ToString() + "车"+sd.ID;
                string a = String.Format("方量：{0} 生产时间：{1} 运输车号：{2} 搅拌机组：{3}", sd.SignInCube, sd.ProduceDate, sd.CarID, sd.ProductLineName);
                string b = String.Format("砼强度：{0} 施工部位：{1} 浇筑方式：{2} 操作员：{3} 质检员：{4} 配比号：{5} 任务单：{6}", sd.ConStrength, sd.ConsPos, sd.CastMode, sd.Operator, sd.Surveyor, sd.ConsMixpropID, sd.TaskID);
                //添加一个sheet
                ISheet sheet1 = workBook.CreateSheet(id);
                IRow row1 = sheet1.CreateRow(0);
                row1.CreateCell(0).SetCellValue(title);
                IRow row2 = sheet1.CreateRow(1);
                row2.CreateCell(0).SetCellValue(a);
                IRow row3 = sheet1.CreateRow(2);
                row3.CreateCell(0).SetCellValue(b);

                SqlServerHelper helper = new SqlServerHelper();
                DataSet ds = helper.ExecuteDataset("select ActualAmount,TheoreticalAmount,SiloID,ErrorValue,PerAmount,PotTimes from ProductRecordItems  p1 left join ProductRecord p2 on p1.ProductRecordID=p2.ProductRecordID where ShipDocID='" + id + "'", System.Data.CommandType.Text, null);

                IList<dynamic> pr = new List<dynamic>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        pr.Add(new
                        {
                            ActualAmount = dr["ActualAmount"].ToString().Trim(),
                            TheoreticalAmount = dr["TheoreticalAmount"].ToString().Trim(),
                            SiloID = dr["SiloID"].ToString().Trim(),
                            ErrorValue = dr["ErrorValue"].ToString().Trim(),
                            PerAmount = dr["PerAmount"].ToString(),
                            PotTimes = dr["PotTimes"].ToString().Trim()
                        });
                    }
                    if (pr.Count > 0)
                    {
                        int pottimes = pr.Max(p => Convert.ToInt32(p.PotTimes));

                        int rownum = 4 + pottimes * 3;

                        sheet1.CreateRow(3).CreateCell(0).SetCellValue(".");
                        sheet1.CreateRow(4).CreateCell(0).SetCellValue("单方");

                        for (int i = 1; i < pottimes + 1; i++)
                        {
                            sheet1.CreateRow(2 + i * 3).CreateCell(0).SetCellValue("第" + i + "罐配比");
                            sheet1.CreateRow(3 + i * 3).CreateCell(0).SetCellValue("第" + i + "罐用量");
                            sheet1.CreateRow(4 + i * 3).CreateCell(0).SetCellValue("第" + i + "罐误差");
                        }
                        sheet1.CreateRow(1 + rownum).CreateCell(0).SetCellValue("总计");
                        sheet1.CreateRow(2 + rownum).CreateCell(0).SetCellValue("总误差");


                        List<SiloProductLine> spls = this.service.GetGenericService<SiloProductLine>().All().Where(p => p.ProductLineID == sd.ProductLineID).ToList();
                        int colum = 1;
                        foreach (SiloProductLine spl in spls)
                        {
                            sheet1.GetRow(3).CreateCell(colum).SetCellValue(spl.SiloName);
                            var il = pr.Where(p => p.SiloID == spl.SiloID).ToList();
                            if (il.Count>0)
                            {
                                sheet1.GetRow(4).CreateCell(colum).SetCellValue(il.FirstOrDefault().PerAmount);
                                decimal sum_a = 0;
                                decimal sum_t = 0;
                                for (int i = 1; i < pottimes + 1; i++)
                                {
                                    decimal ta = Convert.ToDecimal(il.FirstOrDefault(p => Convert.ToDecimal(p.PotTimes) == i).TheoreticalAmount==null ? 0:il.FirstOrDefault(p => Convert.ToDecimal(p.PotTimes) == i).TheoreticalAmount);
                                    decimal aa = Convert.ToDecimal(il.FirstOrDefault(p => Convert.ToDecimal(p.PotTimes) == i).ActualAmount == null ? 0 : il.FirstOrDefault(p => Convert.ToDecimal(p.PotTimes) == i).ActualAmount);
                                    sum_a = sum_a + aa;
                                    sum_t = sum_t + ta;
                                    sheet1.GetRow(2 + i * 3).CreateCell(colum).SetCellValue(ta.ToString());
                                    sheet1.GetRow(3 + i * 3).CreateCell(colum).SetCellValue(aa.ToString());
                                    sheet1.GetRow(4 + i * 3).CreateCell(colum).SetCellValue(il.FirstOrDefault(p => Convert.ToDecimal(p.PotTimes) == i).ErrorValue);
                                }
                                sheet1.GetRow(1 + rownum).CreateCell(colum).SetCellValue(sum_a.ToString());
                                sheet1.GetRow(2 + rownum).CreateCell(colum).SetCellValue(Math.Round(((sum_a - sum_t) * 100 / sum_t),2).ToString());
                            }
                            colum++;
                        }
                    }
                }
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            workBook.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "生产记录明细.xls");
        }

        public ActionResult Import(string id)
        {
            bool result = this.service.ProductRecord.Import(id);
            return OperateResult(true, Lang.Msg_Operate_Success, result);
        }

        public ActionResult CopyRecord(string id, int pots)
        {
            try
            {
                ProductRecord obj = this.service.GetGenericService<ProductRecord>().Get(id);

                ShippingDocument ShippingDocument = this.service.GetGenericService<ShippingDocument>().Get(obj.ShipDocID);


                IList<ProductRecord> ps = this.service.GetGenericService<ProductRecord>().Query().Where(m => m.ShipDocID == obj.ShipDocID).ToList();
                int? MaxPots = this.service.DispatchList.CalculateTotalPot(obj.PCRate, ShippingDocument.SendCube);
                IList<ProductRecordItem> items = this.service.ProductRecordItemService.Query().Where(m => m.ProductRecordID == id).ToList();
                if (MaxPots < ps.Count + 1)
                {
                    throw new Exception("本车生产" + ShippingDocument.SendCube + "方，最多" + MaxPots + "盘，超出盘数了");
                }
                ProductRecord temp = new ProductRecord();
                temp.ID = obj.ID.Substring(0, obj.ID.LastIndexOf('_')) + "_" + pots;
                temp.IsManual = true;
                temp.PCRate = obj.PCRate;
                temp.PotTimes = pots;
                temp.ShipDocID = obj.ShipDocID;
                temp.BuildTime = obj.BuildTime;
                temp.ProduceCube = obj.ProduceCube;
                temp.DispatchID = obj.DispatchID;
                temp.ProductLineID = obj.ProductLineID;
                temp = this.service.GetGenericService<ProductRecord>().Add(temp);


                foreach (ProductRecordItem item in items)
                {
                    ProductRecordItem obj1 = new ProductRecordItem();
                    obj1.ProductRecordID = temp.ID;
                    obj1.SiloID = item.SiloID;
                    obj1.StuffID = item.StuffID;
                    obj1.TheoreticalAmount = item.TheoreticalAmount;
                    obj1.ActualAmount = item.ActualAmount;
                    obj1.ErrorValue = item.ErrorValue;
                    this.service.ProductRecordItemService.Add(obj1);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, temp);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }

        public override ActionResult Delete(string[] id)
        {
            try
            {
                ThreadID tid;
                foreach (string pid in id)
                {
                    IList<ProductRecordItem> items = this.service.ProductRecordItemService.Query().Where(m => m.ProductRecordID == pid).ToList();
                    foreach (ProductRecordItem iitem in items)
                    {
                        this.service.ProductRecordItemService.Delete(iitem);

                        tid = new ThreadID();
                        tid.currentDate = DateTime.Now;
                        tid.typeID = iitem.StuffID;//主材id
                        tid.typename = "0";//主材
                        this.service.ThreadID.Add(tid);
                    }
                }
                return base.Delete(id);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }
    }
}
