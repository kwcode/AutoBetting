
using Ku.Common;
using Ku.DALCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.DAL
{
    public class DbConfig
    {

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnString
        {
            get
            {
                string connString = DALUtil.GetConnString("ConnString1", false);
                return connString;
            }
        }


    }
}
