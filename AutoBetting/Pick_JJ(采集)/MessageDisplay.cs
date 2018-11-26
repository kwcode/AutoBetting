using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ku.UIForm
{
    public class MessageDisplay
    {
        private TextBox content;
        private List<string> stringContent;
        private DateTime lastDisplayTime = DateTime.Now;
        private AutoResetEvent disARE = new AutoResetEvent(false);
        private Thread delayTr;

        /// <summary>
        /// 消息显示类
        /// </summary>
        /// <param name="msgControl"></param>
        public MessageDisplay(TextBox msgControl)
        {
            content = msgControl;
            msgControl.ReadOnly = true;
            msgControl.ScrollBars = ScrollBars.Vertical;

            allowDisplaySameContent = true;
            maxLine = 100;
            stringContent = new List<string>(100);
            isClosing = false;
            hvNotProcMsg = false;
            lastMsg = "";
            pause = false;
            minMsgInterval = 400;
            defaultType = msgControl.FindForm().GetType().Name;

            if (delayTr == null)
            {
                delayTr = new Thread(new ThreadStart(DelayDisThread));
                delayTr.Start();
            }

            content.FindForm().FormClosing += new FormClosingEventHandler(MessageDisplay_FormClosing);

            menu = new ContextMenu();

            menu.MenuItems.Add(new MenuItem("&Clear Filter", new EventHandler(_clearFilter)));
            menu.MenuItems.Add(new MenuItem("&Filter string setting", new EventHandler(_setFilter)));
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(new MenuItem("&Stop", new EventHandler(_stop)));
            menu.MenuItems.Add(new MenuItem("&Display", new EventHandler(_display)));
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(new MenuItem("Cl&ear", new EventHandler(Clear)));
            menu.MenuItems.Add(new MenuItem("&Copy", new EventHandler(CopySelect)));
            menu.MenuItems.Add(new MenuItem("&Copy all", new EventHandler(CopyMSG)));
            content.ContextMenu = menu;
        }

        ContextMenu menu;

        TextBox tbFilter;
        Label tbFilterDis;

        /// <summary>
        /// 清除消息过滤
        /// </summary>
        private void _clearFilter(object sender, EventArgs e)
        {
            if (tbFilter != null && tbFilterDis != null)
            {
                FilterString = string.Empty;
                tbFilter.Text = "";
                tbFilterDis.Visible = false;
            }

        }

        /// <summary>
        /// 设置消息过滤字符串
        /// </summary>
        private void _setFilter(object sender, EventArgs e)
        {
            if (tbFilter == null)
            {
                int x = (content.Width - 180) / 2;
                if (x < 0) x = 0;
                tbFilter = new TextBox();
                tbFilter.Width = 180;
                tbFilter.BorderStyle = BorderStyle.FixedSingle;
                tbFilter.KeyUp += new KeyEventHandler(tbFilter_KeyUp);
                content.Controls.Add(tbFilter);
                tbFilter.Location = new Point(x, 20);
            }

            tbFilter.Visible = true;
            tbFilter.Focus();
        }

        void tbFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FilterString = tbFilter.Text;
                tbFilter.Visible = false;

                if (tbFilterDis == null)
                {
                    tbFilterDis = new Label();
                    tbFilterDis.BorderStyle = BorderStyle.FixedSingle;
                    tbFilterDis.KeyUp += new KeyEventHandler(tbFilter_KeyUp);
                    tbFilterDis.BackColor = Color.FromArgb(200, 200, 200);
                    tbFilterDis.TextAlign = ContentAlignment.MiddleRight;
                    Button _b = new Button();
                    _b.Margin = new Padding(0);
                    _b.Padding = new Padding(0);
                    _b.FlatStyle = FlatStyle.Popup;
                    _b.Text = "x";
                    _b.Width = 17;
                    _b.Height = 19;
                    _b.Cursor = Cursors.Hand;
                    _b.ForeColor = Color.White;
                    _b.BackColor = Color.Black;
                    _b.Click += _clearFilter;
                    tbFilterDis.Controls.Add(_b);
                }

                if (!string.IsNullOrEmpty(tbFilter.Text))
                {
                    tbFilterDis.Visible = true;
                    tbFilterDis.Text = tbFilter.Text;
                    tbFilterDis.Width = tbFilter.Text.Length * 6 + 25;
                    int x = content.Width - tbFilterDis.Width - 30;
                    if (x < 0) x = 0;
                    tbFilterDis.Location = new Point(x, 5);
                    content.Controls.Add(tbFilterDis);
                }
                else
                    tbFilterDis.Visible = false;
                return;
            }

            if (e.KeyCode == Keys.Escape)
                tbFilter.Visible = false;
        }

        /// <summary>
        /// 暂停显示
        /// </summary>
        private void _stop(object sender, EventArgs e)
        {
            this.pause = true;
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        private void _display(object sender, EventArgs e)
        {
            this.pause = false;
        }

        /// <summary>
        /// 清除消息内容
        /// </summary>
        private void Clear(object sender, EventArgs e)
        {
            lock (stringContent)
                stringContent.Clear();

            content.Text = "";
        }

        /// <summary>
        /// 清除消息内容
        /// </summary>
        public void Clear()
        {
            Clear(null, null);
        }

        /// <summary>
        /// 复制选择内容内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopySelect(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(content.SelectedText))
                return;
            try
            {
                Clipboard.SetText(content.SelectedText);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 复制消息内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMSG(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(content.Text))
                return;
            try
            {
                Clipboard.SetText(content.Text);
            }
            catch
            {
            }
        }

        void MessageDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
        }

        private object disLock = new object();

        private void DelayDisThread()
        {
            while (!isClosing)
            {
                Thread.Sleep(400);
                lock (disLock)
                    if (hvNotProcMsg)
                    {
                        disMSG(false);
                        hvNotProcMsg = false;
                    }
            }
        }

        /// <summary>
        /// 显示消息内容
        /// </summary>
        /// <param name="mg">消息内容</param>
        public void msg(string mg)
        {
            msg(defaultType, mg);
        }

        public void msg(string mg, params object[] vals)
        {
            msg(defaultType, string.Format(mg, vals));
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="type">消息类别</param>
        /// <param name="mg">消息内容</param>
        public void msg(string type, string mg)
        {
            if (content == null)
                return;

            if (!string.IsNullOrEmpty(FilterString))
            {
                if (mg.IndexOf(FilterString) == -1)
                    return;
            }

            if (!allowDisplaySameContent)
            {
                if (lastMsg == type + mg)
                    return;
                lastMsg = type + mg;
            }

            string _con = "[" + DateTime.Now.ToString("HH:mm:ss") + "](" + type + ") " + mg;

            lock (stringContent)
            {
                stringContent.Add(_con);

                int MoreCount = stringContent.Count - maxLine;

                if (MoreCount > 0)
                {
                    for (int i = 0; i < MoreCount; i++)
                        stringContent.RemoveAt(0);
                }

                if (pause)
                    return;
            }

            disMSG(true);
        }

        /// <summary>
        /// 开始显示的处理
        /// </summary>
        private void disMSG(bool nodelay)
        {
            if (!nodelay || DateTime.Now.Subtract(lastDisplayTime).TotalMilliseconds > minMsgInterval)
            {
                lastDisplayTime = DateTime.Now;
                string con = "";
                lock (stringContent)
                {
                    con = string.Join("\r\n", stringContent);
                }
                disMSG(con);
            }
            else
            {
                lock (disLock)
                    hvNotProcMsg = true;
                //System.Diagnostics.Debug.WriteLine("hv line not display.");
            }
        }

        /// <summary>
        /// 获取或者设置消息过滤字符串
        /// </summary>
        public string FilterString { get; set; }

        private delegate void msgEventArgs(string mg);

        /// <summary>
        /// 将消息输出到控件上
        /// </summary>
        /// <param name="mg"></param>
        private void disMSG(string mg)
        {
            if (content.InvokeRequired)
            {
                content.BeginInvoke(new msgEventArgs(disMSG), mg);
                return;
            }

            content.Text = mg;
            content.SelectionStart = mg.Length;
            content.ScrollToCaret();
        }

        #region 属性
        /// <summary>
        /// 默认消息标题
        /// </summary>
        public string defaultType { get; set; }

        /// <summary>
        /// 是否暂停显示日志
        /// </summary>
        public bool pause { get; set; }

        /// <summary>
        /// 2条消息之间的最小间隔（防止程序卡死)
        /// </summary>
        public ushort minMsgInterval { get; set; }

        /// <summary>
        /// 最大显示行数，默认为100
        /// </summary>
        public int maxLine { get; set; }

        /// <summary>
        /// 是否可以显示多行相同内容的消息
        /// </summary>
        public bool allowDisplaySameContent { get; set; }

        /// <summary>
        /// 记录最后一条消息
        /// </summary>
        private string lastMsg { get; set; }

        /// <summary>
        /// 是否有没有显示的消息
        /// </summary>
        private bool hvNotProcMsg { get; set; }

        /// <summary>
        /// 窗口是否正在关闭
        /// </summary>
        private bool isClosing { get; set; }
        #endregion
    }
}
