﻿using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using ZLERP.Model.Beton;

namespace ZLERP.Model
{
    /// <summary>
    ///  运输单
    /// </summary>
    public class ShippingDocument : _ShippingDocument
    {

        [DisplayName("施工部位")]
        public override string ConsPos
        {
            get
            {
                return base.ConsPos;
            }
            set
            {
                base.ConsPos = value;
            }
        }

        //comment by:Sky
        //此处代码由邓海波注释，如无特别原因（某功能不能使用）
        //请不要恢复该代码，该处严重影响发货单页面效率，多出很多不必要的关联查询
        //date: 2012-12-10
        ///// <summary>
        ///// 生产任务
        ///// </summary>
        public virtual ProduceTask ProduceTask
        {
            get;
            set;
        }


        [DisplayName("其他要求")]
        public virtual string OtherDemand
        {
            get;
            set;
        }

        [DisplayName("理论配比名称")]
        public virtual string FormulaName
        {
            get;
            set;
        }

        [DisplayName("数据类型")]
        public virtual int? DataType
        {
            get;
            set;
        }


        #region  结算相关信息
        [ScriptIgnore]
        public virtual IList<B_BalanceDel> B_BalanceDels { get; set; }

        [ScriptIgnore]
        public virtual IList<B_TranBalanceDel> B_TranBalanceDels { get; set; }

        [DisplayName("砼款已结算")]
        public virtual bool IsAccount
        {
            get
            {
                if (B_BalanceDels != null && B_BalanceDels.Count > 0)
                {
                    foreach (var b in B_BalanceDels)
                    {
                        if (b.ModelType == ZLERP.Model.Beton.B_Common.ModelType.Beton)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

        }

        [DisplayName("砼款结算单")]
        public virtual string AccountBaleNo
        {
            get
            {
                if (B_BalanceDels != null && B_BalanceDels.Count > 0)
                {
                    foreach (var b in B_BalanceDels)
                    {
                        if (b.ModelType == ZLERP.Model.Beton.B_Common.ModelType.Beton)
                        {
                            return b.B_Balance.BaleNo;
                        }
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
                if (B_TranBalanceDels != null && B_TranBalanceDels.Count > 0)
                {
                    foreach (var b in B_TranBalanceDels)
                    {
                        if (b.ModelType == ZLERP.Model.Beton.B_Common.ModelType.JbCar)
                        {
                            return b.B_TranBalance.BaleNo;
                        }
                    }

                }
                return "";
            }

        }

        [DisplayName("运费已结算")]
        public virtual bool IsTrans
        {
            get
            {
                if (B_TranBalanceDels != null && B_TranBalanceDels.Count > 0)
                {
                    foreach (var b in B_TranBalanceDels)
                    {
                        if (b.ModelType == ZLERP.Model.Beton.B_Common.ModelType.JbCar)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }


        [DisplayName("泵车已结算")]
        public virtual bool IsPonAccount
        {
            get
            {
                if (B_TranBalanceDels != null && B_TranBalanceDels.Count > 0)
                {
                    foreach (var b in B_TranBalanceDels)
                    {
                        if (b.ModelType == ZLERP.Model.Beton.B_Common.ModelType.PeCar)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

        }


        [DisplayName("泵送结算单")]
        public virtual string AccountPonBaleNo
        {
            get
            {
                if (B_TranBalanceDels != null && B_TranBalanceDels.Count > 0)
                {
                    foreach (var b in B_TranBalanceDels)
                    {
                        if (b.ModelType == ZLERP.Model.Beton.B_Common.ModelType.PeCar)
                        {
                            return b.B_TranBalance.BaleNo;
                        }
                    }

                }
                return "";
            }
        }
        #endregion

        /// <summary>
        /// 车辆信息
        /// </summary>
        public virtual Car Car { get; set; }


        /// <summary>
        /// 是否质检
        /// </summary>
        public virtual bool IsQualityInspected { get; set; }

        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
            set;
        }
        [DisplayName("结算备注")]
        public virtual string RemarkJS
        {
            get;
            set;
        }
        [DisplayName("运输补量")]
        public virtual decimal y_ExtraMoney
        {
            get;
            set;
        }
        [DisplayName("运输其他费用")]
        public virtual decimal y_OtherMoney
        {
            get;
            set;
        }
        [DisplayName("运费备注")]
        public virtual string y_Remark
        {
            get;
            set;
        }
    }
}