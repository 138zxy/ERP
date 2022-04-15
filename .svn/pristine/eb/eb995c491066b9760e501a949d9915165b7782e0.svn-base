using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.HR
{
    public class Search
    {
        [DisplayName("开始月份")]
        public virtual DateTime BeginMonth { get; set; }
        [DisplayName("结束月份")]
        public virtual DateTime EndMonth { get; set; }
        [DisplayName("开始时间")]
        public virtual DateTime BeginDate { get; set; }
        [DisplayName("结束时间")]
        public virtual DateTime EndDate { get; set; }
     
    }

    /// <summary>
    /// 计件工资统计视图
    /// </summary>
    public class HR_GZ_PieceAnalysis : EntityBase<string>
    {
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }


        [DisplayName("项目编码")]
        public virtual string Code { get; set; }

        [DisplayName("项目名称")]
        public virtual string Name { get; set; }


        [DisplayName("数量")]
        public virtual decimal Quantity { get; set; }

        [DisplayName("金额")]
        public virtual decimal AllMoney { get; set; }
        [DisplayName("占比")]
        public virtual decimal Proportion { get; set; }
       
    }
     
}
