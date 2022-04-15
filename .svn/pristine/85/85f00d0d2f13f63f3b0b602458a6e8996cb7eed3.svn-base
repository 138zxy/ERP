using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.SupplyChain
{
    public class SC_PiaoIn : Generated.SupplyChain._SC_PiaoIn
    {

        public virtual SC_Supply SC_Supply { get; set; }

        public virtual SC_Lib SC_Lib { get; set; }
        [ScriptIgnore]
        public virtual IList<SC_ZhangIn> SC_ZhangIns { get; set; }

        public virtual SC_PiaoInOrder SC_PiaoInOrder { get; set; }
        #region 付款用的冗余字段

        [DisplayName("入库单号")]
        public virtual int PayInID { get; set; }

        [DisplayName("付款人")]
        [StringLength(50)]
        public virtual string Payer { get; set; }


        [DisplayName("收款人")]
        [StringLength(50)]
        public virtual string Gatheringer { get; set; }

        [StringLength(500)]
        [DisplayName("备注")]
        public virtual string Remark2 { get; set; }

        #endregion
        #region 应付管理使用的冗余字段
        [DisplayName("付款额")]
        public virtual decimal PayMoney2
        {
            get
            {
                return PayOwing;
            }
             
        }

        [DisplayName("优惠额")]
        public virtual decimal PayFavourable2
        {
            get
            {
                return 0;
            } 
        }
        #endregion
    }
}
