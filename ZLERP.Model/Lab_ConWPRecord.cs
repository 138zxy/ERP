using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 混凝土取样及拌合物工作性能检测记录
    /// </summary>
    public class Lab_ConWPRecord : _Lab_ConWPRecord
    {
        public virtual Lab_ConWPRecordItems Lab_ConWPRecordItems
        {
            get;
            set;
        }
    }
}