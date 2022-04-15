using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class SynInfoNCController : BaseController<SynInfoNC, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override ActionResult Add(SynInfoNC entity)
        {
            ActionResult result = base.Add(entity);
            return result;
        }

        [HttpPost]
        public override ActionResult Update(SynInfoNC entity)
        {
            ActionResult result = base.Update(entity);
            return result;
        }

        [HttpPost]
        public override ActionResult Delete(string[] id)
        {
            ActionResult result = base.Delete(id);
            return result;
        }

        /// <summary>
        /// 批量上传
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult ReUpload(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    SynInfoNC SynInfoNC = this.service.SynInfoNC.Get(Convert.ToInt32(id));
                    SynNC entity = this.service.SynNC.Get(SynInfoNC.SynNCID);
                    entity.Status = 0;
                    this.service.SynNC.Update(entity);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(true, e.Message, null);
            }
        }
    }
}
