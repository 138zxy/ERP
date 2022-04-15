using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.Model;
using ZLERP.Model.HR;

namespace ZLERP.Business.HR
{
    /*
        人力资源管理功能服务类 
    */
    public class HRService : ServiceBase<HR_Base_Data>
    {
        internal HRService(IUnitOfWork uow) : base(uow) { }

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Test(HR_PM_Contract HR_PM_Contract)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    HR_PM_Contract = this.m_UnitOfWork.GetRepositoryBase<HR_PM_Contract>().Add(HR_PM_Contract);
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 考勤的事物服务

        private string Week(DateTime weekday)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(weekday.DayOfWeek)];
            return week;
        }


        public ResultInfo BathPaiBan(bool IsSelectDepart, List<int> departmentid, List<string> Pesonid,
           bool IsCancel, DateTime StartPai, DateTime EndPai, bool Monday, bool Tuesday, bool Wednesday,
          bool Thursday, bool Friday, bool Saturday, bool Sunday, bool IsWeekAlternate, bool IsDayAlternate,
           int ArraggeShowID = 0, int StartWeek = 0, int AlternateDay = 0)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var paiService = this.m_UnitOfWork.GetRepositoryBase<HR_KQ_PaiBan>();
                    var personService = this.m_UnitOfWork.GetRepositoryBase<HR_Base_Personnel>();
                    IQueryable<HR_Base_Personnel> persons = null;
                    //获取需要执行排班的人员
                    if (IsSelectDepart)
                    {
                        persons = personService.Query().Where(t => departmentid.Contains(t.DepartmentID));
                    }
                    else
                    {
                        persons = personService.Query().Where(t => Pesonid.Contains(t.ID.ToString()));
                    }

                    List<int> personids = persons.Select(t => Convert.ToInt32(t.ID)).ToList();
                    // persons =personService.Query().Where(t=>)
                    //如果是取消操作，这是删除
                    //如果是非取消操作，同样也需要先删除已经排过的班
                    //按星期交替排班的需要特殊处理
                    if (!IsWeekAlternate)
                    {
                        var query = paiService.Query().Where(t => personids.Contains(t.PersonID) && t.WorkDate >= StartPai && t.WorkDate <= EndPai);
                        foreach (var p in query)
                        {
                            paiService.Delete(p);
                        }
                    }
                    if (!IsCancel)
                    {
                        int WeekAlternateoff = StartWeek == (int)StartPai.DayOfWeek ? 0 : 1;
                        int DayAlternateoff = 0;
                        int DayAlternatei = 0;
                        //查找出班次数据
                        var KQ_Arrange = this.m_UnitOfWork.GetRepositoryBase<HR_KQ_Arrange>().Get(ArraggeShowID.ToString());
                        //如果是非交替排班
                        while (StartPai <= EndPai)
                        {
                            #region 正常排班
                            if (!IsWeekAlternate && !IsDayAlternate)
                            {
                                if (((int)StartPai.DayOfWeek == 1 && !Monday) ||
                                    ((int)StartPai.DayOfWeek == 2 && !Tuesday) ||
                                    ((int)StartPai.DayOfWeek == 3 && !Wednesday) ||
                                    ((int)StartPai.DayOfWeek == 4 && !Thursday) ||
                                    ((int)StartPai.DayOfWeek == 5 && !Friday) ||
                                    ((int)StartPai.DayOfWeek == 6 && !Saturday) ||
                                    ((int)StartPai.DayOfWeek == 0 && !Sunday)
                                    )
                                {
                                    StartPai = StartPai.AddDays(1);
                                    continue;
                                }
                            }
                            #endregion
                            #region 按星期交替排班
                            else if (IsWeekAlternate)
                            {
                                if (StartWeek == (int)StartPai.DayOfWeek)
                                {
                                    if (WeekAlternateoff > 0)
                                    {
                                        if (WeekAlternateoff % 2 == 1)
                                        {
                                            StartPai = StartPai.AddDays(7);
                                            WeekAlternateoff++;
                                            continue;
                                        }
                                    }
                                    WeekAlternateoff++;
                                }

                                if (((int)StartPai.DayOfWeek == 1 && !Monday) ||
                                    ((int)StartPai.DayOfWeek == 2 && !Tuesday) ||
                                    ((int)StartPai.DayOfWeek == 3 && !Wednesday) ||
                                    ((int)StartPai.DayOfWeek == 4 && !Thursday) ||
                                    ((int)StartPai.DayOfWeek == 5 && !Friday) ||
                                    ((int)StartPai.DayOfWeek == 6 && !Saturday) ||
                                    ((int)StartPai.DayOfWeek == 0 && !Sunday)
                                    )
                                {

                                    var query = paiService.Query().Where(t => personids.Contains(t.PersonID) && t.WorkDate == StartPai);
                                    foreach (var z in query)
                                    {
                                        paiService.Delete(z);
                                    }

                                    StartPai = StartPai.AddDays(1);
                                    continue;
                                }
                            }
                            #endregion
                            #region 按多少天交替排班
                            else if (IsDayAlternate)
                            {
                                if (DayAlternatei % AlternateDay == 0)
                                {
                                    if (DayAlternateoff % 2 == 1)
                                    {
                                        StartPai = StartPai.AddDays(AlternateDay);
                                        DayAlternateoff++;
                                        continue;
                                    }
                                    DayAlternateoff++;
                                }
                                DayAlternatei++;
                            }
                            #endregion
                            #region 排班处理
                            HR_KQ_PaiBan KQ_PaiBan = new HR_KQ_PaiBan();
                            KQ_PaiBan.WorkDate = StartPai;
                            KQ_PaiBan.WeekShow = Week(StartPai);
                            KQ_PaiBan.ArrangeID = ArraggeShowID;
                            KQ_PaiBan.WordStartTime = Convert.ToDateTime(StartPai.ToString("yyyy-MM-dd") + " " + KQ_Arrange.WordStartTime);
                            if (KQ_Arrange.IsInnerDay)
                            {
                                KQ_PaiBan.WordEndTime = Convert.ToDateTime(StartPai.AddDays(1).ToString("yyyy-MM-dd") + " " + KQ_Arrange.WordEndTime);
                            }
                            else
                            {
                                KQ_PaiBan.WordEndTime = Convert.ToDateTime(StartPai.ToString("yyyy-MM-dd") + " " + KQ_Arrange.WordEndTime);
                            }
                            KQ_PaiBan.UpStartTime = KQ_PaiBan.WordStartTime.AddHours(-KQ_Arrange.UpInterval);

                            KQ_PaiBan.DwonEndTime = KQ_PaiBan.WordEndTime.AddHours(KQ_Arrange.DownInterval);

                            KQ_PaiBan.IsInnerDay = KQ_Arrange.IsInnerDay;
                            KQ_PaiBan.AutoUp = KQ_Arrange.AutoUp;
                            KQ_PaiBan.AutoDown = KQ_Arrange.AutoDown;
                            KQ_PaiBan.Remark = KQ_Arrange.Remark;

                            if (IsWeekAlternate)
                            {
                                var query = paiService.Query().Where(t => personids.Contains(t.PersonID) && t.WorkDate == StartPai);
                                foreach (var z in query)
                                {
                                    paiService.Delete(z);
                                }
                            }
                            foreach (var p in persons)
                            {
                                var copyPaiBan = (HR_KQ_PaiBan)KQ_PaiBan.Clone();
                                copyPaiBan.PersonID = Convert.ToInt32(p.ID);
                                paiService.Add(copyPaiBan);
                            }
                            StartPai = StartPai.AddDays(1);
                            #endregion
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }



        public ResultInfo InsertRecord(List<HR_KQ_Record> HR_KQ_Records)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var recordService=this.m_UnitOfWork.GetRepositoryBase<HR_KQ_Record>();
                    foreach (var s in HR_KQ_Records)
                    {
                        var piaoin = recordService.Add(s);
                    } 
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null};
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }

        public ResultInfo InsertRecord(List<HR_Base_Personnel> HR_Base_Personnels)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var recordService = this.m_UnitOfWork.GetRepositoryBase<HR_Base_Personnel>();
                    foreach (var s in HR_Base_Personnels)
                    {
                        var piaoin = recordService.Add(s);
                    } 
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功", Data = null};
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }
        }
        
        #endregion


        /// <summary>
        /// 删除工资的时候，必须同步删除明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultInfo DeleteSalaries(string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    var service = this.m_UnitOfWork.GetRepositoryBase<HR_GZ_Salaries>();
                    var serviceDel = this.m_UnitOfWork.GetRepositoryBase<HR_GZ_SalariesItem>();
                    foreach (var id in ids)
                    {
                        var bale = service.Get(id);
                        service.Delete(bale);
                        var SalariesID = Convert.ToInt32(id);
                        var query = serviceDel.Query().Where(t => t.SalariesID == SalariesID);
                        foreach (var q in query)
                        {
                            serviceDel.Delete(q);
                        }
                    }
                    tx.Commit();
                    return new ResultInfo() { Result = true, Message = "操作成功" };
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    tx.Rollback();
                    return new ResultInfo() { Result = false, Message = e.Message.ToString() };
                }
            }

        }
    }
}
