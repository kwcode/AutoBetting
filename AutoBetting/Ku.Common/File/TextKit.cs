using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    /// <summary>
    /// 文本文件操作
    /// </summary>
    public class TextKit
    {
        public static string GetTxt(string path)
        {
            try
            {
                System.IO.FileStream fsr = System.IO.File.OpenRead(path);
                System.IO.StreamReader sr = new System.IO.StreamReader(fsr);
                string record = sr.ReadLine();
                sr.Close();
                sr.Dispose();
                fsr.Close();
                fsr.Dispose();
                return record;
            }
            catch
            {
                return "";
            }

        }

    }
}
