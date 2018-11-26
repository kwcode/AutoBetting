
using Ku.API.Model;
using Ku.Common;
using Ku.Forms.DAL;
using Ku.Forms.Kit;
using Ku.Forms.Model;
using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ku.Forms
{
    public partial class MainForm : Form
    {
        SynchronizationContext SysContext = null;
        private MessageDisplay md = null;
        JJApiKit jjKit = new JJApiKit();
        public MainForm()
        {

            this.FormClosing += Form1_FormClosing;
            InitializeComponent();
            SysContext = SynchronizationContext.Current;
            md = new MessageDisplay(txtOutputMsg);

            DoTask();


        }

        private void msg(string message)
        {
            md.msg(message);
        }

        public bool Login()
        {
            string jj_loginname = Ku.Common.ConfigHelper.GetAppSettingValue("jj_loginname");
            string jj_pwd = Ku.Common.ConfigHelper.GetAppSettingValue("jj_pwd");
            string jj_phone = Ku.Common.ConfigHelper.GetAppSettingValue("jj_phone");
            bool b = jjKit.Login(jj_loginname, jj_pwd, jj_phone);
            return b;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Text = "正在关闭中...";
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            //foreach (BaseTask item in taskList)
            //{
            //    item.Start(md);
            //}
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }
        #region UI赋值
        public void SetCountdownText()
        {
            SysContext.Send(o =>
            {
                lbCountdown.Text = secondResult.ToString();
            }, null);

        }
        #endregion

        #region 线程任务

        private void DoTask()
        {
            try
            {
                bool b = Login();
                if (!b)
                {
                    md.msg("登录失败");
                    Login();
                }
                ProofSecond();
                new Thread(DoThreadDownTime).Start();
                new Thread(DoThreadPickTask).Start();

            }
            catch (Exception ex)
            {
                LogsRecord.write("Error-DoTask", ex.ToString());

                DoTask();
                Thread.Sleep(10000);
            }
        }



        #endregion

        #region 定时器
        private const int baseSecond = 58;
        int secondResult = 0;
        private void ProofSecond()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long serverTime = jjKit.getServerTimeMillisecond();
            string timeSan = serverTime.ToString();
            DateTime time = Ku.Common.TimeKit.ConvertStringToDateTime(timeSan);
            int s = time.Second;
            sw.Stop();
            int code_S = sw.Elapsed.Seconds;
            int ok_second = baseSecond - s - code_S;
            if (ok_second <= 0)
            {
                ok_second = 0;
            }
            secondResult = ok_second;
        }
        private void DoThreadDownTime()
        {
            while (true)
            {
                secondResult--;
                if (secondResult > 0)
                {
                    SetCountdownText();
                }
                else
                {
                    currentIssueNo = preIssueNo + 1;
                    ProofSecond();
                    SetCountdownText();
                }
                Thread.Sleep(1000);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void btnokTime_Click(object sender, EventArgs e)
        {
            ProofSecond();
            SetCountdownText();
        }
        #endregion

        #region 检测投注
        private void DoThreadPickTask(object obj)
        {
            while (true)
            {
                GoTask();
                Thread.Sleep(1000);
            }
        }
        private long preIssueNo = -1;//上期投注期号
        private long currentIssueNo = 0;//当前期号
        /// <summary>
        /// 走任务
        /// </summary>
        public void GoTask()
        {
            switch (secondResult)
            {
                case 45:
                    GoBetSingle();
                    break;
                case 35:
                    if (currentIssueNo != preIssueNo + 1)
                    {
                        GoBetSingle();
                    }
                    break;
                case 25:
                    if (currentIssueNo != preIssueNo + 1)
                    {
                        GoBetSingle();
                    }
                    break;
                case 15:
                case 5:
                default:
                    break;
            }

            //msg(secondResult.ToString());
        }

        //private List<OG1K3Entity> GetPikcList()
        //{
        //    List<OG1K3Entity> list = new List<OG1K3Entity>();

        //    //获取最新的抓取列表
        //    string html = jjKit.GetLotteryOpenResult();
        //    JsonResult jsonR = Util.DeserializeObject<JsonResult>(html);
        //    string code = jsonR.code;
        //    if (code == "success")
        //    {
        //        string D_Date = "";
        //        Data data = jsonR.data;
        //        Backdata backData = data.backData;
        //        Lotteryopen[] lotteryopen = backData.lotteryOpen;
        //        D_Date = backData.time;
        //        foreach (Lotteryopen item in lotteryopen)
        //        {
        //            OG1K3Entity entity = new OG1K3Entity();
        //            entity.openTime = item.openTime;
        //            entity.issueNo = item.issueNo;
        //            entity.daxiao = item.daxiao == "da" ? "大" : "小";
        //            entity.danshuang = item.danshuang == "dan" ? "单" : "双";
        //            entity.Count = item.count;
        //            entity.lotteryOpen = item.lotteryOpen;
        //            List<string> vList = Util.Split(",", item.lotteryOpen);
        //            string Value_1 = vList[0];
        //            string Value_2 = vList[1];
        //            string Value_3 = vList[2];



        //            list.Add(entity);
        //        }
        //    }
        //    else
        //    {
        //        if (jsonR.code == "nologin")
        //        {
        //            msg("重新登陆");
        //            Login();
        //        }
        //        else { msg(jsonR.msg.ToString()); }
        //    }

        //    return list;
        //}


        #endregion

        #region 去投注
        TouzhuKit touzhuKit = new TouzhuKit(TouzhuTypeEnum.大小, 2, 1);
        /// <summary>
        /// 去投注
        /// </summary>
        private void GoBetSingle()
        {
            //获取上一期的的结果
            List<JJOG1K3Result> list = jjKit.GetLotteryOpenList();
            foreach (JJOG1K3Result item in list)
            {
                #region 存档
                int isExist = OG1K3DAL.Instance.GetSingle(item.issueNo);
                if (isExist == 0)
                {
                    DBParamEntity[] pramsAdd =
                            { 
                                new DBParamEntity(){ FieldName="D_Date",Value=item.D_Date},
                                new DBParamEntity(){ FieldName="openTime",Value=item.openTime},
                                new DBParamEntity(){ FieldName="issueNo",Value=item.issueNo},
                                new DBParamEntity(){ FieldName="daxiao",Value=item.daxiao},
                                new DBParamEntity(){ FieldName="danshuang",Value=item.danshuang},
                                new DBParamEntity(){ FieldName="Count",Value=item.Count},
                                new DBParamEntity(){ FieldName="Value_1",Value=item.Value_1},
                                new DBParamEntity(){ FieldName="Value_2",Value=item.Value_2},
                                new DBParamEntity(){ FieldName="Value_3",Value=item.Value_3},
                            };
                    OG1K3DAL.Instance.Add(pramsAdd);
                    msg("期数=" + item.issueNo + ",值=" + item.lotteryOpen);
                }
                #endregion
            }
            preIssueNo = Util.ConvertToInt64(list[0].issueNo);
            currentIssueNo = preIssueNo + 1;
            SysContext.Send(o => { lbissueNo.Text = "距" + currentIssueNo.ToString() + "期投注截止还有："; }, null);

            if (currentIssueNo > preIssueNo)
            {
                touzhuKit.Start(jjKit, list, currentIssueNo);
                //获取账户余额
                double UserBanlance = jjKit.GetUserBanlance();
            }


        }


        #endregion



    }
}
