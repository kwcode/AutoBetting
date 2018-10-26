using Ku.Common;
using Ku.Forms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms
{
    public class JJApiKit
    {
        private string Cookie = string.Empty;
        private long accountId = 0;
        private string Domain = "www.29039.cc";
        private double money = 0;
        private string gameId = "OG1K3";//一分快三
        private string userName = string.Empty;
        private int requestFailCount = 0;
        private int requestMaxFailCount = 3;//请求允许次数 

        #region 登陆账号 
        public bool Login(string uName, string pwd, string phone = "1540139107855")
        {
            userName = uName;
            string md5Pwd = Ku.Common.MD5Helper.MD5Encrypt(pwd);
            Ku.Common.HttpHelper http = new Common.HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.URL = "http://" + Domain + "/tools/_ajax/login";
            //httpItem.Postdata = "{\"requirement\":[\"OG1K3\"]}";
            httpItem.Postdata = "{\"loginName\":\"" + userName + "\",\"loginPwd\":\"" + md5Pwd + "\",\"validCode\":\"\",\"validateDate\":\"" + phone + "\",\"isdefaultLogin\":true}";

            httpItem.Method = "POST";
            httpItem.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            httpItem.Host = Domain;
            httpItem.Cookie = Cookie;
            httpItem.ContentType = "application/json";
            httpItem.Referer = "http://" + Domain + "/login";
            HttpResult httpRes = http.GetHtml(httpItem);
            if (httpRes.StatusCode == System.Net.HttpStatusCode.OK)
            {
                requestFailCount = 0;
                string html = httpRes.Html;
                JsonUserInfo jsonR = Util.DeserializeObject<JsonUserInfo>(html);
                string code = jsonR.code;
                if (code == "success")
                {
                    Cookie = httpRes.Cookie;
                    accountId = jsonR.data.user.userDetail.accountId;
                    money = jsonR.data.user.money;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                requestFailCount++;
                LogsRecord.write("接口调用异常", "重新登录，次数=" + requestFailCount + ",异常=" + httpRes.Html);
                if (requestFailCount >= requestMaxFailCount)
                    throw new LogException("请求异常，" + httpRes.Html);
                return Login(uName, pwd, phone);

            }
        }

        #endregion

        #region 得到抽奖的结果
        /// <summary>
        /// 得到抽奖的结果
        /// </summary>
        /// <returns></returns>
        public string GetLotteryOpenResult()
        {
            Ku.Common.HttpHelper http = new Common.HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.URL = "http://" + Domain + "/tools/_ajax/cache/lotteryOpenCache";
            httpItem.Method = "POST";
            httpItem.Postdata = "{\"requirement\":[\"OG1K3\"]}";
            httpItem.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            httpItem.Host = Domain;
            httpItem.Referer = "http://" + Domain + "/lottery/K3/OG1K3";
            httpItem.ContentType = "application/json";
            httpItem.Cookie = Cookie;
            HttpResult httpRes = http.GetHtml(httpItem);
            if (httpRes.StatusCode == System.Net.HttpStatusCode.OK)
            {
                requestFailCount = 0;
                string html = httpRes.Html;
                return html;
            }
            else
            {
                requestFailCount++;
                LogsRecord.write("接口调用异常", "获取开奖结果，次数=" + requestFailCount + ",异常=" + httpRes.Html);
                if (requestFailCount >= requestMaxFailCount)
                    throw new LogException("请求异常，" + httpRes.Html);
                return GetLotteryOpenResult();

            }
        }

        #endregion

        #region 投注

        /// <summary>
        /// 投注
        /// </summary>
        /// <returns></returns>
        public string BetSingle(string issueNo, double money, JJBetEnum codes)
        {
            ResBetSingle resEntity = new ResBetSingle();
            resEntity.accountId = accountId;
            resEntity.clientTime = 1234;
            resEntity.gameId = gameId;
            resEntity.issue = issueNo;
            List<BetEntity> betList = new List<BetEntity>();
            betList.Add(new BetEntity()
            {
                methodid = "",
                nums = 1,//数量
                rebate = 0.00,//回扣
                times = money.ToString(),
                money = money,
                mode = 1,
                issueNo = issueNo,
                codes = codes.ToString(),
                playId = new string[] { "K3002001010" }
            });
            resEntity.item = betList.ToArray();

            Ku.Common.HttpHelper http = new Common.HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.URL = "http://" + Domain + "/tools/_ajax/cache/lotteryOpenCache";
            httpItem.Method = "POST";
            httpItem.Postdata = "{\"requirement\":[\"OG1K3\"]}";
            httpItem.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            httpItem.Host = Domain;
            httpItem.Referer = "http://" + Domain + "/lottery/K3/OG1K3";
            httpItem.ContentType = "application/json";
            httpItem.Cookie = Cookie;
            HttpResult httpRes = http.GetHtml(httpItem);
            string html = httpRes.Html;

            return html;
        }

        #endregion

        #region 获取余额
        /// <summary>
        /// 获取余额
        /// </summary>
        /// <returns></returns>
        public double GetUserBanlance()
        {
            Ku.Common.HttpHelper http = new Common.HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.URL = "http://" + Domain + "/tools/_ajax//getUserBanlance";
            httpItem.Method = "POST";
            httpItem.Postdata = "{\"userName\": \"" + userName + "\"}";
            httpItem.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            httpItem.Host = Domain;
            httpItem.Referer = "http://" + Domain + "/home";
            httpItem.ContentType = "application/json";
            httpItem.Cookie = Cookie;
            HttpResult httpRes = http.GetHtml(httpItem);
            string html = httpRes.Html;
            JsonUserBanlance jsResult = Util.DeserializeObject<JsonUserBanlance>(html);
            return jsResult.data.money;
        }
        #endregion

        #region 获取服务器的时间
        /// <summary>
        /// 获取服务器的时间
        /// </summary>
        /// <returns></returns>
        public long getServerTimeMillisecond()
        {

            Ku.Common.HttpHelper http = new Common.HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.URL = "http://" + Domain + "/tools/_ajax/getServerTimeMillisecond";
            httpItem.Method = "POST";
            httpItem.Postdata = "{\"requirement\":[\"OG1K3\"]}";
            httpItem.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            httpItem.Host = Domain;
            httpItem.Referer = "http://" + Domain + "/lottery/K3/OG1K3";
            httpItem.ContentType = "application/json";
            httpItem.Cookie = Cookie;
            httpItem.ReadWriteTimeout = 10000;
            HttpResult httpRes = http.GetHtml(httpItem);
            if (httpRes.StatusCode == System.Net.HttpStatusCode.OK)
            {
                requestFailCount = 0;
                string html = httpRes.Html;
                JsonServerTimeMillisecond res = Util.DeserializeObject<JsonServerTimeMillisecond>(html);
                return res.data.serverTime;
            }
            else
            {
                requestFailCount++;
                LogsRecord.write("接口调用异常", "获取服务时间，次数=" + requestFailCount + ",异常=" + httpRes.Html);
                if (requestFailCount >= requestMaxFailCount)
                    throw new LogException("请求异常，" + httpRes.Html);
                return getServerTimeMillisecond();

            }

        }
        #endregion
    }


}
