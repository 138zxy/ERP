﻿using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  采购合同
    /// </summary>
    public class StockPact : _StockPact
    {
        [Required, DisplayName("供应商")]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }


        public virtual IList<StockPactPriceSet> StockPactPriceSets
        {
            get;
            set;
        }

        //public virtual StockPactChild StockPactChild
        //{
        //    get;
        //    set;
        //}

        //public virtual StockPactChildChild StockPactChildChild
        //{
        //    get;
        //    set;
        //}
        [DisplayName("供货厂商")]
        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }
        [Required, DisplayName("原材料-1")]
        public virtual string StuffID
        {
            get;
            set;
        }

        [DisplayName("原材料-2")]
        public virtual string StuffID1
        {
            get;
            set;
        }

        [DisplayName("原材料-3")]
        public virtual string StuffID2
        {
            get;
            set;
        }
        [DisplayName("原材料-4")]
        public virtual string StuffID3
        {
            get;
            set;
        }

        [DisplayName("原材料-5")]
        public virtual string StuffID4
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

        public virtual string StuffName1
        {
            get
            {
                return this.StuffInfo1 == null ? "" : this.StuffInfo1.StuffName;
            }
        }

        public virtual string StuffName2
        {
            get
            {
                return this.StuffInfo2 == null ? "" : this.StuffInfo2.StuffName;
            }
        }
        public virtual string StuffName3
        {
            get
            {
                return this.StuffInfo3 == null ? "" : this.StuffInfo3.StuffName;
            }
        }

        public virtual string StuffName4
        {
            get
            {
                return this.StuffInfo4 == null ? "" : this.StuffInfo4.StuffName;
            }
        }

        #region 规格

        [DisplayName("规格-1")]
        public virtual string SpecID
        {
            get;
            set;
        }

        [DisplayName("规格-2")]
        public virtual string SpecID1
        {
            get;
            set;
        }

        [DisplayName("规格-3")]
        public virtual string SpecID2
        {
            get;
            set;
        }
        [DisplayName("规格-4")]
        public virtual string SpecID3
        {
            get;
            set;
        }

        [DisplayName("规格-5")]
        public virtual string SpecID4
        {
            get;
            set;
        }
        public virtual string SpecName
        {
            get
            {
                return this.StuffinfoSpec == null ? "" : this.StuffinfoSpec.SpecName;
            }
        }

        public virtual string SpecName1
        {
            get
            {
                return this.StuffinfoSpec1 == null ? "" : this.StuffinfoSpec1.SpecName;
            }
        }

        public virtual string SpecName2
        {
            get
            {
                return this.StuffinfoSpec2 == null ? "" : this.StuffinfoSpec2.SpecName;
            }
        }
        public virtual string SpecName3
        {
            get
            {
                return this.StuffinfoSpec3 == null ? "" : this.StuffinfoSpec3.SpecName;
            }
        }

        public virtual string SpecName4
        {
            get
            {
                return this.StuffinfoSpec4 == null ? "" : this.StuffinfoSpec4.SpecName;
            }
        }
        #endregion


        [DisplayName("期初欠款")]
        public virtual decimal baseMoney
        {
            get;
            set;
        }

        #region 付款需要的辅助字段
        public virtual decimal PaidFavourable
        {
            get
            {
                return this.SupplyInfo == null ? 0 : this.SupplyInfo.PaidFavourable;
            }
        }
        public virtual decimal PaidIn
        {
            get
            {
                return this.SupplyInfo == null ? 0 : this.SupplyInfo.PaidIn;
            }
        }
        public virtual decimal PaidOut
        {
            get
            {
                return this.SupplyInfo == null ? 0 : this.SupplyInfo.PaidOut;
            }
        }
        public virtual decimal PaidOwing
        {
            get
            {
                return this.SupplyInfo == null ? 0 : this.SupplyInfo.PaidOwing;
            }
        }
        public virtual decimal PayMoney
        {
            get
            {
                return this.SupplyInfo == null ? 0 : this.SupplyInfo.PayMoney;
            }
        }
        public virtual decimal PrePay
        {
            get
            {
                return this.SupplyInfo == null ? 0 : this.SupplyInfo.PrePay;
            }
        }

        #endregion
    }
}