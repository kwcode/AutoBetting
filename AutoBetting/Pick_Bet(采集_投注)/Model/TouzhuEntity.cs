using Ku.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms.Model
{
    public class TouzhuEntity
    {
        /// <summary>
        /// 基础费用
        /// </summary>
        public double money { get; set; }
        /// <summary>
        /// 倍数
        /// </summary>
        public int bs { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool isSuccess { get; set; }

        public JJBetEnum codes { get; set; }


        public long IssueNo { get; set; }
    }


}
