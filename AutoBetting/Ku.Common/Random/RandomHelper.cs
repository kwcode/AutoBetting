using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    /// <summary>
    /// 随机数
    /// </summary>
    public class RandomHelper
    {

        private static int _seed = int.Parse(DateTime.Now.Ticks.ToString().Substring(10));
        private static int seed { get { return _seed; } set { _seed = value; } }
        /// <summary>
        /// 数字+大小写字母
        /// </summary>
        public static string AllRndStr = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="RangeString">字符串范围，如："0123456789ABCDEF"</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetRandString(string RangeString, int len)
        {
            char[] _a = RangeString.ToCharArray();

            string VNum = "";
            seed++;
            Random rand = new Random(seed);
            for (int i = 0; i < len; i++)
            {
                VNum += _a[rand.Next(_a.Length - 1)];
            }
            return VNum;
        }
        /// <summary>
        /// 获取随机字符串 0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetRandString(int len)
        {
            char[] _a = AllRndStr.ToCharArray();

            string VNum = "";
            seed++;
            Random rand = new Random(seed);
            for (int i = 0; i < len; i++)
            {
                VNum += _a[rand.Next(_a.Length - 1)];
            }
            return VNum;
        }

        /// <summary>
        /// 获取随机数字
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetRandNumber(int len)
        {
            return GetRandString("0123456789", len);
        }
        public static bool IsNull(object o)
        {
            try
            {
                string str = Util.ConvertToString(o);
                return string.IsNullOrEmpty(str);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
