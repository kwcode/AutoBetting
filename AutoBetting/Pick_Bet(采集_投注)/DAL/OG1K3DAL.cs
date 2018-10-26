using Ku.Common;
using Ku.DB;
using Ku.Forms.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Ku.Forms.DAL
{
    public class OG1K3DAL
    {
        #region 提供方便外部调用的静态实例化方法
        /// <summary>
        /// 提供方便外部调用的静态实例化方法 
        /// </summary>
        public static OG1K3DAL Instance
        {
            get
            {
                return new OG1K3DAL();
            }
        }

        #endregion
        private string connString = string.Empty;
        private string tableName = "JJ_一分快三";
        public OG1K3DAL()
        {
            connString = Ku.Common.ConfigHelper.GetConnectionStringValue("ConnString1");
        }
        public int GetSingle(string issueNo)
        {
            object obj = DBHelper.GetSingle(connString, tableName, "count(0)", "  issueNo ={0}", issueNo);
            return Util.ConvertToInt32(obj);
        }

        public int Add(DBParamEntity[] dbParam)
        {
            SqlParameter[] pramsAdd = ConvertHelper.ConvertToSqlParameter(dbParam);
            return DBHelper.Add(connString, tableName, pramsAdd);
        }

    }
}
