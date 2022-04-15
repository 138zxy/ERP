using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace ZLERP.Model.SupplyChain
{
    public class SC_Goods : Generated.SupplyChain._SC_Goods
    {
        public virtual SC_GoodsType SC_GoodsType { get; set; }
  
        public virtual IList<SC_GoodsUnit> SC_GoodsUnits { get; set; }

        public virtual SC_GoodsUnit SC_GoodsUnit { get; set; }
        [ScriptIgnore]
        public virtual IList<SC_NowLib> SC_NowLibs { get; set; }
        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }
        public virtual string TypeName
        {
            get
            {
                if (SC_GoodsType != null)
                {
                    return SC_GoodsType.TypeName;
                }
                return "";
            }
        }

        /// <summary>
        /// 库存总数量
        /// </summary>
        [DisplayName("库存总数量")]
        public virtual decimal AllNum
        {
            get
            {
                if (SC_NowLibs != null && SC_NowLibs.Count > 0)
                {
                    return SC_NowLibs.Sum(t => t.Quantity);
                }
                return 0;
            }
        }

        /// <summary>
        /// 库存价格
        /// </summary>
        public virtual decimal LibPrice
        {
            get
            {
                if (SC_NowLibs != null && SC_NowLibs.Count > 0)
                {
                    return SC_NowLibs.First().PirceUnit;
                }
                return 0;
            }
        }
    }
}
