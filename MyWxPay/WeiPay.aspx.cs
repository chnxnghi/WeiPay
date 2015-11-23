using System;
using System.Web.UI;

namespace MyWxPay
{
    public partial class WeiPay: System.Web.UI.Page
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
                    Response.End();
                    Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
                    //BtnSubmit.Visible = false;
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
                    //string nonceStr = unifiedOrderResult.GetValue("nonce_str").ToString();
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                    //BtnSubmit.Visible = false;
                    Response.End();
                }
            }
        }
    }
}
