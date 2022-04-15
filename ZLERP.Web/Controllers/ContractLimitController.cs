using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model.Beton;
using System.Collections.Specialized;


namespace ZLERP.Web.Controllers
{
    public class ContractLimitController : BaseController<ContractLimit, string>
    {

        public override System.Web.Mvc.ActionResult Add(ContractLimit ContractLimit)
        {
            string validateMessage;
            if (!ValidateContractLimit(ContractLimit, out validateMessage))
            {
                return OperateResult(false, validateMessage, null);
            }

            return base.Add(ContractLimit);
        }


        public override System.Web.Mvc.ActionResult Update(ContractLimit ContractLimit)
        {
            string validateMessage;
            if (!ValidateContractLimit(ContractLimit, out validateMessage))
            {
                return OperateResult(false, validateMessage, null);
            }

            return base.Update(ContractLimit);
        }


        /// <summary>
        /// 合同限制验证
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ValidateContractLimit(ContractLimit entity, out string errorMessage)
        {
            if (string.Equals(entity.LimitType, ContractLimitType.LimitCube, StringComparison.OrdinalIgnoreCase))
            {
                decimal result;

                if (!string.IsNullOrEmpty(entity.WarnValue) && !decimal.TryParse(entity.WarnValue, out result))
                {
                    errorMessage = "方量限制设置错误，【预警值】必须为空或者数字！";
                    return false;
                }

                if (!string.IsNullOrEmpty(entity.LimitValue) && !decimal.TryParse(entity.LimitValue, out result))
                {
                    errorMessage = "方量限制设置错误，【限制值】必须为空或者数字！";
                    return false;
                }

            }
            else if (string.Equals(entity.LimitType, ContractLimitType.LimitMoney, StringComparison.OrdinalIgnoreCase))
            {
                decimal result;

                if (!string.IsNullOrEmpty(entity.WarnValue) && !decimal.TryParse(entity.WarnValue, out result))
                {
                    errorMessage = "金额限制设置错误，【预警值】必须为空或者数字！";
                    return false;
                }

                if (!string.IsNullOrEmpty(entity.LimitValue) && !decimal.TryParse(entity.LimitValue, out result))
                {
                    errorMessage = "金额限制设置错误，【限制值】必须为空或者数字！";
                    return false;
                }

            }
            else if (string.Equals(entity.LimitType, ContractLimitType.LimitTime, StringComparison.OrdinalIgnoreCase))
            {
                DateTime result;

                if (!string.IsNullOrEmpty(entity.WarnValue) && !DateTime.TryParse(entity.WarnValue, out result))
                {
                    errorMessage = "时间限制设置错误，【预警值】必须为空或者时间字符串！";
                    return false;
                }

                if (!string.IsNullOrEmpty(entity.LimitValue) && !DateTime.TryParse(entity.LimitValue, out result))
                {
                    errorMessage = "时间限制设置错误，【限制值】必须为空或者时间字符串！";
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }















    }
}