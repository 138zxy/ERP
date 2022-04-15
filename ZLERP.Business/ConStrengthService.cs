using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class ConStrengthService : ServiceBase<ConStrength>
    {
        internal ConStrengthService(IUnitOfWork uow) : base(uow) { }

        public dynamic GetDynamicColByDataType(string Datatype)
        {
            IList<ConStrength> conlist = this.m_UnitOfWork.GetRepositoryBase<ConStrength>().Query().OrderBy(m => m.ConStrengthCode).ToList();
            if (Datatype!="")
            {
                conlist = conlist.Where(m => m.IsSH == Convert.ToInt32(Datatype)).OrderBy(m => m.ConStrengthCode).ToList();
            }
            IList<object> ColMs = new List<object>();
            foreach (ConStrength entity in conlist)
            {
                var colm = new
                {
                    label = entity.ConStrengthCode,
                    //label = entity.ConStrengthCode + "</br>" + entity.ConStrengthCode,
                    name =  entity.ConStrengthCode ,
                    index =  entity.ConStrengthCode ,
                    //width = 50,
                    align = "right",
                    editable = true,
                    //editrules = "{ number: true }",
                    sortable = false

                };
                ColMs.Add(colm);
            }
            return new
            {
                Result = true
                ,
                Message = string.Empty
                ,
                ColumnData = ColMs
            };
        }
    }
}
