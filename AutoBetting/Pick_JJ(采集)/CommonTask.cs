using Ku.Common;
using Ku.DB;
using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ku.Forms
{
    public class CommonTask : BaseTask
    {
        private string connString = string.Empty;
        JJKit jjKit = new JJKit();
        public override void DoThreadTask()
        {
            connString = Ku.Common.ConfigHelper.GetConnectionStringValue("ConnString1");

            var b = Login();
            if (b)
            {
                //double moey = jjKit.GetUserBanlance();
                msg("登录成功。。。");
                msg("开始抓取。。。");
                while (true)
                {
                    try
                    {
                        Pick();
                        Thread.Sleep(30000);
                    }
                    catch (Exception ex)
                    {
                        msg("已经停止###" + ex.ToString());
                        Thread.Sleep(10000);
                    }
                }
            }
            else
            {
                msg("登录失败");
            }

        }

        public bool Login()
        {
            string jj_loginname = Ku.Common.ConfigHelper.GetAppSettingValue("jj_loginname");
            string jj_pwd = Ku.Common.ConfigHelper.GetAppSettingValue("jj_pwd");
            string jj_phone = Ku.Common.ConfigHelper.GetAppSettingValue("jj_phone");
            bool b = jjKit.Login(jj_loginname, jj_pwd, jj_phone);
            return b;
        }

        public void Pick()
        {
            string html = jjKit.Pick_一分快三();
            try
            {

                JsonResult jsonR = Util.DeserializeObject<JsonResult>(html);
                string code = jsonR.code;
                if (code == "success")
                {
                    string D_Date = "";
                    Data data = jsonR.data;
                    Backdata backData = data.backData;
                    Lotteryopen[] lotteryopen = backData.lotteryOpen;
                    D_Date = backData.time;
                    foreach (Lotteryopen item in lotteryopen)
                    {
                        string openTime = item.openTime;
                        long issueNo = item.issueNo;
                        string daxiao = item.daxiao == "da" ? "大" : "小";
                        string danshuang = item.danshuang == "dan" ? "单" : "双";
                        int Count = item.count;
                        List<string> vList = Util.Split(",", item.lotteryOpen);
                        string Value_1 = vList[0];
                        string Value_2 = vList[1];
                        string Value_3 = vList[2];

                        #region 存档
                        object obj = DBHelper.GetSingle(connString, "JJ_一分快三", "count(0)", "  issueNo ={0}", issueNo);
                        int isExist = Util.ConvertToInt32(obj);
                        if (isExist == 0)
                        {
                            SqlParameter[] pramsAdd =
                            { 
                                new SqlParameter(){ ParameterName="@D_Date",Value=D_Date},
                                new SqlParameter(){ ParameterName="@openTime",Value=openTime},
                                new SqlParameter(){ ParameterName="@issueNo",Value=issueNo},
                                new SqlParameter(){ ParameterName="@daxiao",Value=daxiao},
                                new SqlParameter(){ ParameterName="@danshuang",Value=danshuang},
                                new SqlParameter(){ ParameterName="@Count",Value=Count},
                                new SqlParameter(){ ParameterName="@Value_1",Value=Value_1},
                                new SqlParameter(){ ParameterName="@Value_2",Value=Value_2},
                                new SqlParameter(){ ParameterName="@Value_3",Value=Value_3},
                            };
                            int res = DBHelper.Add(connString, "JJ_一分快三", pramsAdd);
                            msg("期数=" + issueNo + ",值=" + item.lotteryOpen);
                        }
                        #endregion

                    }
                }
                else
                {
                    if (jsonR.code == "nologin")
                    {
                        msg("重新登陆");
                        Login();
                    }
                    else { msg(jsonR.msg.ToString()); }

                }
            }
            catch (Exception ex)
            {
                Ku.Common.LogsRecord.write("Error", html);
                throw ex;
            }

        }

    }
}
