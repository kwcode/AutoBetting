
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
    public partial class 闪烁提示Form : Form
    {
        int i = 0;
        private Icon ico1 = Resource.notifyA;
        private Icon transparent = Resource.transparent;//两个图标 切换显示 以达到消息闪动的效果   
        public 闪烁提示Form()
        {
            InitializeComponent();

        }

        private void 闪烁提示Form_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //如果i=0则让任务栏图标变为透明的图标并且退出    
            System.Threading.Thread.Sleep(500);
            if (i < 1)
            {
                this.notifyIcon1.Icon = transparent;
                i++;
                this.notifyIcon1.ShowBalloonTip(1000, "标题", "内容", ToolTipIcon.Warning);//警告图标
                return;
            }
            //如果i!=0,就让任务栏图标变为ico1,并将i置为0;    
            else
            {
                this.notifyIcon1.Icon = ico1;

            }

            i = 0;

        }
    }
}
