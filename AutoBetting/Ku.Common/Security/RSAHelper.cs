﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ku.Common
{
    public class RSAHelper
    {
        /// <summary>
        /// 默认编码格式
        /// </summary>
        public static Encoding defaultEncoding = Encoding.UTF8;
        private const string encryptKey = "EncryKey";
        #region 加密字符串
        /// <summary> 
        /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string Encrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象   
            byte[] key = defaultEncoding.GetBytes(encryptKey);//定义字节数组，用来存储密钥    
            byte[] data = defaultEncoding.GetBytes(str);//定义字节数组，用来存储要加密的字符串  
            MemoryStream MStream = new MemoryStream();//实例化内存流对象      
            //使用内存流实例化加密流对象   
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);//向加密流中写入数据      
            CStream.FlushFinalBlock();//释放加密流      
            return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串  
        }
        #endregion

        #region 解密字符串
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象    
            byte[] key = defaultEncoding.GetBytes(encryptKey); //定义字节数组，用来存储密钥    
            byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串  
            MemoryStream MStream = new MemoryStream();//实例化内存流对象      
            //使用内存流实例化解密流对象       
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);//向解密流中写入数据     
            CStream.FlushFinalBlock();//释放解密流      
            return defaultEncoding.GetString(MStream.ToArray());//返回解密后的字符串  
        }
        #endregion
    }
}
