using System;
using System.Collections.Generic;
using System.Text;
namespace Ku.Domain
{
    /// <summary>
    /// 实体：JJ_一分快三Entity
    /// 创建工具 :TCode
    /// 生成时间:2018-11-03 23:13
    /// </summary>
    public class JJ_一分快三Entity
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
        /// 日期
        /// </summary>
        public DateTime D_Date { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public long issueNo { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string openTime { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string daxiao { get; set; }
        /// <summary>
        /// 单双
        /// </summary>
        public string danshuang { get; set; }
        /// <summary>
        /// 值1
        /// </summary>
        public int Value_1 { get; set; }
        /// <summary>
        /// 值2
        /// </summary>
        public int Value_2 { get; set; }
        /// <summary>
        /// 值3
        /// </summary>
        public int Value_3 { get; set; }
        /// <summary>
        /// 和值
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 大小组
        /// </summary>
        public int DXGroup { get; set; }
        /// <summary>
        /// 单双组
        /// </summary>
        public int DSGroup { get; set; }

        #endregion

    }
}

