using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;

namespace ZLERP.Model
{

    /// <summary>
    /// 实体类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public class SynNC : _SynNC
    {
        public virtual string Result
        {
            get
            {
                if (this.SynInfoNCs != null && this.SynInfoNCs.Count > 0)
                {
                    SynInfoNC SynInfoNC = this.SynInfoNCs.Where(m => m.SynNCID==ID).OrderByDescending(m => m.BuildTime).FirstOrDefault();
                    if (SynInfoNC!=null)
                    {
                        string InfoNC = "异常信息:";
                        int index = SynInfoNC.NcResultDescription.IndexOf(InfoNC);
                        string result = SynInfoNC.NcResultDescription.Substring(index + 5);
                        return result;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            set { this.Result = value; }
        }
    }
}

