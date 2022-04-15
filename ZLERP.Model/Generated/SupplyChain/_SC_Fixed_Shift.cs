using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SC_Fixed_Shift : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ShiftNo);
            sb.Append(FixedID);
            sb.Append(Fcode);
            sb.Append(Fname);
            sb.Append(DepartMent);
            sb.Append(Storeman);
            sb.Append(Position);
            sb.Append(ShiftDate);
            sb.Append(ShiftMan);
            sb.Append(StoremanNew);
            sb.Append(PositionNew);
            sb.Append(ApproveMan);
            sb.Append(Remark);
            sb.Append(BarCode);
            sb.Append(BrevityCode);
            sb.Append(DepartMentNew);
            sb.Append(Ftype);
            sb.Append(FtypeNew);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 流水号
        /// </summary>
        [DisplayName("流水号")]
        [StringLength(50)]
        [Required]
        public virtual string ShiftNo
        {
            get;
            set;
        }
        /// <summary>
        /// 资产编号
        /// </summary>
        [DisplayName("资产编号")]
        public virtual int? FixedID
        {
            get;
            set;
        }
        /// <summary>
        /// 资产编码
        /// </summary>
        [DisplayName("资产编码")]
        [StringLength(50)]
        public virtual string Fcode
        {
            get;
            set;
        }
        /// <summary>
        /// 资产名称
        /// </summary>
        [DisplayName("资产名称")]
        [StringLength(50)]
        public virtual string Fname
        {
            get;
            set;
        }
        /// <summary>
        /// 原使用部门
        /// </summary>
        [DisplayName("原使用部门")]
        [StringLength(50)]
        public virtual string DepartMent
        {
            get;
            set;
        }
        /// <summary>
        /// 原保管员
        /// </summary>
        [DisplayName("原保管员")]
        [StringLength(50)]
        public virtual string Storeman
        {
            get;
            set;
        }
        /// <summary>
        /// 原存放位置
        /// </summary>
        [DisplayName("原存放位置")]
        [StringLength(50)]
        public virtual string Position
        {
            get;
            set;
        }
        /// <summary>
        /// 转移日期
        /// </summary>
        [DisplayName("转移日期")]
        [Required]
        public virtual System.DateTime? ShiftDate
        {
            get;
            set;
        }
        /// <summary>
        /// 转移人
        /// </summary>
        [DisplayName("转移人")]
        [StringLength(50)]
        public virtual string ShiftMan
        {
            get;
            set;
        }
        /// <summary>
        /// 新保管员
        /// </summary>
        [DisplayName("新保管员")]
        [StringLength(50)]
        public virtual string StoremanNew
        {
            get;
            set;
        }
        /// <summary>
        /// 新存放位置
        /// </summary>
        [DisplayName("新存放位置")]
        [StringLength(50)]
        public virtual string PositionNew
        {
            get;
            set;
        }
        /// <summary>
        /// 批准人
        /// </summary>
        [DisplayName("批准人")]
        [StringLength(50)]
        public virtual string ApproveMan
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(16)]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 资产条形码
        /// </summary>
        [DisplayName("资产条形码")]
        [StringLength(50)]
        public virtual string BarCode
        {
            get;
            set;
        }
        /// <summary>
        /// 资产拼音简码
        /// </summary>
        [DisplayName("资产拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode
        {
            get;
            set;
        }
        /// <summary>
        /// 新使用部门
        /// </summary>
        [DisplayName("新使用部门")]
        [StringLength(50)]
        public virtual string DepartMentNew
        {
            get;
            set;
        }
        /// <summary>
        /// 原分类号
        /// </summary>
        [DisplayName("原分类号")]
        [StringLength(50)]
        public virtual string Ftype
        {
            get;
            set;
        }
        /// <summary>
        /// 新分类号
        /// </summary>
        [DisplayName("新分类号")]
        [Required]
        public virtual string FtypeNew
        {
            get;
            set;
        }
        #endregion
    }
}
