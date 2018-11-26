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
    /// 创建日期:2018-11-03 22:24
    /// </summary>
    public class UserInfoDAL : BaseDAL<UserInfoEntity>
    {

        #region 必须的构造函数，指定数据库链接和表名
        /// <summary>
        /// 必须的构造函数，指定数据库链接和表名
        /// </summary>
        public UserInfoDAL()
            : base(DbConfig.ConnString, "UserInfo")
        {
        }
        #endregion

        #region 提供方便外部调用的静态实例化方法
        /// <summary>
        /// 提供方便外部调用的静态实例化方法 
        /// </summary>
        public static UserInfoDAL Instance
        {
            get
            {
                return new UserInfoDAL();
            }
        }
        #endregion
        #region 新增
        /// <summary>
        ///新增
        /// <summary>
        /// <param name="entity">增加的实体</param>
        /// <returns>成功返回自增ID</returns>
        public int Add(UserInfoEntity entity)
        {
            SqlParameter[] pramsAdd =
			{
				DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,entity.CreateTS),
				DALUtil.MakeInParam("@money",System.Data.SqlDbType.Decimal,9,entity.money),
			};
            return Add(pramsAdd);
        }
        #endregion

        public void EditMoney(int userId, decimal money)
        {
            SqlParameter[] pramsModify =
			{ 
				DALUtil.MakeInParam("@money",System.Data.SqlDbType.Decimal,9,money),
			};
            Modify(pramsModify, userId);
        }


    }
}

