using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using WeiPay;
using WxPayAPI;

namespace WeiPayWeb
{
    public partial class WeiPay : System.Web.UI.Page
    {
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public string attach = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(), "page load");
            if (!IsPostBack)
            {
                string body = "恒指天成产品注册码";
                attach = this.Request.QueryString["attach"];
                string openid = Request.QueryString["openid"];
                string total_fee = Request.QueryString["total_fee"];
                //检测是否给当前页面传递了相关参数
                if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee) || string.IsNullOrEmpty(attach))
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                    Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
                    BtnSubmit.Visible = false;
                    return;
                }

                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.openid = openid;
                jsApiPay.total_fee = int.Parse(total_fee);

                //JSAPI支付预处理
                try
                {
                    string out_trade_no = String.Format("{0}-{1}-{2:yyyyMMddHHmmsss}", attach, EncryptHelper.XorEncryptStr(attach), DateTime.Now);
                    string ip = Page.Request.UserHostAddress;
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(out_trade_no, body, attach, ip);
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                    //在页面上显示订单信息
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                    //Response.Write(String.Format("<span style='color:#00CD00;font-size:20px'>{0}</span>", unifiedOrderResult.ToPrintStr()));

                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                    BtnSubmit.Visible = false;
                    Response.End();
                }
            }
            /*this.NotifyUrl = PayConfig.NotifyUrl;
            #region 支付操作============================
            #region 基本参数===========================
            TimeStamp = TenpayUtil.getTimestamp();
            NonceStr = TenpayUtil.getNoncestr();
            var packageReqHandler = new RequestHandler(Context);
            packageReqHandler.init();
            //设置package订单参数  具体参数列表请参考官方pdf文档，请勿随意设置
            packageReqHandler.setParameter("body", this.Body); //商品信息 127字符
            packageReqHandler.setParameter("appid", PayConfig.AppId);
            packageReqHandler.setParameter("mch_id", PayConfig.MchId);
            packageReqHandler.setParameter("nonce_str", NonceStr.ToLower());
            packageReqHandler.setParameter("notify_url", PayConfig.NotifyUrl);
            packageReqHandler.setParameter("openid", this.UserOpenId);
            packageReqHandler.setParameter("out_trade_no", this.Attach + "-" + EncryptHelper.XorEncryptStr(this.Attach) + "-" + DateTime.Now.ToString("yyyyMMddHHmmsss")); //商家订单号
            packageReqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress); //用户的公网ip，不是商户服务器IP
            packageReqHandler.setParameter("total_fee", this.TotalFee); //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.setParameter("trade_type", "JSAPI");
            if (!string.IsNullOrEmpty(this.Attach))
                packageReqHandler.setParameter("attach", this.Attach);//自定义参数 127字符 机器号
            #endregion
            #region sign===============================
            Sign = packageReqHandler.CreateMd5Sign("key", PayConfig.AppKey);
            LogUtil.WriteLog("WeiPay sign：" + Sign);
            #endregion

            #region 获取package包======================
            packageReqHandler.setParameter("sign", Sign);

            string data = packageReqHandler.parseXML();
            LogUtil.WriteLog("WeiPay 页面  package（XML）：" + data);

            string prepayXml = HttpUtil.Send(data, "https://api.mch.weixin.qq.com/pay/unifiedorder");
            LogUtil.WriteLog("WeiPay 页面  package（Back_XML）：" + prepayXml);

            //获取预支付ID
            var xdoc = new XmlDocument();
            xdoc.LoadXml(prepayXml);
            XmlNode xn = xdoc.SelectSingleNode("xml");
            XmlNodeList xnl = xn.ChildNodes;
            if (xnl.Count > 7)
            {
                PrepayId = xnl[7].InnerText;
                Package = string.Format("prepay_id={0}", PrepayId);
                LogUtil.WriteLog("WeiPay 页面  package：" + Package);
            }
            #endregion

            #region 设置支付参数 输出页面  该部分参数请勿随意修改 ==============
            var paySignReqHandler = new RequestHandler(Context);
            paySignReqHandler.setParameter("appId", PayConfig.AppId);
            paySignReqHandler.setParameter("timeStamp", TimeStamp);
            paySignReqHandler.setParameter("nonceStr", NonceStr);
            paySignReqHandler.setParameter("package", Package);
            paySignReqHandler.setParameter("signType", "MD5");
            PaySign = paySignReqHandler.CreateMd5Sign("key", PayConfig.AppKey);

            LogUtil.WriteLog("WeiPay 页面  paySign：" + PaySign);
            #endregion
            #endregion*/
        }


        /// <summary>
        /// 获取传递的支付参数，并绑定页面上
        /// </summary>
    }
}
