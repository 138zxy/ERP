using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.SupplyChain
{
    public class SC_Fixed_Deprecia : Generated.SupplyChain._SC_Fixed_Deprecia
    {
        #region 查询条件

        [DisplayName("截止年月：")]
        public virtual DateTime SearchMonth { get; set; }
        [DisplayName("包括已清理")]
        public virtual bool IsClear { get; set; }

        public virtual string AnalysisMonth2
        {
            get {
                return base.AnalysisMonth.ToString("yyyy-MM");
            }
            set{}
        }
        public virtual string ActualMonth2
        {
            get
            {
                return base.ActualMonth.ToString("yyyy-MM");
            }
            set{}
        }
        #endregion
    }
}
