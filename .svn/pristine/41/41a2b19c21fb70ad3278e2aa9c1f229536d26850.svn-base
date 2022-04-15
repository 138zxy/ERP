using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.SupplyChain
{
    public class SC_ZhangTo : Generated.SupplyChain._SC_ZhangTo
    {
        public virtual SC_Goods SC_Goods { get; set; }

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
                                int b = num / q.Rate;
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
