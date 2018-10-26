using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    /// <summary>
    /// 字符串等 转换的扩展类 
    /// 调用方式 引用名称空间 using Ku.Common;
    /// </summary>
    public static class UtilExpand
    {
        #region 转换为Int32

        public static int ToInt32(this object oValue, int defaultValue = 0)
        {
            int iValue = 0;
            if (oValue != null && (!int.TryParse(oValue.ToString(), out iValue)))
                iValue = defaultValue;
            return iValue;
        }

        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="defaultValue">默认值为0</param>
        public static int ToInt32(this string sValue, int defaultValue)
        {
            int iValue = 0;
            if (!int.TryParse(sValue, out iValue))
                iValue = defaultValue;
            return iValue;
        }

        #endregion

        #region 转换为T实体类型

        /// <summary>
        /// 转换为T实体类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="oValue">需要转换的对象</param>
        /// <param name="IsNew">是否新生成一个</param>
        /// <returns></returns>
        public static T ToModel<T>(this object oValue, bool IsNew = true) where T : new()
        {
            T tModel = default(T);
            try { tModel = (T)oValue; }
            catch { tModel = IsNew ? new T() : default(T); }
            return tModel;
        }
        #endregion

        #region 转换为decimal类型
        /// <summary>
        /// 转换为decimal类型
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        public static decimal ToDecimal(this string sValue, decimal defaultValue = 0)
        {
            decimal dValue = 0;
            if (!decimal.TryParse(sValue, out dValue))
                dValue = defaultValue;
            return dValue;
        }
        #endregion

        #region 转换为DateTime类型
        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        public static DateTime ToDateTime(this string sValue, DateTime? defaultValue = null)
        {
            DateTime dValue = new DateTime();
            if (!DateTime.TryParse(sValue, out dValue))
                if (defaultValue != null)
                    dValue = (DateTime)defaultValue;
            return dValue;
        }
        #endregion

        #region 检测是否为空
        public static bool IsNull(this string sValue)
        {
            return string.IsNullOrEmpty(sValue);
        }
        #endregion

        #region 通用转换JSON  JsonConvert.SerializeObject
        /// <summary>
        /// 通用转换JSON  JsonConvert.SerializeObject
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static string ToJson(this object objData)
        {
            var iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(objData, iso);
        }
        #endregion

        #region 判断是否是int值

        public static bool IsInt(this object oValue)
        {
            if (oValue == null)
                return false;
            int iValue = 0;
            if (int.TryParse(oValue.ToString(), out iValue))
                return true;
            else
                return false;
        }
        #endregion

        #region 判断是否是decimal值

        /// <summary>
        /// 判断是否是decimal值
        /// </summary>
        public static bool IsDecimal(this object oValue)
        {
            if (oValue == null)
                return false;
            decimal dValue = 0;
            if (decimal.TryParse(oValue.ToString(), out dValue))
                return true;
            else
                return false;
        }
        #endregion

        #region 判断是否是DateTime

        /// <summary>
        /// 判断是否是DateTime
        /// </summary>
        public static bool IsDateTime(this object oValue)
        {
            if (oValue == null)
                return false;
            DateTime datetime = DateTime.UtcNow;
            if (DateTime.TryParse(oValue.ToString(), out datetime))
                return true;
            else
                return false;
        }

        #endregion

        #region 去掉前后的空格
        /// <summary>
        /// 从当前 System.String 对象移除所有前导空白字符和尾部空白字符。
        /// </summary>
        /// <param name="oValue"></param>
        /// <returns></returns>
        public static string ToTrim(this object oValue)
        {
            string o = oValue.ToString();
            return o.Trim();
        }
        #endregion
    }
}
