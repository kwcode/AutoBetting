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
    /// 创建日期:2018-11-03 22:30
    /// </summary>
    public class BetRecordDAL : BaseDAL<BetRecordEntity>
    {

        #region 必须的构造函数，指定数据库链接和表名
        /// <summary>
        /// 必须的构造函数，指定数据库链接和表名
        /// </summary>
        public BetRecordDAL()
            : base(DbConfig.ConnString, "BetRecord")
        {
        }
        #endregion

        #region 提供方便外部调用的静态实例化方法
        /// <summary>
        /// 提供方便外部调用的静态实例化方法 
        /// </summary>
        public static BetRecordDAL Instance
        {
            get
            {
                return new BetRecordDAL();
            }
        }
        #endregion
        #region 新增
        /// <summary>
        ///新增
        /// <summary>
        /// <param name="entity">增加的实体</param>
        /// <returns>成功返回自增ID</returns>
        public int Add(BetRecordEntity entity)
        {
            SqlParameter[] pramsAdd =
			{
			    DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,entity.CreateTS),
				DALUtil.MakeInParam("@issueNo",System.Data.SqlDbType.BigInt,8,entity.issueNo),
				DALUtil.MakeInParam("@BetValue",System.Data.SqlDbType.NVarChar,100,entity.BetValue),
				DALUtil.MakeInParam("@ResultValue",System.Data.SqlDbType.NVarChar,100,entity.ResultValue),
				DALUtil.MakeInParam("@BetAmount",System.Data.SqlDbType.Int,4,entity.BetAmount),
				DALUtil.MakeInParam("@Balance",System.Data.SqlDbType.Decimal,9,entity.Balance),
				DALUtil.MakeInParam("@isSuccess",System.Data.SqlDbType.Int,4,entity.isSuccess),
				DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,entity.UserID),
				DALUtil.MakeInParam("@WinStatu",System.Data.SqlDbType.Decimal,9,entity.WinStatu),
			};
            return Add(pramsAdd);
        }
        #endregion


    }
}

