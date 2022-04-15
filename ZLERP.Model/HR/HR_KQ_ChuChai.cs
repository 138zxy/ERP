using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated.HR;

namespace ZLERP.Model.HR
{
    public class HR_KQ_ChuChai : _HR_KQ_ChuChai
    {
        public virtual HR_Base_Personnel HR_Base_Personnel { get; set; }
        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }
        public virtual string Name
        {
            get
            {
                return HR_Base_Personnel.Name;
            }
        }
    }
}
