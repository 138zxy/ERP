using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.HR
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _HR_PM_Transfer : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(PersonID);
            sb.Append(PersonName);
            sb.Append(IsDept);
            sb.Append(OldDepartmentID);
            sb.Append(OldDepartmentName);
            sb.Append(NewDepartmentID);
            sb.Append(NewDepartmentName);
            sb.Append(IsPosition);
            sb.Append(OldPositionID);
            sb.Append(OldPosition);
            sb.Append(NewPositionID);
            sb.Append(NewPosition);
            sb.Append(IsPost);
            sb.Append(OldPostID);
            sb.Append(OldPostName);
            sb.Append(NewPostID);
            sb.Append(NewPostName);
            sb.Append(TransferDate);
            sb.Append(Operator);
            sb.Append(Reason);
            sb.Append(Delflag);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        /// <summary>
        /// 单据号
        /// </summary>
        [DisplayName("单据号")]
        [Required]
        public virtual string TransferNo
        {
            get;
            set;
        }
        /// <summary>
        /// 员工编码
        /// </summary>
        [DisplayName("员工编码")]
        [Required]
        public virtual int PersonID
        {
            get;
            set;
        }
        /// <summary>
        /// 人员名称
        /// </summary>
        [DisplayName("人员名称")]
        public virtual string PersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否部门调动
        /// </summary>
        [DisplayName("是否部门调动")]
        public virtual bool IsDept
        {
            get;
            set;
        }
        /// <summary>
        /// 原部门编码
        /// </summary>
        [DisplayName("原部门编码")]
        public virtual int OldDepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 原部门
        /// </summary>
        [DisplayName("原部门")]
        public virtual string OldDepartmentName
        {
            get;
            set;
        }
        /// <summary>
        /// 新部门ID
        /// </summary>
        [DisplayName("新部门ID")]
        public virtual int NewDepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 新部门
        /// </summary>
        [DisplayName("新部门")]
        public virtual string NewDepartmentName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否职务调动
        /// </summary>
        [DisplayName("是否职务调动")]
        public virtual bool IsPosition
        {
            get;
            set;
        }
        /// <summary>
        /// 原职务ID
        /// </summary>
        [DisplayName("原职务ID")]
        public virtual string OldPositionID
        {
            get;
            set;
        }
        /// <summary>
        /// 原职务
        /// </summary>
        [DisplayName("原职务")]
        public virtual string OldPosition
        {
            get;
            set;
        }
        /// <summary>
        /// 新职务ID
        /// </summary>
        [DisplayName("新职务ID")]
        public virtual string NewPositionID
        {
            get;
            set;
        }
        /// <summary>
        /// 现职务
        /// </summary>
        [DisplayName("新职务")]
        public virtual string NewPosition
        {
            get;
            set;
        }
        /// <summary>
        /// 是否调动岗位
        /// </summary>
        [DisplayName("是否调动岗位")]
        public virtual bool IsPost
        {
            get;
            set;
        }
        /// <summary>
        /// 原岗位编码
        /// </summary>
        [DisplayName("原岗位编码")]
        public virtual string OldPostID
        {
            get;
            set;
        }
        /// <summary>
        /// 原岗位名称
        /// </summary>
        [DisplayName("原岗位")]
        public virtual string OldPostName
        {
            get;
            set;
        }
        /// <summary>
        /// 新岗位编码
        /// </summary>
        [DisplayName("新岗位编码")]
        public virtual string NewPostID
        {
            get;
            set;
        }
        /// <summary>
        /// 新岗位名称
        /// </summary>
        [DisplayName("新岗位")]
        public virtual string NewPostName
        {
            get;
            set;
        }
        /// <summary>
        /// 调动时间
        /// </summary>
        [DisplayName("调动时间")]
        [Required]
        public virtual System.DateTime? TransferDate
        {
            get;
            set;
        }
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        public virtual string Operator
        {
            get;
            set;
        }
        /// <summary>
        /// 原因
        /// </summary>
        [DisplayName("原因")]
        public virtual string Reason
        {
            get;
            set;
        }
        /// <summary>
        /// 删除标识
        /// </summary>
        [DisplayName("删除标识")]
        public virtual bool Delflag
        {
            get;
            set;
        }


        [DisplayName("状态")]
        public virtual string State { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor { get; set; }

        #endregion
    }
}
