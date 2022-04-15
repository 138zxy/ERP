using System;
using System.Collections.Generic;
using System.Text;
using WeightingSystem.Models;
using System.Data.SqlClient;
 

namespace WeightingSystem.Helpers
{
    [Serializable]
    class ERPDataProvider
    {
        public string m_ERPConnString = Config.ConfigKey.Server + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.Server, string.Empty) + ";" +
        Config.ConfigKey.Database + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.Database, string.Empty) + ";" +
        Config.ConfigKey.uid + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.uid, string.Empty) + ";" +
        Config.ConfigKey.pwd + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.pwd, string.Empty) + ";";
        string m_Select = "SELECT StuffInID,CarNo,totalNum,CarWeight,InDate,OutDate,Operator,(SELECT TOP 1 stuffName FROM stuffInfo WHERE stuffInfo.StuffID = StuffIn.StuffID) AS StuffName,StuffID,siloID,(SELECT TOP 1 siloName FROM silo WHERE silo.siloId = StuffIn.siloId) AS siloName,SupplyID,(SELECT TOP 1 SupplyName FROM SupplyInfo WHERE SupplyInfo.SupplyID = StuffIn.SupplyID) AS SupplyName,TransportID,WRate,Proportion,(SELECT TOP 1 SupplyName FROM SupplyInfo WHERE SupplyInfo.SupplyID = StuffIn.TransportID) as TransportName,Driver,InNum,Pic1,Pic2,Pic3,Pic4,DarkWeight,FastMetage,Spec,ParentID,(select top 1 FinalStuffType from StuffType inner join StuffInfo on StuffType.StuffTypeID = StuffInfo.StuffTypeID and StuffInfo.StuffID = StuffIn.StuffID) as FinalStuffType ";//,TransferName,KZL 
        string m_From = " from StuffIn ";
        /// <summary>
        /// 取得供货商列表
        /// </summary>
        /// <returns></returns>
        public IList<ListItem> GetSupplyList()
        {
            string sql = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.SqlSupply, string.Empty);
            return GetListFromSql(sql);
        }

        public IList<ListItem> GetTransferList()
        {
            string sql = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.SqlTransfer, string.Empty);
            return GetListFromSql(sql);
        }

        
        public IList<WeightingSystem.Models.User> GetUserList(string where)
        {
            string sql = "select UserID,Password,TrueName  from users where " + where;
            IList<WeightingSystem.Models.User> list = new List<WeightingSystem.Models.User>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    WeightingSystem.Models.User record = new WeightingSystem.Models.User();
                    record.UserID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    record.Password = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    record.UserName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                    list.Add(record);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private bool AddStuffIn(StuffIn record)
        {
            string m_Insert = @"INSERT INTO StuffIn(StuffInID,StuffID,Spec,SiloID,CarNo,SupplyID,GageUnit,TotalNum,CarWeight,WRate,InNum,InDate,Driver,AH,OutDate,Operator,pic1,pic2,pic3,pic4,DarkWeight,FastMetage,SourceAddr,BuildTime,ParentID,Proportion,OrderNum,TransportID,FootNum)
                            values( 
                            @StuffInID,
                            @StuffID,
                            @Spec,
                            @SiloID,
                            @CarNo,
                            @SupplyID,
                            '公斤',                            
                            @TotalNum,
                            @CarWeight,
                            @WRate,
                            @InNum,                           
                            @InDate,
                            @Driver,
                            'Auto',
                            @OutDate,
                            @Operator,
                            @Pic1,
                            @Pic2,
                            @Pic3,
                            @Pic4,
                            @DarkWeight,
                            @FastMetage,
                            @SourceAddr,
                            @BuildTime,
                            @ParentStuffInID,
                            @Proportion,
                            @OrderNum,@TransportID,@FootNum);";

         
            SqlParameter[] paras ={　
                                new SqlParameter("@StuffInID", record.StuffInID),
                                new SqlParameter("@StuffID", record.StuffID),
                                new SqlParameter("@Spec", record.Spec == null ? string.Empty : record.Spec),
                                new SqlParameter("@SiloID", record.SiloID),
                                new SqlParameter("@CarNo", record.CarNo),
                                new SqlParameter("@SupplyID", record.SupplyID),
                                new SqlParameter("@TotalNum", record.TotalNum),
                                new SqlParameter("@CarWeight", record.CarWeight),
                                new SqlParameter("@WRate", record.WRate),
                                new SqlParameter("@InNum", record.InNum),
                                new SqlParameter("@InDate", record.InDate), 
                                new SqlParameter("@Driver", record.Driver == null ? string.Empty : record.Driver),
                                record.OutDate ==null? new SqlParameter("@OutDate", DBNull.Value) :new SqlParameter("@OutDate", record.OutDate),
                                new SqlParameter("@Operator", record.Operator == null ? string.Empty : record.Operator),
                                new SqlParameter("@Pic1", record.Pic1 == null ? string.Empty : record.Pic1),
                                new SqlParameter("@Pic2", record.Pic2 == null ? string.Empty : record.Pic2),
                                new SqlParameter("@Pic3", record.Pic3 == null ? string.Empty : record.Pic3),
                                new SqlParameter("@Pic4", record.Pic4 == null ? string.Empty : record.Pic4),
                                new SqlParameter("@DarkWeight", record.DarkWeight),
                                new SqlParameter("@FastMetage", record.FastMetage),
                                new SqlParameter("@SourceAddr",record.SourceAddr == null?string.Empty: record.SourceAddr),
                                record.OutDate ==null? new SqlParameter("@BuildTime", DBNull.Value) :new SqlParameter("@BuildTime", record.OutDate),
                                new SqlParameter("@ParentStuffInID",string.IsNullOrEmpty(record.ParentStuffInID) ?string.Empty: record.ParentStuffInID),
                                new SqlParameter("@Proportion", record.Proportion),
                                new SqlParameter("@OrderNum", record.OrderNum),
                                record.TransportID==null||record.TransportID==""?new SqlParameter("@TransportID", DBNull.Value):new SqlParameter("@TransportID", record.TransportID),
                                new SqlParameter("@FootNum", record.FootNum)
              };
            
            return SqlHelper.ExecuteNonQuery(m_ERPConnString,System.Data.CommandType.Text, m_Insert, paras) > 0;
        }



        /// <summary>
        /// 换罐
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool ChangeSilo(StuffIn record)
        {
            


            SqlParameter[] paras ={　
                                new SqlParameter("@StuffinID", record.StuffInID),
                                new SqlParameter("@StuffID", record.StuffID),
                                new SqlParameter("@Spec", record.Spec == null ? string.Empty : record.Spec),
                                new SqlParameter("@SiloID", record.SiloID),
                                new SqlParameter("@CarNo", record.CarNo),
                                new SqlParameter("@SupplyID", record.SupplyID),
                                new SqlParameter("@TotalNum", record.TotalNum),
                                new SqlParameter("@CarWeight", record.CarWeight),
                                new SqlParameter("@WRate", record.WRate),
                                new SqlParameter("@InNum", record.InNum),
                                new SqlParameter("@Driver", record.Driver == null ? string.Empty : record.Driver),
                                new SqlParameter("@Operator", record.Operator == null ? string.Empty : record.Operator),
                                new SqlParameter("@Pic1", record.Pic1 == null ? string.Empty : record.Pic1),
                                new SqlParameter("@Pic2", record.Pic2 == null ? string.Empty : record.Pic2),
                                new SqlParameter("@Pic3", record.Pic3 == null ? string.Empty : record.Pic3),
                                new SqlParameter("@Pic4", record.Pic4 == null ? string.Empty : record.Pic4),
                                new SqlParameter("@DarkWeight", record.DarkWeight),
                                new SqlParameter("@FastMetage", record.FastMetage),
                                new SqlParameter("@SourceAddr",record.SourceAddr == null?string.Empty: record.SourceAddr),
                                new SqlParameter("@ParentStuffInID",string.IsNullOrEmpty(record.ParentStuffInID) ?string.Empty: record.ParentStuffInID)
                                
              };

            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.StoredProcedure, "ChangeSiloForMetage", paras) > 0;
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="stuffInId"></param>
        /// <returns></returns>
        private bool UpdateStuffIn(StuffIn record)
        {
            string sql = "update StuffIn set StuffID = @StuffID,Spec = @Spec,SiloID = @SiloID,CarNo = @CarNo,SupplyID = @SupplyID";
            sql += ",TotalNum = @TotalNum,CarWeight = @CarWeight,WRate = @WRate,InNum = @InNum,InDate = @InDate,Driver = @Driver";
            sql += ",OutDate = @OutDate,Operator =@Operator,pic1 =@pic1,pic2 =@pic2,pic3=@pic3,pic4 =@pic4,DarkWeight =@DarkWeight";
            sql += ",FastMetage = @FastMetage,Remark = @Remark ,SourceAddr = @SourceAddr,BuildTime = @BuildTime,TransportID = @TransportID,FootNum=@FootNum where StuffInID = @StuffInID";

            SqlParameter[] paras ={　
                                new SqlParameter("@StuffInID", record.StuffInID),
                                new SqlParameter("@StuffID", record.StuffID),
                                new SqlParameter("@Spec", record.Spec == null ? string.Empty : record.Spec),
                                new SqlParameter("@SiloID", record.SiloID == null ? string.Empty :record.SiloID),
                                new SqlParameter("@CarNo", record.CarNo),
                                new SqlParameter("@SupplyID", record.SupplyID),
                                new SqlParameter("@TotalNum", record.TotalNum),
                                new SqlParameter("@CarWeight", record.CarWeight),
                                new SqlParameter("@WRate", record.WRate),
                                new SqlParameter("@InNum", record.InNum),
                                new SqlParameter("@InDate", record.InDate), 
                                new SqlParameter("@Driver", record.Driver == null ? string.Empty : record.Driver),
                                record.OutDate == DateTime.MinValue ? new SqlParameter("@OutDate", DBNull.Value) :new SqlParameter("@OutDate", record.OutDate),
                                new SqlParameter("@Operator", record.Operator == null ? string.Empty : record.Operator),
                                new SqlParameter("@Pic1", record.Pic1 == null ? string.Empty : record.Pic1),
                                new SqlParameter("@Pic2", record.Pic2 == null ? string.Empty : record.Pic2),
                                new SqlParameter("@Pic3", record.Pic3 == null ? string.Empty : record.Pic3),
                                new SqlParameter("@Pic4", record.Pic4 == null ? string.Empty : record.Pic4),
                                new SqlParameter("@DarkWeight", record.DarkWeight),
                                new SqlParameter("@FastMetage", record.FastMetage),
                                new SqlParameter("@Remark", record.Remark),
                                record.TransportID==""?new SqlParameter("@TransportID", DBNull.Value):new SqlParameter("@TransportID", record.TransportID),
                                new SqlParameter("@FootNum", record.FootNum),
                                new SqlParameter("@SourceAddr",record.SourceAddr == null?string.Empty: record.SourceAddr),
                                record.OutDate == DateTime.MinValue ? new SqlParameter("@BuildTime", DBNull.Value) :new SqlParameter("@BuildTime", record.OutDate),
                                 };
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras) > 0;
        
        }
        
        public int checkExistStuffInID(string stuffInId)
        {
            string sql = "select count(*) from StuffIN  where StuffINID = '"+ stuffInId +"'";
            int count = 0;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                     
                    count = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                    break;
                }

                return count;
            }
        }

        /// <summary>
        /// 重新称量毛重
        /// </summary>
        /// <param name="Weight"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool reWeight(decimal Weight, string ID,string pic1_path,string pic2_path)
        {
            string m_reUpdateWeight = "UPDATE StuffIn set TotalNum=@TotalNum,Pic1 = @Pic1,Pic2 = @Pic2 WHERE StuffInID = @ID";
            SqlParameter[] paras ={　
                                new SqlParameter("@ID",ID), 
                                new SqlParameter("@TotalNum", Weight),
                                new SqlParameter("@Pic1", pic1_path), 
                                new SqlParameter("@Pic2", pic2_path) 
                               };
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text,m_reUpdateWeight, paras) > 0;
        }

        /// <summary>
        /// 获取材料类型
        /// </summary>
        /// <returns></returns>
        public IList<ListItem> getStuffType()
        {
            string sql = "select * from StuffType";
            return GetListFromSql(sql);
        }


        /// <summary>
        /// 根据车号取得最后一条入库记录
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        public StuffIn GetStuffInInfo(string carNo)
        {
            string sql = m_Select + m_From + string.Format(" WHERE CarNo='{0}' and  Innum = 0 ORDER BY StuffInID DESC ", carNo);
            return null;
           
        }


        public StuffIn Find(string StuffInID)
        {
            string sql = m_Select + m_From + "where StuffInID = '" + StuffInID + "'";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<StuffIn> list = ReaderToStuffInList(sdr);
                if (list.Count > 0)
                    return list[0];
                else
                    return null;
            }

        }

        /// <summary>
        /// 删除毛重记录
        /// </summary>

        /// <param name="ID"></param>
        /// <returns></returns>
        public bool delWeight(string ID)
        {
            string deleteSql = "delete from stuffIn where StuffInID = @StuffInID";
            SqlParameter[] paras ={　
                                new SqlParameter("@StuffInID",ID)
                               };
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, deleteSql, paras) > 0;
        }


        public string checkNoCarWeight(string CarNo)
        {
            string m_checkCarWeight = string.Format("SELECT  StuffInID FROM STUFFIN  WHERE CarWeight = 0 and TotalNum > 0 and CarNo = '{0}' ", CarNo);
            string ID = string.Empty;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, m_checkCarWeight, null))
            {
                while (sdr.Read())
                {
                    ID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                }
            }
            return ID;

        }


        public IList<ListItem> GetStuffList()
        {
            string sql = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.SqlStuff, string.Empty);
            return GetListFromSql(sql);
        }

        public IList<Silo> GetSiloList(string stuffId)
        {
          
            IList<Silo> list;
            SqlParameter[] paras ={　
                                new SqlParameter("@Stuffid",stuffId)
                               };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.StoredProcedure, "getSilo", paras))
            {
                list = new List<Silo>();
                while (sdr.Read())
                {
                     
                    Silo _silo = new Silo();
                    _silo.SiloID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    _silo.SiloName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    _silo.StuffID = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                    _silo.Content = sdr.IsDBNull(3) ? 0 : sdr.GetDecimal(3);
                    list.Add(_silo);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取所有筒仓信息
        /// </summary>
        /// <returns></returns>
        public IList<Silo> GetSiloList()
        {
            string sql = "SELECT SiloID,SiloName,StuffID,Content FROM dbo.Silo where StuffID is not null and StuffID <>'' and isUsed = 1";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<Silo> SiloList = new List<Silo>();
                while (sdr.Read())
                {
                    Silo _silo = new Silo();
                    _silo.SiloID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    _silo.SiloName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    _silo.StuffID = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                    _silo.Content = sdr.IsDBNull(3) ? 0 : sdr.GetDecimal(3);
                    SiloList.Add(_silo);
                }
                return SiloList;
            }
        }


     

        public bool SaveStuffIn(StuffIn si)
        {
           
            if (string.IsNullOrEmpty(si.StuffInID))            //增加
            {
                PublicHelper ph = new PublicHelper();
                si.StuffInID = ph.getMetageNo(si);
                
                if (this.AddStuffIn(si))
                {
                    LocalDataProvider ldp = new LocalDataProvider();
                    Config c = new Config();
                    AutoGenerateHelper agh = new AutoGenerateHelper();
                    string prefixPat = agh.GetPrefix(c.prefixPat);

                    int LastValue = agh.getLastValue(prefixPat, c.idLen) + si.LastValue;
                    ldp.remarkAutoGenerateID(prefixPat, c.idLen, LastValue);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else                                               //修改
            {
                return this.UpdateStuffIn(si);
            }
           
        }

        /// <summary>
        /// 取得所有入库记录
        /// </summary>
        /// <returns></returns>
        public IList<StuffIn> GetStuffInList()
        {
            string sql = m_Select + m_From + " WHERE InNum is null or InNum = 0 and lifecycle <> -1 ORDER BY StuffINId DESC";
            IList<StuffIn> list;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = ReaderToStuffInList(sdr);
            }
            return list;
        }

 
        

        IList<StuffIn> ReaderToStuffInList(SqlDataReader sdr)
        {
            IList<StuffIn> list = new List<StuffIn>();

            while (sdr.Read())
            {
                StuffIn record = new StuffIn();
                record.StuffInID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.CarNo = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                record.TotalNum = sdr.IsDBNull(2) ? 0 : sdr.GetDecimal(2);
                record.CarWeight = sdr.IsDBNull(3) ? 0 : sdr.GetDecimal(3);
                record.InDate = sdr.IsDBNull(4) ? DateTime.MinValue : sdr.GetDateTime(4);
                record.OutDate = sdr.IsDBNull(5) ? DateTime.MinValue : sdr.GetDateTime(5);
                record.Operator = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
                record.StuffName = sdr.IsDBNull(7) ? string.Empty : sdr.GetString(7);
                record.StuffID = sdr.IsDBNull(8) ? string.Empty : sdr.GetString(8);
                record.SiloID = sdr.IsDBNull(9) ? string.Empty : sdr.GetString(9);
                record.SiloName = sdr.IsDBNull(10) ? string.Empty : sdr.GetString(10);
                record.SupplyID = sdr.IsDBNull(11) ? string.Empty : sdr.GetString(11);
                record.SupplyName = sdr.IsDBNull(12) ? string.Empty : sdr.GetString(12);
                record.TransportID = sdr.IsDBNull(13) ? string.Empty : sdr.GetString(13);
                record.WRate = sdr.IsDBNull(14) ? 0 : sdr.GetDecimal(14);
                record.Proportion = sdr.IsDBNull(15) ? 0 : decimal.Parse(sdr.GetString(15));
                record.TransportName = sdr.IsDBNull(16)?string.Empty:sdr.GetString(16);
                record.Driver = sdr.IsDBNull(17) ? string.Empty : sdr.GetString(17);
                record.InNum = sdr.IsDBNull(18) ? 0 : sdr.GetDecimal(18);
                record.Pic1 = sdr.IsDBNull(19) ? string.Empty : sdr.GetString(19);
                record.Pic2 = sdr.IsDBNull(20) ? string.Empty : sdr.GetString(20);
                record.Pic3 = sdr.IsDBNull(21) ? string.Empty : sdr.GetString(21);
                record.Pic4 = sdr.IsDBNull(22) ? string.Empty : sdr.GetString(22);
                record.DarkWeight = sdr.IsDBNull(23) ? 0 : sdr.GetInt32(23);
                record.FastMetage = sdr.IsDBNull(24) ? false : sdr.GetBoolean(24);
                record.Spec = sdr.IsDBNull(25) ? string.Empty : sdr.GetString(25);
                record.ParentStuffInID = sdr.IsDBNull(26) ? string.Empty : sdr.GetString(26);
                record.FinalStuffType = sdr.IsDBNull(27) ? string.Empty : sdr.GetString(27);
                list.Add(record);

            }
            return list;
        }

        private IList<StuffInfo> ReaderToStuffInfoList(SqlDataReader sdr)
        {
            IList<StuffInfo> list = new List<StuffInfo>();

            while (sdr.Read())
            {
                StuffInfo record = new StuffInfo();
                record.StuffID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.StuffName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                record.StuffShortName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                record.SupplyID = sdr.IsDBNull(3) ? string.Empty : sdr.GetString(3);
                record.SupplyName = sdr.IsDBNull(4) ? string.Empty : sdr.GetString(4);
                record.Spec = sdr.IsDBNull(5) ? string.Empty : sdr.GetString(5);
                record.StuffTypeID = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
                record.DarkRate = sdr.IsDBNull(7) ? 0 : sdr.GetDecimal(7);
                record.Inventory = sdr.IsDBNull(8) ? 0 : sdr.GetDecimal(8);
                 
                list.Add(record);

            }
            return list;
        }

        /// <summary>
        /// 根据ID取得单条入库单记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public StuffIn GetStuffIn(string Id)
        {
            string Sql = m_Select + " from MetageStuffIn as StuffIn where  StuffInID ='" + Id + "'";
            IList<StuffIn> list ; 
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, Sql, null))
            {
                list = ReaderToStuffInList(sdr);
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }


        private IList<StuffInfo> ReaderToStuffSupplyInfoList(SqlDataReader sdr)
        {
            IList<StuffInfo> list = new List<StuffInfo>();

            while (sdr.Read())
            {
                StuffInfo record = new StuffInfo();
                record.StuffID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.StuffName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                record.StuffShortName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                record.SupplyID = sdr.IsDBNull(3) ? string.Empty : sdr.GetString(3);
                record.SupplyName = sdr.IsDBNull(4) ? string.Empty : sdr.GetString(4);
                record.Spec = sdr.IsDBNull(5) ? string.Empty : sdr.GetString(5);
                record.StuffTypeID = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
                record.DarkRate = sdr.IsDBNull(7) ? 0 : sdr.GetDecimal(7);
                record.SopRate = sdr.IsDBNull(8) ? 0 : sdr.GetDecimal(8);
                record.totalCount = sdr.IsDBNull(9) ? 0 : sdr.GetInt32(9);
                record.totalNum = sdr.IsDBNull(10) ? 0 : sdr.GetDecimal(10);
                list.Add(record);

            }
            return list;
        }

        private IList<ShippingDocument> ReaderToShippingDocumentList(SqlDataReader sdr)
        {
            IList<ShippingDocument> list = new List<ShippingDocument>();

            while (sdr.Read())
            {
                ShippingDocument record = new ShippingDocument();
                record.ShipDocID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.TaskID = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                record.ContractID = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                record.ContractName = sdr.IsDBNull(3) ? string.Empty : sdr.GetString(3);
                record.CustomerID = sdr.IsDBNull(4) ? string.Empty : sdr.GetString(4);
                record.ConstructUnit = sdr.IsDBNull(5) ? string.Empty : sdr.GetString(5);
                record.RealSlump = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
                record.CustName = sdr.IsDBNull(7) ? string.Empty : sdr.GetString(7);
                record.ProjectName = sdr.IsDBNull(8) ? string.Empty : sdr.GetString(8);
                record.ProjectAddr = sdr.IsDBNull(9) ? string.Empty : sdr.GetString(9);
                record.ConStrength = sdr.IsDBNull(10) ? string.Empty : sdr.GetString(10);
                record.ProductLineName = sdr.IsDBNull(11) ? string.Empty : sdr.GetString(11);
                record.CastMode = sdr.IsDBNull(12) ? string.Empty : sdr.GetString(12);
                record.ConsPos = sdr.IsDBNull(13) ? string.Empty : sdr.GetString(13);
                record.PumpName = sdr.IsDBNull(14) ? string.Empty : sdr.GetString(14);
                
                record.ParCube = sdr.IsDBNull(15) ? 0 : sdr.GetDecimal(15);
                record.SendCube = sdr.IsDBNull(16) ? 0 : sdr.GetDecimal(16);
                record.ProvidedCube = sdr.IsDBNull(17) ? 0 : sdr.GetDecimal(17);
                record.ProvidedTimes = sdr.IsDBNull(18) ? 0 : sdr.GetInt32(18);
                record.ProduceDate =   sdr.GetDateTime(19);
                record.CarID = sdr.IsDBNull(20) ? string.Empty : sdr.GetString(20);
             //   record.ServerTime =sdr.IsDBNull(21)?  : sdr.GetDateTime(21);
                if (sdr.IsDBNull(21))
                {
                    record.ServerTime = null;
                }
                else
                {
                    record.ServerTime = sdr.GetDateTime(21);
                }
                record.ConstrengthExchange = sdr.GetInt32(22);
                record.TotalWeight = sdr.IsDBNull(23) ? 0 : sdr.GetInt32(23);
                record.CarWeight = sdr.IsDBNull(24) ? 0 : sdr.GetInt32(24);
                record.Weight = sdr.IsDBNull(25) ? 0 : sdr.GetInt32(25);
                record.Exchange = sdr.IsDBNull(26) ? 0 :sdr.GetInt32(26);
                record.Cube = sdr.IsDBNull(27) ? 0 : sdr.GetDecimal(27);


                list.Add(record);

            }
            return list;
        }


        private IList<TZrelation> ReaderToTzRelationList(SqlDataReader sdr)
        {
            IList<TZrelation> list = new List<TZrelation>();

            while (sdr.Read())
            {
                TZrelation record = new TZrelation();
                record.SourceShipDocID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.Cube = sdr.IsDBNull(1) ? 0 : sdr.GetDecimal(1);
                record.CarID = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                record.TaskID = sdr.IsDBNull(3) ? string.Empty : sdr.GetString(3);
                record.ContractID = sdr.IsDBNull(4) ? string.Empty : sdr.GetString(4);
                record.ContractName = sdr.IsDBNull(5) ? string.Empty : sdr.GetString(5);
                record.CustomerID = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
                record.CustName = sdr.IsDBNull(7) ? string.Empty : sdr.GetString(7);
                record.ProjectName = sdr.IsDBNull(8) ? string.Empty : sdr.GetString(8);
                record.ProjectAddr = sdr.IsDBNull(9) ? string.Empty : sdr.GetString(9);
                record.ConStrength = sdr.IsDBNull(10) ? string.Empty : sdr.GetString(10);
                record.CastMode = sdr.IsDBNull(11) ? string.Empty : sdr.GetString(11);
                record.ConsPos = sdr.IsDBNull(12) ? string.Empty : sdr.GetString(12);
                record.PumpName = sdr.IsDBNull(13) ? string.Empty : sdr.GetString(13);
                record.ParCube = sdr.IsDBNull(14) ? 0 : sdr.GetDecimal(14);
                record.SendCube = sdr.IsDBNull(15) ? 0 : sdr.GetDecimal(15);
                record.ProvidedCube = sdr.IsDBNull(16) ? 0 : sdr.GetDecimal(16);
                record.ProvidedTimes = sdr.IsDBNull(17) ? 0 : sdr.GetInt32(17);
                record.ProduceDate = sdr.GetDateTime(18);
                record.TotalWeight = sdr.IsDBNull(19) ? 0 : sdr.GetInt32(19);
                record.CarWeight = sdr.IsDBNull(20) ? 0 : sdr.GetInt32(20);
                record.Weight = sdr.IsDBNull(21) ? 0 : sdr.GetInt32(21);
                record.Exchange = sdr.IsDBNull(22) ? 0 : sdr.GetInt32(22);
                record.BuildTime = sdr.GetDateTime(23);
               
                list.Add(record);

            }
            return list;
        }

        /// <summary>
        /// 读取材料暗扣
        /// </summary>
        /// <param name="stuffID"></param>
        /// <returns></returns>
        public decimal StuffDarkRate(string stuffID)
        { 
            string sql = "select DarkRate from stuffInfo where stuffID ='"+ stuffID +"'";
            decimal DarkRate = 0;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    DarkRate = sdr.IsDBNull(0) ? 0 : sdr.GetDecimal(0);
                }
            }
            return DarkRate;
        }


        private IList<ListItem> GetListFromSql(string sql) {
            if (string.IsNullOrEmpty(sql))
                return null;
            else
            {
                IList<ListItem> list;
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
                {
                    list = new List<ListItem>();
                    while (sdr.Read())
                    {
                        ListItem li = new ListItem();
                        li.Value = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                        li.Text = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                        list.Add(li);
                    }
                    return list;
                }

            }
        }

        /// <summary>
        ///按车号查询记录
        /// </summary>
        /// <returns></returns>
        public IList<StuffIn> GetStuffInListDetail(string CarNo)
        {
            string s_where = "  where  InNum > 0  and lifecycle <> -1";

            if (!("").Equals(CarNo))
            {
                s_where = s_where + "  and      CarNo  = '" + CarNo + "'";
            }

            string sql = m_Select + " from MetageStuffIn as StuffIn " + s_where + " order by OutDate desc";

            IList<StuffIn> list = new List<StuffIn>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = ReaderToStuffInList(sdr);
            }
            return list;
        }


        /// <summary>
        ///按条件查询查询记录
        /// </summary>
        /// <returns></returns>
        public IList<StuffIn> GetStuffInListDetail(string CarNo, string BaleId, string StuffId, string SupplyId,  string BeginTime, string EndTime)
        {


            string s_where = "  where  (OutDate between  '" + BeginTime + "' and   '" + EndTime + "'  )  and InNum > 0  and lifecycle <> -1";

            if (!("").Equals(CarNo))
            {
                s_where = s_where + "  and      CarNo  like '%" + CarNo + "%'";

            }
            if (!("").Equals(StuffId))
            {
                s_where = s_where + "  and     StuffID='" + StuffId + "'";

            } if (!("").Equals(SupplyId))
            {
                s_where = s_where + "  and     SupplyID='" + SupplyId + "'";

            } 
            if (!("").Equals(BaleId))
            {
                s_where = s_where + "  and     SiloID='" + BaleId + "'";
            }


            string sql = m_Select + " from MetageStuffIn as StuffIn " + s_where;

            IList<StuffIn> list = new List<StuffIn>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {

                list = ReaderToStuffInList(sdr);

            }
            return list;
        }

        /// <summary>
        /// 读取所有材料列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<StuffInfo> getStuffInfo(string where)
        {
            string sql = "select StuffID,StuffName,StuffShortName,StuffInfo.SupplyID,SupplyName,Sizex,StuffInfo.StuffTypeID,DarkRate,Inventory from StuffInfo join StuffType on StuffInfo.StuffTypeID = StuffType.StuffTypeID left join SupplyInfo on StuffInfo.SupplyID = SupplyInfo.SupplyID  where StuffInfo.IsUsed = 1 and StuffInfo.IsMetage = 1 and ";
            sql += where + "Order by StuffInfo.StuffTypeID";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<StuffInfo> list = ReaderToStuffInfoList(sdr);
                return list;
            }
       
        }


        /// <summary>
        /// 读取所有材料供应商关联表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<StuffInfo> getStuffSupplyInfo(string where)
        {
            string sql = "select StuffInfo.StuffID,StuffName,StuffShortName,StuffSupply.SupplyID,SupplyName,Sizex,StuffInfo.StuffTypeID,DarkRate,SopRate,";
            sql += "(SELECT COUNT(*) FROM dbo.StuffIn AS si WHERE si.StuffID = dbo.StuffInfo.StuffID AND  si.SupplyID =  StuffSupply.SupplyID AND si.Lifecycle = 1) AS totalCount, ";
            sql += "(CAST((SELECT isnull(sum(isnull(Innum,0)),0) FROM StuffIn AS si WHERE si.StuffID = StuffInfo.StuffID AND  si.SupplyID =  StuffSupply.SupplyID AND si.Lifecycle = 1)/1000 AS DECIMAL(18,2) )) AS totalNum from StuffInfo join StuffType on ";
            sql += "StuffInfo.StuffTypeID = StuffType.StuffTypeID  join StuffSupply on StuffInfo.StuffID = StuffSupply.StuffID ";
            sql += "join SupplyInfo on StuffSupply.SupplyID = SupplyInfo.SupplyID where StuffInfo.IsUsed = 1 and StuffInfo.IsMetage = 1 and ";
            sql += where;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<StuffInfo> list = ReaderToStuffSupplyInfoList(sdr);
                return list;
            }
        }

        /// <summary>
        /// 入库统计
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<StuffInfo> getStuffSupplyQuery(string where,string timewhere)
        {
            string sql = "select StuffInfo.StuffID,StuffName,StuffShortName,StuffSupply.SupplyID,SupplyName,Sizex,StuffInfo.StuffTypeID,DarkRate,";
            sql += "(SELECT COUNT(*) FROM dbo.StuffIn AS si WHERE si.StuffID = dbo.StuffInfo.StuffID AND  si.SupplyID =  StuffSupply.SupplyID AND si.Lifecycle = 1 and " + timewhere + ") AS totalCount, ";
            sql += "(CAST((SELECT isnull(sum(isnull(Innum,0)),0) FROM StuffIn AS si WHERE si.StuffID = StuffInfo.StuffID AND  si.SupplyID =  StuffSupply.SupplyID AND si.Lifecycle = 1 and " + timewhere + ")/1000 AS DECIMAL(18,2) )) AS totalNum from StuffInfo join StuffType on ";
            sql += "StuffInfo.StuffTypeID = StuffType.StuffTypeID  join StuffSupply on StuffInfo.StuffID = StuffSupply.StuffID ";
            sql += "join SupplyInfo on StuffSupply.SupplyID = SupplyInfo.SupplyID where  (SELECT COUNT(*) FROM dbo.StuffIn AS si WHERE si.StuffID = dbo.StuffInfo.StuffID AND  si.SupplyID =  StuffSupply.SupplyID AND si.Lifecycle = 1 and " + timewhere + ") > 0 and ";
            sql += where;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<StuffInfo> list = ReaderToStuffSupplyInfoList(sdr);
                
                return list;
            }
        }

        public void SaveStuffSupply(string StuffID,IList<ListItem> stuffSupplyList)
        {
            string sql = "begin transaction DELETE FROM dbo.StuffSupply WHERE StuffID = '" + StuffID + "';";
            foreach(ListItem li in stuffSupplyList)
            {
                sql += "insert into StuffSupply values('" + StuffID + "','" + li.Value + "');";
            }
            sql += "commit transaction";

             SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null);
        }

        //zyx
        public ShippingDocument GetShippingDocumentByCarId(string CarId)
        {
            string sql = "select top 1 ShipDocID,ShippingDocument.TaskID,ContractID,ContractName,CustomerID,ConstructUnit,RealSlump,CustName,ProjectName,ProjectAddr,ShippingDocument.ConStrength,ProductLineName,ShippingDocument.CastMode,ConsPos";
            sql += ",PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,CarID,GetDate() as ServerTime ,CAST(Consmixprop.Weight AS INT)  as ConstrengthExchange";
            sql += ", TotalWeight, CarWeight,ShippingDocument.Weight , ShippingDocument.Exchange,cube ";
            sql += " from ShippingDocument ";
            sql += " left join Consmixprop on ShippingDocument.ConsmixpropId = Consmixprop.ConsmixpropId  left join Formula on Consmixprop.formulaID = Formula.FormulaID";
            sql += " where CarID ='" + CarId + "' and IsEffective = 1 and Formula.FormulaType <> 'FType_S'  order by ShippingDocument.BuildTime desc ";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocument> list = ReaderToShippingDocumentList(sdr);
                if (list.Count > 0)
                    return list[0];
                else
                    return null;

            }
        }


        public bool ExistSourceShipDocTz(string ShipDocID)
        {
            string sql = "select count(*) from tzralation where sourceshipDocID = '" + ShipDocID + "'";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            return count > 0 ? true : false;

        }
        public TZrelation GetTzRelation(int id)
        {
            string sql = "select SourceShipDocID,tzralation.Cube,tzralation.CarID,TaskID,ContractID,ContractName,CustomerID,CustName,ProjectName,ProjectAddr,ConStrength,CastMode,";
            sql += "ConsPos,PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,tzralation.TotalWeight,tzralation.CarWeight,tzralation.Weight,tzralation.Exchange,tzralation.BuildTime  ";
            sql += "from tzralation left join shippingDocument on tzralation.SourceShipDocID = shippingDocument.shipDocId where tzralation.TZRalationID =" + id.ToString();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<TZrelation> list = ReaderToTzRelationList(sdr);
                if (list.Count > 0)
                    return list[0];
                else
                    return null;

            }
        }


        /// <summary>
        /// 获取ERP内设置的车辆回厂时间差
        /// </summary>
        /// <returns>单位，小时</returns>
        public decimal getCarHourInterval()
        {
            string sql = "select top 1 ConfigValue  from sysconfig where ConfigName = 'HourInterval'";
            decimal HourInterval = 0;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    HourInterval = sdr.IsDBNull(0) ? 0 : int.Parse(sdr.GetString(0));
                }
            }
            return  Math.Round(HourInterval/60 , 2);
        }

        public int AlterTzRelation(TZrelation tz)
        {
            string sql = @" update tzralation set Cube=@Cube,Builder=@Builder, BuildTime=getDate(), AH='Auto', TotalWeight=@TotalWeight, CarWeight=@CarWeight, Weight=@Weight ,Exchange=@Exchange where CarID='" + tz.CarID + "'and isCompleted = 0";
            SqlParameter[] paras ={　
                                new SqlParameter("@Cube",tz.Cube),
                                new SqlParameter("@Builder", MainForm.CurrentUserID),
                                new SqlParameter("@TotalWeight", tz.TotalWeight),
                                new SqlParameter("@CarWeight", tz.CarWeight),
                                new SqlParameter("@Weight", tz.Weight),
                                new SqlParameter("@Exchange", tz.Exchange)
              };

            if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras)) > 0)
            {
                return AddTzRelationHistory(tz, getTZID(tz.CarID));
            }
            else
            {
                return 0;
            }
        }

        private int AddTzRelationHistory(TZrelation tz, Source source)
        {
            string sql = @" insert into tzralationhistory (SourceShipDocID ,SourceCube,Cube ,CarID,  IsAudit, IsCompleted ,
                                                    Builder, BuildTime, AH, TotalWeight, CarWeight, Weight ,Exchange,IsLock,ReturnType,ParentID,operation,operationnum,operationcube)
                            values(@SourceShipDocID,"+source.sourcecube+@",@Cube, @CarID, 0, 0 ,@Builder,getDate(),'Auto',@TotalWeight,@CarWeight,
                                   @Weight,@Exchange,4,@ReturnType,@ParentID,'add',@operationnum,@operationcube )  select SCOPE_IDENTITY()";
            if (source.id > 0)
            {
                SqlParameter[] paras ={　
                                new SqlParameter("@SourceShipDocID", tz.SourceShipDocID),
                                new SqlParameter("@Cube",tz.Cube),
                                new SqlParameter("@CarID", tz.CarID),
                                new SqlParameter("@Builder", MainForm.CurrentUserID),
                                new SqlParameter("@TotalWeight", tz.TotalWeight),
                                new SqlParameter("@CarWeight", tz.CarWeight),
                                new SqlParameter("@Weight", tz.Weight),
                                new SqlParameter("@Exchange", tz.Exchange),
                                new SqlParameter("@ReturnType",tz.Type),
                                new SqlParameter("@ParentID",source.id),
                                new SqlParameter("@operationnum",tz.CarID),
                                new SqlParameter("@operationcube",tz.Cube)
              };
              Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
              return source.id;
            }
            else
            {
                return 0;
            }
        }

        public class Source
        {
            public int id { get; set; }
            public decimal sourcecube { get; set; }
        }

        private Source getTZID(string p)
        {
            string sql = "select top 1 tzralationid,sourcecube from tzralation where carid='"+p+"' and isCompleted = 0";
            Source s = new Source();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                     s.id = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                     s.sourcecube = sdr.IsDBNull(0) ? 0 : sdr.GetDecimal(1); 
                }
            }
            return s;
        }

        public int AddTzRelationHistory(TZrelation tz,int id,int flag)
        {
            string sql = @" insert into tzralationhistory (SourceShipDocID ,SourceCube,Cube ,CarID,  IsAudit, IsCompleted ,
                                                    Builder, BuildTime, AH, TotalWeight, CarWeight, Weight ,Exchange,IsLock,ReturnType,ParentID,operation,operationnum,operationcube)
                            values(@SourceShipDocID,@Cube,@Cube, @CarID, 0, 0 ,@Builder,getDate(),'Auto',@TotalWeight,@CarWeight,
                                   @Weight,@Exchange," + flag + ",@ReturnType,@ParentID,'add',@operationnum,@operationcube )  select SCOPE_IDENTITY()";
            if (id > 0)
            {
                SqlParameter[] paras ={　
                                new SqlParameter("@SourceShipDocID", tz.SourceShipDocID),
                                new SqlParameter("@Cube",tz.Cube),
                                new SqlParameter("@CarID", tz.CarID),
                                new SqlParameter("@Builder", MainForm.CurrentUserID),
                                new SqlParameter("@TotalWeight", tz.TotalWeight),
                                new SqlParameter("@CarWeight", tz.CarWeight),
                                new SqlParameter("@Weight", tz.Weight),
                                new SqlParameter("@Exchange", tz.Exchange),
                                new SqlParameter("@ReturnType",tz.Type),
                                new SqlParameter("@ParentID",id),
                                new SqlParameter("@operationnum",tz.CarID),
                                new SqlParameter("@operationcube",tz.Cube)
              };
                Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
                return id;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加剩料记录
        /// </summary>
        /// <param name="sd"></param>
        public int AddTzRelation(TZrelation tz)
        {
            string sql = @" insert into tzralation (SourceShipDocID ,SourceCube,Cube ,CarID,  IsAudit, IsCompleted ,
                                                    Builder, BuildTime, AH, TotalWeight, CarWeight, Weight ,Exchange,IsLock,ReturnType)
                            values(@SourceShipDocID, @Cube,@Cube, @CarID, 0, 0 ,@Builder,getDate(),'Auto',@TotalWeight,@CarWeight,
                                   @Weight,@Exchange,0,@ReturnType )  select SCOPE_IDENTITY()";
            SqlParameter[] paras ={　
                                new SqlParameter("@SourceShipDocID", tz.SourceShipDocID),
                                new SqlParameter("@Cube",tz.Cube),
                                new SqlParameter("@CarID", tz.CarID),
                                new SqlParameter("@Builder", MainForm.CurrentUserID),
                                new SqlParameter("@TotalWeight", tz.TotalWeight),
                                new SqlParameter("@CarWeight", tz.CarWeight),
                                new SqlParameter("@Weight", tz.Weight),
                                new SqlParameter("@Exchange", tz.Exchange),
                                new SqlParameter("@ReturnType",tz.Type)                                
              };
            int id = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
            return AddTzRelationHistory(tz,id,0);
        }


        /// <summary>
        /// 更新发货单上的出厂过磅信息
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public bool UpdateShippingDocument(ShippingDocument sd)
        {
            string sql = string.Empty;
             
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, "UseMetageCube", string.Empty)))
            {
                sql += "BEGIN TRANSACTION UPDATE ProduceTasks SET ProvidedCube = ProvidedCube + " + sd.Cube + ", ProvidedTimes = ProvidedTimes + 1 where TaskID ='" + sd.TaskID + "'";
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Exchange=" + sd.Exchange + ",Cube =" + sd.Cube + ",ParCube=" + sd.Cube + ",SignInCube =" + sd.Cube + ",ProvidedCube =  (select top 1 ProvidedCube from ProduceTasks where ProduceTasks.taskid='" + sd.TaskID + "') ,";
                sql += "ProvidedTimes = (select top 1 ProvidedTimes from ProduceTasks where ProduceTasks.taskid='" + sd.TaskID + "') where shipDocId ='" + sd.ShipDocID + "' COMMIT TRANSACTION";
               
            }
            else
            {
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Exchange=" + sd.Exchange + ",Cube =" + sd.Cube +  "  where shipDocId ='" + sd.ShipDocID + "'";
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null) > 0;
        }


        /// <summary>
        /// 判断车辆是否存在未完成的转退料记录
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public bool checkCarTzCompleted(string CarID)
        {
            string sql = "select count(*) from tzralation where carID ='" + CarID + "' and isCompleted = 0";
            int count =Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 在ERP内取得所有可用的车辆信息
        /// </summary>
        /// <returns></returns>
        public IList<ListItem> getCarList()
        {
            string sql = " select CarID from Car where isUsed = 1  and  CarTypeID = 'CT1' order by CarID ";
            IList<ListItem> list;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = new List<ListItem>();
                while (sdr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    li.Text = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    list.Add(li);
                }
                return list;
            }
        }

        /// <summary>
        /// 在ERP内取得所有砼强度信息
        /// </summary>
        /// <returns></returns>
        public IList<ListItem> getConstrengthList()
        {
            string sql = " select ConStrengthCode from Constrength  order by ConStrengthCode ";
            IList<ListItem> list;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = new List<ListItem>();
                while (sdr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    li.Text = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    list.Add(li);
                }
                return list;
            }
        
        }

       /// <summary>
       /// 查询出厂过磅记录
       /// </summary>
       /// <param name="begin">开始时间</param>
       /// <param name="end">结束时间</param>
       /// <param name="CarID">车辆编号</param>
       /// <param name="Constrength">砼强度</param>
       /// <returns></returns>
        public IList<ShippingDocument> getShippingRecord(DateTime begin,DateTime end, string CarID,string Constrength)
        {
            string sql = " select ShipDocID,TaskID,ContractID,ContractName,CustomerID,ConstructUnit,RealSlump";
            sql += ",CustName,ProjectName,ProjectAddr,ConStrength,ProductLineName,CastMode,ConsPos,PumpName";
            sql += ",ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,CarID, null as ServerTime ,0 as ConstrengthExchange";
            sql += ",TotalWeight,CarWeight,Weight,Exchange,Cube from shippingDocument where cube > 0 and IsEffective = 1 and ProduceDate between '"+begin.ToString()+"'";
            sql += " and '"+ end.ToString() +"'";
            if (!string.IsNullOrEmpty(CarID))
            {
                sql += " and CarId = '" + CarID.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(Constrength))
            { 
                sql += " and Constrength ='"+ Constrength.Trim() +"'";
            }
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocument> list = ReaderToShippingDocumentList (sdr);

                return list;
            }
        }


        public IList<TZrelation> getTZRecord(DateTime begin, DateTime end, string CarID, string Constrength, string ShipDocID)
        {
            string sql = "select SourceShipDocID,tzralation.Cube,tzralation.CarID,TaskID,ContractID,ContractName,CustomerID,CustName,ProjectName,ProjectAddr,ConStrength,CastMode,";
            sql += "ConsPos,PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,tzralation.TotalWeight,tzralation.CarWeight,tzralation.Weight,tzralation.Exchange,tzralation.BuildTime  ";
            sql += "from tzralation left join shippingDocument on tzralation.SourceShipDocID = shippingDocument.shipDocId where tzralation.cube > 0 and tzralation.BuildTime between '" + begin.ToString() + "' and '" + end.ToString() + "'";

            if (!string.IsNullOrEmpty(CarID))
            {
                sql += " and tzralation.CarID ='" + CarID + "'";
            }

            if (!string.IsNullOrEmpty(Constrength))
            {
                sql += " and constrength='"+Constrength+"'";
            }

            if (!string.IsNullOrEmpty(ShipDocID))
            { 
                sql += " and SourceShipDocID ='"+ ShipDocID +"'";
            }

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<TZrelation> list = ReaderToTzRelationList(sdr);

                return list;
            }
        }


        public int AddCarEmptyWeight(CarEmptyWeight cew)
        {
            string sql = "insert into CarEmptyWeight (CarID, Weight, Builder, BuildTime) values (@CarID,@Weight,@Builder,getdate()) ";

            SqlParameter[] paras ={　
                                new SqlParameter("@CarID", cew.CarID),
                                new SqlParameter("@Weight",cew.Weight),
                                new SqlParameter("@Builder", cew.Builder)
                                
              };

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
        }


        /// <summary>
        /// 获得罐车系统皮重
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public int getCarEmptyWeight(string CarID)
        {
            string sql = "select top 1 Weight from CarEmptyWeight where CarID='" + CarID + "' order by ID desc";

            int? weight = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));

            if (weight == null)
            {
                weight = 0;
            }

            return Convert.ToInt32(weight);
        }

        public string FindSourceCarID(string CarID)
        {
            string sql = "select CarID from ShippingDocument where ShipDocID in (select sourceshipDocID from tzralation where carID ='" + CarID + "' and isCompleted = 0)";
            string sourceCarID = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (sourceCarID == null)
            {
                sourceCarID ="";
            }

            return sourceCarID;
        }

        
    }
}
