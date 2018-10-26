using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Ku.DB
{
    internal class DBUtil
    {
        #region Object转换为Int32
        /// <summary>
        /// Object转换为Int32
        /// </summary>
        /// <param name="o">Object</param>
        /// <returns>int 报错也返回0</returns>
        public static int ConvertToInt32(object o)
        {
            try
            {
                int obj = 0;
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    obj = Convert.ToInt32(o);
                }
                return obj;
            }
            catch
            {
                return -99;
            }
        }
        #endregion

        /// <summary>
        /// 构造参数化参数 如@p1
        /// </summary>
        /// <param name="s">sql语句格式如 where name={0} and id={1}</param>
        /// <param name="paramsCount">参数数量</param>
        /// <returns></returns>
        private static string GetParamedSQL(string s, int paramsCount)
        {
            string[] array = new string[paramsCount];
            for (int i = 0; i < paramsCount; i++)
            {
                array[i] = "@Paramed" + i.ToString();
            }
            return string.Format(s, array);
        }
        protected static string GetParamName(int paramindex)
        {
            return "@Paramed" + paramindex.ToString();
        }
        /// <summary>
        /// 构造一个可用的参数的数据源执行
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="s"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static SqlCommand GetParamedCommand(string s, object[] objects)
        {
            SqlCommand sqlCommand = new SqlCommand();
            //dbCommand.CommandTimeout = this.CommandTimeout;
            sqlCommand.CommandText = GetParamedSQL(s, objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                SqlParameter dbParameter = new SqlParameter();
                dbParameter.ParameterName = GetParamName(i);
                if (objects[i] == null || Convert.IsDBNull(objects[i]))
                {
                    dbParameter.Value = DBNull.Value;
                }
                else
                {
                    dbParameter.Value = objects[i];
                }
                sqlCommand.Parameters.Add(dbParameter);
            }

            return sqlCommand;
        }

        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

    }
}
