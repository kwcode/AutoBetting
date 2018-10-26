using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Ku.Common
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogsRecord
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logName">日志名</param>
        /// <param name="content">内容</param>
        /// <param name="FileNameByDay">是否每天产生一个目录</param>
        public static void write(string logName, string content, bool FileNameByDay)
        {
            string fileName = FileNameByDay ? DateTime.Now.ToString("dd-HH") + " " + logName + ".txt" : logName + ".txt";

            string logPath = AppDomain.CurrentDomain.BaseDirectory + "log";
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            logPath += "\\" + DateTime.Now.ToString("yyyyMM");
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            writeLog(logPath, fileName, content);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logName">日志名</param>
        /// <param name="content">内容</param>
        public static void write(string logName, string content)
        {
            write(logName, content, true);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logName">日志名</param>
        /// <param name="content">内容</param>
        /// <param name="LogsPath">指定特定日志目录</param>
        public static void write(string logName, string content, string LogsPath)
        {
            string fileName = DateTime.Now.ToString("dd-HH") + " " + logName + ".txt";

            if (!Directory.Exists(LogsPath))
                throw new Exception("Path [" + LogsPath + "] not exist!");

            LogsPath = LogsPath.TrimEnd('\\');

            LogsPath += "\\" + DateTime.Now.ToString("yyyyMM");
            if (!Directory.Exists(LogsPath))
                Directory.CreateDirectory(LogsPath);

            writeLog(LogsPath, fileName, content);
        }

        private static object writeLogObj = new object();

        private static void writeLog(string path, string fileName, string content)
        {
            string fullPath = path + "\\" + fileName;
            string info = "[" + DateTime.Now.ToString("MM-dd HH:mm:ss") + "] " + content + "\r\n";

            try
            {
                lock (writeLogObj)
                {
                    if (fullPath != nowStreamName)
                    {
                        if (nowStreamName != null)
                            closeLogFile();

                        mySw = new StreamWriter(fullPath, true, ASCIIEncoding.UTF8);
                        nowStreamName = fullPath;
                    }

                    mySw.Write(info);
                    mySw.Flush();
                }
            }
            catch
            {
                writeLostLog(path, content);
            }
            closeLogFile();
        }

        private static StreamWriter loseStreamWriter;
        private static string loseLogName = null;
        private static void writeLostLog(string path, string content)
        {
            try
            {
                string fileName = path + "\\" + DateTime.Now.ToString("dd-HH") + "_lostLog.txt";
                if (fileName != loseLogName)
                {
                    closeLoseLogFile();
                    loseStreamWriter = new StreamWriter(fileName, true, ASCIIEncoding.Default);
                }
                loseLogName = fileName;

                string info = "[" + DateTime.Now.ToString("MM-dd HH:mm:ss") + "] " + content + "\r\n";
                loseStreamWriter.Write(info);
                loseStreamWriter.Flush();
            }
            catch { }
        }

        /// <summary>
        /// 关闭丢失日志记录文件流
        /// </summary>
        public static void closeLoseLogFile()
        {
            if (loseStreamWriter != null)
            {
                loseLogName = null;
                loseStreamWriter.Close();
                loseStreamWriter.Dispose();
                loseStreamWriter = null;
            }
        }

        private static StreamWriter mySw;

        private static string nowStreamName { get; set; }

        /// <summary>
        /// 关闭最后打开的日志文件流
        /// </summary>
        public static void closeLogFile()
        {
            try
            { 
                if (mySw != null && nowStreamName != null)
                {
                    nowStreamName = null;
                    mySw.Close();
                    mySw.Dispose();
                    mySw = null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private StreamWriter writer;
        private Thread tr;
        private bool isRunning = true;

        /// <summary>
        /// 日志记录类
        /// </summary>
        /// <param name="logName">日志名</param>
        public LogsRecord(string logName)
        {
            LogName = logName;

            CurrLogPath = AppDomain.CurrentDomain.BaseDirectory + "log";
            if (!Directory.Exists(CurrLogPath))
                Directory.CreateDirectory(CurrLogPath);

            checkWriter();

            if (tr == null)
            {
                tr = new Thread(new ThreadStart(flushThread));
                tr.Start();
            }
        }

        private void checkWriter()
        {
            string _path = CurrLogPath + "\\" + DateTime.Now.ToString("yyyyMM");

            if (_path != CurrLogPath1)
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                CurrLogPath1 = _path;
            }

            string _name = DateTime.Now.ToString("dd-HH") + " " + LogName + ".txt";

            if (CurrFileName != _name)
            {
                try
                {
                    if (writer != null)
                        writer.Dispose();

                    writer = new StreamWriter(CurrLogPath1 + "\\" + _name);
                }
                catch
                {
                    throw new Exception("This logname is opened by other process.");
                }

                CurrFileName = _name;
            }
        }

        public string LogName { get; set; }

        /// <summary>
        /// 正在写入的文件名
        /// </summary>
        private string CurrFileName { get; set; }

        /// <summary>
        /// 日志根目录，不变
        /// </summary>
        private string CurrLogPath { get; set; }

        /// <summary>
        /// 日志具体目录，每月一个文件夹
        /// </summary>
        private string CurrLogPath1 { get; set; }

        /// <summary>
        /// 日志先放在内存中
        /// </summary>
        private List<string> memLog = new List<string>();

        /// <summary>
        /// 写日志(非静态)
        /// </summary>
        /// <param name="content">内容</param>
        public void write(string content)
        {
            string info = "[" + DateTime.Now.ToString("MM-dd HH:mm:ss") + "] " + content;
            ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProc), info);
        }

        private void TaskProc(object state)
        {
            lock (memLog)
                memLog.Add((string)state);
        }

        private void flushThread()
        {
            while (isRunning)
            {
                checkWriter();

                string _con = null;
                lock (memLog)
                {
                    if (memLog.Count > 0)
                    {
                        _con = string.Join("\r\n", memLog);
                        memLog.Clear();
                    }
                }

                if (_con != null)
                {
                    writer.WriteLine(_con);
                    writer.Flush();
                }

                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// 关闭当前日志流
        /// </summary>
        public void close()
        {
            isRunning = false;
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
