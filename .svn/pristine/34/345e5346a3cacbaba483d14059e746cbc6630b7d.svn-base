using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class SysConfigController : BaseController<SysConfig, int>
    {
        /// <summary>
        /// 清空系统缓存
        /// </summary>
        /// <returns></returns>
        public JsonResult RefreshCache()
        {
            CacheHelper.RemoveCacheByType(null);
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }
        /// <summary>
        /// 更新参数记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Update(SysConfig entity)
        {
            if (entity.ConfigName != null)
            {
                SysConfig sys = this.service.SysConfig.Query()
                .Where(s => s.ConfigName == entity.ConfigName).OrderByDescending(s => s.ID).FirstOrDefault();
                entity.ID = sys.ID;
            }
            if (entity.DateType=="Time")
            {
                entity.ConfigValue=ToDBC(entity.ConfigValue.ToString().Trim());
            }
            CacheHelper.RemoveCacheByType(null);
            var actionResult = base.Update(entity);

            var list = this.service.SysConfig.GetAllSysConfigs().ToList();

            return actionResult;
        }
        /// <summary>
        /// 根据参数名称更新参数
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult UpdateByName(string configName, string value)
        {
            try
            {
                SysConfig sys = this.service.SysConfig.Query()
                   .Where(s => s.ConfigName == configName).FirstOrDefault();
                sys.ConfigValue = value;
          
                return this.Update(sys);
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, null);
                throw;
            }
        }
        /// <summary>
        /// 根据参数名称-获取系统参数配置
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public ActionResult GetSysConfig(string configName) {
            //SysConfig sys0 = this.service.SysConfig.GetSysConfig(configName);
            SysConfig sys = this.service.SysConfig.Query()
               .Where(s => s.ConfigName == configName).FirstOrDefault();
            return OperateResult(true,"",sys );
        }

        public override System.Web.Mvc.ActionResult Index()
        {
            var SysConfigs = this.service.SysConfig.GetAllSysConfigs();
            ViewBag.SysConfigs = SysConfigs;
            //判断GPS功能是否开启，开启才显示相关GPS设置
            var gpsFunc = this.service.SysFunc.Query().Where(p => p.ID == "42")
                .FirstOrDefault();
            ViewBag.GpsFuncEnabled = false;
            if (gpsFunc != null && !gpsFunc.IsDisabled) {
                ViewBag.GpsFuncEnabled = true;
            }
            return base.Index();
        }

        public static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
    }
}
