using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated.Beton;
using ZLERP.Model.Beton;
namespace ZLERP.Model.Beton
{
    public class B_TranYingSFrecdetail :  _B_TranYingSFrecdetail
    {

        public virtual B_TranBalance B_TranBalance { get; set; }
    }
}
