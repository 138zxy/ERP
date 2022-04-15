﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.Material
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _M_TranBalanceDel : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(ID);
            sb.Append(BaleBalanceID); 
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        [DisplayName("主体ID")] 
        public virtual int BaleBalanceID { get; set; }

        [DisplayName("进料单号")] 
        public virtual string StuffInID { get; set; }


        [DisplayName("单价")]
        public virtual decimal Price { get; set; }

        [DisplayName("其他费用")]
        public virtual decimal OtherMoney { get; set; }

        [DisplayName("总价")]
        public virtual decimal AllMoney { get; set; }


        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        #endregion
    }
}
