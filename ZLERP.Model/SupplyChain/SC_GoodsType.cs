using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.SupplyChain;

namespace ZLERP.Model.SupplyChain
{
    public class SC_GoodsType : _SC_GoodsType, ITreeGridable
    {
        public virtual string TypeNameAll { get; set; }

        #region ITreeGridable 成员

        public virtual int level
        {
            get
            {
                return this.DptLevel;
            }
        }

        public virtual string parent
        {
            get
            {
                return this.ParentID.ToString();
            }
        }

        public virtual bool leaf
        {
            get
            {
                return this.IsLeaf;
            }
        }

        public virtual bool expanded
        {
            get
            {
                return false;
            }
        }

        public virtual bool loaded
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
