using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms.Model
{

    public class JsonUserInfo
    {
        public string code { get; set; }
        public Data data { get; set; }
        public object msg { get; set; }

        public class Data
        {
            public string isdefaultLogin { get; set; }
            public User user { get; set; }
        }

        public class User
        {
            public double money { get; set; }
            public string loginName { get; set; }
            public object nickName { get; set; }
            public long loginTime { get; set; }
            public string type { get; set; }
            public int parentId { get; set; }
            public string userStatus { get; set; }
            public string bettingStatus { get; set; }
            public string freezeStatus { get; set; }
            public string blackStatus { get; set; }
            public Userdetail userDetail { get; set; }
            public int testAccountType { get; set; }
            public object userWithdrawAvail { get; set; }
            public int pwdEncodeType { get; set; }
        }

        public class Userdetail
        {
            public object id { get; set; }
            public object createDate { get; set; }
            public long accountId { get; set; }
            public object loginName { get; set; }
            public object nickName { get; set; }
            public object awardAmount { get; set; }
            public object ytdTotalAward { get; set; }
            public string imgName { get; set; }
            public string sex { get; set; }
            public object totalAward { get; set; }
            public string userLevel { get; set; }
            public string levelName { get; set; }
            public object gameName { get; set; }
            public object gameNickName { get; set; }
            public string userBirthDay { get; set; }
            public object favorGame { get; set; }
            public object userMail { get; set; }
            public string userMobile { get; set; }
            public string realName { get; set; }
        }
    }




}
