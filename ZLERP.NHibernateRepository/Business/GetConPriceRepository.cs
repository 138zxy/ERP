using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using ZLERP.IRepository;
using ZLERP.Model;

namespace ZLERP.NHibernateRepository
{
    public class GetConPriceRepository : NHRepositoryBase<ContractItem>, IGetConPriceRepository
    {
        public GetConPriceRepository(ISession session)
            : base(session)
        {

        }

        /// <summary>
        /// 获取砼单价
        /// </summary>
        /// <param name="contractID">合同编号</param>
        /// <param name="conStrength">砼强度</param>
        /// <param name="produceDate">生产时间</param>
        /// <returns></returns>
        public decimal GetConPrice(string contractID, string conStrength, DateTime produceDate)
        {
            string sp = "select dbo.GetConPrice(:ContractID,:ConStrength,:ProduceDate)";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("ContractID", contractID);
            query.SetString("ConStrength", conStrength);
            query.SetDateTime("ProduceDate", produceDate);
            object ret = query.UniqueResult();
            if (ret != null)
                return Convert.ToDecimal(ret);
            else
                return 0;
        }
       /// <summary>
        /// 获取泵单价
       /// </summary>
        /// <param name="contractID">合同编号</param>
       /// <param name="pumpName">泵名称</param>
       /// <returns></returns>
        public decimal GetConPumpPrice(string contractID, string pumpName)
        {
            string sp = "select dbo.GetConPumpPrice(:ContractID,:PumpName)";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("ContractID", contractID);
            query.SetString("PumpName", pumpName);
            object ret = query.UniqueResult();
            if (ret != null)
                return Convert.ToDecimal(ret);
            else
                return 0;
        }


        /// <summary>
        /// 获取砼单价
        /// </summary>
        /// <param name="contractID">合同编号</param>
        /// <param name="conStrength">砼强度</param>
        /// <param name="produceDate">生产时间</param>
        /// <returns></returns>
        public decimal GetConGHPrice(string contractID, string conStrength, DateTime produceDate)
        {
            string sp = "select dbo.GetConPriceGH(:ContractID,:ConStrength,:ProduceDate)";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("ContractID", contractID);
            query.SetString("ConStrength", conStrength);
            query.SetDateTime("ProduceDate", produceDate);
            object ret = query.UniqueResult();
            if (ret != null)
                return Convert.ToDecimal(ret);
            else
                return 0;
        }

        /// <summary>
        /// 获取砼单价
        /// </summary>
        /// <param name="contractID">合同编号</param>
        /// <param name="conStrength">砼强度</param>
        /// <param name="produceDate">生产时间</param>
        /// <returns></returns>
        public decimal GetConSHPrice(string contractID, string conStrength, DateTime produceDate)
        {
            string sp = "select dbo.GetConPriceSH(:ContractID,:ConStrength,:ProduceDate)";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("ContractID", contractID);
            query.SetString("ConStrength", conStrength);
            query.SetDateTime("ProduceDate", produceDate);
            object ret = query.UniqueResult();
            if (ret != null)
                return Convert.ToDecimal(ret);
            else
                return 0;
        }
    }

}
