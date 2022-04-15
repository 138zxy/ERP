using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;

namespace ZLERP.IRepository
{
    public static class MicrosoftExts
    {

        private static readonly byte[] CharMap = InitCharMap();

        public static void AddCollection<T>(this List<T> list, params T[] items)
        {
            foreach (T local in items)
            {
                list.Add(local);
            }
        }
        public static string ToStr(this object o)
        {
            if (o == null)
            {
                return "";
            }
            return o.ToString();
        }
        public static bool Between(this int i, int smaller, int bigger)
        {
            return ((i >= smaller) && (i <= bigger));
        }

        public static bool BetweenCO(this int i, int smaller, int bigger)
        {
            return ((i >= smaller) && (i < bigger));
        }

        public static bool BetweenOC(this int i, int smaller, int bigger)
        {
            return ((i > smaller) && (i <= bigger));
        }

        public static bool BetweenOO(this int i, int smaller, int bigger)
        {
            return ((i > smaller) && (i < bigger));
        }

        public static bool ContainsAll(this string source, params char[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach (char ch in values)
            {
                if (!source.Contains<char>(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAny(this string source, params char[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach (char ch in values)
            {
                if (source.Contains<char>(ch))
                {
                    return true;
                }
            }
            return false;
        }

        private static Dictionary<string, object> ConvertToDic(DataRow dr)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (dr != null)
            {
                foreach (DataColumn column in dr.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    object obj2 = dr[column];
                    dictionary[columnName] = obj2;
                }
            }
            return dictionary;
        }

        public static DateTime End(this DateTime date)
        {
            return date.Date.AddDays(1.0).AddSeconds(-1.0);
        }

        public static void EnqueueCollection<T>(this Queue<T> list, params T[] items)
        {
            foreach (T local in items)
            {
                list.Enqueue(local);
            }
        }

        public static string FirstLetterToUpper(this string input)
        {
            if ((input ?? "").Length == 0)
            {
                throw new ArgumentNullException("input");
            }
            return (input.First<char>().ToString().ToUpper() + string.Join<char>("", input.ToLower().Skip<char>(1)));
        }

        public static string FirstLetterToUpperOnly(this string input)
        {
            if ((input ?? "").Length == 0)
            {
                throw new ArgumentNullException("input");
            }
            return (input.First<char>().ToString().ToUpper() + string.Join<char>("", input.Skip<char>(1)));
        }

        //public static bool FirstTableHasRows(this DataSet ds, out DataRow dr)
        //{
        //    return ds.HasRowsWithTable(1, out dr);
        //}

        public static IList<TResult> ForEachNew<TResult, TEnter>(this IList<TEnter> list, Func<TEnter, TResult> func)
        {
            IList<TResult> list2 = new List<TResult>();
            IEnumerator<TEnter> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                list2.Add(func(enumerator.Current));
            }
            return list2;
        }

        //public static string GetAvailableName(this DirectoryInfo path, string dir, string newName = "")
        //{
        //    if (!path.IsValidFileName(newName))
        //    {
        //        newName = path.ToValidFileName(newName);
        //    }
        //    string extension = Path.GetExtension(newName);
        //    if (!string.IsNullOrWhiteSpace(extension))
        //    {
        //        newName = Path.GetFileNameWithoutExtension(newName);
        //    }
        //    string str2 = Path.Combine(dir, newName);
        //    string str3 = str2;
        //    int num = 2;
        //    while (System.IO.File.Exists(str3) || Directory.Exists(str3))
        //    {
        //        str3 = string.Format("{0}({1}){2}", str2, num++, extension);
        //    }
        //    return str3;
        //}

        public static string GetCharType(this char c)
        {
            switch (CharMap[c])
            {
                case 0:
                    return "其他";

                case 1:
                    return "数字";

                case 2:
                    return "小写字母";

                case 3:
                    return "大写字母";

                case 4:
                    return "特殊符号";

                case 5:
                    return "标点符号";
            }
            return "其他";
        }

        public static object GetDefault(this Type t)
        {
            if (!t.IsValueType)
            {
                return null;
            }
            return Activator.CreateInstance(t);
        }

        //public static SqlDbType GetSqlDbType(this Type type)
        //{
        //    return SystemExtension.MapSqlDbType(type);
        //}

        //public static SqlDbType[] GetSqlDbTypes(this Type type)
        //{
        //    return SystemExtension.MapSqlDbTypes(type);
        //}

        //[Obsolete("test")]
        //public static string GetType2(this object o)
        //{
        //    return SystemExtension.GetOrigionType(o);
        //}

        public static bool HasRows(this DataTable dt)
        {
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        public static bool HasRowsWithTable(this DataSet ds, int count, out DataRow dr)
        {
            if (count < 1)
            {
                count = 1;
            }
            dr = null;
            if (((ds != null) && (ds.Tables != null)) && (ds.Tables.Count >= count))
            {
                DataTable table = ds.Tables[count - 1];
                if ((table != null) && (table.Rows.Count > 0))
                {
                    dr = table.Rows[0];
                    if (dr != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasTable(this DataSet ds)
        {
            return (((ds != null) && (ds.Tables != null)) && (ds.Tables.Count > 0));
        }

        public static bool In(this int i, params int[] g)
        {
            foreach (int num in g)
            {
                if (i == num)
                {
                    return true;
                }
            }
            return false;
        }

        private static byte[] InitCharMap()
        {
            byte[] buffer = new byte[0xffff];
            for (char ch = '0'; ch <= '9'; ch = (char)(ch + '\x0001'))
            {
                buffer[ch] = 1;
            }
            for (char ch2 = 'a'; ch2 <= 'z'; ch2 = (char)(ch2 + '\x0001'))
            {
                buffer[ch2] = 2;
            }
            for (char ch3 = 'A'; ch3 <= 'Z'; ch3 = (char)(ch3 + '\x0001'))
            {
                buffer[ch3] = 3;
            }
            buffer[0x2f] = 4;
            buffer[0x5c] = 4;
            buffer[0x7c] = 4;
            buffer[0x24] = 4;
            buffer[0x23] = 4;
            buffer[0x2b] = 4;
            buffer[0x25] = 4;
            buffer[0x26] = 4;
            buffer[0x2d] = 4;
            buffer[0x5e] = 4;
            buffer[0x2a] = 4;
            buffer[0x3d] = 4;
            buffer[0x2c] = 5;
            buffer[0x2e] = 5;
            buffer[0x21] = 5;
            buffer[0x3a] = 5;
            buffer[0x3b] = 5;
            buffer[0x3f] = 5;
            buffer[0x22] = 5;
            buffer[0x27] = 5;
            return buffer;
        }

        public static bool IsValidFileName(this DirectoryInfo path, string fileName)
        {
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                if (fileName.IndexOf(ch) > -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidPath(this DirectoryInfo p, string path)
        {
            foreach (char ch in Path.GetInvalidPathChars())
            {
                if (path.IndexOf(ch) > -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static string Link(this IList<string> parts)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in parts)
            {
                builder.Append(str);
            }
            return builder.ToString();
        }

        public static string Link(this string[] parts)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in parts)
            {
                builder.Append(str);
            }
            return builder.ToString();
        }

        //private static List<Dictionary<string, object>> ListItems(DataTable dt)
        //{
        //    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        //    if (dt.HasRows())
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            Dictionary<string, object> item = ConvertToDic(row);
        //            list.Add(item);
        //        }
        //    }
        //    return list;
        //}

        public static bool NotIn(this int i, params int[] g)
        {
            foreach (int num in g)
            {
                if (i == num)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Outer(this int i, int smaller, int bigger)
        {
            if (i >= smaller)
            {
                return (i > bigger);
            }
            return true;
        }

        public static bool OuterCC(this int i, int smaller, int bigger)
        {
            if (i > smaller)
            {
                return (i >= bigger);
            }
            return true;
        }

        public static bool OuterCO(this int i, int smaller, int bigger)
        {
            if (i > smaller)
            {
                return (i > bigger);
            }
            return true;
        }

        public static bool OuterOC(this int i, int smaller, int bigger)
        {
            if (i >= smaller)
            {
                return (i >= bigger);
            }
            return true;
        }

        public static string PrintChars(this byte[] b)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in b)
            {
                builder.Append(num.ToString() + " ");
            }
            return builder.ToString();
        }

        public static string Reverse(this string source)
        {
            return new string(source.ToCharArray().Reverse<char>().ToArray<char>());
        }

        public static DateTime Start(this DateTime date)
        {
            return date.Date;
        }

        public static byte ToByte(this object o)
        {
            try
            {
                return Convert.ToByte(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static byte[] ToBytes(this object o)
        {
            try
            {
                return Convert.FromBase64String(o.ToStr());
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public static System.DataReader ToDataReader(this DataTable dt)
        //{
        //    return System.DataReader.GetDataReader(ListItems(dt));
        //}

        public static DateTime ToDateTime(this object o)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static decimal ToDecimal(this object o)
        {
            try
            {
                return Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                return 0.0M;
            }
        }

        public static decimal ToDecimal(this object o, int scale)
        {
            decimal d = 0M;
            try
            {
                d = Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                d = 0.0M;
            }
            return Math.Round(d, scale, MidpointRounding.AwayFromZero);
        }

        public static double ToDouble(this object o)
        {
            try
            {
                return Convert.ToDouble(o);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        public static string ToFullString(this EndPoint ep)
        {
            if ((ep != null) && (ep is IPEndPoint))
            {
                IPEndPoint point = ep as IPEndPoint;
                return (point.Address.ToString() + ":" + point.Port);
            }
            return "unknown ip";
        }

        public static int ToInt(this object o)
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //public static string ToJson(this IList<DateTime> dList)
        //{
        //    return SystemExtension.GetJson(dList);
        //}

        public static string ToJson(this DateTime date)
        {
            return string.Format("{{'Year':{0},'Month':{1},'Day':{2}}}", date.Year, date.Month, date.Day);
        }

        //public static string ToJson(this DateTime[] dates)
        //{
        //    return SystemExtension.GetJson(dates);
        //}

        public static string ToLine(this IList<string> list)
        {
            if (list == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            list.ToList<string>().ForEach(delegate(string s)
            {
                sb.AppendFormat("{0},", s);
            });
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        //private static List<Dictionary<string, object>> ToList(this DataTable dt)
        //{
        //    return ListItems(dt);
        //}

        //public static string ToRMBLetter(this decimal number)
        //{
        //    return SystemExtension.GetRMB(number);
        //}



        public static string ToValidFileName(this DirectoryInfo path, string fileName)
        {
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                if (fileName.IndexOf(ch) > -1)
                {
                    fileName = fileName.Replace(ch.ToString(), "");
                }
            }
            return fileName;
        }

        //public static void TraverseDirectory(this DirectoryInfo path, string root, Action<string> fileHandler)
        //{
        //    path.TraverseFile(root, fileHandler);
        //    foreach (string str in Directory.GetDirectories(root))
        //    {
        //        path.TraverseDirectory(str, fileHandler);
        //    }
        //}

        private static void TraverseFile(this DirectoryInfo path, string currentDir, Action<string> fileHandler)
        {
            foreach (string str in Directory.GetFiles(currentDir))
            {
                fileHandler(str);
            }
        }
    }
}
