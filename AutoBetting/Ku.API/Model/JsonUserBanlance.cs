using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.API.Model
{
    public class JsonUserBanlance
    {
        public string code { get; set; }
        public Data data { get; set; }
        public object msg { get; set; }

        public class Data
        {
            public object agentRebates { get; set; }
            public double money { get; set; }
            public object loginName { get; set; }
            public object nickName { get; set; }
            public object loginTime { get; set; }
            public object type { get; set; }
            public object parentId { get; set; }
            public object userStatus { get; set; }
            public object bettingStatus { get; set; }
            public object freezeStatus { get; set; }
            public object blackStatus { get; set; }
            public object userDetail { get; set; }
            public object testAccountType { get; set; }
            public object userWithdrawAvail { get; set; }
            public object pwdEncodeType { get; set; }
        }
    }


}
