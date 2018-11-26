
using Ku.Common;
using Ku.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.DAL
{
    public class DPConnect
    {
        #region 默认的连接池

        private static DataPool _DataConnString = null;

        public static DataPool DataPool_ConnString
        {
            get
            {
                if (_DataConnString == null)
                {
                    _DataConnString = new DataPool(1, DbConfig.ConnString);
                }
                return _DataConnString;
            }
        }
        #endregion
         

    }
}
