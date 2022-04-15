using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model
{
    public class GpsInfo
    {
        public int id { get; set; }//标识ID
        public string terminalid { get; set; }//设备ID
        public string cartype { get; set; } //车辆标示0徐工搅拌车、1中联搅拌车、2中联泵车、3中联车载泵、4中联砂浆背罐车
        public double? longtidue { get; set; }//已转换为mapbar的经度
        public double? latitude { get; set; }//已转换为mapbar的纬度

        public double? originlongtidue { get; set; }//原始经度
        public double? originlatitude { get; set; }//原始纬度

        public double? speed { get; set; } //速度
        public double? direction { get; set; }//方向(角度)
        public double? oil { get; set; }//油量
        public double? distance { get; set; }//总里程

        public byte? unload { get; set; }//卸料（正反转状态）0:停止1：正转 2：反转

        public DateTime? unloadtime { get; set; }//卸料时间

        public int? place { get; set; }//车辆所处位置和状态（0：未知 1：休息 2：养护 3：待命 4：出厂5：工地6：回厂 31进站进料）

        public int? errorcode { get; set; }//错误编码

        public DateTime? receivetime { get; set; }//接收时间

        /// <summary>
        /// ACC标志，1－点火状态；0－熄火状态。
        /// </summary>
        public string accflag { get; set; }

        /// <summary>
        /// 搅拌车状态。0：熄火，1：搅拌，2：卸料，3：停止，4：未知  (正反转状态)
        /// </summary>
        public string status { get; set; }

        public string rmp { get; set; }//发动机转速 转/分
        public string rmj { get; set; }//正反转转速 转/分

        /// <summary>
        /// 上电总时间(分钟)
        /// </summary>
        public double? acconcount { get; set; }

        public double Weight { get; set; }
    }
}
