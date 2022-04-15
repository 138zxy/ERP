﻿using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model.Material;
using System.Web.Script.Serialization;
using System.Linq; 
namespace ZLERP.Model
{
    /// <summary>
    ///  原料入库记录
    /// </summary>
    public class StuffIn : _StuffIn
    {

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
        /// <summary>
        /// 结算数量来源
        /// </summary>
        [DisplayName("结算数量来源")]
        public virtual int? FootFrom
        {
            get;
            set;
        }
        [DisplayName("入库原料"), Required]
        public virtual string StuffID
        {
            get;
            set;
        }
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }
        [DisplayName("入库筒仓"), Required]
        public virtual string SiloID
        {
            get;
            set;
        }
        public virtual string SiloName
        {
            get;
            set;
        }
        [DisplayName("价格编号")]
        public virtual string StockPactID
        {
            get;
            set;
        }
        public virtual string StockPactNo
        {
            get
            {
                return this.StockPact == null ? "" : this.StockPact.StockPactNo;
            }
        }
        [DisplayName("供货厂商"), Required]
        public virtual string SupplyID
        {
            get;
            set;
        }

        [DisplayName("采购公司")]
        public virtual int? CompanyID
        {
            get;
            set;
        }
        [DisplayName("毛重图片1")]
        public virtual string pic1
        {
            get;
            set;
        }

        [DisplayName("毛重图片2")]
        public virtual string pic2
        {
            get;
            set;
        }

        [DisplayName("毛重图片3")]
        public virtual string pic3
        {
            get;
            set;
        }

        [DisplayName("毛重图片4")]
        public virtual string pic4
        {
            get;
            set;
        }

        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }

        public virtual string CompName
        {
            get
            {
                return this.Company == null ? "" : this.Company.CompName;
            }
        }

        [DisplayName("运输公司")]
        public virtual string TransportID
        {
            get;
            set;
        }

        public virtual string TransportName
        {
            get
            {
                return this.TransportInfo == null ? "" : this.TransportInfo.SupplyName;
            }
        }

        [DisplayName("是否委托试验")]
        public virtual bool IsExam
        {
            get;
            set;
        }
        /// <summary>
        /// 按照一吨多少方折算
        /// </summary>
        [DisplayName("折算方量")]
        public virtual decimal? Volume
        {
            get;
            set;
            //get
            //{
            //    return FinalFootNum * Proportion / 1000;
            //}

        }
        public virtual string SpecName
        {
            get
            {
                return this.StuffinfoSpec == null ? "" : this.StuffinfoSpec.SpecName;
            }
        }

        [DisplayName("规格")]
        public virtual string Spec { get; set; }
        [ScriptIgnore]
        public virtual IList<M_BaleBalanceDel> M_BaleBalanceDels { get; set; }

        [ScriptIgnore]
        public virtual IList<M_TranBalanceDel> M_TranBalanceDels { get; set; }

        [DisplayName("货款是否结算")]
        public virtual bool IsAccount
        {
            get
            {
                if (M_BaleBalanceDels != null && M_BaleBalanceDels.Count>0)
                {
                    return true;
                }
                return false;
            }

        }

        [DisplayName("货款结算单")]
        public virtual string AccountBaleNo
        {
            get
            {
                if (M_BaleBalanceDels != null && M_BaleBalanceDels.Count > 0)
                {
                    if (M_BaleBalanceDels[0].M_BaleBalance != null)
                    {
                        return M_BaleBalanceDels[0].M_BaleBalance.BaleNo;
                    }
                }
                return "";
            }

        }
        [DisplayName("运费结算单")]
        public virtual string AccountTranNo
        {
            get
            {
                if (M_TranBalanceDels != null && M_TranBalanceDels.Count > 0)
                {
                    if (M_TranBalanceDels[0].M_TranBalance != null)
                    {
                        return M_TranBalanceDels[0].M_TranBalance.BaleNo;
                    }
                }
                return "";
            }

        }

        [DisplayName("运费是否结算")]
        public virtual bool IsTrans
        {
            get
            {
                if (M_TranBalanceDels != null && M_TranBalanceDels.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// 运输方式
        /// </summary>
        [DisplayName("运输方式")]
        public virtual string TransportType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否质检
        /// </summary>
        [DisplayName("是否质检")]
        public virtual bool IsQualityInspected { get; set; }


        public virtual string SourceName
        {
            get
            {
                return this.SourceInfo == null ? "" : this.SourceInfo.SupplyName;
            }
        }


    }




    /// <summary>
    /// InNum验证信息
    /// 便于学习使用
    /// </summary>
    public class InNumValidationAttribute :System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public InNumValidationAttribute()
            : base()
        {

        }
        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            object obj = validationContext.ObjectInstance;

            StuffIn newobj = (StuffIn)obj;
            if (newobj.InNum > newobj.TotalNum - newobj.CarWeight)
                return new ValidationResult(validationContext.DisplayName + "的数量大于了净重值！");

            return ValidationResult.Success;
        }

       
    }



}