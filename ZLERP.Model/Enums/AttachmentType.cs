using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 附件类型
    /// </summary>
    public enum AttachmentType
    {
        /// <summary>
        /// 通知公告附件
        /// </summary>
        Notice,
        /// <summary>
        /// 合同附件
        /// </summary>
        Contract,
        /// <summary>
        /// 系统消息附件
        /// </summary>
        SysMsg,
        /// <summary>
        /// 原材料合同附件
        /// </summary>
        StockPact,
        /// <summary>
        /// 资产文件
        /// </summary>
        SC_Fixed,

        /// <summary>
        /// 资产项目文件
        /// </summary>
        SC_Fixed_Del,

        /// <summary>
        /// 商品文件
        /// </summary>
        SC_Goods,
        /// <summary>
        /// 请假附件
        /// </summary>
        HR_KQ_Leave,

        /// <summary>
        /// 出差附件
        /// </summary>
        HR_KQ_ChuChai,

        /// <summary>
        /// 加班附件
        /// </summary>
        HR_KQ_Over,
        /// <summary>
        /// 工资管理部分
        /// </summary>
        HR_GZ_Piece,
        HR_GZ_Timing,
        HR_GZ_Deduct,
        /// <summary>
        /// 车辆文件
        /// </summary>
        Car,
        /// <summary>
        /// 发票文件
        /// </summary>
        B_PiaoYingSFrec,
        /// <summary>
        /// 发票文件
        /// </summary>
        M_PiaoYingSFrec,
        /// <summary>
        /// 工地计划附件
        /// </summary>
        CustomerPlan,
        /// <summary>
        /// 生产任务附件
        /// </summary>
        ProduceTask,


    }
}
