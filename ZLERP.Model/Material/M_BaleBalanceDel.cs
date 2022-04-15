using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using ZLERP.Model.Generated.Material;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace ZLERP.Model.Material
{
    public class M_BaleBalanceDel : _M_BaleBalanceDel
    {
        public virtual StuffIn StuffIn { get; set; }
        [ScriptIgnore]
        public virtual M_BaleBalance M_BaleBalance { get; set; }
        
        public virtual string SpecName
        {
            get
            {
                return this.StuffinfoSpec == null ? "" : this.StuffinfoSpec.SpecName;
            }
        }
    }
}
