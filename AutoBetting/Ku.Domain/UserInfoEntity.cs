using System;
using System.Collections.Generic;
using System.Text;
namespace Ku.Domain
{
    /// <summary>
    /// 实体：UserInfoEntity
    /// 创建工具 :TCode
    /// 生成时间:2018-11-03 22:24
    /// </summary>
    public class UserInfoEntity
    {
        #region 原始字段

        /// <summary>
        /// ID主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTS { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal money { get; set; }

        #endregion

    }
}

