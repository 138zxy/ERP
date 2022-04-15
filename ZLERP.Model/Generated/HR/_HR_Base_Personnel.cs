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
    public abstract class _HR_Base_Personnel : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(JobNo);
            sb.Append(Name);
            sb.Append(Code);
            sb.Append(PyCode);
            sb.Append(Sex);
            sb.Append(Nation);
            sb.Append(Marry);
            sb.Append(Birthday);
            sb.Append(PostDate);
            sb.Append(RecordSchool);
            sb.Append(School);
            sb.Append(Profession);
            sb.Append(GraduateDate);
            sb.Append(SocialState);
            sb.Append(Address);
            sb.Append(IDno);
            sb.Append(IDAddress);
            sb.Append(Telphone);
            sb.Append(CellPhone);
            sb.Append(UrgentPerson);
            sb.Append(PositionType);
            sb.Append(Post);
            sb.Append(MountGuardProperty);
            sb.Append(DepartmentID);
            sb.Append(Meno);
            sb.Append(State);
            sb.Append(EmploymentForm);
            sb.Append(Email);
            sb.Append(Vita);
            sb.Append(CorrectionDate);
            sb.Append(ArchivesNo);
            sb.Append(BankNo);
            sb.Append(SchoolRecordNo);
            sb.Append(PhotoPath);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        [StringLength(50)]
        public virtual string JobNo
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [StringLength(30)]
        [Required]
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        [DisplayName("员工编号")]
        [StringLength(20)]
        public virtual string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 拼音码
        /// </summary>
        [DisplayName("拼音码")]
        [StringLength(20)]
        public virtual string PyCode
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        [StringLength(50)]
        public virtual string Sex
        {
            get;
            set;
        }
        /// <summary>
        /// 民族
        /// </summary>
        [DisplayName("民族")]
        [StringLength(20)]
        public virtual string Nation
        {
            get;
            set;
        }
        /// <summary>
        /// 结婚状态：0未婚 1已婚 2未知
        /// </summary>
        [DisplayName("结婚状态")]
        public virtual string Marry
        {
            get;
            set;
        }
        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public virtual System.DateTime? Birthday
        {
            get;
            set;
        }
        /// <summary>
        /// 入职时间
        /// </summary>
        [DisplayName("入职时间")]
        public virtual System.DateTime? PostDate
        {
            get;
            set;
        }
        /// <summary>
        /// 学历
        /// </summary>
        [DisplayName("学历")]
        [StringLength(30)]
        public virtual string RecordSchool
        {
            get;
            set;
        }
        /// <summary>
        /// 毕业学校
        /// </summary>
        [DisplayName("毕业学校")]
        [StringLength(30)]
        public virtual string School
        {
            get;
            set;
        }
        /// <summary>
        /// 所学专业
        /// </summary>
        [DisplayName("所学专业")]
        [StringLength(30)]
        public virtual string Profession
        {
            get;
            set;
        }
        /// <summary>
        /// 毕业时间
        /// </summary>
        [DisplayName("毕业时间")]
        [StringLength(30)]
        public virtual DateTime? GraduateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 社保状态
        /// </summary>
        [DisplayName("社保状态")]
        [StringLength(10)]
        public virtual string SocialState
        {
            get;
            set;
        }
        /// <summary>
        /// 现住地址
        /// </summary>
        [DisplayName("现住地址")]
        [StringLength(250)]
        public virtual string Address
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        [StringLength(50)]
        public virtual string IDno
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证地址
        /// </summary>
        [DisplayName("身份证地址")]
        [StringLength(250)]
        public virtual string IDAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        [DisplayName("家庭电话")]
        [StringLength(20)]
        public virtual string Telphone
        {
            get;
            set;
        }
        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        [StringLength(20)]
        public virtual string CellPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 紧急联系人
        /// </summary>
        [DisplayName("紧急联系人")]
        [StringLength(30)]
        public virtual string UrgentPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        [StringLength(20)]
        public virtual string PositionType
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位
        /// </summary>
        [DisplayName("岗位")]
        [StringLength(20)]
        public virtual string Post
        {
            get;
            set;
        }
        /// <summary>
        /// 上岗性质
        /// </summary>
        [DisplayName("上岗性质")]
        [StringLength(20)]
        public virtual string MountGuardProperty
        {
            get;
            set;
        }
        /// <summary>
        /// 部门编码
        /// </summary>
        [DisplayName("部门")]
        public virtual int DepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public virtual string Meno
        {
            get;
            set;
        }
        /// <summary>
        /// 人员状态：在职、离职等
        /// </summary>
        [DisplayName("人员状态")]
        [Required]
        public virtual string State
        {
            get;
            set;
        }
        /// <summary>
        /// 用工形式
        /// </summary>
        [DisplayName("用工形式")]
        [StringLength(20)]
        public virtual string EmploymentForm
        {
            get;
            set;
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [DisplayName("电子邮件")]
        [StringLength(30)]
        public virtual string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 个人简历
        /// </summary>
        [DisplayName("个人简历")]
        public virtual string Vita
        {
            get;
            set;
        }
        /// <summary>
        /// 转正日期
        /// </summary>
        [DisplayName("转正日期")]
        public virtual DateTime? CorrectionDate
        {
            get;
            set;
        }
        /// <summary>
        /// 档案号
        /// </summary>
        [DisplayName("档案号")]
        public virtual string ArchivesNo
        {
            get;
            set;
        }
        /// <summary>
        /// 银行卡号
        /// </summary>
        [DisplayName("银行卡号")]
        [StringLength(40)]
        public virtual string BankNo
        {
            get;
            set;
        }
        /// <summary>
        /// 学历证号
        /// </summary>
        [StringLength(40)]
        [DisplayName("学历证号")]
        public virtual string SchoolRecordNo
        {
            get;
            set;
        }
        /// <summary>
        /// 照片
        /// </summary>
        [DisplayName("照片")]
        [StringLength(150)]
        public virtual string PhotoPath
        {
            get;
            set;
        }
        #endregion
    }
}
