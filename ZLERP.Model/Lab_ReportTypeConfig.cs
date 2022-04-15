using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class Lab_ReportTypeConfig : _Lab_ReportTypeConfig
    {
        [DisplayName("材料类型")]
        public virtual string StuffTypeName
        {
            get
            {
                return StuffType == null ? string.Empty : StuffType.StuffTypeName;
            }
        }
    }
}
