using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.SupplyChain
{
    public class SC_NowLib : Generated.SupplyChain._SC_NowLib
    {
        public virtual SC_Supply SC_Supply { get; set; }

        public virtual SC_Lib SC_Lib { get; set; }

        public virtual SC_Goods SC_Goods { get; set; }

        /// <summary>
        /// 库存总金额
        /// </summary>
        public virtual decimal LibMoney
        {
            get
            {
                return this.Quantity * this.PirceUnit;
            }
        }
        /// <summary>
        /// 低于预警值
        /// </summary>
        public virtual string Warning { get; set; }

        /// <summary>
        /// 高于预警值
        /// </summary>
        public virtual string UpWarning { get; set; }
        /// <summary>
        /// 赋值单位
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
                        if (this.SC_Goods.Unit != q.Unit)
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
                                desc += num.ToString() + this.SC_Goods.Unit;
                            }
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
