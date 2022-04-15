using System;
using System.Collections.Generic;
using System.Text;
using WeightingSystem.Models;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
 

namespace WeightingSystem.Helpers
{
    [Serializable]
    class ERPDataProvider
    {
        public string m_ERPConnString = Config.ConfigKey.Server + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.Server, string.Empty) + ";" +
        Config.ConfigKey.Database + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.Database, string.Empty) + ";" +
        Config.ConfigKey.uid + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.uid, string.Empty) + ";" +
        Config.ConfigKey.pwd + "=" + Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.pwd, string.Empty) + ";";
        string m_Select = @"SELECT StuffInID,CarNo,totalNum,CarWeight,InDate,OutDate,Operator,(SELECT TOP 1 stuffName FROM stuffInfo 
                            WHERE stuffInfo.StuffID = StuffIn.StuffID) AS StuffName,StuffID,siloID,siloName,SupplyID,(SELECT TOP 1 SupplyName FROM SupplyInfo 
                            WHERE SupplyInfo.SupplyID = StuffIn.SupplyID) AS SupplyName,TransportID,WRate,Proportion,(SELECT TOP 1 SupplyName FROM SupplyInfo 
                            WHERE SupplyInfo.SupplyID = StuffIn.TransportID) as TransportName,Driver,InNum,Pic1,Pic2,Pic3,Pic4,DarkWeight,FastMetage, (SELECT SpecName FROM StuffinfoSpec WHERE SpecID=StuffIn.SpecID ) as Spec ,ParentID,
                            (select top 1 FinalStuffType from StuffType inner join StuffInfo on StuffType.StuffTypeID = StuffInfo.StuffTypeID and StuffInfo.StuffID = StuffIn.StuffID) as FinalStuffType 
                            ,MingWeight,CompanyID,SourceNumber,SourceAddr,SupplyNum,Volume,SpecID,Batch";//,TransferName,KZL 
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
        public DataTable GetDataTable(string sql) {
            DataSet ds = SqlHelper.ExecuteDataSet(m_ERPConnString, System.Data.CommandType.Text, sql, null);
            if(ds.Tables.Count>0){
                return ds.Tables[0];
            }
            return new DataTable();
        }
        public int ExecuteNonQuery(string sql)
        {
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, CommandType.Text, sql, null);
        }

        public IList<ListItem> GetCmpList()
        {
            string sql = "select CompName,CompName from Company";
            return GetListFromSql(sql);
        }

        #region GetCmpList

        public IList<ListIntItem> GetSaleCmpList()
        {
            string sql = "select CompanyID,CompName from Company";
            return GetCmpListSql(sql);
        }

        private IList<ListIntItem> GetCmpListSql(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return null;
            else
            {
                IList<ListIntItem> list;
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
                {
                    list = new List<ListIntItem>();
                    while (sdr.Read())
                    {
                        ListIntItem li = new ListIntItem();
                        li.Value = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                        li.Text = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                        list.Add(li);
                    }
                    return list;
                }

            }
        }
        #endregion
        public IList<ListItem> GetCmpValueList()
        {
            string sql = "select CompanyID,CompName from Company";
            IList<ListItem> list;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = new List<ListItem>();
                while (sdr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = sdr.IsDBNull(0) ? string.Empty : sdr.GetInt32(0).ToString();
                    li.Text = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    list.Add(li);
                }
            }
            return list;
        }


        public IList<ListItem> GetSpecValueList(string StuffID)
        {
            string sql = "select SpecID,SpecName from StuffinfoSpec where StuffID='" + StuffID + "' ";
            IList<ListItem> list;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = new List<ListItem>();
                while (sdr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = sdr.IsDBNull(0) ? string.Empty : sdr.GetInt32(0).ToString();
                    li.Text = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    list.Add(li);
                }
            }
            return list;
        }
        public IList<ListItem> GetGHGoods()
        {
            string sql = "select StuffID,StuffName from StuffInfo";
            return GetListFromSql(sql);
        }

        public IList<ListItem> GetGHProjectList()
        {
            string sql = "SELECT SupplyID,SupplyName FROM dbo.SupplyInfo where IsUsed = 1 order by SupplyName ";
            return GetListFromSql(sql);
        }
        /// <summary>
        /// 获取材料出厂厂商
        /// </summary>
        /// <returns></returns>
        public IList<ListItem> GetStuffSupplyList(string SupplyType)
        {
            string sql = "";
            if (SupplyType != "")
            {
                sql = "SELECT SupplyID,SupplyName FROM SupplyInfo where IsUsed = 1 AND SupplyKind ='" + SupplyType + "' order by SupplyName";
            }
            else
            {
                sql = "SELECT SupplyID,SupplyName FROM SupplyInfo where IsUsed = 1 order by SupplyName";
            }
            return GetListFromSql(sql);
        }

        public IList<ListItem> GetTransferList()
        {
            string sql = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.SqlTransfer, string.Empty);
            return GetListFromSql(sql);
        }

        public IList<ListItem> GetSourceList()
        {
            string sql = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.SqlSource, string.Empty);
            return GetListFromSql(sql);
        }
        public IList<ListItem> GetTitleList()
        {
            string sql = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.SqlTitle, string.Empty);
            return GetListFromSql(sql);
        }

        public string getGHLastID()
        {
            string sql = "select max(lastid) from ShippingDocumentGH";
            string lastvalue = "0";
            using (DataSet ds= SqlHelper.ExecuteDataSet(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                lastvalue = ds.Tables[0].Rows[0][0].ToString();
            }
            if (lastvalue=="")
            {
                lastvalue = "0";
            }
            return lastvalue;
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
            string m_Insert = @"INSERT INTO StuffIn(StuffInID,StuffID,Spec,SiloID,SiloName,CarNo,SupplyID,GageUnit,TotalNum,CarWeight,WRate,InNum,InDate,Driver,AH,OutDate
                               ,Operator,pic1,pic2,pic3,pic4,DarkWeight,FastMetage,SourceAddr,BuildTime,Builder,ParentID,Proportion,OrderNum,TransportID,FootNum,
                               WeightName,MingWeight,RateMode,SupplyNum,CompanyID,SourceNumber,Lifecycle,FinalFootNum,Batch,SpecID)
                            values( 
                            @StuffInID,
                            @StuffID,
                            @Spec,
                            @SiloID,
                            @SiloName,
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
                            @BuildTime,@Builder,
                            @ParentStuffInID,
                            @Proportion,
                            @OrderNum,@TransportID,@FootNum,@WeightName,@MingWeight,@RateMode,@SupplyNum,@CompanyID,@SourceNumber,@Lifecycle,@FinalFootNum,@Batch,@SpecID); 
                        if(SELECT COUNT(*) FROM AutoGenerateId WHERE [Table]='StuffIn' AND IDPrefix=@IDPrefix)>0
                        BEGIN 
	                        update AutoGenerateId set NextValue=@NextValue where [Table]='StuffIn' and IDPrefix=@IDPrefix ;
                        END
                        ELSE
                        BEGIN 
	                        INSERT INTO AutoGenerateId VALUES('StuffIn',@IDPrefix,@NextValue);
                        END;
";
            //update AutoGenerateId set NextValue=@NextValue where [Table]='StuffIn' and IDPrefix=@IDPrefix 
            if (record.FastMetage == true)
            {
                m_Insert += " UPDATE dbo.Silo SET Content = Content + @InNum WHERE SiloID = @SiloID; UPDATE dbo.StuffInfo SET Inventory = Inventory + @InNum  WHERE StuffID = @StuffID;";
            }
            SqlParameter[] paras ={　
                                new SqlParameter("@StuffInID", record.StuffInID),
                                new SqlParameter("@StuffID", record.StuffID),
                                new SqlParameter("@Spec", record.Spec == null ? string.Empty : record.Spec),
                                new SqlParameter("@SiloID", record.SiloID),
                                new SqlParameter("@SiloName", record.SiloName),
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
                                record.SourceAddr==null||record.SourceAddr==""?new SqlParameter("@SourceAddr", DBNull.Value):new SqlParameter("@SourceAddr", record.SourceAddr),
                                record.OutDate ==null? new SqlParameter("@BuildTime", record.InDate) :new SqlParameter("@BuildTime", record.OutDate),
                                new SqlParameter("@Builder", record.Builder),
                                new SqlParameter("@ParentStuffInID",string.IsNullOrEmpty(record.ParentStuffInID) ?string.Empty: record.ParentStuffInID),
                                new SqlParameter("@Proportion", record.Proportion),
                                new SqlParameter("@OrderNum", record.OrderNum),
                                record.TransportID==null||record.TransportID==""?new SqlParameter("@TransportID", DBNull.Value):new SqlParameter("@TransportID", record.TransportID),
                                new SqlParameter("@FootNum", record.FootNum),
                                new SqlParameter("@WeightName", record.WeightName),
                                new SqlParameter("@MingWeight", record.MingWeight),
                                new SqlParameter("@RateMode", record.RateMode),
                                new SqlParameter("@NextValue", record.NextValue),
                                new SqlParameter("@IDPrefix", record.IDPrefix),
                                new SqlParameter("@SupplyNum", record.SupplyNum),
                                new SqlParameter("@CompanyID", record.CompanyID==null?"":record.CompanyID),
                                new SqlParameter("@SourceNumber", record.SourceNumber==null?"":record.SourceNumber),
                                new SqlParameter("@Lifecycle", record.FastMetage),
                                new SqlParameter("@Batch", record.Batch == null ? string.Empty : record.Batch),
                                new SqlParameter("@FinalFootNum", record.FinalFootNum),
                                record.SpecID==0?new SqlParameter("@SpecID", DBNull.Value):new SqlParameter("@SpecID", record.SpecID)
                               
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
                new SqlParameter("@ParentStuffInID",string.IsNullOrEmpty(record.ParentStuffInID) ?string.Empty: record.ParentStuffInID),
                new SqlParameter("@SupplyNum",record.SupplyNum),
                record.SpecID==0?new SqlParameter("@SpecID", DBNull.Value):new SqlParameter("@SpecID", record.SpecID)                
              };

            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.StoredProcedure, "ChangeSiloForMetage", paras) > 0;
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="stuffInId"></param>
        /// <returns></returns>
        public bool UpdateStuffIn(StuffIn record)
        {
            return UpStuffIn(record, 0);       
        }
        /// <summary>
        /// 修改入库（改变入库状态）-影响库存
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool UpdateStuffInAddState(StuffIn record)
        {
            return UpStuffIn(record, 1);
        }
        private bool UpStuffIn(StuffIn record, int flag)
        {
            DataSet ds = SqlHelper.ExecuteDataSet(m_ERPConnString, System.Data.CommandType.Text, "select InNum from StuffIn where StuffInID = '" + record.StuffInID + "'", null);
            string oldInNum = ds.Tables[0].Rows[0][0].ToString();
            
            string sql = "";
            if (flag == 0)//不改变状态
            {
                sql = @"update StuffIn set StuffID = @StuffID,Spec = @Spec,SiloID = @SiloID,SiloName = @SiloName,CarNo = @CarNo,SupplyID = @SupplyID";
            }
            else
            {
                sql = @"update StuffIn set StuffID = @StuffID,Spec = @Spec,SiloID = @SiloID,SiloName = @SiloName,CarNo = @CarNo,SupplyID = @SupplyID,Lifecycle=1";
            }
            sql += @",TotalNum = @TotalNum,CarWeight = @CarWeight,WRate = @WRate,InNum = @InNum,InDate = @InDate,Driver = @Driver";
            sql += @",OutDate = @OutDate,Operator =@Operator,pic1 =@pic1,pic2 =@pic2,pic3=@pic3,pic4 =@pic4,DarkWeight =@DarkWeight";
            sql += @",FastMetage = @FastMetage,Remark = @Remark ,SourceAddr = @SourceAddr,BuildTime = @BuildTime,TransportID = @TransportID,
                     FootNum=@FootNum,MingWeight=@MingWeight,RateMode=@RateMode,SupplyNum=@SupplyNum,CompanyID=@CompanyID,SourceNumber=@SourceNumber,
                     FinalFootNum=@FinalFootNum,Proportion=@Proportion ,Volume=@Volume ,Batch=@Batch where StuffInID = @StuffInID;";

            sql += @"UPDATE dbo.Silo SET Content = Content - @oldInNum + @InNum WHERE SiloID = @SiloID; UPDATE dbo.StuffInfo SET Inventory = Inventory - @oldInNum + @InNum  WHERE StuffID = @StuffID;";
            SqlParameter[] paras ={　
                        new SqlParameter("@StuffInID", record.StuffInID),
                        new SqlParameter("@StuffID", record.StuffID),
                        new SqlParameter("@Spec", record.Spec == null ? string.Empty : record.Spec),
                        new SqlParameter("@SiloID", record.SiloID == null ? string.Empty :record.SiloID),
                        new SqlParameter("@SiloName", record.SiloName == null ? string.Empty :record.SiloName),
                        new SqlParameter("@CarNo", record.CarNo),
                        new SqlParameter("@SupplyID", record.SupplyID),
                        new SqlParameter("@TotalNum", record.TotalNum),
                        new SqlParameter("@CarWeight", record.CarWeight),
                        new SqlParameter("@WRate", record.WRate),
                        new SqlParameter("@InNum", record.InNum),
                        new SqlParameter("@InDate", record.InDate), 
                        new SqlParameter("@Driver", record.Driver == null ? string.Empty : record.Driver),
                        new SqlParameter("@oldInNum", oldInNum == null ? 0 : Convert.ToDecimal(oldInNum)),
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
                        record.SourceAddr==""?new SqlParameter("@SourceAddr", DBNull.Value):new SqlParameter("@SourceAddr", record.SourceAddr),
                        record.OutDate == DateTime.MinValue ? new SqlParameter("@BuildTime", DBNull.Value) :new SqlParameter("@BuildTime", record.OutDate),
                        new SqlParameter("@MingWeight", record.MingWeight),
                        new SqlParameter("@RateMode", record.RateMode),
                        new SqlParameter("@CompanyID", record.CompanyID),
                        new SqlParameter("@SourceNumber", record.SourceNumber),
                        new SqlParameter("@SupplyNum", record.SupplyNum),
                        new SqlParameter("@FinalFootNum", record.FinalFootNum),
                        new SqlParameter("@Proportion", record.Proportion),
                        new SqlParameter("@Batch", record.Batch),
                        new SqlParameter("@Volume", record.Volume)
            };
            foreach (SqlParameter p in paras)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value;
                }
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras) > 0;  
        }
        /// <summary>
        /// 修改入库单-不影响库存
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool UpdateStuffInNoKc(StuffIn record)
        {
            string sql = "";
            sql = @"update StuffIn set StuffID = @StuffID,Spec = @Spec,SiloID = @SiloID,CarNo = @CarNo,SupplyID = @SupplyID";
            sql += @",TotalNum = @TotalNum,CarWeight = @CarWeight,WRate = @WRate,InNum = @InNum,InDate = @InDate,Driver = @Driver";
            sql += @",OutDate = @OutDate,Operator =@Operator,pic1 =@pic1,pic2 =@pic2,pic3=@pic3,pic4 =@pic4,DarkWeight =@DarkWeight";
            sql += @",FastMetage = @FastMetage,Remark = @Remark ,SourceAddr = @SourceAddr,BuildTime = @BuildTime,TransportID = @TransportID,
                     FootNum=@FootNum,MingWeight=@MingWeight,RateMode=@RateMode,SupplyNum=@SupplyNum,CompanyID=@CompanyID,SourceNumber=@SourceNumber,
                     FinalFootNum=@FinalFootNum where StuffInID = @StuffInID;";
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
                        new SqlParameter("@MingWeight", record.MingWeight),
                        new SqlParameter("@RateMode", record.RateMode),
                        new SqlParameter("@CompanyID", record.CompanyID),
                        new SqlParameter("@SourceNumber", record.SourceNumber),
                        new SqlParameter("@SupplyNum", record.SupplyNum),
                        new SqlParameter("@FinalFootNum", record.InNum)
            };
            foreach (SqlParameter p in paras)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value;
                }
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras) > 0;
        }
        /// <summary>
        /// 修改材料出厂记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool UpdateStuffSale(StuffSale record)
        {
            string sql = "update StuffSale set CarID = @CarID,CarNo = @CarNo,StuffID = @StuffID,StuffName = @StuffName,SupplyID = @SupplyID";
            sql += ",CompName = @CompName,TotalWeight = @TotalWeight,CarWeight = @CarWeight,Weight = @Weight,WeightMan = @WeightMan,WeightName = @WeightName";
            sql += ",ArriveTime = @ArriveTime,DeliveryTime =@DeliveryTime,Remark =@Remark,Modifier =@Modifier,ModifyTime =@ModifyTime";
            sql += "  where StuffSaleID = @StuffSaleID";
            SqlParameter[] paras ={　
                                new SqlParameter("@StuffSaleID", record.StuffSaleID),
                                new SqlParameter("@CarID", record.CarID == null ? string.Empty : record.CarID),
                                new SqlParameter("@CarNo", record.CarNo == null ? string.Empty : record.CarNo),
                                new SqlParameter("@StuffID", record.StuffID == null ? string.Empty :record.StuffID),
                                new SqlParameter("@StuffName", record.StuffName == null ? string.Empty : record.StuffName),
                                new SqlParameter("@SupplyID", record.SupplyID == null ? string.Empty : record.SupplyID),
                                new SqlParameter("@CompName", record.CompName == null ? string.Empty : record.CompName),
                                new SqlParameter("@TotalWeight", record.TotalWeight == null ? 0 : record.TotalWeight),
                                new SqlParameter("@CarWeight", record.CarWeight == null ? 0 : record.CarWeight),
                                new SqlParameter("@Weight", record.Weight == null ? 0 : record.Weight),
                                new SqlParameter("@WeightMan", record.WeightMan == null ? string.Empty : record.WeightMan),
                                new SqlParameter("@WeightName", record.WeightName == null ? string.Empty : record.WeightName),
                                new SqlParameter("@ArriveTime", record.ArriveTime ),
                                new SqlParameter("@DeliveryTime", record.DeliveryTime ),
                                new SqlParameter("@Remark", record.Remark == null ? string.Empty : record.Remark),
                                new SqlParameter("@Modifier", record.Modifier == null ? string.Empty : record.Modifier),
                                new SqlParameter("@ModifyTime", DateTime.Now)
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
        /// 检查是否存在当前销售单号
        /// </summary>
        /// <param name="stuffInId"></param>
        /// <returns></returns>
        public int checkExistStuffSaleID(string StuffSaleID)
        {
            string sql = "select count(*) from StuffSale  where StuffSaleID = '" + StuffSaleID + "'";
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
        /// 获取材料暗扣率
        /// </summary>
        /// <param name="stuffId"></param>
        /// <param name="supplyID"></param>
        /// <returns></returns>
        public decimal GetDarkRate(string stuffId, string supplyID)
        {
            IList<StockPack> list;
            string sql = string.Format(@"SELECT 0 AS Xh, StockPactID,PactName
                                        FROM StockPact 
                                        WHERE SupplyID='{0}' 
                                        AND (stuffId='{1}')
                                        UNION all
                                        SELECT 1,StockPactID,PactName
                                        FROM StockPact 
                                        WHERE SupplyID='{0}' 
                                        AND (stuffId1='{1}')
                                        UNION all
                                        select 2,StockPactID,PactName
                                        FROM StockPact 
                                        WHERE SupplyID='{0}' 
                                        AND (stuffId2='{1}')
                                        UNION all
                                        select 3,StockPactID,PactName
                                        FROM StockPact 
                                        WHERE SupplyID='{0}' 
                                        AND (stuffId3='{1}')
                                        UNION all
                                        select 4,StockPactID,PactName
                                        FROM StockPact 
                                        WHERE SupplyID='{0}' 
                                        AND (stuffId4='{1}')", supplyID, stuffId);
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = new List<StockPack>();
                while (sdr.Read())
                {

                    StockPack stockPack = new StockPack();
                    stockPack.Xh = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0);
                    stockPack.StockPactID = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    stockPack.PactName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                    list.Add(stockPack);
                }
                string no = "";
                if (list.Count == 0)
                {
                    no = "";
                }
                else
                {
                    no = list[0].Xh.ToString() == "0" ? "" : list[0].Xh.ToString();
                }
                sql = string.Format(@"SELECT isnull(DarkRate{0},0) FROM StockPact  WHERE SupplyID='{1}' AND (stuffId{0}='{2}')", no, supplyID, stuffId);
                DataTable dt = SqlHelper.ExecuteDataSet(m_ERPConnString, System.Data.CommandType.Text, sql, null).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDecimal(dt.Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 获取原材料合同列表
        /// </summary>
        /// <param name="stuffId"></param>
        /// <param name="supplyID"></param>
        /// <returns></returns>
        public IList<StockPack> GetStockPackList(string stuffId, string supplyID)
        {
            IList<StockPack> list;
            string sql = "select StockPactID,PactName from StockPact where SupplyID='" + supplyID + "' and (stuffId='" + stuffId + "' or stuffId1='" + stuffId + "' or stuffId2='" + stuffId + "' or stuffId3='" + stuffId + "' or stuffId4='" + stuffId + "')";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = new List<StockPack>();
                while (sdr.Read())
                {

                    StockPack stockPack = new StockPack();
                    stockPack.StockPactID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    stockPack.PactName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    list.Add(stockPack);
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
            string sql = "SELECT SiloID,SiloName,StuffID,Content FROM Silo where StuffID is not null and StuffID <>'' and isUsed = 1";
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
            if (string.IsNullOrEmpty(si.StuffInID))//增加
            {
                PublicHelper ph = new PublicHelper();               
                si.StuffInID = ph.getMetageNo(si);
                
                if (this.AddStuffIn(si))
                {                    
                    LocalDataProvider ldp = new LocalDataProvider();
                    Config c = new Config();
                    AutoGenerateHelper agh = new AutoGenerateHelper();
                    string prefixPat = agh.GetPrefix(c.prefixPat);//前缀

                    //生成进出库单号
                    int LastValue = agh.getLastValue(prefixPat, c.idLen) + si.NextValue;
                    ldp.remarkAutoGenerateID(prefixPat, c.idLen, LastValue);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else//修改
            {
                return this.UpdateStuffIn(si);
            }         
        }

        //过皮的保存单据
        public bool SaveStuffInGP(StuffIn si)
        {
            if (string.IsNullOrEmpty(si.StuffInID))//增加
            {
                PublicHelper ph = new PublicHelper();
                si.StuffInID = ph.getMetageNo(si);
                if (this.AddStuffIn(si))
                {
                    LocalDataProvider ldp = new LocalDataProvider();
                    Config c = new Config();
                    AutoGenerateHelper agh = new AutoGenerateHelper();
                    string prefixPat = agh.GetPrefix(c.prefixPat);//前缀

                    //生成进出库单号
                    int LastValue = agh.getLastValue(prefixPat, c.idLen) + si.NextValue;
                    ldp.remarkAutoGenerateID(prefixPat, c.idLen, LastValue);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else//修改
            {
                return this.UpdateStuffInAddState(si);
            }
        }
        /// <summary>
        /// 取得记录
        /// </summary>
        /// <returns></returns>
        public IList<AutoGenerate> GetAutoGenerateList(string where)
        {
            string sql = "select * from AutoGenerateId where " + where;
            IList<AutoGenerate> list;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = ReaderToAutoGenerateList(sdr);
            }
            return list;
        }
        IList<AutoGenerate> ReaderToAutoGenerateList(SqlDataReader sdr)
        {
            IList<AutoGenerate> list = new List<AutoGenerate>();
            while (sdr.Read())
            {
                AutoGenerate record = new AutoGenerate();
                record.Table = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.IDPrefix = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                record.NextValue = sdr.IsDBNull(2) ? 0 : sdr.GetInt32(2);
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// 取得所有入库记录
        /// </summary>
        /// <returns></returns>
        public IList<StuffIn> GetStuffInList()
        {
            string sql = m_Select + m_From + " WHERE lifecycle =0 ORDER BY indate desc,StuffINId DESC"; 
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
                record.MingWeight = sdr.IsDBNull(28) ? 0 : sdr.GetDecimal(28);
                record.CompanyID = sdr.IsDBNull(29) ? string.Empty : sdr.GetInt32(29).ToString();
                record.SourceNumber = sdr.IsDBNull(30) ? string.Empty : sdr.GetString(30);
                record.SourceAddr = sdr.IsDBNull(31) ? string.Empty : sdr.GetString(31);
                record.SupplyNum = sdr.IsDBNull(32) ? 0 : sdr.GetDecimal(32);
                record.Volume = sdr.IsDBNull(33) ? 0 : sdr.GetDecimal(33);
                record.SpecID = sdr.IsDBNull(34) ? 0 : sdr.GetInt32(34);
                record.Batch = sdr.IsDBNull(35) ? string.Empty : sdr.GetString(35);
                list.Add(record);

            }
            return list;
        }

        IList<StuffIn> ReaderToMetageStuffInList(SqlDataReader sdr)
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
                record.TransportName = sdr.IsDBNull(16) ? string.Empty : sdr.GetString(16);
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
                record.MingWeight = sdr.IsDBNull(28) ? 0 : sdr.GetDecimal(28);
                record.CompanyID = sdr.IsDBNull(29) ? string.Empty : sdr.GetInt32(29).ToString();
                record.SourceNumber = sdr.IsDBNull(30) ? string.Empty : sdr.GetString(30);
                record.CompanyName = sdr.IsDBNull(31) ? string.Empty : sdr.GetString(31);
                record.Remark = sdr.IsDBNull(32) ? string.Empty : sdr.GetString(32);
                record.SourceAddr = sdr.IsDBNull(33) ? string.Empty : sdr.GetString(33);
                record.Volume = sdr.IsDBNull(34) ? 0 : sdr.GetDecimal(34);
                record.SpecID = sdr.IsDBNull(35) ? 0 : sdr.GetInt32(35);
                record.Batch = sdr.IsDBNull(36) ? string.Empty : sdr.GetString(36);
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


        private IList<StuffSale> ReaderToStuffSaleList(SqlDataReader sdr)
        {
            IList<StuffSale> list = new List<StuffSale>();

            while (sdr.Read())
            {
                StuffSale record = new StuffSale();
                record.StuffSaleID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                record.CarID = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                record.CarNo = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                record.StuffID = sdr.IsDBNull(3) ? string.Empty : sdr.GetString(3);
                record.StuffName = sdr.IsDBNull(4) ? string.Empty : sdr.GetString(4);
                record.SupplyID = sdr.IsDBNull(5) ? string.Empty : sdr.GetString(5);
                record.CompName = sdr.IsDBNull(6) ? string.Empty : sdr.GetString(6);
                record.TotalWeight = sdr.IsDBNull(7) ? 0 : sdr.GetInt32(7);
                record.CarWeight = sdr.IsDBNull(8) ? 0 : sdr.GetInt32(8);
                record.Weight = sdr.IsDBNull(9) ? 0 : sdr.GetInt32(9);
                record.WeightMan = sdr.IsDBNull(10) ? string.Empty : sdr.GetString(10);
                record.WeightName = sdr.IsDBNull(11) ? string.Empty : sdr.GetString(11);
                record.ArriveTime = sdr.GetDateTime(12);
                record.DeliveryTime = sdr.GetDateTime(13);
                record.Remark = sdr.IsDBNull(14) ? string.Empty : sdr.GetString(14);
                record.Builder = sdr.IsDBNull(15) ? string.Empty : sdr.GetString(15);
                record.BuildTime = sdr.GetDateTime(16);
                record.CompanyID = sdr.IsDBNull(17) ? string.Empty : sdr.GetString(17);
                //record.ModifyTime = sdr.GetDateTime(18);
                list.Add(record);

            }
            return list;
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
        /// <summary>
        /// 读取数据到运输单
        /// </summary>
        /// <param name="sdr"></param>
        /// <returns></returns>
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
                if (sdr.IsDBNull(22))
                {
                    record.ConstrengthExchange = null;
                }
                else
                {
                    record.ConstrengthExchange = sdr.GetInt32(22);
                }
                //record.ConstrengthExchange = sdr.GetInt32(22);
                record.TotalWeight = sdr.IsDBNull(23) ? 0 : sdr.GetInt32(23);
                record.CarWeight = sdr.IsDBNull(24) ? 0 : sdr.GetInt32(24);
                record.Weight = sdr.IsDBNull(25) ? 0 : sdr.GetInt32(25);

                record.Exchange = sdr.IsDBNull(26) ? 0 : Convert.ToDecimal(sdr["Exchange"]);
                record.Cube = sdr.IsDBNull(27) ? 0 : sdr.GetDecimal(27);
                //Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime
                record.Driver = sdr.IsDBNull(28) ? string.Empty : sdr.GetString(28);
                record.Tel = sdr.IsDBNull(29) ? string.Empty : sdr.GetString(29);
                record.ImpGrade = sdr.IsDBNull(30) ? string.Empty : sdr.GetString(30);
                record.DeliveryTime = sdr.IsDBNull(31) ? DateTime.Now : sdr.GetDateTime(31);
                record.Title = sdr.IsDBNull(32) ? string.Empty : sdr.GetString(32);

                record.Remark = sdr.IsDBNull(33) ? string.Empty : sdr.GetString(33);
                record.WeightMan = sdr.IsDBNull(34) ? string.Empty : sdr.GetString(34);

                record.ShipDocType = sdr.IsDBNull(35) ? string.Empty : sdr.GetString(35);
                record.PlanCube = sdr.IsDBNull(36) ? 0 : sdr.GetDecimal(36);
                record.SlurryCount = sdr.IsDBNull(37) ? 0 : sdr.GetDecimal(37);
                record.ShippingCube = sdr.IsDBNull(38) ? 0 : sdr.GetDecimal(38);
                record.LinkMan = sdr.IsDBNull(39) ? string.Empty : sdr.GetString(39);
                record.Salesman = sdr.IsDBNull(40) ? string.Empty : sdr.GetString(40);
                //record.SlurryExchange = sdr.GetInt32(41);
                record.Operator = sdr.IsDBNull(42) ? string.Empty : sdr.GetString(42);
                record.FormulaName = sdr.IsDBNull(43) ? string.Empty : sdr.GetString(43);
                record.ConsMixpropID = sdr.IsDBNull(44) ? string.Empty : sdr.GetString(44);
                record.Surveyor = sdr.IsDBNull(45) ? string.Empty : sdr.GetString(45);
                record.OtherDemand = sdr.IsDBNull(46) ? string.Empty : sdr.GetString(46);
                record.Distance = sdr.IsDBNull(47) ? 0 : Convert.ToDecimal(sdr.GetDecimal(47));
                record.DataType = sdr.IsDBNull(48) ? 0 : sdr.GetInt32(48);
                list.Add(record);

            }
            return list;
        }

        /// <summary>
        /// 读取数据到运输单
        /// </summary>
        /// <param name="sdr"></param>
        /// <returns></returns>
        private IList<ShippingDocumentGH> ReaderToShippingDocumentListGH(SqlDataReader sdr)
        {
            IList<ShippingDocumentGH> list = new List<ShippingDocumentGH>();

            while (sdr.Read())
            {
                ShippingDocumentGH record = new ShippingDocumentGH();
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
                record.ProduceDate = sdr.GetDateTime(19);
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
                if (sdr.IsDBNull(22))
                {
                    record.ConstrengthExchange = null;
                }
                else
                {
                    record.ConstrengthExchange = sdr.GetInt32(22);
                }
                //record.ConstrengthExchange = sdr.GetInt32(22);
                record.TotalWeight = sdr.IsDBNull(23) ? 0 : sdr.GetInt32(23);
                record.CarWeight = sdr.IsDBNull(24) ? 0 : sdr.GetInt32(24);
                record.Weight = sdr.IsDBNull(25) ? 0 : sdr.GetInt32(25);

                record.Exchange = sdr.IsDBNull(26) ? 0 : Convert.ToDecimal(sdr["Exchange"]);
                record.Cube = sdr.IsDBNull(27) ? 0 : sdr.GetDecimal(27);
                //Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime
                record.Driver = sdr.IsDBNull(28) ? string.Empty : sdr.GetString(28);
                record.Tel = sdr.IsDBNull(29) ? string.Empty : sdr.GetString(29);
                record.ImpGrade = sdr.IsDBNull(30) ? string.Empty : sdr.GetString(30);
                record.DeliveryTime = sdr.IsDBNull(31) ? DateTime.Now : sdr.GetDateTime(31);
                record.Title = sdr.IsDBNull(32) ? string.Empty : sdr.GetString(32);

                record.Remark = sdr.IsDBNull(33) ? string.Empty : sdr.GetString(33);
                record.WeightMan = sdr.IsDBNull(34) ? string.Empty : sdr.GetString(34);

                record.ShipDocType = sdr.IsDBNull(35) ? string.Empty : sdr.GetString(35);
                record.PlanCube = sdr.IsDBNull(36) ? 0 : sdr.GetDecimal(36);
                record.SlurryCount = sdr.IsDBNull(37) ? 0 : sdr.GetDecimal(37);
                record.ShippingCube = sdr.IsDBNull(38) ? 0 : sdr.GetDecimal(38);
                record.LinkMan = sdr.IsDBNull(39) ? string.Empty : sdr.GetString(39);
                record.Salesman = sdr.IsDBNull(40) ? string.Empty : sdr.GetString(40);
                //record.SlurryExchange = sdr.GetInt32(41);
                record.Operator = sdr.IsDBNull(42) ? string.Empty : sdr.GetString(42);
                record.CustSiloNo = sdr.IsDBNull(43) ? string.Empty : sdr.GetString(43);
                record.FormulaName = sdr.IsDBNull(44) ? string.Empty : sdr.GetString(44);
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
                record.Exchange = sdr.IsDBNull(22) ? 0 : sdr.GetDecimal(22);
                record.BuildTime = sdr.GetDateTime(23);
                record.Type = sdr.IsDBNull(24) ? string.Empty : sdr.GetString(24);
               
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
            //string s_where = "  where  InNum > 0  and lifecycle =1 ";
            string s_where = "  where  lifecycle =1 ";

            if (!("").Equals(CarNo))
            {
                s_where = s_where + "  and      CarNo  = '" + CarNo + "'";
            }

            string sql = m_Select + " from MetageStuffIn as StuffIn " + s_where + " and OutDate>'"+DateTime.Now.ToString("yyyyMMdd")+"' order by OutDate desc";

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
            string sql = @"SELECT StuffInID,CarNo,totalNum,CarWeight,InDate,OutDate,Operator,(SELECT TOP 1 stuffName FROM stuffInfo 
                            WHERE stuffInfo.StuffID = StuffIn.StuffID) AS StuffName,StuffID,siloID, siloName,SupplyID,(SELECT TOP 1 SupplyName FROM SupplyInfo 
                            WHERE SupplyInfo.SupplyID = StuffIn.SupplyID) AS SupplyName,TransportID,WRate,Proportion,(SELECT TOP 1 SupplyName FROM SupplyInfo 
                            WHERE SupplyInfo.SupplyID = StuffIn.TransportID) as TransportName,Driver,InNum,Pic1,Pic2,Pic3,Pic4,DarkWeight,FastMetage,(SELECT SpecName FROM StuffinfoSpec WHERE SpecID=StuffIn.SpecID ) as Spec,ParentID,
                            (select top 1 FinalStuffType from StuffType inner join StuffInfo on StuffType.StuffTypeID = StuffInfo.StuffTypeID and StuffInfo.StuffID = StuffIn.StuffID) as FinalStuffType 
                            ,MingWeight,CompanyID,SourceNumber,CompName as CompanyName,Remark,SourceAddr,Volume,SpecID,Batch";//,TransferName,KZL 
            //string s_where = "  where  (OutDate between  '" + BeginTime + "' and   '" + EndTime + "'  )  and InNum > 0  and lifecycle <> -1 and lifecycle <> 2";
            string s_where = "  where  (OutDate between  '" + BeginTime + "' and   '" + EndTime + "'  )   and lifecycle <> 0 and lifecycle <> 2";
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
            sql = sql + " from MetageStuffIn_Report as StuffIn " + s_where;
            IList<StuffIn> list = new List<StuffIn>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                list = ReaderToMetageStuffInList(sdr);
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

        /// <summary>
        /// 材料销售出厂统计
        /// </summary>
        /// <param name="where"></param>
        /// <param name="timewhere"></param>
        /// <returns></returns>
        public IList<StuffSale> getStuffSaleQuery(string where)
        {
            string sql = "select * from StuffSale " + where;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<StuffSale> list = ReaderToStuffSaleList(sdr);

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

        /// <summary>
        /// 获取运输单信息（根据车号）
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        public ShippingDocument GetShippingDocumentByCarId(string CarId)
        {
            string sql = "select top 1 ShipDocID,ShippingDocument.TaskID, ShippingDocument.ContractID, ShippingDocument.ContractName, ShippingDocument.CustomerID, ShippingDocument.ConstructUnit,RealSlump,CustName,ProjectName,ShippingDocument.ProjectAddr,ShippingDocument.ConStrength,ProductLineName,ShippingDocument.CastMode,ConsPos";
            sql += ",PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,ShippingDocument.CarID,GetDate() as ServerTime ,CAST(Consmixprop.Weight AS INT)  as ConstrengthExchange";
            sql += ", TotalWeight, ShippingDocument.CarWeight,ShippingDocument.Weight , CAST(ShippingDocument.Exchange AS decimal(18,2)) Exchange,cube,Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime,Title,ShippingDocument.Remark,WeightMan,ShipDocType,PlanCube,SlurryCount,ShippingCube";
            sql += ", LinkMan, Contract.Salesman ,isnull(CAST(Cons.Weight AS INT),0) as SlurryExchange,Operator,ShippingDocument.FormulaName,ShippingDocument.ConsMixpropID,Surveyor,OtherDemand,ShippingDocument.Distance,ShippingDocument.DataType";
            sql += " from ShippingDocument ";
            sql += " left join Consmixprop on ShippingDocument.ConsmixpropId = Consmixprop.ConsmixpropId  left join Formula on Consmixprop.formulaID = Formula.FormulaID";
            sql += " left join Consmixprop Cons on ShippingDocument.TaskID = Cons.TaskID AND Cons.ProductLineID=  ShippingDocument.ProductLineID AND Cons.IsSlurry=1";
            sql += " left join Contract on ShippingDocument.ContractID = Contract.ContractID ";
            sql += " left join Car on ShippingDocument.CarID = Car.CarID ";
            sql += " where ShippingDocument.CarID ='" + CarId + "' and IsEffective = 1   order by ShippingDocument.BuildTime desc ";
            //sql += " where ShippingDocument.CarID ='" + CarId + "' and IsEffective = 1 and IsProduce=1  order by ShippingDocument.BuildTime desc ";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocument> list = ReaderToShippingDocumentList(sdr);
                if (list.Count > 0)
                    return list[0];
                else
                    return null;

            }
        }

        /// <summary>
        /// 获取运输单信息（根据运输单号）
        /// </summary>
        /// <param name="ShipDocID"></param>
        /// <returns></returns>
        public ShippingDocument GetShippingDocumentByShipId(string ShipDocID)
        {
            string sql = "select top 1 ShipDocID,ShippingDocument.TaskID, ShippingDocument.ContractID, ShippingDocument.ContractName, ShippingDocument.CustomerID, ShippingDocument.ConstructUnit,RealSlump,CustName,ProjectName,ShippingDocument.ProjectAddr,ShippingDocument.ConStrength,ProductLineName,ShippingDocument.CastMode,ConsPos";
            sql += ",PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,ShippingDocument.CarID,GetDate() as ServerTime ,CAST(Consmixprop.Weight AS INT)  as ConstrengthExchange";
            sql += ", TotalWeight, ShippingDocument.CarWeight,ShippingDocument.Weight , CAST(ShippingDocument.Exchange AS decimal(18,2)) Exchange,cube,Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime,Title,ShippingDocument.Remark,WeightMan,ShipDocType,PlanCube,SlurryCount,ShippingCube";
            sql += ", LinkMan, Contract.Salesman ,isnull(CAST(Cons.Weight AS INT),0) as SlurryExchange,Operator,ShippingDocument.FormulaName,ShippingDocument.ConsMixpropID,Surveyor,OtherDemand,ShippingDocument.Distance,ShippingDocument.DataType";
            sql += " from ShippingDocument ";
            sql += " left join Consmixprop on ShippingDocument.ConsmixpropId = Consmixprop.ConsmixpropId  left join Formula on Consmixprop.formulaID = Formula.FormulaID";
            sql += " left join Consmixprop Cons on ShippingDocument.TaskID = Cons.TaskID AND Cons.ProductLineID=  ShippingDocument.ProductLineID AND Cons.IsSlurry=1";
            sql += " left join Contract on ShippingDocument.ContractID = Contract.ContractID ";
            sql += " left join Car on ShippingDocument.CarID = Car.CarID ";
            sql += " where ShippingDocument.ShipDocID ='" + ShipDocID + "' and IsEffective = 1   order by ShippingDocument.BuildTime desc ";
            //sql += " where ShippingDocument.CarID ='" + CarId + "' and IsEffective = 1 and IsProduce=1  order by ShippingDocument.BuildTime desc ";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocument> list = ReaderToShippingDocumentList(sdr);
                if (list.Count > 0)
                    return list[0];
                else
                    return null;

            }
        }

        /// <summary>
        /// 获取运输单信息（根据车号）
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        public ShippingDocumentGH GetShippingDocumentByCarIdGH(string CarId)
        {
            string sql = "select top 1 ShipDocID,ShippingDocument.TaskID, ShippingDocument.ContractID, ShippingDocument.ContractName, ShippingDocument.CustomerID, ShippingDocument.ConstructUnit,RealSlump,CustName,ProjectName,ShippingDocument.ProjectAddr,ShippingDocument.ConStrength,ProductLineName,ShippingDocument.CastMode,ConsPos, LinkMan,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,ShippingDocument.CarID,GetDate() as ServerTime ,CAST(Consmixprop.Weight AS INT)  as ConstrengthExchange, TotalWeight, ShippingDocument.CarWeight,ShippingDocument.Weight , CAST(ShippingDocument.Exchange AS decimal(18,2)) Exchange,cube,Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime,Title,ShippingDocument.Remark,WeightMan,ShipDocType,PlanCube,SlurryCount,ShippingCube, LinkMan, Contract.Salesman ,isnull(CAST(Cons.Weight AS INT),0) as SlurryExchange,Operator,CustSiloNo,FormulaName from ShippingDocumentGH ShippingDocument left join ConsMixpropGH Consmixprop on ShippingDocument.ConsmixpropId = Consmixprop.ConsmixpropId  left join FormulaGH on Consmixprop.formulaID = FormulaGH.FormulaGHID  left join ConsMixpropGH Cons on ShippingDocument.TaskID = Cons.TaskID AND Cons.ProductLineID=  ShippingDocument.ProductLineID AND Cons.IsSlurry=1  left join ContractGH Contract on ShippingDocument.ContractID = Contract.ContractID  left join Car on ShippingDocument.CarID = Car.CarID ";
            sql += " where ShippingDocument.CarID ='" + CarId + "' and IsEffective = 1  order by ShippingDocument.BuildTime desc ";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocumentGH> list = ReaderToShippingDocumentListGH(sdr);
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
            sql += "ConsPos,PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,tzralation.TotalWeight,tzralation.CarWeight,tzralation.Weight,tzralation.Exchange,tzralation.BuildTime  ,Dic.DicName ReturnType";
            sql += " from tzralation left join shippingDocument on tzralation.SourceShipDocID = shippingDocument.shipDocId LEFT JOIN DIC ON DicID=tzralation.ReturnType where tzralation.TZRalationID =" + id.ToString();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<TZrelation> list = ReaderToTzRelationList(sdr);
                if (list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }
        public TZrelation GetFHTzRelationByCarId(string carid)
        {
            string sql = "select top 1 TZRalationID,Cube,IsLock from TZRalation where IsCompleted=0 and CarID='" + carid + "' and (IsLock=1 or IsLock=2) order by TZRalationID desc";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<TZrelation> list = new List<TZrelation>();

                while (sdr.Read())
                {
                    TZrelation record = new TZrelation();
                    record.TZRalationID = (sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0)).ToString();
                    record.Cube = sdr.IsDBNull(1) ? 0 : sdr.GetDecimal(1);
                    record.IsLock = sdr.IsDBNull(2) ? 0 : Convert.ToInt32(sdr.GetString(2));

                    list.Add(record);

                }
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
            string sql = @" update tzralation set Cube=@Cube,Builder=@Builder, BuildTime=getDate(), AH='Auto', TotalWeight=@TotalWeight, CarWeight=@CarWeight, Weight=@Weight ,Exchange=@Exchange where TZRalationID='" + tz.TZRalationID + "'";
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
                //return AddTzRelationHistory(tz, getTZID(tz.CarID));
                return 1;
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
                                                    Builder, BuildTime, AH, TotalWeight, CarWeight, Weight ,Exchange,IsLock,ReturnType,ActionType,TzRalationFlag,DataType)
                            values(@SourceShipDocID, @Cube,@Cube, @CarID, 0, 0 ,@Builder,getDate(),'Auto',@TotalWeight,@CarWeight,
                                   @Weight,@Exchange,0,@ReturnType,'AT1',@TzRalationFlag,@DataType )  select SCOPE_IDENTITY()";
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
                                new SqlParameter("@TzRalationFlag",tz.TzRalationFlag) ,
                                new SqlParameter("@DataType",tz.DataType) 
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
                sql += "Exchange=" + sd.Exchange + ",DeliveryTime='" + DateTime.Now + "',Title='" + sd.Title + "',Cube =" + sd.Cube + ",ParCube=" + sd.Cube + ",SignInCube =" + sd.Cube + ",ProvidedCube =  (select top 1 ProvidedCube from ProduceTasks where ProduceTasks.taskid='" + sd.TaskID + "') ,";
                sql += "ProvidedTimes = (select top 1 ProvidedTimes from ProduceTasks where ProduceTasks.taskid='" + sd.TaskID + "') where shipDocId ='" + sd.ShipDocID + "' COMMIT TRANSACTION";

            }
            else
            {
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Exchange=" + sd.Exchange + ",Cube =" + sd.Cube + "  where shipDocId ='" + sd.ShipDocID + "'";
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null) > 0;
        }
        /// <summary>
        /// ProvidedTimes\ProvidedCube直接传递
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public bool UpdateShippingDocument2(ShippingDocument sd)
        {
            string sql = string.Empty;
            bool IsUseWeightTime = Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, "UseWeightTime", string.Empty));
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, "UseMetageCube", string.Empty)))
            {
                sql += "BEGIN TRANSACTION UPDATE ProduceTasks SET ProvidedTimes =  " + sd.ProvidedTimes + ",ProvidedCube=" + sd.ProvidedCube + " where TaskID ='" + sd.TaskID + "'";
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Exchange=" + sd.Exchange + ",Title='" + sd.Title + "',Cube =" + sd.Cube + ",ParCube=" + sd.Cube + ",CarID='" + sd.CarID
                    + "',SignInCube =" + sd.Cube + ",ProvidedTimes=" + sd.ProvidedTimes + ",ProvidedCube =  " + sd.ProvidedCube + ",OtherDemand='"+sd.OtherDemand + "',Remark ='" + sd.Remark + "',WeightMan='" + sd.WeightMan + "',WeightName='" + sd.WeightName + "'";
                if (IsUseWeightTime)
                {
                    sql += ",DeliveryTime='" + sd.DeliveryTime + "'";
                }
                sql += " where shipDocId ='" + sd.ShipDocID + "' COMMIT TRANSACTION";
            }
            else
            {
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",WeightMan='" + sd.WeightMan + "',WeightName='" + sd.WeightName + "',OtherDemand='" + sd.OtherDemand+ "',Remark ='" + sd.Remark + "',";
                if (IsUseWeightTime)
                {
                    sql += "DeliveryTime='" + sd.DeliveryTime + "',";
                }
                sql += "Exchange=" + sd.Exchange + ",Cube =" + sd.Cube + "  where shipDocId ='" + sd.ShipDocID + "'";
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null) > 0;
        }
        /// <summary>
        /// ProvidedTimes\ProvidedCube直接传递
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public bool UpdateShippingDocument2GH(ShippingDocumentGH sd)
        {
            string sql = string.Empty;
            bool IsUseWeightTime = Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, "UseWeightTime", string.Empty));
            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, "UseMetageCube", string.Empty)))
            {
                sql += "BEGIN TRANSACTION UPDATE ProduceTasks SET ProvidedTimes =  " + sd.ProvidedTimes + ",ProvidedCube=" + sd.ProvidedCube + " where TaskID ='" + sd.TaskID + "'";
                sql += "update shippingDocumentGH set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Exchange=" + sd.Exchange + ",Title='" + sd.Title + "',Cube =" + sd.Cube + ",ParCube=" + sd.Cube + ",CarID='" + sd.CarID
                    + "',SignInCube =" + sd.Cube + ",ProvidedTimes=" + sd.ProvidedTimes + ",ProvidedCube =  " + sd.ProvidedCube + ",Remark ='" + sd.Remark + "',WeightMan='" + sd.WeightMan + "',WeightName='" + sd.WeightName + "',CustSiloNo='" + sd.CustSiloNo + "'";
                if (IsUseWeightTime)
                {
                    sql += ",DeliveryTime='" + sd.DeliveryTime + "'";
                }
                sql += " where shipDocId ='" + sd.ShipDocID + "' COMMIT TRANSACTION";
            }
            else
            {
                sql += "update shippingDocumentGH set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",WeightMan='" + sd.WeightMan + "',WeightName='" + sd.WeightName + "',CustSiloNo='" + sd.CustSiloNo + "',";
                if (IsUseWeightTime)
                {
                    sql += "DeliveryTime='" + sd.DeliveryTime + "',";
                }
                sql += "Exchange=" + sd.Exchange + ",Cube =" + sd.Cube + "  where shipDocId ='" + sd.ShipDocID + "'";
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null) > 0;
        }

        public bool UpdateShippingRecord(ShippingDocument sd, decimal addCube)
        {
            string sql = string.Empty;

            if (Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, "UseMetageCube", string.Empty)))
            {
                sql += "BEGIN TRANSACTION UPDATE ProduceTasks SET ProvidedCube = ProvidedCube + " + addCube + " where TaskID ='" + sd.TaskID + "'";
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Title='" + sd.Title + "',Cube =" + sd.Cube + ",ParCube=" + sd.Cube + ",CarID=" + sd.CarID + ",SignInCube =" + sd.Cube + ",ProvidedCube =  ProvidedCube + " + addCube;
                sql += " where shipDocId ='" + sd.ShipDocID + "' COMMIT TRANSACTION";

            }
            else
            {
                sql += "update shippingDocument set TotalWeight= " + sd.TotalWeight.ToString() + ", CarWeight = " + sd.CarWeight.ToString() + " ,Weight = " + sd.Weight + ",";
                sql += "Exchange=" + sd.Exchange + ",Cube =" + sd.Cube + "  where shipDocId ='" + sd.ShipDocID + "'";
            }
            return SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null) > 0;
        }

        /// <summary>
        /// 更新回厂时间
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public bool UpdateArriveTime(string ShipDocID)
        {
            string sql = @"update ShippingDocument set ArriveTime=getdate() where ShipDocID='" + ShipDocID + "'";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            return count > 0 ? true : false;
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
            string sql = " select CarID from Car where isUsed = 1  and  CarTypeID in ('CT8','CT1')  order by CarID ";
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
        /// 在ERP内取得所有可用的车辆信息
        /// </summary>
        /// <returns></returns>
        public IList<ListItem> getCarListGH()
        {
            string sql = " select CarID from Car where isUsed = 1  and  CarTypeID = 'CT8' order by CarID ";
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
        /// 获取材料出厂车辆
        /// </summary>
        /// <param name="StuffCarType"></param>
        /// <returns></returns>
        public IList<ListItem> getStuffCarList(string StuffCarType)
        {
            string sql = "";
            if (StuffCarType != "")
            {
                sql = " select CarID,CarNo from Car where isUsed = 1  and  CarTypeID = '" + StuffCarType + "' order by CarID ";
            }
            else {
                sql = " select CarID,CarNo from Car where isUsed = 1  order by CarID ";
            }
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
            string sql = "select ShipDocID,ShippingDocument.TaskID, ShippingDocument.ContractID, ShippingDocument.ContractName, ShippingDocument.CustomerID, ShippingDocument.ConstructUnit,RealSlump,CustName,ProjectName,ShippingDocument.ProjectAddr,ShippingDocument.ConStrength,ProductLineName,ShippingDocument.CastMode,ConsPos";
            sql += ",PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,CarID,GetDate() as ServerTime ,CAST(Consmixprop.Weight AS INT)  as ConstrengthExchange";
            sql += ", TotalWeight, CarWeight,ShippingDocument.Weight , ShippingDocument.Exchange,cube,Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime,Title,ShippingDocument.Remark,WeightMan,ShipDocType,PlanCube,SlurryCount,ShippingCube";
            sql += ", LinkMan, Contract.Salesman,0,Operator ,ShippingDocument.FormulaName,ShippingDocument.ConsMixpropID,Surveyor,OtherDemand,ShippingDocument.Distance,ShippingDocument.DataType";
            sql += " from ShippingDocument ";
            sql += " left join Consmixprop on ShippingDocument.ConsmixpropId = Consmixprop.ConsmixpropId  left join Formula on Consmixprop.formulaID = Formula.FormulaID";
            sql += " left join Contract on ShippingDocument.ContractID = Contract.ContractID ";
            sql += " where cube > 0 and IsEffective = 1 and ProduceDate between '" + begin.ToString() + "'";
            sql += " and '"+ end.ToString() +"'";
            if (!string.IsNullOrEmpty(CarID))
            {
                sql += " and CarId = '" + CarID.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(Constrength))
            {
                sql += " and ShippingDocument.Constrength ='" + Constrength.Trim() + "'";
            }
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocument> list = ReaderToShippingDocumentList(sdr);
                return list;
            }
        }

        /// <summary>
        /// 查询干混出厂过磅记录
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="CarID">车辆编号</param>
        /// <param name="Constrength">砼强度</param>
        /// <returns></returns>
        public IList<ShippingDocumentGH> getShippingRecordGH(DateTime begin, DateTime end, string CarID, string Constrength)
        {
            string sql = "select ShipDocID,ShippingDocument.TaskID, ShippingDocument.ContractID, ShippingDocument.ContractName, ShippingDocument.CustomerID, ShippingDocument.ConstructUnit,RealSlump,CustName,ProjectName,ShippingDocument.ProjectAddr,ShippingDocument.ConStrength,ProductLineName,ShippingDocument.CastMode,ConsPos";
            sql += ",LinkMan,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,CarID,GetDate() as ServerTime ,CAST(Consmixprop.Weight AS INT)  as ConstrengthExchange";
            sql += ", TotalWeight, CarWeight,ShippingDocument.Weight , ShippingDocument.Exchange,cube,Driver,Tel,ShippingDocument.ImpGrade,DeliveryTime,Title,ShippingDocument.Remark,WeightMan,ShipDocType,PlanCube,SlurryCount,ShippingCube";
            sql += ", LinkMan, Contract.Salesman,0,Operator, CustSiloNo, ShippingDocument.FormulaName";
            sql += " from ShippingDocumentGH  ShippingDocument";
            sql += " left join ConsmixpropGH Consmixprop on ShippingDocument.ConsmixpropId = Consmixprop.ConsmixpropId  left join Formula on Consmixprop.formulaID = Formula.FormulaID";
            sql += " left join ContractGH Contract on ShippingDocument.ContractID = Contract.ContractID ";
            sql += " where cube > 0 and IsEffective = 1 and ProduceDate between '" + begin.ToString() + "'";
            sql += " and '" + end.ToString() + "'";
            if (!string.IsNullOrEmpty(CarID))
            {
                sql += " and CarId = '" + CarID.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(Constrength))
            {
                sql += " and ShippingDocument.Constrength ='" + Constrength.Trim() + "'";
            }
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                IList<ShippingDocumentGH> list = ReaderToShippingDocumentListGH(sdr);
                return list;
            }
        }


        public IList<TZrelation> getTZRecord(DateTime begin, DateTime end, string CarID, string Constrength, string ShipDocID)
        {
            string sql = "select SourceShipDocID,tzralation.Cube,tzralation.CarID,TaskID,ContractID,ContractName,CustomerID,CustName,ProjectName,ProjectAddr,ConStrength,CastMode,";
            sql += "ConsPos,PumpName,ParCube,SendCube,ProvidedCube,ProvidedTimes,ProduceDate,tzralation.TotalWeight,tzralation.CarWeight,tzralation.Weight,tzralation.Exchange,tzralation.BuildTime  ,Dic.DicName ReturnType ";
            sql += " from tzralation left join shippingDocument on tzralation.SourceShipDocID = shippingDocument.shipDocId LEFT JOIN DIC ON DicID=tzralation.ReturnType where tzralation.cube > 0 and tzralation.BuildTime between '" + begin.ToString() + "' and '" + end.ToString() + "'";

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
            //string sql = "insert into CarEmptyWeight (CarID, Weight, Builder, BuildTime) values (@CarID,@Weight,@Builder,getdate()) ";

            string sql = "update car set CarWeight=@Weight where CarID=@CarID";

            SqlParameter[] paras ={　
                                new SqlParameter("@CarID", cew.CarID),
                                new SqlParameter("@Weight",cew.Weight),
                                new SqlParameter("@Builder", cew.Builder)
                                
              };

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
        }

        public int AddShippingDocumentGH(ShippingDocumentGH sdGH)
        {
            string sql = "insert into ShippingDocumentGH (ShipDocID,lastid,ProduceDate,CustName,ProjectName,ConStrength,CarID,SupplyUnit,TotalWeight,CarWeight,Weight,Builder,Remark) values (@ShipDocID,@lastid,@ProduceDate,@CustName,@ProjectName,@ConStrength,@CarID,@SupplyUnit,@TotalWeight,@CarWeight,@Weight,@Builder,@Remark) ";

            SqlParameter[] paras ={　
                                new SqlParameter("@ShipDocID", sdGH.ShipDocID),
                                new SqlParameter("@lastid", sdGH.lastid),
                                new SqlParameter("@ProduceDate", sdGH.ProduceDate),
                                new SqlParameter("@CustName",sdGH.CustName),
                                new SqlParameter("@ProjectName",sdGH.ProjectName),
                                new SqlParameter("@ConStrength",sdGH.ConStrength),
                                new SqlParameter("@CarID", sdGH.CarID),
                                new SqlParameter("@SupplyUnit", sdGH.SupplyUnit),
                                new SqlParameter("@TotalWeight", sdGH.TotalWeight),
                                new SqlParameter("@CarWeight", sdGH.CarWeight),
                                new SqlParameter("@Weight", sdGH.Weight),
                                new SqlParameter("@Builder", sdGH.Builder),
                                new SqlParameter("@Remark", sdGH.Remark)
              };

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
        }
        /// <summary>
        /// 材料销售出厂新增记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddStuffSale(StuffSale entity)
        {
            string sql = @"insert into StuffSale (CarID,CarNo,StuffID,StuffName,SupplyID,CompanyID,CompName,TotalWeight,CarWeight,Weight,WeightMan,WeightName,ArriveTime,DeliveryTime,Remark,Builder,BuildTime,StuffSaleID) values (@CarID,@CarNo,@StuffID,@StuffName,@SupplyID,@CompanyID,@CompName,@TotalWeight,@CarWeight,@Weight,@WeightMan,@WeightName,@ArriveTime,@DeliveryTime,@Remark,@Builder,@BuildTime,@StuffSaleID);
                        if(SELECT COUNT(*) FROM AutoGenerateId WHERE [Table]='StuffSale' AND IDPrefix=@IDPrefix)>0
                        BEGIN 
	                        update AutoGenerateId set NextValue=@NextValue where [Table]='StuffSale' and IDPrefix=@IDPrefix ;
                        END
                        ELSE
                        BEGIN 
	                        INSERT INTO AutoGenerateId VALUES('StuffSale',@IDPrefix,@NextValue);
                        END; ";
            SqlParameter[] paras ={　
                                new SqlParameter("@StuffSaleID", entity.StuffSaleID),
                                new SqlParameter("@CarID", entity.CarID),
                                new SqlParameter("@CarNo", entity.CarNo),
                                new SqlParameter("@StuffID", entity.StuffID),
                                new SqlParameter("@StuffName", entity.StuffName),
                                new SqlParameter("@SupplyID", entity.SupplyID),
                                new SqlParameter("@CompanyID", entity.CompanyID),
                                new SqlParameter("@CompName", entity.CompName),
                                new SqlParameter("@TotalWeight", entity.TotalWeight),
                                new SqlParameter("@CarWeight", entity.CarWeight),
                                new SqlParameter("@Weight", entity.Weight),
                                new SqlParameter("@WeightMan", entity.WeightMan),
                                new SqlParameter("@WeightName", entity.WeightName),
                                new SqlParameter("@ArriveTime", entity.ArriveTime),
                                new SqlParameter("@DeliveryTime", entity.DeliveryTime),
                                new SqlParameter("@Remark", entity.Remark),
                                new SqlParameter("@Builder", entity.Builder),
                                new SqlParameter("@BuildTime", entity.BuildTime),
                                new SqlParameter("@NextValue", entity.NextValue),
                                new SqlParameter("@IDPrefix", entity.IDPrefix)
            };
            int a = Convert.ToInt32(SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, paras));
            UpdateSiloStuffKc(entity.StuffID, entity.SiloID, Convert.ToDecimal(entity.Weight));
            return a;
        }

        /// <summary>
        /// 修改筒仓材料库存
        /// </summary>
        /// <param name="stuffid"></param>
        /// <param name="siloid"></param>
        /// <param name="changeNum"></param>
        /// <returns></returns>
        public int UpdateSiloStuffKc(string stuffid, string siloid, decimal changeNum)
        {
            string sql0 = string.Format(@"SELECT Content FROM Silo WHERE siloid='{0}' AND StuffID='{1}'", siloid, stuffid);
            decimal? content = Convert.ToDecimal(SqlHelper.ExecuteScalar(m_ERPConnString, CommandType.Text, sql0, null));
            if (content == null)
            {
                content = 0;
            }
            string sql = string.Format(@"UPDATE StuffInfo SET Inventory=Inventory+{0} WHERE StuffID='{1}';UPDATE Silo SET Content=Content+{0} WHERE SiloID='{2}'", changeNum, stuffid, siloid);
            //插入出库台账记录
            sql = sql + string.Format(@"INSERT INTO SiloLedgerContent_Use
                                        ( SiloID ,
                                          StuffID ,
                                          Action ,
                                          BaseStore ,
                                          [Use] ,
                                          Remaining ,
                                          ActionTime
                                        )VALUES('{0}','{1}','销售出库','{2}','{3}','{4}',getdate())", siloid, stuffid, content, changeNum, content - changeNum);
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(m_ERPConnString, System.Data.CommandType.Text, sql, null));
        }

        /// <summary>
        /// 获得罐车系统皮重
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public int getCarEmptyWeight(string CarID)
        {
            //string sql = "select top 1 Weight from CarEmptyWeight where CarID='" + CarID + "' order by ID desc";
            string sql = "select top 1 isnull(CarWeight,0) as CarWeight from Car where CarID='" + CarID + "' order by CarID desc";
            int? weight = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (weight == null)
            {
                weight = 0;
            }
            return Convert.ToInt32(weight);
        }

        /// <summary>
        /// 获得货车系统皮重
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public int getStuffCarEmptyWeight(string CarNo)
        {
            string sql = "select top 1 isnull(CarWeight) as CarWeight from Car where CarNo='" + CarNo + "' order by LastBackTime desc";
            int? weight = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (weight == null)
            {
                weight = 0;
            }
            return Convert.ToInt32(weight);
        }


        /// <summary>
        /// 获得货车入场时间
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public DateTime getStuffCarLastTime(string CarNo)
        {
            string sql = "select top 1 LastBackTime from Car where CarNo='" + CarNo + "' order by LastBackTime desc";
            DateTime? LastBackTime = Convert.ToDateTime(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (LastBackTime == null)
            {
                LastBackTime =DateTime.Now;
            }
            return Convert.ToDateTime(LastBackTime);
        }
        public int getProvidedTimes(string TaskID)
        {
            string sql = "select top 1 ProvidedTimes from ProduceTasks where TaskID='" + TaskID + "'";
            object a = SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null);
            object b = a.ToString() == "" ? 0 : a;
            int? ProvidedTimes = Convert.ToInt32(b);

            if (ProvidedTimes == null)
            {
                ProvidedTimes = 0;
            }
            
            return Convert.ToInt32(ProvidedTimes);
        }

        public decimal getProvidedCube(string TaskID)
        {
            string sql = "select top 1 ProvidedCube from ProduceTasks where TaskID='" + TaskID + "'";

            object obj = SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null);
            decimal? ProvidedCube = (obj==DBNull.Value) ? 0 : Convert.ToDecimal(obj);

            if (ProvidedCube == null)
            {
                ProvidedCube = 0;
            }

            return Convert.ToDecimal(ProvidedCube);
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

        /// <summary>
        /// 是否管理员
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool isAdmin(string userid)
        {
            string sql = "SELECT COUNT(*) FROM Users a LEFT JOIN dic b ON a.UserType=b.TreeCode WHERE UserID LIKE '%" + userid + "%' AND ParentID='UserType' AND DicName='系统管理员'";

            int? success = Convert.ToInt32(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));

            if (success == null||success==0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获得砼强度对应的换算率
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public int getConStrengthExchange(string ConStrength)
        {
            string sql = "select top 1 Exchange from ConStrength where ConStrengthCode='" + ConStrength + "' order by ConStrengthID desc";
            object obj = SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null);
            int? conStrength = 0;
            if (obj == null||obj==DBNull.Value)
            {
                conStrength = 0;
            }
            else
            {
                conStrength = Convert.ToInt32(obj);
            }
            return Convert.ToInt32(conStrength);
        }
        /// <summary>
        /// 获取字典类型
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<Dic> GetDicList(string where)
        {
            string sql = "SELECT DicID,DicName,TreeCode,ParentID FROM Dic WHERE " + where;
            IList<Dic> list = new List<Dic>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    Dic record = new Dic();
                    record.DicID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    record.DicName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    list.Add(record);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有筒仓的状态信息
        /// </summary>
        /// <returns></returns>
        public IList<Silo> GetAllSiloList()
        {
            string sql = @"SELECT s.SiloID, s.SiloName, s.SiloShortName, s.MinContent, s.MaxContent, s.Content, s.MinWarm, s.MaxWarm, s.OrderNum, pl.ProductLineID, pl.ProductLineName, si.StuffID, si.StuffName 
                           FROM dbo.Silo s INNER JOIN dbo.SiloProductLine spl ON s.SiloID = spl.SiloID 
                           INNER JOIN dbo.ProductLine pl ON spl.ProductLineID = pl.ProductLineID 
                           INNER JOIN dbo.StuffInfo si ON s.StuffID = si.StuffID 
                           WHERE s.ISUSED = 1 AND pl.ISUSED = 1 AND s.StuffID IS NOT NULL
                           ORDER BY pl.ProductLineID, s.SiloID";


            IList<Silo> list = new List<Silo>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(m_ERPConnString, System.Data.CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    Silo silo = new Silo();
                    silo.SiloID = sdr.IsDBNull(0) ? string.Empty : sdr.GetString(0);
                    silo.SiloName = sdr.IsDBNull(1) ? string.Empty : sdr.GetString(1);
                    silo.SiloShortName = sdr.IsDBNull(2) ? string.Empty : sdr.GetString(2);
                    if (!sdr.IsDBNull(3)) silo.MinContent = sdr.GetDecimal(3);
                    if (!sdr.IsDBNull(4)) silo.MaxContent = sdr.GetDecimal(4);
                    if (!sdr.IsDBNull(5)) silo.Content = sdr.GetDecimal(5);
                    if (!sdr.IsDBNull(6)) silo.MinWarm = sdr.GetDecimal(6);
                    if (!sdr.IsDBNull(7)) silo.MaxWarm = sdr.GetDecimal(7);
                    if (!sdr.IsDBNull(8)) silo.OrderNum = sdr.GetInt32(8);
                    silo.ProductLineID = sdr.IsDBNull(9) ? string.Empty : sdr.GetString(9);
                    silo.ProductLineName = sdr.IsDBNull(10) ? string.Empty : sdr.GetString(10);
                    silo.StuffID = sdr.IsDBNull(11) ? string.Empty : sdr.GetString(11);
                    silo.StuffName = sdr.IsDBNull(12) ? string.Empty : sdr.GetString(12);

                    list.Add(silo);
                }
            }
            
            return list;
        }

        /// <summary>
        /// 获取入库单材料规格
        /// </summary>
        /// <returns></returns>
        public string GetSpecByStuffID(string StuffID)
        {
            string sql = "SELECT DicName FROM Dic WHERE DicID=(SELECT Spec FROM StuffInfo WHERE StuffID='" + StuffID + "')";
            string Spec = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (Spec == null)
            {
                Spec = "";
            }
            return Spec;
        }

        /// <summary>
        /// 根据车号获取上一车司机
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public string GetDriverByCarID(string CarID)
        {
            string sql =string.Format("SELECT TOP 1 Driver FROM StuffIn WHERE CarNo ='{0}' ORDER BY BuildTime DESC",CarID);
            string Driver = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (Driver == null)
            {
                Driver = "";
            }
            return Driver;
        }

        /// <summary>
        /// 根据车号获取上一车来源地
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public string GetSourceAddrByCarID(string CarID)
        {
            string sql = string.Format("SELECT TOP 1 SourceAddr FROM StuffIn WHERE CarNo ='{0}' ORDER BY BuildTime DESC", CarID);
            string SourceAddr = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (SourceAddr == null)
            {
                SourceAddr = "";
            }
            return SourceAddr;
        }


        public int getProvidedTimesGH(string TaskID)
        {
            string sql = "select top 1 ProvidedTimes from ProduceTasksGH where TaskID='" + TaskID + "'";
            object a = SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null);
            object b = a.ToString() == "" ? 0 : a;
            int? ProvidedTimes = Convert.ToInt32(b);

            if (ProvidedTimes == null)
            {
                ProvidedTimes = 0;
            }

            return Convert.ToInt32(ProvidedTimes);
        }

        public decimal getProvidedCubeGH(string TaskID)
        {
            string sql = "select top 1 ProvidedCube from ProduceTasksGH where TaskID='" + TaskID + "'";

            object obj = SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null);
            decimal? ProvidedCube = (obj == DBNull.Value) ? 0 : Convert.ToDecimal(obj);

            if (ProvidedCube == null)
            {
                ProvidedCube = 0;
            }

            return Convert.ToDecimal(ProvidedCube);
        }

        public string getTelByShippingDocumentGH(string ShipDocID)
        {
            string sql = "SELECT Tel FROM ProjectGH WHERE ProjectID=(SELECT ProjectID FROM ShippingDocumentGH WHERE ShipDocID='" + ShipDocID + "')";
            string Tel = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (Tel == null)
            {
                Tel = "";
            }
            return Tel;
        }
        public string getCarNoByCarID(string CarID)
        {
            string sql = "SELECT CarNo FROM Car WHERE CarID='" + CarID + "'";
            string CarNo = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (CarNo == null)
            {
                CarNo = "";
            }
            return CarNo;
        }
        public string getFootNumBySupplyID(string SupplyID)
        {
            string sql = "SELECT ISNULL(FootFrom,0) FootFrom FROM StockPact WHERE SupplyID='" + SupplyID + "'";
            string FootNum = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (FootNum == null)
            {
                FootNum = "";
            }
            return FootNum;
        }

        public string getPriceByStuffInID(string StuffInID)
        {
            string sql = "SELECT ISNULL(DBO.GetStockPactPrice(SP.StockPactID,STU.StuffID,STU.InDate),0) StockPrice FROM StuffIn STU  LEFT JOIN StockPact SP ON SP.SupplyID=STU.SupplyID  WHERE STUFFINID='" + StuffInID + "'";
            string Price = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (Price == null)
            {
                Price = "";
            }
            return Price;
        }
        public string getBetonTagByShipDocID(string ShipDocID)
        {
            string sql = "SELECT BetonTag,* FROM ProduceTasks WHERE TaskID=( SELECT TaskID FROM ShippingDocument WHERE ShipDocID='" + ShipDocID + "')";
            string BetonTag = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (BetonTag == null)
            {
                BetonTag = "";
            }
            return BetonTag;
        }

        public string getCarWeightByCarID(string CarID)
        {
            string sql = "SELECT CarWeight FROM Car WHERE CarID='" + CarID + "'";
            string CarWeight = Convert.ToString(SqlHelper.ExecuteScalar(m_ERPConnString, System.Data.CommandType.Text, sql, null));
            if (CarWeight == null || CarWeight == "")
            {
                CarWeight = "0";
            }
            return CarWeight;
        }
    }
}
