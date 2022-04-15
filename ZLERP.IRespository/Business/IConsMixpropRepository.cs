using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IRepository
{
    public interface IConsMixpropRepository : IRepositoryBase<ConsMixprop>
    {
        /// <summary>
        /// 检查施工配比是否超出计量范围
        /// </summary> 
        /// <param name="taskId"></param>
        /// <param name="productLineId"></param>
        /// <param name="concreteRZ">混凝土罐容比</param>
        /// <param name="slurryRZ">砂浆罐容比</param>
        /// <param name="checkConcrete">是否检查混凝土配比</param>
        /// <param name="checkSlurry">是否检查砂浆配比</param>
        /// <returns>未超出返回null, 否则返回超秤的错误信息</returns>
        string CheckMesureScale(string taskId, string productLineId, decimal concreteRZ, decimal slurryRZ, bool checkConcrete, bool checkSlurry);
        /// <summary>
        /// 施工配比批量修改
        /// </summary>
        /// <param name="consid">施工配比编号</param>
        /// <param name="taskid">任务单编号</param>
        /// <param name="pid">机组编号</param>
        /// <returns></returns>
        bool BatchEditConsMixpropItems(string consid, string taskid, string pid);
    }
}
