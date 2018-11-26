using Ku.Common;
using Ku.DB;
using Ku.Forms.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        public int GetSingle(long issueNo)
        {
            object obj = DBHelper.GetSingle(connString, tableName, "count(0)", "  issueNo ={0}", issueNo);
            return Util.ConvertToInt32(obj);
        }

        public int Add(DBParamEntity[] dbParam)
        {
            SqlParameter[] pramsAdd = ConvertHelper.ConvertToSqlParameter(dbParam);
            return DBHelper.Add(connString, tableName, pramsAdd);
        }

        public List<OG1K3Entity> GetTop50List()
        {
            System.Text.StringBuilder sqlTxt = new StringBuilder();
            sqlTxt.Append(" SELECT TOP 50 Id,issueNo FROM JJ_一分快三 WHERE  issueNo<");
            sqlTxt.Append(" (SELECT  TOP 1 issueNo FROM JJ_一分快三 WHERE  DXGroup IS NULL    ORDER BY issueNo      ASC   ) ");
            sqlTxt.Append("ORDER BY issueNo DESC ");
            DataSet ds = Ku.DB.DBHelper.Query(connString, sqlTxt.ToString());
            return Ku.Common.ConvertHelper.ConvertDataTableToEntityList<OG1K3Entity>(ds.Tables[0]);
        }

        public List<OG1K3Entity> GetNoGroupList(long issueNo)
        {
            DataSet ds = Ku.DB.DBHelper.Query(connString, "SELECT * FROM JJ_一分快三 WHERE  issueNo>{0}", issueNo);
            return Ku.Common.ConvertHelper.ConvertDataTableToEntityList<OG1K3Entity>(ds.Tables[0]);
        }

    }
}
