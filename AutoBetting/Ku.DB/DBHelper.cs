using Ku.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Ku.DB
{
    /// <summary>
    /// 比较通用的sql帮助类， 
    /// 必须参数化，防注入
    /// </summary>
    public class DBHelper
    {
        #region 新增
        /// <summary>
        /// 新增返回 自增ID
        /// </summary> 
        public static int Add(string connString, string Table, SqlParameter[] pramsAdd)
        {
            try
            {
                if (pramsAdd != null && pramsAdd.Length > 0)
                {
                    using (SqlConnection MyConn = new SqlConnection(connString))
                    {
                        Object obj = new object();
                        MyConn.Open();
                        SqlCommand cmd = new SqlCommand();
                        StringBuilder sqlParameterName = new StringBuilder();
                        StringBuilder sqlValue = new StringBuilder();
                        foreach (SqlParameter pram in pramsAdd)
                        {
                            //Value为null值不添加
                            if (pram.Value != null)
                            {
                                //时间小于1100的跳过
                                if (pram.DbType == DbType.DateTime)
                                {
                                    if (Convert.ToDateTime(pram.Value) < Convert.ToDateTime("1000-1-1"))
                                    {
                                        continue;
                                    }
                                }

                                //生成ParameterName 表()表字段
                                sqlParameterName.Append(pram.ParameterName.Replace("@", "") + ",");

                                //生成ParameterName 表()表字段
                                sqlValue.Append(pram.ParameterName + ",");

                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                        if (sqlParameterName.ToString().Trim() != "")
                        {
                            //删除最后多余的 ,(逗号)
                            sqlParameterName.Remove(sqlParameterName.Length - 1, 1);

                            //删除最后多余的 ,(逗号)
                            sqlValue.Remove(sqlValue.Length - 1, 1);
                        }
                        string sqlText = "insert into " + Table + " (" + sqlParameterName + ") Values(" + sqlValue + ") ;select @@IDENTITY";
                        cmd.Connection = MyConn;
                        //初始Sql语句
                        cmd.CommandText = sqlText;
                        obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return DBUtil.ConvertToInt32(obj.ToString());
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new LogException("Add()自增" + ex.ToString());
            }
        }
        /// <summary>
        /// 新增返回  影响行数
        /// </summary> 
        public static int Add2(string connString, string Table, SqlParameter[] pramsAdd)
        {
            try
            {
                if (pramsAdd != null && pramsAdd.Length > 0)
                {
                    using (SqlConnection MyConn = new SqlConnection(connString))
                    {
                        Object obj = new object();
                        MyConn.Open();
                        SqlCommand cmd = new SqlCommand();
                        StringBuilder sqlParameterName = new StringBuilder();
                        StringBuilder sqlValue = new StringBuilder();
                        foreach (SqlParameter pram in pramsAdd)
                        {
                            //Value为null值不添加
                            if (pram.Value != null)
                            {
                                //生成ParameterName 表()表字段
                                sqlParameterName.Append(pram.ParameterName.Replace("@", "") + ",");

                                //生成ParameterName 表()表字段
                                sqlValue.Append(pram.ParameterName + ",");

                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                        if (sqlParameterName.ToString().Trim() != "")
                        {
                            //删除最后多余的 ,(逗号)
                            sqlParameterName.Remove(sqlParameterName.Length - 1, 1);

                            //删除最后多余的 ,(逗号)
                            sqlValue.Remove(sqlValue.Length - 1, 1);
                        }
                        string sqlText = "insert into " + Table + " (" + sqlParameterName + ") Values(" + sqlValue + ") ";
                        cmd.Connection = MyConn;
                        //初始Sql语句
                        cmd.CommandText = sqlText;
                        obj = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return DBUtil.ConvertToInt32(obj.ToString());
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new LogException("Add()行数-------" + ex.ToString());
            }
        }
        #endregion

        #region 修改
        /// <summary>
        ///  修改记录
        /// </summary>      
        /// <param name="Table">表名</param>
        /// <param name="pramsAdd">表字段值</param>
        /// <returns>影响数</returns>
        public static int Modify(string connString, string Table, SqlParameter[] pramsModify, SqlParameter[] pramsWhere)
        {
            try
            {
                if (pramsModify != null && pramsModify.Length > 0)
                {
                    using (SqlConnection MyConn = new SqlConnection(connString))
                    {
                        Object obj = new object();
                        MyConn.Open();
                        SqlCommand cmd = new SqlCommand();

                        StringBuilder sqlParameterSet = new StringBuilder();
                        foreach (SqlParameter pram in pramsModify)
                        {
                            if (pram.Value != null)
                            {
                                //格式如name=@name,time=@time
                                sqlParameterSet.Append(pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName + ",");
                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                        //删除最后多余的 ,(逗号)
                        if (sqlParameterSet.ToString().Trim() != "")
                        {
                            sqlParameterSet.Remove(sqlParameterSet.Length - 1, 1);
                        }

                        StringBuilder sqlWhere = new StringBuilder();
                        sqlWhere.Append(" 1=1 ");//保证pramsWhere参数为空的时候 正常使用
                        if (pramsWhere != null && pramsWhere.Length > 0)
                        {
                            string ParameterNameLeft = "";
                            foreach (SqlParameter pram in pramsWhere)
                            {
                                ParameterNameLeft = pram.ParameterName.ToString().Replace("@", "");
                                pram.ParameterName = pram.ParameterName + "_Where";//避免和修改的参数冲突 加_Where
                                //格式如：AND id=@id__Where
                                sqlWhere.Append(" AND " + ParameterNameLeft + "=" + pram.ParameterName);
                                cmd.Parameters.Add(pram);
                            }
                        }
                        //Sql语句
                        string sqlText = " update  " + Table + " set " + sqlParameterSet + " where " + sqlWhere;

                        //初始连接Conn
                        cmd.Connection = MyConn;
                        //初始Sql语句
                        cmd.CommandText = sqlText;
                        obj = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return DBUtil.ConvertToInt32(obj.ToString());
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }
        #endregion

        #region 删除
        public static int Delete(string connString, string Table, SqlParameter[] pramsWhere)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    MyConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    StringBuilder sqlWhere = new StringBuilder();
                    if (pramsWhere != null && pramsWhere.Length > 0)
                    {
                        string ParameterNameLeft = "";
                        foreach (SqlParameter pram in pramsWhere)
                        {
                            ParameterNameLeft = pram.ParameterName.ToString().Replace("@", "");
                            pram.ParameterName = pram.ParameterName + "_Where";//避免和修改的参数冲突 加_Where
                            //格式如：AND id=@id__Where
                            sqlWhere.Append(" AND " + ParameterNameLeft + "=" + pram.ParameterName);
                            cmd.Parameters.Add(pram);
                        }
                    }
                    if (string.IsNullOrEmpty(sqlWhere.ToString().Trim()))
                    {
                        return 0;
                    }
                    else
                    {
                        sqlWhere.Insert(0, " 1=1 ");
                    }
                    //Sql语句
                    string sqlText = " DELETE " + Table + " WHERE " + sqlWhere;

                    //初始连接Conn
                    cmd.Connection = MyConn;
                    //初始Sql语句
                    cmd.CommandText = sqlText;
                    obj = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return DBUtil.ConvertToInt32(obj.ToString());
                }

            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }
        #endregion

        #region 获取一条数据
        /// <summary>
        /// 获取一条数据的DataTable
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="Table"></param>
        /// <param name="SelectIF"></param>
        /// <param name="pramsWhere"></param>
        /// <returns></returns>
        public static DataTable GetDataTable1(string connString, string Table, string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            try
            {
                if (SelectIF.ToLower().IndexOf("top") < 0)
                {
                    SelectIF = " top 1 " + SelectIF;
                }
                DataTable dataTable = GetDataTable2(connString, Table, SelectIF, pramsWhere, OrderName);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }
        #endregion

        #region 查询-取得集合
        /// <summary>
        /// 返回DataTable(多条记录数据源) 
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="SelectIF">条件：如ID,Title 以逗号分隔</param>
        /// <param name="pramsWhere">SqlParameter参数传值</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetDataTable2(string connString, string Table, string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    SqlCommand cmd = new SqlCommand();
                    MyConn.Open();
                    StringBuilder sqlWhere = new StringBuilder();
                    if (pramsWhere != null)
                    {
                        foreach (SqlParameter pram in pramsWhere)
                        {
                            if (pram.Value != null)
                            {
                                sqlWhere.Append(" AND " + pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName);
                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                    }
                    string sqlText = " SELECT " + SelectIF + " FROM  " + Table + " WHERE 1=1 " + sqlWhere.ToString();
                    if (!string.IsNullOrEmpty(OrderName))
                    {
                        sqlText += " ORDER BY " + OrderName;
                    }
                    cmd.Connection = MyConn;
                    cmd.CommandText = sqlText;
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable("yes");
                    MyAdapter.Fill(dataTable);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// 条件 如：Title='{0}'
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="Table">表</param>
        /// <param name="SelectIF"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="OrderName">排序值 </param>
        /// <param name="strWhere">条件如不带Where 如：Title='{0}'</param> 
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataTable GetDataTablePage(string connString, string Table, string SelectIF, int PageIndex, int PageSize, string OrderName, string strWhere, params object[] values)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT " + SelectIF + " FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    if (!string.IsNullOrEmpty(OrderName.Trim()))
                    {
                        strSql.Append("order by T." + OrderName);
                    }
                    strSql.Append(")AS Row, " + SelectIF + "  from " + Table + " T ");
                    strSql.Append(" WHERE " + strWhere);

                    strSql.Append(" ) TT");
                    strSql.Append(" WHERE TT.Row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize));
                    SqlCommand cmd = DBUtil.GetParamedCommand(strSql.ToString(), values);
                    MyConn.Open();
                    cmd.Connection = MyConn;
                    //cmd.CommandText = strSql.ToString();
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable("yes");
                    MyAdapter.Fill(dataTable);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }

        //public static DataTable GetDataTablePage(string connString, string Table, string SelectIF, int PageIndex, int PageSize, string strWhere, string OrderName = "ID", params object[] values)
        //{
        //    try
        //    {
        //        using (SqlConnection MyConn = new SqlConnection(connString))
        //        {
        //            Object obj = new object();
        //            SqlCommand cmd = new SqlCommand();
        //            MyConn.Open();
        //            cmd.Connection = MyConn;
        //            cmd.CommandText = "p_GetPageList";//存储过程
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = Table;
        //            cmd.Parameters.Add("@SelectIF", SqlDbType.NVarChar, 500).Value = SelectIF.ToString();
        //            cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = PageIndex.ToString();
        //            cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = PageSize.ToString();
        //            cmd.Parameters.Add("@strWhere", SqlDbType.NVarChar, 100).Value = strWhere;
        //            cmd.Parameters.Add("@OrderName", SqlDbType.NVarChar, 200).Value = OrderName;

        //            SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
        //            DataTable dataTable = new DataTable("yes");
        //            MyAdapter.Fill(dataTable);
        //            MyAdapter.Dispose();
        //            cmd.Parameters.Clear();
        //            MyConn.Close();
        //            MyConn.Dispose();
        //            return dataTable;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new LogException(ex.ToString());
        //    }
        //}

        #endregion

        #region 执行sql语句 一般用于模糊查询或链接查询  已经参数化
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string connectionString, string SQLString, SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                DBUtil.PrepareCommand(cmd, connection, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new LogException(ex.ToString());
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 获取数据集合 可做拼接语句使用
        /// </summary>
        /// <param name="connString">数据库链接</param>
        /// <param name="sqlText">sql语句如：select * from 表 where  id={0},name like '%{1}%' </param>
        /// <param name="values">数据集合</param>
        /// <returns></returns>
        public static DataSet Query(string connString, string sqlText, params object[] values)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    SqlCommand cmd = DBUtil.GetParamedCommand(sqlText, values);
                    MyConn.Open();
                    cmd.Connection = MyConn;
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    MyAdapter.Fill(ds);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }

        #endregion

        #region 获取结果的第一行的第一列,忽略其他列或行。
        /// <summary>
        /// 获取结果的第一行的第一列,忽略其他列或行。
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="Table">Count(0)</param>
        /// <param name="SelectIF"></param>
        /// <param name="pramsWhere"></param>
        /// <returns></returns>
        public static object GetSingle(string connString, string Table, string SelectIF, SqlParameter[] pramsWhere)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        MyConn.Open();
                        StringBuilder sqlWhere = new StringBuilder();
                        if (pramsWhere != null)
                        {
                            foreach (SqlParameter pram in pramsWhere)
                            {
                                if (pram.Value != null)
                                {
                                    sqlWhere.Append(" AND " + pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName);
                                    //添加到 cmd 中参数
                                    cmd.Parameters.Add(pram);
                                }
                            }
                        }
                        string sqlText = " SELECT " + SelectIF + " FROM  " + Table + " WHERE 1=1 " + sqlWhere.ToString();
                        cmd.Connection = MyConn;
                        cmd.CommandText = sqlText;
                        Object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }
        /// <summary>
        /// 获取结果的第一行的第一列,忽略其他列或行。
        /// 条件：ID={0}
        /// </summary> 
        /// <param name="SelectIF">Count(0)</param>
        /// <param name="sqlWhere">ID={0}</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public static object GetSingle(string connString, string Table, string SelectIF, string sqlWhere, params object[] values)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    string sqlText = " SELECT " + SelectIF + " FROM  " + Table + " WHERE " + sqlWhere;
                    SqlCommand cmd = DBUtil.GetParamedCommand(sqlText, values);
                    MyConn.Open();
                    cmd.Connection = MyConn;
                    Object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return obj;

                }
            }
            catch (Exception ex)
            {
                throw new LogException(ex.ToString());
            }
        }

        #endregion

        #region 执行语句
        /// <summary>
        /// 执行语句 返回影响行数
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="SqlTxt"></param>
        /// <param name="pramsWhere"></param>
        /// <returns></returns>
        public static int ExecIntResult(string connString, string sqlText, params object[] values)
        {
            int row = 0;
            using (SqlConnection MyConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = DBUtil.GetParamedCommand(sqlText, values))
                {
                    MyConn.Open();
                    cmd.Connection = MyConn;
                    row = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                }

            }
            return row;
        }
        #endregion

    }
}
