using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.Web.Controllers.Attributes;


namespace ZLERP.Web.Controllers
{
    public class ProductLineController : BaseController<ProductLine, string>
    {

        public override System.Web.Mvc.ActionResult Index()
        {
            //生成匿名类型有"ID", "MeasureName"两个属性 按MeasureName排序 true查询所有数据
            var measureList =
                this.service.GetGenericService<MeasureInfo>()
                .All(new List<string> { "ID", "MeasureName" }, "MeasureName", true)
                .Select(p=>p.ID +":" +  p.MeasureName)
                .ToArray();


            IList<SelectListItem> companySelectItems =
                this.service.Company
                .All()
                .Select(company => new SelectListItem() { Value = company.ID.ToString(), Text = company.CompName })
                .ToList();

            companySelectItems.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Empty, Selected = true });

            ViewBag.MeasureOptions =  String.Join(";", measureList);
            ViewBag.CompanySelectItems = companySelectItems;
            ViewBag.DType = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "0", Text = "混凝土" },
                    new SelectListItem(){ Value = "1", Text = "干混" },
                    new SelectListItem(){ Value = "2", Text = "湿拌" }};

            return base.Index();
        }
        /// <summary>
        /// 机组筒仓查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual ActionResult FindSilo(string pid)
        {
            ProductLine p = this.service.GetGenericService<ProductLine>().Get(pid);
            IList<SiloProductLine> siloList = p.SiloProductLines;
            JqGridData<SiloProductLine> data = new JqGridData<SiloProductLine>()
            {
                page = 1,
                records = siloList.Count,
                pageSize = siloList.Count,
                rows = siloList
            };
            return Json(data);
        }

        /// <summary>
        /// 更新筒仓排序、rate。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public virtual ActionResult UpdateSiloProductLine(SiloProductLine entity)
        {
            
                this.service.GetGenericService<SiloProductLine>().Update(entity, Request.Form);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
           
        }

        /// <summary>
        /// 分配筒仓
        /// </summary>
        /// <param name="pid">机组ID</param>
        /// <param name="sid">筒仓ID</param>
        /// <returns></returns>
        public virtual ActionResult AssignSilo(string pid,string sid)
        {
            try
            {
                this.service.ProductLine.AssignSilo(pid, sid);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, null);
            }
        }
        
        /// <summary>
        /// 获取可分配的筒仓
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual ActionResult FindAvailableSilo(string pid,int? type)
        {
            IList<Silo> queryList = this.service.ProductLine.FindSilo(pid,type); 
            JqGridData<Silo> data = new JqGridData<Silo>()
            {
                page = 1,
                records = queryList.Count,
                pageSize = queryList.Count,
                rows = queryList
            };
            return Json(data);
        } 


        /// <summary>
        /// GPS查找生产线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetProduceLinesByFactoryId()
        {
            var produceLines = from item in  this.m_ServiceBase.Query().Where(s =>s.IsUsed).OrderBy(s => s.OrderNum)
                               select new
                               {
                                   Id = item.ID,
                                   Name = item.ProductLineName
                               };
            return Json(produceLines);
        }

        public override ActionResult Add(ProductLine entity)
        {
            SysConfig SysConfig = base.service.SysConfig.GetSysConfig("IsOpenNC");
            if (SysConfig != null)
            {
                if (bool.Parse(SysConfig.ConfigValue) && (entity.CompanyID == 0 || entity.CompanyID == null))
                {
                    return OperateResult(false, "您设置了NC必填，请填写公司编码！", null);
                }
                if (bool.Parse(SysConfig.ConfigValue) && (entity.Org_Stockorg == "" || entity.Org_Stockorg == null))
                {
                    return OperateResult(false, "您设置了NC必填，请填写NC库存组织！", null);
                }
            }
            return base.Add(entity);
        }
    }
}
