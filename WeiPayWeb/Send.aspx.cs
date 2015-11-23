using Newtonsoft.Json;
using System;
using WeiPay;
using WxPayAPI;

namespace WeiPayWeb
{
    public partial class Send : System.Web.UI.Page
    {
        /// <summary>
        /// 调用js获取收货地址时需要传入的参数
        /// 格式：json串
        /// 包含以下字段：
        ///     appid：公众号id
        ///     scope: 填写“jsapi_address”，获得编辑地址权限
        ///     signType:签名方式，目前仅支持SHA1
        ///     addrSign: 签名，由appid、url、timestamp、noncestr、accesstoken参与签名
        ///     timeStamp：时间戳
        ///     nonceStr: 随机字符串
        /// </summary>
        public static string wxEditAddrParam { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(), "page load");
            if (!IsPostBack)
            {
                JsApiPay jsApiPay = new JsApiPay(this);
                try
                {
                    //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                    jsApiPay.GetOpenidAndAccessToken();
                    ViewState["openid"] = jsApiPay.openid;
                    //GetUserOpenId();
                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                    BtnSave.Visible = false;
                }
            }
        }

        /// <summary>
        /// 获取当前用户的微信 OpenId，如果知道用户的OpenId请不要使用该函数
        /// </summary>
        private void GetUserOpenId()
        {
            string code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                string code_url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=lk#wechat_redirect", PayConfig.AppId, PayConfig.SendUrl + "?showwxpaytitle=1");
                LogUtil.WriteLog(" IsPostBack=False,code为null获取OpenId:" + code_url);
                Response.Redirect(code_url);// 使用Response.Redirect方式向自画面迁移时，此时IsPostBack＝false
            }
            else
            {
                LogUtil.WriteLog(" ============ 开始 获取微信用户相关信息 =====================");

                #region 获取支付用户 OpenID================
                string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", PayConfig.AppId, PayConfig.AppSecret, code);
                string returnStr = HttpUtil.Send("", url);
                LogUtil.WriteLog("Send 页面  returnStr 第一个：" + returnStr);

                var obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);

                url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", PayConfig.AppId, obj.refresh_token);
                returnStr = HttpUtil.Send("", url);
                obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);
                ViewState["openid"] = obj.openid;
                LogUtil.WriteLog("Send 页面  access_token：" + obj.access_token);
                LogUtil.WriteLog("Send 页面  openid=" + obj.openid);
                url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", obj.access_token, obj.openid);
                returnStr = HttpUtil.Send("", url);
                LogUtil.WriteLog("Send 页面  returnStr：" + returnStr);
                LogUtil.WriteLog(" ============ 结束 获取微信用户相关信息 =====================");
               
                #endregion
            }
        }


        /// <summary>
        /// 提交支付信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string total_fee = PayConfig.Tenpay;
            if (ViewState["openid"] != null)
            {
                if (!string.IsNullOrEmpty(txtMCode.Text) && (txtMCode.Text.Trim().Length == 8) && string.Equals(txtMCode.Text, txtSure.Text, StringComparison.CurrentCulture))
                {
                    string openid = ViewState["openid"].ToString();
                    string attach = txtMCode.Text.Trim();
                    string url = String.Format("{0}?showwxpaytitle=1&openid={1}&total_fee={2}&attach={3}", PayConfig.PayUrl, openid, total_fee, attach);
                    Response.Redirect(url);
                }
            }
            else
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");
                BtnSave.Visible = false;
            }
        }
    }
}
