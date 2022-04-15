using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated.Beton;
using ZLERP.Model.Beton;
namespace ZLERP.Model.Beton
{
    public class B_YingSFrecdetail : _B_YingSFrecdetail
    {

        public virtual B_Balance B_Balance { get; set; }
    }
}
