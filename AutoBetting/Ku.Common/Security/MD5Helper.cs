using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ku.Common
{
    public class MD5Helper
    {
        #region 进行MD5加密

        /// <summary>
        /// 进行MD5加密
        /// </summary>
        /// <param name="inputString">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }

        #endregion


        #region 通用MD5加密的令牌
        /// <summary>
        /// 通用MD5加密的令牌
        /// 加密公式：md5(md5(账号+用户ID+KEY)) 
        /// 双层加密
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetMD5Token(string loginName, int userId, string key)
        {
            string md5Str = "";
            try
            {
                md5Str = MD5Encrypt(loginName + userId + key);
                md5Str = MD5Encrypt(md5Str);
            }
            catch { }

            return md5Str;
        }
        #endregion
    }
}
