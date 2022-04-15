using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace ZLERP.Model.SupplyChain
{
    public class SC_PiaoTo : Generated.SupplyChain._SC_PiaoTo
    {
        /// <summary>
        /// 调出
        /// </summary>
        public virtual SC_Lib SC_LibOut { get; set; }

        /// <summary>
        /// 调入
        /// </summary>
        public virtual SC_Lib SC_LibIn { get; set; }

        [ScriptIgnore]
        public virtual IList<SC_ZhangTo> SC_ZhangTos { get; set; }
    }
}
