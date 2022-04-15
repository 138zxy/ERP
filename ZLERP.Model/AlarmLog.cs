using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
namespace ZLERP.Model
{
    /// <summary>
    /// 附件:各功能上传的附件文件
    /// </summary>
    public class AlarmLog : _Alarmlog
    {
        /// <summary>
        /// GPS设备号
        /// </summary>
        public virtual string SN
        {
            get { return this.Car == null ? "" : this.Car.TerminalID; }
        }

    }
}