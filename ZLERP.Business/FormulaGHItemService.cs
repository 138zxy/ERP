using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class FormulaGHItemService : ServiceBase<FormulaGHItem>
    {
        internal FormulaGHItemService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        public bool IsEnableEdit(int id, decimal amount)
        {
            
            FormulaGHItem item = this.m_UnitOfWork.GetRepositoryBase<FormulaGHItem>().Get(id);
            decimal s_amount = (decimal)item.StandardAmount;
            if (s_amount > 0)
            {
                if ((Math.Abs(amount - s_amount) / s_amount) > (decimal)0.05)
                    throw new Exception("修改量超过了标准量的范围");
                else
                {
                    //符合情况的更新理论配比
                    item.StuffAmount = amount;
                    this.m_UnitOfWork.GetRepositoryBase<FormulaGHItem>().Update(item, null);
                    this.m_UnitOfWork.Flush();
                    return true;
                }
            }
            else
            {
                throw new Exception("该参考配比不使用该原料");
            }
        }

        public override FormulaGHItem Add(FormulaGHItem entity)
        {
            List<FormulaGHItem> list=this.m_UnitOfWork.GetRepositoryBase<FormulaGHItem>()
                .Query().Where(m=>m.FormulaGHID==entity.FormulaGHID && m.StuffTypeID==entity.StuffTypeID).ToList();
            if (list.Count > 0)
            {
                throw new Exception("该配比库已存在该材料的用量信息");
            }
            else
            {
                return base.Add(entity);
            }
        }
    }
}
