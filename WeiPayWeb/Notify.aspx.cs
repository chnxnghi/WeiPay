using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.IO;
using System.Text;
using WeiPay;



namespace WeiPayWeb
{
    /**
  * 
  * 作用：支付完成以后通知页面，该页面实现数据库的更新操作，比如更新订单状态等等
  * 备注：请注意更新代码的填写位置
  * 
  * */
    public partial class Notify : System.Web.UI.Page
    {
        protected string message = "";
        string mcode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            mcode = this.Request.QueryString["mcode"];
            if (!string.IsNullOrEmpty(mcode))
            {
                mcode = EncryptHelper.XorEncryptStr(mcode);
                message = mcode;
                return;
            }
            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(PayConfig.AppKey);
            //判断签名
            try
            {
                string error = "";
                if (resHandler.isWXsign(out error))
                {
                    #region 协议参数=====================================
                    //--------------协议参数--------------------------------------------------------
                    string return_code = resHandler.getParameter("return_code");
                    string return_msg = resHandler.getParameter("return_msg");
                    string appid = resHandler.getParameter("appid");
                    //以下字段在 return_code 为 SUCCESS 的时候有返回--------------------------------
                    string mch_id = resHandler.getParameter("mch_id");
                    string device_info = resHandler.getParameter("device_info");
                    string nonce_str = resHandler.getParameter("nonce_str");
                    string result_code = resHandler.getParameter("result_code");
                    string err_code = resHandler.getParameter("err_code");
                    string err_code_des = resHandler.getParameter("err_code_des");

                    //以下字段在 return_code 和 result_code 都为 SUCCESS 的时候有返回---------------
                    string openid = resHandler.getParameter("openid");
                    string is_subscribe = resHandler.getParameter("is_subscribe");
                    string trade_type = resHandler.getParameter("trade_type");
                    string bank_type = resHandler.getParameter("bank_type");
                    string total_fee = resHandler.getParameter("total_fee");
                    string fee_type = resHandler.getParameter("fee_type");
                    string transaction_id = resHandler.getParameter("transaction_id");
                    string out_trade_no = resHandler.getParameter("out_trade_no");
                    string attach = resHandler.getParameter("attach");
                    string time_end = resHandler.getParameter("time_end");
                    #endregion
                    //支付成功
                    if (!out_trade_no.Equals("") && return_code.Equals("SUCCESS") && result_code.Equals("SUCCESS"))
                    {
                        /**
                         *  这里输入用户逻辑操作，比如更新订单的支付状态
                         *  这里可以发给用户信息操作
                         * **/
                        LogUtil.WriteLog("============ 支付成功 ===============");
                        //Response.Write("success");
                        var packageReqHandler = new RequestHandler(Context);
                        packageReqHandler.init();
                        packageReqHandler.setParameter("return_code", "SUCCESS");
                        packageReqHandler.setParameter("return_msg", "OK");
                        string data = packageReqHandler.parseXML();
                        LogUtil.WriteLog("Notify 页面  package（XML）：" + data);
                        Response.Write(data);
                    }
                    else
                    {
                        message = err_code + err_code_des;
                        LogUtil.WriteLog("Notify  支付失败 " + err_code + ":" + err_code_des);
                    }
                }
                else
                {
                    //这里为我们自己调用显示的内容
                    LogUtil.WriteLog("Notify 页面  isWXsign= false ，显示自定义消息：" + mcode);
                    
                }
            }
            catch (Exception ee)
            {
                LogUtil.WriteLog("Notify 页面  发送异常错误：" + ee.Message);
            }
        }
    }
}
