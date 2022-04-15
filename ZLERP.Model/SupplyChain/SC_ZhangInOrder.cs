using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.SupplyChain
{
    public class SC_ZhangInOrder : Generated.SupplyChain._SC_ZhangInOrder
    {
        /// <summary>
        /// 金额（虚拟）
        /// </summary>
        [DisplayName("金额(元)")]
        public virtual decimal? ZhangMoney
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
        [DisplayName("已入数量")]
        public virtual decimal InQuantity
        {
            get
            {
                if (SC_PiaoInOrder != null)
                {
                    var query = from n in SC_PiaoInOrder.SC_PiaoIns.Where(t => t.Condition == SC_Common.InStatus.InLib).ToList()
                                select new
                                {
                                    Quantity = n.SC_ZhangIns.Where(t => t.GoodsID == this.GoodsID && t.Unit==this.Unit).Sum(t => t.Quantity)
                                };
                    return query.Sum(t => t.Quantity);
                }
                else
                {
                    return 0;
                }
            }
        }
        [DisplayName("待入数量")]
        public virtual decimal WaitQuantity
        {
            get
            {
                if (SC_PiaoInOrder != null)
                {
                    var query = from n in SC_PiaoInOrder.SC_PiaoIns.Where(t => t.Condition == SC_Common.InStatus.Ini).ToList()
                                select new
                                {
                                    Quantity = n.SC_ZhangIns.Where(t => t.GoodsID == GoodsID && t.Unit == this.Unit).Sum(t => t.Quantity)
                                };
                    return query.Sum(t => t.Quantity);
                }
                else {
                    return 0;
                }
            }
        }

        [DisplayName("未入数量")]
        public virtual decimal UnQuantity
        {
            get
            {
                return Quantity - InQuantity;
            }
        }
        public virtual SC_Goods SC_Goods { get; set; }

        /// <summary>
        /// 对应的采购单
        /// </summary>
        [ScriptIgnore]
        public virtual SC_PiaoInOrder SC_PiaoInOrder { get; set; }

        /// <summary>
        /// 辅助单位
        /// </summary>
        public virtual string AuxiliaryUnit
        {
            get
            {
                if (SC_Goods.IsAuxiliaryUnit)
                {
                    var num = this.Quantity;
                    var desc = "";
                    var list = SC_Goods.SC_GoodsUnits.OrderByDescending(t => t.Rate).ToList();
                    foreach (var q in list)
                    {
                        if (this.Unit != q.Unit)
                        {
                            if (num > q.Rate)
                            {
                                decimal b = num / (decimal)q.Rate;
                                num = num % q.Rate;
                                desc += b.ToString() + q.Unit;
                            }
                        }
                        else
                        {
                            if (num > 0)
                            {
                                desc += num.ToString() + this.Unit;
                            }
                            break;
                        }
                    }
                    return desc;
                }
                else
                {
                    return "";
                }

            }
        }
    }
}
