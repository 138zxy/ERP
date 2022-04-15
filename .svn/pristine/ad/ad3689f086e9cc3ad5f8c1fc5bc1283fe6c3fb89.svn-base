using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.SupplyChain;

namespace ZLERP.Model.SupplyChain
{
    public class SC_Fixed_Shift : _SC_Fixed_Shift
    {
        public virtual SC_GoodsType SC_GoodsType { get; set; }
        public virtual SC_GoodsType SC_GoodsTypeNew { get; set; }
       
        /// <summary>
        /// 原分类
        /// </summary>
        public virtual string FTypeName
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
        /// 新分类
        /// </summary>
        public virtual string FTypeNameNew
        {
            get
            {
                if (SC_GoodsTypeNew != null)
                {
                    return SC_GoodsTypeNew.TypeName;
                }
                return "";
            }
        }
    }
}
