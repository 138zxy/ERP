using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    public class ContractLimit : EntityBase<string>
    {
        public override int GetHashCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().ToString())
                .Append(this.ID)
                .Append(this.ContractID)
                .Append(this.IsEnabled)
                .Append(this.LimitType)
                .Append(this.WarnValue)
                .Append(this.LimitValue);


            return builder.ToString().GetHashCode();

        }

        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        public virtual string ContractID { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// 限制类型
        /// </summary>
        [DisplayName("限制类型")]
        public virtual string LimitType { get; set; }

        /// <summary>
        /// 预警值
        /// </summary>
        [DisplayName("预警值")]
        public virtual string WarnValue { get; set; }

        /// <summary>
        /// 限制值
        /// </summary>
        [DisplayName("限制值")]
        public virtual string LimitValue { get; set; }

        /// <summary>
        /// 预警监测信息
        /// </summary>
        [DisplayName("预警监测信息")]
        public virtual string CheckWarnMessage { get; set; }

        /// <summary>
        /// 限制监测信息
        /// </summary>
        [DisplayName("限制监测信息")]
        public virtual string CheckLimitMessage { get; set; }


        [DisplayName("关系")]
        public virtual string Relation { get; set; }
        [DisplayName("是否达到预警")]
        public virtual bool IsWarn { get; set; }
        [DisplayName("是否达到限制")]
        public virtual bool IsLimit { get; set; }
        [DisplayName("关系")]
        public virtual string CurrentValue { get; set; }
        /// <summary>
        /// 合同信息
        /// </summary>
        [ScriptIgnore]
        public virtual Contract Contract { get; set; }
    }
}
