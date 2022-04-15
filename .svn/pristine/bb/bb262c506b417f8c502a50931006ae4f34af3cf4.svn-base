using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.HR
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _HR_KQ_Set : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(DeviceType);  
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("设备型号")] 
        [StringLength(50)]
        public virtual string DeviceType { get; set; }

        [DisplayName("通讯方式")] 
        [StringLength(50)]
        public virtual string Dispatch { get; set; }

        [DisplayName("IP地址")]
        [StringLength(50)]
        public virtual string IP { get; set; }

        [DisplayName("串口号")]
        [StringLength(50)]
        public virtual string SerialPort { get; set; }

        [DisplayName("通讯密码")]
        [StringLength(50)]
        public virtual string DispatchPass { get; set; }

        [DisplayName("迟到所多少分钟算旷工")] 
        public virtual int?  LateMinuteKG { get; set; }

        [DisplayName("早退所多少分钟算旷工")]
        public virtual int? LeaveMinuteKG { get; set; }

        [DisplayName("端口号")]
        [StringLength(50)]
        public virtual string PortNo { get; set; }

        [DisplayName("机器号")]
        [StringLength(50)]
        public virtual string Machine { get; set; }

        [DisplayName("波特率")]
        [StringLength(50)]
        public virtual string BoteLV { get; set; }


        [DisplayName("旷工处罚倍数")] 
        public virtual decimal? KGRate { get; set; }


        [DisplayName("当月总免迟到分钟")] 
        public virtual int? MonthLateMinute { get; set; }
        #endregion
    }
}
