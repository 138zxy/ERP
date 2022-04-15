using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    public static class ContractLimitType
    {
        /// <summary>
        /// 方量控制，WarnValue和LimitValue为Decimal类型
        /// </summary>
        public static readonly string LimitCube = "LimitCube";

        /// <summary>
        /// 金额控制，WarnValue和LimitValue为Decimal类型
        /// </summary>
        public static readonly string LimitMoney = "LimitMoney";

        /// <summary>
        /// 时间控制，WarnValue和LimitValue为Datetime类型
        /// </summary>
        public static readonly string LimitTime = "LimitTime";

        /// <summary>
        /// 垫资控制，WarnValue和LimitValue为Decimal类型
        /// </summary>
        public static readonly string LimitLoan = "LimitLoan";

        /// <summary>
        /// 预付款控制，WarnValue和LimitValue为Decimal类型
        /// </summary>
        public static readonly string LimitPrepay = "LimitPrepay"; 
    }

}
