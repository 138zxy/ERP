using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.HR
{
    public class V_HR_KQ_Result
    {
        public HR_KQ_Result HR_KQ_Result { get; set; }

        public HR_KQ_ResultMain HR_KQ_ResultMain { get; set; }

        #region 查询统计字段

        [DisplayName("为已选部门所有员工排班(如选全部部门将为全部部门排班)")]
        public virtual bool IsSelectDepart { get; set; }
        [DisplayName("开始日期")]
        public virtual DateTime StartR { get; set; }
        [DisplayName("截止日期")]
        public virtual DateTime EndR { get; set; }
        #endregion
    }

}
