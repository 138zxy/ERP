using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace WeightingSystem.Helpers
{
    public class Config
    {
        /// <summary>
        /// 配置节
        /// </summary>
        /// 
        public  string CompanyName = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.CompanyName, string.Empty);
        public bool FastMetage = Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.FastMetage, "false"));
        public bool AutoPrint = Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.AutoPrint, "true"));
        public bool SoundWeight = Convert.ToBoolean(Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.SoundWeight, "false"));
        
        public  string COM = Config.Ini.GetString(Config.Section.MetageSetting, Config.ConfigKey.Com, string.Empty);
        public  string BaudRate = Config.Ini.GetString(Config.Section.MetageSetting, Config.ConfigKey.BaudRate, string.Empty);
        public  bool Positive = Convert.ToBoolean(Config.Ini.GetString(Config.Section.MetageSetting, Config.ConfigKey.Positive, string.Empty));
        public  string DataLength = Config.Ini.GetString(Config.Section.MetageSetting, Config.ConfigKey.DataLength, string.Empty);
        public  string StartChar = Config.Ini.GetString(Config.Section.MetageSetting, Config.ConfigKey.StartChar, string.Empty);
        public  string EndChar = Config.Ini.GetString(Config.Section.MetageSetting, Config.ConfigKey.EndChar, string.Empty);
        public  string Server = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.Server, string.Empty);
 
        public  string Database = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.Database, string.Empty);
        public  string uid = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.uid, string.Empty);
        public  string pwd = Config.Ini.GetString(Config.Section.DB, Config.ConfigKey.pwd, string.Empty);
        public  int RateMode = Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.RateMode, 0);
        public  string prefixPat = Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.prefixPat, string.Empty);
        public  int idLen = Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.idLen, 0);
        public  int DarkWeightMode = Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.DarkRateMode, 0);
        public  int adminMode = Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.adminMode, 0);




        public class Section
        {
            public const string DB = "DB";

            public const string Car = "Car";

            public const string Speech = "Speech";

            public const string BaseSetting = "BaseSetting";

            public const string MetageSetting = "MetageSetting";

            public const string KeyBoard = "KeyBoard";


            public static string HIKNetConfig = "HIKNetConfig";
        }

        public class ConfigKey
        {
            public const string Server = "Server";

            public const string CompanyName = "CompanyName";

            public const string prefixPat = "prefixPat";

            public const string idLen = "idLen";

            public const string Exchange = "Exchange";          //换算率，如果磅秤系统内设置了换算率，则统一按照此换算率计算，若不设置，或者设置为0，则采用ERP计算

            public const string PrintUnit = "PrintUnit";

            public const string FastMetage = "FastMetage";

            public const string EnablePrintStuffName = "EnablePrintStuffName";

            public const string VideoQuality = "VideoQuality";

            public const string AutoPrint = "AutoPrint";

            public const string SoundWeight = "SoundWeight";

            public const string StuffInputFree = "StuffInputFree";

            public const string Database = "Database";

            public const string Videos = "Videos";

            public const string uid = "uid";

            public const string pwd = "pwd";

            public const string Com = "COM";

            public const string BaudRate = "BaudRate";

            public const string DataLength = "DataLength";

            public const string RateMode = "RateMode";

            public const string adminMode = "adminMode";

            public const string Positive = "Positive";

            public const string StartChar = "StartChar";

            public const string EndChar = "EndChar";
 
            /// <summary>
            /// 暗扣方式 0:不暗扣,1:ERP管理暗扣,2:磅房系统管理暗扣 
            /// </summary>
            public const string DarkRateMode = "DarkRateMode";
            /// <summary>
            /// 供货商查询sql
            /// </summary>
            public const string SqlSupply = "SqlSupply";
            /// <summary>
            /// 运输商查询sql
            /// </summary>
            public const string SqlTransfer = "SqlTransfer";

            public const string SqlBale = "SqlBale";

            public const string SqlStuff = "SqlStuff";

            public const string CustomSpeech = "CustomSpeech";


            public const string PrintTZReport = "PrintTZReport";
            public const string PrintFCReport = "PrintFCReport";

            public const string PrintShipDocReport = "PrintShipDocReport";

            public const string UseMetageCube = "UseMetageCube";


            public const string A1 = "A1";

            public const string A2 = "A2";

            public const string A3 = "A3";

            public const string A4 = "A4";

            public const string A5 = "A5";

            public const string A6 = "A6";

            public const string B1 = "B1";

            public const string B2 = "B2";

            public const string B3 = "B3";

            public const string B4 = "B4";

            public const string B5 = "B5";

            public static string IP1 = "IP1";

            public static string Port1 = "Port1";

            public static string UserName1 = "UserName1";

            public static string Password1 = "Password1";

            public static string Channel1 = "Channel1";


            public static string IP2 = "IP2";

            public static string Port2 = "Port2";

            public static string UserName2 = "UserName2";

            public static string Password2 = "Password2";

            public static string Channel2 = "Channel2";

            public static string CaptureMode = "CaptureMode";

            public static string HKVideos = "HKVideos";

            public static string HKwPicQuality = "HKwPicQuality";

            public static string HKwPicSize = "HKwPicSize";
        }

         static IniFile _Config = LoadConfig();

         /// <summary>
         /// Ini配置文件操作
         /// </summary>
         public static IniFile Ini
         {
             get
             {
                 return _Config;
             }
         }


        static string ConfigFilePath
        {
            get
            {
                return Path.Combine(
                                  Path.GetDirectoryName(
                                  Assembly.GetExecutingAssembly().Location
                                  ),
                                  "config.ini");
            }
        }


        static IniFile LoadConfig()
        {

            return new IniFile(ConfigFilePath);
        }
         

        
    }
}
