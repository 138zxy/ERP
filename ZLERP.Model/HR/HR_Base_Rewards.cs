﻿using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ZLERP.Model.Generated.HR;
namespace ZLERP.Model.HR
{
    /// <summary>
    /// 人事管理-奖惩
    /// </summary>
    public class HR_Base_Rewards : _HR_Base_Rewards
    {
        public virtual HR_Base_Personnel HR_Base_Personnel { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public virtual string DepartmentName
        {
            get { return HR_Base_Personnel == null ? "" : HR_Base_Personnel.Department.DepartmentName; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        public virtual string TrueName
        {
            get { return HR_Base_Personnel == null ? "" : HR_Base_Personnel.Name; }
        }

        /// <summary>
        /// 是否发送至通知公告
        /// </summary>
        [DisplayName("是否发送至通知公告")]
        public virtual bool IsNotice
        {
            get;
            set;
        }

        /// <summary>
        /// 通知公告编号
        /// </summary>
        [DisplayName("通知公告编号")]
        public virtual int? NoticeID
        {
            get;
            set;
        }

        /// <summary>
        /// 批准时间
        /// </summary>
        [DisplayName("批准时间")]
        public virtual DateTime? ApproveDate
        {
            get;
            set;
        }

        /// <summary>
        /// 批准人
        /// </summary>
        [DisplayName("批准人")]
        public virtual string ApproveMan
        {
            get;
            set;
        }

        /// <summary>
        /// 撤销时间
        /// </summary>
        [DisplayName("撤销时间")]
        public virtual DateTime? RevocationDate
        {
            get;
            set;
        }

        /// <summary>
        /// 撤销原由
        /// </summary>
        [DisplayName("撤销原由")]
        [StringLength(256)]
        public virtual string RevocationReason
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public virtual bool IsEffective
        {
            get;
            set;
        }
	}
}