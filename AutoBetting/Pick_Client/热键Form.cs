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
    public partial class 热键Form : Form
    {
        public 热键Form()
        {
            InitializeComponent();
            //this.KeyPreview = false;
            this.Load += 热键Form_Load;
            this.KeyDown += 热键Form_KeyDown;
        }

        private void msg(object message)
        {
            this.Invoke(new Action(() =>
            {
                txtMsg.Text += message.ToString() + "\r\n";

                txtMsg.SelectionStart = txtMsg.Text.Length;
                txtMsg.ScrollToCaret();
            }));
        }
        #region 当前窗体热键

        void 热键Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Shift && e.Control && e.KeyCode == Keys.S)
            {
                MessageBox.Show("【当前窗体】我按了Control +Shift +Alt +S");
            }
        }

        #endregion

        private const int WM_HOTKEY = 786;// 0x312; //窗口消息-热键  
        private const int WM_CREATE = 1;//0x1; //窗口消息-创建  
        private const int WM_DESTROY = 2;// 0x2; //窗口消息-销毁  
        private const int hotKey_id = 13682;// 0x3572; //热键ID  
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m); 
            switch (m.Msg)
            {
                case WM_HOTKEY: //窗口消息-热键ID  
                    switch (m.WParam.ToInt32())
                    {
                        case hotKey_id: //热键ID  
                            msg(m.HWnd + "--" + m.LParam + "--" + m.Msg + "--" + m.Result);
                            MessageBox.Show("【全局】我按了Control +Shift +Alt +S");
                            break;
                        default:
                            
                            break;
                    }
                    break;
                case WM_CREATE: //窗口消息-创建  
                    AppHotKey.RegKey(Handle, hotKey_id, AppHotKey.KeyModifiers.Ctrl | AppHotKey.KeyModifiers.Shift | AppHotKey.KeyModifiers.Alt, Keys.S);
                    break;
                case WM_DESTROY: //窗口消息-销毁  
                    AppHotKey.UnRegKey(Handle, hotKey_id); //销毁热键  
                    break;
                default:
                    break;
            }
        }

        #region 全局热键设置 定义API函数 》 注册热键 》 卸载热键
        void 热键Form_Load(object sender, EventArgs e)
        {
            //Ku.UIForm.AppHotKey.RegKey(IntPtr.Add)
        }
        #endregion
    }
}
