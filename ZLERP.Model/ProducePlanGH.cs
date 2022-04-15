using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产计划表
    /// </summary>
    public class ProducePlanGH : _ProducePlanGH
    {
        [Display(Name = "任务单")]
        public virtual string TaskID
        {
            get;
            set;
        }

        public virtual string ProjectName
        {
            get { return this.ProduceTaskGH == null ? "" : this.ProduceTaskGH.ProjectName; }
        }

        public virtual string ConStrength
        {
            get { return this.ProduceTaskGH == null ? "" : this.ProduceTaskGH.ConStrength; }
        }

        public virtual string ConsPos
        {
            get { return this.ProduceTaskGH == null ? "" : this.ProduceTaskGH.ConsPos; }
        }

        public virtual DateTime? PlanDay
        {
            get { return this.PlanDate.Value.Date; }
        }

        public virtual DateTime? OpenTime
        {
            get { return this.ProduceTaskGH == null ? null : this.ProduceTaskGH.OpenTime; }
        }

    }
}