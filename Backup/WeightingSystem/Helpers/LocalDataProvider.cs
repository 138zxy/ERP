using System;
using System.Collections.Generic;
using System.Text;
using WeightingSystem.Models;
using System.Data.SQLite; 

namespace WeightingSystem.Helpers
{
    public class LocalDataProvider
    {
        //string m_Select = "SELECT ID,CarNo,Weight,CarWeight,InDate,OutDate,Operator,StuffName,StuffID,BaleID,BaleName,IsUploaded,SupplyID,SupplyName,TransferID,TransferName,KZL,StuffWeight ";
        //string m_From = " from StuffIn ";
        string m_UpdateCarWeight = "UPDATE StuffIn set CarWeight=@CarWeight,OutDate=@OutDate WHERE ID=@ID";
//        string m_Insert = @"INSERT INTO StuffIn(CarNo,Weight,CarWeight,InDate,OutDate,Operator,StuffName,StuffID,BaleID,BaleName,IsUploaded,SupplyID,SupplyName,TransferID,TransferName,KZL)
//                            values( 
//                            @CarNo,
//                            @Weight,
//                            @CarWeight,
//                            @InDate,
//                            @OutDate,
//                            @Operator,
//                            @StuffName,
//                            @StuffID,
//                            @BaleID,
//                            @BaleName,
//                            @IsUploaded,
//                            @SupplyID,
//                            @SupplyName,
//                            @TransferID,
//                            @TransferName,
//                            @KZL
//                            );";
        string m_reUpdateWeight = "UPDATE StuffIn set Weight=@Weight WHERE ID = @ID";

       

        ///// <summary>
        ///// 取得所有入库记录
        ///// </summary>
        ///// <returns></returns>
        //public IList<StuffIn> GetStuffInList()
        //{
        //    string sql = m_Select + m_From + " WHERE StuffWeight is null or StuffWeight=0 ORDER BY ID DESC";
        //    IList<StuffIn> list;
        //    using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null)) {
        //        list = ReaderToStuffInList(sdr);
        //    }
        //    return list;
        //}


        public int checkNoCarWeight(string CarWeight)
        {
            string m_checkCarWeight = string.Format("SELECT  ID FROM STUFFIN  WHERE CarWeight = 0 and Weight > 0 and CarNo = '{0}' limit 1", CarWeight);
            int ID = 0;
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(m_checkCarWeight, null))
            {
                while (sdr.Read())
                {
                    ID = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                }
            }
            return ID;

        }

