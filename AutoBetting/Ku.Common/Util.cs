using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace Ku.Common
{
    public class Util
    {
        public static int ConvertToInt32(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    int Num = Convert.ToInt32(o);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }
        public static int ConvertToInt32(string o)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(o))
                {
                    int Num = Convert.ToInt32(o);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public static long ConvertToInt64(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    long Num = Convert.ToInt64(o);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public static decimal ConvertToDecimal(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    decimal Num = 0;
                    decimal.TryParse(o.ToString().Trim(), out Num);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public static Double ConvertToDouble(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    Double Num = 0;
                    Double.TryParse(o.ToString().Trim(), out Num);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public static string ConvertToString(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 转换 字符串 并去掉 前后的空格
        /// 替换 字符中的\t 和
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ConvertToTrim(object o)
        {
            string inputStt = ConvertToString(o);
            //inputStt = inputStt.Replace("\t", "").Replace(" ", "");
            return inputStt.Trim();
        }
        /// <summary>
        /// 转换成 日期字符串
        /// </summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ConvertToDateTimeStr(object o, string format = "yyyy-MM-dd HH:mm")
        {
            string str = "";
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    return Convert.ToDateTime(o).ToString(format); ;
                }
                else
                {
                    return str;
                }
            }
            catch
            {
                return str;
            }
        }
        public static DateTime ConvertToDateTime(object o)
        { 
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    return Convert.ToDateTime(o);
                }
                else
                {
                    return default(DateTime);
                }
            }
            catch
            {
                return default(DateTime);
            }
        }
        /// <summary>
        /// 转换 字符串 然后 Trim
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Trim(object o)
        {
            string str = ConvertToString(o);
            return str.Trim();
        }

        public static bool IsNull(object o)
        {
            try
            {
                string str = ConvertToString(o);
                return string.IsNullOrEmpty(str);
            }
            catch (Exception)
            {

                return true;
            }
        }
        #region 分割 和组合 字符串
        /// <summary>
        /// 二次封装 Split 分割 字符串 
        ///  返回值不包括含有空字符串的数组元素
        /// </summary>
        /// <param name="separator">分隔符，用什么来分割 可以是 , ★★ 等等</param>
        /// <returns></returns>
        public static List<string> Split(string separator, string strValue)
        {
            return strValue.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        /// <summary>
        /// 二次封装  string.Join 
        /// 构造集合的成员，其中在每个成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="objList">集合列表</param>
        /// <returns></returns>
        public static string Join<T>(string separator, List<T> objList)
        {
            return string.Join(separator, objList);
        }
        #endregion
        public static string ToJson(object objData)
        {
            var iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(objData, iso);
        }

        public static T DeserializeObject<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        #region 清理Html
        public static string ClearHtml(string content)
        {
            if (IsNull(content))
            {
                content = string.Empty;
            }
            return RegexHelper.Replace(content, "<.*?>");
        }
        #endregion

        #region 截取字符串
        public static string Substring(string content, int len)
        {
            try
            {

                if (!IsNull(content))
                {

                    if (content.Length > len)
                    {
                        return content.Substring(0, len);
                    }
                    else
                    {
                        return content.Substring(0, content.Length);
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return content;
            }

        }
        #endregion

        #region 清理其他特殊或则乱七八糟的字符 如 \n\t\
        public static string ClearSpecialFH(string inputStr)
        {
            string str = inputStr.Replace("\r", "");
            str = str.Replace("\n", "");
            str = str.Replace("\t", "");
            return str;
        }
        #endregion

    }
}

