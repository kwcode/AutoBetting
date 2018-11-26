using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ku.Forms
{
    public class BaseTask : MessageProc
    {

        public virtual void DoThreadTask()
        {
            msg(this.GetType().Name + "没有实现基类方法");
        }

        Thread taskThread = null;
        public void Start(MessageDisplay md)
        {
            
            if (taskThread == null)
            {
                this.noticeEvent += md.msg;
                msg(this.GetType().Name + "启动...");
                taskThread = new Thread(DoThreadTask);
                taskThread.Start();
            }
            else
            {
                msg(this.GetType().Name + "已启动无需重复启动");
            }
        }

        public void Stop()
        {
            msg(this.GetType().Name + "关闭服务...");
            if (taskThread != null)
            {
                taskThread.Abort();
                taskThread = null;
            }
        }
    }
}
