using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    /// <summary>
    /// 试验室模板
    /// </summary>
	public class Lab_Template : _Lab_Template
    {
        public virtual Lab_TemplateDataConfig Lab_TemplateDataConfig { get; set; }

        /// <summary>
        /// 模板标示 1 原材料试验模板 2 混泥土试验模板 
        /// </summary>
        public virtual string TemplateFlag
        {
            get
            {
                return this.Dic == null ? "" : this.Dic.Field2;
            }
        }
	}

}