        public int getNoSynRecordCount()
        {
            string sql = "select Count(*) from stuffin where IsUploaded=0 and StuffWeight > 0";
            int count = 0;
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null))
            {
                while (sdr.Read())
                {
                    count = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                }
            }
            return count;
        }

        public  IList<PrintStuffInfo>  getPrintStuffList(string stuffID)
        {
            string sql = "select ID,StuffID,PrintStuffName,DefaultSelect from PrintStuffDic ";
            if (!string.IsNullOrEmpty(stuffID))
            {
                sql += " where stuffid='" + stuffID +"'";
            }
            IList<PrintStuffInfo> list = new List<PrintStuffInfo>();
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null)) {

                 
                while (sdr.Read())
                {
                    PrintStuffInfo PrintStuff = new PrintStuffInfo();
                    PrintStuff.ID = sdr.GetInt32(0);
                    PrintStuff.StuffID = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    PrintStuff.PrintStuffName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                    PrintStuff.DefaultSelect = sdr.GetBoolean(3);
                    list.Add(PrintStuff);
                }
                return list;
            
                 
            }

        }


        public void AddPrintStuffName(string stuffId,string PrintStuffName)
        {
            IList<PrintStuffInfo> list = new List<PrintStuffInfo>();
            list = getPrintStuffList(stuffId);
            foreach (PrintStuffInfo li in list)
            {
                if (li.PrintStuffName.Trim() == PrintStuffName.Trim() && stuffId == li.StuffID)
                {

                    string sql1 = "update PrintStuffDic set defaultSelect = 0 where StuffID = '"+ stuffId +"'; update PrintStuffDic set defaultSelect = 1 where id =" + li.ID;
                    SQLiteHelper.ExecuteNonQuery(sql1, null);
                    return;

                }
            }
            string sql = "update PrintStuffDic set defaultSelect = 0 where StuffID = '" + stuffId + "'; insert into PrintStuffDic (stuffid,PrintStuffName,defaultSelect) values('" + stuffId + "','" + PrintStuffName + "',1) ";
            SQLiteHelper.ExecuteNonQuery(sql, null);
        }
        


        /// <summary>
        /// 更新磅单流水号
        /// </summary>
        /// <param name="Prefix"></param>
        /// <param name="IdLen"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool remarkAutoGenerateID(string Prefix,int IdLen,int value)
        {
            string sql = string.Empty;
            sql += "UPDATE AutoGenerateId SET lastValue = " + value.ToString() + " WHERE prefixPat = '" + Prefix + "' AND idlen = " + IdLen + "; ";
            sql += "INSERT INTO AutoGenerateId select  '" + Prefix + "','" + IdLen + "'," + value.ToString() + "  where not exists(select * from AutoGenerateId where prefixPat = '" + Prefix + "' AND idlen = '" + IdLen + "')";
            return SQLiteHelper.ExecuteNonQuery(sql, null) > 0;
        }

        /// <summary>
        /// 更新入库记录皮重
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carWeight"></param>
        /// <returns></returns>
        public bool UpdateStuffInCarWeight(int id, decimal carWeight) {
            SQLiteParameter[] paras ={　
                                new SQLiteParameter("@ID",id), 
                                new SQLiteParameter("@CarWeight", carWeight), 
                                new SQLiteParameter("@OutDate", DateTime.Now)
                                 };
            return SQLiteHelper.ExecuteNonQuery(m_UpdateCarWeight, paras) > 0; 
        }

       

        /// <summary>
        /// 重新称量毛重
        /// </summary>
        /// <param name="Weight"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool reWeight(decimal Weight,int ID)
        {
            SQLiteParameter[] paras ={　
                                new SQLiteParameter("@ID",ID), 
                                new SQLiteParameter("@Weight", Weight) 
                               };
            return SQLiteHelper.ExecuteNonQuery(m_reUpdateWeight, paras) > 0; 
        }

        /// <summary>
        /// 删除毛重记录
        /// </summary>
        
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool delWeight(int ID)
        {
            string deleteSql = "delete from stuffIn where ID = @ID";
            SQLiteParameter[] paras ={　
                                new SQLiteParameter("@ID",ID)
                               };
            return SQLiteHelper.ExecuteNonQuery(deleteSql, paras) > 0;
        }

 
        IList<Car> ReaderToCarList(SQLiteDataReader sdr)
        {
            IList<Car> list = new List<Car>();

            while (sdr.Read())
            {
                Car record = new Car();
                record.CarNo = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.Weight = sdr.IsDBNull(1) ? 0 : sdr.GetInt32(1);
                record.MarkTime = sdr.IsDBNull(2) ? DateTime.Now : sdr.GetDateTime(2);
               
                record.Driver1 = sdr.IsDBNull(3) ? string.Empty : sdr.GetString(3);
                record.Driver2 = sdr.IsDBNull(4) ? string.Empty : sdr.GetString(4);
                record.Driver3 = sdr.IsDBNull(5) ? string.Empty : sdr.GetString(5);
                record.Driver4 = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
               
                list.Add(record);

            }
            return list;
        }

       

        IList<WeightingSystem.Models.User> ReaderToUserList(SQLiteDataReader sdr)
        {
            IList<WeightingSystem.Models.User> list = new List<WeightingSystem.Models.User>();

            while (sdr.Read())
            {
                WeightingSystem.Models.User  user = new WeightingSystem.Models.User();
                user.UserID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                user.Password = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                user.UserName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                
                 
                list.Add(user);

            }
            return list;
        }
       

 
        /// <summary>
        /// 新增车辆
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool addCar(Car car)
        {
            string m_InsertCar = "insert into Car(CarNo,Driver1,Driver2,Driver3,Driver4,Weight,MarkTime) values(@CarNo,@Driver1,@Driver2,@Driver3,@Driver4,@Weight,@MarkTime)";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@CarNo", car.CarNo),
                                    new SQLiteParameter("@Weight", car.Weight),
                                    new SQLiteParameter("@Driver1", car.Driver1),
                                    new SQLiteParameter("@Driver2", car.Driver2),
                                    new SQLiteParameter("@Driver3", car.Driver3),
                                    new SQLiteParameter("@Driver4", car.Driver4),
                                    new SQLiteParameter("@MarkTime", car.MarkTime)
                                     
                                 };
            return SQLiteHelper.ExecuteNonQuery(m_InsertCar, paras) > 0;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool addUser(User user)
        {
            string m_Insertuser = "insert into User(UserID,Password,UserName,Remark,Authority) values(@UserID,@Password,@UserName,@Remark,@Authortity)";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@UserID", user.UserID),
                                    new SQLiteParameter("@Password", user.Password),
                                    new SQLiteParameter("@UserName", user.Password),
                                    //new SQLiteParameter("@Remark", user.Remark),
                                    //new SQLiteParameter("@Authortity", user.Authority)
                                     
                                 };
            return SQLiteHelper.ExecuteNonQuery(m_Insertuser, paras) > 0;
        }

        /// <summary>
        /// 获得车辆列表
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetCarList(string where)
        {
            string sql =  "select * from Car where "+ where;
            IList<Car> list;
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null))
            {
                list = ReaderToCarList(sdr);
            }
            return list;
        }

         

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <returns></returns>
        public IList<WeightingSystem.Models.User> GetUserList(string where)
        {
            string sql = "select UserID,Password,UserName,Remark,Authority from user where " + where;
            IList<WeightingSystem.Models.User> list;
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null))
            {
                list = ReaderToUserList(sdr);  
            }
            return list;
        }




        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="CarNo"></param>
        /// <returns></returns>
        public bool delCar(string CarNo)
        {
            string sql = "delete from Car where CarNo = @CarNo";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@CarNo", CarNo)
                                 };
            return SQLiteHelper.ExecuteNonQuery(sql, paras) > 0;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="CarNo"></param>
        /// <returns></returns>
        public bool delUser(string UserID)
        {
            string sql = "delete from User where UserID = @UserID";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@UserID", UserID)
                                 };
            return SQLiteHelper.ExecuteNonQuery(sql, paras) > 0;
        }


        /// <summary>
        /// 更改车辆信息
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool updateCar(Car car,string oldCarNo)
        {
            string sql = "update Car set CarNo =@CarNo , Weight =@Weight,MarkTime=@MarkTime,Driver1=@Driver1,Driver2=@Driver2,Driver3=@Driver3,Driver4=@Driver4 where CarNo = '"+oldCarNo+"'";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@CarNo", car.CarNo),
                                    new SQLiteParameter("@Weight", car.Weight),
                                    new SQLiteParameter("@MarkTime", car.MarkTime),
                                    new SQLiteParameter("@Driver1", car.Driver1),
                                    new SQLiteParameter("@Driver2", car.Driver2),
                                    new SQLiteParameter("@Driver3", car.Driver3),
                                    new SQLiteParameter("@Driver4", car.Driver4)
                                     
                                 };
            return SQLiteHelper.ExecuteNonQuery(sql, paras) > 0;
        }

        /// <summary>
        /// 更改用户信息
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool updateUser(User user, string oldUserID)
        {
            string sql = "update user set UserID =@UserID , Password = @Password,UserName = @UserName,remark=@remark,authority =@authority where Userid = '" + oldUserID + "'";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@UserID", user.UserID),
                                    new SQLiteParameter("@Password", user.Password),
                                    new SQLiteParameter("@UserName", user.UserName) 
                                     
                                 };
            return SQLiteHelper.ExecuteNonQuery(sql, paras) > 0;
        }


        public int CheckExistCarNo(string CarNo)
        {
            string sql = "select count(*) from Car where CarNo=@CarNo";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@CarNo", CarNo) 
                                 };
            

            int count = 0;
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, paras))
            {
                while (sdr.Read())
                {
                    count = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                }
            }
            return count;
        }


        public int CheckExistUserID(string UserID)
        {
            string sql = "select count(*) from User where UserID=@UserID";
            SQLiteParameter[] paras ={　
                                    new SQLiteParameter("@UserID", UserID) 
                                 };


            int count = 0;
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, paras))
            {
                while (sdr.Read())
                {
                    count = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                }
            }
            return count;
        }

        public IList<string> getDriver(string CarNo)
        {
            string sql = "select  Driver1 from Car where Carno = '" + CarNo + "' union all select Driver2 from Car where Carno = '" + CarNo + "'";
            sql += "union all select Driver3 from Car where Carno = '" + CarNo + "' union all select Driver4 from Car where Carno = '" + CarNo + "'";
            IList<string> source = new List<string>();
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null))
            {
                 
                while (sdr.Read())
                {
                    string Driver =  sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    if (!string.IsNullOrEmpty(Driver))
                    {
                        source.Add(Driver);
                    }
                }
                return source;
            }
        }
 

 
      


        public string getUserAuthority(string UserID,int index)
        {
            string result = string.Empty;
            IList<User> UserList = this.GetUserList("UserID='" + UserID + "'");
            if (UserList.Count > 0)
            {
                //if (index == 0)
                //{
                //    result = UserList[0].Authority;
                //}
                //else
                //{
                //    result = UserList[0].Authority.Split('|')[index];
                //}
            }
            return result;
        }


        public IList<AutoGenerate> getAutoGenerateList(string where)
        {
            string sql = "select * from AutoGenerateId where " + where;
            IList<AutoGenerate> autoGenerateList = new List<AutoGenerate>();
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReader(sql, null))
            {
                while (sdr.Read())
                {
                    AutoGenerate record = new AutoGenerate();
                    record.prefixPat = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    record.idLen = sdr.IsDBNull(1) ? 0 : sdr.GetInt16(1);
                    record.LastValue = sdr.IsDBNull(2) ? 0 : sdr.GetInt16(2);
                
                    autoGenerateList.Add(record);

                }

               
            }

            return autoGenerateList;
        }


        public IList<AutoGenerate> getAutoGenerateList(string where, SQLiteTransaction sqliteTran)
        {
            string sql = "select * from AutoGenerateId where ";
            IList<AutoGenerate> autoGenerateList = new List<AutoGenerate>();
            using (SQLiteDataReader sdr = SQLiteHelper.ExecuteReaderTran(sql, sqliteTran ,null))
            {
                while (sdr.Read())
                {
                    AutoGenerate record = new AutoGenerate();
                    record.prefixPat = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    record.idLen = sdr.IsDBNull(1) ? 0 : sdr.GetInt16(1);
                    record.LastValue = sdr.IsDBNull(2) ? 0 : sdr.GetInt16(2);

                    autoGenerateList.Add(record);

                }

            }

            return autoGenerateList;
        }

    }
}
