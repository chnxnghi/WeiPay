<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiPay.aspx.cs" Inherits="MyWxPay.WeiPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>注册码支付确认</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/lazyloadv3.js" type="text/javascript"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
</head>
    <script type="text/javascript">
        //调用微信JS api 支付
        function jsApiCall()
        {
            WeixinJSBridge.invoke(
            'getBrandWCPayRequest',
            <%=wxJsApiParam%>,//josn串
                    function (res)
                    {
                        document.getElementById("BtnSubmit").disabled = false;
                        if (res.err_msg == "get_brand_wcpay_request:ok")
                        {
                            window.location.href = "<%= MyWxPay.WxPayConfig.NOTIFY_URL + "?mcode=" + attach %>";
                        }
                    }
                    );
               }

               function callpay()
               {
                   if (typeof WeixinJSBridge == "undefined")
                   {
                       if (document.addEventListener)
                       {
                           document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                       }
                       else if (document.attachEvent)
                       {
                           document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                           document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                       }
                   }
                   else
                   {
                       document.getElementById("BtnSubmit").disabled = true;
                       jsApiCall();
                   }
               }
</script>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 2%;">您的机器码是：<%= attach %></div>
        <br />
        <div style="margin-left: 2%;">
            注意：本产品需要付费，费用为<b><span style="color:#f00">10元</span></b>。请点击下方“立即购买”完成支付，付款后自动显示注册码。（恒智天成官方收费，请您放心支付）
        </div>
        <br />
        <div align="center">
            <input id="BtnSubmit" type="button" value="立即购买" onclick="callpay()" Style="width: 210px; height: 50px; border-radius: 15px; background-color: #00CD00; border: 0px #FE6714 solid; cursor: pointer; color: white; font-size: 16px;"" />
        </div><br /><br />
    </form>
</body>
</html>

