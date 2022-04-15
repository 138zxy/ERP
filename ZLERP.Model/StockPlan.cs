using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Linq;
namespace ZLERP.Model
{
    /// <summary>
    ///  采购计划
    /// </summary>
    public class StockPlan : _StockPlan
    {

        [ScriptIgnore]
        public virtual IList<StuffIn> StuffIns { get; set; }
        [DisplayName("已入数量")]
        public virtual decimal? InNum
        {
            get
            {
                if (StuffIns != null && StuffIns.Count > 0)
                {
                    return StuffIns.Sum(t => t.FinalFootNum);
                   
                }
                return 0;
            }
        }
        [DisplayName("未入数量")]
        public virtual decimal? UnNum
        {
            get
            {
                return PlanAmount - InNum;
            }
        }

        [DisplayName("累计车数")]
        public virtual decimal? CarNum
        {
            get
            {
                if (StuffIns != null && StuffIns.Count > 0)
                {
                    return StuffIns.Count();

                }
                return 0;
            }
        }

        [DisplayName("合同名称")]
        public virtual string PactName
        {
            get
            {
                if (StockPact != null)
                {
                    return StockPact.PactName;
                }
                return "";
            }
        }

        [DisplayName("供货厂商")]
        public virtual string SupplyName
        {
            get
            {
                if (StockPact != null)
                {
                    return StockPact.SupplyName;
                }
                return "";
            }
        }
        //[DisplayName("运输公司")]
        //public virtual string TransportName
        //{
        //    get
        //    {
        //        if (TransInfo != null)
        //        {
        //            return TransInfo.SupplyName;
        //        }
        //        return "";
        //    }
        //}

        public virtual StockPact StockPact
        {
            get;
            set;
        }
        public virtual SupplyInfo TransInfo
        {
            get;
            set;
        }

        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }
        public virtual Silo Silo
        {
            get;
            set;
        }


    }
}