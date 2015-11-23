using System;

namespace MyWxPay
{
    public partial class Send : System.Web.UI.Page
    {
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
                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                    BtnSave.Visible = false;
                }
            }
        }

        /// <summary>
        /// 提交支付信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string total_fee = WxPayConfig.TENPAY.ToString();
            if (ViewState["openid"] != null)
            {
                if (!string.IsNullOrEmpty(txtMCode.Text) && (txtMCode.Text.Trim().Length == 8) && string.Equals(txtMCode.Text, txtSure.Text, StringComparison.CurrentCulture))
                {
                    string openid = ViewState["openid"].ToString();
                    string attach = txtMCode.Text.Trim();
                    string url = String.Format("{0}?showwxpaytitle=1&openid={1}&total_fee={2}&attach={3}", WxPayConfig.PAY_URL, openid, total_fee, attach);
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
