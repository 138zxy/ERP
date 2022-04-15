using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IShippingDocumentRepository : IRepositoryBase<ShippingDocument>
    {
        IList<DailyReport> ExecuteShipDoc(string StartDateTime, string EndDateTime, out int total);

        /// <summary>
        /// 运输单批量审核
        /// </summary>
        /// <param name="idstrs"></param>
        /// <param name="CurrentUserID"></param>
        /// <returns></returns>
        bool ShipDocMultiAudit(string idstrs, string CurrentUserID);
    }


    public interface IShippingDocumentGHRepository : IRepositoryBase<ShippingDocumentGH>
    {
        IList<DailyReport> ExecuteShipDoc(string StartDateTime, string EndDateTime, out int total);

        /// <summary>
        /// 运输单批量审核
        /// </summary>
        /// <param name="idstrs"></param>
        /// <param name="CurrentUserID"></param>
        /// <returns></returns>
        bool ShipDocMultiAudit(string idstrs, string CurrentUserID);

        /// <summary>
        /// 运输单调价
        /// </summary>
        /// <param name="beginDate">起始时间(包含)</param>
        /// <param name="endDate">截止时间(包含)</param>
        /// <param name="contractId">合同编号</param>
        /// <returns>执行完影响的行数</returns>
        int RefreshShipDocPrice(DateTime beginDate, DateTime endDate, string contractId);

        /// <summary>
        /// 运费调价
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        int RefreshShipDocFreight(DateTime beginDate, DateTime endDate, string contractId);
    }
}
