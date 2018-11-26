using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms
{
    public interface ITask  
    {
        void Start();
        void Stop();
        event  Ku.UIForm.MessageProc.myNoticeEvent noticeEvent;
    }
}
