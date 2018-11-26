using Ku.Common;
using Ku.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Ku.Forms.DAL
{
    public class JJ_BetSingleDAL
    {
        #region 提供方便外部调用的静态实例化方法
        /// <summary>
        /// 提供方便外部调用的静态实例化方法 
        /// </summary>
        public static JJ_BetSingleDAL Instance
        {
            get
            {
                return new JJ_BetSingleDAL();
            }
        }

        #endregion
        private string connString = string.Empty;
        private string tableName = "JJ_BetSingle";
        public JJ_BetSingleDAL()
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

        public int UploadResult(long issueNo, string sureResult)
        {

            SqlParameter[] pramsModify = {  
                                new SqlParameter(){ ParameterName="@sureResult", Value=sureResult}, 
                            };
            SqlParameter[] pramsWhere = {  
                                new SqlParameter(){ ParameterName="@issueNo", Value=issueNo}, 
                            };
            return DBHelper.Modify(connString, tableName, pramsModify, pramsWhere);
        }

    }
}
