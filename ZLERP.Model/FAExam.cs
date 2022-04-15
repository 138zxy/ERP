﻿using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 细骨料物性试验
    /// </summary>
	public class FAExam : _FAExam
    {
        [DisplayName("原料")]
        public virtual string StuffID
        {
            get;
            set;
        }

        [DisplayName("委托单号")]
        public virtual string CommissionID
        {
            get;
            set;
        }
        public virtual string StuffName
        {
            get { return StuffInfo == null ? string.Empty : StuffInfo.StuffName; }
        }
	}
}