using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    public class Organization : _Organization, ITreeGridable
    {

        public virtual string ParentOrganizationName
        {
            get
            {
                return ParentOrganization != null ? ParentOrganization.OrganizationName : string.Empty;
            }
        }


        public virtual Organization ParentOrganization { get; set; }

        #region ITreeGridable

        public virtual int level
        {
            get { return base.OrganizationLevel; }
        }

        public virtual string parent
        {
            get { return base.ParentId; }
        }

        public virtual bool leaf
        {
            get { return base.IsLeaf; }
        }

        public virtual bool expanded
        {
            get { return false; }
        }

        public virtual bool loaded
        {
            get { return false; }
        }

        #endregion 
    }




}
