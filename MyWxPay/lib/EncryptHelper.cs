using System;

namespace MyWxPay
{
    /// <summary>
    /// 加密算法单元
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// 简单异或加密
        /// </summary>
        /// <param name="p">要加密的字符串</param>
        /// <returns></returns>
        public static string XorEncryptStr(string p)
        {
            return Convert.ToString(Convert.ToInt32(p) ^ 20151026);
        }
    }
}