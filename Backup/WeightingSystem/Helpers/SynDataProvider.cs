using System;
using System.Collections.Generic;
using System.Text;
using WeightingSystem.Models;
using System.Data.SQLite;
using System.Data.SqlClient;
namespace WeightingSystem.Helpers
{
    public class SynDataProvider
    {
        //public int UploadData()
        //{
        //    SQLiteConnection localConn = SQLiteHelper.GetConnection();
        //    ERPDataProvider edp = new ERPDataProvider();
        //    LocalDataProvider ldp = new LocalDataProvider();


        //    SqlConnection ServerConn = new SqlConnection(edp.m_ERPConnString);

           
        //    IList<StuffIn> StuffInList = ldp.getNoSynRecord();

        //    foreach (StuffIn si in StuffInList)
        //    {

        //        if (localConn.State == System.Data.ConnectionState.Closed)
        //        {
        //            localConn.Open();
        //        }
        //        if (ServerConn.State == System.Data.ConnectionState.Closed)
        //        {
        //            ServerConn.Open();
        //        }
        //        SQLiteTransaction LocalTran = localConn.BeginTransaction();
        //        SqlTransaction ServerTran = ServerConn.BeginTransaction();
        //        try
        //        {
        //            string ServerSql = "insert into stuffin(StuffInID,StuffID,SiloID,SupplyID,TransportID,GageUnit,TransportNum,SupplyNum,TotalNum,CarWeight,StockNum,WRate,InNum,FootNum,Driver,InDate,OutDate)";
        //            ServerSql += "values('" + si.ID + "','" + si.StuffID + "','" + si.BaleID + "','" + si.SupplyID + "','" + si.TransferID + "','千克'," + si.StuffWeight + "," + si.StuffWeight + "," + si.Weight + "," + si.CarWeight + "," + si.StuffWeight + "," + si.KZL + "," + si.StuffWeight + "," + si.StuffWeight + "," + (si.Driver==null?"''":si.Driver) + ",'" + si.InDate.ToString() + "','" + si.OutDate.ToString() + "');";
        //            ServerSql += "update silo set Content = Content +" + si.StuffWeight.ToString() + " where siloID ='" + si.BaleID + "';";
        //            ServerSql += "update stuffInfo set Inventory = Inventory + " + si.StuffWeight.ToString() + " where stuffID='" + si.StuffID + "'";
        //            string localSql = " update stuffIn set IsUploaded = 1 where ID = " + si.ID;
        //            SqlHelper.ExecuteNonQuery(ServerTran, System.Data.CommandType.Text, ServerSql, null);
        //            SQLiteHelper.ExecuteNonQuery(LocalTran, localSql, null);
        //            LocalTran.Commit();
        //            ServerTran.Commit();
        //        }
        //        catch
        //        {
        //            LocalTran.Rollback();
        //            ServerTran.Rollback();
        //            continue;
        //        }

        //        finally
        //        {
        //            localConn.Close();
        //            ServerConn.Close();
        //        }

        //    }

        //    return 1;
        //}
        
    }
}