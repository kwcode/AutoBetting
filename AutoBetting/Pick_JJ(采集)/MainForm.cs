
using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ku.Forms
{
    public partial class MainForm : Form
    {
        private MessageDisplay md = null;
        public MainForm()
        {

            this.FormClosing += Form1_FormClosing;
            InitializeComponent();
            md = new MessageDisplay(txtOutputMsg);
            InitTaskList();

            foreach (BaseTask item in taskList)
            {
                item.Start(md);
            }
        }

        #region 任务列表
        List<BaseTask> taskList = new List<BaseTask>();
        private void InitTaskList()
        {
            taskList.Add(new CommonTask());
            taskList.Add(new GrouTask());
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
            foreach (BaseTask item in taskList)
            {
                item.Start(md);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (BaseTask item in taskList)
            {
                item.Stop();
            }
        }

         
    }
}
