using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.SupplyChain
{
    public class Search
    {
        [DisplayName("开始月份")]
        public virtual DateTime BeginMonth { get; set; }
        [DisplayName("结束月份")]
        public virtual DateTime EndMonth { get; set; }
        [DisplayName("开始时间")]
        public virtual DateTime BeginDate { get; set; }
        [DisplayName("结束时间")]
        public virtual DateTime EndDate { get; set; }
        [DisplayName("商品名称")]
        public virtual string GoodsName { get; set; }
        [DisplayName("仓库")]
        public virtual string LibName { get; set; }
        [DisplayName("仓库")]
        public virtual string LibName2 { get; set; }
        [DisplayName("商品名称")]
        public virtual string GoodsID { get; set; }

        [DisplayName("使用人")]
        public virtual string Userid { get; set; }
    }

    /// <summary>
    /// 采购统计视图
    /// </summary>
    public class PurReport : EntityBase<string>
    {
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        [DisplayName("品名")]
        public virtual string GoodsName { get; set; }

        [DisplayName("规格")]
        public virtual string Spec { get; set; }

        [DisplayName("单位")]
        public virtual string Unit { get; set; }

        [DisplayName("供应商")]
        public virtual string SupplierName { get; set; }

        [DisplayName("采购数量")]
        public virtual decimal PurNum { get; set; }

        [DisplayName("采购额")]
        public virtual decimal PurMoney { get; set; }
    }

    /// <summary>
    /// 领料视图
    /// </summary>
    public class UseReport
    {

        [DisplayName("品名")]
        public virtual string GoodsName { get; set; }

        [DisplayName("规格")]
        public virtual string Spec { get; set; }

        [DisplayName("单位")]
        public virtual string Unit { get; set; }

        [DisplayName("使用人")]
        public virtual string UserID { get; set; }

        [DisplayName("出库数量")]
        public virtual decimal OutNum { get; set; }

        [DisplayName("出库额")]
        public virtual decimal OutMoney { get; set; }
    }


    /// <summary>
    ///  付款视图
    /// </summary>
    public class V_YingSFrec
    {

        [DisplayName("供应商")]
        public virtual int UnitID { get; set; }


        [DisplayName("日期")]
        public virtual DateTime FinanceDate { get; set; }


        [DisplayName("单据号")]
        public virtual string FinanceNo { get; set; }

        [DisplayName("记录类型")]
        public virtual string Source { get; set; }

        [DisplayName("付款方式")]
        public virtual string PayType { get; set; }


        [DisplayName("预付额")]
        public virtual decimal FinanceMoney { get; set; }

        [DisplayName("预付付款额")]
        public virtual decimal YFinanceMoney { get; set; }

        [DisplayName("优惠额")]
        public virtual decimal PayFavourable { get; set; }
    }

    /// <summary>
    /// 商品明细账视图
    /// </summary>
    public class V_SC_GoodsReport : EntityBase<string>
    {
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        public virtual string TypeString { get; set; }
        public virtual int GoodsID { get; set; }
        [DisplayName("日期")]
        public virtual DateTime? ReportDate { get; set; }
        [DisplayName("单号")]
        public virtual string OrderNo { get; set; }
        [DisplayName("摘要")]
        public virtual string OrderType { get; set; }
        [DisplayName("品名")]
        public virtual string GoodsName { get; set; }
        [DisplayName("规格")]
        public virtual string Spec { get; set; }
        [DisplayName("单位")]
        public virtual string Unit { get; set; }
        [DisplayName("条码")]
        public virtual string GoodsNo { get; set; }
        [DisplayName("品牌")]
        public virtual string Brand { get; set; }
        [DisplayName("数量")]
        public virtual int? InQuantity { get; set; }
        [DisplayName("单价")]
        public virtual decimal? InPriceUnit { get; set; }
        [DisplayName("金额")]
        public virtual decimal? InMoney { get; set; }
        [DisplayName("数量")]
        public virtual int? OutQuantity { get; set; }
        [DisplayName("单价")]
        public virtual decimal? OutPriceUnit { get; set; }
        [DisplayName("金额")]
        public virtual decimal? OutMoney { get; set; }
        [DisplayName("剩余数量")]
        public virtual decimal? RemainderQuantity { get; set; }
        [DisplayName("剩余金额")]
        public virtual decimal? RemainderMoney { get; set; }
        [DisplayName("单据号")]
        public virtual string PiaoNo { get; set; }
    }

    /// <summary>
    /// 进销存统计
    /// </summary>
    public class V_SC_jxcMonthReport
    {

        public virtual string MonthLy { get; set; }

        public virtual decimal InQuantity { get; set; }

        public virtual decimal OutQuantity { get; set; }

        public virtual decimal LibQuantity { get; set; }

        public virtual decimal InMoney { get; set; }

        public virtual decimal OutMoney { get; set; }

        public virtual decimal LibMoney { get; set; }

    }


    /// <summary>
    /// 进销存明细查询
    /// </summary>
    /// 
    public class V_SC_jxcDetailReport
    {
        [DisplayName("分类")]
        public virtual string TypeName { get; set; }


        [DisplayName("品名")]
        public virtual string GoodsName { get; set; }
        [DisplayName("规格")]
        public virtual string Spec { get; set; }
        [DisplayName("单位")]
        public virtual string Unit { get; set; }
        [DisplayName("条码")]
        public virtual string GoodsNo { get; set; }
        [DisplayName("品牌")]
        public virtual string Brand { get; set; }


        [DisplayName("期初数量")]
        public virtual decimal? IniQuantity { get; set; }

        [DisplayName("期初金额")]
        public virtual decimal? IniMoney { get; set; }

        [DisplayName("本期购进数量")]
        public virtual decimal? InQuantity { get; set; }

        [DisplayName("本期购进金额")]
        public virtual decimal? InMoney { get; set; }

        [DisplayName("本期消耗数量")]
        public virtual decimal? OutQuantity { get; set; }

        [DisplayName("本期消耗金额")]
        public virtual decimal? OutMoney { get; set; }

        [DisplayName("本期拆分数量")]
        public virtual decimal? ChangeQuantity { get; set; }

        [DisplayName("本期拆分金额")]
        public virtual decimal? ChangeMoney { get; set; }

        [DisplayName("本期库存数量")]
        public virtual decimal? LibQuantity { get; set; }

        [DisplayName("本期库存金额")]
        public virtual decimal? LibMoney { get; set; }

    }

    /// <summary>
    /// 单品查询
    /// </summary>
    public class V_SC_jxcGoodsReport
    {
        [DisplayName("商品名称")]
        public virtual string GoodsName { get; set; }

        [DisplayName("日期")]
        public virtual DateTime OrderDate { get; set; }

        /// <summary>
        /// 增减方式
        /// </summary>
        public virtual string Reason { get; set; }

        /// <summary>
        /// 供应商或者部门
        /// </summary>
        public virtual string UnitName { get; set; }

        /// <summary>
        /// 曾建数量
        /// </summary>
        public virtual decimal OrderNum { get; set; }

        /// <summary>
        /// 当时的价格
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 库存结余
        /// </summary>
        public virtual decimal LibQuantity { get; set; }


        public virtual string Operater { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public virtual string LibName { get; set; }


    }
    #region 加工管理提交模型


    public class PiaoChangeModel
    {
        public int id { get; set; }

        public int lib { get; set; }

        public string remark { get; set; }

        public List<Change1> records3 { get; set; }

        public List<Change2> records4 { get; set; }
    }
    public class Change1
    {
        public int GoodsID { get; set; }

        public decimal Quantity { get; set; }

        public string LibID { get; set; }

        public int lib { get; set; }

    }
    public class Change2
    {
        public int GoodsID { get; set; }

        public decimal Quantity { get; set; }

        public decimal Pirce { get; set; }

    }

 
    #endregion
}
