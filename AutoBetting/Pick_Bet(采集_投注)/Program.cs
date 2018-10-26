using Ku.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ku.Forms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{ 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region 方式1:利用Mutex互斥量实现同时只有一个进程实例在运行

            //控制当前程序已经打开(即启动)
            //方式1:利用Mutex互斥量实现同时只有一个进程实例在运行
            //互斥锁（Mutex）
            //互斥锁是一个互斥的同步对象，意味着同一时间有且仅有一个线程可以获取它。 
            //互斥锁可适用于一个共享资源每次只能被一个线程访问的情况
            bool flag = false;
            System.Threading.Mutex hMutex = new System.Threading.Mutex(true, Application.ProductName, out flag);
            bool b = hMutex.WaitOne(0, false);
            /*上面的参数说明：
             第一个参数【initiallyOwned】:true：指示调用线程是否应具有互斥体的初始所有权 （老实说没理解透）
             第二个参数【name】:程序唯一name,（当前操作系统中）判定重复运行的标志 
             第三个参数【createdNew】:返回值,如果检测到已经启动则返回(false)。 
             */
            if (flag)
            {
                //没有启动相同的程序
                //Application.Run(new MainForm());
                Application.Run(new MainForm());
                
            }
            else
            {
                MessageBox.Show("当前程序已在运行，请勿重复运行。");
                Environment.Exit(1);//退出程序  
            }

            #endregion


            //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //}
            //catch (Exception ex)
            //{
            //    LogsRecord.write("应用程序线程致命错误", ex.ToString());
            //}
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogsRecord.write("UnknownError(当某个异常未被捕获时出现)", e.ExceptionObject.ToString());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogsRecord.write("UnknownError(在发生未捕获线程异常时发生)", e.Exception.ToString());
        }
    }
}
