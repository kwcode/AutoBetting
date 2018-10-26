using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms.Model
{
    public class JsonServerTimeMillisecond
    {
        public string code { get; set; }
        public Data data { get; set; }
        public object msg { get; set; }

        public class Data
        {
            public long serverTime { get; set; }
        }
    }


}
