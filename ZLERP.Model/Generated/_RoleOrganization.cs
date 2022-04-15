
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model.Beton; 

namespace ZLERP.Model.Generated
{
    public class _RoleOrganization : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(RoleOrganizationID);
            sb.Append(RoleID);
            sb.Append(OrganizationID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        /// <summary>
        /// RoleOrganizationID
        /// </summary>
        public virtual int RoleOrganizationID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户编号  
        /// </summary>
        public virtual string RoleID
        {
            get;
            set;
        }
        /// <summary>
        /// 组织编号
        /// </summary>
        public virtual string OrganizationID
        {
            get;
            set;
        }   
    }
}
