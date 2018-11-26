
using Ku.Common;
using Ku.Domain;
using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ku.Forms
{
    public partial class MainForm : Form
    {
        SynchronizationContext SysContext = null;
        private MessageDisplay md = null;
        public MainForm()
        {
            this.FormClosing += Form1_FormClosing;
            InitializeComponent();
            SysContext = SynchronizationContext.Current;
            md = new MessageDisplay(txtOutputMsg);
            this.notifyIcon1.Icon = ico1;
            new Thread(DoThreadNotify).Start();
            new Thread(DoThreadTask).Start();

        }
        private void msg(string message)
        {
            md.msg(message);
        }
        //记录上一次的结果 如果调用api一致则不用提示
        private long oldIssueNo { get; set; }
        private void DoThreadTask()
        {
            while (true)
            {
                try
                {
                    var list = GetBetList();
                    long issueNo = list[0].issueNo;
                    if (issueNo != oldIssueNo)
                    {

                        oldIssueNo = issueNo;
                        //监听重复次数
                        //从api获取最新50条记录信息 
                        var repeatDxCount = GetDaxiaoRepeatTimes(list);
                        var repeatDSount = GetDanshuangRepeatTimes(list);
                        string message = "检测重复次数，大小=" + repeatDxCount + ",单双=" + repeatDSount;
                        msg(message);
                        if (repeatDxCount > 6 || repeatDSount > 6)
                        {
                            //string message = "检测重复次数，大小=" + repeatDxCount + ",单双=" + repeatDSount;
                            //msg(message);
                            //连续出现6次 提示


                            StartNotify(message);
                            int num = repeatDxCount;
                            if (repeatDSount > repeatDxCount)
                            {
                                num = repeatDSount;
                            }
                            TipsSound(num);
                        }

                    }
                    Thread.Sleep(1000 * 10);
                }
                catch (Exception ex)
                {
                    LogsRecord.write("异常", ex.ToString());
                    Thread.Sleep(1000 * 10);
                }
            }
        }
        private List<JJ_一分快三Entity> GetBetList()
        {
            HttpHelper http = new HttpHelper();
            string html = http.GetHtml(Ku.Common.ConfigHelper.GetAppSettingValue("apiurl"));
            var list = Ku.Common.Util.DeserializeObject<List<Ku.Domain.JJ_一分快三Entity>>(html);
            return list;
        }



        #region 根据集合 检测重复次数
        private int GetDaxiaoRepeatTimes(List<Ku.Domain.JJ_一分快三Entity> list)
        {
            string first = list[0].daxiao;
            int repeatCount = 0;
            foreach (JJ_一分快三Entity item in list)
            {
                if (item.daxiao == first)
                {
                    repeatCount++;
                }
                else { break; }
            }
            return repeatCount;
        }
        private int GetDanshuangRepeatTimes(List<Ku.Domain.JJ_一分快三Entity> list)
        {
            string first = list[0].danshuang;
            int repeatCount = 0;
            foreach (JJ_一分快三Entity item in list)
            {
                if (item.danshuang == first)
                {
                    repeatCount++;
                }
                else { break; }
            }
            return repeatCount;
        }
        #endregion
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Text = "正在关闭中...";
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

        }
        #region 提示

        //声音提示
        private void TipsSound(int num)
        {
            //根据不同的数字调用不同的音频
            SoundPlayer p = new SoundPlayer();
            p.SoundLocation = Application.StartupPath + "//Sound/QQ//msg.wav";
            p.Load();
            p.Play();
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("重复" + num + "次!");
            synth.Dispose();

        }
        private Icon ico1 = Resource.notifyA;
        private Icon transparent = Resource.transparent;//两个图标 切换显示 以达到消息闪动的效果  
        private bool isNotify = false;

        private void StartNotify(string message)
        {
            isNotify = true;
            this.notifyIcon1.ShowBalloonTip(1000, "", message, ToolTipIcon.Info);//警告图标
        }

        private void DoThreadNotify()
        {
            int i = 0;
            while (true)
            {

                if (isNotify)
                {
                    System.Threading.Thread.Sleep(500);
                    if (i == 0)
                    {
                        this.notifyIcon1.Icon = transparent;
                        i++;
                    }
                    else { this.notifyIcon1.Icon = ico1; i = 0; }
                }
                else
                {
                    this.notifyIcon1.Icon = ico1;
                }
            }
        }
        //图标提示 
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isNotify = false;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 关闭所有的线程
            this.Dispose();
            this.Close();
            Application.Exit();
        }





    }
}
