using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ku.Common
{
    public class ConvertHelper
    {
        #region  实体转换
        /// <summary>
        /// DataTable 转换成实体 List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToEntityList<T>(DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name.ToLower() == dt.Columns[i].ColumnName.ToLower());
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                info.SetValue(s, item[i], null);
                            }
                        }
                        catch { }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        public static T ConvertDataTableToEntity<T>(DataTable dt)
        {

            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            T s = System.Activator.CreateInstance<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow item = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name.ToLower() == dt.Columns[i].ColumnName.ToLower());
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                info.SetValue(s, item[i], null);
                            }
                        }
                        catch { }
                    }
                }
            }
            return s;

        }
        #endregion

        #region  DBParamEntity转SqlParameter
        /// <summary>
        /// DBParamEntity转SqlParameter
        /// </summary> 
        public static SqlParameter[] ConvertToSqlParameter(DBParamEntity[] dbParam)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            if (dbParam != null && dbParam.Length > 0)
            {
                foreach (DBParamEntity item in dbParam)
                {
                    SqlParameter param = new SqlParameter("@" + item.FieldName, item.Value);
                    paramList.Add(param);
                }
            }
            return paramList.ToArray();
        }
        #endregion

    }
}
