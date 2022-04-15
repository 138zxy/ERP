using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    public class CarOilPriceSetting : _CarOilPriceSetting
    {
        [ScriptIgnore]
        public virtual StuffType StuffType
        {
            get;
            set;
        }

        /// <summary>
        /// 燃油类型
        /// </summary>
        public virtual string OilTypeName
        {
            get
            {
                if (StuffType != null)
                {
                    return StuffType.StuffTypeName;
                }
                return string.Empty;
            }
        }
    }
}
