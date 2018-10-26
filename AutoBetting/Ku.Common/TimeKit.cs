using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    /// <summary>
    /// 时间辅助类
    /// </summary>
    public class TimeKit
    {
        public static int GetAge(object o, int defaultValue = 0)
        {
            try
            {

                DateTime dTime = Convert.ToDateTime(o);
                int now = int.Parse(DateTime.Today.ToString("yyyy"));
                int dob = int.Parse(dTime.ToString("yyyy"));
                int Age = (now - dob);
                return Age;
            }
            catch
            {
                return defaultValue;
            }
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

        #region 时间戳转换成时间
        /// <summary>
        /// 时间戳转为C#格式时间        
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
         
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
        #endregion

    }
}
