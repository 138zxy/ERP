using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.HR.Enums
{
    public class BaseDataType
    {
        public static string 性别 = "性别";
        public static string 结婚状态 = "结婚状态";
        public static string 所学专业 = "所学专业";
        public static string 社保状态 = "社保状态";
        public static string 用工形式 = "用工形式";
        public static string 人员状态 = "人员状态";
        public static string 学历 = "学历";
        public static string 岗位 = "岗位";
        public static string 职务 = "职务";
        public static string 政治面貌 = "政治面貌";
        public static string 职称 = " 职称";
        public static string 民族 = "民族";
        public static string 人才等级 = "人才等级";
        public static string 籍贯 = "籍贯";
        public static string 职称取得方式 = "职称取得方式";
        public static string 证照类型 = "证照类型";
        public static string 发证机构 = " 发证机构";
        public static string 合同年份 = "合同年份";
        public static string 合同类型 = "合同类型";
        public static string 合同解除类别 = "合同解除类别";
        public static string 调动类型 = "调动类型";
        public static string 离职类型 = "离职类型";
        public static string 复职类型 = "复职类型";
        public static string 开户行 = "开户行";

        public static string 奖惩处理类别 = "奖惩处理类型";
        public static string 奖惩方式 = "奖惩方式";

        public static string 请假类型 = "请假类型";
        public static string 加班类型 = "加班类型";
        public static string 假期类型 = "假期类型";
        public static string 出差类型 = "出差类型";

        public static string 扣除项目 = "扣除项目";
        public static string 补贴项目 = "补贴项目"; 

    }
    public class PersonState
    {
        /// <summary>
        /// 在职
        /// </summary>
        public static string InPost = "在职";
        /// <summary>
        /// 离职
        /// </summary>
        public static string OutPost = "离职";

    }
    public class RecordState
    {
        /// <summary>
        /// 草稿
        /// </summary>
        public static string Draft = "草稿";
        /// <summary>
        /// 已审核
        /// </summary>
        public static string Audit = "已审核";

    }

    public class PCondition
    {
        /// <summary>
        /// 草稿
        /// </summary>
        public static string Ini = "草稿";
        /// <summary>
        /// 已审核
        /// </summary>
        public static string Audit = "已审核";
         
    }
}
