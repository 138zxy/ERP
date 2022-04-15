using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.SupplyChain
{
    public class V_SC_GongYi
    {
        public SC_GongYi SC_GongYi { get; set; }
        public SC_GongYiDetail SC_GongYiDetail { get; set; }


        public SC_PiaoChange SC_PiaoChange { get; set; }

        [DisplayName("入仓库")]
        public string LibName { get; set; }
        [DisplayName("加工数量")]
        public int CountNum { get; set; }
         
    }
}
