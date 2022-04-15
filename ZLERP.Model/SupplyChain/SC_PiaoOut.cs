using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model.CarManage;

namespace ZLERP.Model.SupplyChain
{
    public class SC_PiaoOut : Generated.SupplyChain._SC_PiaoOut
    {


        public virtual SC_Lib SC_Lib { get; set; }
        [ScriptIgnore]
        public virtual IList<SC_ZhangOut> SC_ZhangOuts { get; set; }
        public virtual Department Department { get; set; }

        public virtual SC_PiaoOutOrder SC_PiaoOutOrder { get; set; }
        public virtual SC_Fixed_Maintain SC_Fixed_Maintain { get; set; }
        public virtual CarMaintain CarMaintain { get; set; }

        public virtual string DepartmentName
        {
            get
            {
                if (Department == null)
                {
                    return SC_Common.OutType.OtherIn;
                }
                else
                {
                    return Department.DepartmentName;
                }
            }
            set { }
        }

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
    }
}
