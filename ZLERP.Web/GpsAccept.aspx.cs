using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLERP.Model;
using System.Text;
using ZLERP.Web.Helpers;
using System.Data;
using log4net;
using System.Web.Mvc;
using System.Web.Services;
using Newtonsoft.Json;
namespace ZLERP.Web
{
    public partial class GpsAccept : System.Web.UI.Page
    {
        static SqlServerHelper helper = new SqlServerHelper();
        protected static ILog log = LogManager.GetLogger(typeof(GpsAccept));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (Request.QueryString["action"]!=null)
                //{
                //    string a = Request.QueryString["action"].ToString();
                //    if (a == "gpsaccept")
                //    {
                //        GpsInfo[] dd = (GpsInfo[])Request.Headers.GetValues("Payload");
                //        string result = insertGPSInfo(dd);
                //        // 下面这些内从可以放在一个方法里，然后通过“token”标记去判断执行哪个方法。
                //        Response.Write(result);
                //        Response.End();
                //    }
                //}
            }
        }

        /// <summary>
        /// 供GPS服务 推送给接口GPS数据
        /// </summary>
        /// <param name="gpsinfos">gps数据按列表形式推送</param>
        /// <returns>永远成功，异常封装Data中返回</returns>
        [WebMethod]
        public static object insertGPSInfo(string gpsinfo)
        {
            GpsInfo[] gpsinfos = JsonConvert.DeserializeObject<GpsInfo[]>(gpsinfo); ;
            string s = InsertGPSInfoFun(gpsinfos);
            if (string.IsNullOrEmpty(s))
                return true;
            else
                return false;
        }
 
        /// <summary>
        /// 插入GPS信息
        /// </summary>
        /// <param name="gpsinfos"></param>
        /// <returns></returns>
        public static string InsertGPSInfoFun(GpsInfo[] gpsinfos)
        {
            StringBuilder bu = new StringBuilder();
            string sql = "";
            foreach (GpsInfo gpsinfo in gpsinfos)
            {
                try
                {
                    //纠偏
                    double[] ds = new double[2];
                    GpsCorrect.transform(gpsinfo.originlatitude.Value, gpsinfo.originlongtidue.Value, ds);
                    //gpsinfo.originlatitude = ds[0];
                    gpsinfo.latitude = ds[0];
                    //gpsinfo.originlongtidue = ds[1];
                    gpsinfo.longtidue = ds[1];


                    sql = string.Format(@"insert into GpsInfo(Trace,TerminalID,Longtidue,Latitude,OriginLongtidue,OriginLatitude,Speed,Height,Direction,Oil,Distance,UnLoad,UnLoadTme,Place,ErrorCode,Sendtime,Receivetime,AccFlag,BeaterStatus,OriginOil,AccOnCount,Builder,BuildTime,[Version],Lifecycle,flag,[Weight]) values('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}','{18}',{19},{20},'{21}',{22},{23},{24},0,{25})"
                   , gpsinfo.cartype
                         , gpsinfo.terminalid
                         , gpsinfo.longtidue == null ? "null" : gpsinfo.longtidue.ToString()
                         , gpsinfo.latitude == null ? "null" : gpsinfo.latitude.ToString()
                         , gpsinfo.originlongtidue == null ? "null" : gpsinfo.originlongtidue.ToString()
                         , gpsinfo.originlatitude == null ? "null" : gpsinfo.originlatitude.ToString()
                         , gpsinfo.speed == null ? "null" : gpsinfo.speed.ToString()
                         , "null"//Height
                         , gpsinfo.direction == null ? "null" : gpsinfo.direction.ToString()
                         , gpsinfo.oil == null ? "null" : gpsinfo.oil.ToString()
                         , gpsinfo.distance == null ? "null" : gpsinfo.distance.ToString()
                         , gpsinfo.unload == null ? "null" : gpsinfo.unload.Value.ToString()
                         , gpsinfo.unloadtime == null ? "null" : ("'" + gpsinfo.unloadtime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                         , gpsinfo.place == null ? "null" : gpsinfo.place.ToString()
                         , gpsinfo.errorcode == null ? "null" : gpsinfo.errorcode.ToString()
                         , "null"//Sendtime
                         , gpsinfo.receivetime == null ? "null" : ("'" + gpsinfo.receivetime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                         , gpsinfo.accflag
                         , GetBeaterStatus(gpsinfo.unload.ToString(), gpsinfo.accflag.ToString())
                         , "null"//OriginOil
                         , gpsinfo.acconcount == null ? "null" : gpsinfo.acconcount.ToString()
                         , "接口"
                         , ("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                         , "1"
                         , "0"
                         , gpsinfo.Weight
                         );
                    helper.ExecuteNonQuery(CommandType.Text, sql, null);

                    //最后更新表
                    UpdateLastestGpsInfo(gpsinfo);

                }
                catch (Exception e)
                {
                    log.Info(string.Format("{0}:{1}", e.Message, sql));
                    bu.AppendFormat("SN:{0} 写入错误:{1}", gpsinfo.terminalid, e.Message);
                }
            }
            return bu.ToString();
        }

        /// <summary>
        /// 获取搅拌车状态
        /// </summary>
        /// <param name="unload"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        static string GetBeaterStatus(string unload, string acc)
        {
            // 搅拌车状态。0：熄火，1：搅拌，2：卸料，3：停止，4：未知
            if (string.IsNullOrEmpty(unload))
                return "4";
            return ((unload == "2") ? "2" : ((unload == "1") ? "1" : (acc == "0" ? "0" : "3")));
        }
        
        /// <summary>
        /// 更新最后GPS信息
        /// </summary>
        /// <param name="gpsinfo"></param>
        private static void UpdateLastestGpsInfo(GpsInfo gpsinfo)
        {
            string sql = "";
            try
            {
                //查询LastestGpsInfo表是否存在
                int k = Convert.ToInt32(helper.ExecuteDataset(string.Format("select TerminalID from LastestGpsInfo where TerminalID='{0}'", gpsinfo.terminalid),CommandType.Text,null).Tables[0].Rows.Count);
                bool isNew = k > 0 ? false : true;

                if (isNew)
                {
                    //新增不需要判断
                    sql = string.Format(@"insert into LastestGpsInfo(Trace,TerminalID,Longtidue,Latitude,OriginLongtidue,OriginLatitude,Speed,Height,Direction,Oil,Distance,UnLoad,UnLoadTme,Place,ErrorCode,Sendtime,Receivetime,AccFlag,BeaterStatus,OriginOil,AccOnCount,Builder,BuildTime,[Version],Lifecycle,[Weight]) 
                                          values('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}','{18}',{19},{20},'{21}',{22},{23},{24},{25})"
                        , gpsinfo.cartype
                        , gpsinfo.terminalid
                        , gpsinfo.longtidue == null ? "null" : gpsinfo.longtidue.ToString()
                        , gpsinfo.latitude == null ? "null" : gpsinfo.latitude.ToString()
                        , gpsinfo.originlongtidue == null ? "null" : gpsinfo.originlongtidue.ToString()
                        , gpsinfo.originlatitude == null ? "null" : gpsinfo.originlatitude.ToString()
                        , gpsinfo.speed == null ? "null" : gpsinfo.speed.ToString()
                        , "null"
                        , gpsinfo.direction == null ? "null" : gpsinfo.direction.ToString()
                        , gpsinfo.oil == null ? "null" : gpsinfo.oil.ToString()
                        , gpsinfo.distance == null ? "null" : gpsinfo.distance.ToString()
                        , gpsinfo.unload == null ? "null" : gpsinfo.unload.Value.ToString()
                        , gpsinfo.unloadtime == null ? "null" : ("'" + gpsinfo.unloadtime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                        , gpsinfo.place == null ? "null" : gpsinfo.place.ToString()
                        , gpsinfo.errorcode == null ? "null" : gpsinfo.errorcode.ToString()
                        , "null"//Sendtime
                        , gpsinfo.receivetime == null ? "null" : ("'" + gpsinfo.receivetime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                        , gpsinfo.accflag
                        , GetBeaterStatus(gpsinfo.unload.ToString(), gpsinfo.accflag.ToString())
                        , "null"//OriginOil
                        , gpsinfo.acconcount == null ? "null" : gpsinfo.acconcount.ToString()
                        , "接口"
                        , ("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                        , "1"
                        , "0"
                        , gpsinfo.Weight
                        );
                    helper.ExecuteNonQuery(CommandType.Text, sql, null);
                }
                else
                {
                    //判断是否有比新增数据时间更新的数据，如果存在则表示是历史数据
                    k = Convert.ToInt32(helper.ExecuteDataset(string.Format("select count(*) from LastestGpsInfo where TerminalID='{0}' and receivetime>='{1}'", gpsinfo.terminalid, gpsinfo.receivetime.Value.ToString("yyyy-MM-dd HH:mm:ss")), CommandType.Text, null).Tables[0].Rows.Count);
                    if (k == 0)
                    {
                        sql = string.Format(@"update LastestGpsInfo set Trace='{0}',Longtidue={2},Latitude={3},OriginLongtidue={4},OriginLatitude={5},Speed={6},Height={7},Direction={8},Oil={9},Distance={10},UnLoad={11},UnLoadTme={12},Place={13},ErrorCode={14},Sendtime={15},Receivetime={16},AccFlag='{17}',BeaterStatus='{18}',OriginOil={19},AccOnCount={20},Builder='{21}',BuildTime={22},[Version]={23},Lifecycle={24},[Weight]={25} where TerminalID='{1}'"
                        , gpsinfo.cartype
                        , gpsinfo.terminalid
                        , gpsinfo.longtidue == null ? "null" : gpsinfo.longtidue.ToString()
                        , gpsinfo.latitude == null ? "null" : gpsinfo.latitude.ToString()
                        , gpsinfo.originlongtidue == null ? "null" : gpsinfo.originlongtidue.ToString()
                        , gpsinfo.originlatitude == null ? "null" : gpsinfo.originlatitude.ToString()
                        , gpsinfo.speed == null ? "null" : gpsinfo.speed.ToString()
                        , "null"
                        , gpsinfo.direction == null ? "null" : gpsinfo.direction.ToString()
                        , gpsinfo.oil == null ? "null" : gpsinfo.oil.ToString()
                        , gpsinfo.distance == null ? "null" : gpsinfo.distance.ToString()
                        , gpsinfo.unload == null ? "null" : gpsinfo.unload.Value.ToString()
                        , gpsinfo.unloadtime == null ? "null" : ("'" + gpsinfo.unloadtime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                        , gpsinfo.place == null ? "null" : gpsinfo.place.ToString()
                        , gpsinfo.errorcode == null ? "null" : gpsinfo.errorcode.ToString()
                        , "null"//Sendtime
                        , gpsinfo.receivetime == null ? "null" : ("'" + gpsinfo.receivetime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                        , gpsinfo.accflag
                        , GetBeaterStatus(gpsinfo.unload.ToString(), gpsinfo.accflag.ToString())
                        , "null"//OriginOil
                        , gpsinfo.acconcount == null ? "null" : gpsinfo.acconcount.ToString()
                        , "接口"
                        , ("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                        , "1"
                        , "0"
                        , gpsinfo.Weight
                        );
                        helper.ExecuteNonQuery(CommandType.Text, sql, null);
                    }
                }
            }
            catch (Exception e)
            {
                log.Info(string.Format("{0}:{1}", e.Message, sql));
            }
        }
    }
    public class GpsCorrect
    {
        static double pi = 3.14159265358979324;
        static double a = 6378245.0;
        static double ee = 0.00669342162296594323;

        public static void transform(double wgLat, double wgLon, double[] latlng)
        {
            if (outOfChina(wgLat, wgLon))
            {
                latlng[0] = wgLat;
                latlng[1] = wgLon;
                return;
            }
            double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
            double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
            double radLat = wgLat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            latlng[0] = wgLat + dLat;
            latlng[1] = wgLon + dLon;
        }

        private static bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        private static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}