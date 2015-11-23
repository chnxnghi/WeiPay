using System;
using System.Collections.Generic;
using System.Web;

namespace MyWxPay
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}