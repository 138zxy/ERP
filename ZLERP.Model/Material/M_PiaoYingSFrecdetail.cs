using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated.Material;
using ZLERP.Model.Material;
namespace ZLERP.Model.Material
{
    public class M_PiaoYingSFrecdetail : _M_PiaoYingSFrecdetail
    {

        public virtual M_BaleBalance M_BaleBalance { get; set; }
    }
}
