using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  车辆信息
    /// </summary>
	public class Car : _Car
    {
        [DisplayName("数据类型")]
        public virtual int? DataType { get; set; }

        [Required]
        [Display(Name = "车辆代号")]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        [Required]
        public override string CarNo
        {
            get
            {
                return base.CarNo;
            }
            set
            {
                base.CarNo = value;
            }
        }
        [Required]
        public override string CarTypeID
        {
            get
            {
                return base.CarTypeID;
            }
            set
            {
                base.CarTypeID = value;
            }
        }

        /// <summary>
        /// 车种
        /// </summary>
        [DisplayName("车种")]
        public virtual string CarClassID
        {
            get;
            set;
        }

        public virtual string CarClassName
        {
            get { return this.CarClass == null ? "" : this.CarClass.CarClassName; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName
        {
            get { return this.Company == null ? "" : this.Company.CompName; }
        }

        /// <summary>
        /// 车队名称
        /// </summary>
        public virtual string FleetName
        {
            get { return this.B_CarFleet == null ? "" : this.B_CarFleet.FleetName; }
        }

        /// <summary>
        /// 公司编号
        /// </summary>
        [DisplayName("所属公司")]
        [Required]
        public virtual int CompanyID
        {
            get;
            set;
        }

        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }

        /// <summary>
        /// 显示车辆的第一张图片
        /// </summary>
        public virtual string PonthUrl
        {
            get
            {
                if (Attachments != null && Attachments.Count > 0)
                {
                    foreach (var t in Attachments)
                    {
                        if (t.FileType.ToLower() == ".png" || t.FileType.ToLower() == ".ipg")
                        {
                            return t.FileUrl;
                        }
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 燃油类型
        /// </summary>
        public virtual string OilTypeName
        {
            get
            {
                if (StuffType != null)
                {
                    return StuffType.StuffTypeName;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 视频设备编码
        /// </summary>
        [DisplayName("视频设备编码")]
        public virtual string VideoDeviceGuid
        {
            get;
            set;
        }










	}
}