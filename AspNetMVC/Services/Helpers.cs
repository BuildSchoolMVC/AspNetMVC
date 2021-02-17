﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AspNetMVC.Services
{
    public class Helpers
    {
        /// <summary>
        /// 解析cookie裡的值
        /// </summary>
        /// <param name="cookieValue"></param>
        /// <returns></returns>
        public static string DecodeCookie(string cookieValue)
        {
            return System.Text.Encoding.UTF8.GetString(System.Web.Security.MachineKey.Unprotect(Convert.FromBase64String(cookieValue), "protectedCookie"));
        }

        /// <summary>
        /// 用MD5加密
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string ToMD5(string strings)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(strings);//將要加密的字串轉換為位元組陣列
            byte[] encryptdata = md5.ComputeHash(bytes);//將字串加密後也轉換為字元陣列
            return Convert.ToBase64String(encryptdata);//將加密後的位元組陣列轉換為加密字串
        }
    }
}