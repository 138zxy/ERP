using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.HR;

namespace ZLERP.Model.HR
{
    public class HR_Base_Personnel : _HR_Base_Personnel
    {
        public virtual Department Department { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string DepartmentName
        {
            get
            {
                if (Department != null)
                {
                    return Department.DepartmentName;
                }
                return "";
            }
        }
    }
}
