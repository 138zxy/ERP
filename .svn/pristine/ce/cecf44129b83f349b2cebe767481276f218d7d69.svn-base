using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.SupplyChain
{
    public class SC_ZhangOutOrder : Generated.SupplyChain._SC_ZhangOutOrder
    {


        [DisplayName("已出数量")]
        public virtual decimal InQuantity
        {
            get
            {
                if (SC_PiaoOutOrder != null)
                {
                    if (SC_PiaoOutOrder.SC_PiaoOuts != null)
                    {
                        var query = from n in SC_PiaoOutOrder.SC_PiaoOuts.Where(t => t.Condition == SC_Common.InStatus.OutLib).ToList()
                                    select new
                                    {
                                        Quantity = n.SC_ZhangOuts.Where(t => t.GoodsID == this.GoodsID && t.Unit == this.Unit).Sum(t => t.Quantity)
                                    };
                        return query.Sum(t => t.Quantity);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
        [DisplayName("待出数量")]
        public virtual decimal WaitQuantity
        {
            get
            {
                if (SC_PiaoOutOrder != null)
                {
                    if (SC_PiaoOutOrder.SC_PiaoOuts != null)
                    {
                        var query = from n in SC_PiaoOutOrder.SC_PiaoOuts.Where(t => t.Condition == SC_Common.InStatus.Ini).ToList()
                                    select new
                                    {
                                        Quantity = n.SC_ZhangOuts.Where(t => t.GoodsID == GoodsID && t.Unit == this.Unit).Sum(t => t.Quantity)
                                    };
                        return query.Sum(t => t.Quantity);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        [DisplayName("未出数量")]
        public virtual decimal UnQuantity
        {
            get
            {
                return Quantity - InQuantity;
            }
        }
        public virtual SC_Goods SC_Goods { get; set; }

        /// <summary>
        /// 对应的申请单
        /// </summary>
        [ScriptIgnore]
        public virtual SC_PiaoOutOrder SC_PiaoOutOrder { get; set; }

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
