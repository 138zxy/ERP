using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public class  _ContractPumpPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName)
              .Append(this.ID)
              .Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 合同泵送编号
        /// </summary>
        [DisplayName("合同泵送编号")]
        public virtual int ContractPumpID
        {
            get;
            set;
        }
        /// <summary>
        /// 泵送单价
        /// </summary>
        [DisplayName("泵送单价")]
        public virtual decimal PumpPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 改价时间
        /// </summary>
        [DisplayName("改价时间")]
        public virtual decimal? ChangeTime
        {
            get;
            set;
        }
        
        [ScriptIgnore]
        public virtual ContractPump ContractPump
        {
            get;
            set;
        }


        #endregion
    }
}
