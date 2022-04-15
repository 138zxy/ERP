using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated.SupplyChain
{


    public abstract class _SC_Fixed_Circulate : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.GetType().FullName);
            sb.Append(ID); 
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }
        #endregion
        [DisplayName("资产号")]
        public virtual int FixedID { get; set; }

        [DisplayName("流水号")]
        [Required]
        [StringLength(50)]
        public virtual string CirculateNo { get; set; }

        [DisplayName("资产编号")]
        [StringLength(50)]
        public virtual string Fcode { get; set; }

        [DisplayName("资产名称")]
        [StringLength(50)]
        public virtual string Fname { get; set; }


        [DisplayName("借用部门")]
        [StringLength(50)]
        public virtual string BorrowDepart { get; set; }


        [DisplayName("借用人")]
        [StringLength(50)]
        public virtual string BorrowMan { get; set; }



        [DisplayName("借出日期")]
        public virtual DateTime? BorrowDate { get; set; }

        [DisplayName("拟还日期")]
        public virtual DateTime? MayBackDate { get; set; }


        [DisplayName("批准人")]
        [StringLength(50)]
        public virtual string ApproveMan { get; set; }


        [DisplayName("借出备注")]
        [StringLength(500)]
        public virtual string BorrowRemark { get; set; }

        [DisplayName("归还日期")]
        public virtual DateTime? BackDate { get; set; }


        [DisplayName("归还备注")]
        [StringLength(500)]
        public virtual string BackRemark { get; set; }

        [DisplayName("是否归还")]
        public virtual bool IsBack { get; set; }


        [DisplayName("资产所属部门")]
        [StringLength(50)]
        public virtual string DepartMent { get; set; }


        [DisplayName("资产条形码")]
        [StringLength(50)]
        public virtual string BarCode { get; set; }

        [DisplayName("资产拼音简码")]
        [StringLength(50)]
        public virtual string BrevityCode { get; set; }
    }
}
  


