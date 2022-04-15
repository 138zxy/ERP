using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using ZLERP.Model.ViewModels;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using ServiceStack.Redis.Generic;

namespace ZLERP.Web.Helpers
{
    public class RedisHelper
    {
        public static RedisClient _RedisClient;
        public static string m_ConnStr = ConfigurationManager.ConnectionStrings["ZLERPGPS"].ConnectionString;

        public RedisHelper()
        {
            if (_RedisClient == null)
            {
                string ip = ConfigurationManager.AppSettings["RedisIP"];
                string port = ConfigurationManager.AppSettings["RedisPort"];
                
                _RedisClient = new RedisClient(ip, Convert.ToInt32(port));
            }
        }
        
        //获得对应键的数据库集合
        //public List<Point> GetGPSByDB(string start, string end, string id)
        //{
        //    List<Point> ops = new List<Point>();
        //    Point hp;
        //    using (SqlConnection conn = new SqlConnection(m_ConnStr))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string sql = string.Format("select Longtidue,Latitude,Speed,Sendtime,Oil,AccFlag from gpsinfo where TerminalID='{0}' and Receivetime>='{1}' and Receivetime<='{2}' order by Receivetime ", id, start, end);
        //            SqlCommand comm = new SqlCommand();
        //            comm.Connection = conn;
        //            comm.CommandText = sql;
        //            SqlDataReader read = comm.ExecuteReader();
        //            while (read.Read())
        //            {
        //                hp = new Point();
        //                hp.X = Convert.ToDouble(read[0]);
        //                hp.Y = Convert.ToDouble(read[1]);
        //                hp.Spd = Convert.ToDecimal(read[2].ToString() == "" ? 0.00m : read[2]);
        //                hp.Time = read[3].ToString() == "" ? "时间值错误" : Convert.ToDateTime(read[3].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
        //                hp.Oil = Convert.ToDecimal(read[4].ToString() == "" ? 0.00m : read[4]);
        //                hp.Acc = read[5].ToString();
        //                ops.Add(hp);
        //            }
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //        return ops;
        //    }


        //}

        //获取游览器得到的查询
        //public List<Point> GetGPSValue(string id, string start, string end)
        //{
        //    try
        //    {
        //        ArrayList list = GetNewKey("0", start, end, id);
        //        List<Point> opintBuilder = new List<Point>();
        //        string[] keys, date;
        //        List<Point> temp;
        //        DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<Point>));

        //        List<string> values = new List<string>();
        //        System.Text.StringBuilder bu = new System.Text.StringBuilder();

        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            //是否存在
        //            if (_RedisClient.Exists(list[i].ToString()) == 1)
        //            {
        //                //存在
        //                values.Add(list[i].ToString());
        //                _RedisClient.Expire(list[i].ToString(), 18000);//5h
        //            }
        //            else
        //            {
        //                //不存在
        //                if (values.Count != 0)
        //                {
        //                    g:
        //                    values = _RedisClient.GetValues(values);
        //                    //组合出值
        //                    for (int j = 0; j<values.Count;j++ )
        //                    {
        //                        bu.Append(values[j]);
        //                    }

        //                    using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(bu.ToString().Replace("][",","))))
        //                    {
        //                        temp = formatter.ReadObject(stream) as List<Point>;
        //                        opintBuilder.AddRange(temp);
        //                    }
        //                    bu.Clear();
        //                    values.Clear();
        //                }

        //                //keys = list[i].ToString().Split('|');
        //                //date = keys[0].Split('_');
        //                //temp = GetGPSByDB(date[0] + "-" + date[1] + "-" + date[2] + " " + date[3] + ":00:00", date[0] + "-" + date[1] + "-" + date[2] + " " + date[3] + ":59:59", keys[1]);
        //                //if (temp.Count == 0)
        //                //    continue;
        //                //opintBuilder.AddRange(temp);
        //                //using (MemoryStream stream = new MemoryStream())
        //                //{
        //                //    formatter.WriteObject(stream, temp);
        //                //    _RedisClient.Set(list[i].ToString(), stream.ToArray());
        //                //}
        //            }
        //            //_RedisClient.Expire(list[i].ToString(), 18000);//5h
        //        }
                
        //        return opintBuilder;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public List<Point> GetGPSValue(string id, string start, string end)
        //{
        //    try
        //    {
        //        List<string> list = GetNewKey("0", start, end, id);
        //        List<Point> opintBuilder = new List<Point>();
        //        List<Point> temp;
        //        DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<Point>));

        //        System.Text.StringBuilder bu = new System.Text.StringBuilder();

        //        list = _RedisClient.GetValues(list);
        //        //组合出值
        //        for (int j = 0; j < list.Count; j++)
        //        {
        //            bu.Append(list[j]);
        //        }

        //        using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(bu.ToString().Replace("][", ","))))
        //        {
        //            temp = formatter.ReadObject(stream) as List<Point>;
        //            opintBuilder.AddRange(temp);
        //        }
        //        bu = null;
        //        list = null;

        //        return opintBuilder;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        

        ////获得键集合
        //public List<string> GetNewKey(string type, string start_date,string end_date, string id)
        //{
        //    List<string> list = new List<string>();
        //    //GPS
        //    if (type == "0")
        //    { 
        //        //按年月日小时编号划分 yyyy_MM_dd_HH|id
        //        start_date = start_date.Substring(0, 13)+":00:00";
        //        end_date = end_date.Substring(0, 13) + ":00:00";
        //        DateTime start = Convert.ToDateTime(start_date);
        //        DateTime end = Convert.ToDateTime(end_date).AddHours(1);
                                
        //        for (; ; )
        //        {
        //            if (start > end)
        //                break;
        //            list.Add(start.ToString("yyyy_MM_dd_HH|") + id);
        //            start = start.AddHours(1);
        //        }
        //    }

        //    return list;
        //}

        public IEnumerable<T> GetList<T>(string listId)
        {
            IRedisTypedClient<T> iredisClient = _RedisClient.As<T>();
            IList<string> op = _RedisClient.SearchKeys(listId);
            List<T> list = null;
            op = op.OrderBy(p => p).ToList();
            foreach (string s in op)
            {
                if (list == null)
                    list = iredisClient.Lists[s].ToList();
                else
                {
                    list.AddRange(iredisClient.Lists[s].ToList());
                }
            }
            return list == null ? (iredisClient.Lists[listId].ToList()) : list;
        }


        public List<Point> GetGPSValue(string id, string start, string end)
        {
            try
            {
                //注意保证日期数据没有null
                DateTime _begin = Convert.ToDateTime(start);
                DateTime _end = Convert.ToDateTime(end);
                List<Point> list = new List<Point>();
                string key = "";
                while (_begin < _end)
                {
                    if (_begin.ToString("HH") == "00")
                    {
                        if (_begin.AddDays(1) <= _end)
                        {
                            key = string.Format("GPS_{0}_{1}_*", id, _begin.ToString("yyyyMMdd"));
                            _begin = _begin.AddDays(1);
                        }
                        else
                        {
                            key = string.Format("GPS_{0}_{1}_{2}", id, _begin.ToString("yyyyMMdd"), _begin.ToString("HH"));
                            _begin = _begin.AddHours(1);
                        }
                    }
                    else
                    {
                        key = string.Format("GPS_{0}_{1}_{2}", id, _begin.ToString("yyyyMMdd"), _begin.ToString("HH"));
                        _begin = _begin.AddHours(1);
                    }
                    list.AddRange(GetList<Point>(key));
                    
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}