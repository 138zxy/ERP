using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated.HR
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _HR_GZ_SafeSet : EntityBase<string>
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

        #region Properties

        [DisplayName("社保套账")]
        public virtual int SetID { get; set; }

        [DisplayName("社保项目")]
        public virtual string ItemName { get; set; }

        [DisplayName("个人缴纳/元")]
        public virtual decimal PersonPay { get; set; }

        [DisplayName("备注")]
        public virtual string Remark { get; set; }
        #endregion
    }
}
