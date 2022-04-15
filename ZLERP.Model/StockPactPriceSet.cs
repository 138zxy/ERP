using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class StockPactPriceSet : _StockPactPriceSet
    {
        public virtual StuffInfo StuffInfo { get; set; }


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
        //[DisplayName("规格-1")]
        //public virtual string SpecID
        //{
        //    get;
        //    set;
        //}
        public virtual string SpecName
        {
            get
            {
                return this.StuffinfoSpec == null ? "" : this.StuffinfoSpec.SpecName;
            }
        }
    }
}
