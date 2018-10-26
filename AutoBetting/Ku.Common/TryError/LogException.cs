
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ku.Common
{
    [Serializable]
    public class LogException : Exception
    {
        public LogException(bool isLog = true)
            : base()
        {
            string Name = this.GetType().Name;
            if (isLog)
            {
                LogsRecord.write("LogException-Error", this.ToString());
            }
        }
        public LogException(string message, bool isLog = true)
            : base(message)
        {
            if (isLog)
            {
                LogsRecord.write("LogException-Error", message);
            }
        }
        public LogException(string message, Exception inner, bool isLog = true)
            : base(message, inner)
        {
            if (isLog)
            {
                LogsRecord.write("LogException-Error", message + "--" + inner.ToString());
            }
        }

        public List<string> ErrorList { get; set; }
        /// <summary>
        /// 添加错误信息 到错误列表
        /// </summary>
        /// <param name="message"></param>
        public void Add(string message)
        {
            if (ErrorList == null || ErrorList.Count <= 0)
            {
                ErrorList = new List<string>();
            }

            ErrorList.Add(message);
        }
        /// <summary>
        /// 错误详细内容
        /// </summary>
        public string GetErrorInfo
        {
            get
            {
                return Util.Join("", ErrorList);
            }
        }

        public string ErrorContent { get; set; }
    }
}
