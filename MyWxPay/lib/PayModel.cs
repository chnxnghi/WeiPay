﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace WeiPay
{
    /**
    * 
    * 作用：微信支付相关支付信息实体类，支付信息通过该类封装，微信支付版本V3.3.7
    * 
    * */
    public class PayModel
    {
        /// <summary>
        /// 商户订单号（必填）
        /// </summary>
        private string ordersn;
        public string OrderSN
        {
            get
            {
                if (string.IsNullOrEmpty(ordersn))
                    return DateTime.Now.ToString("yyyyMMddHHmmsss");
                return ordersn;
            }
            set
            {
                ordersn = value;
            }
        }

        /// <summary>
        /// 订单支付金额单位为分（必填）
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 商品信息（必填，长度不能大于127字符）
        /// </summary>
        private string body;
        public string Body
        {
            get
            {
                if (string.IsNullOrEmpty(body))
                    return "恒智天成产品注册码";
                if (body.Length > 127)
                    return body.Substring(0, 120) + "...";
                return body;
            }
            set
            {
                body = value;
            }
        }


        /// <summary>
        /// 支付用户微信OpenId（必填）
        /// </summary>
        public string OpenId { get; set; }


        /// <summary>
        /// 用户自定义参数原样返回，不能有中文不然调用Notify页面会有错误。（长度不能大于127字符）
        /// </summary>
        /// 
        public string Attach { get; set; }
 

        /// <summary>
        /// 重写ToString函数，获取跳转到支付页面的url其中附带参数
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("WeiPay.aspx?showwxpaytitle=1");
            sb.AppendFormat("&Body={0}", Body);
            sb.AppendFormat("&total_fee={0}", TotalFee);
            sb.AppendFormat("&Attach={0}", Attach);

            return sb.ToString();
        }

    }
}
