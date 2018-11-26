using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms
{

    public class ResBetSingle
    {
        /// <summary>
        /// 500137556
        /// </summary>
        public long accountId { get; set; }
        /// <summary>
        /// 1540306708819
        /// </summary>
        public long clientTime { get; set; }
        /// <summary>
        /// OG1K3
        /// </summary>
        public string gameId { get; set; }
        /// <summary>
        /// 201810231379
        /// </summary>
        public string issue { get; set; }
        public BetEntity[] item { get; set; }
    }

    public class BetEntity
    {
        /// <summary>
        /// K3002001001
        /// </summary>
        public string methodid { get; set; }
        /// <summary>
        /// 1数量
        /// </summary>
        public int nums { get; set; }
        /// <summary>
        /// 回扣
        /// </summary>
        public double rebate { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string times { get; set; }
        public double money { get; set; }
        public int mode { get; set; }
        /// <summary>
        /// 201810231379
        /// </summary>
        public string issueNo { get; set; }
        /// <summary>
        /// codes
        /// </summary>
        public string codes { get; set; }
        /// <summary>
        /// K3002001010
        /// </summary>
        public string[] playId { get; set; }
    }

}
