using Ku.Common;
using System;
using System.IO;
using System.Reflection;
namespace Ku.UIForm
{
    /// <summary>
    /// 消息处理类
    /// </summary>
    public class MessageProc : MarshalByRefObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// 消息类别,默认为当前类名
        /// </summary>
        /// <returns></returns>
        protected virtual string msgType()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// 消息事件委托
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mg"></param>
        public delegate void myNoticeEvent(string type, string mg);

        /// <summary>
        /// 消息事件
        /// </summary>
        public event myNoticeEvent noticeEvent;

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="mg">消息内容</param>
        protected void msg(string mg)
        {
            if (noticeEvent != null)
            {
                myNoticeEvent notice = new myNoticeEvent(SubEvent);
                notice.BeginInvoke(msgType(), mg, null, null);
            }
        }

        protected void msg(string patten, params object[] vals)
        {
            if (noticeEvent != null)
            {
                myNoticeEvent notice = new myNoticeEvent(SubEvent);
                notice.BeginInvoke(msgType(), string.Format(patten, vals), null, null);
            }
        }

        private void SubEvent(string type, string mg)
        {
            if (noticeEvent != null)
            {
                noticeEvent(type, mg);
            }
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="type">消息类别</param>
        /// <param name="mg">消息内容</param>
        protected void msg(string type, string mg)
        {
            if (noticeEvent != null)
                noticeEvent(type, mg);
        }

        protected void Show(string type, string content, DebugType debugType = DebugType.LogOnly)
        {
            switch (debugType)
            {
                case DebugType.MsgOnly:
                    msg(content);
                    break;
                case DebugType.LogOnly:
                    LogsRecord.write(type, content);
                    break;
                case DebugType.Both:
                    msg(content);
                    LogsRecord.write(type, content);
                    break;

                default:
                    break;
            }
        }

        protected void Show(string content, DebugType debugType = DebugType.LogOnly)
        {
            Show("Error", content, debugType);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum DebugType
    {
        /// <summary>
        /// 
        /// </summary>
        Both,
        /// <summary>
        /// 
        /// </summary>
        LogOnly,
        /// <summary>
        /// 
        /// </summary>
        MsgOnly,
        /// <summary>
        /// 
        /// </summary>
        None,

    }
}
