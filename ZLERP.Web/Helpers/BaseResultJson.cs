using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Web.Helpers
{
    public class BaseResultJson
    {
        public BaseResultJson()
        {

        }
        public BaseResultJson(string Errmsg,bool Issuccess,int Errcode)
        {
            success = Issuccess;
            errcode = Errcode;
            errmsg = Errmsg;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int? errcode { get; set; }

        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 成功，返回数据
        /// </summary>
        public object data { get; set; }

    }
}
