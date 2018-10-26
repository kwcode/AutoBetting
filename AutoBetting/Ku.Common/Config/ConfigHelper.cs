using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    public class ConfigHelper
    {
        #region 得到AppSettings中的配置字符串信息
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary> 
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion

        #region 获取 ConnectionStrings 中的配置字符串信息
        /// <summary>
        /// 获取 ConnectionStrings 中的配置字符串信息
        /// 一般用于数据库连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionStringValue(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        #endregion
    }
}
