using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model.SupplyChain;

namespace ZLERP.Web.Controllers.SupplyChain
{
    public class SC_FixedController : BaseController<SC_Fixed, string>
    {

        public override ActionResult Index()
        {
            ViewBag.AddType = new SupplyChainHelp().GetAddType();
            //折旧方法
            List<SelectListItem> Depreciations = new List<SelectListItem>();
            Depreciations.Add(new SelectListItem { Text = SC_Common.Depreciation.DYear, Value = SC_Common.Depreciation.DYear });
            Depreciations.Add(new SelectListItem { Text = SC_Common.Depreciation.DAll, Value = SC_Common.Depreciation.DAll });
            Depreciations.Add(new SelectListItem { Text = SC_Common.Depreciation.Duble, Value = SC_Common.Depreciation.Duble });
            ViewBag.Depreciation = Depreciations;

            ViewBag.Users = new SupplyChainHelp().GetUser();
            ViewBag.Departments = new SupplyChainHelp().GetDepartment();


            return base.Index();
        }

        public ActionResult GenerateOrderNo()
        {
            string orderNo = string.Empty;
            int MaxID = GetMaxID();
            if (MaxID > 0)
            {
                string codeString = MaxID.ToString().PadLeft(6, '0');
                orderNo = "F" + codeString;
            }
            else
            {
                orderNo = "F000001";
            }
            return OperateResult(true, "订单号生成成功", orderNo);
        }

        private int GetMaxID()
        {
            var sc_fixed = this.m_ServiceBase.Query().OrderByDescending(t => t.ID).FirstOrDefault();
            if (sc_fixed != null)
            {
                var codeString = sc_fixed.ID;
                var code = Convert.ToInt32(codeString) + 1;
                return code;
            }
            return 0;
        }

        public override ActionResult Add(SC_Fixed SC_Fixed)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SC_Fixed.Ftype))
                {
                    return OperateResult(false, "请选择分类", null);
                }
                var type = this.service.GetGenericService<SC_GoodsType>().Get(SC_Fixed.Ftype.ToString());
                if (type == null)
                {
                    return OperateResult(false, "选择的分类不正确", null);
                }
                //SC_Fixed.TypeNo = type.TypeNo;
                //SC_Fixed.TypeName = type.TypeName;
                if (!SC_Fixed.IsBath)
                {
                    return base.Add(SC_Fixed);
                }
                else
                {
                    int BathNum = Convert.ToInt32(SC_Fixed.BathNum);
                    if (BathNum <= 0)
                    {
                        return OperateResult(false, "请输入正确的批量数", null);
                    }
                    int MaxID = GetMaxID();
                    int i = 0;
                    while (i < BathNum)
                    {
                        var Fixed = (SC_Fixed)SC_Fixed.Clone();
                        string orderNo = string.Empty;
                        if (MaxID > 0)
                        {
                            string codeString = MaxID.ToString().PadLeft(6, '0');
                            orderNo = "F" + codeString;
                        }
                        else
                        {
                            orderNo = "F000001";
                        }
                        Fixed.Fcode = orderNo;
                        base.Add(Fixed);
                        MaxID++;
                        i++;
                    }
                    return OperateResult(true, "批量新增成功", null);
                }
            }
            catch (Exception ex)
            {

                return OperateResult(false, ex.Message.ToString(), null);
            }
        }

        public override ActionResult Update(SC_Fixed SC_Fixed)
        {
            var re = base.Update(SC_Fixed);
            var fix = this.m_ServiceBase.Get(SC_Fixed.ID);
            //var type = this.service.GetGenericService<SC_GoodsType>().Get(SC_Fixed.Ftype.ToString());
            //if (type == null)
            //{
            //    return OperateResult(false, "选择的分类不正确", null);
            //}
            //fix.TypeNo = type.TypeNo;
            //fix.TypeName = type.TypeName;
           // this.m_ServiceBase.Update(fix, null);
            return re;
        }
        
        /// <summary>
        /// 获取拼音简码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public ActionResult LP_TOPY_SHORT(string str)
        {
            return new SupplyChainHelp().LP_TOPY_SHORT(str);
        }
        /// <summary>
        /// 获取固定资产列表
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            return new SupplyChainHelp().GetSC_FixedInfo(q, textField, valueField, orderBy, ascending, condition);
        }


        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Compute(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperateResult(false, "请选择单据", null);
            }
            this.service.SupplyChainService.UpdateFixedCondition(Convert.ToInt32(id));
            return OperateResult(true, "操作成功", null);
        }
    }
}
