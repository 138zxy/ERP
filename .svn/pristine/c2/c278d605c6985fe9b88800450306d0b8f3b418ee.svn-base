using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.SupplyChain
{
    public class SC_Fixed : Generated.SupplyChain._SC_Fixed
    {
        public virtual SC_GoodsType SC_GoodsType { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
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
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }
        [DisplayName("是否批量录入")]
        public virtual bool IsBath { get; set; }
        [DisplayName("数量")]
        public virtual int? BathNum { get; set; }
    }
}
