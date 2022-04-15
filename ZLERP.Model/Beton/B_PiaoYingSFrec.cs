using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated.Beton;
namespace ZLERP.Model.Beton
{
    public class B_PiaoYingSFrec : _B_PiaoYingSFrec
    {

        public virtual Contract Contract { get; set; }
        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }
    }
}
