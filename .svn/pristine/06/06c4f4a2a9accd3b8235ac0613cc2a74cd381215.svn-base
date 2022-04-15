using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.HR;
using System.ComponentModel;

namespace ZLERP.Model.HR
{
    public class HR_KQ_PaiBan : _HR_KQ_PaiBan
    {
        public virtual HR_KQ_Arrange HR_KQ_Arrange { get; set; }

        public virtual HR_Base_Personnel HR_Base_Personnel { get; set; }

        public virtual string ArrangeName
        {
            get
            {
                if (HR_KQ_Arrange == null)
                {
                    return "";
                }
                return HR_KQ_Arrange.ArrangeName;
            }
        }

        public virtual string Name
        {
            get
            {
                if (HR_Base_Personnel == null)
                {
                    return "";
                }
                return HR_Base_Personnel.Name;
            }
        }

        #region 以下字段为 虚体字段，批量排班用的参数

        [DisplayName("为已选部门所有员工排班(如选全部部门将为全部部门排班)")]
        public virtual bool IsSelectDepart { get; set; }

        [DisplayName("班次选择")]
        public virtual int ArraggeShowID { get; set; }

        [DisplayName("取消排班(用于对节假日等特殊情况批量取消排班)")]
        public virtual bool IsCancel { get; set; }
        [DisplayName("开始日期")]
        public virtual DateTime StartPai { get; set; }
        [DisplayName("截止日期")]
        public virtual DateTime EndPai { get; set; }
        [DisplayName("星期一")]
        public virtual bool Monday { get; set; }
        [DisplayName("星期二")]
        public virtual bool Tuesday { get; set; }
        [DisplayName("星期三")]
        public virtual bool Wednesday { get; set; }
        [DisplayName("星期四")]
        public virtual bool Thursday { get; set; }
        [DisplayName("星期五")]
        public virtual bool Friday { get; set; }
        [DisplayName("星期六")]
        public virtual bool Saturday { get; set; }
        [DisplayName("星期天")]
        public virtual bool Sunday { get; set; }

        [DisplayName("星期交替排班(每隔一个星期进行排班)")]
        public virtual bool IsWeekAlternate { get; set; }
        [DisplayName("星期开始")]
        public virtual int StartWeek { get; set; }

        [DisplayName("隔天数交替排班(每隔一个天数进行排班)")]
        public virtual bool IsDayAlternate { get; set; }
        [DisplayName("相隔天数")]
        public virtual int AlternateDay { get; set; }

        #endregion
    }
}
