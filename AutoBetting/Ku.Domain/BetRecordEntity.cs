using System;
using System.Collections.Generic;
using System.Text;
namespace Ku.Domain
{
    /// <summary>
    /// 实体：BetRecordEntity
    /// 创建工具 :TCode
    /// 生成时间:2018-11-04 00:31
    /// </summary>
    public class BetRecordEntity
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
        /// 期数
        /// </summary>
        public long issueNo { get; set; }
        /// <summary>
        /// 投注值
        /// </summary>
        public string BetValue { get; set; }
        /// <summary>
        /// 结果只
        /// </summary>
        public string ResultValue { get; set; }
        /// <summary>
        /// 投注金额
        /// </summary>
        public int BetAmount { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 是否正确
        /// </summary>
        public int isSuccess { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 中奖 状态
        /// </summary>
        public decimal WinStatu { get; set; }

        #endregion

    }
}

