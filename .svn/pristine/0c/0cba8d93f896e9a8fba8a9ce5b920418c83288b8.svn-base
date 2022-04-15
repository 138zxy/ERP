using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    ///  机组信息
    /// </summary>
	public class ProductLine : _ProductLine
    {
        /// <summary>
        /// 机组ID
        /// </summary>
        [Required]
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
        /// <summary>
        /// 盘容量
        /// </summary>
        [Required]
        public override decimal? DishNum
        {
            get
            {
                return base.DishNum;
            }
            set
            {
                base.DishNum = value;
            }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        //[Required]
        public virtual string CompName
        {
            get { return this.Company == null ? "" : this.Company.CompName; }
        }

        /// <summary>
        /// 生产线名称
        /// </summary>
        [Required]
        public override string ProductLineName
        {
            get
            {
                return base.ProductLineName;
            }
            set
            {
                base.ProductLineName = value;
            }
        }

        [ScriptIgnore]
        public virtual IList<SiloProductLine> SiloProductLines
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Silo> Silos
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<ConsMixprop> ConsMixprops
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Company Company
        {
            get;
            set;
        }
	}
}