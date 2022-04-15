using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using log4net;
using System.IO;


namespace WeightingSystem.Helpers
{
    public class SQLiteHelper
    {
        static ILog logger = LogManager.GetLogger(typeof(SQLiteHelper));
        /// <summary>
        /// 执行无返回值的查询
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = GetConnection();
            try
            {
                SQLiteCommand cmd = conn.CreateCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                logger.Error("ExecuteNonQuery错误", ex);
                return -1;
            }
            finally {
                conn.Close();
            }
            
        }

        public static int ExecuteNonQuery(SQLiteTransaction sqliteTran,string cmdText, params SQLiteParameter[] commandParameters)
        {
            
            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, sqliteTran.Connection, sqliteTran, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                logger.Error("ExecuteNonQuery错误", ex);
                return -1;
            }
        }

        public static SQLiteConnection GetConnection()
        {
            string connectionString = string.Format(@"Data Source={0}\\ws.db; Pooling=true;FailIfMissing=false;",
               Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
             
            return new SQLiteConnection(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SQLiteDataReader ExecuteReader( string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection conn = GetConnection();
           
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SQLiteDataReader ExecuteReaderTran(string cmdText,SQLiteTransaction sqlitetran, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection conn = sqlitetran.Connection;

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, sqlitetran, CommandType.Text, cmdText, commandParameters);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, CommandType cmdType, string cmdText, SQLiteParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();
           
            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


    }
}
