using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated.Material;
namespace ZLERP.Model.Material
{
    public class M_PiaoYingSFrec : _M_PiaoYingSFrec
    {

        public virtual SupplyInfo SupplyInfo { get; set; }

        public virtual string SupplyName
        {
            get
            {
                if (SupplyInfo != null)
                {
                    return SupplyInfo.SupplyName;
                }
                return "";
            }
        }
        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }
    }
}
