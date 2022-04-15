using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    public class RoleOrganization : _RoleOrganization
    {
        public virtual Organization Organization { get; set; }
        public virtual Role Role { get; set; }
    }
}
