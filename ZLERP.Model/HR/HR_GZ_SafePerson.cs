using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.HR;

namespace ZLERP.Model.HR
{
    public class HR_GZ_SafePerson : _HR_GZ_SafePerson
    {
        public virtual HR_Base_Personnel HR_Base_Personnel { get; set; }
        public virtual HR_GZ_Safe HR_GZ_Safe { get; set; }

        public virtual string Name
        {
            get
            {
                return HR_Base_Personnel.Name;
            }
        }
    }
}
