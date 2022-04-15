using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model.Material;
namespace ZLERP.Model
{
    /// <summary>
    /// 运输商单价设定
    /// </summary>
	public class TransPrice : _TransPrice
    {


        [Required, Display(Name = "运输商")]
        public virtual string TransportID { get; set; }

        [Required, Display(Name = "供货商")]
        public virtual string SupplyID { get; set; }


        [Display(Name = "原料来源地")]
        public virtual string SourceID { get; set; }


        [Required, Display(Name = "原材料")]
        public virtual string StuffID { get; set; }


        public virtual string TransportName {
            get {
                if (SupplyInfo != null)
                {
                    return TransportInfo.SupplyName;
                }
                return "";
            }
        }

        public virtual string SupplyName
        {
            get
            {
                if (SupplyInfo != null)
                {
                    return SupplyInfo.SupplyName;
                }
                return "";
            }
        }

        public virtual string SourceName
        {
            get
            {
                if (SourceInfo != null)
                {
                    return SourceInfo.SupplyName;
                }
                return "";
            }
        }
        public virtual string StuffName
        {
            get
            {
                if (StuffInfo != null)
                {
                    return StuffInfo.StuffName;
                }
                return "";
            }
        }
	}
}