using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ku.Domain;
using Ku.DALCommon;

namespace Ku.DAL
{
    /// <summary>
    /// 创建工具 :TCode
    /// 创建日期:2018-11-03 23:13
    /// </summary>
    public class JJ_一分快三DAL : BaseDAL<JJ_一分快三Entity>
    {

        #region 必须的构造函数，指定数据库链接和表名
        /// <summary>
        /// 必须的构造函数，指定数据库链接和表名
        /// </summary>
        public JJ_一分快三DAL()
            : base(DbConfig.ConnString, "JJ_一分快三")
        {
        }
        #endregion

        #region 提供方便外部调用的静态实例化方法
        /// <summary>
        /// 提供方便外部调用的静态实例化方法 
        /// </summary>
        public static JJ_一分快三DAL Instance
        {
            get
            {
                return new JJ_一分快三DAL();
            }
        }
        #endregion

        #region 新增
        /// <summary>
        ///新增
        /// <summary>
        /// <param name="entity">增加的实体</param>
        /// <returns>成功返回自增ID</returns>
        public int Add(JJ_一分快三Entity entity)
        {
            SqlParameter[] pramsAdd =
			{
				DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,entity.CreateTS),
				DALUtil.MakeInParam("@D_Date",System.Data.SqlDbType.Date,3,entity.D_Date),
				DALUtil.MakeInParam("@issueNo",System.Data.SqlDbType.BigInt,8,entity.issueNo),
				DALUtil.MakeInParam("@openTime",System.Data.SqlDbType.VarChar,100,entity.openTime),
				DALUtil.MakeInParam("@daxiao",System.Data.SqlDbType.NVarChar,10,entity.daxiao),
				DALUtil.MakeInParam("@danshuang",System.Data.SqlDbType.NVarChar,10,entity.danshuang),
				DALUtil.MakeInParam("@Value_1",System.Data.SqlDbType.Int,4,entity.Value_1),
				DALUtil.MakeInParam("@Value_2",System.Data.SqlDbType.Int,4,entity.Value_2),
				DALUtil.MakeInParam("@Value_3",System.Data.SqlDbType.Int,4,entity.Value_3),
				DALUtil.MakeInParam("@Count",System.Data.SqlDbType.Int,4,entity.Count),
				DALUtil.MakeInParam("@DXGroup",System.Data.SqlDbType.Int,4,entity.DXGroup),
				DALUtil.MakeInParam("@DSGroup",System.Data.SqlDbType.Int,4,entity.DSGroup),
			};
            return Add(pramsAdd);
        }
        #endregion

        #region 获取最新的 期数 如果没有则返回
        public int GetNewId(int Id)
        {
            JJ_一分快三Entity entity = GetAnEntity("Id,issueNo", "Id>{0}  ORDER BY  Id   ASC ", Id);
            if (entity != null && entity.ID > 0)
            {
                return entity.ID;
            }
            else { return Id; }
        }
        #endregion

        #region 获取最近的30条
        public List<JJ_一分快三Entity> GetTop30(int id)
        {
            return GetList("top 30 *", " id<={0}  ORDER BY  id DESC ", id);
        }
        #endregion
    }
}

